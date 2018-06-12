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
    public partial class AddAccount : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack)
            { 
            
            }

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
                           ISysAccountService userService = IoC.Resolve<ISysAccountService>();
                           result = userService.IsUnique(code) ? "0" : "-1";
                       }
                       break;
               }
               Response.Write(result);
               Response.End();
           }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ISysAccountService userService = IoC.Resolve<ISysAccountService>();
                if (this.txtCode.Text.Trim().Length < 6 || this.txtCode.Text.Trim().Length > 16)
                {
                    JsAlert("登录名验证错误！");
                    return;
                }
                if (this.txtPassWord.Text.Trim().Length < 6 || this.txtPassWord.Text.Trim().Length > 16)
                {
                    JsAlert("登录密码验证错误！");
                    return;
                }

                if (!userService.IsUnique(this.txtCode.Text.Trim()))
                {                    
                    JsAlert("登录名已经存在！");
                    return;
                }

                SysAccount account = new SysAccount();
                account.Code = this.txtCode.Text.Trim();
                account.Name = this.txtName.Text.Trim();
                account.PassWord = this.txtPassWord.Text.Trim();
                account.IsEnabled = true;
                if (userService.AddAccount(account))
                {
                    this.txtCode.Text = string.Empty;
                    this.txtName.Text = string.Empty;
                    this.txtPassWord.Text = string.Empty;
                    JsAlert("保存成功！", true);
                }
                else
                {
                    JsAlert("保存失败，请稍后再试！");
                }
            }
            catch (Exception ex)
            {
                JsAlert("保存失败，请稍后再试！");
            }
        }
    }
}