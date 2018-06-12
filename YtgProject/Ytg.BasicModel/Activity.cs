using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 活动中心
    /// </summary>
    public class Activity : BaseEntity
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 活动url
        /// </summary>
        public string ActivityUrl { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int States { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
