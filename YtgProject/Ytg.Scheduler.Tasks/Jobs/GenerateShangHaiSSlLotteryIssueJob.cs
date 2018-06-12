using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm;

namespace Ytg.Scheduler.Tasks
{
    /// <summary>
    /// 生成上海时时乐
    /// </summary>
    public class GenerateShangHaiSSlLotteryIssueJob : IJob
    {
         readonly LotteryIssuesData mLotteryIssuesData;
         public GenerateShangHaiSSlLotteryIssueJob()
         {
             mLotteryIssuesData = new LotteryIssuesData();
         }

        public void Execute(IJobExecutionContext context)
        {

            LogManager.Info("开始执行GenerateJxsscLotteryIssueJob任务，生成上海时时乐、江苏快三期数！");
            CreateIssue(DateTime.Now);
            CreateIssue(DateTime.Now.AddDays(1));
            CreateIssue(DateTime.Now.AddDays(2));
        }

        private void CreateIssue(object param)
        {
            DateTime date = Convert.ToDateTime(param);
            string nextDate = date.ToString("yyyy/MM/dd");
            try
            {
                //上海时时乐
                var sourceShssl = new Ytg.Scheduler.Comm.IssueBuilder.GenerateShangHaiSslLotteryIssue().Generate(date);
                foreach (var item in sourceShssl)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                    // string r = string.Format("上海时时乐IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                    // LogManager.Info(r);
                }
                LogManager.Info("上海时时乐成功，总期数为：" + sourceShssl.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("生成上海时时乐异常", ex);
            }


            try
            {
                //江苏快三
                var sourceefjsks = new Ytg.Scheduler.Comm.IssueBuilder.Generategdjsk3LotteryIssue().Generate(date);
                foreach (var item in sourceefjsks)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                    //string r = string.Format("江苏快三 IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                    //LogManager.Info("=====江苏块3=============" + r);
                }
                LogManager.Info("上海江苏快三成功，总期数为：" + sourceefjsks.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("生成江苏块3异常", ex);
            }

           
        }
    }
}
