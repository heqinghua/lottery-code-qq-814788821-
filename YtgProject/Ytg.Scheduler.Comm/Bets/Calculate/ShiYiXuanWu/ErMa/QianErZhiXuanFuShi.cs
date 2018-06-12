using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.ErMa
{
    /// <summary>
    /// 广东十一选五/三码/前二直选复式  验证通过 2015/05/25 2016 01 12
    /// </summary>
    public class QianErZhiXuanFuShi : BaseCalculate
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
            var newContent = betContent.Replace('|', ',').Replace('&', ' ');
            newContent = VerificationSelectBetContent11x5Many(newContent);
            if (newContent.Split(',').Length == 2)
                return newContent;
            return string.Empty;
        }

        private  List<string> TotalBet(string[] items)
        {
            List<string> list = new List<string>();

            if (items.Length < 2) return list;
            var item1 = items[0].Split(' ');//第一位
            var item2 = items[1].Split(' ');//第二位

            //定义一个第一位的字符串不在第二 和第三中间的数组
            var item = from a in item1
                       from b in item2
                       select string.Format("{0},{1}", a, b);
            foreach (var it in item)
            {
                if (it.Split(',').Distinct().Count() == 2) list.Add(it);
            }
            return list;
        }

        private bool IsWin(string[] list, string openResult)
        {
            var items = openResult.Split(',');
            var open = string.Join(",", items.Take(2));
            var bets = TotalBet(list);

            return bets.Contains(open);
        }

        
    }
}
