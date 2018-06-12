using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Customer
{
    public partial class LineUser : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Bind();
            }
        }


        private void Bind()
        {
            ISysUserService userService = IoC.Resolve<ISysUserService>();

            int totalCount = 0;
            var result = userService.GetLineUsers(pagerControl.CurrentPageIndex, pagerControl.PageSize, ref totalCount);
            this.pagerControl.RecordCount = totalCount;
            this.repList.DataSource = result;
            this.repList.DataBind();
        }

        protected void pagerControl_PageChanged(object sender, EventArgs e)
        {
            this.Bind();
        }

        public string ToUserStateString(string state)
        {
            string toStr = "普通会员";
            switch (state)
            {
                case "Proxy":
                    toStr = "代理";
                    break;
                case "BasicProy":
                    toStr = "总代理";
                    break;
                case "Main":
                    toStr = "主管";
                    break;

            }
            return toStr;
        }

    }
}