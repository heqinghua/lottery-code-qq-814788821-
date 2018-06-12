using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.ServerWeb.Views.pay.zhifu;

namespace Ytg.ServerWeb.Mobile.Users
{
    public partial class AutoRechargeCnt : BasePage
    {

        public decimal Min = 50;//充值最小值
        public decimal Max = 2000;//充值最大值
        protected void Page_Load(object sender, EventArgs e)
        { 
            Min = Comm.Utils.ZfbMin;
            Max = Comm.Utils.ZfbMax;
            if (!IsPostBack) {
                LoadData(); 
            }

           
        }

        private void LoadData()
        {
            decimal monery;
            string payType = "";

            if (Request.Form["amount"] != null && Request.Form["bankCode"] != null)
            {
                payType = Request.Form["bankCode"].ToString();
                monery = Convert.ToDecimal(Request.Form["amount"].ToString());
            }
            else 
            {
                Response.Redirect("/Views/pay/Payment.aspx");
                return;
            }

            //验证支付金额
            if (monery < Min || monery > Max)
            {
                Response.Redirect("/Views/pay/Payment.aspx");
                return;
            }
            //if (payType == "cft") {
            //    //跳转到值付微信支付
            //    IRecordTempService recordService = IoC.Resolve<IRecordTempService>();
            //    var item = recordService.Create(new BasicModel.RecordTemp()
            //    {
            //        Guid = Guid.NewGuid().ToString(),
            //        IsCompled = false,
            //        IsEnable = true,
            //        OccDate = DateTime.Now,
            //        TradeAmt = monery,
            //        UserId = CookUserInfo.Id,
            //        MY18PT = "wechart",
            //        MY18FY = "R" + Utils.BuilderNum()
            //    });
            //    recordService.Save();
            //    //跳转
            //    string url=Ytg.ServerWeb.Views.pay.zhifu.ZhiFuPayConfig.PayDns + "/Views/pay/zhifu/wechart/WxPay.aspx?tok=" + item.Guid;
            //    Response.Redirect(url);
            //    return;
            //}
            //else if (payType == "zfb")
            //{

                ICompanyBankService companyBankServices = IoC.Resolve<ICompanyBankService>();
                CompanyBankVM companyBank = companyBankServices.GetCompanyBank(payType);
                if (companyBank == null)
                {
                    Response.Redirect("/Views/pay/Payment.aspx");
                    return;
                }

                var result = companyBankServices.GetRechargeBankInfo(companyBank.BankId, CookUserInfo.Id, monery).FirstOrDefault();
                if (result == null)
                {
                    Response.Redirect("/Views/pay/Payment.aspx");
                    return;
                }
                userName.Text = result.BankOwner;
                userCode.Text = result.BankNo;
                txtNum.Text = result.Num;
                this.hidBankid.Value = result.Id.ToString();
            hidecztype.Value = payType;
            this.imgLogo.AlternateText = result.BankName;
                this.imgLogo.ImageUrl = "/Views/pay/mobao/images/" + payType + ".jpg";
                this.lbMonery.Text = monery.ToString();
                bankLink.NavigateUrl = result.BankWebUrl;
            //}
            //else
            //{
            //    IRecordTempService recordService = IoC.Resolve<IRecordTempService>();
            //    var item = recordService.Create(new BasicModel.RecordTemp()
            //    {
            //        Guid = Guid.NewGuid().ToString(),
            //        IsCompled = false,
            //        IsEnable = true,
            //        OccDate = DateTime.Now,
            //        TradeAmt = monery,
            //        UserId = CookUserInfo.Id,
            //        MY18PT = payType,
            //        MY18FY = "R" + Utils.BuilderNum()
            //    });
            //    recordService.Save();
            //    string url = ZhiFuPayConfig.PayDns + "/Views/pay/zhifu/MerDinpayUTF-8.aspx?tok=" + item.Guid;
            //    Response.Redirect(url);
            //    //eturn ZhiFuPayConfig.PayDns + "/Views/pay/zhifu/MerDinpayUTF-8.aspx?tok=" + item.Guid;
            //}
        }
    }
}