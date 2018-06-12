using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.Users
{
    public partial class LockCardNum :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //锁定
            string password = this.txtpwd.Text.Trim();
            if (string.IsNullOrEmpty(password))
            {
                Alert("请输入资金密码!");
                return;
            }
            ISysUserService mSysUserService = IoC.Resolve<ISysUserService>();
            ISysUserBalanceService mSysUserBalanceService = IoC.Resolve<ISysUserBalanceService>(); 
            //验证资金密码
            if (!mSysUserBalanceService.VdUserBalancePwd(CookUserInfo.Id, password))
            {
                Alert("资金密码验证失败!");
                return;
            }
            //修改为锁定
            if (mSysUserService.LockUserCards(this.CookUserInfo.Id))
            {
                //关闭窗口
                Response.Write("<script>parent.window.location.href = '/Views/Users/BindBankCard.aspx?dt="+DateTime.Now.ToString()+"';</script>");
            }
            else
            {
                Alert("锁定失败,请稍后重试!");
                return;
            }
        }
    }
}