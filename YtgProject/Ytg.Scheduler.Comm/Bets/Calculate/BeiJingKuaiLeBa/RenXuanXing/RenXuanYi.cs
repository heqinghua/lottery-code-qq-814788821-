using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.BeiJingKuaiLeBa.RenXuanXing
{
    /// <summary>
    /// 任选1  验证通过
    /// </summary>
    public class RenXuanYi : BaseCalculate
    {
        
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var winAmt = 0m;
            //最多中20个，每注奖金6.4
            //开奖数据
            var opens = openResult.Split(',');
            //投注数据
            var bets = item.BetContent.Split(' ');
            //统计opens bets里面有多少数据在里面。
            var count = opens.Count(m => bets.Any(n => m == n));
            winAmt = count * this.GetBaseAmtLstItem(1,item);

            if (winAmt!=0)
            {
               
                item.IsMatch = true;
                item.WinMoney = winAmt;
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(' ').Length;
        }
    }
}
