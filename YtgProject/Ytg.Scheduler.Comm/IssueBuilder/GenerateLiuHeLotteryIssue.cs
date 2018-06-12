using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 生成六合彩期数  一年152期 每周二和周六开奖 2015152 每晚9点33开奖
    /// </summary>
    public class GenerateLiuHeLotteryIssue : BaseGenerateSscLotteryIssue
    {
        /// <summary>
        /// 生成
        /// </summary>
        public List<LotteryIssue> Generate(DateTime issueDate)
        {

            int year = issueDate.Year;
            DateTime preBegin=DateTime.Parse(year + "/" + "01/01 21:34:00");//每年1月1号
            DateTime begin = preBegin;
            int dayCount = GetYearDays(year);


            List<LotteryIssue> issueCodes = new List<LotteryIssue>();
            int beginIssue=1;
            for (var i = 0; i < dayCount; i++)
            {
                int week=CaculateWeekDay(begin.Year,begin.Month,begin.Day);
                if (week == 1 || week == 3 || week == 5)
                {
                    //周二周四或周六
                    issueCodes.Add(new LotteryIssue()
                    {
                        IssueCode = year.ToString()+beginIssue.ToString("d3"),
                        LotteryId = LotteryId,
                        EndSaleTime = begin.Subtract(TimeSpan.FromSeconds(10)),//前10分钟停止下单
                        EndTime = begin,
                        LotteryTime = begin,
                        StartSaleTime = preBegin.AddMinutes(10),
                        StartTime = preBegin,
                    });
                    beginIssue++;
                }
                begin=begin.AddDays(1);
            }

            return issueCodes;
        }

        /// <summary>
        /// 获取当前年份总共多少天
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private int GetYearDays(int year)
        {
            if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
            {
                return 366;
            }

            return 365;

        }

        /// <summary>
        /// 根据年月日计算星期几(Label2.Text=CaculateWeekDay(2004,12,9);)
        /// </summary>
        /// <param name="y">年</param>
        /// <param name="m">月</param>
        /// <param name="d">日</param>
        /// <returns></returns>
        private int CaculateWeekDay(int y, int m, int d)
        {
            if (m == 1 || m == 2)
            {
                m += 12;
                y--;         //把一月和二月看成是上一年的十三月和十四月，例：如果是2004-1-10则换算成：2003-13-10来代入公式计算。
            }
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7;
            return week;
            //string weekstr = "";
            //switch (week)
            //{
            //    case 0: weekstr = "星期一"; break;
            //    case 1: weekstr = "星期二"; break;
            //    case 2: weekstr = "星期三"; break;
            //    case 3: weekstr = "星期四"; break;
            //    case 4: weekstr = "星期五"; break;
            //    case 5: weekstr = "星期六"; break;
            //    case 6: weekstr = "星期日"; break;
            //}

            //return weekstr;
        }


        protected override int LotteryId
        {
            get { return 21; }
        }
    }
}
