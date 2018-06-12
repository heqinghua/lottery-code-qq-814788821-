using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 后台管理功能 报表管理 结算报表、消费报表
    /// </summary>
    [Serializable, DataContract]
    public class SettlementReportVM
    {
        /// <summary>
        /// 类型：
        /// 结算报表：按月 就是月份，按日：就是日期
        /// 消费报表：用户帐号
        /// </summary>
        [DataMember]
        public string TypeName { get; set; }

        /// <summary>
        /// 活动礼金
        /// </summary>
        [DataMember]
        public decimal Huodonglijin { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        [DataMember]
        public decimal Chongzhijine { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        [DataMember]
        public decimal Tixianjine { get; set; }

        /// <summary>
        /// 投注总额
        /// </summary>
        [DataMember]
        public decimal Touzhu { get; set; }

        /// <summary>
        /// 返点总额
        /// </summary>
        [DataMember]
        public decimal Fandian { get; set; }

        /// <summary>
        /// 中奖总额
        /// </summary>
        [DataMember]
        public decimal Zhongjiang { get; set; }

        /// <summary>
        /// 分红总额
        /// </summary>
        [DataMember]
        public decimal Fenhong { get; set; }

        /// <summary>
        /// 总盈亏
        /// </summary>
        [DataMember]
        public decimal YingKui { get; set; }

        /// <summary>
        /// 总记录条数
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }
    }
}
