using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.LotteryBasic;

namespace Ytg.Core.Service.Lott
{
    /// <summary>
    /// 参与用户服务
    /// </summary>
    public interface IBuyTogetherService : ICrudService<BuyTogether>
    {

        /// <summary>
        /// 根据投注id获取合买内容
        /// </summary>
        /// <param name="bettid"></param>
        /// <returns></returns>
        List<BuyTogether> GetForBettid(int bettid);

        /// <summary>
        /// 添加获修改内容
        /// </summary>
        /// <param name="SerialNo"></param>
        /// <param name="TradeType"></param>
        /// <param name="together"></param>
        /// <returns></returns>
        int AddTogether(string SerialNo, int TradeType,BuyTogether together);

        /// <summary>
        /// 修改投注相关的合买数据
        /// </summary>
        /// <param name="bettid"></param>
        /// <param name="state"></param>
        int UpdateBuyTogetherState(int bettid, int state);

        List<BuyTogether> GetForBettidLst(int bettid);
    }
}
