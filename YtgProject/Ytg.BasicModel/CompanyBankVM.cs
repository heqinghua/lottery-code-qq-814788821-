using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    public class CompanyBankVM
    {
        public int Id { get; set; }

        /// <summary>
        /// 银行编号
        /// </summary>
        public int BankId { get; set; }

        /// <summary>
        /// 银行
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 银行帐号,工商银行就填绑定的Email
        /// </summary>
        public string BankNo { get; set; }

        /// <summary>
        /// 银行开户人
        /// </summary>
        public string BankOwner { get; set; }

        /// <summary>
        /// 开户省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 开户支行
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// 转账是否需要提供支行 省份信息
        /// </summary>
        public bool IsProvideBranch { get; set; }

        /// <summary>
        /// 银行地址
        /// </summary>
        public string BankWebUrl { get; set; }

        /// <summary>
        /// 是否为入金银行
        /// </summary>
        public bool IsIncomingBank { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IsProvideBranchDesc { get; set; }

        public string StatusDesc { get; set; }

        public int TotalCount { get; set; }

        public string Num { get; set; }
    }
}
