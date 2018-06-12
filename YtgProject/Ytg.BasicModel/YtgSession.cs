using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 用户登录session
    /// </summary>
    public class YtgSession : BaseEntity
    {
        /// <summary>
        /// session id
        /// </summary>
        [Newtonsoft.Json.JsonProperty]
        public string SessionId { get; set; }


        /// <summary>
        /// 用户id
        /// </summary>
        [Newtonsoft.Json.JsonProperty]
        public int UserId { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Newtonsoft.Json.JsonProperty]
        public string Code { get; set; }
    }
}
