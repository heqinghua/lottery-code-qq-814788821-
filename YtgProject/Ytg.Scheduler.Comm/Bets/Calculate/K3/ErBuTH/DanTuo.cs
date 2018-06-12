using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.K3.ErBuTH
{
    /// <summary>
    /// 胆拖选号 1|2&3&4&5 2016 01 13
    /// </summary> 
    public class DanTuo : BiaoZhun
    {


        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            openResult = openResult.Replace(",", "");
            var betContentArray = item.BetContent.Split(',');

            int dan = 0;
            if (!int.TryParse(betContentArray[0], out dan))
                return;
            int count = 0;
            if (openResult.Contains(dan.ToString()))
            {
                var tuo = betContentArray[1];
                foreach (var t in tuo)
                {
                    int ot;
                    if (!int.TryParse(t.ToString(), out ot))
                        continue;
                    if (openResult.Contains(ot.ToString()))
                    {
                        count++;
                        break;
                    }
                }
            }
            if (count > 0)
            {
                item.IsMatch = true;
                item.WinMoney = BiaoZhunTotalWinMoney(item, AMT); 
            }

        }

        public override string HtmlContentFormart(string betContent)
        {
            betContent = betContent.Replace('|', ',').Replace('&', ' ');
            betContent = VerificationSelectBetContentK3Many(betContent);
            //验证两组数据不能有相同的数字
            var array = betContent.Split(',');
            if (array.Length != 2)
                return string.Empty;
            if (array[0].Split(' ').Any(c => array[1].Split(' ').Contains(c)))
                return string.Empty;
            return betContent.Replace(" ", "");//去掉空格
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            var array= item.BetContent.Split(',');
            return array[1].Length;
        }
    }
}
