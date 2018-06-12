using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 山东11选5 每天87期  9点06分开始 10分钟一期
    /// </summary>
    public class Generatesd11x5LotteryIssue:BaseGenerateSscLotteryIssue
    {
        /// <summary>
        /// 生成
        /// </summary>
        public List<LotteryIssue> Generate(DateTime issueDate)
        {
            List<LotteryIssue> result = new List<LotteryIssue>();
            DateTime beginDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 08:25:00");
            result.AddRange(Builder(beginDate, 10 * 60, 1, 87));

            return result;
        }

        protected override int FormartCount
        {
            get
            {
                return 2;
            }
        }

       

        protected override int LotteryId
        {
            get { return 19; }
        }

        /// <summary>
        /// 结束推迟时间
        /// </summary>
        protected override int IssueEndTimeAppend
        {
            get
            {
                return 50;
            }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return -(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["EndSaleMinutes_shangdong"])-(4*60-30));
            }
        }
    }
}
