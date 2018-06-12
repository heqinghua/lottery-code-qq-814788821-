using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic.DTO
{
    /// <summary>
    /// 投注列表List 查询对象
    /// </summary>
    public class BetListDTO
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public int tradeType { get; set; }

        /// <summary>
        /// 彩票
        /// </summary>
        public string lotteryCode { get; set; }

        /// <summary>
        /// 玩法
        /// </summary>
        public int palyRadioCode { get; set; }

        /// <summary>
        /// 期数
        /// </summary>
        public string issueCode { get; set; }

        /// <summary>
        /// 模式
        /// </summary>
        public int model { get; set; }

        /// <summary>
        /// 投注编号
        /// </summary>
        public string betCode { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string userAccount { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int userType { get; set; }
    }
}
