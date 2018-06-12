using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 系统管理员登录日志
    /// </summary>
    public class SysAccountLog : BaseEntity
    {
        public SysAccountLog()
        {
        }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        [MaxLength(50)]
        public string Ip { get; set; }
        
        /// <summary>
        /// 登录设备
        /// </summary>
        [MaxLength(200)]
        public string ServerSystem { get; set; }

        /// <summary>
        /// 客户端
        /// </summary>
        [MaxLength(200)]
        public string UseSource { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Descript { get; set; }

    }
}
