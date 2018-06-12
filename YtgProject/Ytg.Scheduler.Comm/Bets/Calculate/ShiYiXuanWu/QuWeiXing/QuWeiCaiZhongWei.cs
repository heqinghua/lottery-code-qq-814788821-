using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.QuWeiXing
{
    /// <summary>
    /// 广东十一选五/趣味定单双/趣味_猜中位 3,9 4,8 5,7   验证通过 2015/05/25
    /// </summary>
    public class QuWeiCaiZhongWei : BaseCalculate
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var _3_9 = this.GetBaseAmtLstItem(1, item); // 3,9
            _3_9 = _3_9 - _3_9 * 0.06m;
            var _4_8 = this.GetBaseAmtLstItem(2, item);//4,8
            _4_8 = _4_8 - _4_8 * 0.06m;
            var _5_7 = this.GetBaseAmtLstItem(3, item) ;//5,7
            _5_7 = _5_7 - _5_7 * 0.06m;
            var _6 = this.GetBaseAmtLstItem(4, item) ;//6
            _6 = _6 - _6 * 0.06m;
            var winMonery = 0m;
            //排序获取投注项
            var result = item.BetContent.Split(',').Select(c => Convert.ToInt32(c)).OrderBy(c => c);
            var comNum = Convert.ToInt32(openResult.Split(',').OrderBy(c=>c).ToArray()[2]);//中间数字

            if (result.Where(c=>c==3 || c==9).Contains(comNum))//中 3,9
                winMonery += _3_9;

            if (result.Where(c => c == 4 || c == 8).Contains(comNum))//4,8
                winMonery += _4_8;

            if (result.Where(c => c == 5 || c == 7).Contains(comNum))//5,7
                winMonery += _5_7;

            if (result.Where(c => c == 6).Contains(comNum))//6
                winMonery += _6;
            if (winMonery != 0)
            {
                item.IsMatch = true;
                item.WinMoney = BiaoZhunTotalWinMoney(item, winMonery);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(',').Length;
        }

        public override string HtmlContentFormart(string betContent)
        {
            betContent = betContent.Replace("&", ",");
            return VerificationSelectBetContent(betContent, 3, 9, 1, false);
        }



        
    }
}
