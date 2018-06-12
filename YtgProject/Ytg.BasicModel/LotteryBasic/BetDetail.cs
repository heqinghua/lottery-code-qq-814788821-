using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic
{
    /// <summary>
    /// 投注明细
    /// </summary>
    public class BetDetail : BaseEntity
    {
        public BetDetail()
        {
            this.BetCount = 1;
            this.TotalAmt = 0;
            this.Multiple = 1;
            this.Model = 1;
            this.PrizeType = 1;
            this.BackNum = 0;
            this.Stauts = BetResultType.NotOpen;
            this.Bili = 0;
            this.Secrecy = 0;
            this.PartakeUserCount = 0;
            this.PartakeMonery = 0;
            this.GroupByState = 0;


        }

        /// <summary>
        /// 投注期号
        /// </summary>
        public string IssueCode { get; set; }

        /// <summary>
        ///  注单编号	(生成规则?)
        /// </summary>
        [MaxLength(100)]
        public string BetCode { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>

        public int UserId { get; set; }


        /// <summary>
        /// 用户
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public SysUser User { get; set; }

        /// <summary>
        /// 投多少注 总注数
        /// </summary>
        public int BetCount { get; set; }


        /// <summary>
        /// 总共多少钱
        /// </summary>
        public decimal TotalAmt { get; set; }


        /// <summary>
        /// 菜种编码
        /// </summary>
        [MaxLength(100)]
        public string LotteryCode { get; set; }

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
        /// 奖金类型 1 为有返点 0为无返点
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
        /// 位置信息
        /// </summary>
        public string PostionName { get; set; }


        /// <summary>
        /// 是否满足统计要求
        /// 投注注数不能超过最大注数的80%算入投注送礼包活动 1为允许统计 0为不允许统计
        /// </summary>
        public int HasState { get; set; }


        /// <summary>
        /// 是否已经使用当注领取礼包 默认为0，未领取
        /// </summary>
        public int IsUseState { get; set; }



        #region 合买20180430


        /// <summary>
        /// 购买类型 0:代购 1：合买
        /// </summary>
        public int IsBuyTogether { get; set; }

        /// <summary>
        /// 认购金额
        /// </summary>
        public decimal Subscription { get; set; }


        /// <summary>
        /// 保密设置 0:公开 1：参与可见 2:完全保密
        /// </summary>
        public int Secrecy { get; set; }


        /// <summary>
        /// 参与用户
        /// </summary>
        public int PartakeUserCount { get; set; }


        /// <summary>
        /// 已参与金额
        /// </summary>
        public decimal PartakeMonery { get; set; }


        /// <summary>
        /// 剩余金额
        /// </summary>
        public decimal SurplusMonery { get; set; }

        /// <summary>
        /// 完成比例
        /// </summary>
        public double Bili{ get; set; }


        /// <summary>
        /// 合买状态
        /// 0:未满员
        /// 1:已满员
        /// </summary>
        public int GroupByState { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("IssueCode:{0} BetCode:{1} TotalAmt:{2} LotteryCode:{3} WinMoney:{4} IsMatch:{5} OpenResult:{6} Stauts:{7}", IssueCode, BetCode, TotalAmt, LotteryCode, WinMoney, IsMatch, OpenResult, Stauts);
        }

    }
}
