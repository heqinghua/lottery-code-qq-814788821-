using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 生成江西时时彩 销售时间：09:00-23:15（84期）    10分钟开奖  返奖59%
    /// </summary>
    public class GenerateJxSscLotteryIssue:BaseGenerateSscLotteryIssue
    {
       

        /// <summary>
        /// 生成江西时时彩期数
        /// </summary>
        /// <param name="issueDate"></param>
        /// <returns></returns>
        public List<LotteryIssue> Generate(DateTime issueDate)
        {
            List<LotteryIssue> result = new List<LotteryIssue>();
            var beginDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 09:02:20");
            //每期10分零9秒
            int sec = 10 * 60 + 9;
            result= this.Builder(beginDate, sec, 1, 84);
            return result;
        }

        /// <summary>
        /// 江西时时彩
        /// </summary>
        protected override int LotteryId
        {
            get { return 2; }
        }

       
      
    }
}
