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
    /// 角色权限服务
    /// </summary>
    public class RolePermissionService: CrudService<RolePermission>, IRolePermissionService
    {
        public RolePermissionService(IRepo<RolePermission> repo)
            : base(repo)
        {

        }

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="permissionIds">权限ID集合</param>
        /// <returns></returns>
        public bool SetRolePermission(int roleId, List<int> permissionIds)
        {
            try
            {
                List<RolePermission> list = this.GetRolePermissionList(roleId);
                if (list != null && list.Count > 0)
                {
                    //移除现有的角色
                    list.ForEach(m => { this.Delete(m); });
                }

                //新增
                foreach (var permissionId in permissionIds)
                {
                    RolePermission rolePermission = new RolePermission();
                    rolePermission.RoleId = roleId;
                    rolePermission.PermissionId = permissionId;
                    this.Create(rolePermission);
                }

                //保存
                this.Save();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
            
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public List<RolePermission> GetRolePermissionList(int roleId)
        {
            return this.GetAll().Where(m => m.RoleId == roleId).ToList();
        }
    }
}
