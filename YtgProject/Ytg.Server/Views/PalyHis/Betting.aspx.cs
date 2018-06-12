using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.Views.PalyHis
{
    public partial class Betting : BasePage
    {
        public string FilterSdate = string.Empty;
        public string FiltetEDate = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.BindData();

            DateTime nowDate = DateTime.Now;
            DateTime eDate = DateTime.Now.AddDays(1);
            if (nowDate.Hour < 3)
            {
                nowDate = nowDate.AddDays(-1);
            }
            FilterSdate = Utils.ToGetNowBeginDateStr();
            FiltetEDate = Utils.ToGetNowEndDateStr();
        }

        private void BindData()
        {
            //获取游戏集合
            ILotteryTypeService lotteryTypeService = IoC.Resolve<ILotteryTypeService>();

            this.drpGames.Items.Add(new ListItem("所有游戏", ""));
            foreach (var item in lotteryTypeService.GetLotteryTypes())
            {
                this.drpGames.Items.Add(new ListItem(item.LotteryName, item.Id + "," + item.LotteryCode));
            }
            this.drpGames.SelectedIndex = 0;
            //
        }
    }
}