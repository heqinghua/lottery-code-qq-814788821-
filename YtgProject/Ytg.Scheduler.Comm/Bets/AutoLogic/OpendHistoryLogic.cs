using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.AutoLogic
{
    public class OpendHistoryLogic
    {
        
        //记录奖金历史总额
        private Dictionary<string, OpendHistory> mOpendHistory = new Dictionary<string, OpendHistory>();


        private static object LockObject = new object();

        

        private OpendHistoryLogic()
        {
          
        }


        private static OpendHistoryLogic mOpendHistoryLogic = null;//

        public static OpendHistoryLogic CreateInstance()
        {
            if (mOpendHistoryLogic == null)
                mOpendHistoryLogic = new OpendHistoryLogic();
            return mOpendHistoryLogic;
        }


        /// <summary>
        /// 可释放的奖金 若返回的值，小于0，则 不允许释放金额
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <returns></returns>
        public decimal GetWinMonery(string lotteryCode)
        {
            lock (LockObject)
            {
                OpendHistory outOpendHistory = null;
                if (!mOpendHistory.TryGetValue(lotteryCode, out outOpendHistory))
                    return 0;

                var disMonery= outOpendHistory.DisposeMonery();
                if (disMonery < 0)
                    return 0;
                return disMonery;
            }
        }


        /// <summary>
        /// 记录数据
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <param name="bettMonery"></param>
        /// <param name="winMonery"></param>
        /// <returns></returns>
        public void PutLotteryData(string lotteryCode, decimal bettMonery, decimal winMonery)
        {
            lock (LockObject)
            {
                OpendHistory outOpendHistory = null;
                if (!mOpendHistory.TryGetValue(lotteryCode, out outOpendHistory))
                {
                    outOpendHistory = new OpendHistory();
                    mOpendHistory.Add(lotteryCode, outOpendHistory);
                }
                //获取返点比
                outOpendHistory.BettMonery += bettMonery;
                if (winMonery > 0)
                {
                    outOpendHistory.Inint() ;
                }

                LogManager.Error(outOpendHistory.ToString());
            }
        }



    }
}
