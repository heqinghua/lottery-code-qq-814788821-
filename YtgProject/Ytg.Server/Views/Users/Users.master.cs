using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.Users
{
    public partial class Users : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //获取是否开启摩宝支付
                ISysSettingService settingService = IoC.Resolve<ISysSettingService>();
                var fs = settingService.GetSetting("mobao_pay");
                if (fs != null)
                {
                    mb_pay.Visible = fs.Value == "0" ? true : false;
                }
                //智付
                var zf = settingService.GetSetting("zhifu_pay");
                if (zf != null)
                {
                    zf_pay.Visible = zf.Value == "0" ? true : false;
                }

                //my18
                var my18 = settingService.GetSetting("my18_pay");
                if (my18 != null)
                {
                    my_18.Visible = my18.Value == "0" ? true : false;
                }
            }
        }
    }
}