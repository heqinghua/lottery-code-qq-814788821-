using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Repository;
using Omu.ValueInjecter;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Common;
using Ytg.Comm;

namespace Ytg.Data
{
    public class Repo<T> : IRepo<T> where T : BaseEntity,new ()
    {
        protected readonly DbContext mDbContext;

        protected readonly IDbContextFactory mFactory;

        protected log4net.ILog log = null;
        
        public Repo(IDbContextFactory factory)
        {
            log = log4net.LogManager.GetLogger("errorLog");
            this.mFactory = factory;
            this.mDbContext = this.mFactory.GetDbContext();

        }

        /// <summary>
        /// 根据id获取单个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id)
        {
            return mDbContext.Set<T>().Find(id);
        }

        /// <summary>
        /// ef 关联
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetNoTracking()
        {
           
            return this.mDbContext.Set<T>().AsNoTracking(); 
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            if (typeof(IDelEntity).IsAssignableFrom(typeof(T)))
                return IoC.Resolve<IDelRepo<T>>() .GetAll();

            return this.mDbContext.Set<T>(); 
        }

        public IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate, bool showDeleted = false)
        {
            if (typeof(IDelEntity).IsAssignableFrom(typeof(T)))
                return IoC.Resolve<IDelRepo<T>>().Where(predicate, showDeleted);

            return mDbContext.Set<T>().Where(predicate);
        }


        public T Insert(T o)
        {
            var t = mDbContext.Set<T>().Create();
            t.InjectFrom(o);
            mDbContext.Set<T>().Add(o);
            return t;
        }

        public void Save()
        {
            mDbContext.SaveChanges();
        }

        public void Restore(T o)
        {
            if (o is DelEntity)
                IoC.Resolve<IDelRepo<T>>().Restore(o);
        }


        public void Delete(T o)
        {
            //if (o is DelEntity)
            //    (o as DelEntity).IsDelete = true;
            //else
                mDbContext.Set<T>().Remove(o);
        }


        #region  sql


        public List<T> GetSqlSource(string sql)
        {
            return GetSqlSource<T>(sql);
        }

        /// <summary>
        /// 根据sql查询语句获取所有数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<E> GetSqlSource<E>(string sql)
        {
            DbCommand dbCmd = this.mFactory.GetDbCommand();
            dbCmd.CommandType = CommandType.Text;
            List<E> query = null;
            DbDataReader SqlReader = null;
            try
            {
                dbCmd.CommandText = sql;
                dbCmd.Connection = this.mFactory.GetDbConnection();
                if (dbCmd.Connection.State == System.Data.ConnectionState.Closed)
                {
                    dbCmd.Connection.Open();
                }
                SqlReader = dbCmd.ExecuteReader();
                query = ((IObjectContextAdapter)this.mDbContext).ObjectContext.Translate<E>(SqlReader).ToList();
                if (SqlReader != null && !SqlReader.IsClosed)
                {
                    SqlReader.Close();
                    SqlReader.Dispose();
                }
                if (dbCmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    dbCmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error(" GetSqlSource<E>("+sql+")", ex);
                    throw ex;
            }
            finally
            {
                if (SqlReader != null && !SqlReader.IsClosed)
                {
                    SqlReader.Close();
                    SqlReader.Dispose();
                }
                if (dbCmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    dbCmd.Connection.Close();
                }
            }

            return query;
        }
        /// <summary>
        /// 根据sql查询语句获取所有数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetSqlTableSource(string sql)
        {
            DataTable dt;
            DbCommand dbCmd = this.mFactory.GetDbCommand();
            dbCmd.CommandType = CommandType.Text;
            DbDataReader dbReader = null;
            try
            {
                dbCmd.CommandText = sql;
                dbCmd.Connection = this.mFactory.GetDbConnection();
                if (dbCmd.Connection.State == System.Data.ConnectionState.Closed)
                {
                    dbCmd.Connection.Open();
                }
                DbDataAdapter da = this.mFactory.GetDbDataAdapter();
                da.SelectCommand = dbCmd;
                dt = new DataTable();
                da.Fill(dt);
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                    dbReader.Dispose();
                }
                if (dbCmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    dbCmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                log.Error(" GetSqlSource<E>(" + sql + ")", ex);
                throw ex;
            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                    dbReader.Dispose();
                }
                if (dbCmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    dbCmd.Connection.Close();
                }
            }
            return dt;
        }

