using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.BeiJingKuaiLeBa.RenXuanXing
{
    /// <summary>
    /// 任选5
    /// </summary>
    public class RenXuanWu : BaseCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var winMonery = WinAmt(item, openResult);
            if (winMonery != 0)
            {
                item.IsMatch = true;
                item.WinMoney = winMonery;
            }
        }


        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            var items = item.BetContent.Split(' ');
            if (items.Length < 5 || items.Length > 8) return 0;
            return Cmn(items.Length, 5);
        }

    
        /// <summary>
        /// 中奖金额
        /// 测试代码： var winAmt = BeiJingKuaiLeBa.RenXuanXing.RenXuanWu.WinAmt("01,02,03,04,05,06,07,08", "01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20");
        /// 中奖与否根据金额大小来判断
        /// </summary>
        /// <param name="betContent"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public decimal WinAmt(BasicModel.LotteryBasic.BetDetail item, string result)
        {
            //五中五 五中四 五中三
            var winAmt = 0m;
            //中五个：600，中四个：50，中三个：6
            //开奖数据
            var opens = result.Split(',');
            //投注数据
            var bets = item.BetContent.Split(' ');
            var betCount = bets.Length;
            //统计opens bets里面有多少数据在里面。
            var count = opens.Count(m => bets.Any(n => m == n));

            var baseAmt5= GetBaseAmtLstItem(5, item);
            var baseAmt4 = GetBaseAmtLstItem(4, item);
            var baseAmt3 = GetBaseAmtLstItem(3, item);

            if (count >= 5)
            {
                //因为Cmn(0,1)=1所以，要加这个判断
                winAmt = Cmn(count, 5) * baseAmt5;
                //未中奖数>=3
                if (betCount - count > 0)//这个表示不是全中
                {
                    winAmt += Cmn(count, 4) * Cmn(betCount - count, 1) * baseAmt4;
                }
                //betCount-count>=2的情况，表示有两个每中的
                if (betCount - count >= 2)
                {
                    winAmt += Cmn(count, 3) * Cmn(betCount - count, 2) * baseAmt3;
                }
            }
            else if (count == 4)
            {
                winAmt = Cmn(betCount - count, 1) * baseAmt4;
                if (betCount - count >= 2)
                {
                    winAmt += Cmn(betCount - count, 2) * Cmn(count, 3) * baseAmt3;
                }
            }
            else if (count == 3)
            {
                winAmt = Cmn(betCount - count, 2) * baseAmt3;
            }
            return winAmt;
        }

         int Fac(int k)
        {
            int t = 1;
            for (int i = 1; i <= k; i++)
                t *= i;
            return t;
        }
         int Cmn(int M, int N)
        {
            int p;
            p = Fac(M) / (Fac(N) * Fac(M - N));
            return p;
        }

        
    }
}
