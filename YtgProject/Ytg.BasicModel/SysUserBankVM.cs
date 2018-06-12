using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 用户绑定银行卡VM
    /// </summary>
    public class SysUserBankVM
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 对应银行id
        /// </summary>
        public int BankId { get; set; }

        public string BankName { get; set; }

        /// <summary>
        /// 开户省份
        /// </summary>
        public int ProvinceId { get; set; }

        public string Province { get; set; }

        /// <summary>
        /// 开户城市
        /// </summary>
        public int CityId { get; set; }

        public string City { get; set; }

        /// <summary>
        /// 开户支行
        /// </summary>
        public string Branch { get; set; }


        /// <summary>
        /// 银行帐号,工商银行就填绑定的Email
        /// </summary>
        public string BankNo { get; set; }

        /// <summary>
        /// 银行帐号简写
        /// </summary>
        public string BankNoShort { get; set; }

        /// <summary>
        /// 银行开户人
        /// </summary>
        public string BankOwner { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime OccDate { get; set; }

        public string IsDeleteDesc { get; set; }

        public int TotalCount { get; set; }
    }
}
