using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service.Lott
{
    /// <summary>
    /// 彩票周期
    /// </summary>
    [ServiceContract]
    public interface ILotteryCycleService : ICrudService<LotteryCycle>
    {
        [OperationContract]
        IEnumerable<LotteryCycle> GetLotteryCycles();

        [OperationContract]
        bool CreateLotteryCycle(LotteryCycle model);

        [OperationContract]
        IEnumerable<LotteryCycleVM> GetLotteryCycleVms(int? lotteryId, int pageSize, int pageIndex, ref int pageCount, ref int iRecordCount);

    }
}
