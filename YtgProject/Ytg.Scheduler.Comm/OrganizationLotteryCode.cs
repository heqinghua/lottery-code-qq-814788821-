using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm
{
    public class OrganizationLotteryCode
    {
        static DateTime stDate;

        static int issueCode;

        static OrganizationLotteryCode()
        {
            stDate = Convert.ToDateTime("2015/04/21");//key 2015/04/21 690685

            issueCode = Convert.ToInt32(ConfigHelper.Bjkl8IssueCode);//2015/04/21 09点期号
        }

        /// <summary>
        /// 组织期数编号
        /// </summary>
        /// <param name="lotteryType"></param>
        /// <param name="lotteryCode"></param>
        /// <returns></returns>
        public static string GetCode(string lotteryType, string issueCode,string beginDay)
        {
            string newIssueCode = issueCode;

            switch (lotteryType.ToLower())
            {
                case "cqssc":
                case "jxssc":
                case "xjssc":
                case "tjssc":
                    newIssueCode = DateTime.Now.ToString("yyyyMMdd") + issueCode.Substring(issueCode.Length - 3, 3);//20150501001
                    break;
                case "gd11x5":
                case "hljssc":
                case "shssl":
                    newIssueCode = DateTime.Now.ToString("yyyyMMdd") + issueCode.Substring(issueCode.Length - 2,2);//20150501001
                    break;
                case "bjkl8":
                    newIssueCode = BuilderIssueCode(issueCode,beginDay);
                    break;
               
            }

            return newIssueCode;
        }

        static string BuilderIssueCode(string nowIssueCode, string beginDay)
        {
            if (beginDay == DateTime.Now.ToString("yyyy-MM-dd"))
                return nowIssueCode;

            int nowCode = Convert.ToInt32(nowIssueCode);
            DateTime endDate = DateTime.Now;
            int days =(int)endDate.Subtract(stDate).TotalDays;
            int nowStartIssueCode = issueCode + days * 179;//当天第一期期号

            //任务开始日期的第一期号
            int oldDays =(int)Convert.ToDateTime(beginDay).Subtract(stDate).TotalDays;
            int oldStartIssueCode = issueCode + oldDays * 179;

            int issueNun = Convert.ToInt32(nowIssueCode) - oldStartIssueCode;//当前期号

            return (nowStartIssueCode + issueNun).ToString();//当前期号
        }


    }
}
