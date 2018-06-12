using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Role
{
    public partial class EditRole : BasePage
    {
        private int roleId = 0;
        private IRoleService roleService = IoC.Resolve<IRoleService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["roleId"], out roleId))
                roleId = -1;  

            if (!IsPostBack)
            {              
                InitRoleInfo();
            }

            //if (!string.IsNullOrEmpty(Request.Params["action"]))
            //{
            //    string result = "";
            //    switch (Request.Params["action"])
            //    {
            //        case "unique":
            //            string roleName = Request.Params["roleName"];
            //            if (string.IsNullOrEmpty(roleName))
            //            {
            //                result = "-1";
            //            }
            //            else
            //            {
            //                IRoleService roleService = IoC.Resolve<IRoleService>();
            //                result = roleService.IsUnique(roleName) ? "0" : "-1";
            //            }
            //            break;
            //    }
            //    Response.Write(result);
            //    Response.End();
            //}

         
        }

        /// <summary>
        /// 初始化角色信息
        /// </summary>
        public void InitRoleInfo()
        {
            Ytg.BasicModel.Role role = roleService.Get(roleId);
            if (null != role)
            {
                this.txtRoleName.Text = role.Name;
                this.txtDescript.Text = role.Descript;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtRoleName.Text.Trim().Length > 15)
                {
                    JsAlert("角色名长度不能超过15个字符！");
                    return;
                }

                if (this.txtDescript.Text.Trim().Length > 100)
                {
                    JsAlert("描述不能超过100个字符！");
                    return;
                }

                if (!roleService.IsUnique(this.txtRoleName.Text.Trim()))
                {
                    JsAlert("角色名已经存在！");
                    return;
                }

                Ytg.BasicModel.Role role = roleService.Get(roleId);
                role.Name = this.txtRoleName.Text.Trim();
                role.Descript = this.txtDescript.Text.Trim();

                roleService.Save();

                JsAlert("保存成功！", true);
            }
            catch (Exception ex)
            {
                JsAlert("保存失败，请稍后再试！");
            }
        }
    }
}