using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Comm;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 用户
    /// </summary>
    [ServiceContract]
    public interface ISysUserService : ICrudService<SysUser>
    {

        /// <summary>
        /// 修改用户密码根据id,直接修改密码，必须验证资金密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="password"></param>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        bool UpdatePassword(int uid, string password);

        /// <summary>
        /// 增加用户登录次数统计
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        bool AppendUserLoginCount(string code);

        /// <summary>
        /// 删除会员数据
        /// </summary>
        /// <param name="id">会员ID</param>
        /// <returns></returns>
        int delCustomer(int id);

        /// <summary>
        /// 指定父用户的父用户是否为指定值
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="parentparentid"></param>
        /// <returns></returns>
        bool HasParentIsParentid(int parentid, int parentparentid);

        /// <summary>
        /// 是否唯一
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsUnique(string login);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        void ChangePassword(int id, string password);

        SysUser Get(string login, string password);

        SysUser Get(int uid, string password);

        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        SysUser GetAm(string login, string password);

        /// <summary>
        /// 是否存在指定code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        SysUser Get(string code);

        /// <summary>
        /// 创建用户 wcf
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool CreateUser(SysUser item);


        /// <summary>
        /// 查询 用户
        /// </summary>
        /// <param name="code"></param>
        /// <param name="nickName"></param>
        /// <param name="isDel"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<SysUser> GetUsers(string code, string nickName, bool? isDel, int? userType, int pageIndex, ref int totalCount);

        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SysUser GetForId(int id);

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool UpdateItem(int id, SysUser item);


        /// <summary>
        /// 根据用户id获取用户返点值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        double? GetUserRebate(int id);

        /// <summary>
        /// 根据用户id修改用户返点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rebate"></param>
        /// <returns></returns>
        bool UpdateUserRebate(int uid, double rebate);

        /// <summary>
        /// 根据用户id修改用户自动注册返点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rebate"></param>
        /// <returns></returns>
        bool UpdateAutoRegRebate(int uid, double rebate);

        bool UpdateRecharge(int uid, bool IsRecharge);


        /// <summary>
        /// 获取团队余额
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        decimal? GroupUserAmt(int uid);

        /// <summary>
        /// 获取用户当天交易额
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        decimal? GetDailyBusinessTransaction(int uid);

        /// <summary>
        /// 获取团队销量
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        decimal? GroupUserSales(int uid, DateTime beginDate, DateTime endDate);

        /// <summary>
        /// 获取下级最大返点值
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        double? GetChildMaxRebate(int uid);

        /// <summary>
        /// 获取上级最大返点值
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        double? GetParentMaxRebate(int uid);


        /// <summary>
        /// 获取子用户列表
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        List<TreeDTO> GetChildrens(int uid, bool hasCustomer, bool hasParent);


        /// <summary>
        /// 获取第一下级用户列表
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="code">登录名</param>
        /// <param name="startmonery">开始剩余余额</param>
        /// <param name="endmonery">结束剩余余额</param>
        /// <param name="order">排序字段</param>
        /// <param name="orderType">拍讯类型</param>
        /// <param name="level">用户级别</param>
        /// <param name="startRemb">返点开始</param>
        /// <param name="endRemb">返点结束</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        List<SysUserDTO> GetChildrens(int? id, bool isSelf, string code, decimal startmonery, decimal endmonery, string order, int orderType, int level, double startRemb, double endRemb, int playType, int pageIndex, ref int totalCount, ref int pageCount);

        /// <summary>
        /// 获取用户的所有父级
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        List<SysUser> GetParentUsers(int userid);

        /// <summary>
        /// 修改用户问候语
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="greetings"></param>
        /// <returns></returns>
        bool UpdateUserGreetings(int uid, string greetings);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="password"></param>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        bool UpdatePassword(int uid, string password, string oldPassword);

        [OperationContract]
        bool Disable(int userId);

        [OperationContract]
        bool Enable(int userId);


        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        bool ModifyUserStatus(int userId);

        /// <summary>
        /// 后台WCF 会员资料
        /// </summary>
        /// <param name="code"></param>
        /// <param name="userType"></param>
        /// <param name="isDelete"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<SysUserVM> SelectBy(string code, int userType, string nickName, int isDelete, double? beginQuo, double? endQuo, int? parentId, int pageIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 获取管理用户信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        [OperationContract]
        List<ManagerUserDTO> GetcUsers(string code, int isdel, int roleid);


        bool LockUserCards(int uid);

        bool UnLockUserCards(int uid);

        /// <summary>
        /// 获取用户锁定次数
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        int GetUserLockUserCount(int uid);

        /// <summary>
        /// 获取自助设置值
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        double GetAutoRegRebate(int uid);

        /// <summary>
        /// 设置用户提款最低投注金额
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="monery"></param>
        /// <returns></returns>
        int UpdateUserMinMinBettingMoney(int uid, decimal monery);

        /// <summary>
        /// 设置用户投注限制为0
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        int DefaultUserMinMinBettingMoney(int uid);

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <returns></returns>
        List<SysUser> GetLineUsers(int pageindex, int pageSize, ref int totalCount);


        /// <summary>
        /// 获取用户信息以及用户返点信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        BettUserDto GetUserAndZiJin(int userId);


        /// <summary>
        /// 解锁银行卡 后台
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        bool ManagerUnLockUserCards(int uid);

        /// <summary>
        /// 需输入电话等信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool InputMobile(int userId);

        /// <summary>
        /// 无需输入电话等信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool InputNotMobile(int userId);

        /// <summary>
        /// 设置为测试账号
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        bool SetTest(int userid);

        /// <summary>
        /// 设置为非测试账号
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        bool SetUnTest(int userid);
    }
}
