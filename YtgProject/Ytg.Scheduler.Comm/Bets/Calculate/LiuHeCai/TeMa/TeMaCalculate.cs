using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.LiuHeCai.TeMa
{
    /// <summary>
    /// 特码 
    /// </summary>
    public class TeMaCalculate : BaseCalculate
    {


        /// <summary>
        /// 特码  49,25,10,09,40,48+32
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult">1</param>
        /// <param name="item">345</param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var list = openResult.Split('+');//特码
            if (list.Length != 2)
                return;
            int openTeMa = 0;
            int betContent = -1;
            if (!int.TryParse(list[1], out openTeMa) ||
                !int.TryParse(item.BetContent, out betContent))
                return;
            if (openTeMa != betContent)
                return;

            item.IsMatch = true;//中奖
            item.WinMoney = item.TotalAmt * ConfigHelper.GetLiuhecaibat;
            item.Stauts = BasicModel.BetResultType.Winning;
        }


        public override string HtmlContentFormart(string betContent)
        {
            return betContent;
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return 1;
        }
    }
}
