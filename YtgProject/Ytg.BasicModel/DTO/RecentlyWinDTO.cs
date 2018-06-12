using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class RecentlyWinDTO
    {
        public string Code { get; set; }

        public decimal WinMoney { get; set; }
        //t1.IssueCode,lt.LotteryName

        /// <summary>
        /// 期号
        /// </summary>
        public string IssueCode { get; set; }

        /// <summary>
        /// 彩种
        /// </summary>
        public string LotteryName { get; set; }
    }
}
