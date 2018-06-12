using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 系统操作日志
    /// </summary>
    public class SysOperationLog:BaseEntity
    {
        /// <summary>
        /// 操作用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 操作用户登录名
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 操作对象ID
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 操作动作
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        [MaxLength(200)]
        public string ClassName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        [MaxLength(200)]
        public string MethodName { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [MaxLength(50)]
        public string TableName { get; set; }

        /// <summary>
        /// 旧值
        /// </summary>
        [MaxLength(2000)]
        public string OldValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        [MaxLength(2000)]
        public string NewValue { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [MaxLength(50)]
        public string IpAddress { get; set; }
        
        /// <summary>
        /// 计算机名称
        /// </summary>
        [MaxLength(50)]
        public string ComputerName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(2000)]
        public string Description { get; set; }
    }
}
