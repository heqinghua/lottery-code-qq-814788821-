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
    /// 系统管理后台登录日志
    /// </summary>
    [ServiceContract]
    public interface ISysAccountLogsService : ICrudService<SysAccountLog>
    {
        [OperationContract]
        List<SysAccountLog> GetLoginLogs(string sDate, string eDate);

        [OperationContract]
        List<SysAccountLogsMV> GetLoginLogs(string sDate, string eDate, string code, int pageIndex, ref int totalCount);
    }
}
