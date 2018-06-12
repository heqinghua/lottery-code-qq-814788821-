using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.Report
{
    /// <summary>
    /// 盈亏列表
    /// </summary>
    [Serializable, DataContract]
    public class ProfitLossList
    {
        /// <summary>
        /// 交易时间
        /// </summary>
        [DataMember]
        public string Occdate { get; set; }

        /// <summary>
        /// 充值
        /// </summary>
        [DataMember]
        public decimal Chongzhi { get; set; }

        /// <summary>
        /// 提现
        /// </summary>
        [DataMember]
        public decimal Tixian { get; set; }

        /// <summary>
        /// 投注
        /// </summary>
        [DataMember]
        public decimal Touzhu { get; set; }

        /// <summary>
        /// 游戏返点
        /// </summary>
        [DataMember]
        public decimal Youxifandian { get; set; }

        /// <summary>
        /// 奖金派送
        /// </summary>
        [DataMember]
        public decimal Jiangjinpaisong { get; set; }

        /// <summary>
        /// 活动礼金
        /// </summary>
        [DataMember]
        public decimal Huodonglijin { get; set; }


        /// <summary>
        /// 分红
        /// </summary>
        [DataMember]
        public decimal Fenhong { get; set; }

        /// <summary>
        /// 盈亏总额
        /// </summary>
        [DataMember]
        public decimal Yingkui { get; set; }
    }
}
