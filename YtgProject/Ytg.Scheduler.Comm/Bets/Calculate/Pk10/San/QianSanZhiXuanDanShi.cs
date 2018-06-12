using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Pk10.San
{
    /// <summary>
    ///前四单式 验证通过
    /// </summary>
    public class QianSanZhiXuanDanShi : BaseCalculate
    {
       
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            if (IsWin(item.BetContent.Split(','), openResult))
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }
        }
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return TotalBet(item.BetContent.Split(',')).Count;
        }

        public override string HtmlContentFormart(string betContent)
        {
            return VerificationBetContent(betContent.Replace('&', ','), 3);
        }

        private  List<string> TotalBet(string[] items)
        {
            var list = new List<string>();
            foreach (var item in items)
            {
                var nums = item.Split(' ');
                if (nums.Distinct().Count() != 3) continue;
                if (nums.Any(m => m.Length == 2 || (Convert.ToInt32(m) > 0 && Convert.ToInt32(m) < 11))) list.Add(item);
            }
            return list;
        }

        private  bool IsWin(string[] list, string openResult)
        {
            var flag = false;
            var items = openResult.Split(',');

            var open = string.Join(" ", items.Take(3));

            foreach (var item in list) {
                if (item == open)
                    return true;
            }
            return flag;
        }

        
    }
}
