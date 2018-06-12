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
    /// 配额表
    /// </summary>
    public class SysQuota : DelEntity
    {

        /// <summary>
        /// 隶属用户
        /// </summary>
        [DataMember]
        public int SysUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public SysUser SysUser { get; set; }

        /// <summary>
        /// 配额类型
        /// </summary>
        [MaxLength(100), DataMember]
        public string QuotaType { get; set; }

        /// <summary>
        /// 配额当前数量
        /// </summary>
        [DataMember]
        public int MaxNum { get; set; }


    }
}
