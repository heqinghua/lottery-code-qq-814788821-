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
    /// 用户余额
    /// </summary>
    [ServiceContract]
    public interface ISysUserBalanceService : ICrudService<SysUserBalance>
    {

        /// <summary>
        /// 根据用户获取余额数据
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        SysUserBalance GetUserBalance(int uid);

        /// <summary>
        /// 通过存储过程修改用户余额
        /// </summary>
        /// <param name="balanceDetails"></param>
        /// <param name="changeMonery">变化金额</param>
        /// <returns></returns>
        int UpdateUserBalance(SysUserBalanceDetail balanceDetails, decimal changeMonery);

        ///// <summary>
        ///// 修改用户余额
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="nowMonery"></param>
        ///// <returns>大于0修改正常，-999为钱被禁用,-998为没有该条记录</returns>
        //int UpdateUserBalance(int uid, decimal nowMonery);

        ///// <summary>
        ///// 改变用户余额
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="tradeAmt"></param>
        ///// <returns></returns>
        //bool ChangeUserBalance(int uid, decimal tradeAmt);


        /// <summary>
        /// 修改资金密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool UpdateUserBalancePwd(int uid, string oldPassword, string newPassword);

        /// <summary>
        /// 初始化资金密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool InintUserBalancePwd(int uid, string newPassword);

        /// <summary>
        /// 验证资金密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool VdUserBalancePwd(int uid, string password);

        /// <summary>
        /// 启用资金
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [OperationContract]
        bool UnFrozen(int uid);

        /// <summary>
        /// 禁用资金
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [OperationContract]
        bool Frozen(int uid);

        /// <summary>
        /// 修改资金状态
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        bool ModifyBalanceStatus(int userId);

        /// <summary>
        /// 用户充值
        /// </summary>
        /// <param name="type">类型 0：用户名,1：用户Id</param>
        /// <param name="codeValue">用户标识</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        //[OperationContract]
        //bool Recharge(int type,string codeValue,decimal value);

        /// <summary>
        /// 增加用户余额
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="nowMonery"></param>
        /// <returns></returns>
        //int AddUserBalance(int uid, decimal nowMonery,ref decimal oldMonery);

        /// <summary>
        /// 验证是否允许提款
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="outMonery"></param>
        /// <returns></returns>
        int HasMention(int uid, decimal outMonery);

        /// <summary>
        /// 撤销派奖
        /// </summary>
        /// <param name="betCode"></param>
        /// <returns></returns>
        bool Cannel(string betCode);

         /// <summary>
        /// 后台修改资金密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        bool UpdateManUserBalancePwd(int uid, string newPwd);
    }

}
