using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    public class SysAccountService : CrudService<SysAccount>, ISysAccountService
    {
        protected readonly IHasher mHasher;
        public SysAccountService(IRepo<SysAccount> repo, IHasher hasher)
            : base(repo)
        {
            this.mHasher = hasher;
            this.mHasher.SaltSize = 8;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userCode">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public SysAccount Login(string userCode, string password)
        {
            var user = mRepo.Where(o => o.Code == userCode).FirstOrDefault();
            if (user == null || !mHasher.CompareStringToHash(password, user.PassWord)) 
                return null;
            return user;
        }

        /// <summary>
        /// 新增账号
        /// </summary>
        /// <returns></returns>
        public bool AddAccount(SysAccount sysAccount)
        {
            if (null == sysAccount)
                return false;
            
            sysAccount.PassWord = this.mHasher.Encrypt(sysAccount.PassWord);
            this.Create(sysAccount);
            this.Save();

            return true;
        }

       /// <summary>
        /// 修改密码
       /// </summary>
       /// <param name="uid"></param>
       /// <param name="password">新密码</param>
       /// <param name="oldPassword">旧密码</param>
        public bool UpdatePassword(int uid, string password, string oldPassword)
        {
            var user = this.Get(uid);
            if (user == null)
                return false;
            if (!mHasher.CompareStringToHash(oldPassword, user.PassWord))
                return false;
            user.PassWord = this.mHasher.Encrypt(password);
            this.Save();
            return true;
        }

        /// <summary>
        /// 修改管理员信息,根据UID
        /// </summary>
        /// <param name="uid">uid</param>
        /// <param name="sysAccount">sysAccount</param>
        /// <returns></returns>
        public bool UpdateSysAccount(int uid, SysAccount sysAccount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 停用当前账号
        /// </summary>
        /// <param name="uid">uid</param>
        /// <returns></returns>
        public bool Disable(int uid)
        {
            var info = this.Get(uid);
            if (null == info)
                return false;

            info.IsEnabled = false;
            this.Save();
            return true;
        }

        /// <summary>
        /// 启用当前账号
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool Enable(int uid)
        {
            var info = this.Get(uid);
            if (null == info)
                return false;

            info.IsEnabled = true;
            this.Save();
            return true;
        }

        /// <summary>
        /// 获取官员账号列表
        /// </summary>
        /// <returns></returns>
        public List<SysAccount> GetSysAccount()
        {
            return this.GetAll().ToList();
        }

        public List<SysAccount> GetSysAccount(int pageIndex, ref int totalCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 账号唯一性验证
        /// </summary>
        /// <param name="code">登录账号</param>
        /// <returns></returns>
        public bool IsUnique(string code)
        {
            return this.mRepo.Where(o => o.Code == code).Count() == 0;
        }
    }
}
