using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 系统管理员账号
    /// </summary>
    public class SysAccount : BaseEntity
    {
        public SysAccount()
        {

        }

        /// <summary>
        /// 账号
        /// </summary>
        [MaxLength(10)]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(10)]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(50)]
        public string PassWord { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 上次登录IP
        /// </summary>
        public string PreLoginIp { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? PreLoginTime { get; set; }
    }
}
