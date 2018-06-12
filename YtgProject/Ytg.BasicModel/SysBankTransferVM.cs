using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    public class SysBankTransferVM
    {
        public int Id { get; set; }
        public int BankId { get; set; }

        public string BankName { get; set; }

        public string BankLogo { get; set; }

        public string BankWebUrl { get; set; }

        public decimal MinAmt { get; set; }

        public decimal MaxAmt { get; set; }

        public decimal VipMinAmt { get; set; }

        public decimal VipMaxAmt { get; set; }

        public decimal Poundage { get; set; }

        public string BeginTime { get; set; }

        public string EndTime { get; set; }

        public bool IsRecharge { get; set; }

        public string IsEnableDesc { get; set; }

        public DateTime OccDate { get; set; }

        public int TotalCount { get; set; }

        /// <summary>
        /// 是否开通vip 充提
        /// </summary>
        public bool IsOpenVip { get; set; }
    }
}
