using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
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
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SysRoleService : CrudService<SysRole>, ISysRoleService
    {
        public SysRoleService(IRepo<SysRole> repo)
            : base(repo)
        {

        }


        public List<SysRole> GetAllRoles()
        {
            return this.GetAll().ToList();
        }

        public bool UpdateRole(SysRole item)
        {
            var c = this.Get(item.Id);
            if (null == c)
                return false;
            c.Description = item.Description;
            c.IsDelete = item.IsDelete;
            c.ParentID = item.ParentID;
            c.RoleName = item.RoleName;

            this.Save();
            return true;
        }

        public bool AddRole(SysRole item)
        {
            this.Create(item);
            this.Save();
            return true;
        }


        public List<SysRolePermission> GetRolePermission(int roleid)
        {
            string sql = "select * from SysRolePermissions  where sysRoleId=" + roleid;
            return this.GetSqlSource<SysRolePermission>(sql);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="menuids"></param>
        /// <returns></returns>
        public bool UpdateRolePermission(int roleid, string menuids)
        {
            if (string.IsNullOrEmpty(menuids))
                return false;
            var par = new SqlParameter()
            {
                ParameterName = "@sysRoleId",
                Value = roleid
            };
            string delSql = "delete SysRolePermissions  where sysRoleId=" + roleid;
            this.mRepo.GetDbContext.Database.ExecuteSqlCommand(delSql, par);

            var array = menuids.Split(',');
            foreach (var a in array)
            {
                if (string.IsNullOrEmpty(a))
                    continue;
                string insertSql = "INSERT INTO [SysRolePermissions]" +
                                   "([SysMenuId],[SysRoleId],[Code],[IsDelete],[OccDate])" +
                                   "VALUES(@SysMenuId,@SysRoleId,'','false',@OccDate)";
                var par1 = new SqlParameter()
                {
                    ParameterName = "@sysRoleId",
                    Value = roleid
                };
                var par2 = new SqlParameter()
                {
                    ParameterName = "@SysMenuId",
                    Value = a
                };
                var par3 = new SqlParameter()
                {
                    ParameterName = "@OccDate",
                    Value = DateTime.Now
                };
                this.mRepo.GetDbContext.Database.ExecuteSqlCommand(insertSql, par1, par2, par3);
            }

            return true;
        }
    }
}
