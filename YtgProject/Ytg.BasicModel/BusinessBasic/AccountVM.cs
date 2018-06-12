using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.BusinessBasic
{
    public class AccountVM
    {
        public string AccountNum { get; set; }

        public string UserName { get; set; }

        public DateTime Date { get; set; }

        public int type { get; set; }

        public int GameType { get; set; }

        public int PlayType { get; set; }

        public int PeriodNum { get; set; }

        public string Pattern { get; set; }

        /// <summary>
        ///收入
        /// </summary>
        public decimal Income { get; set; }
        /// <summary>
        ///支出
        /// </summary>
        public decimal Expenditure { get; set; }
        /// <summary>
        ///余额
        /// </summary>
        public decimal Balance { get; set; }
        public string Remark { get; set; }
    }
}
