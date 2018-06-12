using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Customer
{
    public partial class UserRecharge : BasePage
    {
        int userId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["id"], out userId))
            {
                Response.End();
                return;
            }
            if (!IsPostBack)
            {

                if (Request.Params["tp"] == "1")//分红
                {
                    radFenHong.Checked = true;
                    btnSave.Text = "确认分红";
                }
                else if (Request.Params["tp"] == "0")
                {
                    radDefault.Checked = true;
                    btnSave.Text = "确认充值";
                }
                else {
                    radKouk.Checked = true;
                    btnSave.Text = "确认扣款";
                }

                this.Bind();
            }
        }

        private void Bind()
        {
           
           
            var userBalance = IoC.Resolve<ISysUserBalanceService>();
            var bans = userBalance.GetUserBalance(userId);
            if (null == bans)
            {
                JsAlert("请联系开发人员检查此用户数据是否正确！");
                Response.End();
                return;
            }
            ISysUserService sysUserService = IoC.Resolve<ISysUserService>();
            var item = sysUserService.Get(userId);
            if (item == null)
            {
                Response.End();
                return;
            }
            this.txtCode.Text = item.Code;
            this.txtNickName.Text = item.NikeName;
            this.txtMonery.Text = bans.UserAmt.ToString();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            decimal changeMonery = 0m;
            if (!decimal.TryParse(this.txtinMonery.Text.Trim(), out changeMonery) || changeMonery <= 0)
            {
                JsAlert("充值金额格式错误！");
                return;
            }

            var details = new SysUserBalanceDetail()
            {
                RelevanceNo = "system.recharge",
                SerialNo = "q" + Utils.BuilderNum(),
                Status = 0,
                TradeAmt = changeMonery,
                TradeType =radDefault.Checked?TradeType.系统充值:TradeType.分红,
                UserId = Convert.ToInt32(Request.QueryString["id"])
            };
            if (radKouk.Checked)
            {
                details.TradeType = TradeType.其他;
                details.TradeAmt = 0 - details.TradeAmt;
                changeMonery = 0 - changeMonery;
            }

            var userBalance = IoC.Resolve<ISysUserBalanceService>();
            if (userBalance.UpdateUserBalance(details, changeMonery) > 0)
            {
                if (radDefault.Checked)
                {
                    //普通充值，需增加提款限额
                    double bili = 5;
                    ISysSettingService settingService = IoC.Resolve<ISysSettingService>();
                    var fs = settingService.GetAll().Where(x => x.Key == "chongzhiBili").FirstOrDefault();
                    if (null != fs)
                    {
                        if (!double.TryParse(fs.Value, out bili))
                            bili = 5;
                    }
                    ISysUserService userServices = IoC.Resolve<ISysUserService>();
                    var minOutMonery = (changeMonery * (decimal)(bili / 100));
                    if (userServices.UpdateUserMinMinBettingMoney(details.UserId, minOutMonery) > 0)
                    {
                        //更新用户提款流水要求

                    }
                }
                if (Request.Params["tp"] == "1")//分红
                    JsAlert("分红成功", true);
                else if (Request.Params["tp"] == "0")
                    JsAlert("充值成功", true);
                else
                    JsAlert("扣减成功", true);
            }
            else
            {
                JsAlert("充值失败，请关闭后重试", false);
            }

        }
    }
}