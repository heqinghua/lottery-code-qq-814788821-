using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 联系人
    /// </summary>
    public class ConTactDTO
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string code { get; set; }

        public string NikeName { get; set; }

        public int? ParentId { get; set; }

        public bool IsLogin { get; set; }
    }
}
