using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class RadioHelper
    {

        public RadioHelper()
        {
            this.Childrens =null;
        }

        /// <summary>
        /// 玩法id
        /// </summary>
        public int[] Key { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string methoddesc { get; set; }

        /// <summary>
        /// 投注帮助信息
        /// </summary>
        public string methodhelp { get; set; }

        /// <summary>
        /// 投注是列子
        /// </summary>
        public string methodexample { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 显示格式
        /// </summary>
        public string show_str { get; set; }

        /// <summary>
        /// 拆分字符
        /// </summary>
        public string code_sp { get; set; }

        /// <summary>
        /// 布局
        /// </summary>
        public List<string> Layouts { get; set; }

        public string methodid { get; set; }

        /// <summary>
        /// 是否为按钮
        /// </summary>
        public string isButton { get; set; }

        /// <summary>
        /// 是否显示任选位置
        /// </summary>
        public string selPosition { get; set; }

        /// <summary>
        /// 任选默认显示选择位置
        /// </summary>
        public string defaultposition { get; set; }

        /// <summary>
        /// 子项显示名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 子项显示描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 子项
        /// </summary>
        public List<RadioHelper> Childrens { get; set; }
    }
}
