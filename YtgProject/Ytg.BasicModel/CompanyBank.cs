using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    public class CompanyBank : EnaEntity
    {
        public int BankId { get; set; }

        /// <summary>
        /// 银行
        /// </summary>
        [JsonIgnore]
        public virtual SysBankType Bank { get; set; }

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
        /// 转账的时候需要提供支行信息及开户省份信息
        /// </summary>
        public bool IsProvideBranch { get; set; }

        /// <summary>
        /// 是否为入金银行
        /// </summary>
        public bool IsIncomingBank { get; set; }
    }
}
