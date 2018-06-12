using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages
{
    public partial class Charts : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                this.Bind();
            }
        }

        private void Bind()
        {
            ISysLogService sysLogService = IoC.Resolve<ISysLogService>();

            string beginDate = DateTime.Now.ToString("yyyy/MM") + "/01 00:00:00";
            string endDate = Convert.ToDateTime(beginDate).AddMonths(1).ToString("yyyy/MM/dd ") + " 23:59:59";
            var result = BuilderUserVisit(sysLogService, beginDate, endDate);//天
            this.rprDay.DataSource = result;
            this.rprDay.DataBind();


            beginDate = DateTime.Now.ToString("yyyy") + "/01/01 00:00:00";
            endDate = Convert.ToDateTime(beginDate).AddYears(1).ToString("yyyy/MM/dd ") + " 23:59:59";
            var result1 = BuilderUserVisit(sysLogService, beginDate, endDate, 2);//月
            this.rptMonth.DataSource = result1;
            this.rptMonth.DataBind();

            var result2 = BuilderUserVisit(sysLogService, beginDate, endDate, 3);//年
            this.repYear.DataSource = result2;
            this.repYear.DataBind();
        }

        /// <summary>
        /// 用户访问天统计
        /// </summary>
        private List<SysUserLoginStatisticsVM> BuilderUserVisit(ISysLogService sysLogService, string beginDate, string endDate, int type = 1)
        {


            var result = sysLogService.SelectLoginLogStatistics(type, beginDate, endDate);
            return result;
            //StringBuilder builder = new StringBuilder();

            //foreach (var item in result)
            //{
            //    string period = item.OccDate;
            //    builder.Append("{");
            //    builder.AppendFormat("period:'{0}',会员: {1},代理:{2},总代理:{3},", period, item.MemberCount, item.ProxyCount, item.BasicProyCount);
            //    builder.Append("},");
            //}
            //return builder.ToString();
        }

        protected void lkDay_Click(object sender, EventArgs e)
        {
            rowYear.Visible = false;
            rowMonth.Visible = false;
            rowDay.Visible = true;
        }

        protected void lkMon_Click(object sender, EventArgs e)
        {
            rowYear.Visible = false;
            rowMonth.Visible = true;
            rowDay.Visible = false;
        }

        protected void lkYear_Click(object sender, EventArgs e)
        {
            rowYear.Visible = true;
            rowMonth.Visible = false;
            rowDay.Visible = false;
        }
    }
}