using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;

namespace Ytg.Core.Repository
{
    /// <summary>
    /// 基础仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepo<T> where T:BaseEntity
    {
        DbContext GetDbContext { get; }

        T Get(int id);

        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false);
        
        T Insert(T o);
        void Save();
        void Delete(T o);
        void Restore(T o);

        List<T> GetSqlSource(string sql);

        List<E> GetSqlSource<E>(string sql);

        DataTable GetSqlTableSource(string sql);

        List<T> GetEntitysPage(string sql, string fileds, string SortName, ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount);

        List<E> GetEntitysPage<E>(string sql, string fileds, string SortName, ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount);

        List<T> GetEntitysPage(string sql,string key, string fileds, string SortName, ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount);

        List<E> GetEntitysPage<E>(string sql,string key, string fileds, string SortName, ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount);


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
        /// ef 关联
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetNoTracking();

        /// <summary>
        /// 执行存储过程，无返回参数
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="dbParameters"></param>
        int ExProcNoReader(string procName, params DbParameter[] dbParameters);
        
    }
}
