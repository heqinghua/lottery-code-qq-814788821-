using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [MaxLength(500)]
        public string Descript { get; set; }
    }
}
