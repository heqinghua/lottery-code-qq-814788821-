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
    public class SysQuotaDetaiDTO
    {

        public int Id { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 返点类型
        /// </summary>
        public string QuotaType { get;set;}

        /// <summary>
        /// 对应配额
        /// </summary>
        public int SysQuotaId { get; set; }

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

        public DateTime OccDate { get;set;}
        
    }
}
