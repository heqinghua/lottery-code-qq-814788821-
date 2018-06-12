using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 系统角色
    /// </summary>
    public class SysRole : DelEntity
    {
        public SysRole()
        {
            
        }

        [MaxLength(100)]
        public  string RoleName { get; set; }

        public int? ParentID { get; set; }

        /// <summary>
        /// 父角色
        /// </summary>
        public virtual SysRole Parent { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(500)]
        public  string Description { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual ICollection<SysUser> SysUsers { get; set; }
        /// <summary>
        /// 组织结构
        /// </summary>
        public virtual ICollection<SysOrganize> SysOrganizes { get; set; }
    }
}
