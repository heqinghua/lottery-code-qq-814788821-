using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 三分彩  共480期
    /// </summary>
    public class GenerategdErFenCaiLotteryIssue : BaseGenerateSscLotteryIssue
    {
        private DateTime missueDate;
        /// <summary>
        /// 生成
        /// </summary>
        public List<LotteryIssue> Generate(DateTime issueDate)
        {
            this.missueDate = issueDate;
            List<LotteryIssue> result = new List<LotteryIssue>();
            DateTime beginDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 00:00:00");
            result.AddRange(Builder(beginDate, 3 * 60, 1, 480));

            return result;
        }

        protected override string BuilderIssueCode(DateTime endDate, string code)
        {
            return this.missueDate.ToString("MMdd") + code;
        }

        protected override int LotteryId
        {
            get
            {
                return 12;
            }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return -5;
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
                return 3;
            }
        }
    }
}
