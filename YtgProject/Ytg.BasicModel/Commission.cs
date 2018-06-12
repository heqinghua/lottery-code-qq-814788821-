using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 佣金领取记录
    /// </summary>
    public class Commission : BaseEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 领取金额
        /// </summary>
        public decimal WinMonery { get; set; }

        /// <summary>
        /// 下级当日投注金额
        /// </summary>
        public decimal ChildrenByMonery { get; set; }

        /// <summary>
        /// 领取日
        /// </summary>
        public int OccDaty { get; set; }
    }
}
