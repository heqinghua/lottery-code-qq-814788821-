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
    /// 用于修改用户余额、返点的队列
    /// </summary>
    public class OpenOfficialQueue
    {
        static object LockObject = new object();

        public static OpenOfficialQueue mOpenOfficialQueue = null;

        public static OpenOfficialQueue CreateInstance()
        {
            if (mOpenOfficialQueue == null)
                mOpenOfficialQueue = new OpenOfficialQueue();
            return mOpenOfficialQueue;
        }

    
        private OpenOfficialQueue(){

            ThreadPool.QueueUserWorkItem(UpdateBetDetail, null);//
        }

        private Queue<BetDetail> mQueue = new Queue<BetDetail>();//队列

        public void Put(BetDetail detail)
        {
            lock (LockObject)
            {
                mQueue.Enqueue(detail);
            }
        }
        /// <summary>
        /// 移除并返回底部对象
        /// </summary>
        public BetDetail Dequeue()
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
            var buyTogetherService=new BuyTogetherService(new Repo<BuyTogether>(factory));
            /****初始化结束**/
            while (true)
            {
                var betDetail = this.Dequeue();
                try
                {
                    if (null == betDetail)
                    {
                        LogManager.Info("同步数据队列中暂无数据，开始休眠一秒！");
                        System.Threading.Thread.Sleep(1000);//休眠一秒
                        continue;
                    }
                    if (betDetail.IsMatch)
                    {
                        if (betDetail.IsBuyTogether == 0)
                        {
                            //代购，使用原来方式进行处理
                            rebateHelper.UpdateUserBanance(betDetail.UserId, betDetail.WinMoney, TradeType.奖金派送, betDetail.BetCode, 0);
                            
                        }
                        else
                        {
                            var source= buyTogetherService.GetForBettid(betDetail.Id);
                            if (source != null && source.Count > 0)
                            {

                                var winMonery = betDetail.WinMoney;//总中奖总金额
                                var totalMonery = betDetail.TotalAmt;
                                decimal bili = 0m;
                                Console.WriteLine("处理合买/投注期数=" + betDetail.BetCode + "中奖金额=" + winMonery + " 投注金额=" + totalMonery);
                                foreach (var item in source)
                                {
                                    var subscription = item.Subscription;
                                    bili = subscription / totalMonery;
                                    var itemWinMonery = bili * winMonery;
                                    LogManager.Info("认购金额:" + subscription + "分配奖金:" + itemWinMonery);
                                    rebateHelper.UpdateUserBanance(betDetail.UserId, itemWinMonery, TradeType.奖金派送, item.BuyTogetherCode, 0);
                                    System.Threading.Thread.Sleep(1);
                                    //修改中奖金额和状态
                                    item.WinMonery = itemWinMonery;
                                    item.Stauts = BetResultType.Winning;

                                    LotteryIssuesData.UpdateBuyTogerher(betDetail.Id, 1, itemWinMonery);
                                }
                                

                                //处理自身用户奖金
                                bili = betDetail.Subscription / totalMonery;
                                var usWinMonery = bili * winMonery;
                                rebateHelper.UpdateUserBanance(betDetail.UserId, usWinMonery, TradeType.奖金派送, betDetail.BetCode, 0);
                            }
                        }
                    }

                    //线程同步计算返点
                    rebateHelper.BettingCalculate(betDetail.PrizeType, betDetail.UserId, betDetail.TotalAmt, betDetail.BetCode, rebateHelper.GetRadioMaxRemo(betDetail.PalyRadioCode, betDetail.BonusLevel));

                    LogManager.Info("修改投注信息成功！" + betDetail.ToString());
                }
                catch (Exception ex)
                {
                    LogManager.Error("修改投注用户信息异常,投注信息:" + betDetail.ToString() + "\n", ex);
                }
            }
        }
    }
}
