using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 数据库备份记录
    /// </summary>
    public class DataBaseBak : BaseEntity
    {
        /// <summary>
        /// 备份文件名称
        /// </summary>
        public string FileName { get; set; }


        /// <summary>
        /// 操作者
        /// </summary>
        public string OpenUser { get; set; }

    }

}
