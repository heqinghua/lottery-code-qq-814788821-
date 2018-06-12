using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.ServerWeb.Page.Bank;

namespace Ytg.ServerWeb.Views.pay
{
    /// <summary>
    /// 处理在线充值逻辑 ,赠送金额
    /// </summary>
    public class WebRechangComm
    {
        /// <summary>
        /// 充值金额，  当天第一次充值
        /// </summary>
        /// <param name="userAmt"></param>
        /// <returns></returns>
        public static bool ManagerSend(decimal userAmt, int userid, string relevanceNo)
        {
            //验证当天是否领取过充值奖励
            ISendHisterService sendHisterService = IoC.Resolve<ISendHisterService>();
            if (sendHisterService.Where(x => x.OccDay == Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"))).Count() > 0)
            {
                return false;
            }
            ISysUserBalanceService balanceService = IoC.Resolve<ISysUserBalanceService>();//用户余额

            var activityMonery = RechargeConfig.AppendMonery(userAmt);//充值活动;

            if (activityMonery > 0)
            {
                //创建活动账变
                balanceService.UpdateUserBalance(new SysUserBalanceDetail()
                {
                    BankId = -1,
                    RelevanceNo = relevanceNo,
                    SerialNo = "d" + Utils.BuilderNum(),
                    Status = 0,
                    TradeAmt = activityMonery,
                    TradeType = Ytg.BasicModel.TradeType.充值活动,
                    UserAmt = activityMonery,
                    UserId = userid,
                }, activityMonery);

                //更新用户提款流水要求 赠送金额 10 倍流水
                decimal bili = 8;
                ISysUserService userServices = IoC.Resolve<ISysUserService>();
                decimal minOutMonery = (activityMonery + userAmt) * bili;
                if (userServices.UpdateUserMinMinBettingMoney(userid, minOutMonery) > 0)
                {
                    //更新用户提款流水要求
                    sendHisterService.Create(new SendHister()
                    {
                        OccDate = DateTime.Now,
                        OccDay = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")),
                        UserId = userid
                    });
                    sendHisterService.Save();
                    return true;
                }
            }

            return false;


            
        }
    }
}