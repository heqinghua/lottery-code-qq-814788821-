using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 提现排队
    /// </summary>
    public class MentionQueu : EnaEntity
    {
        public int UserId { get; set; }
        public int UserBankId { get; set; }

        /// <summary>
        /// 申请发起时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal Poundage { get; set; }

        /// <summary>
        /// 实际到账
        /// </summary>
        public decimal RealAmt { get; set; }

        /// <summary>
        /// 提现单号，需要将该值添入到余额明细表里面
        /// </summary>
        public string MentionCode { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal MentionAmt { get; set; }

        /// <summary>
        /// 排队人数,处理完一笔提现记录，需要将该值减1
        /// </summary>
        public int QueuNumber { get; set; }

        /// <summary>
        /// 状态：0 排队中 1提现成功 2提现失败 3 用户撤销
        /// </summary>
        public int Status { get; set; }


        /// <summary>
        /// 审核:0 未审核  1 已审核(提现金额大于等于五千才需审核)
        /// </summary>
        public int Audit { get; set; }

        /// <summary>
        /// 提现失败信息
        /// </summary>
        public string ErrorMsg { get; set; }

    }
}
