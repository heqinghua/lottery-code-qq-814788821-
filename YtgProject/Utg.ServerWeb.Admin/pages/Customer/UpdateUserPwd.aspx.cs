using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Service;

namespace Utg.ServerWeb.Admin.pages.Customer
{
    public partial class UpdateUserPwd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Params["code"]))
            {

                Response.End();
                return;
            }
            this.txtCode.Text = Request.Params["code"];
            this.txtcodeZij.Text = Request.Params["code"];
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCode.Text.Trim()))
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtpassword.Text.Trim()))
            {
                JsAlert("请填写登陆密码！");
                return;
            }
            if (this.txtpassword.Text.Trim() != this.txtRePwd.Text.Trim())
            {
                JsAlert("两次登陆密码不一致！");
                return;
            }

            ISysUserService userService = IoC.Resolve<ISysUserService>();
            var item = userService.Get(this.txtCode.Text.Trim());
            if (null == item)
            {
                JsAlert("账号不存在！");
                return;
            }
            if (userService.UpdatePassword(item.Id, this.txtpassword.Text.Trim()))
            {
                JsAlert("登陆密码修改成功！");
            }
            else
            {
                JsAlert("登陆密码修改失败！");
            }
        }

        protected void btnChangeZij_Click(object sender, EventArgs e)
        {
            //修改资金密码
            if (string.IsNullOrEmpty(this.txtCode.Text.Trim()))
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtZiJPwd.Text.Trim()))
            {
                JsAlert("请填写资金密码！");
                return;
            }
            if (this.txtZiJPwd.Text.Trim() != this.txtZiJRePwd.Text.Trim())
            {
                JsAlert("两次资金密码不一致！");
                return;
            }

            ISysUserBalanceService userService = IoC.Resolve<ISysUserBalanceService>();
            ISysUserService users = IoC.Resolve<ISysUserService>();
            var uinfo= users.Get(this.txtCode.Text.Trim());
            if (uinfo == null) {

                JsAlert("非法请求");
            }

            if (userService.UpdateManUserBalancePwd(uinfo.Id, this.txtZiJPwd.Text.Trim()))
            {
                JsAlert("登陆密码修改成功！");
            }
            else {
                JsAlert("登陆密码修改失败！");
            }

        }
    }
}