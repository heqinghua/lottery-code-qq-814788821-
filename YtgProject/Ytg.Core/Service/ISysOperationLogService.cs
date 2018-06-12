using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 操作日志服务
    /// </summary>
    [ServiceContract]
    public interface ISysOperationLogService : ICrudService<SysOperationLog>
    {
        /// <summary>
        /// 获取系统操作日志列表
        /// </summary>
        /// <param name="userCode">操作人用户</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">截止日期</param>
        /// <returns></returns>
        [OperationContract]
        List<SysOperationLog> GetSysOperationLogs(string userCode, DateTime beginDate, DateTime endDate, int pageIndex, int pageSzie, ref int totalCount);
    }
}
