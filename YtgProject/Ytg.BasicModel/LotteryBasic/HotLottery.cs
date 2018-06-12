using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic
{
    /// <summary>
    /// 热门彩种
    /// </summary>
    [Serializable, DataContract]
    public class HotLottery : EnaEntity
    {
        ///// <summary>
        ///// 彩种类型编号
        ///// </summary>
        //[DataMember]
        //public int Id { get; set; }

        /// <summary>
        /// 彩种类型名称
        /// </summary>
        [DataMember]
        public string LotteryName { get; set; }

        /// <summary>
        /// 最近开奖期数
        /// </summary>
        [DataMember]
        public string IssueCode { get; set; }

        /// <summary>
        /// 最近开奖号
        /// </summary>
        [DataMember]
        public string OpenResult { get; set; }
    }
}
