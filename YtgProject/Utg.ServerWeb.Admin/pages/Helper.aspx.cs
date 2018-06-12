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

namespace Utg.ServerWeb.Admin.pages
{
    public partial class Helper : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.Params["action"];
            if (!string.IsNullOrEmpty(action))
            {
                switch (action)
                {
                    case "lotteryRadio"://根据彩种获取所有玩法
                        this.GetLotteryRadio();
                        break;
                    case "filterBetting":
                        this.FilterBetting();//查询
                        break;
                    case "undo"://撤单
                        this.Undo();
                        break;
                    case "delete"://删单
                        this.Delete();
                        break;
                    case "edit"://修单
                        this.Edit();
                        break;
                }

                Response.End();
            }
        }


        #region 根据玩法获取所有彩种
        private void GetLotteryRadio()
        {
            string lotteryCode = Request.Params["lotteryCode"];
            string radioCode = Request.Params["radioCode"];
            var result = IoC.Resolve<IPlayTypeRadioService>().GetPattRado(lotteryCode, radioCode);
            if (result == null)
            {
                AppGlobal.RenderResult(ApiCode.Empty, "");
                return;
            }
            AppGlobal.RenderResult<List<PlayRado>>(ApiCode.Success, result);
        }
        #endregion

        #region 查询投注记录
        private void FilterBetting()
        {
            DateTime beginTime;
            DateTime endTime;
            if (!DateTime.TryParse(Request.Params["beginTime"], out beginTime)
                || !DateTime.TryParse(Request.Params["endTime"], out endTime))
            {
                beginTime = DateTime.Now.AddMinutes(-10);
                endTime = DateTime.Now;
            }

            int status = Utils.ReqInt("status");//= Convert.ToInt32((cmStatus.SelectedItem as ComboBoxItem).Tag.ToString());
            int palyRadioCode = Utils.ReqInt("palyRadioCode");//= cmLotteryPalyType.SelectPalyType;
            int model = Utils.ReqInt("model");// = Convert.ToInt32((cmModel.SelectedItem as ComboBoxItem).Tag.ToString());
            int userType = Utils.ReqInt("userType");//= Convert.ToInt32((cmUserType.SelectedItem as ComboBoxItem).Tag.ToString());

            string lotteryCode = Request.Params["lotteryCode"];
            string issueCode = Request.Params["IssueCode"];
            string betCode = Request.Params["BetCode"];
            string userCode = Request.Params["userCode"];

            int pageIndex = Convert.ToInt32(Request.Params["pageIndex"]);
            int pageSize = 20;
            int totalCount = 0;
            var result = IoC.Resolve<IBetDetailService>().GetBetListBy(beginTime, endTime, status, 2, lotteryCode, palyRadioCode, issueCode, model, betCode, userCode, userType, -1, pageIndex, pageSize, ref totalCount);
            //var result = IoC.Resolve<IBetDetailService>().GetBetRecord(beginTime, endTime, status, lotteryCode, palyRadioCode, issueCode, model, betCode, userCode, userType, pageIndex, pageSize, ref totalCount);
            AppGlobal.RenderResult<List<BetList>>(ApiCode.Success, result, "", 0, totalCount);
        }
        #endregion

        #region 撤单
        private void Undo() {
            string id = Request.Params["betCode"];
            string catchNum = Request.Params["catchNum"];
            if (!string.IsNullOrEmpty(id))
            {
                ISysUserBalanceService userBanceService = IoC.Resolve<ISysUserBalanceService>();
                if (userBanceService.Cannel(id))
                    AppGlobal.RenderResult(ApiCode.Success, "");
                else
                    AppGlobal.RenderResult(ApiCode.Success, "");

                //修改当前内容为系统撤单
                if (id.ToLower().StartsWith("i"))
                {
                    //追号
                    this.CannelCatchNum(catchNum, id);
                }
                else
                {
                    //投注
                    this.CannelBettNum(id);
                }
            }
          
        }

        /// <summary>
        /// 撤销投注
        /// </summary>
        /// <param name="betcode"></param>
        private void CannelBettNum(string betcode)
        {

            if (string.IsNullOrEmpty(betcode))
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            IBetDetailService betDetailService = IoC.Resolve<IBetDetailService>();
            //验证请求参数是否正确
            var fs = betDetailService.Where(item => item.BetCode == betcode).FirstOrDefault();
            if (null == fs)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            //撤单处理
            fs.Stauts = BetResultType.SysCancel;
            betDetailService.Save();

            //返还金额
            ISysUserBalanceService sysUserBalanceService = IoC.Resolve<ISysUserBalanceService>();
            var details = new SysUserBalanceDetail()
            {
                RelevanceNo = fs.BetCode,
                SerialNo = "b" + Utils.BuilderNum(),
                Status = 0,
                TradeAmt = fs.TotalAmt,
                TradeType = TradeType.撤单返款,
                UserId = fs.UserId
            };
            sysUserBalanceService.UpdateUserBalance(details, fs.TotalAmt);
        }

        /// <summary>
        /// 终止追号
        /// </summary>
        private void CannelCatchNum(string catchNum,string catchNumIssueCode)
        {

           ISysCatchNumService sysCatchNumService= IoC.Resolve<ISysCatchNumService>();
           ISysCatchNumIssueService sysCatchNumIssueService = IoC.Resolve<ISysCatchNumIssueService>();
            //验证当前单号是否正在进行
            var fs = sysCatchNumService.Where(item => item.CatchNumCode == catchNum).FirstOrDefault();
            if (fs == null)
            {
                //非法撤单
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            //获取注单详情
            var source = sysCatchNumIssueService.GetCatchIssue(catchNum);
           
            //获取用户余额
            CatchNumIssue catchNumIssue=null;
            int noCannelCount = 0;
            foreach (var item in source)
            {

                //未开奖的状态才允许撤单
                if (catchNumIssueCode == item.CatchNumIssueCode)
                {
                    fs.UserCannelIssue++;
                    fs.UserCannelMonery += item.TotalAmt;
                    item.Stauts = BetResultType.SysCancel; //对本注进行撤单
                    catchNumIssue = item;

                }
                if (item.Stauts == BetResultType.NotOpen)
                    noCannelCount++;
            }
            //撤单
            sysCatchNumIssueService.Save();
            if (noCannelCount <= 0) //全部撤销
                sysCatchNumService.CannelCatch(catchNum);
           sysCatchNumService.Save();

           //返还金额
           if (catchNumIssue != null)
           {
               ISysUserBalanceService sysUserBalanceService = IoC.Resolve<ISysUserBalanceService>();
               var details = new SysUserBalanceDetail()
                        {
                            RelevanceNo = catchNumIssue.CatchNumIssueCode,
                            SerialNo = "b" + Utils.BuilderNum(),
                            Status = 0,
                            TradeAmt = catchNumIssue.TotalAmt,
                            TradeType = TradeType.追号返款,
                            UserId = fs.UserId
                        };
               sysUserBalanceService.UpdateUserBalance(details, catchNumIssue.TotalAmt);
           }
        }
        #endregion

        #region 删单

        private void Delete()
        {
            int id = Utils.ReqInt("id");
            if (id < 1)
            {
                AppGlobal.RenderResult(ApiCode.Empty, "");
                return;
            }
            if (IoC.Resolve<IBetDetailService>().DeleteById(id))
            {
                AppGlobal.RenderResult(ApiCode.Success, "");
            }
            else
            {
                AppGlobal.RenderResult(ApiCode.Fail, "");
            }
        }

        #endregion

        #region 修改

        private void Edit()
        {
            int id = 0;
            string issueCode = Request.Params["issueCode"];
            string betContent = Request.Params["betContent"];
            int.TryParse(Request.Params["id"].ToString(), out id);
            string catNuo = Request.Params["catNuo"];
            if (string.IsNullOrEmpty(catNuo))
            {
                AppGlobal.RenderResult(ApiCode.Fail, "");
            }
            else
            {
                if (catNuo.StartsWith("c"))
                {
                    //修改追号
                    var catService= IoC.Resolve<ISysCatchNumService>();
                    var fs= catService.GetAll().Where(x => x.CatchNumCode == catNuo).FirstOrDefault();
                    if (fs != null)
                    {
                        fs.BetContent = betContent;
                        catService.Save();
                        AppGlobal.RenderResult(ApiCode.Success, "");
                    }
                    else
                    {
                        AppGlobal.RenderResult(ApiCode.Fail, "");
                    }
                    
                }
                else
                {
                    var betdetail = IoC.Resolve<IBetDetailService>();
                    var fs= betdetail.GetAll().Where(x=>x.BetCode==catNuo).FirstOrDefault();
                    if (fs!=null)
                    {
                        fs.BetContent = betContent;
                        betdetail.Save();
                        AppGlobal.RenderResult(ApiCode.Success, "");
                    }
                    else
                    {
                        AppGlobal.RenderResult(ApiCode.Fail, "");
                    }
                }
            }
        }

        #endregion
    }
}
