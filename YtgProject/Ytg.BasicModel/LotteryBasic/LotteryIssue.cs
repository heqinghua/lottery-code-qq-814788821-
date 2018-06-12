using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 彩票期数
    /// </summary>
    public class LotteryIssue : EnaEntity
    {
        public LotteryIssue()
        {
            
        }

        /// <summary>
        /// 期数如：20141019001	
        /// </summary>
        [MaxLength(100),DataMember]
        public string IssueCode { get; set; }

  
        /// <summary>
        /// 彩票类型id
        /// </summary>
       [DataMember]
        public int? LotteryId { get; set; }
        public virtual LotteryType Lottery { get; set; }

        /// <summary>
        /// 开始时间 期数
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间 期数
        /// </summary>
        [DataMember]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 开始销售时间
        /// </summary>
        [DataMember]
        public DateTime StartSaleTime { get; set; }

        /// <summary>
        /// 结束销售时间 在什么时间不允许在购买
        /// </summary>
        [DataMember]
        public DateTime? EndSaleTime { get; set; }

        /// <summary>
        /// 开奖结果
        /// </summary>
        [MaxLength(200), DataMember]
        public string Result { get; set; }

        /// <summary>
        /// 开奖时间
        /// </summary>
        [DataMember]
        public DateTime? LotteryTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500), DataMember]
        public string Remark { get; set; }


        public override string ToString()
        {

            return string.Format("IssueCode={0},EndTime={1},LotteryId={2}", IssueCode, EndTime, LotteryId);
        }
        
    }
}
