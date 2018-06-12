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
    public partial class Recharge : BasePage
    {

        public int BaseMaxMonery = 100000;//总代可充值10w

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //验证是否设置资金密码
                ISysUserBalanceService userBananceService = IoC.Resolve<ISysUserBalanceService>();
                var userBalance = userBananceService.GetUserBalance(this.CookUserInfo.Id);
                if (null == userBalance || string.IsNullOrEmpty(userBalance.Pwd))
                {
                    //Response.Redirect("/Views/Users/UpdatePwd.aspx?zj=xx");
                    //Response.Write();
                    ClientScript.RegisterStartupScript(this.GetType(), "change_postion", "parent.window.location='/Views/Users/UpdatePwd.aspx?zj=xx';", true);
                   // return;
                }
                this.BindData();
                if (CookUserInfo.UserType == BasicModel.UserType.BasicProy || CookUserInfo.UserType == BasicModel.UserType.Main)
                {
                    //总代/主管可以设置充值分红类型
                    PanelType.Visible = true;
                    BaseMaxMonery = 100000;
                }
                else
                {
                    BaseMaxMonery = 10000;
                }
                spanEnd.InnerText = BaseMaxMonery.ToString();//充值限额为10w
            }
        }

        private void BindData()
        {
            int uid;
            if (!int.TryParse(Request.Params["id"], out uid))
            {
                Response.End();
                return;
            }
            //获取当前用户信息
            ISysUserService userService = IoC.Resolve<ISysUserService>();
            var inuser= userService.Get(uid);
            if (inuser == null)
            {
                Response.End();
                return;
            }
            this.lbInCode.Text = inuser.Code;

            ISysUserBalanceService userBalance = IoC.Resolve<ISysUserBalanceService>();
            var value = userBalance.GetUserBalance(this.CookUserInfo.Id);
            if (null == value || value.UserAmt < 10)
            {
                //Alert("您的余额不够"); 
                return;
            }
            this.lbMonery.Text = value.UserAmt.ToString();
        }

        
    }
}