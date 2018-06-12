using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb
{
    public partial class lottery : BasePage
    {
        public List<LotteryType> LotteryTypes=new List<LotteryType>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (CookUserInfo.Rebate<1)
            //{
            //    Response.Redirect("~/Views/UserGroup/UsersList.aspx");
            //    return;
            //}
            this.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            if (!IsPostBack)
            {
               this.InintLotterys();
            }
         
        }

        public string GetLotteryUrl(object lotteryCode)
        {
            
            string lt="";
            switch (lotteryCode.ToString()) {
                case "hk6":
                    lt = "GameLxc";
                    break;
                case "jsk3":
                    lt = "GameK3";
                    break;
                case "bjpk10":
                    lt = "GamePk10";
                    break;
                default:
                    lt = "GameCenter";
                    break;
            }
            return lt;
        }

        private void InintLotterys()
        {
            IGroupNameTypeService groupNameTypeService=IoC.Resolve<IGroupNameTypeService>();
            ILotteryTypeService lotteryService = IoC.Resolve<ILotteryTypeService>();

            LotteryTypes = lotteryService.GetEnableLotterys();

            var groupResult= groupNameTypeService.GetAll().OrderBy(c => c.OrderNo);
            rptMenus.DataSource = groupResult;
            rptMenus.DataBind();

        }


        private void Betting()
        {
            var play_source = Request.Params["play_source"];
            var pmode = Request.Params["pmode"];
            var lt_projects = Request.Params["lt_project[]"];
            var lt_issue_start = Request.Params["lt_issue_start"];

        }

        protected void rptMenus_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var gnt= e.Item.DataItem as GroupNameType;
            if (null == gnt)
                return;

            var rptChildre = (e.Item.FindControl("rptChildren") as Repeater);
            rptChildre.DataSource= LotteryTypes.Where(c => c.GroupName == gnt.Id).OrderBy(x=>x.Sort);
            rptChildre.DataBind();
        }
    }
}