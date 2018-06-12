using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Scheduler.Comm;

namespace Ytg.ServerWeb.Page.Bank
{
    /// <summary>
    /// 用于接受my18充值请求
    /// 
    /// </summary>
    public partial class ytg_user_recharge : System.Web.UI.Page
    {
        
        /*
        MY18DT=到帐时间
        MY18oid=支付宝/财付通/网银交易流水号
        MY18JYF=打款人
        MY18FY=附言内容
        MY18M=打款金额
        MY18HF=手续费
        MY18SKR=收款人
        MY18PT=支付方式
         * ?MY18DT=2015/06/09&MY18oid=1321611&MY18JYF=和清华&MY18FY=31&MY18M=50&MY18HF=0&MY18SKR=ss&MY18PT=招商银行
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            string MY18DT = Request.Params["MY18DT"];//到帐时间
            string MY18oid = Request.Params["MY18oid"];//支付宝/财付通/网银交易流水号
            string MY18JYF = Request.Params["MY18JYF"];//打款人
            string MY18FY = Request.Params["MY18FY"];//附言内容
            string MY18M = Request.Params["MY18M"];//打款金额
            string MY18HF = Request.Params["MY18HF"];//手续费
            string MY18SKR = Request.Params["MY18SKR"];//收款人
            string MY18PT = Request.Params["MY18PT"];//支付方式

            LogManager.Info(string.Format("MY18DT={0} MY18oid={1}  MY18JYF={2}  MY18FY={3} MY18M={4}  MY18HF={5}  MY18SKR={6}  MY18PT={7}", MY18DT, MY18oid, MY18JYF, MY18FY, MY18M, MY18HF, MY18SKR, MY18PT));

            string ip = Utils.GetIp();
            LogManager.Info(ip);
            if (!YtgConfig.mYtg_User_RechargeIps.Split(',').Contains(ip))//不是允许访问的ip地址列表，怎不做任何处理
                return;
            int ourMY18FY;
            if (!string.IsNullOrEmpty(MY18DT) && !string.IsNullOrEmpty(MY18oid) && int.TryParse(MY18FY, out ourMY18FY))
            {
                IRecordTempService recordService = IoC.Resolve<IRecordTempService>();
                var tempItem = recordService.Get(ourMY18FY);
                if (null == tempItem || tempItem.IsCompled)
                {
                    return;
                }
                
                tempItem.MY18DT = MY18DT;
                tempItem.MY18oid = MY18oid;
                tempItem.MY18JYF = MY18JYF;
                tempItem.MY18FY = MY18FY;
                tempItem.MY18M = MY18M;
                tempItem.MY18HF = MY18HF;
                tempItem.MY18SKR = MY18SKR;
                tempItem.MY18PT = MY18PT;


                bool isCompled = true;

                //验证充值金额是否和提交订单金额一致
                decimal MY18HFDec;
                if (decimal.TryParse(MY18M, out MY18HFDec))
                {
                    if (MY18HFDec == tempItem.TradeAmt)
                    {
                        //订单完成，充值金额和订单金额一致
                        ISysUserBalanceService balanceService = IoC.Resolve<ISysUserBalanceService>();//用户余额
                        var balanceitem = balanceService.GetUserBalance(tempItem.UserId);

                        try
                        {
                            //更新用户账变
                            balanceService.UpdateUserBalance(new Ytg.BasicModel.SysUserBalanceDetail()
                            {
                                BankId = tempItem.BankId,
                                RelevanceNo = tempItem.Id.ToString(),
                                SerialNo = "m" + Utils.BuilderNum(),
                                Status = 0,
                                TradeAmt = tempItem.TradeAmt,
                                TradeType = Ytg.BasicModel.TradeType.用户充值,
                                UserAmt = balanceitem.UserAmt,
                                UserId = tempItem.UserId,
                            }, tempItem.TradeAmt);
                            tempItem.IsCompled = true;
                            recordService.Save();

                            isCompled = Ytg.ServerWeb.Page.PageCode.UserComm.ManagerRecharge(tempItem.TradeAmt, tempItem.UserId);
                            
                        }
                        catch (Exception ex)
                        {
                            isCompled = false;
                            LogManager.Error("MY18充值失败", ex);
                        }

                        try
                        {
                            if (tempItem.IsCompled)
                            {
                                IMessageService messageService = IoC.Resolve<IMessageService>();
                                messageService.Create(new Message()
                                {
                                    FormUserId = -1,
                                    MessageType = 8,
                                    OccDate = DateTime.Now,
                                    Status = 0,
                                    Title = "充值成功提示",
                                    MessageContent = string.Format("您通过在线充值{0}元已经成功到账！", tempItem.TradeAmt),
                                    ToUserId = balanceitem.UserId
                                });
                                messageService.Save();
                            }
                        }
                        catch (Exception ex)
                        {
                            isCompled = false;
                            LogManager.Error("MY18充值失败消息插入失败", ex);
                        }
                    }
                    else
                    {
                        isCompled = false;
                    }
                }
                else
                {
                    isCompled = false;
                }

                if (isCompled == true)
                {
                    //插入系统消息表
                    Response.Redirect("notify.aspx", true);     // 回写‘SUCCESS’方式一： 重定向到一个专门用于处理回写‘SUCCESS’的页面，这样可以保证输出内容中只有'SUCCESS'这个字符串。
                }
                else 
                {
                    Response.Write("充值失败，请联系在线客服确认是否充值成功！");
                }
            }
        }
    }
}