using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Helper
{
    public partial class Notice : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }
        private void BindData()
        {
            INewsService newsServices = IoC.Resolve<INewsService>();
            var result = newsServices.GetAll().OrderByDescending(c => c.OccDate);
            rptNews.DataSource = result;
            rptNews.DataBind();
        }

        public string GetState(object isshow)
        {
            if (Convert.ToBoolean(isshow))
                return "color:red;font-weight:bold;";
            else
                return "";
        }

        public string GetIsOpend(int index,object id)
        {
            if (Request.Params["id"]==id.ToString())
                return "class='active'";
            if (index == 0 && string.IsNullOrEmpty(Request.Params["id"]))
                return "class='active'";
            return "";
        }
    }
}