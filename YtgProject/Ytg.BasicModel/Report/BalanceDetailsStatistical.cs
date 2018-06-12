using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.Report
{
    /// <summary>
    /// 用户余额统计报表 数据统计
    /// </summary>
    [Serializable, DataContract]
    public class BalanceDetailsStatistical
    {
        /// <summary>
        /// 交易时间
        /// </summary>
        [DataMember]
        public string Occdate { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        [DataMember]
        public decimal TradeAmt { get; set; }
    }
}
