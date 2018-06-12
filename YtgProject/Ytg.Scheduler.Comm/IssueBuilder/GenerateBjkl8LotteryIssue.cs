using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 北京快乐8
    /// 快乐8开奖结果：北京快乐8开奖结果，每日179期，每天9:00 - 23:55，每5分钟公布一次北京快乐8开奖结果。
    /// </summary>
    public class GenerateBjkl8LotteryIssue: BaseGenerateSscLotteryIssue
    {
        DateTime stDate =Convert.ToDateTime( "2015/04/21");//key 2015/04/21 690685

        int issueCode=Convert.ToInt32(ConfigHelper.Bjkl8IssueCode);//2015/04/21 09点期号

        /// <summary>
        /// 生成
        /// </summary>
        public List<LotteryIssue> Generate(DateTime issueDate){
        
            List<LotteryIssue> result = new List<LotteryIssue>();
            DateTime beginDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 09:00:00");
            result.AddRange(Builder(beginDate, 5 * 60, 1, 179));

            return result;
        }

        protected override string BuilderIssueCode(DateTime endDate, string code)
        {
            int days = Convert.ToInt32(Convert.ToDateTime(endDate.ToString("yyyy/MM/dd")).Subtract(stDate).TotalDays);

            int value = issueCode + days * 179;
            value += Convert.ToInt32(code) - 1;
            return value.ToString();
        }

        protected override int LotteryId
        {
            get { return 10; }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return 0;
            }
        }

      
    }
}
