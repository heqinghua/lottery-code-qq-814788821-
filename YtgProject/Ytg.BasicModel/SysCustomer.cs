using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 客服表
    /// </summary>
    public class SysCustomer : BaseEntity
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginCode { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnLine { get; set; }
    }
}
