using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.Views.Activity.rgz
{
    public partial class RgzPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CookUserInfo.Rebate != 0 && CookUserInfo.Rebate != 0.1)
                {
                    Response.End();
                    return;
                }
                int bettUserCount = 0;
                var bettMonery = GetM(ref bettUserCount);
                lbbetMonery.Text = bettMonery.ToString();
                lbCountSum.Text = bettUserCount.ToString();
                lbMonery.Text = CatchMonery(bettMonery, bettUserCount);
            }
        }

        private string CatchMonery(decimal bettMonery, int userCount)
        {
            if (CookUserInfo.Rebate == 0)
            {
                ltTl.Text = "直属奖励";
                ltGz.Text = "直属工资";
                ltZd.Visible = false;
                ltZs.Visible = true;
            }
            else
            {
                ltTl.Text = "总代奖励";
                ltGz.Text = "总代工资";
                ltZd.Visible = true;
                ltZs.Visible = false;
            }

            if (CookUserInfo.Rebate == 0 && bettMonery >= 50000 && userCount >= 5)
            {
                //直属工资，必须达到5w
                return (Math.Floor(bettMonery) * (decimal)(0.3 / 100)).ToString();
            }
            else if (CookUserInfo.Rebate == 0.1 && bettMonery >= 10000 && userCount >= 5)
            {

                //总代工资
                return (Math.Floor(bettMonery) * (decimal)(1 / 100)).ToString();
            }
            else
            {
                btnMe.Text = "未达标";
                btnMe.Enabled = false;
                return "0.0000";
            }

        }

        private decimal GetM(ref int bettUserCount)
        {
            DateTime beginDate = Utils.GetNowBeginDate().AddDays(-1);
            DateTime endDate = beginDate.AddDays(1);

            var betDetailService = IoC.Resolve<IBetDetailService>();
            var bettMonery = betDetailService.GetUserGroupBettMonery(CookUserInfo.Id, beginDate, endDate, ref bettUserCount);

            return bettMonery;
        }

        protected void btnMe_Click(object sender, EventArgs e)
        {
            if (CookUserInfo.Rebate != 0 || CookUserInfo.Rebate != 0.1)
            {
                Response.End();
                return;
            }

            int betUserCount = 0;
            var bettMonery= GetM(ref betUserCount);
            decimal dm = Convert.ToDecimal(bettMonery);
            if (dm > 0)
            {
                //插入工资
                //插入账变
                //存入账号
                var details = new BasicModel.SysUserBalanceDetail()
                {
                    RelevanceNo = CookUserInfo.Id.ToString(),
                    SerialNo = "q" + Utils.BuilderNum(),
                    Status = 0,
                    TradeAmt = dm,
                    TradeType = BasicModel.TradeType.分红,
                    UserId = CookUserInfo.Id
                };

                //奖励金额
                ISysUserBalanceService userBalanceServices = IoC.Resolve<ISysUserBalanceService>();
                if (userBalanceServices.UpdateUserBalance(details, dm) > 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('领取日工资成功！');</script>");
                    return;
                }
            }

            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('领取日工资失败，请稍后再试！');</script>");
        }
    }
}