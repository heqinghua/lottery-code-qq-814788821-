using Newtonsoft.Json;
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
    /// 玩法
    /// </summary>
    public class PlayType : EnaEntity
    {
        public PlayType()
        {
            this.IsEnable = true;
        }

        [DataMember]
        public string PlayTypeName { get; set; }

        /// <summary>
        /// 彩票编码：拼音简称
        /// </summary>
        [MaxLength(20), DataMember]
        public string LotteryCode { get; set; }

        ///编码
        /// </summary>
        [DataMember]
        public int PlayCode { get; set; }

        /// <summary>
        /// 用于任选分组,空则无
        /// </summary>
        [MaxLength(50), DataMember]
        public string GroupName { get; set; }

        /// <summary>
        /// 是否为默认项
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否为最新
        /// </summary>
        public bool IsNew { get; set; }
    }
}
