using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Permission : BaseEntity
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 页面地址
        /// </summary>
        [MaxLength(200)]
        public string Url { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        [MaxLength(200)]
        public string Action { get; set; }

        /// <summary>
        /// 1=根节点,2=菜单,3=动作
        /// </summary>
        public int ActionType { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public int PId { get; set; }
    }
}
