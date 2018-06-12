using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Manager
{
    public partial class EditAccount : BasePage
    {
        private int userId = 0;
        private ISysAccountService userService = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            userService = IoC.Resolve<ISysAccountService>();
            if (!IsPostBack){ }

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
                            result = userService.IsUnique(code) ? "0" : "-1";
                        }
                        break;
                }
                Response.Write(result);
                Response.End();
                return;
            }

            if (!int.TryParse(Request.Params["userId"], out userId))
                userId = -1;

            //初始化数据
            InitUserInfo();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitUserInfo()
        {
            SysAccount account = userService.Get(userId);
            if (null != account)
            {
                this.txtCode.Text = account.Code;
                this.txtName.Text = account.Name;
                this.drpUserType.SelectedValue = account.IsEnabled == true ? "1" : "0";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           try
           {
                if (this.txtCode.Text.Trim().Length < 6 || this.txtCode.Text.Trim().Length > 16)
                {
                    Warning("登录名验证错误！");
                    return;
                }
                if (this.txtpassword.Text.Trim().Length < 6 || this.txtpassword.Text.Trim().Length > 16)
                {
                    Warning("登录密码验证错误！");
                    return;
                }

                if (!userService.IsUnique(this.txtCode.Text.Trim()))
                {
                    Warning("登录名已经存在！");
                    return;
                }
                
                SysAccount account = userService.Get(this.userId);
                account.Code = this.txtCode.Text.Trim();
                account.Name = this.txtName.Text.Trim();
                account.PassWord = this.txtpassword.Text.Trim();
                account.IsEnabled = this.drpUserType.SelectedValue == "1" ? true : false;

                userService.Save();

                JsAlert("保存成功！", true);
                this.txtCode.Text = string.Empty;

           }
           catch(Exception ex)
           {
               JsAlert("保存失败，请稍后再试！");
           }
        }
    }
}