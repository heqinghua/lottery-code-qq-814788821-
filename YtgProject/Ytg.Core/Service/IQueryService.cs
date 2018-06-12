using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 查询接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
   
    public interface IQueryService<T> where T: BaseEntity, new()
    {
    
        IEnumerable<T> GetAll();


        List<T> GetSqlSource(string sql);

        List<E> GetSqlSource<E>(string sql);

        DataTable GetSqlTableSource(string sql);

        List<T> GetEntitysPage(string sql, string fileds, string SortName, ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount);

        List<E> GetEntitysPage<E>(string sql, string fileds, string SortName, ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount);

        List<T> GetEntitysPage(string sql,string key, string fileds, string SortName, ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount);

        List<E> GetEntitysPage<E>(string sql, string key, string fileds, string SortName, ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount);

        /// <summary>
        /// 执行存储过程 
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="dbParameters"></param>
        /// <returns></returns>
        List<T> ExProc(string procName, params DbParameter[] dbParameters);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="procName"></param>
        /// <param name="dbParameters"></param>
        /// <returns></returns>
        List<E> ExProc<E>(string procName, params DbParameter[] dbParameters);

        /// <summary>
        /// Ef非关联集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetNoTracking();

    }
}
