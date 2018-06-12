using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Comm;
using System.ServiceModel;
using Ytg.BasicModel.DataBasic;

namespace Ytg.Core.Service.Data
{
    /// <summary>
    /// 统计通用服务
    /// </summary>
    [ServiceContract]
    public interface IGeneralService : ICrudService<GeneralEntity>
    {
        /// <summary>
        /// 获取活动统计数据
        /// </summary>
        [OperationContract]
        IEnumerable<GeneralVM> GetActivitiesList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount);

        /// <summary>
        /// 获取投注统计数据
        /// </summary>
        [OperationContract]
        IEnumerable<GeneralVM> GetBettingList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount);

        /// <summary>
        /// 获取分红统计数据
        /// </summary>
        [OperationContract]
        IEnumerable<GeneralVM> GetBonusList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount);

        /// <summary>
        /// 获取提现统计数据
        /// </summary>
        [OperationContract]
        IEnumerable<GeneralVM> GetMentionList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount);

        /// <summary>
        /// 获取中奖统计数据
        /// </summary>
        [OperationContract]
        IEnumerable<GeneralVM> GetPrizeList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount);

        /// <summary>
        /// 获取返点统计数据
        /// </summary>
        [OperationContract]
        IEnumerable<GeneralVM> GetRebateList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount);

        /// <summary>
        /// 获取充值统计数据
        /// </summary>
        [OperationContract]
        IEnumerable<GeneralVM> GetRechargeList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount);

        /// <summary>
        /// 获取盈亏统计数据
        /// </summary>
        [OperationContract]
        IEnumerable<ProfitVM> GetProfitList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount);
    }
}
