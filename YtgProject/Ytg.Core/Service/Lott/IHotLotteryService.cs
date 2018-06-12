using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;

namespace Ytg.Core.Service.Lott
{
    public interface IHotLotteryService : ICrudService<HotLottery>
    {
        /// <summary>
        /// 获取热门彩种
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<HotLottery> GetHotLottery();
    }
}
