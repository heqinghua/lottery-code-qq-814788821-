using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.ErMa
{
    /// <summary>
    /// 广东十一选五/三码/前二组选胆码 验证通过 2015/05/25
    /// </summary>
    public class QianErZuXuanDanTuo : BaseCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var items = item.BetContent.Split(',');
            if (items.Length != 2) return ;
            if (items[0].Split(' ').Length != 1) return ;
            if (item.BetContent.Split(new char[] { ' ', ',' }).Distinct().Count() < 3) return ;
            var isWin = false;
            var list = TotalBet(items, openResult, ref isWin);

            if (isWin)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
            }

        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            var items = item.BetContent.Split(',');
            if (items.Length != 2) return 0;
            if (items[0].Split(' ').Length != 1) return 0;
            if (item.BetContent.Split(new char[] { ' ', ',' }).Distinct().Count() < 3) return 0;
            var isWin = false;
            var list = TotalBet(items, "00,00,00,00,0", ref isWin);
            return list.Count;
        }


        private  List<string> TotalBet(string[] items, string openResult, ref bool isWin)
        {
            isWin = false;
            //前面已经经过验证了，进来的都是验证通过的了
            var list = new List<string>();
            //胆码是比开的号码
            var tuoma = items[1].Split(' ');
            var opens = openResult.Split(',').Take(2);
            var ite = from t in tuoma
                      select string.Format("{0} {1}", items[0], t);
            foreach (var item in ite)
            {
                if (item.Split(' ').All(m => opens.Any(n => n == m)))
                {
                    isWin = true;
                    break;
                }
            }
            return ite.ToList();
        }

       
    }
}
