using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;

namespace Ytg.Service.Lott
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PlayTypeService : CrudService<PlayType>, IPlayTypeService
    {


        public PlayTypeService(IRepo<PlayType> repo)
            : base(repo)
        {
        
        }

        public IEnumerable<PlayType> GetAllPalyType(int lotid)
        {
            return this.Where(item => item.LotteryCode == lotid.ToString()).OrderBy(c=>c.OccDate);

        }


        public bool UpdatePlayTypeState(int id, bool isenale)
        {
            var item = this.Get(id);
            if (null == item)
                return false;
            item.IsEnable = isenale;
            return true;
        }

        public IEnumerable<PlayType> GetAllPalyTypes()
        {
            return this.GetAll().OrderBy(c => c.OccDate);
        }
    }
}
