using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Core.Service;
using Ytg.ServerWeb.Page.PageCode.Users;

namespace Ytg.ServerWeb.Mobile.userCenter
{
    public partial class Recovery :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { BindData(); }
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
            var maxRemo = (Utils.GetMaxRemo(userCookieUser) - user.Rebate);
            //获取用户类型， 1800还是1700 

            if (user.Rebate >= Utils.MinRemo1800_1700)
            {
                noneDiv.Visible = true;
                otherDiv.Visible = false;
            }
            this.lbUser.Text = user.Code;
            this.lbLevel.Text = Math.Round(maxRemo,1).ToString();
            //获取用户余额
            ISysUserBalanceService userBalances = IoC.Resolve<ISysUserBalanceService>();
            lbMonery.Text = userBalances.GetUserBalance(uid).UserAmt.ToString();
            //获取下级最大配额
            var minRemo = userServices.GetChildMaxRebate(uid);
            minRemo = minRemo < 0 ? Utils.GetMaxRemo(userCookieUser) : minRemo;
            minRemo = (Utils.GetMaxRemo(userCookieUser) - minRemo);
            minRemo = Math.Round(minRemo.Value, 1);
            lbChildMax.Text = minRemo == -1 ? "0.0" : minRemo.ToString();
            //获取所有配额
            BuilderNowQuotaHtml(uid, userCookieUser);
            //构建降点级别
            //maxRemo
            double maxValue = maxRemo-0.1;
            while (maxValue >= minRemo)
            {
                var itemValue = Math.Round(maxValue, 2);

                string text = itemValue.ToString();
                if (text.Length == 1)
                    text += ".0";
                var nowValue = Math.Round((Utils.GetUserRemo(userCookieUser) - itemValue), 1);
                drpRemo.Items.Add(new ListItem(text, nowValue.ToString()));
                maxValue -= 0.1;
            }
            drpRemo.SelectedIndex = 0;
        }

        /// <summary>
        /// 构建我的配额
        /// </summary>
        private void BuilderNowQuotaHtml(int uid,CookUserInfo userInfo)
        {
            
            ISysQuotaService quotaService = IoC.Resolve<ISysQuotaService>();
            var result = quotaService.GetUserQuotaMax(uid);
            if (result == null || result.Count() < 1)
                return;

            lb.Visible = false;
            chkhuishou.Visible = true;
            StringBuilder builder = new StringBuilder();
            StringBuilder builder1 = new StringBuilder();
            builder.Append("<tr>");
            builder1.Append("<tr>");
            builder.Append("<th>返点级别</th>");
            builder1.Append("<td style='text-align:center;'>配额个数</td>");

            foreach (var item in result)
            {
                builder.Append("<th >" + (userInfo.PlayType==0?item.QuotaType:Utils.ParseShowRebateName1700(item.QuotaType)) + "</th>");
                builder1.Append("<td style='text-align:center;'>" + item.MaxNum + "</td>");
            }
            builder.Append("</tr>");
            builder1.Append("</tr>");
            string resStr = "<table style='width:660px;margin-bottom:10px;' border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"ltable\">" + builder.ToString() + builder1.ToString() + "</table>";
            ltmePeie.Text = resStr;
        }



        /// <summary>
        /// 显示禁止回收提示内容
        /// </summary>
        /// <returns></returns>
        public string BuilderShowView()
        {
            double userMaxRebate = Utils.GetMaxRemo(CookUserInfo);
            var result=NoRecoveryAttributeHelper.AddPointsAttributes();
            if (null == result || result.Count < 1)
                return string.Empty;
            result = result.Where(x => x.targer >= CookUserInfo.Rebate).ToList();
            StringBuilder builder = new StringBuilder();
            builder.Append(" <table style='margin: 10px 5px 0px;' border='0' cellspacing='0' cellpadding='0' class='ltable'>");
            builder.Append("<tbody>");
            builder.Append(" <tr><td colspan='5' style='text-align:center;'><b> <span style='color:red;'>十天</span>内团队销量达到以下标准的不能降点</b></td></tr>");
       
            /**content*/
            foreach (var item in result)
            {
                builder.Append("<tr><td align='center'>" + Math.Round(userMaxRebate - Convert.ToDouble(item.targer), 1) + "</td>");
                builder.Append("<td align='center'><b>" + item.SalesStand + "</b></td>");
                builder.Append("</tr>");
            }
            /**content end */
            builder.Append("</tbody>");
            builder.Append("</table>");
            return builder.ToString();
      
            
        }

   
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}