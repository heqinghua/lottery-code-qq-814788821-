using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
    public class SysUserService : CrudService<SysUser>, ISysUserService
    {
        protected readonly IHasher mHasher;

        public SysUserService(IRepo<SysUser> repo, IHasher hasher)
            : base(repo)
        {
            this.mHasher = hasher;
            this.mHasher.SaltSize = 8;
        }

        public override SysUser Create(SysUser item)
        {
            item.Password = mHasher.Encrypt(item.Password);
            return base.Create(item);
        }

        public virtual bool IsUnique(string login)
        {
            return this.mRepo.Where(o => o.Code == login).Count() == 0;
        }


        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        public virtual void ChangePassword(int id, string password)
        {
            mRepo.Get(id).Password = mHasher.Encrypt(password);
            mRepo.Save();
        }

        public virtual SysUser Get(int uid,string password) {
            var user = mRepo.Where(o => o.Id == uid && o.UserType != UserType.Manager).FirstOrDefault();
            if (user == null || !mHasher.CompareStringToHash(password, user.Password)) return null;
            return user;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="password"></param>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        public bool UpdatePassword(int uid, string password, string oldPassword)
        {
            var user = this.Get(uid);
            if (user == null)
                return false;
            if (!mHasher.CompareStringToHash(oldPassword, user.Password))
                return false;
            user.Password = this.mHasher.Encrypt(password);
            this.Save();
            return true;
        }

        /// <summary>
        /// 修改用户密码根据id,直接修改密码，必须验证资金密码
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="password"></param>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        public bool UpdatePassword(int uid, string password)
        {
            var user = this.Get(uid);
            if (user == null)
                return false;
            user.Password = this.mHasher.Encrypt(password);
            this.Save();
            return true;
        }

        public virtual SysUser Get(string login, string password)
        {

            var user = mRepo.Where(o => o.Code == login  && o.UserType!= UserType.Manager).FirstOrDefault();
            if (user == null || !mHasher.CompareStringToHash(password, user.Password)) return null;
            return user;
        }


        public IEnumerable<SysUser> GetUsers(string code, string nickName, bool? isDel, int? userType, int pageIndex, ref int totalCount)
        {
            var source = this.GetAll();
            if (!string.IsNullOrEmpty(code))
                source = source.Where(item => code.IndexOf(item.Code) != -1);
            if (!string.IsNullOrEmpty(nickName))
                source = source.Where(item => nickName.IndexOf(item.Code) != -1);
            if (isDel.HasValue)
                source = source.Where(item => item.IsDelete == isDel.Value);
            if (userType.HasValue)
            {
                UserType tp = UserType.General;
                if (userType.Value == 0)
                    tp = UserType.General;
                else if (userType.Value == 1)
                    tp = UserType.Proxy;
                else if (userType.Value == 3)
                    tp = UserType.BasicProy;
                source = source.Where(item => item.UserType == tp);
            }
            else
            {
                source = source.Where(item => item.UserType != UserType.Manager && item.UserType!=UserType.Customer);
            }
            totalCount = source.Count();
            return source.Page(pageIndex, AppGlobal.ManagerDataPageSize);
        }

        /// <summary>
        /// 获取管理员用户信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public List<ManagerUserDTO> GetcUsers(string code, int isdel, int roleid)
        {
            string sql = "select SysYtgUser.Id,SysYtgUser.Code,SysYtgUser.RoleId,SysYtgUser.OccDate,SysYtgUser.IsDelete,SysRoles.RoleName from SysYtgUser " +
                       "inner join SysRoles on SysYtgUser.RoleId=SysRoles.id " +
                       "where UserType=2 ";
            
            if (!string.IsNullOrEmpty(code))
                sql += " and Code='"+code+"'";        
            if (roleid != -1)
                sql += " and RoleId="+roleid+"";
            if (isdel != -1)
                sql += " and IsDelete="+isdel+"";

            return this.GetSqlSource<ManagerUserDTO>(sql);
        }


        public bool CreateUser(SysUser item)
        {
            if (null == item)
                return false;
            this.Create(item);
            this.Save();
            //初始化用户余额
            ISysUserBalanceService userbalance = IoC.Resolve<ISysUserBalanceService>();
            userbalance.Create(new SysUserBalance()
            {
                IsOpenVip=false,
                OccDate=DateTime.Now,
                Status=0,
                UserAmt=0,
                UserId=item.Id,
            });
            userbalance.Save();

            ISysQuotaService quotaService = IoC.Resolve<ISysQuotaService>();
            if (item.UserType == UserType.BasicProy)//总代配合初始化
                quotaService.InintPrxyQuota(item);
            else if (item.UserType == UserType.Proxy)//普通代理配合设置基本上多为零
                quotaService.InintPrxyQuota(item, 0);

            return true;
        }


        public SysUser GetForId(int id)
        {
            return this.Get(id);
        }

        public bool UpdateItem(int id, SysUser item)
        {
            if (null == item)
                return false;
            var info = this.Get(id);
            info.Password = item.Password;
            info.UserType = item.UserType;
            info.Rebate = item.Rebate;
            info.IsDelete = item.IsDelete;
            info.PlayType = item.PlayType;
            this.Save();
            return true;
        }


        /// <summary>
        /// 获取用户返点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public double? GetUserRebate(int id)
        {
            var item = this.Get(id);
            if (item == null)
                return null;
            return item.Rebate;
        }

        /// <summary>
        /// 获取第一下级用户列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<SysUserDTO> GetChildrens(int? id, bool isSelf, string code, decimal startmonery, decimal endmonery, string order, int orderType, int level, double startRemb, double endRemb, int playType, int pageIndex, ref int totalCount, ref int pageCount)
        {

            string sql =@"select u.*,ub.UserAmt,ub.Status 
 from SysYtgUser as u
 left join SysUserBalances as ub
 on u.Id=ub.UserId
 where 1=1";

            bool isFig=false;
            if (!string.IsNullOrEmpty(code)){
                sql += string.Format(" and u.Code='{0}'", Utils.ChkSQL(code));
                isFig=true;
            }
            if (startmonery != -1 && endmonery > startmonery){
                sql += string.Format(" and ub.UserAmt between {0} and {1}", startmonery, endmonery);
                isFig=true;
            }
            if (level != -1){
                sql += string.Format(" and u.UserType={0}", level);
                isFig=true;
            }
            if (startRemb != -1 && endRemb < startRemb){
                sql += string.Format(" and u.Rebate between {0} and {1}", endRemb,startRemb);
                isFig=true;
            }
            if (playType != -1)//奖金组 0 为1800 1 为1700
            {
                sql += string.Format(" and u.PlayType={0}", playType);
                isFig=true;
            }
            if (!isFig)
            {
                sql += string.Format(" and  ParentId={0}", id);
            }
            else
            {
                sql += string.Format(" and UserId in(SELECT * FROM f_SysYtgUser_GetChildren({0}))",id);
            }

            string orderName = string.IsNullOrEmpty(order)?" Code":order;
           

            sql = "( " + sql + " ) as t1";
            var list = this.GetEntitysPage<SysUserDTO>(sql,"Id", "*", orderName, orderType == 1 ? ESortType.DESC : ESortType.ASC, pageIndex, 20, ref pageCount, ref totalCount);
            if (!isSelf)
            {
                var self = this.GetSqlSource<SysUserDTO>(string.Format(@"select u.*,ub.UserAmt,ub.Status from SysYtgUser as u
                        left join SysUserBalances as ub
                        on u.Id=ub.UserId
                        WHERE u.Id={0}", id.Value)).FirstOrDefault();
                if (list != null && self != null) list.Insert(0, self);
                if (list == null && self != null)
                {
                    list = new List<SysUserDTO>();
                    list.Add(self);
                }
            }
            return list;
        }


        /// <summary>
        /// 根据登录名获取用户信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public SysUser Get(string code)
        {
            return this.Where(item => item.Code == code).FirstOrDefault();
        }


        /// <summary>
        /// 修改用户返点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rebate"></param>
        /// <returns></returns>
        public bool UpdateUserRebate(int uid, double rebate)
        {
            var sysUser = this.Get(uid);
            if (null == sysUser)
                return false;

            sysUser.Rebate = rebate;
            this.Save();
            return true;
        }

        /// <summary>
        /// 修改用户问候语
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="greetings"></param>
        /// <returns></returns>
        public bool UpdateUserGreetings(int uid, string greetings)
        {
            var user = this.Get(uid);
            if (null == user)
                return false;
            user.Greetings = greetings;
            this.Save();
            return true;
        }

        /// <summary>
        /// 根据用户id修改用户自动注册返点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rebate"></param>
        /// <returns></returns>
        public bool UpdateAutoRegRebate(int uid, double rebate)
        {
            var sysUser = this.Get(uid);
            if (null == sysUser)
                return false;
            sysUser.AutoRegistRebate = rebate;
            this.Save();
            return true;
        }

        /// <summary>
        /// 获取用户当前
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public double GetAutoRegRebate(int uid)
        {
            return this.GetAll().Where(c => c.Id == uid).Select(c => c.AutoRegistRebate).FirstOrDefault();
        }

        /// <summary>
        /// 修改开启和取消充值功能
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="IsRecharge"></param>
        /// <returns></returns>
        public bool UpdateRecharge(int uid, bool IsRecharge)
        {
            var sysUser = this.Get(uid);
            if (null == sysUser)
                return false;

            sysUser.IsRecharge = IsRecharge;
            this.Save();
            return true;
        }


        /// <summary>
        /// 获取用户团队余额
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public System.Decimal? GroupUserAmt(int uid)
        {
            string procName = "GroupUserAmt";
            var fc = IoC.Resolve<IDbContextFactory>();
            var p = fc.GetDbParameter("@uid", System.Data.DbType.Int32);
            p.Value = uid;

            var amt = this.ExProc<System.Decimal?>(procName, p);
            return amt.FirstOrDefault();
        }

        /// <summary>
        /// 获取用户当天交易额
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public System.Decimal? GetDailyBusinessTransaction(int uid)
        {
            string procName = "GetDailyBusinessTransaction";
            var fc = IoC.Resolve<IDbContextFactory>();
            var p = fc.GetDbParameter("@uid", System.Data.DbType.Int32);
            p.Value = uid;

            var amt = this.ExProc<System.Decimal?>(procName, p);
            return amt.FirstOrDefault();
        }

        //删除会员
        public int delCustomer(int id)
        {
            int result = 0;
            var fc = IoC.Resolve<IDbContextFactory>();
            var p = fc.GetDbParameter("@userId", System.Data.DbType.Int32);
            p.Value = id;

            var p1 = fc.GetDbParameter("@result", System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
            p1.Value = result;

            
            this.ExProc<object>("sp_delCustomer", p, p1);

            return Convert.ToInt32(p1.Value);
        }

        /// <summary>
        /// 获取团队销量
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public decimal? GroupUserSales(int uid, DateTime beginDate, DateTime endDate)
        {
            string procName = "UserGroupSales";
            var fc = IoC.Resolve<IDbContextFactory>();
            var p = fc.GetDbParameter("@uid", System.Data.DbType.Int32);
            p.Value = uid;

            var p1 = fc.GetDbParameter("@beginDate", System.Data.DbType.DateTime);
            p1.Value = beginDate;

            var p2 = fc.GetDbParameter("@endDate", System.Data.DbType.DateTime);
            p2.Value = endDate;

            var amt = this.ExProc<System.Decimal?>(procName, p, p1, p2);
            return amt.FirstOrDefault();
        }

        /// <summary>
        /// 获取用户下的所有子用户
        /// 这里要看要不要把客服、上级的用户给加载出来
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>

        public List<TreeDTO> GetChildrens(int uid, bool hasCustomer, bool hasParent)
        {
            var source = this.Where(item => item.ParentId == uid);

            List<TreeDTO> result = new List<TreeDTO>();
            foreach (var item in source)
            {
                TreeDTO dto = new TreeDTO();
                dto.ChildrensCount = this.Where(c => c.ParentId == item.Id).Count();
                dto.HasChildrens = dto.ChildrensCount > 0;
                dto.Id = item.Id;
                dto.Title = item.Code;

                result.Add(dto);
            }
            //
            if (hasParent)
            {
                var curr = this.Get(uid);
                if (curr != null && curr.ParentId.HasValue)
                {
                    var par = this.Get(curr.ParentId.Value);
                    if (par != null)
                    {
                        result.Add(new TreeDTO
                        {
                            HasChildrens = false,
                            Id = par.Id,
                            Title = string.Format("上级 {0}", par.Code)
                        });
                    }
                }
            }
            return result;
        }



        public double? GetChildMaxRebate(int uid)
        {
            var result = this.GetAll().Where(item => item.ParentId == uid);
            double maxValue = -1;
            if (result.Any())
                maxValue = result.Max(u => u.Rebate);
            return maxValue;
        }


        /// <summary>
        /// 获取上级最大返点值
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public double? GetParentMaxRebate(int uid)
        {
            var c = this.Where(item => item.Id == uid).FirstOrDefault();
            if (null == c)
                return null;
            var result = this.Where(p => p.Id == c.ParentId);
            if (result.Any())
                return result.Max(u => u.Rebate);
            return -1;
        }


        /// <summary>
        /// 获取当前用户的所有上级用户
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<SysUser> GetParentUsers(int userid)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" with ");
            sqlBuilder.Append(" my1 as ");
            sqlBuilder.Append(" ( ");
            sqlBuilder.Append(string.Format(" select * from SysYtgUser where id = {0}  ", userid));
            sqlBuilder.Append(" union all select SysYtgUser.* from my1,SysYtgUser where my1.ParentId = SysYtgUser.id ");
            sqlBuilder.Append(" ) ");
            sqlBuilder.Append(" select * from  my1 ");

            return this.GetSqlSource<SysUser>(sqlBuilder.ToString());
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <returns></returns>
        public bool ModifyUserStatus(int userId)
        {
            SysUser mSysUser = this.Get(userId);
            if (mSysUser != null)
            {
                if (mSysUser.IsDelete == true)
                    mSysUser.IsDelete = false;
                else
                    mSysUser.IsDelete = true;
                this.Save();
                return true;
            }
            return false;
        }

        public bool Enable(int userId)
        {
            var entity = this.Get(userId);
            entity.IsDelete = false;
            this.Save();
            return true;
        }

        public bool Disable(int userId)
        {
            var entity = this.Get(userId);
            entity.IsDelete = true;
            this.Save();
            return true;
        }

        /// <summary>
        /// 需输入电话等信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool InputMobile(int userId)
        {
            var entity = this.Get(userId);
            entity.Head = 1;
            this.Save();
            return true;
        }

        /// <summary>
        /// 无需输入电话等信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool InputNotMobile(int userId)
        {
            var entity = this.Get(userId);
            entity.Head = 0;
            this.Save();
            return true;
        }

        /// <summary>
        /// 设置为测试账号
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool SetTest(int userid) {
            var entity = this.Get(userid);
            entity.Sex ="1";
            this.Save();
            return true;
        }

        /// <summary>
        /// 设置为非测试账号
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool SetUnTest(int userid)
        {
            var entity = this.Get(userid);
            entity.Sex = "0";
            this.Save();
            return true;
        }



        /// <summary>
        /// 后台WCF 会员资料
        /// </summary>
        /// <param name="code"></param>
        /// <param name="userType"></param>
        /// <param name="isDelete"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<SysUserVM> SelectBy(string code, int userType, string nickName, int isDelete, double? beginQuo, double? endQuo, int? parentId, int pageIndex, int pageSize, ref int totalCount)
        {
            #region oldCode

            #endregion
            string sql = @"SELECT u.Head,u.Qq,u.MoTel,u.PlayType,u.Id,u.Code,u.Sex,u.NikeName,u.UserType,Convert(varchar(20),b.UserAmt) UserAmt,u.OccDate,u.LastLoginTime,
	IsNull(u.LoginCount,0) LoginCount,u.IsDelete,Convert(varchar(20),u.Rebate) Rebate,u.IsRecharge,IsNull(b.Status,0) Status,IsLogin
FROM dbo.SysYtgUser u
LEFT JOIN dbo.SysUserBalances b ON b.UserId=u.Id where 1=1";

            if (!string.IsNullOrEmpty(code))
            {
                sql += " and u.Code like '%" + code + "%'";
            }
            if (userType != -1)
            {
                sql += " and u.UserType=" + userType;
            }
            if (!string.IsNullOrEmpty(nickName))
            {
                sql += " and u.NikeName like '%" + userType + "%'";
            }
            if (isDelete != -1)
            {
                sql += " and u.IsDelete=" + isDelete;
            }

            if (beginQuo != null && endQuo != null)
            {
                sql += string.Format(" and u.Rebate between {0} and {1}", beginQuo, endQuo);
            }//返点


            if (parentId == null)
            {
                sql += " and u.ParentId is null";
            }
            else if (parentId.Value != -1)
            {
                sql += " and u.ParentId=" + parentId.Value+" or u.id="+parentId.Value;
            } 
            sql = "(" + sql + ") as t1";
            int pageCount = 0;
            return this.GetEntitysPage<SysUserVM>(sql, "Id", "*", "Id", ESortType.ASC, pageIndex, pageSize, ref pageCount, ref totalCount);
        }


        public SysUser GetAm(string login, string password)
        {
            var user = mRepo.Where(o => o.Code == login && o.IsDelete == false && o.UserType == UserType.Manager).FirstOrDefault();
            if (user == null || !mHasher.CompareStringToHash(password, user.Password)) return null;
            return user;
        }


        public bool LockUserCards(int uid)
        {
            var user=this.Get(uid);
            if (null == user)
                return false;
            user.UserLockCount++;
            user.IsLockCards = true;
            this.Save();
            return true;
        }

        /// <summary>
        /// 解除银行卡锁定
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool UnLockUserCards(int uid)
        {
            var item = this.Get(uid);
            if (null == item || item.UserLockCount >= 2)
                return false;
            item.IsLockCards = false;
            this.Save();
            return true;
        }


        /// <summary>
        /// 解锁银行卡 后台
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool ManagerUnLockUserCards(int uid)
        {
            var item = this.Get(uid);
            if (null == item )
                return false;
            item.IsLockCards = false;
            this.Save();
            return true;
        }


         int ISysUserService.GetUserLockUserCount(int uid)
        {
            var user = this.Get(uid);
            if (user == null || user.UserLockCount >= 2)
                return 2;
            return 0;
        }

        /// <summary>
        /// 指定父用户的父用户是否为指定值
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="parentparentid"></param>
        /// <returns></returns>
        public bool HasParentIsParentid(int parentid, int parentparentid)
        {
            return this.Where(x => x.Id == parentid && x.ParentId == parentparentid).FirstOrDefault() != null;
        }

        /// <summary>
        /// 设置用户提款最低投注金额
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="monery"></param>
        /// <returns></returns>
        public int UpdateUserMinMinBettingMoney(int uid, decimal monery)
        {
            System.Data.SqlClient.SqlParameter[] dbParams = new System.Data.SqlClient.SqlParameter[]{
                new System.Data.SqlClient.SqlParameter("@MinBettingMoney",System.Data.SqlDbType.Decimal),
                new System.Data.SqlClient.SqlParameter("@id",System.Data.SqlDbType.Int)
            };
            dbParams[0].Value = monery;
            dbParams[1].Value = uid;

            //先获取用户余额及用户充值比例
            string sql = "update SysYtgUser set MinBettingMoney=@MinBettingMoney,MinBettingMoneryTime=getdate() where Id=@id";
            return this.mRepo.GetDbContext.Database.ExecuteSqlCommand(sql, dbParams);
        }

        /// <summary>
        /// 设置用户投注限制为0
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DefaultUserMinMinBettingMoney(int uid)
        {
            System.Data.SqlClient.SqlParameter[] dbParams = new System.Data.SqlClient.SqlParameter[]{
                new System.Data.SqlClient.SqlParameter("@id",System.Data.SqlDbType.Int)
            };
            dbParams[0].Value = uid;
            string sql = "update SysYtgUser set MinBettingMoney=0 where Id=@id";
            return this.mRepo.GetDbContext.Database.ExecuteSqlCommand(sql, dbParams);
        }

        /// <summary>
        /// 增加用户登录次数统计
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool AppendUserLoginCount(string code)
        {
            string sql = "update SysYtgUser set   LoginCount=LoginCount+1 where Code=@Code";
            return this.mRepo.GetDbContext.Database.ExecuteSqlCommand(sql, new System.Data.SqlClient.SqlParameter("@Code", code)) > 0;
        }

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <returns></returns>
        public List<SysUser> GetLineUsers(int pageindex, int pageSize, ref int totalCount)
        {
            string sql = "select * from [SysYtgUser] where IsLogin=1";
            sql = "(" + sql + ")as t1";
            int pageCount=0;
            return this.GetEntitysPage<SysUser>(sql, "LastLoginTime","*", "LastLoginTime DESC", ESortType.DESC, pageindex, pageSize, ref pageCount, ref totalCount);
        }

        /// <summary>
        /// 获取用户信息以及用户返点信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BettUserDto GetUserAndZiJin(int userId)
        {
            string sql = "select sy.id,sy.Code,sy.Rebate,sy.UserType,sy.IsDelete,b.Status,sy.PlayType from [SysYtgUser] as sy inner join  [SysUserBalances] as b on sy.Id=b.UserId where sy.id=" + userId;
            return this.GetSqlSource<BettUserDto>(sql).FirstOrDefault();
        }

      
    }
}
