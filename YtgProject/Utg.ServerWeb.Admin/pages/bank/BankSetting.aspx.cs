using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.bank
{
    public partial class BankSetting : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.BindList();

        }

        private void BindList()
        {
            ICompanyBankService userService = IoC.Resolve<ICompanyBankService>();

            int? bankid = null;
            int? status = null;
            
            int totalCount = 0;
            var result = userService.CompanyBankSelectBy(bankid, status, pagerControl.CurrentPageIndex, ref totalCount);
            this.pagerControl.RecordCount = totalCount;
            this.repList.DataSource = result;
            this.repList.DataBind();
        }

       
        protected void pagerControl_PageChanged(object sender, EventArgs e)
        {
            this.BindList();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.BindList();
        }

        
        protected void btnRef_Click(object sender, EventArgs e)
        {
            this.BindList();
        }

        //禁用
        protected void btnDisabled_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;
            int id = Convert.ToInt32(e.CommandArgument);
            ICompanyBankService userService = IoC.Resolve<ICompanyBankService>();
            userService.SetStatus(id, false);
            this.BindList();
            JsAlert("禁用成功！");
        }

        //启用
        protected void btnEabled_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;
            int id = Convert.ToInt32(e.CommandArgument);
            ICompanyBankService userService = IoC.Resolve<ICompanyBankService>();
            userService.SetStatus(id, true);
            this.BindList();
            JsAlert("启用成功！");
        }

        //删除
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;
            int id = Convert.ToInt32(e.CommandArgument);
            ICompanyBankService userService = IoC.Resolve<ICompanyBankService>();
            userService.Delete(id);
            this.BindList();
            JsAlert("删除成功！");
        }

        public bool GetVis(object state) {
            if (null == state)
                return true;
            return state.ToString() == "禁用" ? false : true;
        }

        /// <summary>
        /// 设置入金账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbSetIncomingBank_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;
            int id = Convert.ToInt32(e.CommandArgument);
            ICompanyBankService userService = IoC.Resolve<ICompanyBankService>();
            userService.SetIncomingBank(id);
            this.BindList();
            JsAlert("设置成功！");
        }

        protected void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Label lab = e.Item.FindControl("BankId") as Label;
                //int bankId = Convert.ToInt32(lab.Text);
                //if (bankId == 20 || bankId == 21)
                //{
                //    lab.Visible = true;
                //}
                //else
                //{
                //    //lbtn.Visible = false;
                //}

                //DataRowView rowItem = e.Item.DataItem as DataRowView;
                //if (rowItem != null)
                //{
                //    LinkButton lbtn = e.Item.FindControl("lbSetIncomingBank") as LinkButton;
                //    int bankId = Convert.ToInt32(lbtn.ToolTip);
                //    if (bankId == 20 || bankId == 21)
                //    {
                //        lbtn.Visible = true;
                //    }
                //    else
                //    {
                //        lbtn.Visible = false;
                //    }
                //}
            }
        }
    }
}