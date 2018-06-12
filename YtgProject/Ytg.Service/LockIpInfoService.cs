using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    public class LockIpInfoService : CrudService<LockIpInfo>, ILockIpInfoService
    {
        public LockIpInfoService(IRepo<LockIpInfo> repo)
            : base(repo)
        {
        }

        /// <summary>
        /// 验证当前IP是否锁定
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool IsLockIp(string ip)
        {
            return  this.GetAll().Where(x=>x.Ip==ip).Count()>0;
        }
    }
}
