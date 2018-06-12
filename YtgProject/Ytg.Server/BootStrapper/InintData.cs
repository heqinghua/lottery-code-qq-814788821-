using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm;
using Ytg.Scheduler.Comm.IssueBuilder;

namespace Ytg.ServerWeb
{
    public class InintData
    {
        public static void Initital()
        {
            var mLotteryIssuesData = new LotteryIssuesData();
            var source = new Ytg.Scheduler.Comm.IssueBuilder.GenerateSscLotteryIssue().Generate(DateTime.Now);
            foreach (var item in source)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                Console.WriteLine(r);
            }
            //江西时时彩
            var sourcejxssc = new GenerateJxSscLotteryIssue().Generate(DateTime.Now);
            foreach (var item in sourcejxssc)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                Console.WriteLine(r);
            }
            //黑龙江是时时彩
            var sourceHljssc = new GenerateHljSscLotteryIssue().Generate(DateTime.Now);
            foreach (var item in sourceHljssc)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                Console.WriteLine(r);
            }
            //新疆时时彩
            var sourceXjssc = new GenerateXjsscLotteryIssue().Generate(DateTime.Now);
            foreach (var item in sourceXjssc)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                Console.WriteLine(r);
            }

            //天津是时时彩
            var sourceTjssc = new GenerateTjsscLotteryIssue().Generate(DateTime.Now);
            foreach (var item in sourceTjssc)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                Console.WriteLine(r);
            }

            //十一选五
            var source11x5 = new Generategd11x5LotteryIssue().Generate(DateTime.Now);
            foreach (var item in source11x5)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                Console.WriteLine(r);
            }

            //上海时时乐
            var sourceShssl = new GenerateShangHaiSslLotteryIssue().Generate(DateTime.Now);
            foreach (var item in sourceShssl)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                Console.WriteLine(r);
            }
            ////北京快乐8
            //var sourceBjkl8 = new GenerateBjkl8LotteryIssue().Generate(DateTime.Now);
            //foreach (var item in sourceBjkl8)
            //{
            //    mLotteryIssuesData.AddLotteryIssueCode(item);
            //    string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
            //    Console.WriteLine(r);
            //}
            //福彩3d
            var sourceFc3d = new GrnerateFc3dLotteryIssue().Generate();
            foreach (var item in sourceFc3d)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                Console.WriteLine(r);
            }
            //排列3、5
            var pl35Source = new GrneratePl35LotteryIssue().Generate();
            foreach (var item in pl35Source)
            {
                mLotteryIssuesData.AddLotteryIssueCode(item);
                string r = string.Format("IssueCode={0},StartTime={1},EndSaleTime={2},EndTime={3}", item.IssueCode, item.StartTime.ToString("dd HH:mm:ss"), item.EndSaleTime.Value.ToString("dd HH:mm:ss"), item.EndTime.Value.ToString("dd HH:mm:ss"));
                Console.WriteLine(r);
            }
            

        }
    }
}
