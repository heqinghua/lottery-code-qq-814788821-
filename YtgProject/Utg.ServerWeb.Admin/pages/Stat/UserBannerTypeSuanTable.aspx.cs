using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Stat
{
    public partial class UserBannerTypeSuanTable : BasePage
    {
        private string TraceTypeStr = "13,12,1,24";//充值

        protected void Page_Load(object sender, EventArgs e)
        {
          
            this.TraceTypeStr = Request.Params["tracetype"];
            if(string.IsNullOrEmpty(this.TraceTypeStr))
                this.TraceTypeStr = "1,24";//充值
            this.ltTitle.Text =Request.Params["title"];
            if (string.IsNullOrEmpty(this.ltTitle.Text))
                this.ltTitle.Text = "充值统计";
            this.ltTitle.Text = "" + this.ltTitle.Text;
            ltHead1.Text = Request.Params["head"];
            if (string.IsNullOrEmpty(ltHead1.Text))
                ltHead1.Text = "充值总额";

            if (!IsPostBack)
            {
                this.Bind();

            }
        }

        public string Formart(object occF) {
            if (null == occF)
                return string.Empty;
            var s = occF.ToString();
            if (s.Length == 6)
            {
                return s.Insert(4, "-");
            }
            return s;
        }

        public string FilterType()
        {
            if (ViewState["FilterType"] == null)
                return "0";
            return ViewState["FilterType"].ToString();
        }

        private void Bind()
        {
            DateTime? beginDate = null;
            DateTime? endDate = null;
            DateTime b;
            DateTime e;
            if (!DateTime.TryParse(this.txtBeginDate.Text.Trim(), out b) || !DateTime.TryParse(this.txtEndDate.Text.Trim(), out e))
            {
                if (this.FilterType() == "0")
                {
                    //按天
                    //string year = DateTime.Now.ToString("yyyy");
                    //string month = DateTime.Now.ToString("MM");
                    beginDate = Utils.GetNowBeginDate();//  Convert.ToDateTime(year + "-" + month + "-01 03:00:00");
                    endDate = Utils.GetNowEndDate(); //Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 03:00:00");
                }
                else
                {
                    //年
                    beginDate = DateTime.Now.AddYears(-1);
                    endDate = DateTime.Now;
                }
            }
            else
            {
                beginDate = Convert.ToDateTime(b.ToString("yyyy/MM/dd") + " 00:03:00");
                endDate = Convert.ToDateTime(e.ToString("yyyy/MM/dd") + " 00:03:00");
            }
            ICountDataService dataService = IoC.Resolve<ICountDataService>();

            this.repList.DataSource = dataService.FilterSp_userBannerTypeSuanTable(beginDate.Value, endDate.Value, this.TraceTypeStr, Convert.ToInt32(this.FilterType()));
            this.repList.DataBind();
            this.repList.Visible = true;

        }

        protected void lkDay_Click(object sender, EventArgs e)
        {
            ViewState["FilterType"] = "0";
            this.ltTitle.Text = "日" + this.ltTitle.Text;
            this.ltHead.Text = "统计日期";
            this.Bind();
        }

        protected void lkMon_Click(object sender, EventArgs e)
        {
            ViewState["FilterType"] = "1";
            this.ltTitle.Text = "月" + this.ltTitle.Text;
            this.ltHead.Text = "统计月份";

            this.Bind();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.Bind();
        }
    }
}