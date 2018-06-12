using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;

namespace Ytg.ServerWeb
{
    public partial class AccountCenter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                Account.Text = "";
                UserName.Text = "";
            }
          //  Hasher hasher = new Hasher();
           // var c= hasher.Encrypt("aspNetCompatibilityEnabled|bindingConfiguration");
//            aspNetCompatibilityEnabled|bindingConfiguration
        }
    }
}