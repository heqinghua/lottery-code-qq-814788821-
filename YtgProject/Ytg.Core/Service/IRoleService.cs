using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public interface IRoleService : ICrudService<Role>  
    {
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        bool UpdateRole(Role role);

        /// <summary>
        /// 角色名称唯一性检查
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        bool IsUnique(string roleName);

        bool IsUnique(string roleName, int roleId);
    }
}
