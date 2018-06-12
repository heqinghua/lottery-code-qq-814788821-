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
    /// 用户余额
    /// </summary>
    public class SysUserBalance : BaseEntity
    {
        public SysUserBalance()
        {

        }

        /// <summary>
        /// 对应用户
        /// </summary>
        public int UserId { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public SysUser User { get; set; }

        /// <summary>
        /// 资金密码
        /// </summary>
        [MaxLength(50), Newtonsoft.Json.JsonIgnore]
        public string Pwd { get; set; }

        /// <summary>
        /// 用户余额
        /// </summary>
        [DataMember, ConcurrencyCheck]
        public decimal UserAmt { get; set; }

        /// <summary>
        /// 状态0 正常 1禁用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否开通vip充提功能
        /// </summary>
        public bool IsOpenVip { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OpTime { get; set; }

    }
}
