using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Scheduler.Comm;
using Ytg.Scheduler.Comm.Bets;

namespace Ytg.Scheduler.Tasks.Jobs
{
    /// <summary>
    /// 处理开奖任务，根据配置文件指定时间轮询
    /// </summary>
    public class OpenOfficialResultJob : IJob
    {
        readonly HttpHelper mHttpHelper;

        readonly LotteryIssuesData mLotteryIssuesData;


        //延迟开奖彩种
        readonly string DelayOpenLotteryCodes = "cqssc,JXSSC,hljssc,xjssc,tjssc,gd11x5,fc3d,pl5,VIFFC5,sd11x5,jx11x5,jsk3,INFFC5,FFC5,bjpk10";


        public OpenOfficialResultJob()
        {
            mHttpHelper = new HttpHelper();
            mLotteryIssuesData = new LotteryIssuesData();
         
        }


        public void Execute(IJobExecutionContext context)
        {
            var groupName = context.Trigger.Key.Name;
            int type = groupName == "openresult_task" ? 0 : 1;//0为普通 1 为一分彩盒2两分
            LogManager.Info("开始执行开奖任务！" + groupName);
            try
            {
                var result = mLotteryIssuesData.GetNowOpenIssue(type);//获取当前50分钟内存在未开奖数据的集合，进行开奖结果抓取
                foreach (var item in result)
                {
                    //api 请求url
                    string url = ConfigHelper.BuilderApiUrl(item);
                    var openResults = mHttpHelper.DoGet(url);//获取开奖对象集合
                    if (null != openResults)
                    {
                        PoolParam param = new PoolParam()
                        {
                            lotteryid = item,
                            LotteryIssues = openResults
                        };
                        ThreadPool.QueueUserWorkItem(UpdateLotteryIssueResult, param);
                    }
                }

                #region old
                //Dictionary<int, List<LotteryIssue>> asyDictionary = new Dictionary<int, List<LotteryIssue>>();
                //foreach (var item in result)
                //{
                //    List<LotteryIssue> issues = null;
                //    if (!asyDictionary.ContainsKey(item.LotteryId.Value))
                //    {
                //        issues = new List<LotteryIssue>();
                //        asyDictionary.Add(item.LotteryId.Value, issues);
                //    }
                //    else
                //    {
                //        issues = asyDictionary[item.LotteryId.Value];
                //    }
                //    string key = item.LotteryId + "," + item.IssueCode;
                //    if (CalculationQueue.CreateInstance().IsCompledTask(key))
                //    {
                //        issues.Add(item);
                //        LogManager.Error("添加队列key:" + key + " issue:" + item.IssueCode);
                //    }
                //    else
                //    {
                //        LogManager.Error("队列中已经存在key:" + key + " issue:" + item.IssueCode);
                //    }
                //}

                ////获取开奖
                //foreach (var key in asyDictionary.Keys)
                //{
                //    var issue = asyDictionary[key];
                //    string issueCode = "";
                //    issue.ForEach(x => issueCode += x.IssueCode + ",");
                //    LogManager.Info(string.Format("开始执行获取开奖数据逻辑 key={0} issue={1}", key, issueCode));
                //    ThreadPool.QueueUserWorkItem(new WaitCallback(GetLotteryResult), new PoolParam()
                //    {
                //        lottertId = key,
                //        LotteryIssues = issue
                //    });
                //}
                #endregion
            }
            catch (Exception ex)
            {
                LogManager.Error("执行开奖任务异常", ex);
            }
        }

        //public void GetLotteryResult(object param)
        //{
        //    PoolParam par = param as PoolParam;
        //    int lotteryid = par.lottertId;
        //    string lotteryType="";
        //    ConfigHelper.GetLotteryDictionary().TryGetValue(lotteryid, out lotteryType); 
        //    List<LotteryIssue> issues = par.LotteryIssues;

        //    //api 请求url
        //    string url = ConfigHelper.BuilderApiUrl(lotteryid);
        //    LogManager.Info("开始调用开奖接口，请求Url=" + url);
        //    //获取开奖结果，如未获取到数据，则轮询指定次数
        //    int whileCount = 0;
        //    while (whileCount < ConfigHelper.GetApiLotteryRound)
        //    {
        //        var result = mHttpHelper.DoGet(url);

        //        if (UpdateLotteryIssueResult(lotteryType, issues, result))
        //            break;//验证，处理成功，跳出循环

        //        whileCount++;//继续循环拉取数据
        //        System.Threading.Thread.Sleep(ConfigHelper.GetApiLotterySleep);
        //    }
        //}

        private void AutoOpenResult(string lotteryCode,string issueCode)
        {
            string key = lotteryCode + "_" + issueCode;
            bool iscompled = AutoLotteryC.CreateInstan().Add(key);
            if (!iscompled)
            {
                LogManager.Info(key + "正在运算，跳出再次运算！");
                return;
            }

            var mLotteryIssuesData = new LotteryIssuesData();
            //计算开奖结果
            Ytg.Scheduler.Comm.Bets.FenBetDetailsCalculate betDetailsCalculate = new Ytg.Scheduler.Comm.Bets.FenBetDetailsCalculate();
            string openresult = betDetailsCalculate.Calculate(lotteryCode,issueCode, "");
            bool compled = mLotteryIssuesData.UpdateResult(new OpenResultEntity()
            {
                expect = issueCode,
                opencode = openresult,
                opentime = DateTime.Now
            }, lotteryCode);
            if (compled)
            {
                //移除
                AutoLotteryC.CreateInstan().Remove(key);
                LogManager.Info(key + "运算完成，移除！");

            }
        }


