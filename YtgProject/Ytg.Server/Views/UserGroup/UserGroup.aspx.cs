using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.UserGroup
{
    public partial class UserGroup : BasePage
    {
        public string Code;
        public string NockName;
        public string Monery;
        public decimal NonMonery;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ISysUserService mSysUserService = IoC.Resolve<ISysUserService>();
                var monery = mSysUserService.GroupUserAmt(this.CookUserInfo.Id);
                NonMonery=monery == null ? 0 : monery.Value;
                Monery = string.Format("{0:N2}", NonMonery);
                Code = CookUserInfo.Code;
                NockName = CookUserInfo.NikeName;
            }
        }
    }
}