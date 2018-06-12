using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    /// <summary>
    /// 这个共页面使用
    /// </summary>
    public class MessageDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// 谁发的
        /// </summary>
        public int FormUserId { get; set; }

        /// <summary>
        /// 谁发送的
        /// </summary>
        public string FormUser { get; set; }

        /// <summary>
        /// 对话人员
        /// </summary>
        public string DialogueUser { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int ToUserId { get; set; }


        public string ToUser { get; set; }

        /// <summary>
        /// 消息列表：1系统消息 2 私人消息 4 中奖消息 8充提信息
        /// </summary>
        public int MessageType { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent { get; set; }

        /// <summary>
        /// 状态：0未读、1已读
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime OccDate { get; set; }

        public string Title { get; set; }
    }
}
