using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    public abstract class FiveSpecialDetailsCalculate : BaseCalculate
    {
        public override string HtmlContentFormart(string betContent)
        {
            betContent = VerificationSelectBetContent(betContent.Replace("&", " "), 0, 9, 1, false, ' ');
            if (string.IsNullOrEmpty(betContent))
                return string.Empty;
            return betContent.Replace(" ", "");
        }
    }
}
