using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel.DTO;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.Users
{
    public partial class Binding : BasePage
    {
      

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ViewStateMode = System.Web.UI.ViewStateMode.Enabled;
            if (!IsPostBack)
            {
                //获取第一张绑定卡
                ISysUserBankService userBankService = IoC.Resolve<ISysUserBankService>();
                var VdBankDTOInfo = userBankService.GetUserBank(this.CookUserInfo.Id);
                if (null == VdBankDTOInfo)
                {
                    Response.Redirect("/Views/Users/ConfirmBindCardNum.aspx");
                    return;
                }
                //固定显示6位*
                //取最后3位
                //string last=string.Join("",VdBankDTOInfo.BankNo.Skip(VdBankDTOInfo.BankNo.Length - 3));
                //VdBankDTOInfo.BankNo = "******" + last;
                VdBankDTOInfo.BankNo = Utils.PaseShowBankNum(VdBankDTOInfo.BankNo);

                this.lbbankName.Text = VdBankDTOInfo.BankName;
                this.lbBankNo.Text = VdBankDTOInfo.BankNo;
            }
        }

        

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //验证
            string userName = Request.Params["oldpwd"];
            string card = Request.Params["txtCard"];
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(card))
            {
                Response.End();
                return;
            }
            //验证
            ISysUserBankService userBankService = IoC.Resolve<ISysUserBankService>();
            if (userBankService.VidataCard(card, userName))
            {
                //验证成功
                Response.Redirect("/Views/Users/ConfirmBindCardNum.aspx");
            }
            else
            {
                //验证失败
                Alert("银行卡信息验证失败!");
            }
        }
    }
}