        /// <summary>
        /// 调用服务 去更新数据库开奖结果表
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <param name="openTime"></param>
        /// <returns></returns>
        public void UpdateLotteryIssueResult(object obj)
        {
            PoolParam param= obj as PoolParam;
            int lotteryid=param.lotteryid;
            List<OpenResultEntity> result = param.LotteryIssues;

            string lotteryType = "";
            ConfigHelper.GetLotteryDictionary().TryGetValue(lotteryid, out lotteryType);


            var lotteryIssuesData = new LotteryIssuesData(); 
            var betDetailsCalculate = new BetDetailsCalculate();
            foreach (var item in result)
            {
                AutoSubmit.AutoSubmitBetting.Run(lotteryType);

                try
                {
                    if (lotteryType == "tenct")
                    {
                        //
                        if (YtgJob.AijFenfenAuto == 1)
                        {
                            AutoOpenResult(lotteryType, item.expect);
                            continue;
                        }
                    }
                    else if (lotteryType == "FFC1")
                    {
                        if (YtgJob.AiJErFenAuto == 1)
                        {
                            AutoOpenResult(lotteryType, item.expect);
                            continue;
                        }
                    }
                    else if (lotteryType == "twbingo")
                    {
                        if (YtgJob.AiJWuFenAuto == 1)
                        {
                            AutoOpenResult(lotteryType, item.expect);
                            continue;
                        }
                    }
                    else if (lotteryType == "krkeno")
                    {
                        if (YtgJob.Hg15Auto == 1)
                        {
                            AutoOpenResult(lotteryType, item.expect);
                            continue;
                        }
                    }
                    else if (lotteryType == "VIFFC5")
                    {
                        if (YtgJob.HeNeWuFenAuto == 1)
                        {
                            AutoOpenResult(lotteryType, item.expect);
                            continue;
                        }
                    }
                    else if (lotteryType == "INFFC5")
                    {
                        if (YtgJob.YiNiWuFenAuto == 1)
                        {
                            AutoOpenResult(lotteryType, item.expect);
                            continue;
                        }
                    }
                    else if (lotteryType == "xjssc")
                    {
                        if (YtgJob.tjAuto == 1)
                        {
                            AutoOpenResult(lotteryType, item.expect);
                            continue;
                        }
                    }


                    if (lotteryType.ToLower() == "bjkl8" && item.opencode.IndexOf('+') != -1)
                    {
                        //北京快乐8，去掉飞盘
                        item.opencode = item.opencode.Split('+')[0];
                    }
                    else if (lotteryType.ToLower() == "krkeno" || lotteryType.ToLower() == "twbingo")
                    {
                        //韩国1.5分彩
                        //01,04,05,08,
                        //10,11,14,15,
                        //20,22,23,26,
                        //29,31,34,43,
                        //57,61,73,79


                        //08,09,13,16,
                        //17,19,23,27,
                        //30,34,49,51,
                        //54,56,59,60,
                        //68,73,74,79 + 08
                        if (item.opencode.IndexOf('+') >= 0)
                        {
                            item.opencode = item.opencode.Split('+')[0];
                        }
                        var codeArray = item.opencode.Split(',');//开奖结果
                        if (codeArray.Length != 20)
                            return;
                        //四位计算和值尾数
                        var newCode = GetSumLastNum(codeArray.Skip(0).Take(4).ToArray()) + "," +
                            GetSumLastNum(codeArray.Skip(4).Take(4).ToArray()) + "," +
                            GetSumLastNum(codeArray.Skip(8).Take(4).ToArray()) + "," +
                            GetSumLastNum(codeArray.Skip(12).Take(4).ToArray()) + "," +
                            GetSumLastNum(codeArray.Skip(16).Take(4).ToArray());
                        item.opencode = newCode;

                    } 
                    LogManager.Error("******************获取开奖结果 lotteryid=" + lotteryid + "***opencode=" + item.opencode + "**************");
                    bool isCompled = lotteryIssuesData.UpdateResult(item, lotteryid);//保存开奖结果

                    if (isCompled)
                    {
                        //取消延迟开奖
                        //if (DelayOpenLotteryCodes.Contains(lotteryType))
                        //{
                        //    System.Threading.Thread.Sleep(1000 * 30);
                        //}
                        betDetailsCalculate.Calculate(lotteryType, item.expect, item.opencode);//计算投注明细中奖金额
                        LogManager.Info(string.Format("计算开奖成功： type={0},lotteryCode={1}", lotteryType, item.expect));
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Error("OpenLotteryResultJob->UpdateLotteryIssueResult异常", ex);
                }
            }

        }

        private string GetSumLastNum(string[] array)
        {
            var sum = Convert.ToInt32(array[0])+ Convert.ToInt32(array[1])+ Convert.ToInt32(array[2])+ Convert.ToInt32(array[3]);
            return sum.ToString().LastOrDefault().ToString();
        }
    }

    public class PoolParam
    {
        public int lotteryid { get; set; }

        public List<OpenResultEntity> LotteryIssues { get; set; }
    }
}
