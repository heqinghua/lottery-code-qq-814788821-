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
    /// <summary>
    /// 系统消息
    /// </summary>
    public class SysMessagesService : CrudService<SysMessages>, ISysMessagesService
    {
        public SysMessagesService(IRepo<SysMessages> repo)
            : base(repo)
        {
        }
    }
}
