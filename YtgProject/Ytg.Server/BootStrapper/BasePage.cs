using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Ytg.BasicModel;
using Ytg.Comm.Security;

namespace Ytg.ServerWeb
{
    /// <summary>
    /// page基类
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 彩种编码
        /// </summary>
        protected string LotteryCode { get; set; }

        /// <summary>
        /// 彩种id
        /// </summary>
        protected int LotteryId { get; set; }

        /// <summary>
        /// denglu xinxi 
        /// </summary>
        protected CookUserInfo CookUserInfo { get; private set; }

        public BasePage()
        {
            LotteryId = -1;

        }

        protected override void OnInit(EventArgs e)
        {
           //this.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            base.OnInit(e);

            if (!ValidateLoginUser())
            {
                if (Comm.Utils.IsMobile())
                    Response.Redirect("/wap/login.html");
                else
                    Response.Redirect("/login.html");
            }
          
        }

        public virtual bool ValidateLoginUser()
        {
            var cookUser = FormsPrincipal<CookUserInfo>.TryParsePrincipal(Request);
            if (null == cookUser || cookUser.UserData == null)
                return false;
            this.CookUserInfo = cookUser.UserData;
            return true;
        }


        #region 客户端 alert消息
        /// <summary>
        /// t客户端alert消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="action">执行脚本</param>
        protected virtual void Alert(string msg,string action,double time)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert(\"" + msg + "\");</script>");
        }
        protected virtual void Alert(string msg,string action)
        {
            this.Alert(msg, action,1.5);
        }
        protected virtual void Alert(string msg)
        {
            this.Alert(msg,"",1.5);
        }
        #endregion
    }
}