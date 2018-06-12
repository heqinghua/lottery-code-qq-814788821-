using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class FilterSysQuotaDTO
    {
        /// <summary>
        /// id
        /// </summary>
        public int? Id { get; set; }


        /// <summary>
        /// 隶属用户
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 返点
        /// </summary>
        public double? Rebate { get; set; }

        /// <summary>
        /// 配额名称
        /// </summary>
        public string QuotaType { get; set; }

        /// <summary>
        /// 配额最大数量
        /// </summary>
        public int? MaxNum { get; set; }


        public DateTime? OccDate { get; set; }
    }
}
