using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.ServerWeb.Page.Bank;

namespace Ytg.ServerWeb.Views.pay.yiyoufu.pay
{
    public partial class Receive : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            String key = ConfigurationManager.AppSettings["ekakey"];//配置文件密钥
            //返回参数
            String orderid = Request["orderid"];//返回订单号
            String opstate = Request["opstate"];//返回处理结果
            String ovalue = Request["ovalue"];//返回实际充值金额
            String sign = Request["sign"];//返回签名
            String ekaorderID = Request["sysorderid"];//亿卡录入时产生流水号。
            String ekatime = Request["systime"];//亿卡处理时间。
            String attach = Request["attach"];//上行附加信息
            String msg = Request["msg"];//亿卡返回订单处理消息
            //http://pay.cccwsm.cn/views/pay/yiyoufu/pay/Receive.aspx?orderid=R9C97BCE0F74C7467&opstate=0&ovalue=1&sysorderid=1604012306249990415&systime=2016-04-01+23%3a06%3a38&attach=&msg=&

            String param = String.Format("orderid={0}&opstate={1}&ovalue={2}{3}", orderid, opstate, ovalue, key);//组织参数
            //比对签名是否有效
            if (sign.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile(param, "MD5").ToLower()))
            {
                //执行操作方法
                if (opstate.Equals("0") || opstate.Equals("-3"))
                {
                    //操作流程成功的情况
                    //
                    //构建支付链接
                    Ytg.Scheduler.Comm.LogManager.Info(orderid + "  " + opstate);
                    //根据订单唯一id获取订单信息
                    IRecordTempService recordService = IoC.Resolve<IRecordTempService>();
                    var item = recordService.GetAll().Where(x => x.MY18FY == orderid && x.IsCompled == false && x.IsEnable).FirstOrDefault();
                    if (null == item)
                    {
                        Response.Write("请不要重复提交订单！");
                        return;
                    }
                    decimal dmTradeAmt;
                    if (!decimal.TryParse(ovalue, out dmTradeAmt))
                    {
                        Response.Write("非法请求！");
                        return;
                    }
                    if (item.TradeAmt != dmTradeAmt)
                    {
                        Response.Write("非法请求！");
                    }
                    //处理订单
                    item.IsCompled = true;
                    item.MY18oid = ekaorderID;
                    item.MY18M = ovalue;
                    item.MY18DT = ekatime;
                    //增加用户余额，处理充值逻辑
                    if (ManagerCallBackLogic(item))
                    {
                        recordService.Save();//保存
                        //逻辑处理完成，跳转
                        // Response.Redirect("notify.aspx", true);     // 回写‘SUCCESS’方式一： 重定向到一个专门用于处理回写‘SUCCESS’的页面，这样可以保证输出内容中只有'SUCCESS'这个字符串。
                        Response.Write("恭喜您，充值成功！");
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
                    }
                    else
                    {
                        // AppendLog("充值失败：请求参数：" + srcString);
                        Response.Write("充值失败，请联系在线客服确认是否充值成功！");
                    }

                }
                else if (opstate.Equals("-1"))
                {
                    //卡号密码错误
                    Response.Write("卡号密码错误！");
                }
                else if (opstate.Equals("-2"))
                {
                    //卡实际面值和提交时面值不符，卡内实际面值未使用
                    Response.Write("卡实际面值和提交时面值不符，卡内实际面值未使用！");
                }
                else if (opstate.Equals("-4"))
                {
                    //卡在提交之前已经被使用
                    Response.Write("卡在提交之前已经被使用！");
                }
                else if (opstate.Equals("-5"))
                {
                    //失败，原因请查看msg

                }
            }
            else
            {
                //签名无效
                Response.Write("非法请求！");
            }
        }

        /// <summary>
        /// 充值赠送
        /// </summary>
        private bool ManagerCallBackLogic(RecordTemp tempItem)
        {
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
                    var activityMonery = RechargeConfig.AppendMonery(tempItem.TradeAmt);//充值活动;
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
                    isCompled = Ytg.ServerWeb.Page.PageCode.UserComm.ManagerRecharge(tempItem.TradeAmt, tempItem.UserId);
                }
            }

            return isCompled;
        }
    }
}