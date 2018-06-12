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
    /// 系统设置表
    /// </summary>
    public class SysSetting : BaseEntity
    {
        /// <summary>
        /// 设置key
        /// </summary>
        [MaxLength(100), DataMember]
        public string Key { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>   
        [MaxLength(1000), DataMember]
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
