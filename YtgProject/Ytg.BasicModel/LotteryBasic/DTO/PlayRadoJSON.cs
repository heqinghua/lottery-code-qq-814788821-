using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic.DTO
{
    /// <summary>
    /// 玩法单选
    /// </summary>
    [Serializable]
    public class PlayRadoJSON
    {
        public int Id { get;set; }



        /// <summary>
        /// 单选名称
        /// </summary>

        public string PlayTypeRadioName { get; set; }

        /// <summary>
        /// 奖金基数
        /// </summary>

        public decimal BonusBasic { get;set;}


        /// <summary>
        /// 舍弃返点，最高奖金
        /// </summary>
        [DataMember]
        public decimal MaxBonus { get; set; }

        #region 1700奖金
        /// <summary>
        /// 奖金基数 1700
        /// </summary>
        [DataMember]
        public decimal BonusBasic17 { get; set; }

        /// <summary>
        /// 舍弃返点，最高奖金 1700
        /// </summary>
        [DataMember]
        public decimal MaxBonus17 { get; set; }
        #endregion 

        /// <summary>
        /// 是否启用
        /// </summary>

        public bool IsEnable { get; set; }


        /// <summary>
        /// 隶属玩法
        /// </summary>
        public int NumCode { get; set; }

        public int RadioCode { get; set; }



    }
}
