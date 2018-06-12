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
    /// 权限服务
    /// </summary>
    [ServiceContract]
    public interface IPermissionService : ICrudService<Permission>    
    {

        /// <summary>
        /// 获取页面操作按钮
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        List<Permission> GetPagePermissionList(int userId, int pId);

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdatePermission(Permission permission);

        
        /// <summary>
        /// 获取获取管理员菜单
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Permission> GetMenuList(int userId);

        ///// <summary>
        ///// 获取
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //List<Permission> GetActionList(int userId);
    }
}
