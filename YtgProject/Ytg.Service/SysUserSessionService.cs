using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Core;
using Ytg.Core.Repository;

namespace Ytg.Service
{
    public class SysUserSessionService: CrudService<UserSession>, ISysUserSessionService
    {
        public SysUserSessionService(IRepo<UserSession> repo)
            : base(repo)
        {
        }

        /// <summary>
        /// 获取各端用户总数
        /// </summary>
        /// <returns></returns>
        public List<UserSessionMangerDto> GetUserSessionManager()
        {
            string sql = "select LoginClient,COUNT(LoginClient) as userCount from UserSessions group by LoginClient";
            return this.GetSqlSource<UserSessionMangerDto>(sql);
        }

        public UserSession GetUserId(int userid)
        {
            string sql = "select id,UserId,SessionId,LoginIp,LastUpdateTime,OccDate,LoginClient from UserSessions where UserId=" + userid;

            var fs=  this.GetSqlSource<UserSession>(sql).FirstOrDefault();

            return fs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns>-0 表示不存在session,当前插入 1 更新session</returns>
        public int UpdateInsertUserSession(UserSession session)
        {

            string procName = "sp_ManagerUserSession";

            var p = new System.Data.SqlClient.SqlParameter("@UserId", System.Data.DbType.Int32);
            p.Value = session.UserId;

            var p2 = new System.Data.SqlClient.SqlParameter("@SessionId", System.Data.DbType.String);
            p2.Value = session.SessionId;

            var p3 = new System.Data.SqlClient.SqlParameter("@LoginIp", System.Data.DbType.String);
            p3.Value = session.LoginIp;

            var p5 = new System.Data.SqlClient.SqlParameter("@LoginClient", System.Data.DbType.String);
            p5.Value = session.LoginClient;

            var p4 = new System.Data.SqlClient.SqlParameter("@state", System.Data.DbType.Int32);
            p4.Direction = System.Data.ParameterDirection.Output;


            this.ExProc<System.Decimal?>(procName, p, p2, p3, p4, p5);

            return p4.Value == null ? -1 : Convert.ToInt32(p4.Value.ToString());

        }

    }
}
