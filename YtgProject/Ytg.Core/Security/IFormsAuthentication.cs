using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Core.Security
{
    /// <summary>
    /// 身份验证相关接口
    /// </summary>
    public interface IFormsAuthentication
    {
        void SignIn(string userName, bool createPersistentCookie, IEnumerable<string> roles);
        void SignOut();
    }
}
