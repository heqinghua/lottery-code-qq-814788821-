using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Mobile.userCenter
{
    public partial class EditUser : BasePage
    {
        public double loginUserRebate;

        public int minPlayType = 0;


        public decimal UserMaxRemo = 0;//最高奖金


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                if (this.CookUserInfo.UserType == BasicModel.UserType.Main)
                {
                    this.panelZd.Visible = true;
                    this.panelOther.Visible = false;
                }
                else
                {
                    if (this.CookUserInfo.UserType == BasicModel.UserType.BasicProy)
                    {
                        playType.Visible = true;

                    }
                    else
                    {
                        if (this.CookUserInfo.PlayType == BasicModel.UserPlayType.P1800)
                            this.playType1800.Checked = true;
                        else
                            this.playType1700.Checked = true;
                    }
                }
            }
        }

        private void BindData()
        {
            //计算当前用户返点
            loginUserRebate =  Math.Round(Utils.GetUserRemo(CookUserInfo) , 2);
            this.minPlayType = CookUserInfo.PlayType == BasicModel.UserPlayType.P1800 ? 1800 : 1700;
            UserMaxRemo = Utils.GetUserQuota(this.CookUserInfo)-1800;
            //this.lbMeRemo.Text = loginUserRebate.ToString();

            //double maxValue = 0.0;
            //while (maxValue <= loginUserRebate)
            //{
            //    string text = Math.Round(maxValue, 2).ToString();
            //    if (text.Length == 1)
            //        text += ".0";
            //    drpBackNum.Items.Add(text);

            //    maxValue += 0.1;
            //}
            //drpBackNum.SelectedIndex = 0;
           
            //this.lbfanwei.Text = string.Format("%(可填范围：0.0-{0})", loginUserRebate);
            ////获取用户配额范围
            //ISysQuotaService quotaService = IoC.Resolve<ISysQuotaService>();
            //var result= quotaService.GetUserQuota(this.CookUserInfo.Id);
            //string itemsStr = "";
            //foreach (var item in result)
            //{
            //    itemsStr+= string.Format("<span style='margin-left:5px;color:#000;'>[{0}]：{1}个</span>", item.QuotaType, item.MaxNum<0?0:item.MaxNum);
            //}

            //this.ltKaihu.Text = itemsStr;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}