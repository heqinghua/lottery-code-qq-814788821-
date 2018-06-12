using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    [ServiceContract]
    public interface ISysRoleService : ICrudService<SysRole>
    {

        /// <summary>
        /// 获取所有角色，未禁用的
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SysRole> GetAllRoles();

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateRole(SysRole item);

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddRole(SysRole item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
         [OperationContract]
        List<SysRolePermission> GetRolePermission(int roleid);

        /// <summary>
        /// 修改角色菜单权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="menuids"></param>
        /// <returns></returns>
         [OperationContract]
        bool UpdateRolePermission(int roleid, string menuids);


    }
}
