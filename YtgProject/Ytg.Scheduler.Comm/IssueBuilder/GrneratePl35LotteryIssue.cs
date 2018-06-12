using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.IssueBuilder
{
    /// <summary>
    /// 排列3、5
    /// </summary>
    public class GrneratePl35LotteryIssue : GrnerateFc3dLotteryIssue
    {
        protected override string OpenTime
        {
            get
            {
                return ConfigHelper.Pl_openTime;
            }
        }

        protected override string StopDays
        {
            get
            {
                return ConfigHelper.Pl_StopDay;
            }
        }

        protected override int LotteryId
        {
            get
            {
                return 9;
            }
        }
    }
}
