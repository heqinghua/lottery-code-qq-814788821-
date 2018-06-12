using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.RenXuanFuShi
{
    /// <summary>
    /// 复试 2中2 验证通过
    /// </summary>
    public class RenXuanErZhongEr : BaseCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var result = item.BetContent;
            var items = result.Split(',');
            if (items.Length < 2) return;
            
            int isWinCount = IsWin(items, openResult);
            if (isWinCount>0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, isWinCount);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            var result = item.BetContent;
            var items = result.Split(',');
            return TotalBet(items).Count;
        }

        public override string HtmlContentFormart(string betContent)
        {
            var newCt= VerificationSelectBetContent11x5(betContent.Replace('&', ','), false);
            if (newCt.Split(',').Length < 2)
                return "";
            return newCt;
        }


        private  List<string> TotalBet(string[] items)
        {
            var list = new List<string>();
            var item = from a in items
                       from b in items
                       where a != b
                       select string.Format("{0},{1}", a, b);
            foreach (var it in item)
            {
                var i = string.Join(",", it.Split(',').OrderBy(m => m));
                if (!list.Any(m => m == i)) list.Add(i);
            }
            return list;
        }

        private int IsWin(string[] list, string result)
        {
            var opens = result.Split(',');
            var source = TotalBet(list);

            return source.Count(c => c.Split(',').All(v => opens.Contains(v)));
        }

        
    }
}
