using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Ytg.Data;

namespace Utg.ServerWeb.Admin
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<YtgDbContext, YtgDbContextConfiguration>());
            WindsorConfigurator.Configure();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            /*************防止攻击***********/
            string q = "<div style='position:fixed;top:0px;width:100%;height:100%;background-color:white;color:green;font-weight:bold;border-bottom:5px solid #999;'><br>您的提交带有不合法参数,谢谢合作!</div>";
            if (Request.Cookies != null)
            {
                if (safe_360.CookieData())
                {
                    Response.Write(q);
                    Response.End();

                }


            }

            if (Request.UrlReferrer != null)
            {
                if (safe_360.referer())
                {
                    Response.Write(q);
                    Response.End();
                }
            }

            if (Request.RequestType.ToUpper() == "POST")
            {
                if (safe_360.PostData())
                {

                    Response.Write(q);
                    Response.End();
                }
            }
            if (Request.RequestType.ToUpper() == "GET")
            {
                if (safe_360.GetData())
                {
                    Response.Write(q);
                    Response.End();
                }
            }
            /*************防止攻击***********/
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}