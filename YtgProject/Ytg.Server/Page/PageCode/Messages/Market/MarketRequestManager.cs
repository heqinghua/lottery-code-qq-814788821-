using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
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
    public class MarketRequestManager : BaseRequestManager
    {
        private readonly ISysUserBalanceService mSysUserBalanceService;
        private readonly ISysUserBalanceDetailService mSysUserBalanceDetailService;
        private readonly IMarketService mMarketService;

        public MarketRequestManager(ISysUserBalanceDetailService sysUserBalanceDetailService, 
            ISysUserBalanceService sysUserBalanceService,
            IMarketService marketService)
        {
            mSysUserBalanceService = sysUserBalanceService;
            mSysUserBalanceDetailService = sysUserBalanceDetailService;
            mMarketService = marketService;
        }

        protected override bool Validation()
        {
            return true;
        }

        protected override void Process()
        {
            int uid = Request.Params["uid"] == null ? 0 : Convert.ToInt32(Request.Params["uid"]);
            decimal giftMoney = Request.Params["giftMoney"] == null ? 0 : Convert.ToDecimal(Request.Params["giftMoney"]);

            switch (this.ActionName)
            {
                //获取满赠活动
                case "GetMzMarket": 
                    GetMzMarket();
                    break;

                //获取奇兵夺宝活动
                case "GetQbdbMarket": 
                    GetQbdbMarket();
                    break;

                case "GetMarketMeun":
                    GetMarketMeun();
                    break;

                //参与满赠活动
                case "ParticipateMzMarket":  
                    //检查当天是否已经参与过
                    if (!mMarketService.MarketIsExist(uid, Convert.ToInt32(TradeType.满赠活动)))
                    {
                        if (AddMarketDetail(uid, giftMoney, TradeType.满赠活动))
                            AppGlobal.RenderResult(ApiCode.Success);
                        else
                            AppGlobal.RenderResult(ApiCode.Exception);
                    }
                    else
                    {
                        AppGlobal.RenderResult(ApiCode.MarketExist);
                    }
                    
                    break;

                //参与奇兵夺宝活动
                case "ParticipateQbdbMarket":
                    //检查抽检时间是否在活动范围之内
                    if (CheckDate())
                    {
                        ////检查当天是否已经参与过
                        //if (!mMarketService.MarketIsExist(uid, Convert.ToInt32(TradeType.奇兵夺宝)))
                        //{
                        //    if (AddMarketDetail(uid, giftMoney, TradeType.奇兵夺宝))
                        //        AppGlobal.RenderResult(ApiCode.Success);
                        //    else
                        //        AppGlobal.RenderResult(ApiCode.Exception);
                        //}
                        //else
                        //{
                        //    AppGlobal.RenderResult(ApiCode.MarketExist);
                        //}
                    }
                    else
                    {
                        AppGlobal.RenderResult(ApiCode.NotScope);
                    }
                    break;

                //检查当前用户，当天是否以参与当前活动
                case "Check":
                     int marketType = Convert.ToInt32(Request.Params["marketType"]);
                     if (MarketIsExist(uid, marketType))
                     {
                         AppGlobal.RenderResult(ApiCode.MarketExist);
                     }
                    break;
            }
        }

        /// <summary>
        /// 获取活动菜单
        /// </summary>
        public void GetMarketMeun()
        {
            var source = this.mMarketService.GetMarketByMeun();
            AppGlobal.RenderResult<IList<Ytg.BasicModel.Market>>(ApiCode.Success, source);
        }

        /// <summary>
        /// 获取满赠活动
        /// </summary>
        public void GetMzMarket()
        {
            var source = this.mMarketService.GetMzMarket();
            AppGlobal.RenderResult<Ytg.BasicModel.Market>(ApiCode.Success, source);
        }

        /// <summary>
        /// 获取奇兵夺宝活动
        /// </summary>
        public void GetQbdbMarket()
        {
            var source = this.mMarketService.GetQbdbMarket();
            AppGlobal.RenderResult<Ytg.BasicModel.Market>(ApiCode.Success, source);
        }

        /// <summary>
        /// 检查抽检时间是否在活动范围之内
        /// </summary>
        /// <returns></returns>
        private bool CheckDate()
        {
            Ytg.BasicModel.Market market = mMarketService.GetQbdbMarket();
            List<KeyValue> keyValueList = JsonConvert.DeserializeObject<List<KeyValue>>(market.MarketRule);
            //检查抽检时间是否在活动范围之内
            int currentHour = DateTime.Now.Hour;
            int currentMinute = DateTime.Now.Minute;
            DateTime morningBeginTime = Convert.ToDateTime(keyValueList.Where(m => m.key == "MorningBeginTime").FirstOrDefault().value);
            DateTime morningEndTime = Convert.ToDateTime(keyValueList.Where(m => m.key == "MorningEndTime").FirstOrDefault().value);
            DateTime afternoonBeginTime = Convert.ToDateTime(keyValueList.Where(m => m.key == "AfternoonBeginTime").FirstOrDefault().value);
            DateTime afternoonEndTime = Convert.ToDateTime(keyValueList.Where(m => m.key == "AfternoonEndTime").FirstOrDefault().value);

            if ((morningBeginTime.Hour == currentHour && currentMinute >= morningBeginTime.Minute && currentMinute <= morningEndTime.Minute)
                || (afternoonBeginTime.Hour == currentHour && currentMinute >= afternoonBeginTime.Minute && currentMinute <= afternoonEndTime.Minute))
                return true;
            return false;
        }

        /// <summary>
        /// 检查当前用户,当天是否参与活动
        /// </summary>
        /// <returns></returns>
        public bool MarketIsExist(int uid,int marketType)
        {
            return mMarketService.MarketIsExist(uid, marketType);
        }

        /// <summary>
        /// 检查当前活动是否关闭
        /// </summary>
        /// <param name="marketType">活动类型</param>
        /// <returns></returns>
        public bool MarketIsClose(string marketType)
        {
            return mMarketService.MarketIsClose(marketType);
        }


        /// <summary>
        /// 创建活动记录
        /// </summary>
        /// <param name="tradeType"></param>
        public bool AddMarketDetail(int uid,decimal giftMoney, TradeType tradeType)
        {
            try
            {
                //获取用户余额
                decimal userAmt = 0;
                var userbalance = this.mSysUserBalanceService.GetUserBalance(uid);
                if (null != userbalance)
                {
                    userAmt = userbalance.UserAmt;//获取用户余额
                    //保存用户余额
                    userbalance.UserAmt = userAmt + giftMoney;
                } 

                //投注消费记录
                this.mSysUserBalanceDetailService.Create(new SysUserBalanceDetail()
                {
                    OpUserId = uid,
                    SerialNo = "m" + DateTime.Now.ToString("MMddHHmmssff"),
                    Status = 0,
                    TradeAmt = giftMoney,
                    TradeType = tradeType,
                    UserAmt = userAmt,
                    UserId = uid
                });
                this.mSysUserBalanceDetailService.Save();
                return true;
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("AddMarketDetail", ex);
                return false;
            }
        }
    }
}