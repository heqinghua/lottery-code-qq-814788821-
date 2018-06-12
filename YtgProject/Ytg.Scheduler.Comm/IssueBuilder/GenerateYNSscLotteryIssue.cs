using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 构建印尼时时彩 - 每天288期，00:05起每5分钟一期
    /// </summary>
    public class GenerateYNSscLotteryIssue : GenerateHNSscLotteryIssue
    {

        private DateTime mIssueDate;

        /// <summary>
        /// 生成
        /// </summary>
        public  List<LotteryIssue> Generate(DateTime issueDate)
        {

            this.mIssueDate = issueDate;
            List<LotteryIssue> result = new List<LotteryIssue>();
            DateTime beginDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 00:00:00");
            result.AddRange(Builder(beginDate, 5 * 60, 1, 288));


            return result;
        }

        protected override string BuilderIssueCode(DateTime endDate, string code)
        {
            return this.mIssueDate.ToString("yyyyMMdd") + code;
        }

        /// <summary>
        /// 结束推迟时间
        /// </summary>
        protected override int IssueEndTimeAppend
        {
            get
            {
               
                return ConfigHelper.GetIssueEndTimeAppend;
            }
        }

        protected override int LotteryId
        {
            get { return 23; }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return -ConfigHelper.GetEndSaleMinutes_wufencs;
            }
        }
    }
}
