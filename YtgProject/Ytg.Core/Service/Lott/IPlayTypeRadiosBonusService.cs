using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.LotteryBasic;

namespace Ytg.Core.Service.Lott
{
    /// <summary>
    /// 玩法单选奖金列表
    /// </summary>
    [ServiceContract]
    public interface IPlayTypeRadiosBonusService : ICrudService<PlayTypeRadiosBonus>
    {

        /// <summary>
        /// 根据单选获取奖金列表
        /// </summary>
        /// <param name="radioCode"></param>
        /// <returns></returns>
        [OperationContract]
        List<PlayTypeRadiosBonus> GetPlayTypeRadiosBonus(int radioCode);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateRadioBonus(PlayTypeRadiosBonus item);
    }
}
