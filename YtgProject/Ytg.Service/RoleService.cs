using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class RoleService: CrudService<Role>, IRoleService
    {

        public RoleService(IRepo<Role> repo)
            : base(repo)
        {

        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool UpdateRole(Role role)
        {
            if (null == role)
                return false;

            Role rRole = this.Get(role.Id);
            if (rRole == null)
                return false;

            rRole.Name = role.Name;
            rRole.Descript = role.Descript;
            this.Save();

            return true;
        }

        /// <summary>
        /// 角色名称唯一性检查
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool IsUnique(string roleName)
        {
            return this.mRepo.Where(o => o.Name == roleName).Count() == 0;
        }

        public bool IsUnique(string roleName, int roleId)
        {
            return this.mRepo.Where(o => o.Name == roleName && o.Id != roleId).Count() == 0;
        }
    }
}
