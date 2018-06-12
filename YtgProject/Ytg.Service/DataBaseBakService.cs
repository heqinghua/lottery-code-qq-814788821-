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
    /// 数据库备份操作类
    /// </summary>
    public class DataBaseBakService : CrudService<DataBaseBak>, IDataBaseBakService
    {
        public DataBaseBakService(IRepo<DataBaseBak> repo)
            : base(repo)
        {

        }
    }
}
