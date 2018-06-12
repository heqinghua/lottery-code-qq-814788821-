using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Core.Service;

namespace Ytg.Core
{
    public interface ISysUserSessionService : ICrudService<UserSession>
    {
        /// <summary>
        /// 根据用户id获取用户登录信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        UserSession GetUserId(int userid);



        /// <summary>
        /// 更新用户session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        int UpdateInsertUserSession(UserSession session);


        /// <summary>
        /// 获取各端用户总数
        /// </summary>
        /// <returns></returns>
        List<UserSessionMangerDto> GetUserSessionManager();
        
    }
}
