using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm;
using Ytg.Scheduler.Comm.IssueBuilder;
using Ytg.Scheduler.Service.BootStrapper;

namespace Ytg.Scheduler.Service
{
    public class InintData
    {
      

        public static void Initital()
        {

            ThreadPool.QueueUserWorkItem(InintSsc, DateTime.Now);
            ThreadPool.QueueUserWorkItem(InintSsc, DateTime.Now.AddDays(1));
            ThreadPool.QueueUserWorkItem(InintSsc, DateTime.Now.AddDays(2));

            ThreadPool.QueueUserWorkItem(InintFc, DateTime.Now);
            ThreadPool.QueueUserWorkItem(InintFc, DateTime.Now.AddDays(1));
            ThreadPool.QueueUserWorkItem(InintFc, DateTime.Now.AddDays(2));

            ThreadPool.QueueUserWorkItem(InintZiZhu, DateTime.Now);
            ThreadPool.QueueUserWorkItem(InintZiZhu, DateTime.Now.AddDays(1));
            ThreadPool.QueueUserWorkItem(InintZiZhu, DateTime.Now.AddDays(2));


            ThreadPool.QueueUserWorkItem(InintJx, DateTime.Now);
            ThreadPool.QueueUserWorkItem(InintJx, DateTime.Now.AddDays(1));
            ThreadPool.QueueUserWorkItem(InintJx, DateTime.Now.AddDays(2));

            ThreadPool.QueueUserWorkItem(InintZiZhu11x5, DateTime.Now);
            ThreadPool.QueueUserWorkItem(InintZiZhu11x5,DateTime.Now.AddDays(1));
            ThreadPool.QueueUserWorkItem(InintZiZhu11x5, DateTime.Now.AddDays(2));

            var mLotteryIssuesData = new LotteryIssuesData();
            //福彩3d
            var sourceFc3d = new GrnerateFc3dLotteryIssue().Generate();
            foreach (var item in sourceFc3d)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
              
            }
            LogManager.Info(string.Format("初始化福彩3d成功，总期数={0}！", sourceFc3d.Count));

            //排列3、5
            var pl35Source = new GrneratePl35LotteryIssue().Generate();
            foreach (var item in pl35Source)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
            }
            LogManager.Info(string.Format("初始化福彩3d成功，总期数={0}！", pl35Source.Count));

        }

        private static void InintSsc(object param)
        {
            DateTime dt = Convert.ToDateTime(param);
            string dtStr = dt.ToString("yyyy/MM/dd");
            var mLotteryIssuesData = new LotteryIssuesData();
            var source = new Ytg.Scheduler.Comm.IssueBuilder.GenerateSscLotteryIssue().Generate(dt);
            foreach (var item in source)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
            }
            LogManager.Info(string.Format("初始化重庆时时彩{0}成功，总期数={1}！", dtStr,source.Count));
            //江西时时彩
