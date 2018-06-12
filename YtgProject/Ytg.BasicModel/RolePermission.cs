using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 角色，权限
    /// </summary>
    public class RolePermission : BaseEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public int PermissionId { get; set; }
    }
}
