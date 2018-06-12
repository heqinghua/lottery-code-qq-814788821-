using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 消息服务
    /// </summary>
    [ServiceContract]
    public interface IMessageService : ICrudService<Message>
    {
        /// <summary>
        /// 发送消息，普通用户发送
        /// </summary>
        /// <param name="fromUserId">哪个用户发的</param>
        /// <param name="toUserId">发给哪个用户</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        bool SendMessage(int fromUserId, int toUserId, string content);

        /// <summary>
        /// 系统发送消息
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        [OperationContract]
        bool SendMessageSystem(string content);

        /// <summary>
        /// 系统发送消息给指定用户
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="toUserId"></param>
        /// <param name="messageType"></param>
        /// <returns></returns>
        [OperationContract]
        bool SendMessageBySystem(string title, string content, int toUserId, int messageType);

        /// <summary>
        /// 给下级发送消息
        /// </summary>
        /// <param name="fromUserId">哪个用户发送的</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        bool SendMessageToSubordinate(int fromUserId, string content);

        /// <summary>
        /// 读消息，这个时候根据Id取该发送人及发给谁的共上下6条消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<MessageDTO> ReadMessage(int id);

        List<MessageDTO> LoadMessage(int fromUserId, int toUserId);

        List<MessageDTO> SelectBy(int pageIndex, int pageSize, int userId, int messageType, int status, ref int totalCount);

        /// <summary>
        /// 后台查看消息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="messagetype"></param>
        /// <param name="status"></param>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<MessageVM> SelectMessageListBy(string code, int messagetype, int status, string sDate, string eDate, int pageIndex, ref int totalCount);

        [OperationContract]
        bool DeleteById(int id);

        /// <summary>
        /// 获取聊天消息
        /// </summary>
        /// <param name="state">0 未读 1 已读 -1 不过滤</param>
        /// <returns></returns>
        List<Message> GetChartMsg(int loginid, int fid, int state, DateTime? minDate, int pageIndex, ref int pageCount, ref int totalCount);

        /// <summary>
        /// 发送消息  群发
        /// </summary>
        /// <param name="messgaeType"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="toUserType"></param>
        void InstatMessage(int messgaeType, string title, string content, int toUserType);

        /// <summary>
        /// 获取某用户的未消息内容
        /// </summary>
        /// <param name="userid"></param>
        List<Message> RefUserMessage(string userid);

        /// <summary>
        /// 修改消息为已读
        /// </summary>
        /// <param name="userid"></param>
        void UpdateReadMessgae(string ids);
    }
}
