using System;
using System.Net;
using System.Windows;


namespace Ytg.BasicModel.LotteryBasic.DTO
{
    /// <summary>
    /// 投注详细信息
    /// </summary>
    public class BetDetailDTO
    {

        /// <summary>
        ///投注项
        /// </summary>
        public string IssueCode { get; set; }

        /// <summary>
        /// 玩法单选编号
        /// </summary>
        public int PalyRadioCode { get; set; }

        /// <summary>
        /// 倍数
        /// </summary>
        public double Multiple { get; set; }

        /// <summary>
        /// 模式 0元 1角 2 分
        /// </summary>
        public int Model { get; set; }

        /// <summary>
        /// 奖金类型
        /// </summary>
        public int PrizeType { get; set; }


        /// <summary>
        /// 投注内容
        /// </summary>
        public string BetContent { get; set; }

        /// <summary>
        /// 返点
        /// </summary>
        public double BackNum { get; set; }

        /// <summary>
        /// 位置名称
        /// </summary>
        public string PostionName { get; set; }

        /// <summary>
        /// 玩法最大注数
        /// </summary>
        public int MaxBetCount { get; set; }


        /// <summary>
        /// 购买类型，0为代购 1：合买
        /// </summary>
        public int ByType { get; set; }

        /// <summary>
        /// 合买最低认购金额
        /// </summary>
        public decimal CreaterBuyPieces { get; set; }

        /// <summary>
        /// 保密设置0公开1参与可见2完全保密
        /// </summary>
        public int Secrecy { get; set; }

        /// <summary>
        /// 购买比例
        /// </summary>
        public decimal ByBili { get; set; }

        public BetDetailDTO Copy()
        {
            return new BetDetailDTO()
            {
                BackNum=BackNum,
                BetContent=BetContent,
                IssueCode=IssueCode,
                Model=Model,
                Multiple=Multiple,
                PalyRadioCode=PalyRadioCode,
                PrizeType=PrizeType,
                MaxBetCount = MaxBetCount,
                ByType= ByType,
                CreaterBuyPieces= ByType,
                Secrecy=0,
                ByBili=ByBili

            };
        }
    }
}
