using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.AutoLogic
{
    public class LotteryBackNum
    {
        /// <summary>
        /// 彩种编号
        /// </summary>
        public string LotteryCode { get; set; }

        /// <summary>
        /// 1800返点
        /// </summary>
        public decimal Back1800 { get; set; }


        /// <summary>
        /// 1700返点
        /// </summary>
        public decimal Back1700 { get; set; }
    }
}
