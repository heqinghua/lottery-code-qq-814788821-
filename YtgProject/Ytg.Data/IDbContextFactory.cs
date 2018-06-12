using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Data
{
    public interface IDbContextFactory
    {
        DbContext GetDbContext();

        System.Data.Common.DbCommand GetDbCommand();


        System.Data.Common.DbDataReader GetDataReader();

        System.Data.Common.DbConnection GetDbConnection();

        System.Data.Common.DbDataAdapter GetDbDataAdapter();

        System.Data.Common.DbParameter GetDbParameter(string param, System.Data.DbType dbType, System.Data.ParameterDirection direction = System.Data.ParameterDirection.Input);

    }
}
