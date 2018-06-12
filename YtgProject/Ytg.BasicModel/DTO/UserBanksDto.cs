using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class UserBanksDto
    {

        /// <summary>
        /// 对应银行id
        /// </summary>
        public int BankId { get; set; }


        /// <summary>
        /// 开户省份
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 开户城市
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 开户支行
        /// </summary>
        public string Branch { get; set; }


        /// <summary>
        /// 银行帐号,工商银行就填绑定的Email
        /// </summary>
        public string BankNo { get; set; }

        /// <summary>
        /// 银行开户人
        /// </summary>
        public string BankOwner { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }


        public int Id { get; set; }

        public DateTime OccDate { get; set; }

        //sy.Code,sy.UserLockCount,sy.IsLockCards,sbt.BankName,sp.ProvinceName,sc.CityName

        public string Code { get; set; }

        public int UserLockCount { get; set; }

        public bool  IsLockCards { get; set; }

        public string BankName { get; set; }

        public string ProvinceName { get; set; }

        public string CityName { get; set; }

    }
}
