using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Pk10
{
    /// <summary>
    /// pk10 定位胆
    /// </summary>
    public class PkDwdCalculate: ZhiXuanFushiDetailsCalculate
    {

        /// <summary>
        /// 计算中奖情况
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="item"></param>
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //
            var res = openResult.Split(',');//得到每一位的投注数

            var arx = item.BetContent.Split('_');
            if (arx.Length != 2)
            {
                item.IsMatch = false;
                return;
            }
            int postion = Convert.ToInt32(arx[1]);//位置
            var temp = arx[0];//投注内容
            bool isWin = temp.Split(' ').Contains(res[postion-1]);
            var stepAmt = 0m;
            if (isWin)
            {
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
                item.IsMatch = true;
            }
        }

        /// <summary>
        /// 计算投注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            var ct = 0;
            var arx = item.BetContent.Split('_');
            var ay= arx[0].Split(',') ;
            foreach (var a in ay)
            {
                if (a == "")
                    continue;
                ct += a.Split(' ').Length;
            }
            return ct;
        }

        public override string HtmlContentFormart(string betContent)
        {
            //01&02&03&04&05&06&07&08&09&10|01&02&03&04&05&06&07&08&09&10|01&02&03&04&05&06&07&08&09&10|01&02&03&04&05&06&07&08&09&10|01&02&03&04&05&06&07&08&09&10|01&02&03&04&05&06&07&08&09&10|01&02&03&04&05&06&07&08&09&10|01&02&03&04&05&06&07&08&09&10|01&02&03&04&05&06&07&08&09&10|01&02&03&04&05&06&07&08&09&10
            if (string.IsNullOrEmpty(betContent))
                return string.Empty;
            string[] arx = betContent.Split('_');
            if (arx.Length != 2)
                return string.Empty;
            int postion =-1;
            if (!int.TryParse(arx[1], out postion))
                return string.Empty;

            betContent = arx[0].Replace("&", " ").Replace("|", ",");
            var array = betContent.Split(',');
            foreach (var ar in array)
            {
                if (ar.Length > 0)
                {
                    if (ar.Split(' ').Any(m => Convert.ToInt32(m.ToString()) < 0 || Convert.ToInt32(m.ToString()) > 10))
                        return string.Empty;
                }
            }
            if (Convert.ToInt32(postion) < 1 || postion > 10)
                return string.Empty;

            return betContent+"_"+postion;
        }
    }
}
