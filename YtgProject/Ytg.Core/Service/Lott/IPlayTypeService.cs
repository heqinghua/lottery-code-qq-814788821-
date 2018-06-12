using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic.DTO;

namespace Ytg.Core.Service.Lott
{
    [ServiceContract]
    public interface IPlayTypeService : ICrudService<PlayType>
    {
        /// <summary>
        /// 获取彩种所有玩法类型
        /// </summary>
        /// <param name="lotid"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<PlayType> GetAllPalyType(int lotid);

        /// <summary>
        /// 获得所有彩票的所有玩法类型
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<PlayType> GetAllPalyTypes();

        [OperationContract]
        bool UpdatePlayTypeState(int id, bool isenale);


    }
}
