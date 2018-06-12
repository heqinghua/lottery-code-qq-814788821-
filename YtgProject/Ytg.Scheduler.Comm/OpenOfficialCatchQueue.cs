using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Data;
using Ytg.Service;
using Ytg.Service.Lott;

namespace Ytg.Scheduler.Comm
{
    /// <summary>
    /// 用于修改用户余额、返点的队列 用于追号
    /// </summary>
    public class OpenOfficialCatchQueue
    {
        static object LockObject = new object();

        public static OpenOfficialCatchQueue mOpenOfficialQueue = null;

        public static OpenOfficialCatchQueue CreateInstance()
        {
            if (mOpenOfficialQueue == null)
                mOpenOfficialQueue = new OpenOfficialCatchQueue();
            return mOpenOfficialQueue;
        }


        private OpenOfficialCatchQueue()
        {

            ThreadPool.QueueUserWorkItem(UpdateBetDetail, null);//
        }

        private Queue<OpenOfficialCatchQueueParam> mQueue = new Queue<OpenOfficialCatchQueueParam>();//队列

        public void Put(OpenOfficialCatchQueueParam detail)
        {
            lock (LockObject)
            {
                mQueue.Enqueue(detail);
            }
        }
        /// <summary>
        /// 移除并返回底部对象
        /// </summary>
        public OpenOfficialCatchQueueParam Dequeue()
        {
            lock (LockObject)
            {
                if (mQueue.Count > 0)
                    return mQueue.Dequeue();
            }
            return null;
        }

        /// <summary>
        /// 负责将对象同步至数据库
        /// </summary>
        private void UpdateBetDetail(object param)
        {
            /****初始化开始**/
            IDbContextFactory factory = new DbContextFactory();
            Ytg.Comm.IHasher hasher = new Ytg.Comm.Hasher();
            var sysUser = new SysUserService(new Repo<SysUser>(factory), hasher);
            var sysUserBalanceService = new SysUserBalanceService(new Repo<SysUserBalance>(factory), hasher, sysUser);
            var sysUserBalanceDetailService = new SysUserBalanceDetailService(new Repo<SysUserBalanceDetail>(factory), sysUserBalanceService);
            var rebateHelper = new Service.Logic.RebateHelper(sysUser, sysUserBalanceService, sysUserBalanceDetailService);
            /****初始化结束**/
            while (true)
            {
                var betDetail = this.Dequeue();
                try
                {
                    if (null == betDetail)
                    {
                        LogManager.Info("追号同步数据队列中暂无数据，开始休眠一秒！");
                        System.Threading.Thread.Sleep(1000);//休眠一秒
                        continue;
                    }
                        //计算返点 游戏返点

                    rebateHelper.BettingCalculate(betDetail.CatchDetail.PrizeType,betDetail.CatchDetail.UserId, betDetail.CatchNumIssue.TotalAmt, betDetail.CatchNumIssue.CatchNumIssueCode, rebateHelper.GetRadioMaxRemo(betDetail.CatchDetail.PalyRadioCode, betDetail.CatchDetail.BonusLevel));

                    if (betDetail.CatchNumIssue.IsMatch)//是否中奖
                        rebateHelper.UpdateUserBanance(betDetail.CatchDetail.UserId, betDetail.CatchNumIssue.WinMoney, TradeType.奖金派送, betDetail.CatchNumIssue.CatchNumIssueCode, 0);

                    if (null != betDetail.ExitCatNumIssues)
                    {
                        //是否有结束追号的期数
                        var exitCount = betDetail.ExitCatNumIssues.Count;
                        for (var i = 0; i < exitCount; i++)
                        {
                            var exit = betDetail.ExitCatNumIssues[i];
                            //if (betDetail.CatchDetail.PrizeType == 1)
                            //    rebateHelper.BettingCannelIssue(betDetail.CatchDetail.UserId, exit.TotalAmt, exit.CatchNumIssueCode, rebateHelper.GetRadioMaxRemo(betDetail.CatchDetail.PalyRadioCode, betDetail.CatchDetail.BonusLevel));//处理返点
                            //else
                            rebateHelper.UpdateUserBanance(betDetail.CatchDetail.UserId, exit.TotalAmt, TradeType.追号返款, exit.CatchNumIssueCode, 0);
                        }
                    }
                    LogManager.Info("修改追号信息成功！" + betDetail.ToString());
                }
                catch (Exception ex)
                {
                    LogManager.Error("修改追号用户信息异常,投注信息:" + betDetail.ToString() + "\n", ex);
                }
            }
        }
    }

    /// <summary>
    /// 追号任务参数
    /// </summary>
    public class OpenOfficialCatchQueueParam
    {
        /// <summary>
        /// 追号信息
        /// </summary>
        public BasicModel.LotteryBasic.DTO.NotCompledCatchNumListDTO CatchDetail { get; set; }

        /// <summary>
        /// 当前期数信息
        /// </summary>
        public CatchNumIssue CatchNumIssue { get; set; }
        /// <summary>
        /// 追号完成撤单总期数
        /// </summary>
        public List<CatchNumIssue> ExitCatNumIssues { get; set; }
    }
}
