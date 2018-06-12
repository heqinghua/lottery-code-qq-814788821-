using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;


namespace Ytg.BasicModel.LotteryBasic.DTO
{
    /// <summary>
    /// 投注实体
    /// </summary>
    public class BettingDTO
    {
        /// <summary>
        /// 投注详细数据
        /// </summary>
        public List<BetDetailDTO> BetDetails { get; set; }


        /// <summary>
        /// 中奖后是否自动停止追号
        /// </summary>
        public bool IsAutoStop { get; set; }

        /// <summary>
        /// 追号期数集合
        /// </summary>
        public List<CatchDto> CatchDtos { get; set; }
    }
}
