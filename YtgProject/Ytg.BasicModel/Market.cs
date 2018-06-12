using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 活动
    /// </summary>
    public class Market : DelEntity
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        [MaxLength(200), DataMember]
        public string MarketName { get; set; }

        /// <summary>
        /// 活动页面地址
        /// </summary>
        [MaxLength(200), DataMember]
        public string MarketPage { get; set; }

        /// <summary>
        /// 活动规则
        /// </summary>
        [MaxLength(1000), DataMember]
        public string MarketRule { get; set; }

        /// <summary>
        /// 活动类型
        ///Market1.首次注册赠XXX元
        ///Market2.每天消费满XXX可领取XXX元
        ///Market3.每天消费达到XXX元，可参与一次抽奖
        ///Market4.每次充值可反X%
        /// </summary>
        
        [MaxLength(20), DataMember]  
        public string MarketType { get; set; }

        /// <summary>
        /// 是否菜单
        /// </summary>
        public bool IsMenu { get; set; }

        /// <summary>
        /// 是否关闭活动
        /// </summary>
        public bool IsColse { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        [MaxLength(2000), DataMember]
        public string Description { get; set; }
    }
}
