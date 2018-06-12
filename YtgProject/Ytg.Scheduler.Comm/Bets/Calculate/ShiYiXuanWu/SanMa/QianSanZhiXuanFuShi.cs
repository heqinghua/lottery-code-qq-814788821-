using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.SanMa
{
    /// <summary>
    /// 广东十一选五/三码/前三直选复式   验证通过 2015/05/24 2016 01 12 
    /// </summary>
    public class QianSanZhiXuanFuShi : BaseCalculate
    {


        private  bool IsWin(string[] list, string openResult)
        {
            //所选位数上的号码与开奖的位数上的号码一致即为中奖 01,02,03,04,05
            var items = openResult.Remove(8, 6);
            var bets = TotalBet(list);
            return bets.Contains(items);
        }

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            if (IsWin(item.BetContent.Split(','), openResult))
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref  stepAmt), stepAmt, 1);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return TotalBet(item.BetContent.Split(',')).Count;
            
        }

        public override string HtmlContentFormart(string betContent)
        {
            var newContent = betContent.Replace('|', ',').Replace('&', ' ');
            newContent= VerificationSelectBetContent11x5Many(newContent);
            if (newContent.Split(',').Length == 3)
                return newContent;
            return string.Empty;
        }

        private  List<string> TotalBet(string[] items)
        {
            List<string> list = new List<string>();

            if (items.Length < 3) return list;
            var item1 = items[0].Split(' ');//第一位
            var item2 = items[1].Split(' ');//第二位
            var item3 = items[2].Split(' ');//第三位

            //定义一个第一位的字符串不在第二 和第三中间的数组
            var item = from a in item1
                       from b in item2
                       from c in item3
                       select string.Format("{0},{1},{2}", a, b, c);
            foreach (var it in item)
            {
                if (it.Split(',').Distinct().Count() == 3) list.Add(it);
            }
            return list;
        }
    }
}
