using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.SanMa
{
    /// <summary>
    /// 广东十一选五/三码/前三组选胆拖  验证通过 2015/05/24
    /// </summary>
    public class QianSanZuXuanDanTuo : BaseCalculate
    {
       
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //胆码最多两个号码，一个号码不能同时出现在胆码和拖码中
            var result = item.BetContent.Trim(',');
            var items = result.Split(',');
            if (items.Length != 2) return;
            if (items[0].Split(' ').Length > 2 || items[0].Split(' ').Length < 1) return;
            if (result.Split(new char[] { ' ', ',' }).Distinct().Count() < 3) return;
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

            //胆码最多两个号码，一个号码不能同时出现在胆码和拖码中
            var result = item.BetContent.Trim(',');
            var items = result.Split(',');
            if (items.Length != 2) return 0;
            if (items[0].Split(' ').Length > 2 || items[0].Split(' ').Length < 1) return 0;
            if (result.Split(new char[] { ' ', ',' }).Distinct().Count() < 3) return 0;
            var isWin = false;
            var list = TotalBet(items, "00,00,00,00,00", ref isWin);

            return list.Count;
        }

        private  List<string> TotalBet(string[] items, string openResult, ref bool isWin)
        {
            isWin = false;
            //前面已经经过验证了，进来的都是验证通过的了
            var list = new List<string>();
            //胆码是比开的号码
            var danma = items[0].Split(' ');
            var tuoma = items[1].Split(' ');
            var opens = openResult.Split(',').Take(3);
            if (danma.Length == 1)
            {
                var ite = from t in tuoma
                          from f in tuoma
                          where t != f
                          select string.Format("{0} {1} {2}", danma[0], t, f)
                          ;
                foreach (var item in ite)
                {
                    var it = string.Join(" ", item.Split(' ').OrderBy(m => m));
                    if (!list.Any(m => m == it)) list.Add(it);
                    if (item.Split(' ').All(m => opens.Any(n => n == m)))
                    {
                        isWin = true;
                    }
                 }
                return list;

            }
            else//2
            {
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
}
