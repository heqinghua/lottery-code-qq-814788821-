using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string code = Request.Form["email"];
            string password = Request.Form["password"];
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(password))
            {
                Response.Write("<script>alert('请填写完整信息！');</script>");
                return;
            }
           
            //var userService = IoC.Resolve<ISysUserService>();
            //var userInfo = userService.GetAm(code, password);
            //if (userInfo == null || userInfo.UserType != Ytg.BasicModel.UserType.Manager)
            //{
            //    Response.Write("<script>alert('登录名不存在或密码错误！');</script>");
            //    return;
            //}

            var sysAccountService = IoC.Resolve<ISysAccountService>();
            
            var userInfo = sysAccountService.Login(code, password);
            if (userInfo == null)
            {
                Response.Write("<script>alert('登录名不存在或密码错误！');</script>");
                return;
            }
            else if (userInfo.IsEnabled == false)
            {
                Response.Write("<script>alert('当前账号已停用！');</script>");
                return;
            }
            else
            {
                //记录登录日志
                var sysAccountLogsService = IoC.Resolve<ISysAccountLogsService>();
                sysAccountLogsService.Create(new SysAccountLog()
                {
                    UserId = userInfo.Id,
                    OccDate = DateTime.Now,
                    Ip = Ytg.Comm.Utils.GetIp(),
                    ServerSystem = Ytg.Comm.Utils.GetUserSystem(),
                 //   Descript = Utils.GetCityByIp(Ytg.Comm.Utils.GetIp())
                });
                sysAccountLogsService.Save();
            }
            
            string preLoginIp=userInfo.LastLoginIp;
            string preLoginTime=userInfo.LastLoginTime!=null?userInfo.LastLoginTime.Value.ToString("yyyy/MM/dd HH:mm:ss"):"";
            string curLoginIp=Utils.GetIp();//当前登录的Ip

            //修改登录IP和时间
            userInfo.PreLoginIp = userInfo.LastLoginIp;
            userInfo.PreLoginTime = userInfo.LastLoginTime;
            userInfo.LastLoginIp = Utils.GetIp();
            userInfo.LastLoginTime = DateTime.Now;
            sysAccountService.Save();//保存信息
           
            //登录成功
            CookUserInfo cokUserInfo = new CookUserInfo()
            {
                Id = userInfo.Id,
                Code = userInfo.Code,
                NikeName=curLoginIp+","+preLoginIp,
                Sex=preLoginTime
            };
            FormsPrincipal<CookUserInfo>.SignIn(userInfo.Code, cokUserInfo, FormsAuthentication.Timeout.Minutes);
            //跳转至index.aspx
            Response.Redirect("/index.html");
        }
    }
}