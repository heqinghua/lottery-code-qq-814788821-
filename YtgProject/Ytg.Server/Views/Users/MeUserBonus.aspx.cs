using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ytg.ServerWeb.Views.Users
{
    public partial class MeUserBonus : BasePage
    {
        public int LoginUserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginUserId = this.CookUserInfo.Id;

        }
    }
}