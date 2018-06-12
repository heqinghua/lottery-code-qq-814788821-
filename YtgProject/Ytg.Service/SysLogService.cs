using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    /// <summary>
    /// 日志
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class SysLogService : CrudService<SysLog>, ISysLogService
    {
        public SysLogService(IRepo<SysLog> repo)
            : base(repo)
        {

        }

        /// <summary>
        /// 后台Wcf查看访问统计数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<SysUserLoginStatisticsVM> SelectLoginLogStatistics(int dateType, string sDate, string eDate)
        {
            var len = 10;
            switch (dateType)
            {
                case 1:
                    len = 10;
                    break;
                case 2:
                    len = 7;
                    break;
                case 3:
                    len = 4;
                    break;
                default:
                    break;
            }
//            var sql = string.Format(@"WITH result AS(
//	            SELECT CONVERT(VARCHAR({0}),t.OccDate,120) OccDate,SUM(Member) MemberCount,SUM(Proxy) ProxyCount,SUM(Zongdai) BasicProyCount,COUNT(1) SumCount
//                FROM (SELECT l.OccDate,
//                        CASE WHEN u.UserType=0 THEN 1 ELSE 0 END Member,
//		                CASE WHEN u.UserType=1 THEN 1 ELSE 0 END Proxy,
//		                CASE WHEN u.UserType=3 THEN 1 ELSE 0 END Zongdai,
//		                UserType 
//                        FROM dbo.SysLogs l
//	                    LEFT JOIN dbo.SysYtgUser u ON u.Code=l.ReferenceCode
//	                    WHERE l.Type=0 AND u.UserType!=2
//                        AND l.OccDate BETWEEN '{3}' AND '{4}'
//                ) t GROUP BY CONVERT(VARCHAR({0}),t.OccDate,120)
//                ),totalCount AS(SELECT COUNT(1) TotalCount FROM result)
//                select * from (select *, ROW_NUMBER() OVER(Order by a.OccDate desc) AS RowNumber from result as a,totalCount) as b
//                where RowNumber BETWEEN ({1}-1)*{2}+1 AND {1}*{2}", len, 1, 999, sDate, eDate);

//            var list = this.GetSqlSource<SysUserLoginStatisticsVM>(sql);
//            return list;
            
            string sql =string.Format( @"SELECT CONVERT(VARCHAR({0}),t.OccDate,120) OccDate,SUM(Member) MemberCount,SUM(Proxy) ProxyCount,SUM(Zongdai) BasicProyCount,COUNT(1) SumCount
                FROM (SELECT l.OccDate,
                        CASE WHEN u.UserType=0 THEN 1 ELSE 0 END Member,
		                CASE WHEN u.UserType=1 THEN 1 ELSE 0 END Proxy,
		                CASE WHEN u.UserType=3 THEN 1 ELSE 0 END Zongdai,
		                UserType 
                        FROM dbo.SysLogs l
	                    LEFT JOIN dbo.SysYtgUser u ON u.Code=l.ReferenceCode
	                    WHERE l.Type=0 AND u.UserType not in(2,4)
                        AND l.OccDate BETWEEN '{1}' AND '{2}') t GROUP BY CONVERT(VARCHAR({0}),t.OccDate,120)", len, sDate, eDate);

            return this.GetSqlSource<SysUserLoginStatisticsVM>(sql);
        }

        /// <summary>
        /// 后台wcf登录日志管理
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="userType"></param>
        /// <param name="code"></param>        
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<SysLoginLogVM> SelectLoginLogs(string sDate, string eDate, int userType, string code, int pageIndex, ref int totalCount)
        {
            string whereSql = "";
            if (!string.IsNullOrEmpty(sDate) && !string.IsNullOrEmpty(eDate))
                whereSql += string.Format("  and l.OccDate BETWEEN '{0}' AND '{1}' ", sDate, eDate);

            //            var sql = string.Format(@"WITH result AS(SELECT l.Id,u.Code,u.UserType,l.Ip,l.UseSource,l.ServerSystem,l.Descript,l.OccDate
            //            FROM dbo.SysLogs l
            //            LEFT JOIN dbo.SysYtgUser u ON u.Code=l.ReferenceCode
            //            WHERE u.UserType!=2 {0} and ({1}=-1 or u.UserType={1})  and ('{2}'='' or u.Code like '%{2}%')
            //            ),totalCount AS(
            //	            SELECT COUNT(1) TotalCount FROM result
            //            )
            //            select * from (
            //	            select *, ROW_NUMBER() OVER(Order by a.OccDate desc) AS RowNumber from result as a,totalCount
            //            ) as b
            //            where RowNumber BETWEEN ({3}-1)*{4}+1 AND {3}*{4}", whereSql,userType, code, pageIndex, AppGlobal.ManagerDataPageSize);

            //            var list = this.GetSqlSource<SysLoginLogVM>(sql);
            //            totalCount = list.Count > 0 ? list.First().TotalCount : 0;
            //            return list;
            string sql =string.Format( @"SELECT l.Id,u.Code,u.UserType,l.Ip,l.UseSource,l.ServerSystem,l.Descript,l.OccDate FROM dbo.SysLogs l
            LEFT JOIN dbo.SysYtgUser u ON u.Code=l.ReferenceCode
            WHERE u.UserType!=2 {0} and ({1}=-1 or u.UserType={1})  and ('{2}'='' or u.Code like '{2}')",whereSql,userType,code);
            sql = "(" + sql + ") as t1";
            int pageCount=0;
            return this.GetEntitysPage<SysLoginLogVM>(sql, "OccDate", "*", "OccDate desc",ESortType.DESC,pageIndex,AppGlobal.ManagerDataPageSize,ref pageIndex,ref totalCount);
            
        }


        #region GP


        /// <summary>
        /// 查询登录日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">截止日期</param>
        /// <param name="userName">用户名称</param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public List<SysLog> GetSysLogs(int logType, DateTime beginDate, DateTime endDate, string userName,string userType)
        {
            return null;
        }


        #endregion

    }
}
