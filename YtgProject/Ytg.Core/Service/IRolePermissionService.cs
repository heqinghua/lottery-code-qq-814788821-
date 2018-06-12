using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 角色权限服务
    /// </summary>
    public interface IRolePermissionService:ICrudService<RolePermission>  
    {
        /// <summary>
        /// 设置角色权限列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="permissionIds">权限ID集合</param>
        /// <returns></returns>
        bool SetRolePermission(int roleId, List<int> permissionIds);


        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<RolePermission> GetRolePermissionList(int roleId);
    }
}
