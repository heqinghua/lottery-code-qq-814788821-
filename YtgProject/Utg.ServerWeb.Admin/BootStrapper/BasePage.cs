using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        const string mWarningTemplate = "<div id='warning_alert' class='alert {0}'><a href='#' class='close' data-dismiss='alert'>&times;</a><strong>警告！</strong>{1}</div>";

        protected CookUserInfo LoginUser { get; private set; }
        protected int menuId = -1;
        protected ActionModel actionModel = null;

        protected override void OnInit(EventArgs e)
        {
            if (!ValidateLoginUser())
                Response.Redirect("/login.aspx");

            if (System.Web.HttpContext.Current.Request["menuId"] != null)
            {
                int.TryParse(System.Web.HttpContext.Current.Request["menuId"].ToString(), out menuId);

                //初始化页面操作按钮
                GetPagePermission();
            }
        }

        /// <summary>
        /// 获取页面操作权限
        /// </summary>
        public void GetPagePermission()
        {
            //获取权限列表
            IPermissionService permissionService = IoC.Resolve<IPermissionService>();
            List<Ytg.BasicModel.Permission> list = permissionService.GetPagePermissionList(LoginUser.Id, menuId);

            actionModel = new ActionModel();
            actionModel.Add = false;
            actionModel.Update = false;
            actionModel.Delete = false;
            actionModel.Detail = false;
            actionModel.SetRole = false;
            actionModel.SetPermission = false;
            actionModel.TransferAccount = false;
            actionModel.Recharge = false;
            actionModel.AccountChange = false;
            actionModel.TeamMoney = false;
            actionModel.Subordinate = false;
            actionModel.Revocation = false;
            actionModel.Disabled = false;
            actionModel.Eabled = false;

            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Action == "add")
                    {
                        actionModel.Add = true;
                    }
                    else if (list[i].Action == "update")
                    {
                        actionModel.Update = true;
                    }
                    else if (list[i].Action == "delete")
                    {
                        actionModel.Delete = true;
                    }
                    else if (list[i].Action == "detail")
                    {
                        actionModel.Detail = true;
                    }
                    else if (list[i].Action == "audit")
                    {
                        actionModel.Audit = true;
                    }
                    else if (list[i].Action == "setRole")
                    {
                        actionModel.SetRole = true;
                    }
                    else if (list[i].Action == "setPermission")
                    {
                        actionModel.SetPermission = true;
                    }
                    else if (list[i].Action == "transferAccount")
                    {
                        actionModel.TransferAccount = true;
                    }
                    else if (list[i].Action == "recharge")
                    {
                        actionModel.Recharge = true;
                    }
                    else if (list[i].Action == "accountChange")
                    {
                        actionModel.AccountChange = true;
                    }
                    else if (list[i].Action == "teamMoney")
                    {
                        actionModel.TeamMoney = true;
                    }
                    else if (list[i].Action == "subordinate")
                    {
                        actionModel.Subordinate = true;
                    }
                    else if (list[i].Action == "revocation")
                    {
                        actionModel.Revocation = true;
                    }
                    else if (list[i].Action == "disabled")
                    {
                        actionModel.Disabled = true;
                    }
                    else if (list[i].Action == "eabled")
                    {
                        actionModel.Eabled = true;
                    }
                }
            }
        }

        public virtual bool ValidateLoginUser()
        {
            var cookUser = FormsPrincipal<CookUserInfo>.TryParsePrincipal(Request);
            if (null == cookUser || cookUser.UserData == null)
                return false;
            this.LoginUser = cookUser.UserData;
            return true;
        }

        /// <summary>
        /// 弹出警告信息
        /// </summary>
        /// <param name="message"></param>
        protected void Warning(string message)
        {
            Alert(message, "alert-warning");
        }

        /// <summary>
        /// 弹出成功信息
        /// </summary>
        /// <param name="message"></param>
        protected void Success(string message)
        {
            Alert(message, "alert-success");
        }

       
        protected void Alert(string message,string type)
        {
            string script = "<script>$(function(){$('#warning_alert').remove();  $('#al_container').append(\"" + string.Format(mWarningTemplate, type, message) + "\");  $(\"#warning_alert\").alert();})</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "warning_alert", script, false);
        }

        protected void JsAlert(string message,bool isHide=false)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "warning_alert_ks", "<script>alert('"+message+"');"+(isHide?"parent.hideModal();":"")+"</script>", false);
        }
    }

    
    public class ActionModel
    {
        /// <summary>
        ///  新增
        /// </summary>
        public bool Add { get; set; }

        /// <summary>
        /// 修改
        /// </summary>
        public bool Update { get;set; }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete { get; set; }

        /// <summary>
        /// 详情
        /// </summary>
        public bool Detail { get; set; }

        //审核
        public bool Audit { get; set; }

        /// <summary>
        /// 设置角色
        /// </summary>
        public bool SetRole { get; set; }

        /// <summary>
        /// 设置权限
        /// </summary>
        public bool SetPermission { get; set; }

        /// <summary>
        /// 转账
        /// </summary>
        public bool TransferAccount { get; set; }

        /// <summary>
        /// 充值
        /// </summary>
        public bool Recharge { get; set; }

        /// <summary>
        /// 账变
        /// </summary>
        public bool AccountChange { get; set; }

        /// <summary>
        /// 团队余额
        /// </summary>
        public bool TeamMoney { get; set; }

        /// <summary>
        /// 下级
        /// </summary>
        public bool Subordinate { get; set; }

        /// <summary>
        /// 撤销
        /// </summary>
        public bool Revocation { get; set; }

        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Eabled { get; set; }
    }
}