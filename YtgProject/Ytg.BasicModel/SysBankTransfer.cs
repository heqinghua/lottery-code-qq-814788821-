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
    /// 转账设置表
    /// </summary>
    [Table("SysYtgBankTransfer")]
    public class SysBankTransfer : EnaEntity
    {
        public SysBankTransfer()
        { }

        /// <summary>
        /// 银行名称
        /// </summary>
        public int? BankId { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public decimal MinAmt { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public decimal MaxAmt { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal Poundage { get; set; }

        /// <summary>
        /// 每天开始时间
        /// </summary>
        [MaxLength(10)]
        public string BeginTime { get; set; }

        /// <summary>
        /// 每天结束时间
        /// </summary>
        [MaxLength(10)]
        public string EndTime { get; set; }

        /// <summary>
        /// 是否充值
        /// 提现：false
        ///充值：true
        /// </summary>
        public bool IsRecharge { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public int OpUser { get; set; }

        /// <summary>
        /// VIP 最大金额
        /// </summary>
        public decimal VipMaxAmt { get; set; }

        /// <summary>
        /// VIP最小金额
        /// </summary>
        public decimal VipMinAmt { get; set; }
    }
}
