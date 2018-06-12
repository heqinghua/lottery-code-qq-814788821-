using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages
{
    public partial class SysUserBanks : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtCode.Text = Request.Params["code"];
                ISysBankType banksType = IoC.Resolve<ISysBankType>();
                var result= banksType.GetAll().ToList();
                if (result == null)
                {
                    result = new List<Ytg.BasicModel.SysBankType>();
                }
                result.Insert(0, new Ytg.BasicModel.SysBankType() { BankName = "全部银行" });
                this.drpBanks.DataTextField = "BankName";
                this.drpBanks.DataSource = result;
                this.drpBanks.DataBind();
                Bind();
            }
        }

        private void Bind()
        {

            string code = txtCode.Text.Trim();
            string bankNames = this.drpBanks.SelectedIndex == 0 ? "" : this.drpBanks.SelectedValue;

            ISysUserBankService sysUserBakService = IoC.Resolve<ISysUserBankService>();
            int totalCount = 0;
            var result = sysUserBakService.FilterUserBanks(code,-1, bankNames, txtOwenName.Text.Trim(), txtCardNum.Text.Trim(), "", "", pagerControl.CurrentPageIndex, ref totalCount);
            this.repList.DataSource = result;
            this.repList.DataBind();
        }

        protected void lbUnLock_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;
            //检查是否有未处理的提现记录
            IMentionQueusService queUsService = IoC.Resolve<IMentionQueusService>();
            var nowManCount= queUsService.Where(x => x.Status == 0).Count();//0 排队中 1提现成功 2提现失败 3 用户撤销
            if (nowManCount > 0)
            {
                JsAlert("有尚未处理的提现请求，解绑失败！");
                return;
            }
            ISysUserBankService sysUserBakService = IoC.Resolve<ISysUserBankService>();
            sysUserBakService.Delete(Convert.ToInt32(e.CommandArgument));
            sysUserBakService.Save();
            JsAlert("解绑成功！");
            this.Bind();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.Bind();
        }

        protected void pagerControl_PageChanged(object sender, EventArgs e)
        {
            this.Bind();
        }

        protected void lbLock_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;
            //解锁银行卡
            ISysUserService userService = IoC.Resolve<ISysUserService>();
            if (userService.ManagerUnLockUserCards(Convert.ToInt32(e.CommandArgument)))
            {
                JsAlert("解锁成功！");
            }
            else
            {
                JsAlert("解锁失败！");
            }
            this.Bind();
        }
    }
}