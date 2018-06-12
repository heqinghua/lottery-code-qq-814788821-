using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 管理员角色服务
    /// </summary>
    public interface IAccountRoleService : ICrudService<AccountRole>  
    {
        /// <summary>
        /// 设置管理员角色
        /// </summary>
        /// <param name="userId">管理员ID</param>
        /// <param name="roleIds">角色ID集合</param>
        /// <returns></returns>
        bool SetAccountRole(int userId, List<int> roleIds);

        /// <summary>
        /// 获取管理员角色ID
        /// </summary>
        /// <param name="userid">管理员ID</param>
        /// <returns></returns>
        List<AccountRole> GetAccountRoleList(int userId);       
 

         /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        List<UserRoleInfo> GetUserRoleName(string roleName);
    }
}
