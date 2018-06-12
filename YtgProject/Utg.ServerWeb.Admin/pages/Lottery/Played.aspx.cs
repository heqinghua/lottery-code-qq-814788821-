using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Utg.ServerWeb.Admin.pages.Lottery
{
    public partial class Played : BasePage
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
            if (lotteryTypeList != null && lotteryTypeList.Count>0)
            {
                if (lotteryCode=="")
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
            if(e.CommandArgument==null)
            {
                JsAlert("修改失败！");
                return;
            }

            lotteryCode = txtLotteryCode.Value;
            int playTypeRadioId=Convert.ToInt32(e.CommandArgument);
            bool isEnable=false;
            for (int i = 0; i < this.repList.Items.Count; i++)
            {
                CheckBox cBox = (CheckBox)repList.Items[i].FindControl("cBox");
                if (cBox != null)
                {
                    int id = Convert.ToInt32(cBox.ToolTip);
                    if (id == playTypeRadioId)
                    {
                        isEnable=cBox.Checked;
                        break;
                    }
                }
            }

            IPlayTypeRadioService playTypeRadioService = IoC.Resolve<IPlayTypeRadioService>();
            if (playTypeRadioService.UpdateStatus(playTypeRadioId, isEnable))
            {
                InitData();
                JsAlert("修改成功！");
            }
            else
            {
                JsAlert("修改失败！");
            }
        }

        public string SetActive(string mLotteryCode)
        {
            if (mLotteryCode == this.lotteryCode)
                return "btn btn-primary";
            return "btn";
        }
    }
}