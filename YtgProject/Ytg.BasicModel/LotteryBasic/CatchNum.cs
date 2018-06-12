using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic
{
    /// <summary>
    /// 追号表
    /// </summary>
    public class CatchNum : BaseEntity
    {
        public CatchNum()
        {
            this.CompledIssue=0;
            this.CompledMonery = 0;
        }

        /// <summary>
        ///  追号编号
        /// </summary>
        [MaxLength(100)]
        public string CatchNumCode { get; set; }

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
        /// 菜种编码
        /// </summary>
        [MaxLength(100)]
        public string LotteryCode { get; set; }

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
        /// 奖金类型 0返点,0 无返点最高奖金
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
        /// 完成金额
        /// </summary>
        public decimal CompledMonery { get; set; }


        /// <summary>
        /// 中间金额
        /// </summary>
        public decimal WinMoney { get; set; }

        /// <summary>
        /// 追号期数
        /// </summary>
        public int CatchIssue { get; set; }

        /// <summary>
        /// 完成期数
        /// </summary>
        public int CompledIssue { get; set; }

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
        /// 保密设置 0:未定义 1:公开 2：参与可见 3:截止可见 4:完全保密
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
        public int Bili { get; set; }

        #endregion

    }
}
