using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 是否禁用
    /// </summary>
    public interface IDelEntity
    {
        /// <summary>
        /// 是否禁用
        /// </summary>
        bool IsDelete { get; set; }
    }
}
