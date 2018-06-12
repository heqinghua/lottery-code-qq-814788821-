using Quartz;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Scheduler.Comm;
using Ytg.Scheduler.Tasks.Jobs;

namespace Ytg.Scheduler.Tasks
{
    /// <summary>
    /// 获取开奖数据
    /// </summary>
    public class YtgJob : IYtgJob
    {  /// <summary>
        /// 是否按照官方开奖结果开奖 一分彩
        /// </summary>
        public static int AijFenfenAuto = 0;

        /// <summary>
        /// 是否按照官方开奖结果开奖  二分彩
        /// </summary>
        public static int AiJErFenAuto = 0;


        /// <summary>
        /// 是否按照官方开奖结果开奖  五分彩
        /// </summary>
        public static int AiJWuFenAuto = 0;

        /// <summary>
        /// 是否按照官方开奖结果开奖  河内五分彩
        /// </summary>
        public static int HeNeWuFenAuto = 0;

        /// <summary>
        /// 是否按照官方开奖结果开奖  印尼时时彩
        /// </summary>
        public static int YiNiWuFenAuto = 0;


        /// <summary>
        /// 是否按照官方开奖结果开奖  韩国1.5
        /// </summary>
        public static int Hg15Auto = 0;

        /// <summary>
        /// 是否按照官方开奖结果开奖  天津
        /// </summary>
        public static int tjAuto = 0;


        private LotteryIssuesData mLotteryIssuesData = null;

        public YtgJob()
        {
            mLotteryIssuesData = new LotteryIssuesData();
        }

        public IDictionary<Quartz.IJobDetail, IList<Quartz.ITrigger>> Initital()
        {
            LogManager.Info("开始初始化任务！");
            IJobDetail jobDetail = JobBuilder.Create(typeof(OpenLotteryResultJob)).Build();
            IJobDetail jobBuilderDetail = JobBuilder.Create(typeof(GenerateLotteryIssueJob)).Build();
            IJobDetail jobBuilderJxsscDetail = JobBuilder.Create(typeof(GenerateJxsscLotteryIssueJob)).Build();
            IJobDetail jobBuilderShangHaishishilDetail = JobBuilder.Create(typeof(GenerateShangHaiSSlLotteryIssueJob)).Build();
            //清除数据
            IJobDetail datarecoveryDetail = JobBuilder.Create(typeof(DataRecoveryJob)).Build();

            IJobDetail yifenJobDetail = JobBuilder.Create(typeof(OpenYifenLotteryResultJob)).Build();//自主开奖任务集合
            IJobDetail openOfficialJobDetail = JobBuilder.Create(typeof(OpenOfficialResultJob)).Build();//50秒执行一次任务,用于开奖
            IJobDetail fen_OpenOfficialJobDetail = JobBuilder.Create(typeof(OpenOfficialResultJob)).Build();//10秒执行一次任务,用于开奖(分分彩、两分彩，)

            //从数据库里面得到所有的任务触发器 ,
           // var list = CreateTriggers(mLotteryIssuesData.GetNowDayIssue());
            //List<ITrigger> fc_plList = new List<ITrigger>();
            ////福彩3d 排列3
            //var fcplSource = mLotteryIssuesData.GetAllIssue();
            //foreach (var item in fcplSource)
            //    fc_plList.Add(CreateTriggerDay(item));


            IDictionary<IJobDetail, IList<ITrigger>> dict = new Dictionary<IJobDetail, IList<ITrigger>>();
            //dict.Add(jobDetail, fc_plList);//构建福彩、排列5开奖任务
            //LogManager.Info("构建福彩、排列5开奖任务成功！");
            dict.Add(openOfficialJobDetail, CreateOpenResultTrigger());//构建开奖任务//
            dict.Add(fen_OpenOfficialJobDetail, CreateOpenFenFenResultTrigger());//构建开奖任务// 10秒执行一次任务，用于开奖（分分彩、两分彩）
            LogManager.Info("构建开奖任务成功！");
            dict.Add(jobBuilderDetail, CreateBuilderIssuesTrigger());//构建生成期数任务
            LogManager.Info("构建生成重庆时时彩任务成功！");
            dict.Add(jobBuilderJxsscDetail, CreateBuilderJXSSCIssuesTrigger());//构建生成期数任务
            LogManager.Info("构建CreateBuilderJXSSCIssuesTrigger成功！");
            dict.Add(jobBuilderShangHaishishilDetail, CreateBuilderShangHaishishilIssuesTrigger());//构建生成期数任务
            LogManager.Info("构建ShangHaishishilIssuesTrigger成功！");
            dict.Add(datarecoveryDetail, CreateDataRecoveryTrigger());//清除数据
            LogManager.Info("构建CreateDataRecoveryTrigger成功！");
            dict.Add(yifenJobDetail, CreateTriggers(mLotteryIssuesData.GetFenFenCaiIssue()));//构建自主开奖任务
            LogManager.Info("构建自主开奖任务成功！");
            LogManager.Info("初始化结束！");

            Tasks.tencent.Pcqq.Run();
            LogManager.Info("初始化腾讯分分彩任务开始");
            return dict;
        }

