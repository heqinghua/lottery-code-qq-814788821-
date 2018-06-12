using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service.Lott
{
    /// <summary>
    /// 彩票类型
    /// </summary>
    [ServiceContract]
    public interface IPlayNumTypeService : ICrudService<PlayTypeNum>
    {
        [OperationContract]
        IEnumerable<PlayTypeNum> GetAllPlayTypeNums();
    }
}
