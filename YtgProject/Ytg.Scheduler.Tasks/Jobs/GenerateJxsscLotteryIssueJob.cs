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
    /// 生成江西时时彩期数
    /// </summary>
    public class GenerateJxsscLotteryIssueJob : IJob
    {
        readonly LotteryIssuesData mLotteryIssuesData;
        public GenerateJxsscLotteryIssueJob()
        {
            mLotteryIssuesData = new LotteryIssuesData();
        }

        public void Execute(IJobExecutionContext context)
        {
            LogManager.Info("开始执行GenerateJxsscLotteryIssueJob任务，生成江西、新疆、天津、广东十一选、江西十一选、山东十一选五期数！");
            CreateIssue(DateTime.Now);

            CreateIssue(DateTime.Now.AddDays(1));
            LogManager.Info("执行GenerateJxsscLotteryIssueJob任务，生成江西、新疆、天津、广东十一选、江西十一选、山东十一选五期数结束！");

            CreateIssue(DateTime.Now.AddDays(2));
            LogManager.Info("执行GenerateJxsscLotteryIssueJob任务，生成江西、新疆、天津、广东十一选、江西十一选、山东十一选五期数结束！");
        }

        private void CreateIssue(object param)
        {
            DateTime date = Convert.ToDateTime(param);
            string nextDate = date.ToString("yyyy/MM/dd");
            //在这里面调用服务
            //try
            //{
            //    var source = new Ytg.Scheduler.Comm.IssueBuilder.GenerateJxSscLotteryIssue().Generate(date);
            //    foreach (var item in source)
            //    {
            //        mLotteryIssuesData.AddLotteryIssueCode(item);
            //        // string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
            //        //  LogManager.Info("=====江西时时彩生成期数=============" + r);
            //    }
            //    LogManager.Info("生成江西时时彩成功，总期数为：" + source.Count + " 开奖日期：" + nextDate);
            //}
            //catch (Exception ex)
            //{
            //    LogManager.Error("生成江西时时彩异常：" + ex.Message);
            //}



            /////生成黑龙江时时彩期数

            var sourceHlj = new Ytg.Scheduler.Comm.IssueBuilder.GenerateHljSscLotteryIssue().Generate(DateTime.Now.AddDays(1));
            foreach (var item in sourceHlj)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                LogManager.Info("=====黑龙江时时彩生成期数=============" + r);
            }

            ////////////////
            /////生成新疆时时彩期数
            try
            {
                var sourceXjssc = new Ytg.Scheduler.Comm.IssueBuilder.GenerateXjsscLotteryIssue().Generate(date);
                foreach (var item in sourceXjssc)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                    // string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                    // LogManager.Info("=====新疆时时彩生成期数=============" + r);
                }
                LogManager.Info("生成新疆时时彩成功，总期数为：" + sourceXjssc.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("生成新疆时时彩异常：" + ex.Message);
            }

            /////生成天津时时彩期数
            try
            {
                var sourceTjssc = new Ytg.Scheduler.Comm.IssueBuilder.GenerateTjsscLotteryIssue().Generate(date);
                foreach (var item in sourceTjssc)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                    //string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                    //LogManager.Info("=====天津时时彩生成期数=============" + r);
                }
                LogManager.Info("生成天津时时彩成功，总期数为：" + sourceTjssc.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("生成天津时时彩异常：" + ex.Message);
            }

            /////广东十一选五

            try
            {
                var source11x5 = new Ytg.Scheduler.Comm.IssueBuilder.Generategd11x5LotteryIssue().Generate(date);
                foreach (var item in source11x5)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                    //string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                    //LogManager.Info("=====广东十一选五生成期数=============" + r);
                }
                LogManager.Info("生成广东11选5成功，总期数为：" + source11x5.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("生成广东11选5异常：" + ex.Message);
            }

            /////江西十一选五
            try
            {
                var sourcejx11x5 = new Ytg.Scheduler.Comm.IssueBuilder.Generatejx11x5LotteryIssue().Generate(date);
                foreach (var item in sourcejx11x5)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                    //string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                    // LogManager.Info("=====江西十一选五生成期数=============" + r);
                }
                LogManager.Info("生成江西11选5成功，总期数为：" + sourcejx11x5.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("生成江西11选5异常：" + ex.Message);
            }


            /////山东十一选五
            try
            {
                var sourcesd11x5 = new Ytg.Scheduler.Comm.IssueBuilder.Generatesd11x5LotteryIssue().Generate(date);
                foreach (var item in sourcesd11x5)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                    //string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                    //LogManager.Info("=====山东十一选五生成期数=============" + r);
                }
                LogManager.Info("生成山东11选5成功，总期数为：" + sourcesd11x5.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("生成山东11选5异常：" + ex.Message);
            }
            //构建河内5分彩
            try
            {
                var hnwfcSource = new Ytg.Scheduler.Comm.IssueBuilder.GenerateHNSscLotteryIssue().Generate(date);
                foreach (var item in hnwfcSource)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                }
                LogManager.Info("生成河内5分成成功，总期数为：" + hnwfcSource.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("生成河内5分成异常：" + ex.Message);
            }

            //构建埃及五分彩
            try
            {
                var aijiSource = new Ytg.Scheduler.Comm.IssueBuilder.GenerateAiJiSscLotteryIssue().Generate(date);
                foreach (var item in aijiSource)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                }
                LogManager.Info("生成河埃及五分彩成成功，总期数为：" + aijiSource.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("生成河埃及五分彩成异常：" + ex.Message);
            }


            //构建印尼时时彩
            try
            {
                var yinniSource = new Ytg.Scheduler.Comm.IssueBuilder.GenerateYNSscLotteryIssue().Generate(date);
                foreach (var item in yinniSource)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                }
                LogManager.Info("生成河埃及印尼彩成成功，总期数为：" + yinniSource.Count + " 开奖日期：" + nextDate);
            }
            catch (Exception ex)
            {
                LogManager.Error("生成河埃及印尼彩成异常：" + ex.Message);
            }

            //pk10
            try
            {
                var pk10Source = new Ytg.Scheduler.Comm.IssueBuilder.GeneratePk_10LotteryIssue().Generate(date);
                foreach (var item in pk10Source)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                }
                LogManager.Info(string.Format("初始PK10{0}成功，总期数={1}！",date.ToString("yyyy/MM/dd"), pk10Source.Count));
            }
            catch (Exception ex)
            {
                LogManager.Error("生成PK10成异常：" + ex.Message);
            }
        }
    }
}

