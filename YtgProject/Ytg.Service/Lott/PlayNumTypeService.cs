using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Repository;
using Ytg.Core.Service.Lott;

namespace Ytg.Service.Lott
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PlayNumTypeService : CrudService<PlayTypeNum>, IPlayNumTypeService
    {
        public PlayNumTypeService(IRepo<PlayTypeNum> repo)
            : base(repo)
        {

        }

        public IEnumerable<PlayTypeNum> GetAllPlayTypeNums()
        {
            return this.GetNoTracking();
        }
    }
}
