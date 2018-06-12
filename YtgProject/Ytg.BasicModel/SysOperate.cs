using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 菜单操作表
    /// </summary>
    public class SysOperate : BaseEntity
    {
        
        public int OperateType { get; set; }

        public int? SysMenuId { get; set; }
        public SysMenu SysMenu { get; set; }
    }
}