        public List<T> GetEntitysPage(string sql, string key, string fileds, string SortName, ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount)
        {

            return this.GetEntitysPage<T>(sql, key, fileds, SortName, SortType, iPageIndex, iPageSize, ref iPageCount, ref iRecordCount);
        }


        /// <summary>
        /// 数据查询分页 存储过程
        /// </summary>
        /// <typeparam name="T">要查询的sql语句对应的实体</typeparam>
        /// <param name="TableName">要查询的sql语句</param>
        /// <param name="SortName">排序列的字段名称</param>
        /// <param name="SortType">排序方式升序还是逆序</param>
        /// <param name="iPageIndex">当前页码</param>
        /// <param name="iPageSize">每页数据行数</param>
        /// <param name="iPageCount">输入查询结果的分页的页数</param>
        /// <param name="iRecordCount">输入查询结果的总行数</param>
        /// <returns>返回查询List</returns>
        /// <Author>hqh</Author>
        public List<E> GetEntitysPage<E>(string sql, string key, string fileds, string SortName, ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount)
        {
            string spName = "sp_page";
            /*
              @TableName VARCHAR(200),     --表名
     @FieldList VARCHAR(2000),    --显示列名，如果是全部字段则为*
     @PrimaryKey VARCHAR(100),    --单一主键或唯一值键
     @Where VARCHAR(2000),        --查询条件 不含'where'字符，如id>10 and len(userid)>9
     @Order VARCHAR(1000),        --排序 不含'order by'字符，如id asc,userid desc，必须指定asc或desc                                 
                                  --注意当@SortType=3时生效，记住一定要在最后加上主键，否则会让你比较郁闷
     @SortType INT,               --排序规则 1:正序asc 2:倒序desc 3:多列排序方法
     @RecorderCount INT,          --记录总数 0:会返回总记录
     @PageSize INT,               --每页输出的记录数
     @PageIndex INT,              --当前页数
     @TotalCount INT OUTPUT,      --记返回总记录
     @TotalPageCount INT OUTPUT   --返回总页数
             */
            DbParameter[] pramers = new DbParameter[]
            {
                    this.mFactory.GetDbParameter("@TableName",DbType.String),//表名
                    this.mFactory.GetDbParameter("@FieldList",DbType.String),//显示列名，如果是全部字段则为*
                   this.mFactory.GetDbParameter("@Order",DbType.String),//排序 不含'order by'字符，如id asc,userid desc，必须指定asc或desc                                 
                    this.mFactory.GetDbParameter("@PageSize",DbType.Int32),//每页输出的记录数
                    this.mFactory.GetDbParameter("@PageIndex",DbType.Int32),//当前页数
                    this.mFactory.GetDbParameter("@SortType",DbType.Int32), //排序规则 1:正序asc 2:倒序desc 3:多列排序方法
                    this.mFactory.GetDbParameter("@Where",DbType.String),//查询条件 不含'where'字符，如id>10 and len(userid)>9
                    this.mFactory.GetDbParameter("@PrimaryKey",DbType.String),//单一主键或唯一值键
                    this.mFactory.GetDbParameter("@TotalPageCount",DbType.Int32),//返回总页数
                    this.mFactory.GetDbParameter("@TotalCount",DbType.Int32),//记返回总记录
                    this.mFactory.GetDbParameter("@RecorderCount",DbType.Int32),//记返回总记录
            };
            int sortValue = 3;
            switch (SortType)
            {case ESortType.ASC:
                    sortValue = 1;
                    break;
                case ESortType.DESC:
                    sortValue = 2;
                    break;
            }
            pramers[0].Value = sql;
            pramers[1].Value = fileds;
            pramers[2].Value = SortName;
            pramers[3].Value = iPageSize;
            pramers[4].Value = iPageIndex;
            pramers[5].Value = sortValue;
            pramers[6].Value = "1=1";
            pramers[7].Value = key;
            pramers[8].Direction = ParameterDirection.Output;
            pramers[9].Direction = ParameterDirection.Output;
            pramers[10].Value = 0;


            DbCommand sqlCmd = this.mFactory.GetDbCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            List<E> query = null;
            DbDataReader SqlReader = null;
            try
            {

                sqlCmd.CommandText = spName;
                sqlCmd.Parameters.AddRange(pramers);
                sqlCmd.Connection = this.mFactory.GetDbConnection();
                if (sqlCmd.Connection.State == System.Data.ConnectionState.Closed)
                {
                    sqlCmd.Connection.Open();
                }
                SqlReader = sqlCmd.ExecuteReader();
                query = ((IObjectContextAdapter)this.GetDbContext).ObjectContext.Translate<E>(SqlReader).ToList();
                if (SqlReader != null && !SqlReader.IsClosed)
                {
                    SqlReader.Close();
                    SqlReader.Dispose();
                }
                if (sqlCmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    sqlCmd.Connection.Close();
                }

                iPageCount = pramers[8].Value == DBNull.Value ? 0 : Convert.ToInt32(pramers[8].Value);
                iRecordCount = pramers[9].Value == DBNull.Value ? 0 : Convert.ToInt32(pramers[9].Value);
            }
            catch (Exception ex)
            {
                log.Error(" GetSqlSource<E>(" + sql + ")", ex);
                throw ex;
            }
            finally
            {
                if (SqlReader != null && !SqlReader.IsClosed)
                {
                    SqlReader.Close();
                    SqlReader.Dispose();
                }
                if (sqlCmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    sqlCmd.Connection.Close();
                }
            }
            return query;
        }


