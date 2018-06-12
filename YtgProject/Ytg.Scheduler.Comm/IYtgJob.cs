using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm
{
    /// <summary>
    /// 任务初始化对象
    /// </summary>
    public interface IYtgJob
    {
        IDictionary<IJobDetail, IList<ITrigger>> Initital();
    }
}
