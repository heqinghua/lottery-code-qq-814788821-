using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 天津时时彩开奖结果，每日84期，每天9:00至23:00点，每10分钟公布一次天津时时彩开奖结果。
    /// </summary>
    public class GenerateTjsscLotteryIssue : BaseGenerateSscLotteryIssue
    {
       

        /// <summary>
        /// 生成天津时时彩期数
        /// </summary>
        /// <param name="issueDate"></param>
        /// <returns></returns>
        public List<LotteryIssue> Generate(DateTime issueDate)
        {
            List<LotteryIssue> result = new List<LotteryIssue>();
            var beginDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 08:58:00");
            //每期10分零9秒
            int sec = 10 * 60;
            result = this.Builder(beginDate, sec, 1, 84);
            return result;
        }

        /// <summary>
        /// 天津时时彩
        /// </summary>
        protected override int LotteryId
        {
            get { return 5; }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return -(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["EndSaleMinutes_tianjing"])-(2*60-40));
            }
        }
    }
}
