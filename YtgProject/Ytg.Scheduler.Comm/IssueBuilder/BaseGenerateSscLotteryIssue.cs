using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Service.Logic;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    public abstract class BaseGenerateSscLotteryIssue
    {
        /// <summary>
        /// 当前种类id
        /// </summary>
        protected abstract int LotteryId { get; }

        /// <summary>
        /// 开奖推迟时间 秒
        /// </summary>
        protected virtual int EndSaleMinutes
        {
            get
            {
                return -SysSettingHelper.EndSaleMinutes;

            }
        }

        /// <summary>
        /// 编号长度
        /// </summary>
        protected virtual int FormartCount
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// 结束推迟时间
        /// </summary>
        protected virtual int IssueEndTimeAppend
        {
            get {
                return ConfigHelper.GetIssueEndTimeAppend;
            }
        }

        protected List<LotteryIssue> Builder(DateTime beginDate, int seconds, int beginCode, int count)
        {
            List<LotteryIssue> source = new List<LotteryIssue>();
            for (var i = 1; i <= count; i++)
            {

                DateTime endTime = beginDate.AddSeconds(seconds).AddSeconds(IssueEndTimeAppend);
                DateTime endSaleTime = endTime.AddSeconds(EndSaleMinutes);
                string code = beginCode.ToString();
                while (i != 0)
                {
                    if (code.Length < FormartCount)
                        code = "0" + code;
                    else
                        break;
                }

                string issueCode = BuilderIssueCode(endTime, code);
               

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
                beginCode++;
            }

            return source;
        }


        protected virtual string BuilderIssueCode(DateTime endDate, string code)
        {
            return endDate.ToString("yyyyMMdd") + code;
        }

        protected virtual string BuilderTaskIssueCode(DateTime endDate, string code)
        {
            return endDate.ToString("yyyyMMdd") + code;
        }

    }
}
