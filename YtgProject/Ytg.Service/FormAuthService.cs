using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Ytg.Core.Security;

namespace Ytg.Service
{
    public class FormAuthService : IFormsAuthentication
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="createPersistentCookie"></param>
        /// <param name="roles"></param>
        public void SignIn(string userName, bool createPersistentCookie, IEnumerable<string> roles)
        {
            var str = string.Join(",", roles);

            //str.Remove(str.Length - 1, 1);

            var authTicket = new FormsAuthenticationTicket(
                1,
                userName,  //user id
                DateTime.Now,
                DateTime.Now.AddDays(-1),  // expiry
                createPersistentCookie,
                str,
                "/");

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));

            if (authTicket.IsPersistent)
            {
                cookie.Expires = authTicket.Expiration;
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 注销
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
