using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class VdBankDTO
    {
        public int BankId { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankNo { get; set; }

        /// <summary>
        /// 开户人
        /// </summary>
        public string BankOwner { get; set; }
    }
}
