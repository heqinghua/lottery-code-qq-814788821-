using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.Users
{
    public partial class UpdatePwd :BasePage
    {
        public string UpdateUserPwd = "";
        public string UpdateUserTable = "";
        public string UpdateZiJinPwd = "";
        public string UpdateZiJinTable = "";
        public int showAl = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var loginUserid = this.CookUserInfo.Id;

                ISysUserBalanceService userBananceService = IoC.Resolve<ISysUserBalanceService>();
                var userBalance = userBananceService.GetUserBalance(loginUserid);
                if (null == userBalance)
                {
                    return;
                }
                if (string.IsNullOrEmpty(userBalance.Pwd))
                {
                    oldzjPwd.Visible = false;
                    nonwZiPwd.Visible = true;
                    showAl = 1;
                    UpdateZiJinPwd = "selected";
                    UpdateUserTable = "display:none;";
                }
                else
                {
                    UpdateUserPwd = "selected";
                    UpdateZiJinTable = "display:none;";
                }
            }
        }
    }
}