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

namespace Utg.ServerWeb.Admin.pages.Stat
{
    public partial class LotteryStat : BasePage
    {
        public string lotteryId = "";
        public List<Ytg.BasicModel.LotteryType> lotteryTypeList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtBeginDate.Text = Utils.GetNowBeginDateStr();
            this.txtEndDate.Text = Utils.GetNowEndDateStr();
            if (!IsPostBack)
            {
                if (Request["lotteryId"] != null)
                    lotteryId = Request["lotteryId"];
                txtLotteryId.Value = lotteryId.ToString();

                InitData();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            //获取彩种列表
            ILotteryTypeService lotteryTypeService = IoC.Resolve<ILotteryTypeService>();
            lotteryTypeList = lotteryTypeService.GetAll().ToList();

            //根据彩种获取玩法列表
            if (lotteryTypeList != null && lotteryTypeList.Count > 0)
            {
                //默认显示彩种
                if (lotteryId == "")
                {
                    lotteryId = lotteryTypeList[0].LotteryCode;
                    this.txtLotteryId.Value = lotteryId;
                }

                string lotteryName = "";
                LotteryType lotteryType = lotteryTypeList.Where(m => m.LotteryCode == lotteryId).FirstOrDefault();
                if (lotteryType != null)
                {
                    lotteryName = lotteryType.LotteryName;
                    lotteryId = lotteryType.LotteryCode;
                }

            }
            this.BindResult();
        }

        private void BindResult()
        {
            string beginDate = this.txtBeginDate.Text.Trim(); //业务开始日期
            string endDate = this.txtEndDate.Text.Trim();     //业务截止日期

            ICountDataService countDataService = IoC.Resolve<ICountDataService>();
            var result = countDataService.GetCountData(beginDate, endDate);
            if (result != null)
            {
                var res = result.lotteryDataList.Where(x => x.LotteryCode == this.txtLotteryId.Value).FirstOrDefault();
                if (null == res || res.playTypeRadioDataList == null)
                    return;
                this.rptList1.DataSource = res.playTypeRadioDataList;
                this.rptList1.DataBind();
            }
        }

        public string SetActive(string mLotteryId)
        {
            if (mLotteryId == this.lotteryId)
                return "btn btn-primary";
            return "btn";
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {

        }
    }
}