using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Core.Service;
using Ytg.ServerWeb.Page.PageCode.Users;

namespace Ytg.ServerWeb.Mobile.userCenter
{
    public partial class UserPoints :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { BindData();  }
        }

        private void InintRemo(double userRemo,double parentRemo,CookUserInfo user)
        {
            
            double curValue = userRemo;
            double curMax = Utils.GetMaxRemo(user);
            while (true)
            {
                 curValue = curValue += 0.1;
                if (curValue <= (curMax-parentRemo))
                {
                    drpRemo.Items.Add(new ListItem(Math.Round(curValue, 1).ToString(), Math.Round(curMax - curValue, 1).ToString()));
                }
                else {
                    break;
                }
            }

          
        }

        private void BindData()
        {
            int uid;
            if (!int.TryParse(Request.Params["uid"], out uid))
            {
                Response.End();
                return;
            }

            ISysUserService userServices = IoC.Resolve<ISysUserService>();
            var user = userServices.Get(uid);
            if (null == user)
            {
                Response.End();
                return;
            }
            var userCookieUser = new CookUserInfo()
            {
                PlayType = user.PlayType
            };

            var maxRemo = Math.Round((Utils.GetMaxRemo(userCookieUser) - user.Rebate), 1);
            this.lbUser.Text = user.Code;
            this.lbLevel.Text = maxRemo.ToString();
            //获取用户余额
            ISysUserBalanceService userBalances = IoC.Resolve<ISysUserBalanceService>();
            lbMonery.Text = userBalances.GetUserBalance(uid).UserAmt.ToString();

            if (user.Rebate<=CookUserInfo.Rebate)
            {
                Label1.Text = user.Code;
                Label2.Text = lbMonery.Text;
                Label3.Text = this.lbLevel.Text;
                us.Visible = true;
                ct.Visible = false;
                return;
            }

            //获取上级级最大配额
            var parentmaxRemo = userServices.GetParentMaxRebate(uid);

            lbChildMax.Text = parentmaxRemo == -1 ? "0.0" : Math.Round(Utils.GetMaxRemo(userCookieUser) - parentmaxRemo.Value, 1).ToString();

            BuilderNowQuotaHtml(userCookieUser);
            InintRemo(maxRemo, parentmaxRemo.Value, userCookieUser);
        }

        /// <summary>
        /// 构建配额
        /// </summary>
        private void BuilderNowQuotaHtml(CookUserInfo userCookie)
        {

            ISysQuotaService quotaService = IoC.Resolve<ISysQuotaService>();
            var result = quotaService.GetUserQuotaMax(CookUserInfo.Id);
            if (result == null || result.Count() < 1)
                result = new List<BasicModel.SysQuota>();
          
            StringBuilder builder = new StringBuilder();
            StringBuilder builder1 = new StringBuilder();
            builder.Append("<tr>");
            builder1.Append("<tr>");
            builder.Append("<th>返点级别</th>");
            builder1.Append("<td style='text-align:center;'>配额个数</td>");

            foreach (var item in result)
            {
                builder.Append("<th >" + (userCookie.PlayType == 0 ? item.QuotaType : Utils.ParseShowRebateName1700(item.QuotaType)) + "</th>");
                builder1.Append("<td style='text-align:center;'>" + item.MaxNum + "</td>");
            }
            builder.Append("</tr>");
            builder1.Append("</tr>");
            string resStr = "<table style='' border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"grayTable\">" + builder.ToString() + builder1.ToString() + "</table>";
            ltmePeie.Text = resStr;
        }


        /// <summary>
        /// 构建升级规则  3天
        /// </summary>
        public string Builder3Rule()
        {
            double userMaxRebate=Utils.GetMaxRemo(CookUserInfo);
            var _3result=PointsAttributeHelper.GetDayResult(3, CookUserInfo.Rebate);
            string xx = BuilderTable(3, _3result, userMaxRebate) + BuilderTable(7, PointsAttributeHelper.GetDayResult(7, CookUserInfo.Rebate), userMaxRebate) + BuilderTable(10, PointsAttributeHelper.GetDayResult(10, CookUserInfo.Rebate), userMaxRebate);
            return xx;
        }

        private string BuilderTable(int day, List<AddPointsAttribute> result, double userMaxRebate)
        {
            if (result.Count < 1)
                return string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.Append(" <table style='' border='0' cellspacing='0' cellpadding='0' class='ltable'>");
            builder.Append("<tbody>");
            builder.Append(" <tr><td colspan='5' style='text-align:center;'><b><span style='color:red;'>"+day+"天</span>量升点标准</b></td></tr>");
            builder.Append("<tr><th >代理级别</th><th>量标准</th><th>时时彩</th><th>11选5</th><th>3D/P3</th></tr>");
            /**content*/
            foreach (var item in result)
            {
                builder.Append("<tr><td align='center'>所有代理</td>");
                builder.Append("<td align='center'><b>" + item.SalesStand + "</b></td>");
                builder.Append("<td align='center'><b>" + ReplaceShowText(userMaxRebate,item.SSC) + "</b></td>");
                builder.Append("<td align='center'><b>" + ReplaceShowText((CookUserInfo.PlayType== BasicModel.UserPlayType.P1800?6.2:11.2),item.Eleven) + "</b></td>");
                builder.Append("<td align='center'><b>" + ReplaceShowText((CookUserInfo.PlayType == BasicModel.UserPlayType.P1800 ? 6.2 : 11.2), item.Td) + "</b></td>");
                builder.Append("</tr>");
            }
            /**content end */
            builder.Append("</tbody>");
            builder.Append("</table>");
            return builder.ToString();

        }

        private string ReplaceShowText(double userMaxRebate,string showText)
        {
            if (string.IsNullOrEmpty(showText))
                return showText;
            var array=showText.Split('(');
            var showRebate = Math.Round(userMaxRebate - Convert.ToDouble(array[0]), 1);
            return showRebate + "(" + array[1];
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}