using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 礼包领取记录表
    /// </summary>
    public class LiBaoEntity : BaseEntity
    {
        /// <summary>
        /// 用户id
        /// </summary>
           [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 领取时间 yyyyMMdd
            [DataMember]
        public int OccDay { get; set; }
          
    }
}
