using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Pk10.Si
{
    /// <summary>
    /// 前四复试
    /// </summary>
    public class PkQianSiFushiCalculate: BaseCalculate
    {

        private bool IsWin(string[] list, string openResult)
        {
            //所选位数上的号码与开奖的位数上的号码一致即为中奖  01,04,08,09,02,10,03,06,07,05
            var items = openResult.Remove(11, 18);
            var bets = TotalBet(list);
            return bets.Contains(items);
        }

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
            var newContent = betContent.Replace('|', ',').Replace('&', ' ');
            newContent = VerificationSelectBetContent11x5Many(newContent);
            if (newContent.Split(',').Length == 4)
                return newContent;
            return string.Empty;
        }

        private List<string> TotalBet(string[] items)
        {
            List<string> list = new List<string>();

            if (items.Length < 3) return list;
            var item1 = items[0].Split(' ');//第一位
            var item2 = items[1].Split(' ');//第二位
            var item3 = items[2].Split(' ');//第三位
            var item4 = items[3].Split(' ');//第三位

            //定义一个第一位的字符串不在第二 和第三中间的数组
            var item = from a in item1
                       from b in item2
                       from c in item3
                       from d in item4
                       select string.Format("{0},{1},{2},{3}", a, b, c, d);
            foreach (var it in item)
            {
                if (it.Split(',').Distinct().Count() == 4) list.Add(it);
            }
            return list;
        }
    }
}
