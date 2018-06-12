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
    ///黑龙江时时彩 - 每天84期，09:30起每10分钟一期
    /// </summary>
    public class GenerateHljSscLotteryIssue : BaseGenerateSscLotteryIssue
    {
       


        /// <summary>
        /// 生成
        /// </summary>
        public  List<LotteryIssue> Generate(DateTime issueDate)
        {
            string spers = System.Configuration.ConfigurationManager.AppSettings["hlgBuilder_sper"];//构建黑龙江时时彩期数生成规则
            if (string.IsNullOrEmpty(spers))
                return new List<LotteryIssue>();

            DateTime beginDate = DateTime.Now;
            string beginStr = "02";//通过02开头
            int startIssueValue = 0;
            if (!DateTime.TryParse(spers.Split(',')[0], out beginDate)) {
                return new List<LotteryIssue>();
            }
            if (!int.TryParse(spers.Split(',')[1], out startIssueValue))
            {
                return new List<LotteryIssue>();
            }
            //计算其实日期与当天相差天数
            var days= DateTime.Now.Subtract(beginDate).Days;
            startIssueValue = startIssueValue + 84 * days;//今天开始期数
            //9.30分开始，一天84期
            int seconds = 10 * 60;//十分钟一起

            List<LotteryIssue> source = new List<LotteryIssue>();
            for (var i = 1; i <= 84; i++)
            {

                DateTime endTime = beginDate.AddSeconds(seconds).AddSeconds(IssueEndTimeAppend);
                DateTime endSaleTime = endTime.AddSeconds(EndSaleMinutes);
                string issueCode = beginStr+startIssueValue.ToString("d5");
                LotteryIssue issue = new LotteryIssue()
                {
                    LotteryId = LotteryId,
                    EndSaleTime = endSaleTime,
                    EndTime = endTime,
                    IssueCode = issueCode,

                    LotteryTime = endTime,
                    StartSaleTime = beginDate,
                    StartTime = beginDate,
                };

                source.Add(issue);
                beginDate = endTime.AddSeconds(-IssueEndTimeAppend);
                startIssueValue++;
            }


            return source;
        }



        protected override int LotteryId
        {
            get { return 3; }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return 0;
            }
        }

        protected override int FormartCount
        {
            get
            {
                return 2;
            }
        }
    }
}
