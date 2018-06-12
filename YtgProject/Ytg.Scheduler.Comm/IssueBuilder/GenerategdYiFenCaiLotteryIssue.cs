using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 一分彩 全天 一分钟一期 共1440期
    /// </summary>
    public class GenerategdYiFenCaiLotteryIssue : BaseGenerateSscLotteryIssue
    {

        /// <summary>
        /// 生成
        /// </summary>
        public List<LotteryIssue> Generate(DateTime issueDate)
        {

            List<LotteryIssue> result = new List<LotteryIssue>();
            DateTime beginDate = Convert.ToDateTime(issueDate.AddDays(-1).ToString("yyyy/MM/dd") + " 00:00:10");
            result.AddRange(Builder(beginDate, 60, 1, 1440));
            return result;
        }

        protected override string BuilderIssueCode(DateTime endDate, string code)
        {
            return endDate.ToString("yyyyMMdd") + code;
        }

        protected override int LotteryId
        {
            get
            {
                return 11;
            }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return -10;
            }
        }

        protected override int IssueEndTimeAppend
        {
            get
            {
                return 0;
            }
        }

        protected override int FormartCount
        {
            get
            {
                return 4;
            }
        }


    }
}
