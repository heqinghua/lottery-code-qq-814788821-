using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    /// <summary>
    /// 查询服务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryService<T> : IQueryService<T> where T : BaseEntity, new()
    {
        public IRepo<T> mRepo;

        public QueryService(IRepo<T> repo)
        {
            this.mRepo = repo;
        }

        public IEnumerable<T> GetAll()
        {
            return this.mRepo.GetAll();
            
        }


        public List<T> GetSqlSource(string sql)
        {
            return this.mRepo.GetSqlSource(sql);
        }

        public List<E> GetSqlSource<E>(string sql)
        {
            return this.mRepo.GetSqlSource<E>(sql);
        }

        public System.Data.DataTable GetSqlTableSource(string sql)
        {
            return this.mRepo.GetSqlTableSource(sql);
        }

        public List<T> GetEntitysPage(string sql,string key, string fileds, string SortName, Comm.ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount)
        {
            return this.mRepo.GetEntitysPage(sql,key, fileds, SortName, SortType, iPageIndex, iPageSize, ref iPageCount, ref iRecordCount);
        }

        public List<E> GetEntitysPage<E>(string sql, string key, string fileds, string SortName, Comm.ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount)
        {
            return this.mRepo.GetEntitysPage<E>(sql, key, fileds, SortName, SortType, iPageIndex, iPageSize, ref iPageCount, ref iRecordCount);
        }

        public List<T> GetEntitysPage(string sql, string fileds, string SortName, Comm.ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount)
        {
            return this.GetEntitysPage(sql, "Id", SortName, SortType, iPageIndex, iPageSize, ref iPageCount, ref iRecordCount);
        }

        public List<E> GetEntitysPage<E>(string sql, string fileds, string SortName, Comm.ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount)
        {
            return this.GetEntitysPage<E>(sql, "Id", SortName, SortType, iPageIndex, iPageSize, ref iPageCount, ref iRecordCount);
        }


        public List<T> ExProc(string procName, params System.Data.Common.DbParameter[] dbParameters)
        {
            return this.mRepo.ExProc(procName,dbParameters);
        }

        public List<E> ExProc<E>(string procName, params System.Data.Common.DbParameter[] dbParameters)
        {
            return this.mRepo.ExProc<E>(procName, dbParameters);
        }

        public int ExProcNoReader(string procName, params System.Data.Common.DbParameter[] dbParameters)
        {
            return this.mRepo.ExProcNoReader(procName, dbParameters);
        }






        public IEnumerable<T> GetNoTracking()
        {
           return this.mRepo.GetNoTracking();
        }


       
    }
}
