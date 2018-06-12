using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.pay
{
    public partial class PayIndex : BasePage
    {
        public decimal Min = 10;//充值最小值

        public decimal Max = 50000;//充值最大值

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                //验证是否设置资金密码
                ISysUserBalanceService userBananceService = IoC.Resolve<ISysUserBalanceService>();
                var userBalance = userBananceService.GetUserBalance(this.CookUserInfo.Id);
                if (null == userBalance || string.IsNullOrEmpty(userBalance.Pwd))
                {
                    Response.Redirect("/Views/Users/UpdatePwd.aspx?zj=xx");
                    return;
                }

                this.InintSettings();
            }
        }

        private void InintSettings() {
            //获取相关配置
            var settingService = IoC.Resolve<ISysSettingService>();
            var fds = settingService.Where(x => x.Key == "CZXZ").FirstOrDefault();
            if (null != fds)
            {
                Min = Convert.ToDecimal(fds.Value.Split(',')[0]);
                Max = Convert.ToDecimal(fds.Value.Split(',')[1]);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //if (!Utils.IsShoping()) {
            //    Alert("每天的充值处理时间为：早上 9:00 至 次日凌晨 2:00");
            //    return ;
            //}
            string radBank = Request.Form["bankCode"];
            
            decimal monery;
            string inStr = Request.Params["amount"];
            if (!decimal.TryParse(inStr, out monery)  )
            {
                Alert("请输入正确的参数");
                return;
            }
            if (radBank == "ZFB")
            { 
              //跳转至支付宝界面
                Response.Redirect("/Views/Users/AutoRechargeCnt.aspx?amount=" + monery + "&bankCode=" + radBank);
                return;
            }

            InintSettings();//获取充值设置


            if (monery < Min || monery > Max)
            {
                Alert(" 单笔充值限额：最低：" + Min + "元，最高：" + Max + "元");
                return;
            }

            //验证码判断
            
            //var hour = DateTime.Now.Hour;
            //if (hour > 2 && hour < 9)
            //{
            //    Alert("该时间段不能充值，请在早上 9:00 至 次日凌晨2:00进行充值");
            //}
            //else
            //{
                IRecordTempService recordService = IoC.Resolve<IRecordTempService>();
                var item=recordService.Create(new BasicModel.RecordTemp()
                {
                    Guid=Guid.NewGuid().ToString(),
                    IsCompled=false,
                    IsEnable=true,
                    OccDate=DateTime.Now,
                    TradeAmt=monery,
                    UserId=CookUserInfo.Id,
                    MY18PT = radBank,
                    MY18FY="R"+Utils.BuilderNum()
                });
                recordService.Save();
                //跳转
                Response.Redirect("/Views/pay/PayConfim.aspx?orderid=" + item.Guid);
            //}
        }
    }
}