using DinpayRSAAPI.COM.Dinpay.RsaUtils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.pay.zhifu
{
    public partial class DinpayToMer_notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        
            if (!IsPostBack)
            {
                //获取智付反馈信息
                string merchant_code = Request.Form["merchant_code"].ToString().Trim();
                string notify_type = Request.Form["notify_type"].ToString().Trim();
                string notify_id = Request.Form["notify_id"].ToString().Trim();
                string interface_version = Request.Form["interface_version"].ToString().Trim();
                string sign_type = Request.Form["sign_type"].ToString().Trim();
                string dinpaysign = Request.Form["sign"].ToString().Trim();
                string order_no = Request.Form["order_no"].ToString().Trim();
                string order_time = Request.Form["order_time"].ToString().Trim();
                string order_amount = Request.Form["order_amount"].ToString().Trim();
                string extra_return_param = Request.Form["extra_return_param"];
                string trade_no = Request.Form["trade_no"].ToString().Trim();
                string trade_time = Request.Form["trade_time"].ToString().Trim();
                string trade_status = Request.Form["trade_status"].ToString().Trim();
                string bank_seq_no = Request.Form["bank_seq_no"];

                /**
                 *签名顺序按照参数名a到z的顺序排序，若遇到相同首字母，则看第二个字母，以此类推，
                *参数名1=参数值1&参数名2=参数值2&……&参数名n=参数值n
                **/
                //组织订单信息
                string signStr = "";
                Ytg.Scheduler.Comm.LogManager.Info("signStr\n\t");
                if (null != bank_seq_no && bank_seq_no != "")
                {
                    signStr = signStr + "bank_seq_no=" + bank_seq_no.ToString().Trim() + "&";
                }

                if (null != extra_return_param && extra_return_param != "")
                {
                    signStr = signStr + "extra_return_param=" + extra_return_param + "&";
                }
                signStr = signStr + "interface_version=V3.0" + "&";
                signStr = signStr + "merchant_code=" + merchant_code + "&";


                if (null != notify_id && notify_id != "")
                {
                    signStr = signStr + "notify_id=" + notify_id + "&notify_type=" + notify_type + "&";
                }

                signStr = signStr + "order_amount=" + order_amount + "&";
                signStr = signStr + "order_no=" + order_no + "&";
                signStr = signStr + "order_time=" + order_time + "&";
                signStr = signStr + "trade_no=" + trade_no + "&";
                signStr = signStr + "trade_status=" + trade_status + "&";

                if (null != trade_time && trade_time != "")
                {
                    signStr = signStr + "trade_time=" + trade_time;
                }
                Ytg.Scheduler.Comm.LogManager.Info(signStr + "\n\t");
                if (sign_type == "RSA-S") //RSA-S的验签方法
                {

                    /**
					1)dinpay_public_key，智付公钥，每个商家对应一个固定的智付公钥（不是使用工具生成的密钥merchant_public_key，不要混淆），
					即为智付商家后台"公钥管理"->"智付公钥"里的绿色字符串内容
					2)demo提供的dinpay_public_key是测试商户号1111110166的智付公钥，请自行复制对应商户号的智付公钥进行调整和替换。
					*/

                    string dinpay_public_key = System.Configuration.ConfigurationManager.AppSettings["merchant_public_key"];// "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCKr1fqFErd5gvEJRclnCOqqK55JlCO67JZOOyvijVElMtNhRMDjAHCZWdzdl++L7lAIOxt5l8hmVDBeXj7zNPMZ170LZokL0f7niPa63zn9KF0eV59m+uddzi297GzAqXcdH13hktsVC4EkHNZqSB0I0S9o1D0XGzmvmU64Y7M0QIDAQAB";
                    //将智付公钥转换成C#专用格式
                    dinpay_public_key = testOrder.HttpHelp.RSAPublicKeyJava2DotNet(dinpay_public_key);
                   // Ytg.Scheduler.Comm.LogManager.Info(dinpay_public_key + "\n\t");
                    //验签
                    bool result = testOrder.HttpHelp.ValidateRsaSign(signStr, dinpay_public_key, dinpaysign);
                    Ytg.Scheduler.Comm.LogManager.Info(result + "  " + result + "\n\t");
                    if (result == true)
                    {
                        //如果验签结果为true，则对订单进行更新
                        //订单更新完之后打印SUCCESS
                        IRecordTempService recordService = IoC.Resolve<IRecordTempService>();
                        decimal dmTradeAmt;
                        Ytg.Scheduler.Comm.LogManager.Info(order_amount + "  " + order_amount + "\n\t");
                        if (!decimal.TryParse(order_amount, out dmTradeAmt))
                        {
                            Response.Write("非法请求！");
                            return;
                        }
                        Ytg.Scheduler.Comm.LogManager.Info(string.Format("接受订单请求：{0}", order_no));
                        int stauts;
                        var item = recordService.Compled_RecordTemp(order_no, trade_no, dmTradeAmt, trade_time, out stauts);
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
                                    messageService.Create(new BasicModel.Message()
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
                                    Response.Write("SUCCESS");
                                }
                                catch (Exception ex)
                                {
                                    Ytg.Scheduler.Comm.LogManager.Info(string.Format("处理订单异常：{0}", ex.Message));
                                }

                            }
                        }
                    }
                    else
                    {
                        //验签失败
                        Response.Write("验签失败");
                    }

                }
                else //RSA验签方法
                {
                    string merPubKeyDir = "D:/1111110166.pfx";
                    string password = "87654321";
                    RSAWithHardware rsaWithH = new RSAWithHardware();
                    rsaWithH.Init(merPubKeyDir, password, "D:/dinpayRSAKeyVersion");
                    bool result = rsaWithH.VerifySign("1111110166", signStr, dinpaysign);
                    if (result == true)
                    {
                        //如果验签结果为true，则对订单进行更新
                        //订单更新完之后必须打印SUCCESS来响应智付服务器以示商户已经正常收到智付服务器发送的异步数据通知，否则智付服务器将会在之后的时间内若干次发送同一笔订单的异步数据！！
                        Response.Write("SUCCESS");
                    }
                    else
                    {
                        //验签失败
                        Response.Write("验签失败");
                    }

                }
            }

        }


        /// <summary>
        /// 追加log
        /// </summary>
        /// <param name="appendText"></param>
        private void AppendLog(string appendText)
        {
            System.IO.File.AppendAllText(System.Configuration.ConfigurationManager.AppSettings["rechangeLogFile"], appendText + "\t\n");
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