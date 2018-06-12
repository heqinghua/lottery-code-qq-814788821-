using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 系统账号接口
    /// </summary>
    [ServiceContract]
    public interface ISysAccountService : ICrudService<SysAccount>
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="password"></param>
        [OperationContract]
        bool UpdatePassword(int uid, string password, string oldPassword);

        /// <summary>
        /// 新增账号
        /// </summary>
        /// <param name="sysAccount"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddAccount(SysAccount sysAccount);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        SysAccount Login(string userCode, string password);

        [OperationContract]
        SysAccount Get(int uid);

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool UpdateSysAccount(int uid, SysAccount sysAccount);

        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [OperationContract]
        bool Disable(int uid);

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [OperationContract]
        bool Enable(int uid);

        /// <summary>
        /// 账号唯一验证
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsUnique(string code);


        [OperationContract]
        List<SysAccount> GetSysAccount();

        [OperationContract]
        List<SysAccount> GetSysAccount(int pageIndex, ref int totalCount);
    }
}
