using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using Quartz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Data;

namespace Ytg.ServerWeb
{
    public class Global : System.Web.HttpApplication
    {
        //private IScheduler mIScheduler;

        protected void Application_Start(object sender, EventArgs e)
        {
            //Ytg.Scheduler.Comm.LogManager.Writer(this.GetType(),"cc");
            /*************
             * 开发中多次修改数据实体和数据库结构
             * 加入该句代码可自动同步实体与数据库版本
             * 实际发布中将注释该代码
             * ************/
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<YtgDbContext, YtgDbContextConfiguration>());

            WindsorConfigurator.Configure();
             Ytg.Scheduler.Comm.LogManager.Info("application started!");

            //InintData.Initital();
            // //初始化任务数据
            //  this.mIScheduler = new SchedulerManager().Initital();
            //this.mIScheduler.Start();
            //Ytg.Scheduler.Comm.LogManager.Writer(this.GetType(), "scheduler start !");
            // var ss= new Ytg.Scheduler.Comm.IssueBuilder.GenerateShangHaiSslLotteryIssue().Generate(DateTime.Now);

            //Ytg.ServerWeb.BootStrapper.SessionManager.LoadSession();
        }

        protected void Session_Start(object sender, EventArgs e)
        { 

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!Ytg.ServerWeb.BootStrapper.SiteHelper.IsOpenSite()) {

                Response.Write(Ytg.ServerWeb.BootStrapper.SiteHelper.GetSiteCloseContent());
                Response.End();
            }
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
            Exception ex = this.Context.Server.GetLastError();
            if (null != ex)
                Ytg.Scheduler.Comm.LogManager.Error("Global.Application_Error", ex);
            //跳转至友好错误页面
           // Response.Redirect("/error.html");
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Hashtable hOnline = (Hashtable)Application["Online"];
            if (hOnline == null)
                return;
            if (hOnline[Session.SessionID] != null)
            {
                hOnline.Remove(Session.SessionID);
                Application.Lock();
                Application["Online"] = hOnline;
                Application.UnLock();
                //移除cookie
                FormsPrincipal<CookUserInfo>.SignOut();
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
            // this.mIScheduler.PauseAll();
           // Ytg.ServerWeb.BootStrapper.SessionManager.SaveSession();
        }
    }
}