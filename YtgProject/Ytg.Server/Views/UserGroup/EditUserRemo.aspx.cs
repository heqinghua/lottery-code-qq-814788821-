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
    public partial class EditUserRemo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.BindData();
        }

        private void BindData()
        {
            int childid;
            if (!int.TryParse(Request.Params["id"], out childid))
            {
                Response.End();
                return;
            }
            //获取用户返点
            ISysUserService sysUserServices = IoC.Resolve<ISysUserService>();
            var user = sysUserServices.Get(childid);
             if (user==null)
             {
                 Response.End();
                 return;
             }
             lbAccount.Text = user.Code;
             lbNickName.Text = user.NikeName;
             lbLevel.Text = (Utils.MaxRemo - user.Rebate).ToString();
            //计算当前用户返点
            double loginUserRebate = Math.Round((Utils.MaxRemo -CookUserInfo.Rebate), 2);
            this.lbMeRemo.Text = loginUserRebate.ToString();

            double boxRebate = Math.Round(loginUserRebate - (Utils.MaxRemo - user.Rebate),2);
            double maxValue = 0.0;
            while (maxValue <= boxRebate)
            {
                string text = Math.Round(maxValue, 2).ToString();
                if (text.Length == 1)
                    text += ".0";
                drpBackNum.Items.Add(text);

                maxValue += 0.1;
            }
            drpBackNum.SelectedIndex = 0;

            this.lbfanwei.Text = string.Format("%(可填范围：0.0-{0})", boxRebate);
            //获取用户配额范围
            ISysQuotaService quotaService = IoC.Resolve<ISysQuotaService>();
            var result = quotaService.GetUserQuota(this.CookUserInfo.Id);
            string itemsStr = "";
            foreach (var item in result)
            {
                itemsStr += string.Format("<span style='margin-left:5px;color:#000;'>[{0}]：{1}个</span>", item.QuotaType, item.MaxNum < 0 ? 0 : item.MaxNum);
            }

            this.ltKaihu.Text = itemsStr;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}