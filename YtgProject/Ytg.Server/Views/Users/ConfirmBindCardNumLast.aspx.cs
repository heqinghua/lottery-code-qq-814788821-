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
    public partial class ConfirmBindCardNumLast :BasePage
    {

        public string BackParam{
            get {
                return (ViewState["BACK"] ?? "").ToString();
            }
            set
            {
                ViewState["BACK"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string banks = Request.Params["drpBanks"];
                string pro = Request.Params["drpPro"];
                string city = Request.Params["drpCity"];
                string zhiHang = Request.Params["txtZhiHang"];
                string openUser = Request.Params["txtOpenUser"];
                string cardnum = Request.Params["txtCardNum"];
                string confirmCardnum = Request.Params["txtConfirmCardNum"];
                BackParam = "drpBanks=" + banks + "&drpPro=" + pro + "&drpCity=" + city + "&txtZhiHang=" + zhiHang + "&txtOpenUser=" + openUser + "&txtCardNum=" + cardnum + "&txtConfirmCardNum=" + confirmCardnum;

                ViewState["drpBanks"] = banks;
                ViewState["drpPro"] = pro;
                ViewState["drpCity"] = city;
                ViewState["txtZhiHang"] = zhiHang;
                ViewState["openUser"] = openUser;

                int banlid = -1;
                int cityid;
                int proid;
                if (string.IsNullOrEmpty(banks) ||
                    string.IsNullOrEmpty(pro) ||
                    string.IsNullOrEmpty(city) ||
                    !int.TryParse(banks.Split('_')[0], out banlid) ||
                    !int.TryParse(city, out cityid) ||
                    !int.TryParse(pro, out proid) ||
                    string.IsNullOrEmpty(openUser) ||
                    string.IsNullOrEmpty(cardnum) ||
                    cardnum != confirmCardnum)
                {
                    Response.End();
                    return;
                }
                confirmzhih.Visible = !string.IsNullOrEmpty(zhiHang);
              
                bankspan.Text = Request.Params["drpBanks_n"];
                opspan.Text = Request.Params["drpPro_n"];
                opscity.Text = Request.Params["drpCity_n"];
                opsname.Text = openUser;
                opsNo.Text = cardnum;
                opszhihang.Text = zhiHang;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.txtZjPwd.Text.Trim())) {
                Response.End();
                return;
            }
            //验证资金密码
            ISysUserBalanceService userBalanceService = IoC.Resolve<ISysUserBalanceService>();
            if (!userBalanceService.VdUserBalancePwd(CookUserInfo.Id, this.txtZjPwd.Text.Trim()))
            {
                //验证失败
                Alert("资金密码验证失败！");
                return;
            }

            var bank = new SysUserBank
            {
                BankNo = opsNo.Text,
                BankId = Convert.ToInt32(ViewState["drpBanks"].ToString().Split('_')[0]),
                BankOwner = opsname.Text,
                Branch = opszhihang.Text,
                CityId = Convert.ToInt32(ViewState["drpCity"]),
                IsDelete = false,
                OccDate = DateTime.Now,
                ProvinceId = Convert.ToInt32(ViewState["drpPro"]),
                UserId = this.CookUserInfo.Id
            };
            ISysUserBankService userBanks = IoC.Resolve<ISysUserBankService>();
            //验证是否存在同样的银行卡号
            if (userBanks.Where(c => c.BankNo == bank.BankNo).FirstOrDefault() != null)
            {
                Alert("银行卡号已经被绑定，请确认是否输入正确！","",3);
                return;
            }
            //验证开户姓名是否一致
            var userBindBanks= userBanks.GetUserBanks(this.CookUserInfo.Id);
            var firstBindCard=userBindBanks.FirstOrDefault();
            bool isCompled = false;
            if (firstBindCard == null)
            {
                isCompled = true;
            }
            else
            {
                //验证是否存在同一银行，的卡
                if (firstBindCard.BankOwner != bank.BankOwner)
                {
                    BackParam += "&at=nb";
                    Response.Redirect("/Views/Users/ConfirmBindCardNum.aspx?" + BackParam);
                    //Alert("一个账户只能绑定同一个开户人姓名的银行卡！");
                    //ClientScript.RegisterStartupScript(this.GetType(),"_down_key","<script>$('#bankback').click();</script>",true);
                    return;
                }
                if (userBindBanks.Where(x => x.BankId == bank.BankId).FirstOrDefault() != null)
                {
                    BackParam += "&at=cb";
                    Response.Redirect("/Views/Users/ConfirmBindCardNum.aspx?" + BackParam);
                    //Alert("同一个银行只允许绑定一张卡！");
                    //ClientScript.RegisterStartupScript(this.GetType(), "_down_key", "<script>$('#bankback').click();</script>", true);
                    return;
                }
            }

            if (userBanks.CreateBank(bank))
            {
                Response.Write("<script>parent.window.location.href = '/Views/Users/BindBankCard.aspx?dt=dt';</script>");
            }
            else
            {
                Alert("绑定银行卡失败，请稍后重试！");
            }

        }
    }
}