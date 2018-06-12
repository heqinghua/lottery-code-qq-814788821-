using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic
{
    /// <summary>
    /// 追号记录视图  这用于后台wcf服务
    /// </summary>
    [Serializable,DataContract]
    public class CatchNumsVM
    {
        public int Id { get; set; }

        /// <summary>
        ///  追号编号
        /// </summary>
        [DataMember]
        public string CatchNumCode { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>

        [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [DataMember]
        public string Code { get; set; }


        /// <summary>
        /// 投多少注 总注数
        /// </summary>
        [DataMember]
        public int BetCount { get; set; }


        /// <summary>
        /// 玩法单选编号 PlayTypeRadios
        /// </summary>
        [DataMember]
        public int PalyRadioCode { get; set; }

        /// <summary>
        /// 追号总金额
        /// </summary>
        [DataMember]
        public decimal SumAmt { get; set; }

        /// <summary>
        /// 模式 0元 1角 2 分
        /// </summary>
        [DataMember]
        public int Model { get; set; }

        /// <summary>
        /// 奖金类型
        /// </summary>
        [DataMember]
        public int PrizeType { get; set; }

        /// <summary>
        /// 返点返点,百分数	
        /// </summary>
        [DataMember]
        public decimal BackNum { get; set; }

        /// <summary>
        /// 投注内容
        /// </summary>
        [DataMember]
        public string BetContent { get; set; }

        /// <summary>
        /// 本次追号状态
        /// </summary>
        [DataMember]
        public CatchNumType Stauts { get; set; }

        /// <summary>
        /// 奖金级别 1700 or 1800
        /// </summary>
        [DataMember]
        public int BonusLevel { get; set; }

        /// <summary>
        /// 中奖后是否自动停止追号
        /// </summary>
        [DataMember]
        public bool IsAutoStop { get; set; }

        /// <summary>
        /// 开始期数
        /// </summary>
        [DataMember]
        public string BeginIssueCode { get; set; }

        /// <summary>
        /// 追号期数
        /// </summary>
        [DataMember]
        public int CatchIssue { get; set; }

        /// <summary>
        /// 完成期数
        /// </summary>
        [DataMember]
        public int CompledIssue { get; set; }

        /// <summary>
        /// 完成金额
        /// </summary>
        [DataMember]
        public decimal CompledMonery { get; set; }


        /// <summary>
        /// 游戏名称
        /// </summary>
        [DataMember]
        public string LotteryName { get; set; }

        /// <summary>
        /// 玩法名称
        /// </summary>
        [DataMember]
        public string PlayTypeRadioName { get; set; }

        [DataMember]
        public DateTime OccDate { get; set; }


        /// <summary>
        /// 中间金额
        /// </summary>
        [DataMember]
        public decimal WinMoney { get; set; }

        /// <summary>
        /// 中奖期数
        /// </summary>
        [DataMember]
        public int WinIssue { get; set; }


        /// <summary>
        /// 用户取消期数
        /// </summary>
        [DataMember]
        public int UserCannelIssue { get; set; }

        /// <summary>
        /// 用户取消金额
        /// </summary>
        [DataMember]
        public decimal UserCannelMonery { get; set; }

        /// <summary>
        /// 中奖后取消期数
        /// </summary>
        [DataMember]
        public int SysCannelIssue { get; set; }

        /// <summary>
        /// 总记录条数
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public string PlayTypeNumName { get; set; }
    }
}
