using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 消息
    /// </summary>
    [DataContract]
    public class Message : DelEntity
    {
        /// <summary>
        /// 谁发的
        /// </summary>
        [DataMember]
        public int FormUserId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [DataMember]
        public int ToUserId { get; set; }

        /// <summary>
        /// 消息列表：1系统消息 2 私人消息 4 中奖消息 8充提信息
        /// </summary>
        [DataMember]
        public int MessageType { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [DataMember]
        public string MessageContent { get; set; }

        /// <summary>
        /// 状态：0未读、1已读
        /// </summary>
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string Title { get; set; }
    }
}
