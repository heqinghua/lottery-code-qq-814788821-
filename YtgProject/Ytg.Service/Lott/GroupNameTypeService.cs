using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Core.Repository;
using Ytg.Core.Service.Lott;

namespace Ytg.Service.Lott
{
    public class GroupNameTypeService : CrudService<Ytg.BasicModel.LotteryBasic.GroupNameType>, IGroupNameTypeService
    {
        public GroupNameTypeService(IRepo<GroupNameType> repo)
            : base(repo)
        {
        }
    }
}
