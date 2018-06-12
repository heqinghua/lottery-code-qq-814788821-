using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    /// <summary>
    /// 投注条件，尽用于投注的时候判断投注金额到达两天充值金额的5%
    /// </summary>
    public class MentionConditionsDTO
    {
        public decimal Touzhu { get; set; }

        public decimal Chongzhi { get; set; }
    }
}
