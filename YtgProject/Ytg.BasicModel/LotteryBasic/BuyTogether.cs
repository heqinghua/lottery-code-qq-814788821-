using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic
{
    /// <summary>
    /// 合买详情
    /// </summary>
    public class BuyTogether : BaseEntity
    {
        public BuyTogether()
        {

            this.Stauts = BetResultType.NotOpen;
            this.Model = 0;
            this.IssueCode = "";
            this.PalyRadioCode = 0;
            this.PostionName = "";


        }
    /// <summary>
    /// 合买用户id
    /// </summary>
    [DataMember]
        public int UserId { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public string BuyTogetherCode { get; set; }

        [DataMember]
        public string NikeName{get;set;}


        [DataMember]
        public string Code{get;set;}

        /// <summary>
        /// 投注id
        /// </summary>
        /// 
        [DataMember]
        public int BetDetailId { get; set; }

        /// <summary>
        /// 认购金额
        /// </summary>
        [DataMember]
        public decimal Subscription { get; set; }

        /// <summary>
        /// 中奖金额
        /// </summary>
        [DataMember]
        public decimal WinMonery { get; set; }

        /// <summary>
        /// 状态：1 已中奖、2 未中奖、3 未开奖、4 已撤单
        /// </summary>
        [DataMember]
        public BetResultType Stauts { get; set; }

        /// <summary>
        /// 模式
        /// </summary>
        public int Model { get; set; }

        /// <summary>
        /// IssueCode
        /// </summary>
        public string IssueCode { get; set; }

        public int PalyRadioCode { get; set; }

        public string PostionName { get; set; }

        public override string ToString()
        {
            return "UserId:" + UserId + "BuyTogetherCode:" + BuyTogetherCode + "NikeName:" + NikeName + "Code:" + Code + "BetDetailId:" + BetDetailId + "Subscription:" + Subscription + "WinMonery:" + WinMonery;
        }
    }
}
