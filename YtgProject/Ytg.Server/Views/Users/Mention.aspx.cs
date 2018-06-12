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
    public partial class Mention : BasePage
    {
        public int MaxShow = 100;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
               
                BindData();
                InintSettings();
                
                if (!IsShoping())
                {
                    this.noShowTitle.Visible = true;
                    tabs.Visible = false;
                    MaxShow = 0;
                }
            }
        }

        /// <summary>
        /// 当前时间是否销售
        /// </summary>
        /// <returns></returns>
        public static bool IsShoping()
        {

            var nowDates = DateTime.Now;
            var hour = nowDates.Hour;//时
            var mis = nowDates.Minute;//分
            //
            int[] hours = new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
            return hours.Contains(hour);
        }

        private void InintSettings()
        {
            //获取相关配置
            var settingService = IoC.Resolve<ISysSettingService>();
            var fds = settingService.Where(x => x.Key == "TXXZ").FirstOrDefault();
            if (null != fds)
            {
                lbMin.Text =fds.Value.Split(',')[0];
                lbMax.Text =fds.Value.Split(',')[1];
            }

            var isopen = settingService.Where(x => x.Key == "ti_xian_isopen").FirstOrDefault();
            if (isopen != null)
            {
                if (isopen.Value == "1") {
                    //关闭
                    this.noShowTitle.Visible = true;
                    tabs.Visible = false;
                    MaxShow = 0;
                    //msg 
                    var info = settingService.Where(x => x.Key == "ti_xian_shi_bai_info").FirstOrDefault();
                    if (null != info) {
                        divInfo.InnerHtml = info.Value;
                    }
                }
            }
        }

        private void BindData()
        {
            ISysUserBankService userBankServices = IoC.Resolve<ISysUserBankService>();
            var result = userBankServices.SelectMentionBank(CookUserInfo.Id);

            foreach (var item in result)
            {
                var bakNo = Utils.PaseShowBankNum(item.BankNo);
                string text = string.Format("{0}|银行卡尾号：{1}", item.BankName,bakNo.Replace("*",""));
                string value = item.Id + "," + item.MinAmt + "," + item.MaxAmt;
                drpCards.Items.Add(new ListItem(text, value));
            }

            if (result.Count > 0)
            {
                var first = result.FirstOrDefault();
                //MinAmt = first.IsOpenVip ? first.VipMinAmt.ToString("f0") : first.MinAmt.ToString("f0");
                //MaxAmt = first.IsOpenVip ? first.VipMaxAmt.ToString("f0") : first.MaxAmt.ToString("f0");
                lbCounr.Text = first.MentionCount.ToString();
                lbMonery.Text = first.UserAmt.ToString("f2");
            }
            else
                this.btnSummit.Visible = false;
        }

        protected void btnSummit_Click(object sender, EventArgs e)
        {
           
            //
            if (!IsShoping())
            {
                Alert("提现时间为早上 10:00 至 次日凌晨0:00！");
                return;
            }

            ISysUserBankService userBankServices = IoC.Resolve<ISysUserBankService>();
            var resctResult = userBankServices.SelectMentionBank(CookUserInfo.Id);
            var resct=resctResult.FirstOrDefault();
            if (resct != null && resct.MentionCount >= 5)
            {
                Alert("今天您已经成功发起了" + resct.MentionCount + "次提现申请,提现失败！");
                return;
            }
          


            decimal outmonery;
            string pwd=this.txtPwd.Text.Trim();
            string selValue = this.drpCards.SelectedValue;
            if (!decimal.TryParse(this.txtoutMonery.Text.Trim(), out outmonery) ||
                string.IsNullOrEmpty(pwd)||
                string.IsNullOrEmpty(selValue))
            {
                Alert("参数验证错误!");
                return;
            }
            /**验证卡绑定时间*/
            foreach (var yh in resctResult)
            {
                string value = yh.Id + "," + yh.MinAmt + "," + yh.MaxAmt;
                if (value == selValue)
                {
                    //判断卡绑定时间
                    if (DateTime.Now.Subtract(yh.OccDate).TotalHours < 2)
                    {
                        Alert("银行卡绑定时间未达2小时，暂不允许提现！");
                        return;
                    }
                }
            }
            /*验证卡绑定时间**/
            var array= selValue.Split(',');
            int bankId;
            if (!int.TryParse(array[0], out bankId))
            {
                Alert("参数错误!");
                return;
            }
            ISysUserService userService = IoC.Resolve<ISysUserService>();
            var iser= userService.GetUserAndZiJin(this.CookUserInfo.Id);
            if (iser == null || iser.IsDelete || iser.Status == 1)
            {
                Alert("资金禁用");
                return;
            }
            ISysUserBalanceService sysUserBalanceService = IoC.Resolve<ISysUserBalanceService>();
            int state=sysUserBalanceService.HasMention(CookUserInfo.Id,outmonery);
            if (state == 0)
            {
                if (!sysUserBalanceService.VdUserBalancePwd(CookUserInfo.Id, pwd))//验证资金密码失败
                {
                    Alert("资金密码错误!");
                    return;
                }
            }
            else
            {
                //1为用户余额不够本次提款 3 流水未达到提款要求
                if (state == 1)
                {
                    Alert("可提款余额不够本次提款！");
                }
                else if (state == 3)
                {
                    Alert("投注金额未达到提款要求，无法提款！");
                }
                else if (state == -1)
                {
                    Alert("提款申请失败，请联系在线客服");
                }
                return;
            }

            try
            {
                var result = userBankServices.SubmitMention(bankId, outmonery, CookUserInfo.Id);
                string msg = "提现申请成功";
                string refurl = "";
                if (result == -1) { msg = "余额不足，提现申请不成功"; }
                else if (result == -2) { msg = "系统异常，稍后再试"; }
                else
                {
                    this.txtoutMonery.Text = "";
                    this.txtPwd.Text = "";
                    //跳转至提现记录
                    refurl = "window.location='/Views/Users/MentionList.aspx'";
                }
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert(\"" + msg + "\",1,function(){"+refurl+"});</script>");
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("btnSummit_Click", ex);
                Alert("提现失败，请稍后再试!");
            }
        }


    }
}