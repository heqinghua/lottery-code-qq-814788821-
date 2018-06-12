using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class UserSessionMangerDto
    {
        /// <summary>
        /// 登录端
        /// </summary>
        public string LoginClient { get; set; }

        /// <summary>
        /// 统计数
        /// </summary>
        public int userCount { get; set; }
    }
}
