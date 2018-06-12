using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Business
{
    /// <summary>
    /// 提现记录管理
    /// </summary>
    public partial class MentionManager : System.Web.UI.Page
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
        /// 查询提现记录数据
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
            int recordCount = 0;

            //ISysUserBalanceDetailService sysUserBalanceDetailService = IoC.Resolve<ISysUserBalanceDetailService>();
            //List<MentionRecodVM> result = sysUserBalanceDetailService.SelectMentionRecod(code, seriaNo, beginDate,endDate,-1, pagerControl.CurrentPageIndex, ref recordCount);
            IMentionQueusService mentionQueusService = IoC.Resolve<IMentionQueusService>();
            List<Ytg.BasicModel.DTO.MentionDTO> result = mentionQueusService.SelectBy(code, seriaNo, beginDate, endDate, -1, -1, Convert.ToInt32(drpStates.SelectedValue), pagerControl.CurrentPageIndex, ref recordCount);
            this.repList.DataSource = result;
            this.repList.DataBind();
            this.pagerControl.RecordCount = recordCount;
        }

        public string GetManagerTime(object staeDesc, object date)
        {
            if (staeDesc == null || date == null)
                return string.Empty;
            if (staeDesc.ToString() == "处理中")
                return string.Empty;
            return Convert.ToDateTime(date).ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}