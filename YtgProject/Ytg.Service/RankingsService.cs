using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    public class RankingsService: CrudService<Rankings>, IRankingsService
    {
        public RankingsService(IRepo<Rankings> repo)
            : base(repo)
        {
        }

    }
}
