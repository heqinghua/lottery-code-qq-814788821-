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
    /// 佣金记录
    /// </summary>
    public class CommissionsService : CrudService<Commission>,ICommissionsService
    {
        public CommissionsService(IRepo<Commission> repo)
            : base(repo)
        {

        }



        public bool HasReceive(int uid, int OccDay)
        {
            return this.Where(c => c.UserId == uid && c.OccDaty == OccDay).Count() > 0;
        }
    }
}
