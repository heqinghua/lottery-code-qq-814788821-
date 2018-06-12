using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic
{
    /// <summary>
    /// 中奖排行
    /// </summary>
    public class Rankings : BaseEntity
    {

        /// <summary>
        /// 昵称
        /// </summary>
        [DataMember]
        public string NikeName { get; set; }

        /// <summary>
        /// 中奖金额
        /// </summary>
        [DataMember]
        public decimal WinMonery { get; set; }
    }
}
