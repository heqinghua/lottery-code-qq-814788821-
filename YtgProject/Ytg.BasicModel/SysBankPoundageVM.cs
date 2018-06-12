using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    public class SysBankPoundageVM
    {
        public int Id { get; set; }

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

        public bool IsDelete { get; set; }

        public DateTime OccDate { get; set; }
        public string FBankName { get; set; }

        public string TBankName { get; set; }

        public int TotalCount { get; set; }
    }
}
