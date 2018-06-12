using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    public class VipMentionApply : EnaEntity
    {
        /// <summary>
        /// true 是打开，false是关闭
        /// </summary>
        public bool IsOpenVip { get; set; }

        public int UserId { get; set; }
    }
}
