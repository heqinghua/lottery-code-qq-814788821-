using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 生成福彩3d一天一期
    /// </summary>
    public class GrnerateFc3dLotteryIssue : BaseGenerateSscLotteryIssue
    {

        /// <summary>
        /// 生成福彩3d
        /// </summary>
        /// <param name="issueDate"></param>
        /// <returns></returns>
        public List<LotteryIssue> Generate()
        {
            var array =Convert.ToInt32(StopDays);//对应期号
            string dd = Convert.ToDateTime(OpenTime).ToString("HH:mm:ss");
            string start = Convert.ToDateTime(OpenTime).AddMinutes(1).ToString("HH:mm:ss");
            DateTime date = Convert.ToDateTime(OpenTime);
            int year = date.Year;
            List<LotteryIssue> result = new List<LotteryIssue>();
            //按照一天一期的逻辑进行生成
            while (true)
            {
                //
                DateTime endTime = Convert.ToDateTime(date.ToString("yyyy/MM/dd ") + dd);//开奖时间
                DateTime beginTime = Convert.ToDateTime(date.ToString("yyyy/MM/dd ") + start);//开奖时间
                DateTime endSaleTime = endTime.AddSeconds(EndSaleMinutes);

                string code = array.ToString();
                LotteryIssue issue = new LotteryIssue()
                {
                    LotteryId = LotteryId,
                    EndSaleTime = endSaleTime,
                    EndTime = endTime,
                    IssueCode = code,
                    LotteryTime = endTime,
                    StartSaleTime = beginTime,
                    StartTime = beginTime,
                };
                result.Add(issue);
                array++;

                date = endTime.AddDays(1);
                if (year != date.Year)
                    break;
            }

            return result;

        }

        protected override int LotteryId
        {
            get { return 7; }
        }

        /// <summary>
        /// 开奖日期
        /// </summary>
        protected virtual string OpenTime
        {
            get
            {
                return ConfigHelper.Fc3d_openTime;
            }
        }

        /// <summary>
        /// 福彩3d开奖日期对应开奖期号
        /// </summary>
        protected virtual string StopDays
        {
            get
            {
                return ConfigHelper.Fc3d_StopDay;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override int EndSaleMinutes
        {
            get
            {
                return -(14 * 60);
            }
        }
    }
}
