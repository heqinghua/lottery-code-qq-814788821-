using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class HeMaiDto
    {
        public int Id { get; set; }

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
        /// 菜种编码
        /// </summary>
        public string LotteryCode { get; set; }

        /// <summary>
        /// 倍数
        /// </summary>
        public int Multiple { get; set; }

        /// <summary>
        /// 模式 0元 1角 2 分
        /// </summary>
        public int Model { get; set; }



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
        /// 位置信息
        /// </summary>
        public string PostionName { get; set; }





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
        public double Bili { get; set; }
        #endregion

        public string Code { get; set; }

        public string NikeName { get; set; }

        public DateTime OccDate { get; set; }

        public int GroupByState { get; set; }




        public int PalyRadioCode { get; set; }


    }
}
