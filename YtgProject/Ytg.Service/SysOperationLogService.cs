using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Data;

namespace Ytg.Service
{
    /// <summary>
    /// 操作日志服务
    /// </summary>
    public class SysOperationLogService : CrudService<SysOperationLog>, ISysOperationLogService
    {
        public SysOperationLogService(IRepo<SysOperationLog> repo): base(repo){ }

        /// <summary>
        /// 获取操作日志列表
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<SysOperationLog> GetSysOperationLogs(string userCode, DateTime beginDate, DateTime endDate, int pageIndex, int pageSzie, ref int totalCount)
        {

            //参数设置
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@userCode",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageSzie",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@beginTime",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@endTime",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output)
            };

            parameters[0].Value = userCode;
            parameters[1].Value = pageIndex;
            parameters[2].Value = pageSzie;
            parameters[3].Value = beginDate;
            parameters[4].Value = endDate;

            List<SysOperationLog> list = this.ExProc<SysOperationLog>("sp_GetSysOperationLogs", parameters);
            totalCount = Convert.ToInt32(parameters[5].Value);

            return list;
        }
    }
}
