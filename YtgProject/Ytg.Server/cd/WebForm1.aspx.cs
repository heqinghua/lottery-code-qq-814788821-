using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.ServerWeb.Page.PageCode;

namespace Ytg.ServerWeb.cd
{
    public partial class WebForm1 : System.Web.UI.Page
    {
         ISysUserService mSysUserService;
         ISysSettingService mSysSettingService;
        ISysUserBalanceService mSysUserBalanceService;
        ISysUserBalanceDetailService mSysUserBalanceDetailService;
        ISysQuotaService mSysQuotaService;//配额
        protected void Page_Load(object sender, EventArgs e)
        {
            mSysUserService = IoC.Resolve<ISysUserService>();
            mSysSettingService = IoC.Resolve<ISysSettingService>();
            mSysUserBalanceService = IoC.Resolve<ISysUserBalanceService>();
            mSysUserBalanceDetailService = IoC.Resolve<ISysUserBalanceDetailService>();
            mSysQuotaService = IoC.Resolve<ISysQuotaService>();


            var path = Server.MapPath("~/cd/80000.txt");
            using (var st = System.IO.File.OpenText(path))
            {
                while (!st.EndOfStream)
                {
                    string code = st.ReadLine().Trim();
                    //13648
                    //13806
                    regist(code, code, 5521);
                    
                }
            }
        }

        private void regist(string account, string nickName, int mloginUserId)
        {


            //验证码

            string qq = "";
            string phone = "";

            var parentItem = this.mSysUserService.Get(mloginUserId);
            if (null == parentItem || parentItem.UserType == UserType.General || parentItem.UserType == UserType.Manager)
            {

                return;
            }

            //验证账号是否存在
            var item = this.mSysUserService.Get(account);
            if (null != item)
            {
                AppGlobal.RenderResult(ApiCode.ValidationFails);//非法请求，无法继续
                return;
            }
            string psw = "a123456";
            //注册
            var outuser = new SysUser()
            {

                Code = account,
                ParentId = parentItem.Id,
                NikeName = nickName,
                Password = psw,
                PlayType = parentItem.PlayType,
                Rebate = (parentItem.Rebate + parentItem.AutoRegistRebate),
                UserType = UserType.Proxy,
                ProxyLevel = parentItem.ProxyLevel + 1,
                Qq = qq,
                MoTel = phone,
                Head = parentItem.Head
            };
            if (outuser.Rebate < 0)
            {
                outuser.Rebate = parentItem.Rebate;
            }
            this.mSysUserService.Create(outuser);
            this.mSysUserService.Save();
            UserComm.InintNewUserBalance(outuser, this.mSysSettingService, this.mSysUserBalanceService, this.mSysUserBalanceDetailService, this.mSysUserService);//初始化新用户金额
            //初始化配额
            this.mSysQuotaService.InintUserQuota(outuser.Id, outuser.ParentId, Math.Round(Ytg.Comm.Utils.MaxRemo - outuser.Rebate, 1));
            //初始化资金密码
            InintUserBalancePassword(outuser.Id, "a123456");
            AppGlobal.RenderResult(ApiCode.Success);//非法请求，无法继续

        }

        private bool InintUserBalancePassword(int userid, string password)
        {

            if (string.IsNullOrEmpty(password))
            {
                return false;
            }
            var isFig = this.mSysUserBalanceService.InintUserBalancePwd(userid, password);

            return isFig;
        }
    }
}