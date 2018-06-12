using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 北京PK10 每天179期，09:07分开始
    /// </summary>
    public class GeneratePk_10LotteryIssue : BaseGenerateSscLotteryIssue
    {


        /// <summary>
        /// 生成
        /// </summary>
        public List<LotteryIssue> Generate(DateTime issueDate)
        {
            string spers = System.Configuration.ConfigurationManager.AppSettings["pk_10_5Builder_sper"];//构建韩国1.5分彩
            if (string.IsNullOrEmpty(spers))
                return new List<LotteryIssue>();

            DateTime beginDate = DateTime.Now;
            issueDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 00:00:00");


            int startIssueValue = 0;
            if (!DateTime.TryParse(spers.Split(',')[0], out beginDate))
            {
                return new List<LotteryIssue>();
            }
            if (!int.TryParse(spers.Split(',')[1], out startIssueValue))
            {
                return new List<LotteryIssue>();
            }
            //计算其实日期与当天相差天数
            var days = issueDate.Subtract(beginDate).Days;
            startIssueValue = startIssueValue + 179 * days;//今天开始期数
            
            int seconds = 60*5;//90秒一期  03:59秒结束， 07:00 继续开始
            issueDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd")+" 09:07:00");
            List<LotteryIssue> source = new List<LotteryIssue>();
            for (var i = 1; i <= 179; i++)
            {

                DateTime endTime = issueDate.AddSeconds(seconds).AddSeconds(IssueEndTimeAppend);
                DateTime endSaleTime = endTime.AddSeconds(EndSaleMinutes);
                string issueCode = startIssueValue.ToString();
                LotteryIssue issue = new LotteryIssue()
                {
                    LotteryId = LotteryId,
                    EndSaleTime = endSaleTime,
                    EndTime = endTime,
                    IssueCode = issueCode,

                    LotteryTime = endTime,
                    StartSaleTime =(i==1?(Convert.ToDateTime( issueDate.AddDays(-1).ToString("yyyy/MM/dd")+" 23:57:00")): issueDate.AddSeconds(-seconds)),
                    StartTime = issueDate.AddSeconds(-seconds),
                };

                source.Add(issue);
                issueDate = endTime.AddSeconds(-IssueEndTimeAppend);
               
                startIssueValue++;
            }


            return source;
        }


        protected override int LotteryId
        {
            get
            {
                return 26;
            }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return -70;
            }
        }

        protected override int IssueEndTimeAppend
        {
            get
            {
                return -(60*5);
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
