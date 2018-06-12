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
    /// 公告管理
    /// </summary>
    public class SysNotice : DelEntity
    {
        /// <summary>
        /// 公告标题
        /// </summary>
        [MaxLength(200)]
        public string Title { get; set; }
        /// <summary>
        /// 公告内容
        /// </summary>
        [MaxLength(2000)]
        public string Content { get; set; }
        /// <summary>
        /// 公告类型
        /// </summary>
        public int NoticeType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 是否醒目
        /// </summary>
        public int IsHot { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public int OpUser { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OpTime { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishTime { get; set; }
    }
}
