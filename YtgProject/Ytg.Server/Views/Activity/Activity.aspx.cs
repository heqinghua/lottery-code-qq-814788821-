using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.Activity
{
    public partial class Activity :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                this.BindData();
        }

        private void BindData()
        {
            IActivityService activityServices = IoC.Resolve<IActivityService>();
            var activitys = activityServices.GetActivitys();
            rptActivitys.DataSource = activitys;
            rptActivitys.DataBind();
        }
    }
}