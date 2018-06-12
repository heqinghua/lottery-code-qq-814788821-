using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;
using Ytg.Service.Lott;

namespace Ytg.ServerWeb.Mobile.userCenter
{
    public partial class BettingDetail : System.Web.UI.Page
    {
        const string HanZi = "一二三四五六七八九十";

        public string LotteryCode;

        private bool isCatch = false;

        public string CatchCode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BetList res = null;
                isCatch = !string.IsNullOrEmpty(Request.Params["catchCode"]);
                if (!isCatch)
                {
                    palCatch.Visible = false;
                    res = this.GetBetting();
                }
                else
                {
                    string catchCode = Request.Params["catchCode"];
                    CatchCode = catchCode;
                    string issueCode = Request.Params["issueCode"];
                    res = GetCatch(catchCode, issueCode);
                    res.PalyRadioCode = res.RadioCode;
                    palCatch.Visible = true;
                }
                var bet = Request.Params["betcode"];
                if (res == null && !string.IsNullOrEmpty(bet) && bet.StartsWith("i"))
                {
                    palCatch.Visible = true;
                    //
                    ISysCatchNumIssueService catNumIssue = IoC.Resolve<ISysCatchNumIssueService>();
                    var item = catNumIssue.Where(x => x.CatchNumIssueCode == bet).FirstOrDefault();
                    if (null != item)
                    {
                        CatchCode = item.CatchNumCode;
                        isCatch = true;
                        res = GetCatch(item.CatchNumCode, item.IssueCode);
                        res.PalyRadioCode = res.RadioCode;

                    }
                }
                this.palCatch.HRef = "/Lottery/CatchDetail.aspx?catchCode=" + CatchCode;
                BindData(res);
            }
        }

        private BetList GetCatch(string catchCode, string issueCode)
        {

            ISysCatchNumIssueService issueServices = IoC.Resolve<ISysCatchNumIssueService>();
            return issueServices.GetCatchIssueDetail(catchCode, issueCode);
        }

        private BetList GetBetting()
        {
            string betCode = Request.Params["betcode"];

            IBetDetailService detailService = IoC.Resolve<IBetDetailService>();
            return detailService.GetBetDetailForBetCode(betCode);
        }

        private void BindData(BetList item)
        {


            if (item.Stauts == 3)
                exitTd.Visible = true;
            if (!isCatch)
            {
                LotteryCode = IoC.Resolve<ILotteryTypeService>().Where(c => c.LotteryCode == item.LotteryCode).FirstOrDefault().Id.ToString();
            }
            else
            {
                ISysCatchNumIssueService issueServices = IoC.Resolve<ISysCatchNumIssueService>();
                var xd = issueServices.GetLotteryId(item.BetCode);
                if (xd != null)
                    LotteryCode = xd.Value.ToString();
            }
            this.lbCode.Text = item.Code;
            this.lbGame.Text = item.LotteryName;
            this.lbbettCode.Text = item.BetCode;

            string st = "本人撤单";
            if (item.Stauts == 1)
                st = "已中奖";
            else if (item.Stauts == 2)
                st = "未中奖";
            else if (item.Stauts == 3)
                st = "未开奖";
            else if (item.Stauts == 5)
                st = "系统撤单";

            string modelStr = "元";
            switch (item.Model)
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

            this.lbState.Text = st;
            this.lbModel.Text = item.Multiple + "倍 - " + modelStr + "";
            this.lbbetTime.Text = item.OccDate.ToString("yyyy-MM-dd HH:mm:ss");
            this.lbIssue.Text = item.IssueCode;
            this.lbOpenTime.Text = item.OpenResult;

            /***/
            lbPlayType.Text = item.PlayTypeName + "" + item.PlayTypeRadioName;//玩法
            this.lbMonerty.Text = item.WinMoney.ToString();
            this.lbSumMonery.Text = item.TotalAmt.ToString();
            this.hidPostionName.Value = item.PostionName;
            //
            //可能中的奖金   
            var fs = PlayTypeRadioServiceCatch.GetAll().Where(p => p.RadioCode == item.PalyRadioCode).FirstOrDefault();
            if (null == fs)
            {

                return;
            }
            txtContent.Text = item.BetContent;
            //"zhouke"
            var betUser = IoC.Resolve<ISysUserService>().Get(item.Code);
            if (null == betUser)
            {
                return;
            }
            List<WinsEntity> winSource = new List<WinsEntity>();
            if (!fs.HasMoreBonus)//普通奖金
            {
                //返点/舍弃返点
                string backText = string.Empty;
                double dm = 0;
                if (betUser.PlayType == 0)//1800
                {
                    dm = Math.Round((fs.MaxRebate - betUser.Rebate), 1);
                    backText = item.PrizeType == 1 ? " - " + dm + " % " : " - 0%";
                }
                else
                {
                    dm = Math.Round((fs.MaxRebate1700 - betUser.Rebate), 1);
                    backText = item.PrizeType == 1 ? " - " + dm + " % " : " - 0%";
                }
                if (dm <= 0)
                    dongtaiMonery.Visible = false;
                lbBackNum.Text = TotalWinMoney(item, 1, fs, betUser, false).ToString();
                lbBackNumlst.Text = backText;
                //如果为lhc，则隐藏返点
                if (item.LotteryCode == "hk6")
                {
                    dongtaiMonery.Visible = false;
                }

                winSource.Add(new WinsEntity()
                {
                    itemContent = txtContent.Text,
                    meWinMoney = item.LotteryCode == "hk6" ? (item.TotalAmt * (decimal)Ytg.ServerWeb.BootStrapper.SiteHelper.GetLiuHeMiult()).ToString() : TotalWinMoney(item, 1, fs, betUser).ToString(),
                    Multiple = item.Multiple,
                    PlayTypeRadioName = string.IsNullOrEmpty(item.PostionName) ? item.PlayTypeRadioName : item.PostionName
                });
            }
            else
            {//拥有更多中奖方式的

                List<string> ct = new List<string>();
                if (item.BetContent.IndexOf("-") >= 0)
                {
                    var cts = item.BetContent.Split('-');
                    foreach (var c in cts)
                    {
                        if (string.IsNullOrEmpty(c))
                            continue;
                        ct.Add(SpecialConvert.ConvertTo(Convert.ToInt32("-" + c)));
                    }
                }
                var wins = PlayTypeRadiosBonusServiceCatch.GetAll().Where(c => c.RadioCode == fs.RadioCode).ToList();
                //是否为特殊玩法
                for (var i = 0; i < wins.Count; i++)
                {
                    var itemw = wins[i];
                    if (ct.Count > 0 && !ct.Contains(itemw.BonusTitle))
                        continue;
                    decimal[] models = { 1M, 0.1M, 0.01M, 0.001M };
                    winSource.Add(new WinsEntity()
                    {
                        itemContent = txtContent.Text,
                        meWinMoney = (Ytg.Comm.Global.DecimalConvert(betUser.PlayType == 0 ? itemw.BonusBasic : itemw.BonusBasic17) * models[item.Model] * item.Multiple).ToString(),
                        Multiple = item.Multiple,
                        PlayTypeRadioName = HanZi[i] + "等奖_" + itemw.BonusTitle
                    });
                }
                dongtaiMonery.Visible = false;
            }
            this.rptWins.DataSource = winSource;
            this.rptWins.DataBind();

        }

        /// <summary>
        /// 计算中奖金额,stepAmt * 10 * item.BackNum *10得意思是：除以0.1相当于*10
        /// </summary>
        /// <param name="item">投注详情</param>
        /// <param name="baseAmt">基础奖金</param>
        /// <param name="stepAmt">每增加0.1的返点 增加多少奖金</param>
        /// <param name="count">中多少注</param>
        /// <returns></returns>
        protected decimal TotalWinMoney(BetList item, int count, PlayTypeRadio fs, SysUser user, bool isAppendMiu = true)
        {
            decimal[] models = { 1M, 0.1M, 0.01M, 0.001M };
            //计算
            decimal stepAmt = 0;
            decimal baseAmt = 0;
            //返点/舍弃返点
            if (user.PlayType == 0)//1800
            {
                baseAmt = fs.MaxBonus - Convert.ToDecimal(user.Rebate) * 10 * fs.StepAmt;
                if (item.PrizeType == 1)
                    baseAmt = fs.BonusBasic;
            }
            else
            {
                baseAmt = fs.MaxBonus17 - Convert.ToDecimal(user.Rebate) * 10 * fs.StepAmt1700;
                if (item.PrizeType == 1)
                    baseAmt = fs.BonusBasic17;
            }

            decimal total = 0;
            if (isAppendMiu)
                total = count * (baseAmt - stepAmt) * models[item.Model] * item.Multiple;
            else
                total = (baseAmt - stepAmt) * models[item.Model];
            return Math.Round(total, 4);
        }

    }

    public class WinsEntity
    {
        public string PlayTypeRadioName { get; set; }

        public string itemContent { get; set; }

        public int Multiple { get; set; }

        public string meWinMoney { get; set; }
    }


}