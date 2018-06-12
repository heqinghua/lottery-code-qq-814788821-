using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.Users
{
    public partial class AutoRechargeCnt : BasePage
    {

        public decimal Min = 50;//充值最小值
        public decimal Max = 2000;//充值最大值

        public string zbfqrcode = "";
        public string wxqrcode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Min = Comm.Utils.ZfbMin;
            Max = Comm.Utils.ZfbMax;
            
            LoadData();

        }
        public static bool IsShoping()
        {
            var nowDates = DateTime.Now;
            var hour = nowDates.Hour;//时
            var mis = nowDates.Minute;//分
            //
            int[] hours = new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 0, 1 };
            Ytg.Scheduler.Comm.LogManager.Info("now-hour："+ hour);
            return hours.Contains(hour);
        }

        private void LoadData()
        {
            decimal monery;
            string payType = "";

            if (Request.Params["amount"] != null && Request.Params["bankCode"] != null)
            {
                payType = Request.Params["bankCode"].ToString();
                monery = Convert.ToInt32(Request.Params["amount"].ToString());
            }
            else 
            {
                Response.Redirect("/Views/pay/PayIndex.aspx");
                return;
            }
            var sp = !IsShoping();
            if (sp) {
                Response.Redirect("/Views/pay/PayIndex.aspx");
                return;
            }
            

            //验证支付金额
            if (monery < Min || monery > Max)
            {
                Response.Redirect("/Views/pay/PayIndex.aspx");
                return;
            }

            ICompanyBankService companyBankServices = IoC.Resolve<ICompanyBankService>();
            try
            {
                CompanyBankVM companyBank = companyBankServices.GetCompanyBank(payType);
                if (companyBank == null)
                {
                    Response.Redirect("/Views/pay/PayIndex.aspx");
                    return;
                }

                var result = companyBankServices.GetRechargeBankInfo(companyBank.BankId, CookUserInfo.Id, monery).FirstOrDefault();
                if (result == null)
                {
                    Response.Redirect("/Views/pay/PayIndex.aspx");
                    return;
                }
                ISysSettingService sysSettingService = IoC.Resolve<ISysSettingService>();
                var zfbsetting=sysSettingService.GetSetting("zhb_rect_url");
                var wxsetting = sysSettingService.GetSetting("wx_rect_url");
                if (null != zfbsetting)
                {
                    zbfqrcode = BootStrapper.SiteHelper.rectImagePath + zfbsetting.Value;
                }
                if (null != wxsetting)
                {
                    wxqrcode = BootStrapper.SiteHelper.rectImagePath + zfbsetting.Value;
                }

                userName.Text = result.BankOwner;
                userCode.Text = result.BankNo;
                txtNum.Text = result.Num;
                this.hidBankid.Value = result.Id.ToString();
                this.imgLogo.AlternateText = result.BankName;
                this.imgLogo.ImageUrl = "/Views/pay/mobao/images/" + payType + ".jpg";
                hidecztype.Value = payType;
                this.lbMonery_.Text = monery.ToString();
                bankLink.NavigateUrl = result.BankWebUrl;

                Ytg.Scheduler.Comm.LogManager.Info(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " -- " + sp + "  BankOwner=" + result.BankOwner + " BankNo=" + result.BankNo + " Num=" + result.Num + " id=" + result.Id + " result.BankName=" + result.BankName);
            }
            catch (Exception ex)
            {
                Alert("系统参数设置错误！");
            }
        }
    }
}