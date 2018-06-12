using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic.DTO
{
    /// <summary>
    /// 销售额升点配置文件实体类
    /// </summary>
    public class AddPointsAttribute
    {
        /// <summary>
        /// 标准天
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 标准值
        /// </summary>
        public string SalesStand { get; set; }

        /// <summary>
        /// 时时彩配额值
        /// </summary>
        public string SSC { get; set; }
        /// <summary>
        /// 11选5
        /// </summary>
        public string Eleven { get; set; }

        /// <summary>
        /// 3D/P3
        /// </summary>
        public string Td { get; set; }

        /// <summary>
        /// 返点级别
        /// </summary>
        public double targer { get; set; }
    }
}
