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
    /// 广东十一选五
    /// 共84期 早上09点10开始开奖 十分钟一期 晚上23点结束
    /// </summary>
    public class Generategd11x5LotteryIssue : BaseGenerateSscLotteryIssue
    {
       
        /// <summary>
        /// 生成
        /// </summary>
        public  List<LotteryIssue> Generate(DateTime issueDate)
        {
            

            List<LotteryIssue> result = new List<LotteryIssue>();
            DateTime beginDate = Convert.ToDateTime(issueDate.ToString("yyyy/MM/dd") + " 09:01:00");
            result.AddRange(Builder(beginDate, 10*60,1, 84));

            return result;
        }

        protected override int FormartCount
        {
            get
            {
                return 2;
            }
        }


        protected override int LotteryId
        {
            get { return 6; }
        }

        protected override int EndSaleMinutes
        {
            get
            {
                return -((ConfigHelper.GetEndSaleMinutes_wufencs+40)+60);
            }
        }
    }

  
}
