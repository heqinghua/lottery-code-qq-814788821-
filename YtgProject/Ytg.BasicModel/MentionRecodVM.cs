using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 提现记录
    /// </summary>
    [Serializable, DataContract]
    public class MentionRecodVM
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

        [DataMember]
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 充值银行
        /// </summary>
        [DataMember]
        public string BankName { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [DataMember]
        public string ProvinceName { get; set; }


        /// <summary>
        /// 城市
        /// </summary>
        [DataMember]
        public string CityName { get; set; }

        /// <summary>
        /// 开户支行
        /// </summary>
        [DataMember]
        public string Branch { get; set; }

        /// <summary>
        /// 开户人
        /// </summary>
        [DataMember]
        public string BankOwner { get; set; }

        /// <summary>
        /// 银行帐号
        /// </summary>
        [DataMember]
        public string BankNo { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        [DataMember]
        public decimal MentionAmt { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        [DataMember]
        public decimal Poundage { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        [DataMember]
        public decimal TradeAmt { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public string StatusDes { get; set; }

        /// <summary>
        /// 关联号
        /// </summary>
        [DataMember]
        public string RelevanceNo { get; set; }

        [DataMember]
        public int TotalCount { get; set; }
    }
}
