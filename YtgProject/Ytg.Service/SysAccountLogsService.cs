using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    public class SysAccountLogsService : CrudService<SysAccountLog>, ISysAccountLogsService
    {
        
        public SysAccountLogsService(IRepo<SysAccountLog> repo)
            : base(repo)
        {

        }

        public List<SysAccountLog> GetLoginLogs(string sDate, string eDate)
        {
            var sql = string.Format(@"select * from SysAccountLogs where OccDate BETWEEN '{0}' AND '{1}", sDate, eDate);

            var list = this.GetSqlSource<SysAccountLog>(sql);
            return list;
        }

        public List<SysAccountLogsMV> GetLoginLogs(string sDate, string eDate, string code, int pageIndex, ref int totalCount)
        {
            string whereSql = "";
            if (!string.IsNullOrEmpty(sDate) && !string.IsNullOrEmpty(eDate))
                whereSql += string.Format("  and l.OccDate BETWEEN '{0}' AND '{1}' ", sDate, eDate);

//            var sql = string.Format(@"WITH result AS(SELECT l.Id,u.Code,u.Name,l.Ip,l.ServerSystem,l.UseSource,l.Descript,l.OccDate
//            FROM dbo.SysAccountLogs l
//            LEFT JOIN dbo.SysAccounts u ON u.id=l.userId
//            WHERE ('{1}'='' or u.Code like '%{1}%') {0}
//            ),totalCount AS(
//	            SELECT COUNT(1) TotalCount FROM result
//            )
//            select * from (
//	            select *, ROW_NUMBER() OVER(Order by a.OccDate desc) AS RowNumber from result as a,totalCount
//            ) as b
//            where RowNumber BETWEEN ({2}-1)*{3}+1 AND {2}*{3}", whereSql, code, pageIndex, AppGlobal.ManagerDataPageSize);

            string sql =string.Format( @"SELECT l.Id,u.Code,u.Name,l.Ip,l.ServerSystem,l.UseSource,l.Descript,l.OccDate
            FROM dbo.SysAccountLogs l
            LEFT JOIN dbo.SysAccounts u ON u.id=l.userId
            WHERE ('{1}'='' or u.Code like '%{1}%') {0}",whereSql,code);

            //var list = this.GetSqlSource<SysAccountLogsMV>(sql);
            //totalCount = list.Count > 0 ? list.First().TotalCount : 0;
            //return list;
            sql = "(" + sql + ") as t1";
            int pageCount=0;
            return GetEntitysPage<SysAccountLogsMV>(sql, "OccDate", "*", "OccDate",ESortType.DESC, pageIndex,AppGlobal.ManagerDataPageSize,ref pageCount, ref totalCount);
        }
    }
}
