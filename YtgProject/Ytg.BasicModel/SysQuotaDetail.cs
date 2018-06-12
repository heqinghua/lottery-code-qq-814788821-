using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 配额账变
    /// </summary>
    public class SysQuotaDetail : BaseEntity
    {
        /// <summary>
        /// 对应配额
        /// </summary>
        public int SysQuotaId { get; set; }

        public SysQuota SysQuota { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public ActionType ActionType { get; set; }

        /// <summary>
        /// 原有配额
        /// </summary>
        public int OldNum { get; set; }

        /// <summary>
        /// 当前配额
        /// </summary>
        public int NowNum { get; set; }

        /// <summary>
        /// 操作用户
        /// </summary>
        public string OpUser { get; set; }
        
    }
}
