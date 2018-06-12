using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.Users
{
    public partial class AutoRecharge :BasePage
    {
        public string inStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("radBank");
                Session.Remove("monery");
                Session.Remove("logo");
                Session.Remove("BankWebUrl");
                Session.Remove("IsInterBank");
                BindData();
            }
        }

        private void BindData()
        {
            ISysBankType sysBankTypes = IoC.Resolve<ISysBankType>();
            var source = sysBankTypes.GetRechargeBankTypes(true);
            this.rpt.DataSource = source;
            this.rpt.DataBind();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            int radBank = 0;
            decimal monery;
            string code = Request.Params["hidbid"];//银行简写
            inStr=Request.Params["monery"];//充值金额
            if (!decimal.TryParse(inStr, out monery) ||
                string.IsNullOrEmpty(code))
            {
                
                Alert("请输入正确的参数");
                return;
            }

            
            ISysBankType sysBankTypes = IoC.Resolve<ISysBankType>();
            var source= sysBankTypes.GetRechargeBankTypes(true);
            var bts= source.Where(c => c.BankId == radBank).FirstOrDefault();
            if (bts == null ||
               bts.MaxAmt < monery ||
               bts.MinAmt > monery)
            {
                Alert(" 单笔充值限额：最低："+bts.MinAmt+"元，最高："+bts.MaxAmt+"元");
                return;
            }

            //验证码判断
            var sCode = System.Web.HttpContext.Current.Session["mRecharge"];
            if (sCode == null || sCode.ToString() != code)
            {
                Alert("验证码输入错误!");
                return;
            }
            
            var hour = DateTime.Now.Hour;
            if (hour > 2 && hour < 9)
            {
                Alert("该时间段不能充值，请在早上 9:00 至 次日凌晨2:00进行充值");
            }
            else
            {
                Session["radBank"] = radBank;
                Session["monery"] = monery;
                Session["logo"] = hidLogo.Value;
                Session["BankWebUrl"] = bts.BankWebUrl;
                Session["IsInterBank"] = bts.IsInterBank;
                Response.Redirect("/Views/Users/AutoRechargeCnt.aspx");
            }
        }
    }
}