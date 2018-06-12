using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;

namespace Ytg.ServerWeb.Mobile
{
    public partial class LotteryMain :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CookUserInfo.UserType == UserType.Main
               || CookUserInfo.UserType == UserType.BasicProy
               || CookUserInfo.Rebate < 0.2)//7.8以及以上的用户不允许投注
                {
                    lt_title_s.Visible = true;
                    lotteryCenter.Visible = false;
                }
                
            }
        }
    }
}