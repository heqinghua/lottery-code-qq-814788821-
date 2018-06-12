using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Ytg.Core.Service;
using Ytg.Comm;
using System.Linq;
using Ytg.BasicModel;
using Ytg.ServerWeb.Page.Bank;

namespace com.mobaopay.merchant
{
    public partial class Callback : System.Web.UI.Page
    {
        public string apiName;
        public string notifyTime;
        public string tradeAmt;
        public string merchNo;
        public string merchParam;
        public string orderNo;
        public string tradeDate;
        public string accNo;
        public string accDate;
        public string orderStatus;
        public string veryfyDesc;
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            {
                //此处可增加一个日志来记录通知数据，便于调试接口。

                Dictionary<string, string> dict = GetRequestPost();
                // 判断是否有带返回参数
                if (dict.Count > 0)
                {
                    // 验证签名，先获取到签名源字符串和签名字符串后，做签名验证。
                    string srcString = string.Format("apiName={0}&notifyTime={1}&tradeAmt={2}&merchNo={3}&merchParam={4}&orderNo={5}&tradeDate={6}&accNo={7}&accDate={8}&orderStatus={9}",
                            dict["apiName"],
                            dict["notifyTime"],
                            dict["tradeAmt"],
                            dict["merchNo"],
                            dict["merchParam"],
                            dict["orderNo"],
                            dict["tradeDate"],
                            dict["accNo"],
                            dict["accDate"],
                            dict["orderStatus"]);
                    string sigString = dict["signMsg"];
                    string notifyType = dict["notifyType"];
                    //if (Int32.Parse(notifyType) == 1)
                    //{
                    //    sigString = System.Web.HttpUtility.UrlDecode(sigString);
                    //}
                    sigString = sigString.Replace("\r", "").Replace("\n", "");
                    bool verifyResult = MobaopaySignUtil.Instance.verifyData(sigString, srcString);
                    veryfyDesc = verifyResult ? "签名验证通过" : "签名验证失败";

                    // 取出用于显示的各个数据，这里只是为了演示，实际应用中应该不需要把这些数据显示到页面上。
                    apiName = dict["apiName"];
                    notifyTime = dict["notifyTime"];
                    tradeAmt = dict["tradeAmt"];        //交易金额
                    merchNo = dict["merchNo"];          //商户号
                    merchParam = dict["merchParam"];    //商户参数，来自支付请求中的商户参数，原物返回，方便商户异步处理需要传递数据
                    orderNo = dict["orderNo"];          //商户订单号
                    tradeDate = dict["tradeDate"];      //商户交易日期
                    accNo = dict["accNo"];              //支付平台订单号
                    accDate = dict["accDate"];          //支付平台订单日期
                    orderStatus = dict["orderStatus"];  //订单状态：0-未支付，1-成功，2-失败；实际上只有成功才会发送通知

                    if (verifyResult)
                    {
                        /**
                         * 验证通过后，请在这里加上商户自己的业务逻辑处理代码.
                         * 比如：
                         * 1、根据商户订单号取出订单数据
                         * 2、根据订单状态判断该订单是否已处理（因为通知会收到多次），避免重复处理
                         * 3、比对一下订单数据和通知数据是否一致，例如金额等
                         * 4、接下来修改订单状态为已支付或待发货
                         * 5、...
                         */

                        try
                        {
                            //处理订单
                            if (orderStatus == "1") //支付成功
                            {
                                //构建支付链接
                                //根据订单唯一id获取订单信息
                                IRecordTempService recordService = IoC.Resolve<IRecordTempService>();
                                /**
                                  //处理订单
                                item.IsCompled = true;
                                item.MY18oid = accNo;
                                item.MY18M = tradeAmt;
                                item.MY18DT = accDate;
                                 */
                                decimal dmTradeAmt;
                                if (!decimal.TryParse(tradeAmt, out dmTradeAmt))
                                {
                                    Response.Write("非法请求！");
                                    return;
                                }
                                Ytg.Scheduler.Comm.LogManager.Info(string.Format("接受订单请求：{0}", orderNo));
                                int stauts;
                                var item = recordService.Compled_RecordTemp(orderNo, accNo, dmTradeAmt, accDate, out stauts);
                                Ytg.Scheduler.Comm.LogManager.Info(string.Format("查询订单状态：{0}", stauts));
                                if (stauts == -1)
                                {
                                    Response.Write("订单已处理成功！");
                                }
                                else if (stauts == -3)
                                {
                                    Response.Write("订单已过期！");
                                }
                                else if (stauts == -2)
                                {
                                    Response.Write("非法请求！");
                                }
                                else
                                {
                                    
                                    //获取当前数据
                                    Ytg.Scheduler.Comm.LogManager.Info(string.Format("处理订单逻辑：{0}", stauts));
                                    //增加用户余额，处理充值逻辑
                                    if (ManagerCallBackLogic(item))
                                    {
                                        recordService.Save();//保存
                                        //逻辑处理完成，跳转

                                        //Response.Write("恭喜您，充值成功！");
                                        //插入充值成功消息
                                        try
                                        {
                                            IMessageService messageService = IoC.Resolve<IMessageService>();
                                            messageService.Create(new Message()
                                            {
                                                FormUserId = -1,
                                                MessageType = 8,
                                                OccDate = DateTime.Now,
                                                Status = 0,
                                                Title = "充值成功提示",
                                                MessageContent = string.Format("您通过在线充值{0}元已经成功到账！", item.TradeAmt),
                                                ToUserId = item.UserId
                                            });
                                            messageService.Save();
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                        //插入系统消息表
                                        Response.Redirect("notify.aspx", true);     // 回写‘SUCCESS’方式一： 重定向到一个专门用于处理回写‘SUCCESS’的页面，这样可以保证输出内容中只有'SUCCESS'这个字符串。
                                    }
                                    else
                                    {
                                        AppendLog("充值失败：请求参数：" + srcString);
                                        Response.Write("充值失败，请联系在线客服确认是否充值成功！");
                                    }

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            AppendLog("充值处理异常：" + ex.Message + "  请求参数：" + srcString);
                            Response.Write("充值异常，请联系在线客服确认是否充值成功！");
                        }
                    }
                }
                else
                {
                    Response.Write("无通知参数");
                    AppendLog("无通知参数：");
                }
            }
        }

        /// <summary>
        /// 追加log
        /// </summary>
        /// <param name="appendText"></param>
        private void AppendLog(string appendText)
        {
            //System.IO.File.AppendAllText(System.Configuration.ConfigurationManager.AppSettings["rechangeLogFile"], appendText + "\t\n");
        }

        /// <summary>
        /// 充值赠送
        /// </summary>
        private bool ManagerCallBackLogic(RecordTemp tempItem) {
            bool isCompled = false;//是否完成
           
            if (tempItem.IsCompled)
            {
                //活动，获取充值返现
                IMarketService marjetService = IoC.Resolve<IMarketService>();
                //存储账变，修改用户余额
                ISysUserBalanceDetailService balanceDetailService = IoC.Resolve<ISysUserBalanceDetailService>();//账号变详情
                ISysUserBalanceService balanceService = IoC.Resolve<ISysUserBalanceService>();//用户余额
                var balanceitem = balanceService.GetUserBalance(tempItem.UserId);
                //修改用户余额
                if (balanceitem == null || balanceitem.Status == 1)
                {
                    isCompled = false;
                    AppendLog("用户资金被禁用，请联系在线客服！");
                }
                else
                {
                    //获取充值返现活动
                    //if (tempItem.TradeAmt >= 100)
                    //{
                    //    var czfx = marjetService.GetCzfxMarket();
                    //    if (!czfx.IsColse)
                    //    {
                    //        var guize = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KeyValue>>(czfx.MarketRule);
                    //        if (null != guize && guize.Count > 0)
                    //        {
                    //            var percentage = guize.Where(c => c.key == "Percentage").FirstOrDefault();
                    //            double bacnNum;
                    //            if (double.TryParse(percentage.value, out bacnNum))
                    //            {
                    //                tempItem.TradeAmt += tempItem.TradeAmt * Convert.ToDecimal((bacnNum / 100));
                    //            }
                    //        }
                    //    }
                    //}

                    var oldm = balanceitem.UserAmt;
                    //var activityMonery = RechargeConfig.AppendMonery(tempItem.TradeAmt);//充值活动;
                    // var activityOld = 0m;
                    //修改
                    //balanceitem.UserAmt += tempItem.TradeAmt;
                    //activityOld = balanceitem.UserAmt;
                    //balanceitem.UserAmt += activityMonery;
                    //balanceService.Save();
                    //var appendTradeAmt=tempItem.TradeAmt+activityMonery;
                    balanceService.UpdateUserBalance(new Ytg.BasicModel.SysUserBalanceDetail()
                    {
                        BankId = tempItem.BankId,
                        RelevanceNo = tempItem.Id.ToString(),
                        SerialNo = "d" + Utils.BuilderNum(),
                        Status = 0,
                        TradeAmt = tempItem.TradeAmt,
                        TradeType = Ytg.BasicModel.TradeType.用户充值,
                        UserAmt = oldm,
                        UserId = tempItem.UserId,
                    }, tempItem.TradeAmt);

                    //if (activityMonery > 0)
                    //{
                    //    //创建活动账变
                    //    balanceService.UpdateUserBalance(new SysUserBalanceDetail()
                    //    {
                    //        BankId = tempItem.BankId,
                    //        RelevanceNo = tempItem.Id.ToString(),
                    //        SerialNo = "d" + Utils.BuilderNum(),
                    //        Status = 0,
                    //        TradeAmt = activityMonery,
                    //        TradeType = Ytg.BasicModel.TradeType.充值活动,
                    //        UserAmt = activityOld,
                    //        UserId = tempItem.UserId,
                    //    }, activityMonery);
                    //}
                    //更新用户提款流水要求
                    //chongzhiBili
                    //double bili = 5;
                    //ISysSettingService settingService = IoC.Resolve<ISysSettingService>();
                    //var fs = settingService.GetAll().Where(x => x.Key == "chongzhiBili").FirstOrDefault();
                    //if (null != fs)
                    //{
                    //    if (!double.TryParse(fs.Value, out bili))
                    //        bili = 5;
                    //}
                    //ISysUserService userServices = IoC.Resolve<ISysUserService>();
                    //var minOutMonery=(tempItem.TradeAmt * (decimal)(bili / 100));
                    //if (userServices.UpdateUserMinMinBettingMoney(tempItem.UserId, minOutMonery) > 0)
                    //{
                    //    //更新用户提款流水要求
                    //    isCompled = true;
                    //}
                    //isCompled = Ytg.ServerWeb.Views.pay.WebRechangComm.ManagerSend(tempItem.TradeAmt, tempItem.UserId, tempItem.Id.ToString());
                    //if (!isCompled)
                    isCompled = Ytg.ServerWeb.Page.PageCode.UserComm.ManagerRecharge(tempItem.TradeAmt, tempItem.UserId);
                }
            }

            return isCompled;
        }

        private Dictionary<string, string> GetRequestPost()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            NameValueCollection coll = Request.Form;

            foreach (string s in coll.AllKeys)
            {
                dict.Add(s, coll.Get(s));
            }
            return dict;
        }
    }
}