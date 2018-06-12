using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.Manager;
using Ytg.BasicModel.Report;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 报表汇总
    /// </summary>
    [ServiceContract]
    public interface ICountDataService : ICrudService<CountData>
    {
        [OperationContract]
        CountData GetCountData(string beginDate, string endDate);

        /// <summary>
        /// 按日统计报表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        List<Sp_DayJieSuanTable> FilterSp_DayJieSuanTable(DateTime beginDate, DateTime endDate);

        /// <summary>
        /// 按日统计报表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        List<Sp_YueJieSuanTable> FilterSp_MonthJieSuanTable(DateTime beginDate, DateTime endDate);

        /// <summary>
        /// 统计报表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        List<StatDto> FilterSp_userBannerTypeSuanTable(DateTime beginDate, DateTime endDate, string tradeType, int tableType);

        /// <summary>
        /// 可清理数据统计
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        List<ClearDateDTO> GetClearDataStat();

        /// <summary>
        /// 清理数据
        /// </summary>
        /// <param name="type">清理类型</param>
        /// <param name="minMonery">最低金额，清理用户数据</param>
        /// <param name="recDay">保留天数</param>
        void ClearData(int type, decimal minMonery, int recDay);
    }
}
