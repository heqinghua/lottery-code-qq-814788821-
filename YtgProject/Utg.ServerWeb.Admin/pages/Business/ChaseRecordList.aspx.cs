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
    public partial class ChaseRecordList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                this.txtBegin.Text = Utils.ToGetNowBeginDateStr();
                this.txtEnd.Text = Utils.ToGetNowEndDateStr(); 
                InintFilter();
                this.Bind();
                
            }
        }

        #region 系统条件初始化
        private void InintFilter()
        {
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
                LotteryName = "全部",
                LotteryCode = ""
            });
            this.drpGame.DataSource = result;
            this.drpGame.DataBind();
            this.drpGame.SelectedIndex = 0;
        }
        #endregion

        private void Bind()
        {
            ISysCatchNumService sysCatchNumService = IoC.Resolve<ISysCatchNumService>();
            int totalCount = 0;
            string dateBegin = "";
            string dateEnd = "";

            DateTime? b =null;
            DateTime? e = null;
           
            DateTime dt;
            DateTime dt1;
            if (DateTime.TryParse(this.txtBegin.Text.Trim(), out dt) && DateTime.TryParse(this.txtEnd.Text.Trim(), out dt1))
            {
                dateBegin = this.txtBegin.Text.Trim();
                dateEnd = this.txtEnd.Text.Trim();
            }
            var result=sysCatchNumService.SelectCatchNumList(dateBegin, dateEnd, this.drpGame.SelectedValue, this.txtNum.Text.Trim(), this.txtUserCode.Text.Trim(), this.pagerControl.CurrentPageIndex, ref totalCount);
            this.repList.DataSource = result;
            this.repList.DataBind();

            this.pagerControl.RecordCount = totalCount;
        }

        protected void pagerControl_PageChanged(object sender, EventArgs e)
        {
            Bind();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}