using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 彩票周期
    /// </summary>
    public class LotteryCycle : BaseEntity
    {
        /// <summary>
        /// 彩票Id
        /// </summary>
        public int? LotteryId { get; set; }

        /// <summary>
        /// 彩票
        /// </summary>
        public virtual LotteryType Lottery { get; set; }

        /// <summary>
        /// 开奖时间范围，从几点到几点。一般是一个特定的时间为一个周期
        /// 一种彩票可以有多个周期数据
        /// </summary>
        [DataMember]
        public string TimeStart { get; set; }

        [DataMember]
        public string TimeEnd { get; set; }

        /// <summary>
        /// 时间间隔，如：5分钟，已分钟为单位
        /// </summary>
        [DataMember]
        public int TimeInterval { get; set; }

        /// <summary>
        /// 销售提前多少分钟结束,如：2分钟，已分钟为单位
        /// </summary>
        [DataMember]
        public int BeforeSaleDeadline { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500), DataMember]
        public string Remark { get; set; }

    }
}
