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
    /// <summary>
    /// 热门彩种
    /// </summary>
    public class HotLotteryService : CrudService<HotLottery>, IHotLotteryService
    {
        public HotLotteryService(IRepo<HotLottery> repo)
            : base(repo)
        {

        }

        public IList<HotLottery> GetHotLottery()
        {
            return this.ExProc<HotLottery>("sp_GetLotteryStat", null);
        }
    }
}
