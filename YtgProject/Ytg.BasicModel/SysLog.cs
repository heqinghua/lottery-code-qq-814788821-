using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public class SysLog : BaseEntity
    {
        public SysLog()
        {

        }

        /// <summary>
        /// 类型 0 为 登陆日志, 1为投注日志 2 投注日志 3 撤单日志 
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 关联标记
        /// </summary>
        public string ReferenceCode { get; set; }


        /// <summary>
        /// ip地址
        /// </summary>
        [MaxLength(50)]
        public string Ip { get; set; }


        /// <summary>
        /// 浏览器/客户端
        /// </summary>
        public string UseSource { get; set; }

        /// <summary>
        /// 操作系统
        /// </summary>
        public string ServerSystem { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Descript { get; set; }

        
    }
}
