using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 首页Banner图片表
    /// </summary>
    public class Banner : DelEntity
    {
        /// <summary>
        /// 图片标题
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// 图片文件名
        /// </summary>
        [DataMember]
        public string FileName { get; set; }
    }
}
