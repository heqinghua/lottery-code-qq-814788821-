using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Core
{
    /// <summary>
    /// 异常类
    /// </summary>
    public class YtgException:Exception
    {
        public YtgException(string message)
            : base(message)
        {
        }

    }
}
