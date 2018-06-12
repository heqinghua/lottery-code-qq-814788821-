using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc
{
    public abstract class RenXuanDanShi : BaseRenXuan
    {
        public override string HtmlContentFormart(string betContent)
        {

            if (string.IsNullOrEmpty(betContent))
                return string.Empty;
           betContent= betContent.Replace("&", ",");
            string postionStr = string.Empty;
            string contentCenter = this.SplitRenXuanContent(betContent, ref postionStr);
            if (!this.VerificationPostion(postionStr)
                || string.IsNullOrEmpty(contentCenter))
                return string.Empty;
            //01&02&03&33&44&55

            var contentArray = contentCenter.Split(',');
            var list = new List<string>();
            foreach (var item in contentArray)
            {
                var nums = item.Select(x => Convert.ToInt32(x.ToString()));
                if (nums.Count() != 2) return string.Empty;
                if (nums.All(m => Convert.ToInt32(m) >= 0 && Convert.ToInt32(m) <= 9)) list.Add(item);
            }
            return betContent;
        }
    }
}
