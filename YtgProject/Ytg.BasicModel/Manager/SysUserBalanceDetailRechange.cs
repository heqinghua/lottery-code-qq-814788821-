using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.Manager
{
    public class SysUserBalanceDetailRechange
    {

        /// <summary>
        /// 对应用户
        /// </summary>
        public int UserId { get; set; }

        public SysUser User { get; set; }

        /// <summary>
        /// 交易号	这里有生成规则
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// 交易金额例： -10消费10块 10进账10块
        /// </summary>
        public decimal TradeAmt { get; set; }

        /// <summary>
        /// 消费前余额
        /// </summary>
        public decimal UserAmt { get; set; }


        /// <summary>
        /// 消费类型
        /// </summary>
        public TradeType TradeType { get; set; }

        /// <summary>
        /// 状态 如：充值成功与否 0成功，1失败
        /// </summary>
        public int Status { get; set; }

        public string RelevanceNo { get; set; }


        /// <summary>
        /// 操作用户id
        /// </summary>
        public int? OpUserId { get; set; }
        /// <summary>
        /// 操作用户
        /// </summary>
        public SysUser OpUser { get; set; }

        /// <summary>
        /// 充值银行Id
        /// </summary>
        public int? BankId { get; set; }

        public string NikeName { get; set; }

        public string Code { get; set; }

        public DateTime OccDate { get; set; }
    }
}
