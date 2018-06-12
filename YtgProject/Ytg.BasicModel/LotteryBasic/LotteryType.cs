using Newtonsoft.Json;
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
    /// 彩票类型
    /// </summary>
    public class LotteryType : EnaEntity
    {
        public LotteryType()
        {
            this.Sort = 0;
            this.SplitBettingSecond = 30;
        }

       
        /// <summary>
        /// 类型名称
        /// </summary>
        [MaxLength(100), DataMember]
        public string LotteryName { get; set; }

        /// <summary>
        /// 彩票编码：拼音简称
        /// </summary>
        [MaxLength(20), DataMember]
        public string LotteryCode { get; set; }

        /// <summary>
        /// 一共多少期
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500), DataMember]
        public string Remark { get; set; }

        /// <summary>
        /// 菜单图片
        /// </summary>
        [DataMember]
        public string ImageSource { get; set; }

        /// <summary>
        /// xap路径
        /// </summary>
        [DataMember]
        public string XapUri { get; set; }

        /// <summary>
        /// 入口全名称名称控件.类
        /// </summary>
        [DataMember]
        public string FullName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DataMember]
        public int Sort { get; set; }


        /// <summary>
        /// 投注间隔
        /// </summary>
        [DataMember]
        public int SplitBettingSecond { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        [DataMember]
        public int GroupName { get; set; }


        /// <summary>
        /// 开始销售时间
        /// </summary>
        public TimeSpan? BeginScallDate { get; set; }


        /// <summary>
        /// 结束销售时间
        /// </summary>
        public TimeSpan? endSAcallDate { get; set; }

    }
}
