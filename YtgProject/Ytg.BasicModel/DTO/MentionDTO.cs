using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class MentionDTO
    {
        //m.Id, m.MentionAmt,m.QueuNumber,m.IsEnable,m.OccDate,
        //	u.Code,ub.BankNo,ub.BankOwner,b.BankName,b.BankWebUrl

        public int Id { get; set; }

        public decimal MentionAmt { get; set; }

        public int QueuNumber { get; set; }

        public string IsEnableDesc { get; set; }

        public DateTime OccDate { get; set; }

        public string Code { get; set; }

        public string BankNo { get; set; }

        public string BankNoShort { get; set; }

        public string BankOwner { get; set; }

        public string BankName { get; set; }

        public string BankWebUrl { get; set; }

        public int TotalCount { get; set; }

        public int Audit { get; set; }

        /// <summary>
        /// 申请发起时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal Poundage { get; set; }

        /// <summary>
        /// 实际到账
        /// </summary>
        public decimal RealAmt { get; set; }

        /// <summary>
        /// 提现单号，需要将该值添入到余额明细表里面
        /// </summary>
        public string MentionCode { get; set; }

        public string ProvinceName { get; set; }

        public string CityName { get; set; }

        public string Branch { get; set; }

        /// <summary>
        /// 1为测试账号
        /// </summary>
        public string Sex { get; set; }

    }
}
