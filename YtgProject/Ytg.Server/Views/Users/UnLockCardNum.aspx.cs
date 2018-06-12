using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.Users
{
    public partial class UnLockCardNum : BasePage
    {
        public string BindCardNums = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllCardNumInfo();
            }
        }

        /// <summary>
        /// 创建所有卡和账号名空间
        /// </summary>
        private void GetAllCardNumInfo()
        {
            ISysUserService mSysUserService = IoC.Resolve<ISysUserService>();
            if (mSysUserService.GetUserLockUserCount(CookUserInfo.Id) >= 2)
            {
                
                Response.Write("<script>alert('您已经解锁过一次，请联系客户！'); parent.window.location.href = '/Views/Users/BindBankCard.aspx?dt=" + DateTime.Now.ToString() + "';</script>");
                return;
            }
            //获取当前绑定卡集合
            ISysUserBankService userbankService = IoC.Resolve<ISysUserBankService>();
            var result = userbankService.GetUserBanks(this.CookUserInfo.Id);

            StringBuilder builder = new StringBuilder();

            for (var i = 0; i < result.Count; i++)
            {
                var item = result[i];
                string title = string.Format("开户名({0})", item.BankName);
                builder.Append("<tr>");

                builder.Append("<td class='lsrd'>");
                builder.Append(title+" :");
                builder.Append("</td>");
                builder.Append("<td>");
                builder.Append("<input type='text' name='BankOwner' style='width:150px;' class='input normal' /><span class='Validform_checktip'>*</span>");
                builder.Append("</td>");
                builder.Append("<td class='lsrd'>");
                builder.Append("卡号 :");
                builder.Append("</td>");
                builder.Append("<td>");
                builder.Append("<input type='text' name='cardNum' class='input normal' style='width:200px;'/><span class='Validform_checktip'>*</span>");
                builder.Append("</td>");
                builder.Append("</tr>");

                //builder.Append("<dl><dt style='width:130px;'>" + title + "</dt><dd></dd>");
               // builder.Append("<dt>卡号</dt><dd></dd></dl>");
            }

            this.BindCardNums = builder.ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //
            string bankOwners= Request.Params["BankOwner"];//开户名
            string cardNums = Request.Params["cardNum"];//账号
            //
            if (string.IsNullOrEmpty(bankOwners) || string.IsNullOrEmpty(cardNums) || string.IsNullOrEmpty(this.txtpwd.Text.Trim()))
            {
                Response.End();
                return;
            }
            var bankOwnersArray = bankOwners.Split(',');
            var cardNumsArray = cardNums.Split(',');
            if (bankOwnersArray.Length != cardNumsArray.Length) {
                Response.End();
                return;
            }
            //验证
            ISysUserBankService userbankService = IoC.Resolve<ISysUserBankService>();
            var result = userbankService.GetUserBanks(this.CookUserInfo.Id);
            for (var i = 0; i < bankOwnersArray.Length; i++)
            {
                string bankOwner = bankOwnersArray[i];
                string cardNum = cardNumsArray[i];
                var fs = result.Where(c => c.BankOwner == bankOwner && c.BankNo == cardNum).FirstOrDefault();
                if (null != fs)
                    result.Remove(fs);
            }
            if (result.Count > 0)
            {
                Alert("请正确填写银行卡信息!");
                GetAllCardNumInfo();//重新绑定
                return;
            }
            //验证资金密码
            ISysUserService mSysUserService = IoC.Resolve<ISysUserService>();
            ISysUserBalanceService mSysUserBalanceService = IoC.Resolve<ISysUserBalanceService>();
            //验证资金密码
            if (!mSysUserBalanceService.VdUserBalancePwd(CookUserInfo.Id, this.txtpwd.Text.Trim()))
            {
                Alert("资金密码验证失败!");
                GetAllCardNumInfo();//重新绑定
                return;
            }
            //修改为锁定
            if (mSysUserService.UnLockUserCards(this.CookUserInfo.Id))
            {
                //关闭窗口
                Response.Write("<script>parent.window.location.href = '/Views/Users/BindBankCard.aspx?dt=" + DateTime.Now.ToString() + "';</script>");
            }
            else
            {
                Alert("解锁失败!");
                GetAllCardNumInfo();//重新绑定
            }
            
        }
    }
}