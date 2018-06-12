using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.RenXuanDanTuo
{
    /// <summary>
    /// 胆拖  二中二 验证通过 2015/05/25
    /// </summary>
    public class RenXuanErZhongEr : BaseCalculate
    {

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var result = item.BetContent;
            var items = result.Split(',');
            if (items.Length != 2) return;
            if (items[0].Split(' ').Length != 1) return;
            if (result.Split(new char[] { ' ', ',' }).Distinct().Count() < 2) return;
            var isWin = IsWin(items.ToList(),openResult);
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
            if (item.BetContent.Split(new char[] { ' ', ',' }).Distinct().Count() < 2) return 0;
            return items[1].Split(' ').Length;
        }

       
        private  bool IsWin(List<string> list, string result)
        {
            var flag = false;
            var opens = result.Split(',');
            if (opens.Any(m => m == list[0]) && opens.Any(m => list[1].IndexOf(m) > -1)) flag = true;
            return flag;
        }

        
    }
}
