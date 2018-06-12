using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using Ytg.Scheduler.Comm;
using Ytg.BasicModel;
using Ytg.Scheduler.Comm.Bets;

namespace Ytg.Scheduler.Tasks
{
    /// <summary>
    /// 实现 开奖结果
    /// </summary>
    public class OpenLotteryResultJob : IJob
    {
        readonly HttpHelper mHttpHelper;

        readonly LotteryIssuesData mLotteryIssuesData;

        readonly BetDetailsCalculate mBetDetailsCalculate;

        public OpenLotteryResultJob()
        {
            mHttpHelper = new HttpHelper();
            mLotteryIssuesData = new LotteryIssuesData();
            mBetDetailsCalculate = new BetDetailsCalculate();
        }


        public void Execute(IJobExecutionContext context)
        {
            // 这个是彩票编号后编号
            var lottType = context.Trigger.Key.Group;
            var lotteryCode = context.Trigger.Key.Name;

            string occDay = DateTime.Now.ToString("yyyy/MM/dd");
            var codeArray = lotteryCode.Split(',');
            if (codeArray.Length == 2)
            {
                lotteryCode = codeArray[0];
                occDay = codeArray[1];
            }

            lotteryCode = OrganizationLotteryCode.GetCode(lottType, lotteryCode, occDay);

            try
            {
                //服务器抓取开奖数据
                GetLotteryResult(lottType, lotteryCode);
            }
            catch (System.Threading.ThreadAbortException ex)
            {
                LogManager.Error("OpenLotteryResultJob->Execute ThreadAbortException " + ex.Message + " :::" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                LogManager.Error(string.Format("OpenLotteryResultJob->Execute while Exception Message={0},StackTrace={1},Source={2}", ex.Message + "_" + lotteryCode + "__" + lottType, ex.StackTrace, ex.Source));
            }
        }

        public void GetLotteryResult(string type, string lotteryCode)
        {

            //api 请求url
            string url = ConfigHelper.BuilderApiUrl(type);

            LogManager.Info(string.Format("开始抓取开奖数据： type={0},lotteryCode={1}", type, lotteryCode));
            //获取开奖结果，如未获取到数据，则轮询指定次数
            int whileCount = 0;
            while (whileCount < ConfigHelper.GetApiLotteryRound)
            {
                var result = mHttpHelper.DoGet(url);
                string log = string.Format("expect={0} ,opencode={1} opentime={2} type={3}", result.FirstOrDefault().expect, result.FirstOrDefault().opencode, result.FirstOrDefault().opentime, type);
                LogManager.Info(log);
                if (UpdateLotteryIssueResult(lotteryCode, type, result))
                    break;//验证，处理成功，跳出循环

                whileCount++;//继续循环拉取数据
                System.Threading.Thread.Sleep(ConfigHelper.GetApiLotterySleep);
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
        private bool UpdateLotteryIssueResult(string lotteryCode, string lotteryType, List<OpenResultEntity> result)
        {
            if (null == result || result.Count < 1)
                return false;

            OpenResultEntity curItem = null;
            foreach (var item in result)
            {
                try
                {
                    if (lotteryType.ToLower() == "bjkl8" && item.opencode.IndexOf('+') != -1)
                    {
                        //北京快乐8，去掉飞盘
                        item.opencode = item.opencode.Split('+')[0];
                    }
                    if (item.expect.Trim() == lotteryCode.Trim())
                    {
                        curItem = item;
                        break;
                    }
                }
                catch (Exception ex)
                {

                    LogManager.Error("OpenLotteryResultJob->UpdateLotteryIssueResult异常", ex);
                }
            }

            if (curItem == null)
                return false;

            bool isCompled = mLotteryIssuesData.UpdateResult(curItem, lotteryType);
            if (isCompled)
            {
                mBetDetailsCalculate.Calculate(lotteryType, curItem.expect, curItem.opencode);//计算投注明细中奖金额
                LogManager.Info(string.Format("同步数据库成功： type={0},lotteryCode={1}", lotteryType, lotteryCode));
            }
            return isCompled;
        }
    }
}
