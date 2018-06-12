using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{

    /// <summary>
    /// 银行表
    /// </summary>
    public class SysBankType : DelEntity
    {
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 银行字母缩写
        /// </summary>
        public string BankDesc { get; set; }

        /// <summary>
        /// 银行官网URL
        /// </summary>
        public string BankWebUrl { get; set; }

        /// <summary>
        /// 银行Logo图片
        /// </summary>
        public string BankLogo { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public int OpUser { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OpTime { get; set; }


        /// <summary>
        /// 是否显示支行
        /// </summary>
        public bool IsShowZhiHang { get; set; }

        /// <summary>
        /// 是否开启自动充值
        /// </summary>
        public bool OpenAutoRecharge { get; set; }

        /// <summary>
        /// 是否支持跨行充值
        /// </summary>
        public bool IsInterBank { get; set; }
    }
}
