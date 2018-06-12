using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Reflection;
using Ytg.Comm.Security;

namespace Ytg.ServerWeb.Page.PageCode
{
    /// <summary>
    /// 移动设备服务提供程序
    /// </summary>
    public class MobilePage : System.Web.UI.Page
    {
       

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        public CookUserInfo LoginUser { get; private set; }

        protected override void OnPreInit(EventArgs e)
        {
          
            this.InititalLoginUser();
            string requestUrl = Request.Url.AbsoluteUri;
            var requestManager = RequestManagerFactory.CreateInstance().CreateBaseRequestManager(requestUrl);
            if (null != requestManager)
                requestManager.ProcessRequest(this);
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        private void InititalLoginUser()
        {
            FormsPrincipal<CookUserInfo> source = FormsPrincipal<CookUserInfo>.TryParsePrincipal(this.Request);
            if (null != source)
                this.LoginUser = source.UserData;
        }





    }
}
