using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 充值记录
    /// </summary>
    [Serializable, DataContract]
    public class RechargeRecodVM
    {
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// 交易号
        /// </summary>
        [DataMember]
        public string SerialNo { get; set; }

        /// <summary>
        /// 充值时间
        /// </summary>
        [DataMember]
        public DateTime OccDate { get; set; }

        /// <summary>
        /// 充值银行
        /// </summary>
        [DataMember]
        public string BankName { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        [DataMember]
        public decimal RechargeAmt { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        [DataMember]
        public decimal Poundage { get; set; }

        /// <summary>
        /// 上分金额
        /// </summary>
        [DataMember]
        public decimal TradeAmt { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public string StatusDes { get; set; }

        [DataMember]
        public int TotalCount { get; set; }
    }
}
