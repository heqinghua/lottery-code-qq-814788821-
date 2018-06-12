using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 充值赠送记录 
    /// </summary>
    public class SendHister : BaseEntity
    {
        /// <summary>
        /// 当天时间
        /// </summary>
        public int OccDay { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 第一次充值金额
        /// </summary>
        public decimal RechangrMonery { get; set; }

        /// <summary>
        /// 是否已经领取过奖励
        /// </summary>
        public bool IsCompled { get; set; }

    }
}
