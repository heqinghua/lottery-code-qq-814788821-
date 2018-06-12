using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Data;

namespace Ytg.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class MessageService : CrudService<Message>, IMessageService
    {
        public MessageService(IRepo<Message> repo)
            : base(repo)
        {
        }
        /// <summary>
        /// 发送消息，普通用户发送
        /// </summary>
        /// <param name="fromUserId">哪个用户发的</param>
        /// <param name="toUserId">发给哪个用户</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public bool SendMessage(int fromUserId, int toUserId, string content)
        {
            var message = this.Create(new Message
            {
                FormUserId = fromUserId,
                ToUserId = toUserId,
                MessageContent = content,
                MessageType = 1,
                OccDate = DateTime.Now,
                Status = 0,
                IsDelete = false
            });
            this.Save();
            return true;
        }

        /// <summary>
        /// 系统发送消息
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public bool SendMessageSystem(string content)
        {
            var flag = false;
            //得到系统里面所有的用户
            var sql = "select * from SysUser";
            var items = this.GetSqlSource<SysUser>(sql);
            if (null != items && items.Count > 0)
            {
                items.ForEach(m =>
                {
                    this.Create(new Message
                    {
                        FormUserId = -1,
                        ToUserId = m.Id,
                        MessageContent = content,
                        MessageType = 0,
                        OccDate = DateTime.Now,
                        Status = 0,
                        IsDelete = false
                    });
                });
            }

            return flag;
        }

        /// <summary>
        /// 给下级发送消息
        /// </summary>
        /// <param name="fromUserId">哪个用户发送的</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public bool SendMessageToSubordinate(int fromUserId, string content)
        {
            var flag = false;
            //得到系统里面所有的用户
            var sql = "select * from SysUser where parentId=" + fromUserId;
            var items = this.GetSqlSource<SysUser>(sql);
            if (null != items && items.Count > 0)
            {
                items.ForEach(m =>
                {
                    this.Create(new Message
                    {
                        FormUserId = fromUserId,
                        ToUserId = m.Id,
                        MessageContent = content,
                        MessageType = 0,
                        OccDate = DateTime.Now,
                        Status = 0,
                        IsDelete = false
                    });
                });
            }

            return flag;
        }

        public List<MessageDTO> ReadMessage(int id)
        {
            var item = this.Get(id);
            item.Status = 1;
            this.Save();
            var sql = string.Empty;
            if (item.ToUserId == -1 || item.FormUserId == -1)
            {
                sql = string.Format("SELECT TOP 6 m.*,f.NikeName FormUser,t.NikeName ToUser FROM dbo.[Messages] m LEFT JOIN dbo.SysYtgUser f ON f.Id=m.FormUserId LEFT JOIN dbo.SysYtgUser t ON t.Id=ToUserId where ((m.FormUserId={0} and m.ToUserId=-1) or (m.ToUserId={0} and m.FormUserId=-1))", item.FormUserId > 0 ? item.FormUserId : item.ToUserId);
            }
            else
                sql = string.Format("SELECT TOP 6 m.*,f.NikeName FormUser,t.NikeName ToUser FROM dbo.[Messages] m LEFT JOIN dbo.SysYtgUser f ON f.Id=m.FormUserId LEFT JOIN dbo.SysYtgUser t ON t.Id=ToUserId where ((m.FormUserId={0} and m.ToUserId={1}) or (m.ToUserId={0} and m.FormUserId={1}))", item.FormUserId, item.ToUserId);

            return this.GetSqlSource<MessageDTO>(sql);
        }


        /// <summary>
        /// 加载消息内容
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId">用户Id</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="status">状态：-1所有 0未读 1已读</param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<MessageDTO> SelectBy(int pageIndex, int pageSize, int userId, int messageType, int status, ref int totalCount)
        {
            #region old
            //#region 设置参数
            //DbParameter[] parameters = new DbParameter[]{
            //   new DbContextFactory().GetDbParameter("@userId",System.Data.DbType.Int32),
            //   new DbContextFactory().GetDbParameter("@messageType",System.Data.DbType.Int32),
            //   new DbContextFactory().GetDbParameter("@status",System.Data.DbType.Int32),

            //   new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
            //   new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32),
            //   new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output)
            //};

            //parameters[0].Value = userId;
            //parameters[1].Value = messageType;
            //parameters[2].Value = status;
            //parameters[3].Value = pageIndex;
            //parameters[4].Value = pageSize;
            //#endregion
            //var list = this.ExProc<MessageDTO>("sp_Messages_SelectByUserId", parameters);
            //totalCount = Convert.ToInt32(parameters[5].Value ?? 0);
            //return list;
            #endregion

            string sql = "(select yu.Code as ToUser,msg.* from [Messages] as msg inner join  [SysYtgUser] as yu on msg.ToUserId =yu.Id where 1=1";
            if (userId != -1)
                sql += string.Format(" and ToUserId={0}", userId);
            if (messageType != -1)
                sql += string.Format(" and MessageType={0}", messageType);

            if (status != -1)
                sql += string.Format(" and Status={0}", status);
            sql += ") as t1";
            int iPageCount = 0;
            return this.GetEntitysPage<MessageDTO>(sql, "Id", "*", "OccDate Desc", ESortType.DESC, pageIndex, pageSize, ref iPageCount, ref totalCount);
        }


        public List<MessageDTO> LoadMessage(int fromUserId, int toUserId)
        {
            var sql = string.Format("SELECT TOP 6 m.*,f.NikeName FormUser,t.NikeName ToUser FROM dbo.[Messages] m LEFT JOIN dbo.SysYtgUser f ON f.Id=m.FormUserId LEFT JOIN dbo.SysYtgUser t ON t.Id=ToUserId where ((m.FormUserId={0} and m.ToUserId={1}) or (m.ToUserId={0} and m.FormUserId={1}))", fromUserId, toUserId);
            return this.GetSqlSource<MessageDTO>(sql);
        }

        /// <summary>
        /// 后台，wcf服务 系统发送消息给指定用户
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="toUserId"></param>
        /// <param name="messageType"></param>
        /// <returns></returns>
        public bool SendMessageBySystem(string title, string content, int toUserId, int messageType)
        {
            var message = this.Create(new Message
            {
                FormUserId = -1,
                ToUserId = toUserId,
                MessageContent = content,
                MessageType = messageType,
                OccDate = DateTime.Now,
                Status = 0,
                IsDelete = false,
                Title = title
            });
            this.Save();
            return true;
        }

        /// <summary>
        /// 后台WCF查询消息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="messagetype"></param>
        /// <param name="status"></param>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<MessageVM> SelectMessageListBy(string code, int messagetype, int status, string sDate, string eDate, int pageIndex, ref int totalCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"WITH result AS(
SELECT m.Id,CASE WHEN m.Title IS NULL THEN SUBSTRING(m.MessageContent,1,50) ELSE m.Title END Title,m.MessageContent,m.MessageType,m.Status,
	m.OccDate,
	(CASE WHEN m.ToUserId=-1 THEN '系统' ELSE tu.Code END) ToUser, (CASE WHEN m.FormUserId=-1 THEN '系统' ELSE fu.Code END) FromUser,m.ToUserId,m.FormUserId FromUserId
FROM dbo.Messages m
LEFT JOIN dbo.SysYtgUser tu ON tu.Id=m.ToUserId
LEFT JOIN dbo.SysYtgUser fu ON fu.Id=m.FormUserId
WHERE m.IsDelete=0 AND ('{0}'='' OR fu.Code LIKE '%{0}%') AND ({1}=-1 OR m.MessageType={1})
	AND ({2}=-1 OR m.STATUS={2}) AND ('{3}'='' or m.OccDate>='{3}') AND ('{4}'='' or m.OccDate<'{4}')
),totalCount AS
(
	SELECT COUNT(1) TotalCount FROM result
)
select * from (
	select *, ROW_NUMBER() OVER(Order by a.OccDate DESC ) AS RowNumber from result as a,totalCount
) as b
where RowNumber BETWEEN ({5}-1)*{6}+1 AND {5}*{6}", code, messagetype, status, sDate, eDate, pageIndex, AppGlobal.ManagerDataPageSize);
            var list = this.GetSqlSource<MessageVM>(sb.ToString());
            totalCount = list.Count > 0 ? list.First().TotalCount : 0;
            return list;
        }

        public bool DeleteById(int id)
        {
            var entity = this.Get(id);
            entity.IsDelete = true;
            this.Save();
            return true;
        }



        /// <summary>
        /// 获取聊天消息
        /// </summary>
        /// <param name="state">0 未读 1 已读 -1 不过滤</param>
        /// <returns></returns>
        public List<Message> GetChartMsg(int loginid, int fid, int state, DateTime? minDate, int pageIndex, ref int pageCount, ref int totalCount)
        {
            string sql = "select top 20 * from [dbo].[ChartMessages] where 1=1 ";
            sql += string.Format(" and ToUserId={0}", loginid);
            //if (minDate != null)
            //    sql += string.Format(" and occdate<'{0}'", minDate.Value);
            if (state != -1)
                sql += " and status=" + state;
            sql += " order by OccDate desc";

            var sources = this.GetSqlSource<Message>(sql);
            //修改状态为已读
            System.Data.SqlClient.SqlParameter[] paramerter = new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@ToUserId",System.Data.SqlDbType.Int)
            };
            paramerter[0].Value = loginid;

            var rowCount = this.mRepo.GetDbContext.Database.ExecuteSqlCommand("update ChartMessages set Status=1 where ToUserId=@ToUserId ", paramerter);
            return sources;
            //return this.GetEntitysPage<Message>(sql, "Id", "*", "OccDate desc", ESortType.DESC, pageIndex, 20, ref pageCount, ref totalCount);
        }

        


        /// <summary>
        /// 发送消息  群发
        /// </summary>
        /// <param name="messgaeType"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="toUserType"></param>
        public void InstatMessage(int messgaeType, string title, string content, int toUserType)
        {
            /*
             @toUserType nvarchar(50),--发送对象
             * @Title nvarchar(100),--消息标题
             * @content nvarchar(500),--消息内容
             * @msgType int --消息类型
             */
            string procName = "sp_sendMessage";
            System.Data.SqlClient.SqlParameter[] paramerter = new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@toUserType",System.Data.SqlDbType.Int),
            new System.Data.SqlClient.SqlParameter("@Title",System.Data.SqlDbType.NVarChar),
            new System.Data.SqlClient.SqlParameter("@content",System.Data.SqlDbType.NVarChar),
            new System.Data.SqlClient.SqlParameter("@msgType",System.Data.SqlDbType.Int)
            };
            paramerter[0].Value = toUserType;
            paramerter[1].Value = title;
            paramerter[2].Value = content;
            paramerter[3].Value = messgaeType;

            this.ExProcNoReader(procName, paramerter);
        }

        /// <summary>
        /// 获取某用户的未消息内容
        /// </summary>
        /// <param name="userid"></param>
        public List<Message> RefUserMessage(string userid)
        {
            string sql = "select * from [Messages] where Status=0 and ToUserId=" + userid + " ";
            sql = "(" + sql + ") as t1";
            int pageCount = 0;
            int totalCount = 0;
            return this.GetEntitysPage<Message>(sql, "OccDate","*", "OccDate desc", ESortType.DESC, 1, 10, ref pageCount, ref totalCount);
        }

         /// <summary>
        /// 修改消息为已读
        /// </summary>
        /// <param name="userid"></param>
        public void UpdateReadMessgae(string ids)
        {
            string sql = "update Messages set Status=1 where id in(" + ids + ")";
            this.mRepo.GetDbContext.Database.ExecuteSqlCommand(sql);
        }
    }
}
