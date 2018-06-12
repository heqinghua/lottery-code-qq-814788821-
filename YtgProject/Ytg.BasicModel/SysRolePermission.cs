using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 权限
    /// </summary>
    public class SysRolePermission: DelEntity
    {
        public int SysMenuId { get; set; }
        /// <summary>
        /// 菜单
        /// </summary>
        public SysMenu SysMenu { get; set; }

        public int SysRoleId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public SysRole SysRole { get;set;}

        [MaxLength(100)]
        public string Code { get; set; }
    }
}
