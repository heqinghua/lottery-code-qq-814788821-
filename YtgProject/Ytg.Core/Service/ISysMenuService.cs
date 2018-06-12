using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    [ServiceContract]
    public interface ISysMenuService:ICrudService<SysMenu>
    {
        [OperationContract]
        List<SysMenu> GetAllMenu();

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateMenu(SysMenu item);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddMenu(SysMenu item);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool Del(int id);
    }
}
