using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.Page.PageCode.Lott
{
    /// <summary>
    /// 彩票期数相关
    /// </summary>
    public class LotteryBasicRequestManager : BaseRequestManager
    {
        readonly ILotteryIssueService mLotteryIssueService;

        readonly IRankingsService mRankingsService;

        public LotteryBasicRequestManager(ILotteryIssueService lotteryIssueService, IRankingsService rankingsService)
        {
            this.mLotteryIssueService = lotteryIssueService;
            this.mRankingsService = rankingsService;
        }

        protected override bool Validation()
        {
            return true;
        }


        protected override void Process()
        {
            switch (this.ActionName.ToLower())
            {
                case "notopenissue":
                    GetNotOpenIssue();
                    break;
                case "openresult":
                    GetOpenResult();
                    break;
                case "topopendresult"://取最近已经开奖的五期数据
                    this.TopOpendResult();
                    break;
                case "nowlotteryissuecode"://获取指定期数当天所有期数
                    GetNowLotteryIssueCode();
                    break;
                case "iscannelnum"://当期是否允许撤单
                    IsCannelNum();
                    break;
                case "getnowsalesissue"://获取当前正在销售的期数
                    this.GetInowsalesIssue();
                    break;
                case "winlst":
                    var res = this.mRankingsService.GetAll().OrderByDescending(x => x.WinMonery).Take(40).ToList();
                    AppGlobal.RenderResult<List<Rankings>>(ApiCode.Success, res);

                    break;
        
               
            }
        }

        #region 获取当前正在销售的期数

        private void GetInowsalesIssue()
        {
            DateTime now = DateTime.Now;
            var issue = this.mLotteryIssueService.GetNowSalesIssue(this.LotteryId);
            if (null == issue)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
            }
            issue.LotteryTime = now;
            AppGlobal.RenderResult<LotteryIssue>(ApiCode.Success,issue);
        }

        #endregion

        #region action

        //获取当天最近开奖的数据
        private void TopOpendResult()
        {
            var source = this.mLotteryIssueService.GetTopOpendIssue(this.LotteryId);
            AppGlobal.RenderResult<List<LotteryIssue>>(ApiCode.Success, source);
        }

        /// <summary>
        /// 获取当天未开奖数据
        /// </summary>
        private void GetNotOpenIssue()
        {
            int lotteryId;
            if (!int.TryParse(Request.QueryString["lotteryId"],out lotteryId))
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }

            var source = this.mLotteryIssueService.GetOccDayNoOpenLotteryIssue(lotteryId);
            AppGlobal.RenderResult<IEnumerable<LotteryIssueDTO>>(ApiCode.Success, source);
        }

        /// <summary>
        /// 获取开奖结果，根据期数编号
        /// </summary>
        private void GetOpenResult()
        {
            string issue = Request.Params["issue"];
            if (string.IsNullOrEmpty(issue))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            var item = this.mLotteryIssueService.GetIssueOpenResult(issue,this.LotteryId);
            if (item == null)
            {
                AppGlobal.RenderResult(ApiCode.Empty);
                return;
            }

            AppGlobal.RenderResult<LotteryIssueDTO>(ApiCode.Success, item);
        }
        #endregion

        #region 获取指定采种当天所有期数
        private void GetNowLotteryIssueCode()
        {
            int lotteryId = 0;
            if (!int.TryParse(Request.Params["lotteryid"], out lotteryId))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var source = this.mLotteryIssueService.GetNowDayLotteryTypeIssue(lotteryId).ToList();
            AppGlobal.RenderResult<List<LotteryIssue>>(ApiCode.Success, source);
        }
        #endregion

        #region 验证指定期数是否允许撤单
        private void IsCannelNum()
        {
            string issue = Request.Params["issueCode"];
            if(string.IsNullOrEmpty(issue) || string.IsNullOrEmpty(this.LotteryCode)){
                AppGlobal.RenderResult(ApiCode.Fail);
                    return;
            }
            var item= this.mLotteryIssueService .Where(c=>c.LotteryId==LotteryId && c.IssueCode==issue).FirstOrDefault();
            if (item == null || item.EndSaleTime.Value.Subtract(DateTime.Now).TotalSeconds <= TimeSpan.FromSeconds(0).TotalSeconds)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
            }
            else
            {
                AppGlobal.RenderResult(ApiCode.Success);
            }
        }
        #endregion
    }
}