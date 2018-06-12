using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 系统字典
    /// </summary>
    public class SysDictionary : SerialNoEntity
    {


        /// <summary>
        /// 分组
        /// </summary>
        [MaxLength(50)]
        public string Group { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        [MaxLength(100)]
        public string DicName { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        [MaxLength(1000)]
        public string dicValue { get; set; }

    }
}
