using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Customer
{
    public partial class EditUser : BasePage
    {
        private int user_id=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Params["action"]))
            {
                string result = "";
                switch (Request.Params["action"])
                {
                    case "unique":
                        string code = Request.Params["code"];
                        if (string.IsNullOrEmpty(code))
                        {
                            result = "-1";
                        }
                        else
                        {
                            ISysUserService userService = IoC.Resolve<ISysUserService>();
                            result = userService.IsUnique(code) ? "0" : "-1";
                        }
                        break;
                }
                Response.Write(result);
                Response.End();
                return;
            }
            if (!int.TryParse(Request.Params["id"], out user_id))
                user_id = -1;
            if (!IsPostBack)
            {
                
                this.Bind();
            }
        }

        private void Bind()
        {
            if (user_id <= 0)
                return;
            ISysUserService userService = IoC.Resolve<ISysUserService>();
            var item = userService.Get(this.user_id);
            if (null == item)
            {
                Response.End();
                return;
            }
            this.txtCode.Text = item.Code;
            this.txtCode.Enabled = false;
            this.txtNickName.Text = item.NikeName;
            this.txtBackNum.Text = item.Rebate.ToString();
            this.drpUserType.SelectedValue = ((int)item.UserType).ToString();
            this.drpjj.SelectedValue = ((int)item.PlayType).ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SysUser user = null;
            ISysUserService userService = IoC.Resolve<ISysUserService>();
            if (user_id > 0)//修改
                user = userService.Get(this.user_id);
            else
            {
                user = new SysUser();
                switch (this.drpUserType.SelectedValue)
                {
                    case "0":
                        user.UserType = UserType.General;
                        break;
                    case "1":
                        user.UserType = UserType.Proxy;
                        break;
                    case "2":
                        user.UserType = UserType.Manager;
                        break;
                    case "3":
                        user.UserType = UserType.BasicProy;
                        break;
                    case "4":
                        user.UserType = UserType.Main;
                        break;
                }
              
            }

            if (this.txtCode.Text.Trim().Length < 6 || this.txtCode.Text.Trim().Length >16)
            {
                Warning("登录名验证错误！");
                return;
            }
            if (this.txtpassword.Text.Trim().Length < 6 || this.txtpassword.Text.Trim().Length > 16)
            {
                Warning("登录密码验证错误！");
                return;
            }

            double outRebate = 0;//默认1800
            if (!double.TryParse(txtBackNum.Text.Trim(), out outRebate))
            {
                Warning("请填写用户返点！");
                return;
            }
            if (!userService.IsUnique(this.txtCode.Text.Trim())) {
                Warning("登录名已经存在！");
                return;
            }
            outRebate = 8;
            user.Code = this.txtCode.Text.Trim();
            user.NikeName = this.txtNickName.Text.Trim();
            user.Password = this.txtpassword.Text.Trim();
            user.Rebate = outRebate;
            user.PlayType = drpjj.SelectedValue == "0" ? UserPlayType.P1800 : UserPlayType.P1700;//
            user.UserType = UserType.Main;
            bool isCompled = false;
            if (user_id > 0)
            {
                userService.Save();
                isCompled = true;
            }
            else
            {
                isCompled = userService.CreateUser(user);
                //处理配额
                ISysQuotaService quotaService = IoC.Resolve<ISysQuotaService>();
                quotaService.InintPrxyQuota(user);
            }
            if (isCompled)
            {
                JsAlert("保存成功！", true);
                this.txtCode.Text = string.Empty;
            }
            else
            {
                JsAlert("保存失败，请稍后再试！");
                //
            }
        }
    }
}