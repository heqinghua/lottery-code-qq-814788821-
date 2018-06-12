using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic.DTO
{
    /// <summary>
    /// 追号记录查询条件
    /// </summary>
    public class FilterCatchNumListParamerDTO
    {
        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 查询结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 游戏编号
        /// </summary>
        public string LotteryCode { get; set; }

        /// <summary>
        /// 游戏玩法编号
        /// </summary>
        public int PalyRadioCode { get; set; }

        /// <summary>
        /// 彩票期数
        /// </summary>
        public string IssueCode { get; set; }

        /// <summary>
        /// 游戏模式 元 分 角
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// 追号编号
        /// </summary>
        public string CatchNumCode { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string PalyUserCode { get; set; }


        /// <summary>
        /// 查询用户范围
        /// </summary>
        public int UserScope { get; set; }

        /// <summary>
        /// 交易类型 0为当天交易
        /// </summary>
        public int tradeType { get; set; }
    }
}
