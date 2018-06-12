using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.Lottery.GroupBy
{
    public partial class index : BasePage
    {
        public List<LotteryType> LotteryTypes = new List<LotteryType>();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            if (!IsPostBack)
                InintLotterys();
        }

        protected void rptMenus_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var gnt = e.Item.DataItem as GroupNameType;
            if (null == gnt)
                return;

            var rptChildre = (e.Item.FindControl("rptChildren") as Repeater);
            rptChildre.DataSource = LotteryTypes.Where(c => c.GroupName == gnt.Id).OrderBy(x => x.Sort);
            rptChildre.DataBind();
        }

        private void InintLotterys()
        {
            IGroupNameTypeService groupNameTypeService = IoC.Resolve<IGroupNameTypeService>();
            ILotteryTypeService lotteryService = IoC.Resolve<ILotteryTypeService>();
            LotteryTypes = lotteryService.GetEnableLotterys();
            
            var groupResult = groupNameTypeService.GetAll().OrderBy(c => c.OrderNo);
            rptMenus.DataSource = groupResult;
            rptMenus.DataBind();

        }
    }
}