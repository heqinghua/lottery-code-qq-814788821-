using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic.DTO
{
    public class CatchNumJsonDTO
    {

        /// <summary>
        ///  追号编号
        /// </summary>
        public string CatchNumCode { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>

        public int UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string Code { get; set; }


        /// <summary>
        /// 投多少注 总注数
        /// </summary>
        public int BetCount { get; set; }


        /// <summary>
        /// 玩法单选编号 PlayTypeRadios
        /// </summary>
        public int PalyRadioCode { get; set; }

        /// <summary>
        /// 追号总金额
        /// </summary>
        public decimal SumAmt { get; set; }

        /// <summary>
        /// 模式 0元 1角 2 分
        /// </summary>
        public int Model { get; set; }

        /// <summary>
        /// 奖金类型
        /// </summary>
        public int PrizeType { get; set; }

        /// <summary>
        /// 返点返点,百分数	
        /// </summary>
        public decimal BackNum { get; set; }

        /// <summary>
        /// 投注内容
        /// </summary>
        public string BetContent { get; set; }

        /// <summary>
        /// 本次追号状态
        /// </summary>
        public CatchNumType Stauts { get; set; }

        /// <summary>
        /// 奖金级别 1700 or 1800
        /// </summary>
        public int BonusLevel { get; set; }

        /// <summary>
        /// 中奖后是否自动停止追号
        /// </summary>
        public bool IsAutoStop { get; set; }

        /// <summary>
        /// 开始期数
        /// </summary>
        public string BeginIssueCode { get; set; }

        /// <summary>
        /// 追号期数
        /// </summary>
        public int CatchIssue { get; set; }

        /// <summary>
        /// 完成期数
        /// </summary>
        public int CompledIssue { get; set; }

        /// <summary>
        /// 完成金额
        /// </summary>
        public decimal CompledMonery { get; set; }


        /// <summary>
        /// 游戏名称
        /// </summary>
        public string LotteryName { get; set; }

        /// <summary>
        /// 玩法名称
        /// </summary>
        public string PlayTypeRadioName { get; set; }

        public DateTime OccDate { get; set; }


        /// <summary>
        /// 中间金额
        /// </summary>
        public decimal WinMoney { get; set; }

        /// <summary>
        /// 中奖期数
        /// </summary>
        public int WinIssue { get; set; }


        /// <summary>
        /// 用户取消期数
        /// </summary>
        public int UserCannelIssue { get; set; }

        /// <summary>
        /// 用户取消金额
        /// </summary>
        public decimal UserCannelMonery { get; set; }

        /// <summary>
        /// 中奖后取消期数
        /// </summary>
        public int SysCannelIssue { get; set; }

        public string PlayTypeName { get; set; }

        public string PostionName { get; set; }

    }
}
