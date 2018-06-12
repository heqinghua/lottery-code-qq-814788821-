using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{

    /// <summary>
    /// 锁定IP列表
    /// </summary>
    public interface ILockIpInfoService : ICrudService<LockIpInfo>
    {
        /// <summary>
        /// 验证当前IP是否锁定
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        bool IsLockIp(string ip);
    }
}
