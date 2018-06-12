using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 1.5分彩 960   00:00-05:00:00 199期 07:00-23:59 679期  共 878
    /// </summary>
    public class GenerategdWuFenCaiLotteryIssue:BaseGenerateSscLotteryIssue
    {
        private DateTime missueDate;

        private int maxIssueCount = 880;//总期数
        /// <summary>
        /// 生成
        /// </summary>
        public List<LotteryIssue> Generate(DateTime issueDate)
        {
            string spers = System.Configuration.ConfigurationManager.AppSettings["hg1_5Builder_sper"];//构建韩国1.5分彩
            if (string.IsNullOrEmpty(spers))
                return new List<LotteryIssue>();

            DateTime beginDate = DateTime.Now;
            issueDate= Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd")+" 00:00:00");


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
            startIssueValue = (startIssueValue + maxIssueCount * days);//今天开始期数
            //00:00:00
            int seconds = 90;//90秒一期  04:59秒结束， 07:00 继续开始

            List<LotteryIssue> source = new List<LotteryIssue>();
            for (var i = 1; i <= maxIssueCount; i++)
            {

                DateTime endTime = issueDate.AddSeconds(seconds).AddSeconds(IssueEndTimeAppend);
                DateTime endSaleTime = endTime.AddSeconds(EndSaleMinutes);
                string issueCode =  startIssueValue.ToString();
                LotteryIssue issue = new LotteryIssue()
                {
                    LotteryId = LotteryId,
                    EndSaleTime = endSaleTime,
                    EndTime = endTime,
                    IssueCode = issueCode,

                    LotteryTime = endTime,
                    StartSaleTime = issueDate,
                    StartTime = issueDate,
                };

                source.Add(issue);
                issueDate = endTime.AddSeconds(-IssueEndTimeAppend);
                if (issueDate.Hour == 5)
                {
                    issueDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd 07:00:00"));
                }
                startIssueValue++;
            }


            return source;
        }


        protected override int LotteryId
        {
            get
            {
                return 15;
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
                return -60;
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
