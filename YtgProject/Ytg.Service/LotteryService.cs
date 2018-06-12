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
    public class LotteryService : CrudService<Lottery>, ILotteryService
    {
        public LotteryService(IRepo<Lottery> repo)
            : base(repo)
        {

        }
    }
}
