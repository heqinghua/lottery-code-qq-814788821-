using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Comm
{
    public static class LotteryIssueTimerHelper
    {
        static Dictionary<string, string> LotteryOpenTimeConfig = new Dictionary<string, string>();

        static LotteryIssueTimerHelper()
        {
            string content = System.Web.Configuration.WebConfigurationManager.AppSettings["lotteryOpenTimes"];
            if (!string.IsNullOrEmpty(content))
            {
                var array = content.Split(',');
                foreach (var item in array)
                {
                    if (string.IsNullOrEmpty(item))
                        continue;
                    var lt = item.Split('|');
                    if (lt.Length != 2)
                        continue;
                    if (!LotteryOpenTimeConfig.ContainsKey(lt[0]))
                        LotteryOpenTimeConfig.Add(lt[0], lt[1]);
                }
            }
        }

        public static string GetTimes(int lotteryId)
        {
            if (LotteryOpenTimeConfig.ContainsKey(lotteryId.ToString()))
                return LotteryOpenTimeConfig[lotteryId.ToString()];
            return string.Empty;
        }
    }

}
