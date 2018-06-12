using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb
{
    public partial class AutoRegist : System.Web.UI.Page
    {
        public int inputType = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string uniqueid= Request.Params["regsionUnqiue"];
                int userid = -1;
                if (string.IsNullOrEmpty(uniqueid) 
                    || !int.TryParse(uniqueid, out userid))
                {
                    return;
                }
                ISysUserService userService = IoC.Resolve<ISysUserService>();
                var user= userService.Get(userid);
                if (user == null)
                    return;
                if (user.Head == 0)
                {
                    //无需输入电话 /qq
                    inputType = 0;
                }
                else
                {
                    //需要输入
                    inputType = 1;

                }


            }
        }
    }
}