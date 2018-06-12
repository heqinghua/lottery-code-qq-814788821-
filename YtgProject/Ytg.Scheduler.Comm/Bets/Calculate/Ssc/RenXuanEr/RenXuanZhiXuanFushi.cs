using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr
{
    /// <summary>
    /// 直选复试 验证通过   2016 01 20 
    /// </summary>
    public class RenXuanZhiXuanFushi : BaseRenXuan
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            if (null == item || string.IsNullOrEmpty(item.BetContent)) return;
            else
            {
                var postion = item.BetContent.Split('_')[1].Select(x => Convert.ToInt32(x.ToString())).ToList();
                var contentArray = item.BetContent.Split('_')[0].Split(',');
                var openNums = openResult.Replace(",", "");
                var newOpenNums = "";
                foreach (var ps in postion)
                {
                    newOpenNums += openNums[ps - 1];
                }

                bool isWin = false;
                var list = GetAllBets(contentArray[0], contentArray[1]);
                var result = list.Find(m => m == newOpenNums);
                if (!string.IsNullOrEmpty(result))
                {
                    isWin = true;
                }

                if (isWin)
                {
                    item.IsMatch = true;
                    decimal stepAmt = 0;
                    item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, 1);
                }

            }

        }


        /// <summary>
        /// 根据投注内容得到所有的组合情况
        /// </summary>
        /// <returns></returns>
        private List<string> GetAllBets(string c0, string c1)
        {
            var list = new List<string>();
            var shi = c0.Select(m => Convert.ToInt32(m.ToString())).ToList();
            var ge = c1.Select(m => Convert.ToInt32(m.ToString())).ToList();
            list = (from s in shi
                    from g in ge
                    select string.Format("{0}{1}", s, g)).ToList();
            return list;
        }


        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {

            if (string.IsNullOrEmpty(item.BetContent))
                return 0;

            var betContentArray= item.BetContent.Split('_');
            var contentArray = betContentArray[0].Split(',');
            var pos = betContentArray[1].Select(x => Convert.ToInt32(x.ToString())).ToList();

            return contentArray[0].Length * contentArray[1].Length;
        }

        public override string HtmlContentFormart(string betContent)
        {
            var splitContent= betContent.Split('_');
            if (splitContent.Length != 2)
                return string.Empty;
            if (splitContent[1].Length < 2)
                return string.Empty;
            betContent = splitContent[0].Replace("&", "").Replace("|", ",");
            var array = betContent.Split(',');
            int compledCount = 0;
            foreach (var item in array)
            {
                if (string.IsNullOrEmpty(item))
                    continue;
                var contentArray = item.Select(x => Convert.ToInt32(x.ToString()));
                if (contentArray.Distinct().Count() != contentArray.Count())
                    return "";
                if (contentArray.Any(m => Convert.ToInt32(m) < 0 || Convert.ToInt32(m) > 9)) return string.Empty;
                compledCount++;
            }
            if (compledCount < 2)
                return string.Empty;
            return betContent + "_" + splitContent[1];
        }
    }
}
