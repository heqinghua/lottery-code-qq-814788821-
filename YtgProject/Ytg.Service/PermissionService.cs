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
    /// 权限服务
    /// </summary>
    public class PermissionService: CrudService<Permission>, IPermissionService
    {
        public PermissionService(IRepo<Permission> repo)
            : base(repo)
        {

        }

        /// <summary>
        /// 获取页面操作按钮
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        public List<Permission> GetPagePermissionList(int userId,int pId)
        {
            string sqlStr = @"select c.* from AccountRoles as a 
                           inner join RolePermissions as b on a.RoleId=b.RoleId
                           inner join [Permissions] as c on b.PermissionId=c.Id
                           where a.UserId=" + userId + " and PId=" + pId;

            var list = this.GetSqlSource<Permission>(sqlStr);
            return list;
        }

        /// <summary>
        /// 获取获取管理员菜单权限列表
        /// </summary>
        /// <returns></returns>
        public List<Permission> GetMenuList(int userId)
        {
            string sqlStr = @"select c.* from AccountRoles as a
		        inner join RolePermissions as b on a.RoleId=b.RoleId
		        inner join [Permissions] as c on b.PermissionId=c.Id
		        where c.ActionType=1 and a.UserId=" + userId.ToString() +
                " group by c.Id,c.Name,c.Url,c.[Action],c.ActionType,c.PId,c.OccDate order by c.OccDate ";
            var list = this.GetSqlSource<Permission>(sqlStr);
            return list;
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public bool UpdatePermission(Permission permission)
        {
            if (null == permission)
                return false;

            Permission rPermission = this.Get(permission.Id);
            if (null == rPermission)
                return false;

            rPermission.Name = permission.Name;
            rPermission.Action = permission.Action;
            rPermission.Url = permission.Url;

            this.Save();

            return true;
        }
    }
}
