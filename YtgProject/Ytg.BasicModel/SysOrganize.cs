using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 组织结构表
    /// </summary>
    public class SysOrganize:DelEntity
    {
        public SysOrganize()
        {
          
        }

        /// <summary>
        /// 组织名称
        /// </summary>
        [MaxLength(100)]
        public  string OrganizeName { get; set; }

        [MaxLength(500)]
        /// <summary>
        /// 描述
        /// </summary>
        public  string Description { get; set; }

        public virtual ICollection<SysRole> SysRoles { get; set; }
    }
}
