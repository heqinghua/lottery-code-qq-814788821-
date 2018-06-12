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
    public partial class BindBankCard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["aspx_zjPwd"] == null)
            //{
            //    this.panel.Visible = false;
            //    this.panelPwd.Visible = true;
            //}
            //else
            //{
                this.panel.Visible = true;
                this.panelPwd.Visible = false;
                panelSc.Visible = true;
            //}
            if (!IsPostBack) { 
              //验证是否设置资金密码
                ISysUserBalanceService userBananceService = IoC.Resolve<ISysUserBalanceService>();
                var userBalance = userBananceService.GetUserBalance(this.CookUserInfo.Id);
                if (null == userBalance || string.IsNullOrEmpty(userBalance.Pwd))
                {
                    Response.Redirect("/Views/Users/UpdatePwd.aspx?zj=xx");
                    return;
                }
            }
        }

        protected void btnVd_Click(object sender, EventArgs e)
        {
            string pwd = this.oldpwd.Text.Trim();
            if (string.IsNullOrEmpty(pwd))
            {
                Alert("请输入资金密码!");
                return;
            }
            ISysUserBalanceService sysUserBalanceService = IoC.Resolve<ISysUserBalanceService>();
            var fig = sysUserBalanceService.VdUserBalancePwd(this.CookUserInfo.Id, pwd);
            if (fig)
            {
                //验证成功
                Session["aspx_zjPwd"] = "ZJMM";
                this.panel.Visible = true;
                this.panelPwd.Visible = false;
                panelSc.Visible = true;
            }
            else { 
             //验证失败
                Session["aspx_zjPwd"] = null;
                Alert("密码错误!");
                this.panel.Visible = false;
                this.panelPwd.Visible = true;
            }
        }
    }
}