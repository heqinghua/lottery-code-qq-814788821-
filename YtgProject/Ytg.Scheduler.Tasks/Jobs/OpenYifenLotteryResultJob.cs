using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm;
using Ytg.Scheduler.Comm.Bets;

namespace Ytg.Scheduler.Tasks.Jobs
{
    /// <summary>
    /// 一分彩开奖模块
    /// </summary>
    public class OpenYifenLotteryResultJob : IJob
    {

        readonly FenBetDetailsCalculate mBetDetailsCalculate;

        readonly LotteryIssuesData mLotteryIssuesData;

        public OpenYifenLotteryResultJob()
        {
            mBetDetailsCalculate = new FenBetDetailsCalculate();
            mLotteryIssuesData = new LotteryIssuesData();
        }

        public void Execute(IJobExecutionContext context)
        {
            // 这个是彩票编号后编号
            var lottType = context.Trigger.Key.Group;
            var lotteryCode = context.Trigger.Key.Name;

            
            var codeArray = lotteryCode.Split(',');
            if (codeArray.Length == 2)
            {
                //08270874
                lotteryCode = codeArray[0];
                LogManager.Info(string.Format("自主开奖开始========={0}==={1}======处理前任务期号{2}", lottType, lotteryCode, lotteryCode));
             //   occDay = codeArray[1];
                //移除code前四位
                lotteryCode=lotteryCode.Remove(0, 4);
                lotteryCode=DateTime.Now.ToString("MMdd") + lotteryCode;
                LogManager.Info(string.Format("自主开奖开始========={0}==={1}======处理后任务期号{2}", lottType, lotteryCode, lotteryCode));
            }

            try
            {

                /**
                 if (YtgJob.AijFenfenAuto == 1)
                        {
                            AutoOpenResult(lotteryType, item.expect);
                            continue;
                        }
                 */
                //if (lotteryCode == "yifencai")
                //{
                //    //一分彩，默认开前一天的数据

                //}
                //else
                //{

                //服务器抓取开奖数据
                string openresult = this.mBetDetailsCalculate.Calculate(lottType, lotteryCode, "");


                //延迟一分钟计算开奖结果 
               

                bool compled = mLotteryIssuesData.UpdateResult(new OpenResultEntity()
                {
                    expect = lotteryCode,
                    opencode = openresult,
                    opentime = DateTime.Now
                }, lottType);
                // }

            }
            catch (System.Threading.ThreadAbortException ex)
            {
                LogManager.Error("自主开奖ThreadAbortException ", ex);
            }
            catch (Exception ex)
            {
                LogManager.Error("自主开奖 while Exception ", ex);
            }

            LogManager.Info(string.Format("自主开奖结束========={0}==={1}===", lottType, lotteryCode));
        }



    }
}
