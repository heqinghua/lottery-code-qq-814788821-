using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages
{
    public partial class NewsList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                this.Bind();
            }

        }

        private void Bind()
        {
            INewsService newsService = IoC.Resolve<INewsService>();
            int totalCount = 0;
            var result = newsService.SelectBy(-1, this.txtTitle.Text.Trim(), "", "", pagerControl.CurrentPageIndex, ref totalCount);
            this.repList.DataSource = result;
            this.repList.DataBind();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.Bind();
        }

        protected void pagerControl_PageChanged(object sender, EventArgs e)
        {
            this.Bind();
        }

        protected void btnDel_Command(object sender, CommandEventArgs e)
        {
            var arg= e.CommandArgument;
            if (null == arg)
                return;
            INewsService newsService = IoC.Resolve<INewsService>();
            newsService.Delete(Convert.ToInt32(arg));
            this.Bind();
        }


       
    }
}