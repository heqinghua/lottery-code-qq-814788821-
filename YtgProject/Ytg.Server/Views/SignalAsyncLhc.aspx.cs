using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.Views
{
    public partial class SignalAsyncLhc : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string btimes = Request.QueryString["starttime"];
                string etimes = Request.QueryString["endtime"];

                DateTime? outBegin = null;
                DateTime? outEnd = null;
                DateTime begin;
                DateTime end;
                if (DateTime.TryParse(btimes, out begin))
                    outBegin = begin;
                if (DateTime.TryParse(etimes, out end))
                    outEnd = end;

                ILotteryIssueService issueService = IoC.Resolve<ILotteryIssueService>();
                var result = issueService.GetHisIssue(21, 50, outBegin, outEnd).OrderBy(x => x.EndTime);
                
                this.rep.DataSource = result;
                this.rep.DataBind();
            }
        }


        public string BuilderNumHtml(object items)
        {
            if (items == null)
                return string.Empty;
            string str = items.ToString();
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            str = str.Replace("+", ",");

            var arrStr = str.Split(',');
            if (arrStr.Length != 7)
                return str;
            string returnSTr = string.Format(@"<div class='red-ball_x'>{0}</div>
                                    <div class='red-ball_x'>{1}</div>
                                    <div class='green-ball_x'>{2}</div>
                                    <div class='red-ball_x'>{3}</div>
                                    <div class='green-ball_x'>{4}</div>
                                    <div class='red-ball_x'>{5}</div>
                                    <div class='sign'></div>
                                    <div class='green-ball_x'>{6}</div>", arrStr[0], arrStr[1], arrStr[2], arrStr[3], arrStr[4], arrStr[5], arrStr[6]);
            return returnSTr;
        }
    }
}