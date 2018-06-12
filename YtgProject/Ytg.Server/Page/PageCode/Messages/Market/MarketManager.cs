using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.Page.PageCode.Market
{
    /// <summary>
    /// 活动
    /// </summary>
    public class MarketManager : BaseRequestManager
    {
        private readonly ISysUserBalanceService mSysUserBalanceService;
        private readonly ISysUserBalanceDetailService mSysUserBalanceDetailService;

        public MarketManager(ISysUserBalanceDetailService sysUserBalanceDetailService, ISysUserBalanceService sysUserBalanceService)
        {
            mSysUserBalanceService = sysUserBalanceService;
            mSysUserBalanceDetailService = sysUserBalanceDetailService;
        }

        protected override void Process()
        {
            switch (this.ActionName.ToLower())
            {
                case "addMarketDetail2":
                    AddMarketDetail(TradeType.满赠活动);
                    break;
                case "addMarketDetail3":
                    AddMarketDetail(TradeType.奇兵夺宝);
                    break;
            }
        }

        /// <summary>
        /// 创建活动记录
        /// </summary>
        /// <param name="tradeType"></param>
        private void AddMarketDetail(TradeType tradeType)
        {
            try
            {
                int userId = Convert.ToInt32(Request.Params["uid"].ToString());
                decimal giftMoney = Convert.ToDecimal(Request.Params["giftMoney"]);

                //获取用户余额
                decimal userAmt = 0;
                var userbalance = this.mSysUserBalanceService.GetUserBalance(userId);
                if (null != userbalance)
                    userAmt = userbalance.UserAmt;//获取用户余额

                //保存用户余额
                userbalance.UserAmt = userAmt + giftMoney;

                //投注消费记录s
                this.mSysUserBalanceDetailService.Create(CreateUserBalanceDetial(userId, giftMoney, tradeType, userAmt));
                this.mSysUserBalanceDetailService.Save();

                AppGlobal.RenderResult(ApiCode.Success);
            }
            catch (Exception ex)
            {
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

       
        /// <summary>
        /// 创建活动记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tradeAmt">交易金额</param>
        /// <param name="tp">交易类型</param>
        /// <param name="userAmt">交易前余额</param>
        /// <returns></returns>
        private SysUserBalanceDetail CreateUserBalanceDetial(int? uid, decimal tradeAmt, TradeType tp, decimal userAmt)
        {
            //扣除笨注彩票
            return new SysUserBalanceDetail()
            {
                OpUserId = uid,
                SerialNo = "m" + DateTime.Now.ToString("MMddHHmmssff"),
                Status = 0,
                TradeAmt = tradeAmt,
                TradeType = tp,
                UserAmt = userAmt,
                UserId = uid.Value
            };
        }
    }
}