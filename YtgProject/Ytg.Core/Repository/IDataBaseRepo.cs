using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Core.Repository
{
    public interface IDataBaseRepo
    {
        /// <summary>
        /// 同步数据库
        /// </summary>
        void SynchronizationDataBase();

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>执行是否成功</returns>
        bool ExecuteSqlCommand(string sql);
    }
}
