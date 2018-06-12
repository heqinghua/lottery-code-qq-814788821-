using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic
{
    /// <summary>
    /// 彩票分类表
    /// </summary>
    public class GroupNameType : EnaEntity
    {

        /// <summary>
        /// 显示标题大类
        /// </summary>
        public string ShowTitle { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int OrderNo { get; set; }

    }
}
