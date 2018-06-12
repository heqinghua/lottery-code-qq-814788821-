using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Data;

namespace Ytg.Service
{
    /// <summary>
    /// 用户余额
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    
    public class SysUserBalanceService : CrudService<SysUserBalance>, ISysUserBalanceService
    {
        readonly IHasher mHasher;

        readonly ISysUserService mSysUserService;//用户名

        log4net.ILog log = null;

        public SysUserBalanceService(IRepo<SysUserBalance> repo, 
            IHasher hasher, 
            ISysUserService sysUserService)
            : base(repo)
        {
            this.mHasher = hasher;
            this.mHasher.SaltSize = 8;
            this.mSysUserService = sysUserService;
            log = log4net.LogManager.GetLogger("errorLog");
        }


        public override SysUserBalance Create(SysUserBalance item)
        {
            if (!string.IsNullOrEmpty(item.Pwd))
                item.Pwd = mHasher.Encrypt(item.Pwd);
            return base.Create(item);
        }

        /// <summary>
        /// 获取用户余额
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public SysUserBalance GetUserBalance(int uid)
        {
            
            return this.Where(item => item.UserId == uid ).FirstOrDefault();
        }

        int UpdateUserBalance(int uid, decimal nowMonery)
        {
            try
            {
                var ub = this.Where(item => item.UserId == uid).FirstOrDefault();
                if (null == ub)
                    return -998;
                if (ub.Status != 0)
                    return -999;

                ub.UserAmt = nowMonery;
                ub.OpTime = DateTime.Now;

                return 1;
            }
            catch (Exception ex)
            {
                log.Error("ISysUserBalanceService.UpdateUserBalance", ex);
            }

            return -1;
        }

        public bool ChangeUserBalance(int uid, decimal tradeAmt)
        {
            try
            {
                var ub = this.Where(item => item.UserId == uid).FirstOrDefault();
                if (null == ub) return false;
                ub.UserAmt -= tradeAmt;
                ub.OpTime = DateTime.Now;
                this.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("ISysUserBalanceService.ChangeUserBalance", ex);
            }
            return false;
        }

        /// <summary>
        /// 增加用户余额
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="nowMonery"></param>
        /// <returns></returns>
        public int AddUserBalance(int uid, decimal nowMonery,ref decimal oldMonery)
        {
            try
            {
                var ub = this.Where(item => item.UserId == uid).FirstOrDefault();
                if (null == ub)
                    return -998;
                if (ub.Status != 0)
                    return -999;
                oldMonery = ub.UserAmt;
                ub.UserAmt += nowMonery;
                ub.OpTime = DateTime.Now;

                return 1;
            }
            catch (Exception ex)
            {
                log.Error("ISysUserBalanceService.AddUserBalance", ex);
            }

            return -1;
        }

