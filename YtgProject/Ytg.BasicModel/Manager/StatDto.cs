using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.Manager
{
    /// <summary>
    /// 后台相关统计报表实体
    /// </summary>
    public class StatDto
    {
        /// <summary>
        /// 显示日期或月份
        /// </summary>
        public string OccFilter { get; set; }


        private decimal? mtradeAmt=0;
        /// <summary>
        /// 统计金额
        /// </summary>
        public decimal? tradeAmt
        {
            get { return mtradeAmt; }
            set
            {
                if (value == null)
                    mtradeAmt = 0;
                else
                    mtradeAmt = Math.Abs(value.Value);
            }
        }
    }
}
