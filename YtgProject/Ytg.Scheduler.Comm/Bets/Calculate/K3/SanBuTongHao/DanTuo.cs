using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.K3.SanBuTongHao
{
    /// <summary>
    /// 胆拖选号 1|2&3&4&5
    /// </summary> 
    public class DanTuo : BiaoZhun
    {


        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            openResult = openResult.Replace(",", "");
            var betContentArray = item.BetContent.Split(',');

            var dan = betContentArray[0];
            int count = 0;
            if (openResult.StartsWith(dan))
            {
                var tuo = betContentArray[1].Replace("&", "");
                foreach (var t in tuo)
                {
                    int ot;
                    if (!int.TryParse(t.ToString(), out ot))
                        continue;
                    if (openResult.EndsWith(ot.ToString()))
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
            var a0Len=array[0].Replace(" ","").Length;
            if ( a0Len<1 || a0Len>2)
                return string.Empty;
            if (array[0].Split(' ').Any(c => array[1].Split(' ').Contains(c)))
                return string.Empty;
            return betContent.Replace(" ", "");//去掉空格
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            var array = item.BetContent.Split(',');
            return array[1].Length;
        }

    }
}
