using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    /// <summary>
    /// html版页面投注参数对象
    /// </summary>
    public class HtmlParamDto
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 玩法code
        /// </summary>
        public int methodid { get; set; }

        /// <summary>
        /// 投注内容
        /// </summary>
        public string codes { get; set; }

        /// <summary>
        /// 总注数
        /// </summary>
        public int nums { get; set; }


        /// <summary>
        /// 模式
        /// </summary>
        public int omodel { get; set; }


        public int times { get; set; }


        /// <summary>
        /// 总价格
        /// </summary>
        public decimal money { get; set; }


        public int mode { get; set; }

        /// <summary>
        /// 位置 ，万，千，百，十，个
        /// </summary>
        public string poschoose{get;set;}


    }
}
