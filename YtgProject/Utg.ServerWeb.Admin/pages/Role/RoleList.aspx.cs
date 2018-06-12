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
    public partial class RoleList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            IRoleService roleService = IoC.Resolve<IRoleService>();
            this.repList.DataSource=roleService.GetAll().ToList();
            this.repList.DataBind();
        }

        protected void btnRef_Click(object sender, EventArgs e)
        {
            InitData();
        }
    }
}