        public List<ITrigger> CreateTriggers(IEnumerable<LotteryIssue> result)
        {
            //LogManager.Info( string.Format("CreateTasks 获取任务总数为{0}", result.Count()));
            List<ITrigger> tasks = new List<ITrigger>();
            //获取当天数据
            foreach (var item in result)
            {
                //构建task
                tasks.Add(CreateTrigger(item));
            }

            return tasks;
        }


        /// <summary>
        /// 创建task ITrigger
        /// </summary>
        /// <param name="issue"></param>
        private ITrigger CreateTrigger(LotteryIssue issue)
        {
            string taskNum = issue.IssueCode+","+issue.StartTime.ToString("yyyy/MM/dd");
            string groupName = LotteryIssuesData.GetAllLotterys().Where(l=>l.Id==issue.LotteryId).FirstOrDefault().LotteryCode;

            ITrigger trigger = new CronTriggerImpl(taskNum, groupName, issue.EndTime.Value.ToString("ss mm HH") + " * * ?");
            return trigger;
        }

        /// <summary>
        /// 创建task ITrigger
        /// </summary>
        /// <param name="issue"></param>
        private ITrigger CreateTriggerDay(LotteryIssue issue)
        {
            string taskNum = issue.IssueCode + "," + issue.StartTime.ToString("yyyy/MM/dd");
            string groupName = LotteryIssuesData.GetAllLotterys().Where(l => l.Id == issue.LotteryId).FirstOrDefault().LotteryCode;
            
            ITrigger trigger = new CronTriggerImpl(taskNum, groupName, issue.EndTime.Value.ToString("ss mm HH dd") + " * ?");
            return trigger;
        }


        /// <summary>
        /// 构建生成期数trigger
        /// </summary>
        /// <returns></returns>
        private ITrigger[] CreateBuilderIssuesTrigger()
        {
            return new ITrigger[] { 
              new CronTriggerImpl("BUILDER_Issues_","AutoBuilderIssues", ConfigHelper.GetApiLotteryBuildIssue)
            };
        }

        /// <summary>
        /// 构建江西时时彩生成期数trigger
        /// </summary>
        /// <returns></returns>
        private ITrigger[] CreateBuilderJXSSCIssuesTrigger()
        {
            return new ITrigger[] { 
              new CronTriggerImpl("BUILDER_Issues_","AutoBuilderJXSSCIssues", ConfigHelper.GetApiLotteryJXSSCBuildIssue)
            };
        }


        /// <summary>
        /// 构建上海时时乐生成期数trigger
        /// </summary>
        /// <returns></returns>
        private ITrigger[] CreateBuilderShangHaishishilIssuesTrigger()
        {
            return new ITrigger[] { 
              new CronTriggerImpl("BUILDER_Issues_","AutoBuilderSHangHaiCIssues", ConfigHelper.GetApiLotteryShangHaiShiShilBuildIssue)
            };
        }



        /// <summary>
        /// 构建清除数据trigger
        /// </summary>
        /// <returns></returns>
        private ITrigger[] CreateDataRecoveryTrigger()
        {
            return new ITrigger[] { 
              new CronTriggerImpl("_Data_Recovery_","Recovery", ConfigHelper.GetDataRecovery)
            };
        }

        #region  构建用于开奖任务
        /// <summary>
        /// 构建用于开奖任务
        /// </summary>
        /// <returns></returns>
        private ITrigger[] CreateOpenResultTrigger()
        {
            return new ITrigger[] { 
              new CronTriggerImpl("openresult_task","CreateOpenResultTrigger", ConfigHelper.OpenResultwhileOpenResult)
            };
        }
        #endregion

        #region  构建用于开奖任务 分分彩和两分彩票
        /// <summary>
        /// 构建用于开奖任务
        /// </summary>
        /// <returns></returns>
        private ITrigger[] CreateOpenFenFenResultTrigger()
        {
            return new ITrigger[] { 
              new CronTriggerImpl("open_fenfen_result_task","CreateOpenFenFenResultTrigger", ConfigHelper.OpenFenFenResultwhileOpenResult)
            };
        }
        #endregion
    }
}
