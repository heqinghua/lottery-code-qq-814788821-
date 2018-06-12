using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 用户访问统计
    /// </summary>
    [Serializable, DataContract]
    public class SysUserLoginStatisticsVM
    {
        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]
        public string OccDate { get; set; }

        /// <summary>
        /// 会员数量
        /// </summary>
        [DataMember]
        public int MemberCount { get; set; }

        /// <summary>
        /// 代理数量
        /// </summary>
        [DataMember]
        public int ProxyCount { get; set; }

        /// <summary>
        /// 总代理数量
        /// </summary>
        [DataMember]
        public int BasicProyCount { get; set; }

        /// <summary>
        /// 登录数量总和
        /// </summary>
        [DataMember]
        public int SumCount { get; set; }

        /// <summary>
        /// 记录总数量
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }
    }
}
