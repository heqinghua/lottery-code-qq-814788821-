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
    /// 每天销售23期，购彩时间为上午10：30至晚上22：00，每半小时开奖一次。
    /// </summary>
    public class GenerateShangHaiSslLotteryIssue : BaseGenerateSscLotteryIssue
    {
       
        /// <summary>
        /// 生成
        /// </summary>
        public  List<LotteryIssue> Generate(DateTime issueDate)
        {
            

            List<LotteryIssue> result = new List<LotteryIssue>();
            DateTime beginDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 10:00:20");
            result.AddRange(Builder(beginDate, 30*60,1, 23));

            return result;
        }



        protected override int LotteryId
        {
            get { return 8; }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return -(3*60);
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
