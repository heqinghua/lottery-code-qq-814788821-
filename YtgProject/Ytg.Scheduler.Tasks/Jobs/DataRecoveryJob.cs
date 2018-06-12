using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm;

namespace Ytg.Scheduler.Tasks.Jobs
{
    /// <summary>
    /// 数据清理任务 自动清理三天前的数据，
    /// 该任务三天执行一次，并且在凌晨三点执行
    /// </summary>
    public class DataRecoveryJob : IJob
    {
        readonly LotteryIssuesData mLotteryIssuesData;

        public DataRecoveryJob()
        {
            mLotteryIssuesData = new LotteryIssuesData();
        }

        public void Execute(IJobExecutionContext context)
        {
            LogManager.Info("开始执行自动清除数据存储过程!");
            
            try
            {
                mLotteryIssuesData.DataRecovery();
                LogManager.Info("执行自动清除数据存储过程成功!");
            }
            catch (Exception ex)
            {
                LogManager.Error("执行自动清除数据存储过程成功异常:" + ex.Message);
            }
            
        }
    }

    
}
