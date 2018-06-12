using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.UserGroup
{
    public partial class AutoRegistSetting : BasePage
    {
        public string registUrl = string.Empty;

        public int minPlayType = 0;

      
        public string curBackNum="0";

        public decimal UserMaxRemo = 0;//最高奖金

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
                this.BindCurBackNum();
            }
        }

        private void BindData()
        {
            IDnsService dnsServices = IoC.Resolve<IDnsService>();
            var result = dnsServices.GetAll().Where(c=>c.IsShowAutoRegist).ToList();
            if (CookUserInfo.Head == 0)
            {
                foreach (var item in result)
                {
                    item.SiteDnsUrl += "/adduser.aspx?usercode=" + CookUserInfo.Id;
                }

                result.Add(new BasicModel.SiteDns()
                {
                    SiteDnsUrl = "http://" + Request.Url.Authority + "/adduser.aspx?usercode=" + CookUserInfo.Id+"&from=augist"
                });
            }
            else {
                foreach (var item in result)
                {
                    item.SiteDnsUrl += "/adduser.aspx?usercode=" + CookUserInfo.Id + "&from=augist";
                }

                result.Add(new BasicModel.SiteDns()
                {
                    SiteDnsUrl = "http://" + Request.Url.Authority + "/adduser.aspx?usercode=" + CookUserInfo.Id + "&from=augist"
                });
            }
            this.fe_text12.DataTextField = "SiteDnsUrl";
            this.fe_text12.DataValueField = "SiteDnsUrl";
            this.fe_text12.DataSource = result;
            this.fe_text12.DataBind();

            this.minPlayType = CookUserInfo.PlayType == BasicModel.UserPlayType.P1800 ? 1800 : 1700;
        }

        private void BindCurBackNum()
        {
            ////获取用户当前设置值
            UserMaxRemo = Utils.GetUserQuota(this.CookUserInfo);
            ISysUserService sysUserServices = IoC.Resolve<ISysUserService>();
            curBackNum = (Utils.GetUserRemo(CookUserInfo) - sysUserServices.GetAutoRegRebate(CookUserInfo.Id)).ToString();
        }

    

        protected void btnSave_Click(object sender, EventArgs e)
        {
            double back =0.0;
            if (!double.TryParse(Request.Form["hidValue"].Replace("%",""), out back))
            {
                Alert("请选择奖金组！");
                return;
            }
            var cd= CookUserInfo.Rebate;
            ISysUserService sysUserServices = IoC.Resolve<ISysUserService>();
            bool isCompled = sysUserServices.UpdateAutoRegRebate(CookUserInfo.Id, Math.Round(back,1));
            if (isCompled)
            {
                Alert("保存设置成功!");
            }
            else
            {
                Alert("保存设置失败，请稍后再试!");
            }
            this.BindData();
            this.BindCurBackNum();
        }
    }
}