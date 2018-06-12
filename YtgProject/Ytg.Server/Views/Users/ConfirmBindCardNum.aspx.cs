using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.Users
{
    public partial class ConfirmBindCardNum : BasePage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
       
                if (!string.IsNullOrEmpty(Request.Params["txtOpenUser"]))
                    this.txtOpenUser.Text = Request.Params["txtOpenUser"];
                if (!string.IsNullOrEmpty(Request.Params["txtZhiHang"]))
                    this.txtZhiHang.Text = Request.Params["txtZhiHang"];
                if (!string.IsNullOrEmpty(Request.Params["txtCardNum"]))
                    this.txtCardNum.Text = Request.Params["txtCardNum"];
                if (!string.IsNullOrEmpty(Request.Params["txtConfirmCardNum"]))
                    this.txtConfirmCardNum.Text = Request.Params["txtConfirmCardNum"];
                this.BindData();

               var at= Request.Params["at"];
                switch(at){
                    case "nb":
                        Alert("一个账户只能绑定同一个开户人姓名的银行卡！");
                        break;
                    case "cb":
                        Alert("同一个银行只允许绑定一张卡！");
                        break;
                }
            }
        }

        private void BindData()
        {
            //获取当前绑定卡总数
            ISysUserBankService userbankService = IoC.Resolve<ISysUserBankService>();
            var bindCount= userbankService.Where(c => c.UserId == CookUserInfo.Id).Count();
            if (bindCount >= 5)
            {
                Response.Write("<script>alert('error');</script>");
                Response.End();
                return;
            }
            meBindNum.Text = bindCount.ToString();
            ISysBankType bankType = IoC.Resolve<ISysBankType>();
            var banks = bankType.SelectAllBankType();//获取银行信息
            foreach (var item in banks)
            {
                if (item.BankDesc == "zfb" || item.BankDesc == "cft")
                    continue;
                drpBanks.Items.Add(new ListItem(item.BankName, item.Id + "_" + item.IsShowZhiHang));
                
            }
            if (!string.IsNullOrEmpty(Request.Params["drpBanks"]))
                drpBanks.SelectedValue = Request.Params["drpBanks"];
            //获取省
            var provinces = bankType.SelectAllProvinces();
            this.drpPro.DataTextField = "ProvinceName";
            this.drpPro.DataValueField = "ProvinceID";
            this.drpPro.DataSource = provinces;
            this.drpPro.DataBind();

            bool ispro=!string.IsNullOrEmpty(Request.Params["drpPro"]);
            if (ispro)
            {
                this.drpPro.SelectedValue = Request.Params["drpPro"];
            }

            if (provinces.Count > 0 || ispro)
            {

                int proid = ispro ? Convert.ToInt32(Request.Params["drpPro"]) : provinces.FirstOrDefault().ProvinceID;
                BindCitys(bankType, proid);
            }

            
        }

        private void BindCitys(ISysBankType bankType, int pid)
        {
            var citys = bankType.SelectAllCitys(pid);
            this.drpCity.DataTextField = "CityName";
            this.drpCity.DataValueField = "CityId";
            this.drpCity.DataSource = citys;
            this.drpCity.DataBind();

            if (!string.IsNullOrEmpty(Request.Params["drpCity"]))
                this.drpCity.SelectedValue = Request.Params["drpCity"];
        }
    }
}
