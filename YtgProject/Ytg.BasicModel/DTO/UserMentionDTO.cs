using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    /// <summary>
    /// 用户发起提现
    /// </summary>
    public class UserMentionDTO
    {
        public int Id { get; set; }

        public DateTime OccDate { get; set; }

        public string BankNo { get; set; }

        public string BankName { get; set; }

        public int MentionCount { get; set; }

        public decimal UserAmt { get; set; }

        public decimal? MinAmt { get; set; }

        public decimal? MaxAmt { get; set; }

        public decimal? VipMaxAmt { get; set; }

        public decimal? VipMinAmt { get; set; }

        public bool IsOpenVip { get; set; }
    }
}
