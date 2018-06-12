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
    public partial class EditBankBaseSetting : BasePage
    {
        private int user_id = 0;
        public string checkedText = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!int.TryParse(Request.Params["id"], out user_id))
                user_id = -1;
            if (!IsPostBack)
            {
                ISysBankType bankTypeServices = IoC.Resolve<ISysBankType>();
                var result = bankTypeServices.GetAllBankType().ToList();
                this.Bind();
            }
        }

        private void Bind()
        {
            if (user_id <= 0)
                return;
            ISysBankType userService = IoC.Resolve<ISysBankType>();
            var item = userService.Get(this.user_id);
            if (null == item)
            {
                Response.End();
                return;
            }
            this.txtBankName.Text = item.BankName;
            this.txtBankDesc.Text = item.BankDesc;
            this.txtBankWebUrl.Text = item.BankWebUrl ?? "";
            this.drpZhiHang.SelectedValue = item.IsShowZhiHang ? "1" : "0";
            this.drpAuto.SelectedValue = item.OpenAutoRecharge ? "1" : "0";
            this.drpKuaiHang.SelectedValue = item.IsInterBank ? "1" : "0";
            this.checkedText = item.BankLogo;
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SysBankType item = null;
            ISysBankType userService = IoC.Resolve<ISysBankType>();
            if (user_id > 0)//修改
                item = userService.Get(this.user_id);
            else
            {
                item = new SysBankType();
            }
            item.BankName=this.txtBankName.Text;
            item.BankDesc=this.txtBankDesc.Text;
            item.BankWebUrl=this.txtBankWebUrl.Text;
            item.IsShowZhiHang=this.drpZhiHang.SelectedValue =="1"?true: false;
            item.OpenAutoRecharge = this.drpAuto.SelectedValue == "1" ? true : false;
            item.IsInterBank = this.drpKuaiHang.SelectedValue == "1" ? true : false;
            item.OpTime = DateTime.Now;
            var logo=Request.Params["bkrad"];
            if (logo != "6")
                item.BankLogo = logo;
            else
                item.BankLogo = this.other.Text.Trim();
          
            bool isCompled = false;
            if (user_id > 0)
            {
                userService.Save();
                isCompled = true;
            }
            else
            {
                 userService.Create(item);
                 userService.Save();
                 isCompled = true;
            }
            if (isCompled)
            {
                JsAlert("保存成功！", true);
                
            }
            else
            {
                JsAlert("保存失败，请稍后再试！");
                //
            }
        }
    }
}