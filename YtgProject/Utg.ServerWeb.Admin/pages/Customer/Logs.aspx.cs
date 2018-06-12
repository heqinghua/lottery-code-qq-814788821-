using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Customer
{
    public partial class Logs : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
               
                //设置默认查询日期
                this.txtBegin.Text = Utils.GetNowBeginDateStr();
                this.txtEnd.Text = Utils.GetNowEndDateStr();

                this.Bind();
            }
        }

        private void Bind()
        {
            ISysLogService sysLogService = IoC.Resolve<ISysLogService>();

            string outBeginStr=string.Empty;
            string outEndStr = string.Empty;

            DateTime outBegin;
            DateTime outEnd;
            if (DateTime.TryParse(this.txtBegin.Text.Trim(), out outBegin) && DateTime.TryParse(this.txtEnd.Text.Trim(), out outEnd))
            {
                outBeginStr = outBegin.ToString("yyyy/MM/dd HH:mm:ss");
                outEndStr = outEnd.ToString("yyyy/MM/dd HH:mm:ss");
            }
            int totalcount = 0;
            var result = sysLogService.SelectLoginLogs(outBeginStr, outEndStr, Convert.ToInt32(drpuserType.SelectedValue), this.txtUserCode.Text.Trim(), this.pagerControl.CurrentPageIndex, ref totalcount);
            this.repList.DataSource = result;
            this.repList.DataBind();
            this.pagerControl.RecordCount = totalcount;
        }
        public string ToUserStateString(string state)
        {
            string toStr = "普通会员";
            switch (state)
            {
                case "Proxy":
                    toStr = "代理";
                    break;
                case "BasicProy":
                    toStr = "总代理";
                    break;
                case "Main":
                    toStr = "主管";
                    break;

            }
            return toStr;
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.Bind();
        }

        protected void pagerControl_PageChanged(object sender, EventArgs e)
        {
            this.Bind();
        }
    }
}