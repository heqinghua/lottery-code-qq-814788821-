using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 锁定IP
    /// </summary>
    public class LockIpInfo : BaseEntity
    {
        public string Ip { get; set; }

        /// <summary>
        /// 锁定ip
        /// </summary>
        public string IpCityName { get; set; }

        /// <summary>
        /// 锁定原因
        /// </summary>
        public string LockReason { get; set; }

    }
}
