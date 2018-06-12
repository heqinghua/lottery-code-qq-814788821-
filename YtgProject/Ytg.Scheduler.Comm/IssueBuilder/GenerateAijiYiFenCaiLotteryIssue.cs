using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 埃及分分彩 - 每天1440期，00:00起每1分钟一期
    /// </summary>
    public class GenerateAijiYiFenCaiLotteryIssue : GenerategdYiFenCaiLotteryIssue
   {


        protected override string BuilderIssueCode(DateTime endDate, string code)
        {
            return endDate.ToString("yyyyMMdd") + code;
        }

        /// <summary>
        /// 埃及分分彩id
        /// </summary>
        protected override int LotteryId
        {
            get
            {
                return 13;
            }
        }
    }

    
        

}