//            var sourcejxssc = new GenerateJxSscLotteryIssue().Generate(dt);
//            foreach (var item in sourcejxssc)
//            {
//                mLotteryIssuesData.AddLotteryIssueCode(item);
////                string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
//              //  Console.WriteLine(r);
//            }
//            LogManager.Info(string.Format("初始化江西时时彩{0}成功，总期数={1}！", dtStr, sourcejxssc.Count));
            //黑龙江是时时彩 暂不生成
            var sourceHljssc = new GenerateHljSscLotteryIssue().Generate(dt);
            foreach (var item in sourceHljssc)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                //string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                //Console.WriteLine(r);
            }
            //LogManager.Info(string.Format("初始化江西时时彩{0}成功！", dtStr));
            //新疆时时彩
            var sourceXjssc = new GenerateXjsscLotteryIssue().Generate(dt);
            foreach (var item in sourceXjssc)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                //string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                //Console.WriteLine(r);
            }
            LogManager.Info(string.Format("初始化新疆时时彩{0}成功，总期数={1}！", dtStr, sourceXjssc.Count));
            //天津是时时彩
            var sourceTjssc = new GenerateTjsscLotteryIssue().Generate(dt);
            foreach (var item in sourceTjssc)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                //string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                //Console.WriteLine(r);
            }
            
            LogManager.Info(string.Format("初始化天津是时时彩{0}成功，总期数={1}！", dtStr, sourceTjssc.Count));
        }

        private static void InintFc(object param)
        {
            var mLotteryIssuesData = new LotteryIssuesData();
            DateTime dt = Convert.ToDateTime(param);
            string dtStr = dt.ToString("yyyy/MM/dd");
            //十一选五
            var source11x5 = new Generategd11x5LotteryIssue().Generate(dt);
            foreach (var item in source11x5)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                //string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                //Console.WriteLine(r);
            }
            LogManager.Info(string.Format("初始化广东十一选五{0}成功，总期数={1}！", dtStr, source11x5.Count));

            //上海时时乐
            var sourceShssl = new GenerateShangHaiSslLotteryIssue().Generate(dt);
            foreach (var item in sourceShssl)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                //string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                //Console.WriteLine(r);
            }
            LogManager.Info(string.Format("初始化上海时时乐{0}成功，总期数={1}！", dtStr, sourceShssl.Count));
            ////北京快乐8
            //var sourceBjkl8 = new GenerateBjkl8LotteryIssue().Generate(dt);
            //foreach (var item in sourceBjkl8)
            //{
            //    mLotteryIssuesData.AddLotteryIssueCode(item);
            //    string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
            //    Console.WriteLine(r);
            //}
            
        }

        private static void InintZiZhu(object param)
        {
            DateTime dt = Convert.ToDateTime(param);
            string dtStr = dt.ToString("yyyy/MM/dd");
            var mLotteryIssuesData = new LotteryIssuesData();
            //一分彩
            var sourcefenfecai = new Ytg.Scheduler.Comm.IssueBuilder.GenerategdYiFenCaiLotteryIssue().Generate(dt);
            foreach (var item in sourcefenfecai)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                //string r = string.Format("分分彩 IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                //Console.WriteLine(r);
            }
            LogManager.Info(string.Format("初始化一分彩{0}成功，总期数={1}！", dtStr, sourcefenfecai.Count));
            //二分彩
            var sourceerfencai = new Ytg.Scheduler.Comm.IssueBuilder.GenerategdErFenCaiLotteryIssue().Generate(dt);
            foreach (var item in sourceerfencai)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                //string r = string.Format("二分彩 IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                //Console.WriteLine(r);
            }
            LogManager.Info(string.Format("初始化二分彩{0}成功，总期数={1}！", dtStr, sourceerfencai.Count));
            //五分彩
            var sourcewufencai = new Ytg.Scheduler.Comm.IssueBuilder.GenerategdWuFenCaiLotteryIssue().Generate(dt);
            foreach (var item in sourcewufencai)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                //string r = string.Format("五分彩 IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                //Console.WriteLine(r);
            }
            LogManager.Info(string.Format("初始化五分彩{0}成功，总期数={1}！", dtStr, sourcewufencai.Count));
        }


        private static void InintJx(object param)
        {
            DateTime dt = Convert.ToDateTime(param);
            string dtStr = dt.ToString("yyyy/MM/dd");
            var mLotteryIssuesData = new LotteryIssuesData();
            ///////江西十一选五

            var sourcejx11x5 = new Ytg.Scheduler.Comm.IssueBuilder.Generatejx11x5LotteryIssue().Generate(dt);
            foreach (var item in sourcejx11x5)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                //string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                //LogManager.Info("=====江西十一选五生成期数=============" + r);
            }
            LogManager.Info(string.Format("初始江西十一选五{0}成功，总期数={1}！", dtStr, sourcejx11x5.Count));

            /////山东十一选五
            var sourcesd11x5 = new Ytg.Scheduler.Comm.IssueBuilder.Generatesd11x5LotteryIssue().Generate(dt);
            foreach (var item in sourcesd11x5)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                //string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                //LogManager.Info("=====山东十一选五生成期数=============" + r);
            }
            LogManager.Info(string.Format("初始山东十一选五{0}成功，总期数={1}！", dtStr, sourcesd11x5.Count));
        }

        private static void InintZiZhu11x5(object param)
        {
            DateTime dt = Convert.ToDateTime(param);
            string dtStr = dt.ToString("yyyy/MM/dd");
            var mLotteryIssuesData = new LotteryIssuesData();
            //五分11选5
            var sourcewf11x5 = new Ytg.Scheduler.Comm.IssueBuilder.Generatewf11x5LotteryIssue().Generate(dt);
            foreach (var item in sourcewf11x5)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                //string r = string.Format("五分11选5 IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                //LogManager.Info("=====五分11选5=============" + r);
            }
            LogManager.Info(string.Format("初始山五分11选5{0}成功，总期数={1}！", dtStr, sourcewf11x5.Count));

            //二分11选5
            var sourceef11x5 = new Ytg.Scheduler.Comm.IssueBuilder.GenerateErf11x5LotteryIssue().Generate(dt);
            foreach (var item in sourceef11x5)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                //string r = string.Format("二分11选5 IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                //LogManager.Info("=====二分11选5=============" + r);
            }
            LogManager.Info(string.Format("初始山二分11选5{0}成功，总期数={1}！", dtStr, sourceef11x5.Count));
            //江苏快三
            var sourceefjsks = new Ytg.Scheduler.Comm.IssueBuilder.Generategdjsk3LotteryIssue().Generate(dt);
            foreach (var item in sourceefjsks)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                //string r = string.Format("江苏快三 IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                //LogManager.Info("=====江苏块3=============" + r);
            }
            LogManager.Info(string.Format("初始山江苏快三{0}成功，总期数={1}！", dtStr, sourceefjsks.Count));

            //埃及分分彩
            var aijifenfenSource = new Ytg.Scheduler.Comm.IssueBuilder.GenerateAijiYiFenCaiLotteryIssue().Generate(dt);
            foreach (var item in aijifenfenSource)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
            }
            LogManager.Info(string.Format("初始山埃及分分彩{0}成功，总期数={1}！", dtStr, aijifenfenSource.Count));

            //河内5分成
            var hnfenfenSource = new Ytg.Scheduler.Comm.IssueBuilder.GenerateHNSscLotteryIssue().Generate(dt);
            foreach (var item in hnfenfenSource)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
            }
            LogManager.Info(string.Format("初始河内时时彩{0}成功，总期数={1}！", dtStr, hnfenfenSource.Count));

            //构建埃及二分彩
            try
            {
                //埃及分分彩
                var aijierFenCai = new Ytg.Scheduler.Comm.IssueBuilder.GenerateAijiLiangFenSscLotteryIssue().Generate(dt);
                foreach (var item in aijierFenCai)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                }
                LogManager.Info(string.Format("初始埃及二分彩{0}成功，总期数={1}！", dtStr, aijierFenCai.Count));
            }
            catch (Exception ex)
            {
                LogManager.Error("埃及二分彩", ex);
            }

            //构建埃及五分彩
            try
            {
                var aijiSource = new Ytg.Scheduler.Comm.IssueBuilder.GenerateAiJiSscLotteryIssue().Generate(dt);
                foreach (var item in aijiSource)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                }
                LogManager.Info(string.Format("初始埃及五分彩{0}成功，总期数={1}！", dtStr, aijiSource.Count));
            }
            catch (Exception ex)
            {
                LogManager.Error("生成河埃及五分彩成异常：" + ex.Message);
            }


            //构建印尼时时彩
            try
            {
                var yinniSource = new Ytg.Scheduler.Comm.IssueBuilder.GenerateYNSscLotteryIssue().Generate(dt);
                foreach (var item in yinniSource)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                }
                LogManager.Info(string.Format("初始印尼时时彩{0}成功，总期数={1}！", dtStr, yinniSource.Count));
            }
            catch (Exception ex)
            {
                LogManager.Error("生成印尼彩成异常：" + ex.Message);
            }

            //pk10
            try
            {
                var pk10Source = new Ytg.Scheduler.Comm.IssueBuilder.GeneratePk_10LotteryIssue().Generate(dt);
                foreach (var item in pk10Source)
                {
                    mLotteryIssuesData.AddLotteryIssueCode(item);
                }
                LogManager.Info(string.Format("初始PK10{0}成功，总期数={1}！", dtStr, pk10Source.Count));
            }
            catch (Exception ex)
            {
                LogManager.Error("生成PK10成异常：" + ex.Message);
            }
            
        }
    }
}
