using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Business
{
    public partial class RechargeManagerUser : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtBeginDate.Text = Utils.GetNowBeginDateStr();
                this.txtEndDate.Text = Utils.GetNowEndDateStr();
                this.InintFilter();

            }
        }

        #region 系统条件初始化

        private void InintFilter()
        {
            Bind();
        }

        #endregion


        /// <summary>
        /// 查询充值记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Bind();
        }

        /// <summary>
        /// 页码改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pagerControl_PageChanged(object sender, EventArgs e)
        {
            Bind();
        }

        /// <summary>
        /// 提现数据列表绑定
        /// </summary>
        private void Bind()
        {
            string code = this.txtUserCode.Text.Trim(); //用户编号
            string seriaNo = this.txtSeriaNo.Text.Trim(); //业务单号
            string beginDate = this.txtBeginDate.Text.Trim(); //业务开始日期
            string endDate = this.txtEndDate.Text.Trim();     //业务截止日期
            int isCompled = Convert.ToInt32(this.drpState.SelectedValue);//状态
            int recordCount = 0;

            DateTime begin;
            DateTime end;

            DateTime? b=null;
            DateTime? e=null;

            if(DateTime.TryParse(beginDate,out begin) && DateTime.TryParse(endDate,out end)){
                b=begin;e=end;
            }
            
            
            ISysUserBalanceDetailService userBalanceDetailsService = IoC.Resolve<ISysUserBalanceDetailService>();

            var result = userBalanceDetailsService.FilterUserBalanceDetails(seriaNo, code, b, e, Convert.ToInt32(this.drpState.SelectedValue), null, null, Convert.ToInt32(drpType.SelectedValue), pagerControl.CurrentPageIndex, pagerControl.PageSize, ref recordCount);

            this.repList.DataSource = result;
            this.repList.DataBind();
            this.pagerControl.RecordCount = recordCount;
        }
    }
}