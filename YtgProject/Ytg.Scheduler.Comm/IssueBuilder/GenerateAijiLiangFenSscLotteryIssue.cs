using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 构建埃及二分彩 埃及二分彩 - 每天720期，00:00起每2分钟一期
    /// </summary>
    public class GenerateAijiLiangFenSscLotteryIssue : BaseGenerateSscLotteryIssue
    {
        private DateTime mIssueDate;

        /// <summary>
        /// 生成
        /// </summary>
        public List<LotteryIssue> Generate(DateTime issueDate)
        {

            this.mIssueDate = issueDate;
            List<LotteryIssue> result = new List<LotteryIssue>();
            DateTime beginDate = Convert.ToDateTime(issueDate.AddDays(-1).ToString("yyyy/MM/dd") + " 23:58:00");
            result.AddRange(Builder(beginDate, 2 * 60, 1, 720));

            return result;
        }

        protected override string BuilderIssueCode(DateTime endDate, string code)
        {
            return this.mIssueDate.ToString("yyyyMMdd") + code;
        }


        protected override int LotteryId
        {
            get { return 24; }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return -20;
            }
        }

        protected override int IssueEndTimeAppend
        {
            get
            {
                return 0;
            }
        }

    }
}
