using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Mobile.userCenter
{
    public partial class News : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            INewsService newsServices = IoC.Resolve<INewsService>();
            var result = newsServices.GetAll().OrderByDescending(c => c.OccDate).Take(20).ToList();
            var openNews = result.Where(x => x.IsShow == 1).FirstOrDefault();
            
            this.rptList.DataSource = result;
            this.rptList.DataBind();
        }
    }
}