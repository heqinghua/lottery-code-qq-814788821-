using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Service.Logic;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 新疆时时彩 
    /// 新疆时时彩 - 每天96期，10:10起每10分钟一期
    /// 共96期
    /// </summary>
    public class GenerateXjsscLotteryIssue : BaseGenerateSscLotteryIssue
    {

        private DateTime mIssueDate;
       


        /// <summary>
        /// 生成
        /// </summary>
        public  List<LotteryIssue> Generate(DateTime issueDate)
        {
            this.mIssueDate = issueDate;

            List<LotteryIssue> result = new List<LotteryIssue>();
            DateTime beginDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 10:00:00");
            result.AddRange(Builder(beginDate, 10*60,1, 96));

            return result;
        }

        protected override string BuilderIssueCode(DateTime endDate, string code)
        {
            return this.mIssueDate.ToString("yyyyMMdd") + code;
        }

       
        protected override int LotteryId
        {
            get { return 4; }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return -(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["EndSaleMinutes_tianjing"])-80);
            }
        }
    }
}
