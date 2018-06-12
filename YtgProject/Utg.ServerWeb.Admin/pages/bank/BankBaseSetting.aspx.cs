using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.bank
{
    public partial class BankBaseSetting : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.BindList();

        }

        private void BindList()
        {

            ISysBankType userService = IoC.Resolve<ISysBankType>();

            bool? isdel = null;

            int totalCount = 0;
            var result = userService.GetBankType("", isdel, pagerControl.CurrentPageIndex, ref totalCount);
            this.pagerControl.RecordCount = totalCount;
            this.repList.DataSource = result;
            this.repList.DataBind();
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

        protected void lbDel_Click(object sender, EventArgs e)
        {
            var lbButton = (sender as LinkButton);
            if (lbButton == null)
                return;
            int id = 0;
            if (!int.TryParse(lbButton.CommandArgument, out id))
            {
                return;
            }
            ISysBankType userService = IoC.Resolve<ISysBankType>();
            userService.Delete(id);
            userService.Save();

            this.BindList();
        }

        protected void chk_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}