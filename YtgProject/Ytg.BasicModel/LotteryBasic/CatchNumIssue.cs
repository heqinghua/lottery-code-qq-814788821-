using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic
{
    /// <summary>
    /// 追号期数表
    /// </summary>
    public class CatchNumIssue : BaseEntity
    {
        /// <summary>
        /// 追号期数编号
        /// </summary>
        [MaxLength(100), DataMember]
        public string CatchNumIssueCode { get; set; }

        /// <summary>
        ///  追号编号（关联）
        /// </summary>
        [MaxLength(100),DataMember]
        public string CatchNumCode { get; set; }

        /// <summary>
        /// 投注期号
        /// </summary>
        [DataMember]
        public string IssueCode { get; set; }


        /// <summary>
        /// 倍数
        /// </summary>
        [DataMember]
        public int Multiple { get; set; }

        /// <summary>
        /// 本次追号花费多少钱
        /// </summary>
        [DataMember]
        public decimal TotalAmt { get; set; }


        /// <summary>
        /// 状态：1 已中奖、2 未中奖、3 未开奖、4 已撤单
        /// </summary>
        [DataMember]
        public BetResultType Stauts { get; set; }

        /// <summary>
        /// 中奖金额
        /// </summary>
        [DataMember]
        public decimal WinMoney { get; set; }

        /// <summary>
        /// 是否中奖
        /// </summary>
        [DataMember]
        public bool IsMatch { get; set; }

        /// <summary>
        /// 开奖号码
        /// </summary>
        [DataMember]
        public string OpenResult { get; set; }

        /// <summary>
        /// 本期销售结束时间
        /// </summary>
        public DateTime? IssueStartTime { get; set; }

        /// <summary>
        /// 本期销售结束时间
        /// </summary>
        public DateTime? IssueEndTime { get; set; }

        /// <summary>
        /// 彩种编号
        /// </summary>
        public int LotteryId { get; set; }
       

    }
}
