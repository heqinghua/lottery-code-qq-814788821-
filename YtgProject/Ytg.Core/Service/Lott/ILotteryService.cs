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
    /// <summary>
    /// 彩票快wcf服务
    /// </summary>

    [ServiceContract]
    public interface ILotteryService : ICrudService<Lottery>
    {
        /// <summary>
        /// 获取开奖数据
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <param name="issueCode"></param>
        /// <param name="date"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<LotteryResultDTO> GetOpenLotteryResult(string lotteryCode, string issueCode, DateTime date, int pageIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 方法说明: 获取未开奖数据
        /// 创建时间：2015-06-07
        /// 创建者：GP
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <param name="date"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<LotteryResultDTO> GetNoLotteryResult(string lotteryCode, DateTime date, int pageIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 手动开奖
        /// </summary>
        /// <param name="issueCode">期号</param>
        /// <param name="openResult">开奖结果</param>
        /// <returns></returns>
        [OperationContract]
        void ManualOpen(string lotteryCode, string issueCode, string openResult);
    }
}
