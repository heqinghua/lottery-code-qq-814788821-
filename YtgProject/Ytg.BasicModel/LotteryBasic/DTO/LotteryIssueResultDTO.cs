using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic.DTO
{
    public class LotteryResultDTO
    {
        /// <summary>
        /// 期号ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 彩种
        /// </summary>
        public string LotteryName { get; set; }

        /// <summary>
        /// 彩种编号
        /// </summary>
        public string LotteryCode { get; set; }

        /// <summary>
        /// 期数
        /// </summary>
        public string IssueCode { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 开奖数据
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 开奖时间
        /// </summary>
        public DateTime LotteryTime { get; set; }

        /// <summary>
        /// 投注金额
        /// </summary>
        public decimal BetMoney { get; set; }

        /// <summary>
        /// 中奖金额
        /// </summary>
        public decimal WinMoney { get; set; }

        /// <summary>
        /// 返点金额
        /// </summary>
        public decimal BackMoney { get; set; }
    }
}
