using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Utg.ServerWeb.Admin.pages.Lottery
{
    public partial class PlayBonus : BasePage
    {

        public string lotteryCode = "";
        public List<Ytg.BasicModel.LotteryType> lotteryTypeList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["lotteryCode"] != null)
                    lotteryCode = Request["lotteryCode"].ToString();
                txtLotteryCode.Value = lotteryCode;
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
                if (lotteryCode == "")
                    lotteryCode = lotteryTypeList[0].LotteryCode;
                IPlayTypeRadioService playTypeRadioService = IoC.Resolve<IPlayTypeRadioService>();
                List<PlayRado> playTypeRadioList = playTypeRadioService.GetPattRado(lotteryCode, "");
                this.repList.DataSource = playTypeRadioList;
                this.repList.DataBind();
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
            {
                JsAlert("修改失败！");
                return;
            }

            lotteryCode = txtLotteryCode.Value;
            int playTypeRadioId = Convert.ToInt32(e.CommandArgument);
            int maxNum = 0;
            for (int i = 0; i < this.repList.Items.Count; i++)
            {
                TextBox cBox = (TextBox)repList.Items[i].FindControl("txtMaxNum");
                if (cBox != null && cBox.ToolTip == playTypeRadioId.ToString())
                {

                    if (!int.TryParse(cBox.Text, out maxNum))
                    {
                        JsAlert("修改失败,注数输入错误！");
                        return;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            IPlayTypeRadioService playTypeRadioService = IoC.Resolve<IPlayTypeRadioService>();
            var item = playTypeRadioService.Get(playTypeRadioId);
            if (item == null)
            {
                JsAlert("修改失败，请刷新后重试");
                return;
            }
            item.MaxBetCount = maxNum;
            playTypeRadioService.Save();
            InitData();
            JsAlert("修改成功！");
        }

        public string SetActive(string mLotteryCode)
        {
            if (mLotteryCode == this.lotteryCode)
                return "btn btn-primary";
            return "btn";
        }
    }
}