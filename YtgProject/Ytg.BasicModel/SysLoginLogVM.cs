using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 后台 用户访问数据
    /// </summary>
    [Serializable, DataContract]
    public class SysLoginLogVM
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public UserType UserType { get; set; }

        [DataMember]
        public string Ip { get; set; }

        [DataMember]
        public string UseSource { get; set; }

        [DataMember]
        public string ServerSystem { get; set; }

        [DataMember]
        public DateTime OccDate { get; set; }

        [DataMember]
        public string Descript { get; set; }
        

        /// <summary>
        /// 记录总数量
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }
    }
}
