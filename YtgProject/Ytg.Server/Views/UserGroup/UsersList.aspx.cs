using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;

namespace Ytg.ServerWeb.Views.UserGroup
{
    public partial class UsersList : BasePage
    {
        public int LoginUserType;

        public double MaxRemb = 7.5;//最高返点

        public string HasFil = "";
        public int RowSpan = 6;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoginUserType = (int)CookUserInfo.UserType;
                MaxRemb = Utils.GetMaxRemo(CookUserInfo);
                if (CookUserInfo.UserType != BasicModel.UserType.Main && CookUserInfo.UserType != BasicModel.UserType.BasicProy) {
                    HasFil = "display:none";
                    RowSpan = 4;
                }
            }
        }
    }
}