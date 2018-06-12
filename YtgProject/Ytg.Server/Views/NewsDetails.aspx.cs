using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views
{
    public partial class NewsDetails : BasePage
    {
        public string  news = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                this.Bind();
            }
        }

        private void Bind() {
            int id=0;
            if(!int.TryParse(Request.QueryString["id"],out id)){
                return ;
            }

            INewsService newsServices = IoC.Resolve<INewsService>();
            var ct= newsServices.Get(id);
            if (ct != null)
                news = System.Web.HttpUtility.UrlDecode(ct.Content);
        }
    }
}