using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.Report
{
    /// <summary>
    /// 中奖记录List
    /// </summary>
    [Serializable, DataContract]
    public class PrizeList
    {
        /// <summary>
        /// 用户余额表Id，用于撤消派奖的
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 帐变编号
        /// </summary>
        [DataMember]
        public string SerialNo { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [DataMember]
        public DateTime OccDate { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        [DataMember]
        public decimal TradeAmt { get; set; }

        /// <summary>
        /// 用户余额
        /// </summary>
        [DataMember]
        public decimal UserAmt { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        [DataMember]
        public int TradeType { get; set; }

        /// <summary>
        /// 彩票
        /// </summary>
        [DataMember]
        public string LotteryName { get; set; }

        [DataMember]
        public string PlayTypeName { get; set; }
        [DataMember]
        public string PlayTypeNumName { get; set; }
        [DataMember]
        public string PlayTypeRadioName { get; set; }

        /// <summary>
        /// 期号
        /// </summary>
        [DataMember]
        public string IssueCode { get; set; }

        /// <summary>
        /// 投注编号
        /// </summary>
        [DataMember]
        public string BetCode { get; set; }

        /// <summary>
        /// 投注数
        /// </summary>
        [DataMember]
        public int BetCount { get; set; }

        /// <summary>
        /// 倍数
        /// </summary>
        [DataMember]
        public int Multiple { get; set; }

        /// <summary>
        /// 模式
        /// </summary>
        [DataMember]
        public int Model { get; set; }

        /// <summary>
        /// 返点类型
        /// </summary>
        [DataMember]
        public int PrizeType { get; set; }

        /// <summary>
        /// 返点
        /// </summary>
        [DataMember]
        public decimal BackNum { get; set; }

        /// <summary>
        /// 开奖结果
        /// </summary>
        [DataMember]
        public string OpenResult { get; set; }
    }
}
