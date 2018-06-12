using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class LotteryIssueDTO_Opens
    {
        /// <summary>
        /// 期数如：20141019001	
        /// </summary>
 
        public string IssueCode { get; set; }


        /// <summary>
        /// 彩票类型id
        /// </summary>

        public int? LotteryId { get; set; }

        /// <summary>
        /// 开始时间 期数
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间 期数
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 开始销售时间
        /// </summary>
   
        public DateTime StartSaleTime { get; set; }

        /// <summary>
        /// 结束销售时间 在什么时间不允许在购买
        /// </summary>
   
        public DateTime? EndSaleTime { get; set; }

        /// <summary>
        /// 开奖结果
        /// </summary>
        
        public string Result { get; set; }

        /// <summary>
        /// 开奖时间
        /// </summary>
  
        public DateTime? LotteryTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        
        public string Remark { get; set; }

        public virtual int Id { get; set; }

        public string LotteryName { get; set; }

    }
}
