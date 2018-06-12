using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic
{
    /// <summary>
    /// 玩法单选详细奖金
    /// </summary>
    public class PlayTypeRadiosBonus : BaseEntity
    {

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string BonusTitle { get; set; }

        /// <summary>
        /// 中奖注数
        /// </summary>
        [DataMember]
        public int BonusCount { get; set;}

        /// <summary>
        ///玩法单选编号
        /// </summary>
        [DataMember]
        public int RadioCode { get; set; }

        /// <summary>
        /// 1800 基础奖金
        /// </summary>
        [DataMember]
        public decimal BonusBasic { get; set; }

        /// <summary>
        /// 1800 最高奖金
        /// </summary>
        [DataMember]
        public decimal MaxBonus { get; set; }

        /// <summary>
        /// 1700 基础奖金
        /// </summary>
        [DataMember]
        public decimal BonusBasic17 { get; set; }

        /// <summary>
        /// 1700 最高奖金
        /// </summary>
        [DataMember]
        public decimal MaxBonus17 { get; set; }

        /// <summary>
        /// 增加0.1 ，增加多少奖金
        /// </summary>
        [DataMember]
        public decimal StepAmt { get; set; }

        /// <summary>
        /// 增加 0.1 1700 玩法增加多少奖金
        /// </summary>
        [DataMember]
        public decimal StepAmt1700 { get; set; }

    }
}
