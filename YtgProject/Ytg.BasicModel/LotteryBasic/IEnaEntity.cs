using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public interface IEnaEntity
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        bool IsEnable { get; set; }
    }
}
