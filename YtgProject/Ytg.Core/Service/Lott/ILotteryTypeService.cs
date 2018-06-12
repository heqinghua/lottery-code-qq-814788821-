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
    /// 彩票类型
    /// </summary>
    [ServiceContract]
    public interface ILotteryTypeService : ICrudService<LotteryType>
    {

        [OperationContract]
        IEnumerable<LotteryType> GetLotteryTypes();

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool UpdateLotteryTypeState(int id, bool isEnable);

        [OperationContract]
        bool UpdateLotteryType(LotteryType item);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="lotteryId"></param>
        /// <param name="sortId"></param>
        /// <returns></returns>
        [OperationContract]
        bool Sort(int lotteryId, int sort);

        LotteryType GetLottery(int lotteryId);

         /// <summary>
        /// 获取启用的彩种
        /// </summary>
        /// <returns></returns>
        List<LotteryType> GetEnableLotterys();
    }
}
