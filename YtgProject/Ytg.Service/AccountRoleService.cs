using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    /// <summary>
    /// 管理员角色服务
    /// </summary>
    public class AccountRoleService : CrudService<AccountRole>, IAccountRoleService
    {
        public AccountRoleService(IRepo<AccountRole> repo)
            : base(repo)
        {

        }

        /// <summary>
        /// 设置管理员角色
        /// </summary>
        /// <param name="userId">管理员ID</param>
        /// <param name="roleIds">角色ID集合</param>
        /// <returns></returns>
        public bool SetAccountRole(int userId, List<int> roleIds)
        {
            try
            {
                List<AccountRole> list = this.GetAccountRoleList(userId);

                if (list != null && list.Count > 0)
                {
                    //移除现有的角色
                    list.ForEach(m => { this.Delete(m); });
                }

                //新增
                foreach (var roleId in roleIds)
                {
                    AccountRole accountRole = new AccountRole();
                    accountRole.UserId = userId;
                    accountRole.RoleId = roleId;
                    this.Create(accountRole);
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
        /// 获取管理员角色列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<AccountRole> GetAccountRoleList(int userId)
        {
            return this.GetAll().Where(m => m.UserId == userId).ToList();
        }


        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public List<UserRoleInfo> GetUserRoleName(string roleName)
        {
            string sql = @"select  sa.Code,r.Name from AccountRoles as ar inner join SysAccounts as sa on ar.UserId=sa.Id inner join Roles as r on r.Id=ar.RoleId where sa.Code='" + roleName + "'";
            return this.GetSqlSource<UserRoleInfo>(sql);
        }
    }
}
