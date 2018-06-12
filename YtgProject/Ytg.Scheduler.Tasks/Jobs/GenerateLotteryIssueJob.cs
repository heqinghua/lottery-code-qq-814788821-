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
    /// 每天定时生成周期  
    /// </summary>
    public class GenerateLotteryIssueJob : IJob
    {
        readonly LotteryIssuesData mLotteryIssuesData;
        public GenerateLotteryIssueJob()
        {
            mLotteryIssuesData = new LotteryIssuesData();
        }

        public void Execute(IJobExecutionContext context)
        {
            LogManager.Info("开始执行GenerateJxsscLotteryIssueJob任务，生成重庆、一分、二分、五分、五分11选5、二分11选5期数！");
            CreateIssue(DateTime.Now);
            CreateIssue(DateTime.Now.AddDays(1));
            CreateIssue(DateTime.Now.AddDays(2));
        }

        private void CreateIssue(object param)
        {
            DateTime date = Convert.ToDateTime(param);
            string nextDate = date.ToString("yyyy/MM/dd");
            //在这里面调用服务
            try
            {
                var source = new Ytg.Scheduler.Comm.IssueBuilder.GenerateSscLotteryIssue().Generate(date);
                foreach (var item in source)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                    //string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                }
                LogManager.Info("生成重庆时时彩成功，总期数为：" + source.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("生成重庆时时彩异常", ex);
            }

            //
            //北京快乐8
            //var sourceBjkl8 = new Ytg.Scheduler.Comm.IssueBuilder.GenerateBjkl8LotteryIssue().Generate(date);
            //foreach (var item in sourceBjkl8)
            //{
            //    mLotteryIssuesData.AddLotteryIssueCode(item);
            //    string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
            //    LogManager.Info("=====生成北京快乐=============" + r);
            //}

            try
            {
                //一分彩
                var sourcefenfecai = new Ytg.Scheduler.Comm.IssueBuilder.GenerategdYiFenCaiLotteryIssue().Generate(date);
                foreach (var item in sourcefenfecai)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                    //string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                    //LogManager.Info("=====生成一分彩=============" + r);
                }
                LogManager.Info("生成一分彩成功，总期数为：" + sourcefenfecai.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("生成一分彩异常", ex);
            }

            try
            {
                //二分彩
                var sourceerfencai = new Ytg.Scheduler.Comm.IssueBuilder.GenerategdErFenCaiLotteryIssue().Generate(date);
                foreach (var item in sourceerfencai)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                    //string r = string.Format("二分彩 IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                    //LogManager.Info("=====生成二分彩=============" + r);
                }
                LogManager.Info("生成二分彩成功，总期数为：" + sourceerfencai.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("二分彩异常", ex);
            }
            try
            {
                //五分彩
                var sourcewufencai = new Ytg.Scheduler.Comm.IssueBuilder.GenerategdWuFenCaiLotteryIssue().Generate(date);
                foreach (var item in sourcewufencai)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                    //                    string r = string.Format("五分彩 IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                    //  LogManager.Info("=====生成五分彩=============" + r);
                }
                LogManager.Info("五分彩成功，总期数为：" + sourcewufencai.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("五分彩异常", ex);
            }

            try
            {

                //五分11选5
                var sourcewf11x5 = new Ytg.Scheduler.Comm.IssueBuilder.Generatewf11x5LotteryIssue().Generate(date);
                foreach (var item in sourcewf11x5)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                    //string r = string.Format("五分11选5 IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                    //LogManager.Info("=====五分11选5=============" + r);
                }
                LogManager.Info("五分11选5成功，总期数为：" + sourcewf11x5.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("五分11选5", ex);
            }

            try
            {
                //二分11选5
                var sourceef11x5 = new Ytg.Scheduler.Comm.IssueBuilder.GenerateErf11x5LotteryIssue().Generate(date);
                foreach (var item in sourceef11x5)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                    //string r = string.Format("二分11选5 IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                    // LogManager.Info("=====二分11选5=============" + r);
                }
                LogManager.Info("二分11选5成功，总期数为：" + sourceef11x5.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("二分11选5", ex);
            }

            //构建埃及二分彩
            try
            {
                //埃及分分彩
                var aijierFenCai = new Ytg.Scheduler.Comm.IssueBuilder.GenerateAijiLiangFenSscLotteryIssue().Generate(date);
                foreach (var item in aijierFenCai)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                }
                LogManager.Info("埃及分二分彩成功，总期数为：" + aijierFenCai.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("埃及二分彩", ex);
            }

            //构建埃及分分彩
             try
            {
                //埃及分分彩
                var aijiFenfenCai = new Ytg.Scheduler.Comm.IssueBuilder.GenerateAijiYiFenCaiLotteryIssue().Generate(date);
                foreach (var item in aijiFenfenCai)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                }
                LogManager.Info("埃及分分彩成功，总期数为：" + aijiFenfenCai.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("埃及分分彩", ex);
            }
            
        }
    }
}
