using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.Act
{
    /// <summary>
    /// 签到
    /// </summary>
    public class Sign : BaseEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Uid { get; set; }

        /// <summary>
        /// 签到日期
        /// </summary>
        public int OccDay { get; set; }

        /// <summary>
        /// 是否已经返现给用户
        /// </summary>
        public bool IsBack { get; set; }

        /// <summary>
        /// 是否已经使用过大转盘了
        /// </summary>
        public bool IsDap { get; set; }
    }
}
