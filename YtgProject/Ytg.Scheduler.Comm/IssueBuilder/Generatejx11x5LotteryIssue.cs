using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    
    /// <summary>
    /// 江西11选5
    /// 共84期 早上09点11开始 十分钟一期 
    /// </summary>
    public class Generatejx11x5LotteryIssue : BaseGenerateSscLotteryIssue
    {
        /// <summary>
        /// 生成
        /// </summary>
        public List<LotteryIssue> Generate(DateTime issueDate)
        {


            List<LotteryIssue> result = new List<LotteryIssue>();
            DateTime beginDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 09:00:00");
            result.AddRange(Builder(beginDate, 10 * 60, 1, 84));

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
            get { return 20; }
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
