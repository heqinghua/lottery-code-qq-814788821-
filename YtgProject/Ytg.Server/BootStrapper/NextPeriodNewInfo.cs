using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ytg.ServerWeb.BootStrapper
{
    public class NextPeriodNewInfo
    {

        public int LotteryId { get; set; }

        public string LotteryCode { get; set;}

        /// <summary>
        /// 
        /// </summary>
        public string IssueCode { get; set; }

        public DateTime EndSaleTime { get; set;}

        public DateTime EndTime { get; set; }

        public string Result { get; set; } 


        public long SubTime { get; set; }


    }
}