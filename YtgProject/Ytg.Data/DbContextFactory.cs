using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Data
{
    /// <summary>
    /// 数据对象工厂
    /// </summary>
    public class DbContextFactory : IDbContextFactory
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private const string mConnectionString = "name=YtgConnection";

        readonly string mSqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["YtgConnection"].ConnectionString;

        private readonly DbContext mDbContext;

        public DbContextFactory()
        {
            this.mDbContext = new YtgDbContext(mConnectionString);

        }

        /// <summary>
        /// 获取数据上下文
        /// </summary>
        /// <returns></returns>
        public System.Data.Entity.DbContext GetDbContext()
        {
            return this.mDbContext;
        }



        /// <summary>
        /// 获取DbCommand
        /// </summary>
        /// <returns></returns>
        public System.Data.Common.DbCommand GetDbCommand()
        {
            return new SqlCommand();
        }


        public System.Data.Common.DbDataReader GetDataReader()
        {
            return null;
            //return new SqlDataReader();
        }

        public System.Data.Common.DbConnection GetDbConnection()
        {
            return new SqlConnection(mSqlConnectionString);
        }


        public System.Data.Common.DbParameter GetDbParameter(string param, System.Data.DbType dbType, System.Data.ParameterDirection dircetion = System.Data.ParameterDirection.Input)
        {
            var pa= new SqlParameter();
            pa.DbType = dbType;
            pa.ParameterName = param;
            pa.Direction = dircetion;
            return pa;
        }


        public System.Data.Common.DbDataAdapter GetDbDataAdapter()
        {
            return new SqlDataAdapter();
        }
    }
}
