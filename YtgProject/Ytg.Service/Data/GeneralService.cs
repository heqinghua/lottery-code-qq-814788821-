using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.DataBasic;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service.Data;

namespace Ytg.Service.Data
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GeneralService : CrudService<GeneralEntity>, IGeneralService
    {
        public GeneralService(IRepo<GeneralEntity> repo)
            : base(repo)
        {

        }
        /// <summary>
        /// 获取活动统计数据
        /// </summary>
        public IEnumerable<GeneralVM> GetActivitiesList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取投注统计数据
        /// </summary>
        public IEnumerable<GeneralVM> GetBettingList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取分红统计数据
        /// </summary>
        public IEnumerable<GeneralVM> GetBonusList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取提现统计数据
        /// </summary>
        public IEnumerable<GeneralVM> GetMentionList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount)
        {
            int isDay = 0;
            StringBuilder strSql = new StringBuilder();
            if (isDay > 0)//按日统计
            {
                strSql.Append("(select sum(Poundage) as SumPondage,CONVERT(varchar(100), OpTime, 23) as OpTime from sysytgbanktransfer where IsRecharge='True' group by OpTime ");
            }
            else//按月统计
                strSql.Append("(select sum(Poundage),substring(CONVERT(varchar(100), OpTime, 23),0,8) as OpTime from sysytgbanktransfer where IsRecharge='True' group by OpTime ");
            strSql.Append(") as t1");
            int pageCount = 0;
            var source = this.GetEntitysPage<GeneralVM>(strSql.ToString(), "Id", "*", "Id", ESortType.ASC, pageIndex, AppGlobal.ManagerDataPageSize, ref pageCount, ref totalCount);
            return source;
        }
        /// <summary>
        /// 获取中奖统计数据
        /// </summary>
        public IEnumerable<GeneralVM> GetPrizeList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取返点统计数据
        /// </summary>
        public IEnumerable<GeneralVM> GetRebateList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取充值统计数据
        /// </summary>
        public IEnumerable<GeneralVM> GetRechargeList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount)
        {
            int isDay = 0;
            StringBuilder strSql = new StringBuilder();
            if (isDay > 0)//按日统计
            {
                strSql.Append("(select sum(Poundage) as SumPondage,CONVERT(varchar(100), OpTime, 23) as OpTime from sysytgbanktransfer where IsRecharge='True' group by OpTime ");
            }
            else//按月统计
                strSql.Append("(select sum(Poundage),substring(CONVERT(varchar(100), OpTime, 23),0,8) as OpTime from sysytgbanktransfer where IsRecharge='True' group by OpTime ");
            strSql.Append(") as t1");
            int pageCount = 0;
            var source = this.GetEntitysPage<GeneralVM>(strSql.ToString(), "Id", "*", "Id", ESortType.ASC, pageIndex, AppGlobal.ManagerDataPageSize, ref pageCount, ref totalCount);
            return source;
      
        
        }
        /// <summary>
        /// 获取盈亏统计数据
        /// </summary>
        public IEnumerable<ProfitVM> GetProfitList(DateTime BeginTime, DateTime EndTime, bool? isDel, int pageIndex, ref int totalCount)
        {
            throw new NotImplementedException();
        }
    }
}
