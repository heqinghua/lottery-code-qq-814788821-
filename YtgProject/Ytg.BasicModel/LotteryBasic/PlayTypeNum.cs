using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 玩法类型编号
    /// </summary>
    public class PlayTypeNum : EnaEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string PlayTypeNumName { get; set; }


        /// <summary>
        /// 玩法编码
        /// </summary>
        [DataMember]
        public int PlayCode { get; set; }

        [DataMember]
        public int NumCode { get; set; }

        
    }
}
