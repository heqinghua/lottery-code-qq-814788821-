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
    public partial class AccountList : BasePage
    {
        private string code = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }

        public void InitData()
        {
            ISysAccountService roleService = IoC.Resolve<ISysAccountService>();
            List<SysAccount> list = null;
            if (string.IsNullOrEmpty(code))
                list = roleService.GetAll().ToList();
            else
                list= roleService.GetAll().Where(m => m.Code.Contains(code)).ToList();

            this.repList.DataSource = list;
            this.repList.DataBind();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            code = txtUserCode.Text.Trim();
            InitData();
        }

        public bool GetVis(object state)
        {
            if (null == state)
                return true;
            return (bool)state;
        }

        //停用
        protected void btnDisabled_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;
            int id = Convert.ToInt32(e.CommandArgument);
            ISysAccountService userService = IoC.Resolve<ISysAccountService>();
            userService.Disable(id);
            this.InitData();
            JsAlert("禁用成功！");
        }

        //启用
        protected void btnEabled_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;
            int id = Convert.ToInt32(e.CommandArgument);
            ISysAccountService userService = IoC.Resolve<ISysAccountService>();
            userService.Enable(id);
            this.InitData();
            JsAlert("启用成功！");
        }

        protected void btnRef_Click(object sender, EventArgs e)
        {
            this.InitData();
        }
    }
}