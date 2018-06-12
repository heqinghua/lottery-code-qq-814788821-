using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Core.Service.Lott;


namespace Ytg.ServerWeb.wap
{
    public partial class lobby : BasePage
    {

        public string ids = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            if (!IsPostBack)
                InintLotterys();
        }

        public string GetLotteryUrl(object lotteryCode)
        {
            
            string lt = "";
            switch (lotteryCode.ToString())
            {
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
            // return lt;
            return "GameCenter";
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public string GetLotteryCatay(object tag)
        {
            if (tag == null)
                return "1";
            if (tag.ToString() == "7" || tag.ToString() == "9")
                return "2";
            return "1";
        }

        /// <summary>
        /// 加载彩种
        /// </summary>
        private void InintLotterys()
        {
            ILotteryTypeService lotteryService = IoC.Resolve<ILotteryTypeService>();
            var LotteryTypes = lotteryService.GetEnableLotterys();

            ids = ",";
            foreach (var item in LotteryTypes) {
                ids = ids +item.Id+",";
            }

            rptChildren.DataSource = LotteryTypes;
            rptChildren.DataBind();

            rptChildrenerect.DataSource = LotteryTypes;
            rptChildrenerect.DataBind();
        }
    }
}