using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 充值、提现手续费表
    /// </summary>
    [Table("SysYtgBankPoundage")]
    public class SysBankPoundage : DelEntity
    {
        public SysBankPoundage()
        { }

        /// <summary>
        /// 转账发起银行
        /// </summary>
        public int FromBankName { get; set; }

        /// <summary>
        /// 转账接收银行
        /// </summary>
        public int ToBankName { get; set; }

        /// <summary>
        /// 减免手续费最小限额
        /// </summary>
        public decimal LimitAmt { get; set; }

        /// <summary>
        /// 是否充值
        /// 充值：true
        /// 提现：false
        /// </summary>
        public bool IsRecharge { get; set; }

        /// <summary>
        /// 提现：需要至少几天连续投注
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 百分比
        /// </summary>
        public decimal Percent { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public int OpUser { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OpTime { get; set; }
    }
}