        /// <summary>
        /// 修改资金密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool UpdateUserBalancePwd(int uid, string oldPassword, string newPassword)
        {
            var userBalance = this.GetAll().Where(item => item.UserId == uid).FirstOrDefault();
            if (null == userBalance)
                return false;
            bool iscompled=this.mHasher.CompareStringToHash(oldPassword, userBalance.Pwd);
            log.Info("iscompled="+ iscompled + "UpdateUserBalancePwd---oldPassword=" + oldPassword+ "   userBalance.Pwd="+ userBalance.Pwd);
            if (iscompled)
            {
                //修改用户密码
                userBalance.Pwd = this.mHasher.Encrypt(newPassword);
                this.Save();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 后台修改资金密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public bool UpdateManUserBalancePwd(int uid, string newPwd)
        {
            var userBalance = this.GetAll().Where(item => item.UserId == uid).FirstOrDefault();
            if (null == userBalance)
                return false;

            //修改用户密码
            userBalance.Pwd = this.mHasher.Encrypt(newPwd);
            this.Save();
            return true;


        }
    


        /// <summary>
        /// 初始化资金密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool InintUserBalancePwd(int uid, string newPassword)
        {
            var userBalance = this.GetAll().Where(item => item.UserId == uid).FirstOrDefault();
            if (null == userBalance)
                return false;

            //修改用户密码
            userBalance.Pwd = this.mHasher.Encrypt(newPassword);
            this.Save();
            return true;
        }

        /// <summary>
        /// 验证资金密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VdUserBalancePwd(int uid, string password)
        {
            var userBalance = this.GetNoTracking().Where(item => item.UserId == uid).FirstOrDefault();
            if (userBalance == null)
                return false;
            return this.mHasher.CompareStringToHash(password, userBalance.Pwd);
        }

        /// <summary>
        /// 修改资金状态
        /// </summary>
        /// <returns></returns>
        public bool ModifyBalanceStatus(int userId)
        {
            SysUserBalance mSysUserBalance = this.Where(m => m.UserId == userId).FirstOrDefault();
            if (mSysUserBalance != null)
            {
                if (mSysUserBalance.Status == 1)
                    mSysUserBalance.Status = 0;
                else
                    mSysUserBalance.Status = 1;
                this.Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool Frozen(int uid)
        {
            var entity = this.Where(m => m.UserId == uid).FirstOrDefault();
            entity.Status = 1;
            this.Save();
            return true;
        }

        public bool UnFrozen(int uid)
        {
            var entity = this.Where(m => m.UserId == uid).FirstOrDefault();
            entity.Status = 0;
            this.Save();
            return true;
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="type">1 为用户id 0 为用户账号</param>
        /// <param name="codeValue"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Recharge(int type, string codeValue, decimal value)
        {
            int uid = -1;
            if (type == 1)
            {
                if (!int.TryParse(codeValue, out uid))
                    return false;
            }
            else if (type == 0)
            {
                //根据用户名获取用户id
                uid = mSysUserService.Where(c => c.Code == codeValue).Select(c => c.Id).FirstOrDefault();
            }

            var ub = this.Where(item => item.UserId == uid).FirstOrDefault();
            if (null == ub) return false;
            ub.UserAmt += value;
            ub.OpTime = DateTime.Now;
            this.Save();
            return true;
        }

        /// <summary>
        /// 通过存储过程修改用户余额
        /// </summary>
        /// <param name="balanceDetails"></param>
        /// <returns></returns>
        public int UpdateUserBalance(SysUserBalanceDetail balanceDetails, decimal changeMonery)
        {
            int rowCount = 0;
            try
            {
                rowCount = PrivateUpdateUserBalance(balanceDetails, changeMonery);
                //throw new Exception();
            }
            catch (Exception ex)
            {
                //一旦发生事务错误，重新提交
                int whileCount = 0;
                while (true)
                {
                    string msg = balanceDetails.RelevanceNo + " 第" + whileCount + "次执行事务";
                    Console.WriteLine(msg);
                    
                    try
                    {
                        rowCount = PrivateUpdateUserBalance(balanceDetails, changeMonery);
                        break;
                    }
                    catch (Exception ex1)
                    {
                        Console.WriteLine("事务提交失败，尝试重新提交 " + ex1.Message);
                    }
                    System.Threading.Thread.Sleep(1000);//一秒后重新提交
                    whileCount++;
                    if (whileCount > 10)
                        break;

                }
                Console.WriteLine(balanceDetails.RelevanceNo + "事务提交Exception " + ex.Message);
            }

            return rowCount;

        }

        /// <summary>
        /// 通过存储过程修改用户余额信息
        /// </summary>
        /// <param name="balanceDetails"></param>
        /// <param name="changeMonery"></param>
        /// <returns></returns>
        private int PrivateUpdateUserBalance(SysUserBalanceDetail balanceDetails, decimal changeMonery)
        {
            string procName = "sp_ChangeUserBalances";

            var p = new System.Data.SqlClient.SqlParameter("@uid", System.Data.DbType.Int32);
            p.Value = balanceDetails.UserId;

            var p2 = new System.Data.SqlClient.SqlParameter("@TradeType", System.Data.DbType.Int32);
            p2.Value = balanceDetails.TradeType;

            var p3 = new System.Data.SqlClient.SqlParameter("@SerialNo", System.Data.DbType.String);
            p3.Value = balanceDetails.SerialNo;

            var p4 = new System.Data.SqlClient.SqlParameter("@relevanceNo", System.Data.DbType.String);
            p4.Value = balanceDetails.RelevanceNo;

            var p5 = new System.Data.SqlClient.SqlParameter("@BankId", System.Data.DbType.Int32);
            p5.Value = balanceDetails.BankId == null ? -1 : balanceDetails.BankId;

            var p6 = new System.Data.SqlClient.SqlParameter("@sumMonery", System.Data.DbType.Decimal);
            p6.Value = changeMonery; //变化金额


            var amt = this.ExProc<System.Int32?>(procName, p, p2, p3, p4, p5, p6);
            if (amt.FirstOrDefault() == null)
                return -1;
            return amt.FirstOrDefault().Value;

        }

        /// <summary>
        /// 验证是否允许提款  状态  0 验证通过 1为用户余额不够本次提款 3 流水未达到提款要求
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="outMonery"></param>
        /// <returns></returns>
        public int HasMention(int uid, decimal outMonery)
        {
            string procName = "sp_HasMention";

            var p = new System.Data.SqlClient.SqlParameter("@uid", System.Data.DbType.Int32);
            p.Value = uid;

            //var p1 = fc.GetDbParameter("@tradeAmt", System.Data.DbType.Decimal);
            //p1.Value = balanceDetails.TradeAmt;

            var p2 = new System.Data.SqlClient.SqlParameter("@outMonery", System.Data.DbType.Decimal);
            p2.Value = outMonery;

            var p3 = new System.Data.SqlClient.SqlParameter("@result", System.Data.DbType.Int32);
            p3.Direction = System.Data.ParameterDirection.Output;

            this.ExProcNoReader(procName, p, p2, p3);
            object state = p3.Value;
            if (null == state)
                return -1;
            return Convert.ToInt32(state.ToString());
        }


        /// <summary>
        /// 撤销派奖
        /// </summary>
        /// <param name="betCode"></param>
        /// <returns></returns>
        public bool Cannel(string betCode)
        {
            // 投注单号或追号单号  奖金派送 = 7,   撤销派奖 = 11,  撤销返点 = 10, 游戏返点 = 6,
            string sql = "select * from [SysUserBalanceDetails] where RelevanceNo='" + betCode + "' and tradeType in(7,6)";
            var result = this.GetSqlSource<SysUserBalanceDetail>(sql);
            if (result == null || result.Count < 1)
            {
                return false;
            }
            //撤销派奖
            foreach (var item in result)
            {
                if (item.TradeType == TradeType.奖金派送)
                {
                    item.TradeType = TradeType.撤销派奖;
                    //构建撤销派奖
                    this.UpdateUserBalance(item, (0 - item.TradeAmt));
                }
                else if (item.TradeType == TradeType.游戏返点)
                {
                    item.TradeType = TradeType.撤销返点;
                    //构建撤销派奖
                    this.UpdateUserBalance(item, (0 - item.TradeAmt));
                }
            }
            return true;
        }

    }
}
