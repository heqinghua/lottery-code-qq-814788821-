using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 活动
    /// </summary>
    [ServiceContract]
    public interface IMarketService : ICrudService<Market>
    {
        /// <summary>
        /// 活动列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Market> GetMarkets();

        /// <summary>
        /// 获取活动菜单
        /// </summary>
        /// <returns></returns>
        List<Market> GetMarketByMeun();

         /// <summary>
        /// 检查当前用户,当天是否已经参与过活动
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        bool MarketIsExist(int uid, int tradeType);

        /// <summary>
        /// 检查当前活动是否关闭
        /// </summary>
        /// <returns></returns>
        bool MarketIsClose(string marketType);

        /// <summary>
        /// 保存活动
        /// </summary>
        /// <param name="banner"></param>
        /// <returns></returns>
        [OperationContract]
        bool SaveMarket(ref Market mMarket);

        /// <summary>
        /// 获取注册
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Market GetZcMarket();

        /// <summary>
        /// 获取满赠
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Market GetMzMarket();

        /// <summary>
        /// 获取奇兵夺宝活动
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Market GetQbdbMarket();

        /// <summary>
        /// 获取充值返现活动
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Market GetCzfxMarket();
    }
}
