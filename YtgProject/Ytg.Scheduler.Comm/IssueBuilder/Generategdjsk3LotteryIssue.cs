using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 江苏快三 - 每天82期，08:40起每10分钟一期
    /// </summary>
    public class Generategdjsk3LotteryIssue : BaseGenerateSscLotteryIssue
    {
        /// <summary>
        /// 生成  20160102080
        /// </summary>
        public List<LotteryIssue> Generate(DateTime issueDate)
        {


            List<LotteryIssue> result = new List<LotteryIssue>();
            DateTime beginDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 08:30:00");
            result.AddRange(Builder(beginDate, 10 * 60, 1, 82));

            return result;
        }


   
       
        protected override int FormartCount
        {
            get
            {
                return 3;
            }
        }

        protected override int LotteryId
        {
            get { return 22; }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return -(ConfigHelper.GetEndSaleMinutes_wufencs+100);
            }
        }
    }
}
