using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 公告管理
    /// </summary>
    [ServiceContract]
    public interface ISysNoticeService : ICrudService<SysNotice>
    {
        /// <summary>
        /// 新增 wcf
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool CreateNotice(SysNotice item);


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="code"></param>
        /// <param name="nickName"></param>
        /// <param name="isDel"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<SysNotice> GetNotice(string sTitle, int? isHot, bool? isDel, int pageIndex, ref int totalCount);

        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SysNotice GetForId(int id);

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool UpdateItem(int id, SysNotice item);
    }
}
