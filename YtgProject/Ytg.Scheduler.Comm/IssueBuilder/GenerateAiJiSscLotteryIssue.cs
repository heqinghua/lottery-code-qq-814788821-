using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{

    /// <summary>
    /// 埃及五分彩(五分彩) - 每天204 期，07:05-23:55起每5分钟一期
    /// </summary>
    public class GenerateAiJiSscLotteryIssue : BaseGenerateSscLotteryIssue
    {
        //private DateTime mIssueDate;

        ///// <summary>
        ///// 生成
        ///// </summary>
        //public List<LotteryIssue> Generate(DateTime issueDate)
        //{

        //    this.mIssueDate = issueDate;
        //    List<LotteryIssue> result = new List<LotteryIssue>();
        //    DateTime beginDate = Convert.ToDateTime(issueDate.AddDays(-1).ToString("yyyy/MM/dd") + " 23:55:00");
        //    result.AddRange(Builder(beginDate, 5 * 60, 1, 288));

        //    return result;
        //}

        //protected override string BuilderIssueCode(DateTime endDate, string code)
        //{
        //    return this.mIssueDate.ToString("yyyyMMdd") + code;
        //}

        ///// <summary>
        ///// 结束推迟时间
        ///// </summary>
        //protected override int IssueEndTimeAppend
        //{
        //    get
        //    {
        //        return 1;
        //    }
        //}

        //protected override int LotteryId
        //{
        //    get { return 25; }
        //}

        //protected override int EndSaleMinutes
        //{
        //    get
        //    {
        //        return -(ConfigHelper.GetEndSaleMinutes_wufencs-59);
        //    }
        //}


        /****************************new******************************************/
        /// <summary>
        /// 生成
        /// </summary>
        public List<LotteryIssue> Generate(DateTime issueDate)
        {
            string spers = System.Configuration.ConfigurationManager.AppSettings["tw_bg_Builder_sper"];//构建台湾宾果
            if (string.IsNullOrEmpty(spers))
                return new List<LotteryIssue>();

            DateTime beginDate = DateTime.Now;
            issueDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 07:05:00");


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
            startIssueValue = startIssueValue + 202 * days+ days;//今天开始期数

            int seconds = 60 * 5;//300秒一期  03:59秒结束， 07:00 继续开始
            issueDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 07:05:00");
            List<LotteryIssue> source = new List<LotteryIssue>();
            for (var i = 1; i <= 202; i++)
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
                    StartSaleTime = (i == 1 ? (Convert.ToDateTime(issueDate.AddDays(-1).ToString("yyyy/MM/dd") + " 23:55:00")) : issueDate.AddSeconds(-seconds)),
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
                return 25;
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
                return -(60 * 5);
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
