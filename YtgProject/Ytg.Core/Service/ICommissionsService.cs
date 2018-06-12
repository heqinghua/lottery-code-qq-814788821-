using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 佣金记录
    /// </summary>
    public interface ICommissionsService : ICrudService<Commission>
    {
        /// <summary>
        /// 当日是否领取
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        bool HasReceive(int uid, int OccDay);
    }
}
