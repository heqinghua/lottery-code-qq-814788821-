using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages
{
    public partial class EditPassword : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtCode.Text = this.LoginUser.Code;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string oldPwd = this.txtOldPassword.Text.Trim();
            string newPwd = this.txtpassword.Text.Trim();
            string reNewPwd = this.txtRePwd.Text.Trim();
            if (string.IsNullOrEmpty(oldPwd) ||
                string.IsNullOrEmpty(newPwd) ||
                string.IsNullOrEmpty(reNewPwd))
            {
                JsAlert("参数不能为空！");
                return;
            }
            if (newPwd != reNewPwd)
            {
                JsAlert("新密码与确认密码不一致！");
                return;
            }

            var sysAccountService = IoC.Resolve<ISysAccountService>();
            var userInfo = sysAccountService.Login(this.LoginUser.Code, oldPwd);
            if (null == userInfo)
            {
                JsAlert("密码输入错误！");
                return;
            }
            if (sysAccountService.UpdatePassword(LoginUser.Id, newPwd, oldPwd))
            {
                JsAlert("密码修改成功");
                return;
            }
            else
            {
                JsAlert("密码修改失败，请稍后再试！");
            }
        }
    }
}