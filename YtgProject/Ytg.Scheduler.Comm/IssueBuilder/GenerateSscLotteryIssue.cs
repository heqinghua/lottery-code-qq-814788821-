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
    /// 重庆时时彩 - 每天120期，00:05起每5、10、5分钟一期
    /// </summary>
    public class GenerateSscLotteryIssue:BaseGenerateSscLotteryIssue
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
            result.AddRange(Builder(beginDate, 5*60,1, 24));
            var fed=result.LastOrDefault().EndTime.Value;
            result.LastOrDefault().EndSaleTime = Convert.ToDateTime(fed.ToString("yyyy/MM/dd 09:58:05"));
            result.LastOrDefault().EndTime=Convert.ToDateTime(fed.ToString("yyyy/MM/dd 09:59:05"));

            beginDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 10:00:00");
            var _10Result= Builder(beginDate, 10 * 60, result.Count() + 1, 72);
            foreach (var item in _10Result)
                item.EndSaleTime= item.EndSaleTime.Value.AddMinutes(-1);//提前截止1分钟
            result.AddRange(_10Result);

            beginDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 22:00:00");
            result.AddRange(Builder(beginDate, 5*60, result.Count() + 1, 24));

            return result;
        }

        protected override string BuilderIssueCode(DateTime endDate, string code)
        {
            return this.mIssueDate.ToString("yyyyMMdd") + code;
        }


        protected override int LotteryId
        {
            get { return 1; }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return -((ConfigHelper.GetEndSaleMinutes_wufencs-60));
            }
        }
    }
}
