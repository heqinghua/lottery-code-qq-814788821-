using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.Lottery
{
    public partial class CatchDetail : BasePage
    {
        public string LotteryCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.BindData();
        }

        public string GetStateCheck(object state)
        {
            BasicModel.BetResultType outCatchNumType;
            Enum.TryParse<BasicModel.BetResultType>(state.ToString(), out outCatchNumType);
            if (outCatchNumType == BasicModel.BetResultType.NotOpen)
                return "";
            return "disabled=\"disabled\"";
        }
        public string GetItemStateStr(object state)
        {
            BasicModel.BetResultType outCatchNumType;
            Enum.TryParse<BasicModel.BetResultType>(state.ToString(), out outCatchNumType);
            string stateStr = "";
            switch (outCatchNumType)
            {
                case BasicModel.BetResultType.Cancel:
                    stateStr = "本人撤单";
                    break;
                case BasicModel.BetResultType.NotOpen:
                    stateStr = "未开奖";
                    break;
                case BasicModel.BetResultType.NotWinning:
                    stateStr = "<span style='color:#0032b8;'>未中奖</span>";
                    break;
                case BasicModel.BetResultType.SysCancel:
                    stateStr = "系统撤单";
                    break;
                case BasicModel.BetResultType.Winning:
                    stateStr = "<span style='color:red;'>已中奖</span>";
                    break;
            }

            return stateStr;
        }


        public string GetStateStr(object state)
        {
            BasicModel.CatchNumType outCatchNumType;
            Enum.TryParse<BasicModel.CatchNumType>(state.ToString(), out outCatchNumType);
            string stateStr = "已完成";
            switch (outCatchNumType)
            {
                case BasicModel.CatchNumType.Cancel:
                    stateStr = "已撤单";
                    break;
                case BasicModel.CatchNumType.Runing:
                    stateStr = "正在进行";
                    break;
            }

            return stateStr;
        }

        private void BindData()
        {
            string catchCode = Request.Params["catchCode"];
            if (string.IsNullOrEmpty(catchCode))
            {
                Response.End();
                return;
            }
            ISysCatchNumIssueService issueServices = IoC.Resolve<ISysCatchNumIssueService>();
            ISysCatchNumService catchNumSerVices = IoC.Resolve<ISysCatchNumService>();
            int totalCount = 0;
            var dt = catchNumSerVices.GetItemForCode(catchCode);
            if (dt == null)
            {
                Response.End();
                return;
            }

            var xd = issueServices.GetLotteryId(catchCode);
            if (xd != null)
                LotteryCode = xd.Value.ToString();

            if (dt.Stauts == BasicModel.CatchNumType.Runing)
                callenTr.Visible = true;
            lbcatchCode.Text = dt.CatchNumCode;
            lbCode.Text = dt.Code;
            lbTime.Text = dt.OccDate.ToString("yyyy/MM/dd HH:mm:ss");
            var prName=(dt.PlayTypeRadioName=="定位胆"?"":(dt.PlayTypeRadioName));
            lbbettCode.Text = string.IsNullOrEmpty(dt.PostionName) ? (dt.PlayTypeName + "" + dt.PlayTypeRadioName) :(dt.PostionName + prName);
            lbGame.Text = dt.LotteryName;
            string modelStr = "元";
            switch (dt.Model)
            {
                case 1:
                    modelStr = "角";
                    break;
                case 2:
                    modelStr = "分";
                    break;
                case 3:
                    modelStr = "厘";
                    break;
            }
            lbPlayType.Text = modelStr;
            lbBeginIssue.Text = dt.BeginIssueCode;
            lbIssueCount.Text = dt.CatchIssue.ToString()+"期";
            lbCompledIssue.Text = dt.CompledIssue.ToString()+"期";
            lbCannelIssue.Text = (dt.UserCannelIssue+dt.SysCannelIssue).ToString()+"期";
            lbMonerty.Text = dt.SumAmt.ToString();
            lbCompledMonerty.Text = dt.CompledMonery.ToString();
            lbWinIssue.Text = dt.WinIssue.ToString()+"期";
            lbWinMonery.Text = dt.WinMoney.ToString();
            lbCannelMonery.Text = dt.UserCannelMonery.ToString();
            lbWinStop.Text = dt.IsAutoStop?"是":"否";

            lbState.Text = GetStateStr(dt.Stauts);
            txtContent.Text = dt.BetContent;

            var result = issueServices.GetCatchIssue(catchCode).OrderBy(c => c.IssueCode);
            rptWins.DataSource = result;
            rptWins.DataBind();
        }
    }
}