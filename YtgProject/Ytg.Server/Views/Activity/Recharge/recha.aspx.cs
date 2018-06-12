using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ytg.ServerWeb.Views.Activity.Recharge
{
    public partial class recha : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 
            }
        }

        protected void btnME_Click(object sender, EventArgs e)
        {
            //充值领取金额
           /* var dm = Ytg.ServerWeb.Views.pay.WebRechangComm.ManagerSend(CookUserInfo.Id);
            if (dm == 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('充值的金额未达到活动标准，领取失败！');</script>");
            }
            else if (dm == -1)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('您今天已经领取过了！');</script>");
            }
            else
            {
                string message = "领取礼包成功，领取金额为：" + dm + "<br/>";
                message += "感谢你的参与，祝你游戏愉快!";
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert(\"" + message + "\",1,function(){});</script>");
            }*/
        }
    }
}