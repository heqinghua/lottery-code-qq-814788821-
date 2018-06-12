using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.bank
{
    public partial class EditBankSetting : BasePage
    {

        private int user_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!int.TryParse(Request.Params["id"], out user_id))
                user_id = -1;
            if (!IsPostBack)
            {
                ISysBankType bankTypeServices = IoC.Resolve<ISysBankType>();
                var result=bankTypeServices.GetAllBankType().ToList();

                drpBanks.DataTextField = "BankName";
                drpBanks.DataValueField = "Id";
                drpBanks.DataSource = result;
                drpBanks.DataBind();
                drpBanks.SelectedIndex = 0;

                this.Bind();
            }
        }

        private void Bind()
        {
            if (user_id <= 0)
                return;
            ICompanyBankService userService = IoC.Resolve<ICompanyBankService>();
            var item = userService.Get(this.user_id);
            if (null == item)
            {
                Response.End();
                return;
            }
            this.txtBankNo.Text = item.BankNo;
            this.txtProvince.Text = item.Province;
            this.txtBranch.Text = item.Branch;
            this.txtBankOwner.Text = item.BankOwner;
            this.cmbStatus.SelectedIndex = item.IsEnable == true ? 0 : 1;
            this.drpBanks.SelectedValue = item.BankId.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CompanyBank item = null;
            ICompanyBankService userService = IoC.Resolve<ICompanyBankService>();
            if (user_id > 0)//修改
                item = userService.Get(this.user_id);
            else
            {
                item = new CompanyBank();
            }

            if (string.IsNullOrEmpty(this.txtBankNo.Text))
            {
                Warning("请填写银行账号！");
                return;
            }
            if (string.IsNullOrEmpty(this.txtBankOwner.Text))
            {
                Warning("请填写开户人！");
                return;
            }

            var utag = this.drpBanks.SelectedValue;
            item.BankId = int.Parse(utag.ToString());
            item.BankNo = this.txtBankNo.Text;
            item.BankOwner = this.txtBankOwner.Text;
            item.Branch = this.txtBranch.Text;
            item.IsEnable = cmbStatus.SelectedValue == "1" ? true : false;
            item.OccDate = DateTime.Now;
            item.Province = this.txtProvince.Text;

            bool isCompled = false;
            if (user_id > 0)
            {
                userService.Save();
                isCompled = true;
            }
            else
            {
                isCompled = userService.Add(item);
            }
            if (isCompled)
            {
                JsAlert("保存成功！", true);
                this.txtBankNo.Text = string.Empty;
                this.txtBankOwner.Text = string.Empty;
                this.txtBranch.Text = string.Empty;
                this.txtProvince.Text = string.Empty;
            }
            else
            {
                JsAlert("保存失败，请稍后再试！");
                //
            }
        }
    }
}