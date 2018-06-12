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
    /// 首页新闻
    /// </summary>
    public class SysNews : DelEntity
    {
        
        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(100),DataMember]
        public string Title { get; set; }

        [ DataMember]
        public string Content { get; set; }

        /// <summary>
        /// 是否弹出提示 1为是 0为否
        /// </summary>
        [DataMember]
        public int IsShow { get; set; }
    }
}
