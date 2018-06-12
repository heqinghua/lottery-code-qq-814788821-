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
    /// 新闻
    /// </summary>
    [ServiceContract]
    public interface INewsService : ICrudService<SysNews>
    {
        [OperationContract]
        List<SysNews> SelectBy(int isShow, string title, string sDate, string eDate, int pageIndex, ref int totalCount);

        [OperationContract]
        bool AddNews(SysNews model);

        [OperationContract]
        bool DeleteNews(int id);

        [OperationContract]
        bool UpdateNews(SysNews model);
    }
}
