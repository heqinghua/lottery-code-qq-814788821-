using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;

namespace Utg.ServerWeb.Admin.pages.Business
{
    public partial class AccountDetailedManager :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUserCode.Text = Request.Params["code"];
                this.txtBegin.Text = Request.QueryString["bt"];
                this.txtEnd.Text = Request.QueryString["end"];
                if (string.IsNullOrEmpty(this.txtBegin.Text.Trim())
                    || string.IsNullOrEmpty(this.txtEnd.Text.Trim()))
                {
                    this.txtBegin.Text = Utils.GetNowBeginDateStr();
                    this.txtEnd.Text = Utils.GetNowEndDateStr();
                }

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
            DateTime beginTime = Utils.GetNowBeginDate();
            DateTime endTime = Utils.GetNowEndDate();
            if (!DateTime.TryParse(this.txtBegin.Text.Trim(), out beginTime) && !DateTime.TryParse(this.txtEnd.Text.Trim(), out endTime))
            {
                beginTime = DateTime.Now.AddDays(-7);
                endTime = DateTime.Now;
            }


            string lotteryCode =this.drpGame.SelectedValue;
            int palyRadioCode = Convert.ToInt32(string.IsNullOrEmpty(this.drpPlayType.SelectedValue) ? "-1" : this.drpPlayType.SelectedValue);
            int model = Convert.ToInt32(this.drpModel.SelectedValue);
            //int userType = Convert.ToInt32((cmUserType.SelectedItem as ComboBoxItem).Tag.ToString());
            string betCode = this.txtNum.Text.Trim();
            string userCode = this.txtUserCode.Text.Trim();
        
            string trtypeStr = Request.Params["ordertype"];
            if (trtypeStr == "-1")
                trtypeStr = "";

            int recordCount=0;
            var result = IoC.Resolve<ISysUserBalanceDetailService>().SelectBy(trtypeStr, beginTime, endTime, 2, userCode, -1, 3, betCode, lotteryCode, palyRadioCode,"", model, -1, pagerControl.CurrentPageIndex, pagerControl.PageSize, ref recordCount);
            this.repList.DataSource = result;
            this.repList.DataBind();
            this.pagerControl.RecordCount = recordCount;


            decimal sum = 0;
            decimal app = 0;
            decimal sub = 0;
            foreach (var item in result)
            {
                sub+=item.OutAmt;
                app+=item.InAmt;
               sum+=(sub+app);
            }
            lbSum.Text = sum.ToString();
            lbApp.Text = app.ToString();
            lbSub.Text = sub.ToString();
        }


        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.Bind();
        }

        protected void pagerControl_PageChanged(object sender, EventArgs e)
        {
            this.Bind();
        }

        protected void drpGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            var code= this.drpGame.SelectedValue;
            if (string.IsNullOrEmpty(code))
                return;

            var result = IoC.Resolve<IPlayTypeRadioService>().GetPattRado(code, "");
            result.Insert(0, new Ytg.BasicModel.LotteryBasic.DTO.PlayRado()
            {
                RadioCode = -1,
                PlayTypeRadioName = "全部"
            });
            this.drpPlayType.DataTextField = "PlayTypeRadioName";
            this.drpPlayType.DataValueField = "RadioCode";
            this.drpPlayType.DataSource = result;
            this.drpPlayType.DataBind();

        }
    }
}