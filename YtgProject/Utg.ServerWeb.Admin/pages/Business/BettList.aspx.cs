using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Utg.ServerWeb.Admin.pages.Business
{
    public partial class BettList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //code="+code+"&bt="+$("#txtBegin").val()+"&end="+$("#txtEnd").val();
            if (!IsPostBack)
            {
                this.txtBegin.Text = Utils.GetNowBeginDateStr();
                this.txtEnd.Text = Utils.GetNowEndDateStr();

                this.InintFilter();


            }
        }

        #region 系统条件初始化
        private void InintFilter() {
            this.BindGames();
        }

        private void BindGames()
        {
            ILotteryTypeService lotteryTypeService = IoC.Resolve<ILotteryTypeService>();
            this.drpGame.DataTextField = "LotteryName";
            this.drpGame.DataValueField = "LotteryCode";
            var result = lotteryTypeService.GetAll().Where(c => c.IsEnable).ToList();
            result.Insert(0, new LotteryType()
            {
                LotteryName="全部",
                LotteryCode=""
            });
            this.drpGame.DataSource = result;
            this.drpGame.DataBind();
            this.drpGame.SelectedIndex = 0;
        }
        #endregion



        protected void btnFilter_Click(object sender, EventArgs e)
        {

        }

        protected void pagerControl_PageChanged(object sender, EventArgs e)
        {

        }

       

       
    }
}