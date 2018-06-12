using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Manager
{
    public partial class AccountLog : BasePage
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

            ISysAccountLogsService sysAccountLogsService = IoC.Resolve<ISysAccountLogsService>();

            string outBeginStr = string.Empty;
            string outEndStr = string.Empty;

            DateTime outBegin;
            DateTime outEnd;
            if (DateTime.TryParse(this.txtBegin.Text.Trim(), out outBegin) && DateTime.TryParse(this.txtEnd.Text.Trim(), out outEnd))
            {
                outBeginStr = outBegin.ToString("yyyy/MM/dd HH:mm:ss");
                outEndStr = outEnd.ToString("yyyy/MM/dd HH:mm:ss");
            }
            int totalcount = 0;
            var result = sysAccountLogsService.GetLoginLogs(outBeginStr, outEndStr, this.txtUserCode.Text.Trim(), this.pagerControl.CurrentPageIndex, ref totalcount);
            this.repList.DataSource = result;
            this.repList.DataBind();
            this.pagerControl.RecordCount = totalcount;
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