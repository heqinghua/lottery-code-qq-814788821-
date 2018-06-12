using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 系统消息
    /// </summary>
    public class SysMessages : BaseEntity
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent { get; set; }

        /// <summary>
        /// 消息提醒 0 投注消息 1 充值消息 2 
        /// </summary>
        public int MsgType { get; set; }

        /// <summary>
        /// 状态 0为未读 1 为已读
        /// </summary>
        public int State { get; set; }
    }
}
