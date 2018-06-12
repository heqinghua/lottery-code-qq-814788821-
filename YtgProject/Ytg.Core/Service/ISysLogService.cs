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
    /// 系统日志
    /// </summary>
    [ServiceContract]
    public interface ISysLogService : ICrudService<SysLog>
    {
        [OperationContract]
        List<SysUserLoginStatisticsVM> SelectLoginLogStatistics(int dateType, string sDate, string eDate);

        [OperationContract]
        List<SysLoginLogVM> SelectLoginLogs(string sDate, string eDate, int userType, string code, int pageIndex, ref int totalCount);
    }
}
