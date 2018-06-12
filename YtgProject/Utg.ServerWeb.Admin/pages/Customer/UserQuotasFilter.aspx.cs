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
    public partial class UserQuotasFilter : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtUserCode.Text = Request.Params["code"];
                this.BindList();
            }

        }

        private void BindList()
        {
            ISysQuotaService userService = IoC.Resolve<ISysQuotaService>();
            int userType = Convert.ToInt32(drpuserType.SelectedValue);
            int userstate = Convert.ToInt32(drpPlayType.SelectedValue);
            int totalCount = 0;
            string otherWhere = "";
            int begin;int end;
            if (drpuserState.SelectedIndex > 0 && int.TryParse(this.txtBegin.Text.Trim(),out begin) && int.TryParse(this.txtEnd.Text.Trim(),out end))
            {
                otherWhere += " and "+"["+drpuserState.SelectedValue+"] between "+begin+" and "+end+"";
            }

            var result = userService.FilterQuotas(this.txtUserCode.Text.Trim(), userType, userstate, otherWhere, pagerControl.CurrentPageIndex, pagerControl.PageSize, ref totalCount);
            this.pagerControl.RecordCount = totalCount;
            this.repList.DataSource = result;
            this.repList.DataBind();

        }

        public string ToUserStateString(string state)
        {
         
            string toStr = "普通会员";
            switch (state)
            {
                case "1":
                    toStr = "代理";
                    break;
                case "3":
                    toStr = "总代理";
                    break;
                case "4":
                    toStr = "主管";
                    break;

            }
            return toStr;
        }

        protected void pagerControl_PageChanged(object sender, EventArgs e)
        {
            this.BindList();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.BindList();
        }


        protected void btnRef_Click(object sender, EventArgs e)
        {
            this.BindList();
        }
    }
}