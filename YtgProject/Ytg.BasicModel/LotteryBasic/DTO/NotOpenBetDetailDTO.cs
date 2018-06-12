using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic.DTO
{
    //未开奖
    public class NotOpenBetDetailDTO
    {

        /// <summary>
        /// 投注期号
        /// </summary>
        public string IssueCode { get; set; }

        /// <summary>
        ///  注单编号	(生成规则?)
        /// </summary>
        public string BetCode { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>

        public int UserId { get; set; }

        /// <summary>
        /// 投多少注 总注数
        /// </summary>
        public int BetCount { get; set; }


        /// <summary>
        /// 总共多少钱
        /// </summary>
        public decimal TotalAmt { get; set; }

        /// <summary>
        /// 玩法单选编号 PlayTypeRadios
        /// </summary>
        public int PalyRadioCode { get; set; }

        /// <summary>
        /// 倍数
        /// </summary>
        public int Multiple { get; set; }

        /// <summary>
        /// 模式 0元 1角 2 分
        /// </summary>
        public int Model { get; set; }

        /// <summary>
        /// 奖金类型
        /// </summary>
        public int PrizeType { get; set; }

        /// <summary>
        /// 返点	返点,百分数	
        /// </summary>
        public decimal BackNum { get; set; }

        /// <summary>
        /// 投注内容
        /// </summary>
        public string BetContent { get; set; }


        /// <summary>
        /// 中奖金额
        /// </summary>
        public decimal WinMoney { get; set; }

        /// <summary>
        /// 是否中奖
        /// </summary>
        public bool IsMatch { get; set; }

        /// <summary>
        /// 开奖号码
        /// </summary>
        public string OpenResult { get; set; }

        /// <summary>
        /// 状态：1 已中奖、2 未中奖、3 未开奖、4 已撤单
        /// </summary>
        public BetResultType Stauts { get; set; }

        /// <summary>
        /// 奖金级别 1700 or 1800
        /// </summary>
        public int BonusLevel { get; set; }

        /// <summary>
        /// 玩法
        /// </summary>
        public string PlayTypeRadioName { get; set; }


        /// <summary>
        /// 投注时间
        /// </summary>
        public DateTime OccDate { get; set; }

        /// <summary>
        /// 数据类型 0为投注数据1为追号数据
        /// </summary>
        public int BetType { get; set; }

        public string PostionName { get; set; }

    }
}
