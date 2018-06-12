using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 大转盘
    /// </summary>
    public class LargeRotary : BaseEntity
    {
        public int Uid { get; set; }

        /// <summary>
        /// 当日抽奖总次数
        /// </summary>
        public int ALlCount { get; set; }

        /// <summary>
        /// 剩余抽奖次数
        /// </summary>
        public int SubCount { get; set; }

    }
}
