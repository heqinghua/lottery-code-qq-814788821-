using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ytg.ServerWeb.Mobile.pay
{
    public partial class Payment : System.Web.UI.Page
    {
        public decimal Min = 50;//充值最小值
        public decimal Max = 2000;//充值最大值
        protected void Page_Load(object sender, EventArgs e)
        {
            Min = Comm.Utils.ZfbMin;
            Max = Comm.Utils.ZfbMax;
        }
    }
}