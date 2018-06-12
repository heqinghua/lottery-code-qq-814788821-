using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 域名验证
    /// </summary>
    public class SiteDns:BaseEntity
    {

        /// <summary>
        /// 域名
        /// </summary>
        public string SiteDnsUrl { get; set; }

        /// <summary>
        /// 是否在自主注册中显示
        /// </summary>
        public bool IsShowAutoRegist { get; set; }
    
    }
}
