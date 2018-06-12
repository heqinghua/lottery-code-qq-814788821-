using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.Act;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    /// <summary>
    /// 签到活动
    /// </summary>
    public class SignService : CrudService<Sign>, ISignService
    {
        public SignService(IRepo<Sign> repo)
            : base(repo)
        {

        }
    }
}
