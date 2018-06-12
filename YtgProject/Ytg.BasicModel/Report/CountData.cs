using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.Report
{
    /// <summary>
    /// 报表汇总
    /// </summary>
    [Serializable, DataContract]
    public class CountData : EnaEntity
    {
        [DataMember]
        public UserData userData { get; set; }

        [DataMember]
        public IList<LotteryTypeData> lotteryTypeDataList { get; set; }

        [DataMember]
        public IList<PlayTypeRadioData> playTypeRadioDataList { get; set; }

        [DataMember]
        public IList<MonthData> monthDataList { get; set; }

        [DataMember]
        public List<ToDayData> toDayData { get; set; }

        [DataMember]
        public YesterDayData yesterDayData { get; set; }

        public IList<LotteryData> lotteryDataList { get; set; }
    }

    #region 彩种数据报表

    public class LotteryData
    {
        /// <summary>
        /// 彩种名称
        /// </summary>
        [DataMember]
        public string LotteryName { get; set; }

        /// <summary>
        /// 彩种编码
        /// </summary>
        [DataMember]
        public string LotteryCode { get; set; }


        /// <summary>
        /// 彩种数据报表
        /// </summary>
        [DataMember]
        public List<PlayTypeRadioData> playTypeRadioDataList { get; set; }
    }

    #endregion

    #region 用户数据报表

    /// <summary>
    /// 用户数据报表
    /// </summary>
    [Serializable, DataContract]
    public class UserData
    {
        /// <summary>
        /// 用户总数量
        /// </summary>
        [DataMember]
        public int UserCount { get; set; }

        /// <summary>
        /// 当天注册会员数量
        /// </summary>
        [DataMember]
        public int DayRegisterNum{get;set;}

        /// <summary>
        /// 代理数量
        /// </summary>
        [DataMember]
        public int ProxyNum { get; set; }

        /// <summary>
        /// 会员数量
        /// </summary>
        [DataMember]
        public int MemberNum { get; set; }

        /// <summary>
        /// 在线人数
        /// </summary>
        [DataMember]
        public int OnLineNum { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        [DataMember]
        public decimal RemainingBalance { get; set; }

        /// <summary>
        /// 本月注册人数
        /// </summary>
        public int MonRegisterNum { get; set; }

        /// <summary>
        /// 上月注册人数
        /// </summary>
        public int PreRegisterNum { get; set; }
    }

    #endregion

    #region 彩票数据报表实体

    /// <summary>
    /// 彩票数据报表实体
    /// </summary>
    [Serializable, DataContract]
    public class LotteryTypeData
    {
        /// <summary>
        /// 彩种名称
        /// </summary>
        [DataMember]
        public string LotteryName { get; set; }

        /// <summary>
        /// 投资金额
        /// </summary>
        [DataMember]
        public decimal BetMoney { get; set; }
    }

    #endregion

    #region 彩票单选数据报表实体

    /// <summary>
    /// 彩票单选数据报表实体
    /// </summary>
    [Serializable, DataContract]
    public class PlayTypeRadioData
    {
        [DataMember]
        public string LotteryCode { get; set; }

        [DataMember]
        public string PlayTypeNumName { get; set; }

        /// <summary>
        /// 单选玩法名称
        /// </summary>
        [DataMember]
        public string RadioName { get; set; }

        /// <summary>
        /// 单选投注金额
        /// </summary>
        [DataMember]
        public decimal BetMoney { get; set; }

        /// <summary>
        /// 单选投注注数
        /// </summary>
        [DataMember]
        public int BetNum { get; set; }
    }

    #endregion

    #region 月报表数据

    /// <summary>
    /// 月报表数据
    /// </summary>
    [Serializable, DataContract]
    public class MonthData
    {
        /// <summary>
        /// 年
        /// </summary>
        [DataMember]
        public int Year { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        [DataMember]
        public int Month { get; set; }

        /// <summary>
        /// 月投注金额
        /// </summary>
        [DataMember]
        public decimal BetMoney { get; set; }

        /// <summary>
        /// 月盈亏金额
        /// </summary>
        [DataMember]
        public decimal ProfitAndLossMoney { get; set; }
    }

    #endregion

    #region 当天报表数据

    /// <summary>
    /// 当天报表数据
    /// </summary>
    [Serializable, DataContract]
    public class ToDayData
    {
        /// <summary>
        /// 当天投注金额
        /// </summary>
        [DataMember]
        public decimal BetMoney { get; set; }

        /// <summary>
        /// 当天盈亏金额
        /// </summary>
        [DataMember]
        public decimal ProfitAndLossMoney { get; set; }

        public string OccDate { get; set; }
    }

    #endregion

    #region 昨天数据报表

    /// <summary>
    /// 昨天数据报表
    /// </summary>
    [Serializable, DataContract]
    public class YesterDayData
    {
        /// <summary>
        /// 昨天投注金额
        /// </summary>
        [DataMember]
        public decimal BetMoney { get; set; }

        /// <summary>
        /// 昨天盈亏金额
        /// </summary>
        [DataMember]
        public decimal ProfitAndLossMoney { get; set; }
    }

   #endregion
}
