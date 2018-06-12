using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Setting
{
    public partial class DataClear : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.Bind();
        }

        private void Bind()
        {
            ICountDataService countDataService = IoC.Resolve<ICountDataService>();
            var source = countDataService.GetClearDataStat().FirstOrDefault();
            if (null == source)
                return;
            lbCustomer.Text = source.UserCount.ToString();
            lbBettcount.Text = source.bettCount.ToString();
            lbCusLoginLog.Text = source.CusLoginLog.ToString();
            lbIssue.Text = source.OpenIssue.ToString();
            lbManLogCount.Text = source.SysLoginLog.ToString();
            lbCusChartCount.Text = source.CusMessage.ToString();
            lbsockChart.Text = source.ChartMsg.ToString();
            lbrechangeCount.Text = source.rechangeCount.ToString();
            lbTiXian.Text = source.TiXian.ToString();
            lbZhanBian.Text = source.ZhanBian.ToString();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            // 0 用户数 1投注、追号、账变 2会员登录日志  3开奖期数 4管理员登录日志 5客户消息 6聊天消息 7充值记录 8充值记录
            int tp = 0;
            decimal monery = 0m;
            int day = 60;
            switch (btn.ID)
            {
                case "btnClear":
                    if (!decimal.TryParse(this.txtMinMoner.Text.Trim(), out monery))
                    {
                        JsAlert("金额格式输入错误！");
                        return;
                    }
                    tp = 0;
                    day = GetDrpDay(drpCode);
                    break;
                case "btnClearBett":
                    tp = 1;
                    day = GetDrpDay(drpBet);
                    break;
                case "btnCusLog":
                    tp = 2;
                    day = GetDrpDay(drpCusLog);
                    break;
                case "btnIssue":
                    tp = 3;
                    day = GetDrpDay(drpIssue);
                    break;
                case "btnManLog":
                    tp = 4;
                    day = GetDrpDay(drpMaLog);
                    break;
                case "btnChartCount":
                    tp = 5;
                    day = GetDrpDay(drpChartCount);
                    break;
                case "btnSockChart":
                    tp = 6;
                    day = GetDrpDay(drpSockChart);
                    break;
                case "btnrechangeCount":
                    tp = 7;
                    day = GetDrpDay(drprechangeCount);
                    break;
                case "btnTiXian":
                    tp = 8;
                    day = GetDrpDay(drpTiXian);
                    break;
                case "btnZhanBian":
                    tp = 9;
                    day = GetDrpDay(drpZhangBian);
                    break;
            }

            ICountDataService countDataService = IoC.Resolve<ICountDataService>();
            countDataService.ClearData(tp, monery, day);
            JsAlert("数据清理成功！");
            this.Bind();
        }

        private int GetDrpDay(DropDownList drpList)
        {
            int day = 60;
            switch (drpList.SelectedIndex)
            {
                case 0:
                    day = 6 * 30;
                    break;
                case 1:
                    day = 3 * 30;
                    break;
                case 2:
                    day = 1 * 30;
                    break;
                case 3:
                    day = 15;
                    break;
                case 4:
                    day = 7;
                    break;
            }
            return day;
        }
    }
}