        /// <summary>
        /// 执行存储过程，无返回参数
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="dbParameters"></param>
        public int ExProcNoReader(string procName, params DbParameter[] dbParameters)
        {
            DbCommand dbCmd = this.mFactory.GetDbCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            try
            {

                dbCmd.CommandText = procName;
                if (dbParameters != null)
                    dbCmd.Parameters.AddRange(dbParameters);
                dbCmd.Connection = this.mFactory.GetDbConnection();
                if (dbCmd.Connection.State == System.Data.ConnectionState.Closed)
                {
                    dbCmd.Connection.Open();
                }
                return dbCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                log.Error(" GetSqlSource<E>(" + procName + ")", ex);
                throw ex;
            }
            finally
            {

                if (dbCmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    dbCmd.Connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行存储过程 使用dbfactory创建dbParameter
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="dbParameters"></param>
        /// <returns></returns>
        public List<T> ExProc(string procName, params DbParameter[] dbParameters)
        {
            return ExProc<T>(procName, dbParameters);
        }

        /// <summary>
        /// 执行存储过程 使用dbfactory创建dbParameter
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="proc"></param>
        /// <param name="dbParameters"></param>
        /// <returns></returns>
        public List<E> ExProc<E>(string procName,params DbParameter[] dbParameters)
        {
            DbCommand dbCmd = this.mFactory.GetDbCommand();
            dbCmd.CommandType = CommandType.StoredProcedure;
            List<E> query = null;
            DbDataReader dbReader = null;
            try
            {

                dbCmd.CommandText = procName;
                if (dbParameters!=null)
                dbCmd.Parameters.AddRange(dbParameters);
                dbCmd.Connection = this.mFactory.GetDbConnection();
                if (dbCmd.Connection.State == System.Data.ConnectionState.Closed)
                {
                    dbCmd.Connection.Open();
                }
                dbReader = dbCmd.ExecuteReader();
                query = ((IObjectContextAdapter)this.mDbContext).ObjectContext.Translate<E>(dbReader).ToList();
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                }
            }
            catch (Exception ex)
            {
                string pms = string.Empty;
                foreach (DbParameter pm in dbParameters)
                    pms+=pm.ParameterName + "," + pm.Value + "|";
                log.Error(" GetSqlSource<E>(" + procName + "," + pms + ")", ex);
                throw ex;
            }
            finally
            {
                if (dbReader != null && !dbReader.IsClosed)
                {
                    dbReader.Close();
                    dbReader.Dispose();
                }
                if (dbCmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    dbCmd.Connection.Close();
                }
            }

            return query;
        }
      
        #endregion


        public DbContext GetDbContext
        {
            get { return this.mDbContext; }
        }


        public List<T> GetEntitysPage(string sql, string fileds, string SortName, ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount)
        {
            return this.GetEntitysPage(sql, "Id", fileds, SortName, SortType, iPageIndex, iPageSize, ref iPageCount, ref iRecordCount);
        }

        public List<E> GetEntitysPage<E>(string sql, string fileds, string SortName, ESortType SortType, int iPageIndex, int iPageSize, ref int iPageCount, ref int iRecordCount)
        {
            return this.GetEntitysPage<E>(sql,"Id", fileds, SortName, SortType, iPageIndex, iPageSize, ref iPageCount, ref iRecordCount);
        }
    }
}
