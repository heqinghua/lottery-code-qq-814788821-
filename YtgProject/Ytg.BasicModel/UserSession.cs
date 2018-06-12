using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 用户登录session表
    /// </summary>
    public class UserSession : BaseEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }


        /// <summary>
        /// session id
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// 最后登录ip
        /// </summary>
        public string LoginIp { get; set; }

        /// <summary>
        /// 最后修改时间（登录时间）
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }


        /// <summary>
        /// 当前登录客户端类型
        /// </summary>
        public string LoginClient { get; set; }
    }
}
