using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    public class SysMenu : DelEntity
    {
        public SysMenu()
        {
            this.Operates = new HashSet<SysOperate>();
            this.SysRolePermissions = new HashSet<SysRolePermission>();
        }

        /// <summary>
        /// 菜单code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        public  int? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(100),DataMember]
        public  string MenuName { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [MaxLength(200)]
        public  string URL { get; set; }

        

        /// <summary>
        /// 是否显示
        /// </summary>
        public  bool IsVisible { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(500)]
        public  string Description { get; set; }

        /// <summary>
        /// 拥有操作
        /// </summary>
        
        [JsonIgnore]
        public virtual ICollection<SysOperate> Operates { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<SysRolePermission> SysRolePermissions { get; set; }
    }
}
