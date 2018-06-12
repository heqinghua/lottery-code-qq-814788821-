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
    /// <summary>
    /// 提现请求管理
    /// </summary>
    public partial class MentionList : BasePage
    {
        public decimal ShenHeMonery = 5000;//提现金额超过需审核

        protected void Page_Load(object sender, EventArgs e)
        {
            //转账操作
            if (Request["action"] =="ajax")
            {
                TransferAccount();
                return;
            }
            if (!IsPostBack)
            {
               // this.txtBeginDate.Text = Utils.GetNowBeginDateStr();
                //this.txtEndDate.Text = Utils.GetNowEndDateStr();
                this.InintFilter();
            }

        }

        public string SubSDes(object stateDesc)
        {
            if (stateDesc == null)
                return string.Empty;
            string descStr = stateDesc.ToString();
            if (descStr.Length > 3)
                return descStr.Substring(1, 2);
            return descStr;
        }

        /// <summary>
        /// 转账处理
        /// </summary>
        private void TransferAccount()
        {
            int id = Convert.ToInt32(Request["id"].ToString());
            int status = Convert.ToInt32(Request["status"].ToString());
            string errorMsg = Request["errorMsg"].ToString();
            decimal realAmt = 0;
            decimal poundage = 0;
            if (!decimal.TryParse(Request.Params["poundage"], out poundage))
                poundage = 0;
           // "realAmt": $("#realAmt").val(), "poundage": $("#poundage").val()
            if (!decimal.TryParse(Request.Params["realAmt"], out realAmt))
            {

                Response.Write("2");
            }
            else
            {

                IMentionQueusService mentionQueusService = IoC.Resolve<IMentionQueusService>();
                bool result = mentionQueusService.MentionDone(id, status, realAmt, poundage, errorMsg);
                if (result)
                    Response.Write("1");
                else
                    Response.Write("2");
            }
            Response.End();
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
            ISysSettingService settingService=IoC.Resolve<ISysSettingService>();
            var sh= settingService.GetAll().Where(x=>x.Key=="QLZHGZ").FirstOrDefault();
            if (sh != null)
            {
                if (!decimal.TryParse(sh.Value, out ShenHeMonery))
                    ShenHeMonery = 5000;
            }
            
            string code = this.txtUserCode.Text.Trim(); //用户编号
            string seriaNo = "";// this.txtSeriaNo.Text.Trim(); //业务单号
            string beginDate = this.txtBeginDate.Text.Trim(); //业务开始日期
            string endDate = this.txtEndDate.Text.Trim();     //业务截止日期
            decimal beginMonery = -1;
            decimal endMoner = -1;
            if (!decimal.TryParse(this.txtBeginMonery.Text, out beginMonery) || !decimal.TryParse(this.txtEndMonery.Text.Trim(), out endMoner))
            {
                beginMonery = -1;
                endMoner = -1;
            }
            int type = Convert.ToInt32(this.drpStates.SelectedValue);
            int recordCount = 0;

            IMentionQueusService mentionQueusService = IoC.Resolve<IMentionQueusService>();
            List<Ytg.BasicModel.DTO.MentionDTO> result = mentionQueusService.SelectBy(code, seriaNo, beginDate, endDate, beginMonery, endMoner, type, pagerControl.CurrentPageIndex, ref recordCount);
            this.repList.DataSource = result;
            this.repList.DataBind();
            this.pagerControl.RecordCount = recordCount;
        }

        public bool getStet(object obj) {
            if (obj == null)
                return false;
            if (obj.ToString() == "1")
                return true;
            return false;
        }

        /// <summary>
        /// 获取转账动作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mentionAmt">提现金额</param>
        /// <param name="audit">是否审核</param>
        /// <returns></returns>
        public string GetTransferAccountAction(int id, decimal mentionAmt, int audit,string isEnableDesc)
        {
            string result = "";

            //权限验证
            if(actionModel.TransferAccount==false || isEnableDesc != "处理中")
                return result;

            //如果提现金额小于五千暂不需审核
            if (mentionAmt < 5000 || (mentionAmt>=5000 && audit==1))
            {
                result = "<a href=\"javascript:ShowDialog(" + id + ")\">处理</a>";
            }
            else if (audit == 0)
            {
                result = "待审核";
            }
            return result;
        }

        protected void btnAudit_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;
            int id = Convert.ToInt32(e.CommandArgument);
            IMentionQueusService mentionQueusService = IoC.Resolve<IMentionQueusService>();
            if (mentionQueusService.Audit(id))
            {
                this.Bind();
                JsAlert("审核成功！");
            }
            else
            {
                JsAlert("审核失败,请稍后再试！");
            }
        }


        /// <summary>
        /// 具有审核权限
        /// </summary>
        /// <returns></returns>
        public bool HasAudit(object Audit, object MentionAmt)
        {
            if (null == Audit || MentionAmt == null)
                return false;

            return (int)Eval("Audit") == 0 && (decimal)Eval("MentionAmt") >= ShenHeMonery && actionModel.Audit;
        }

        protected void btnRef_Click(object sender, EventArgs e)
        {

        }
    }
}