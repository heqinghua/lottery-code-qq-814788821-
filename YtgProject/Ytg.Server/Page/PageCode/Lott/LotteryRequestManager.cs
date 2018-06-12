using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.BasicModel.LotteryBasic;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.Page.PageCode.Lott
{
    /// <summary>
    /// 彩票模块
    /// </summary>
    public class LotteryRequestManager : BaseRequestManager
    {
        readonly IPlayTypeService mPlayTypeService;

        readonly IPlayTypeRadioService mPlayTypeRadioService;

        readonly IPlayNumTypeService mPlayTypeNumService;

        readonly ILotteryTypeService mLotteryTypeService;

        readonly IPlayTypeRadiosBonusService mPlayTypeRadiosBonusService;

        readonly ILotteryIssueService mLotteryIssueService;

        public LotteryRequestManager(ILotteryTypeService lotteryTypeService, IPlayTypeService playTypeService,
            IPlayTypeRadioService playTypeRadioService, 
            IPlayNumTypeService playNumTypeService,
            IPlayTypeRadiosBonusService playTypeRadiosBonusService,
            ILotteryIssueService lotteryIssueService
            )
        {
            this.mLotteryTypeService = lotteryTypeService;
            this.mPlayTypeService = playTypeService;
            this.mPlayTypeRadioService = playTypeRadioService;
            this.mPlayTypeNumService = playNumTypeService;
            this.mPlayTypeRadiosBonusService = playTypeRadiosBonusService;
            this.mLotteryIssueService = lotteryIssueService;
            
        }

        protected override bool Validation()
        {
            return true;
        }

        protected override void Process()
        {
            switch (this.ActionName.ToLower())
            {
                case "playtype":
                    this.GetAllPlayType();
                    break;
                case "playnum":
                    //玩法二类别
                    this.GetAllPlayNum();
                    break;
                case "playradio":
                    this.GetPlayRadio();
                    break;
                case "lotttype":
                    this.GetAllLottTypes();
                    break;
                case "playtyperadiosbonus"://获取玩法单选奖金
                    this.GetPlayTypeRadiosBonus();
                    break;
                case "htmlradio":
                    this.GetPlayReduioForGame();
                    break;
                case "opens":
                    this.GetLotteryOpens();
                    break;
                case "top50":
                    this.GetLotteryTop50();
                    break;
                case "top20trend":
                    this.GetLotteryTop20Trend();
                    break;
                case "top100":
                    this.GetLotteryTop100();
                    break;
            }

        }

        #region action

        /// <summary>
        /// 获取前20条开奖记录，用于mobile
        /// </summary>
        private void GetLotteryTop20Trend()
        {
            /*
              'gid': trendGid,
                'count' : trendCount,
                'pos' : trendPos
             */
            RecordParent reult = new RecordParent();
            int gid = 0;
            int pos = 0;
            if (!int.TryParse(Request.Params["gid"], out gid))
            {
                gid = 1;
            }
            if (!int.TryParse(Request.Params["pos"], out pos))
            {
                pos = 1;
            }

            var result = this.mLotteryIssueService.GetTop50OpendIssue(gid).OrderBy(x=>x.IssueCode).ToList();
            //运算遗漏
            reult.Records = new List<Record>();
            int allCount = result.Count;
            reult.RecordCount = allCount > 20 ? 20 : allCount; 
            Record preRecord = null;
            foreach (var item in result)
            {
                if (string.IsNullOrEmpty(item.Result))
                    continue;
                var res = Convert.ToInt32(item.Result.Replace(",", "")[pos].ToString());
                Record record = new Record();
                record.iOpenNum = res;
                record.sGamePeriod = item.IssueCode;
                if (preRecord != null)
                {
                    record.AppendYl(preRecord);
                    if (res == 0)
                    {
                        record.iYL0 = 0; //+= preRecord.iYL0 == 0 ? 1 : preRecord.iYL0;
                        reult.iYLAvg0++;
                    }
                    else if (res == 1)
                    {
                        record.iYL1 = 0;//+= preRecord.iYL1 == 0 ? 1 : preRecord.iYL1;
                        reult.iYLAvg1++;
                    }
                    else if (res == 2)
                    {
                        record.iYL2 = 0; //+= preRecord.iYL2 == 0 ? 1 : preRecord.iYL2;
                        reult.iYLAvg2++;
                    }
                    else if (res == 3)
                    {
                        record.iYL3 = 0; //+= preRecord.iYL3 == 0 ? 1 : preRecord.iYL3;
                        reult.iYLAvg3++;
                    }
                    else if (res == 4)
                    {
                        record.iYL4 = 0; //+= preRecord.iYL4 == 0 ? 1 : preRecord.iYL4;
                        reult.iYLAvg4++;
                    }
                    else if (res == 5)
                    {
                        record.iYL5 = 0; //+= preRecord.iYL5 == 0 ? 1 : preRecord.iYL5;
                        reult.iYLAvg5++;
                    }
                    else if (res == 6)
                    {
                        record.iYL6 = 0; //+= preRecord.iYL6 == 0 ? 1 : preRecord.iYL6;
                        reult.iYLAvg6++;
                    }
                    else if (res == 7)
                    {
                        record.iYL7 = 0;//+= preRecord.iYL7 == 0 ? 1 : preRecord.iYL7;
                        reult.iYLAvg7++;
                    }
                    else if (res == 8)
                    {
                        record.iYL8 = 0; //+= preRecord.iYL8 == 0 ? 1 : preRecord.iYL8;
                        reult.iYLAvg8++;
                    }
                    else if (res == 9)
                    {
                        record.iYL9 = 0; //+= preRecord.iYL9 == 0 ? 1 : preRecord.iYL9;
                        reult.iYLAvg9++;
                    }

                }
                             

                reult.Records.Add(record);
                preRecord = record;
            }
            //平均遗漏
            if (reult.iYLAvg0 != 0)
                reult.iYLAvg0 = allCount / reult.iYLAvg0;
            if (reult.iYLAvg1 != 0)
                reult.iYLAvg1 = allCount / reult.iYLAvg1;
            if (reult.iYLAvg2 != 0)
                reult.iYLAvg2 = allCount / reult.iYLAvg2;
            if (reult.iYLAvg3 != 0)
                reult.iYLAvg3 = allCount / reult.iYLAvg3;
            if (reult.iYLAvg4 != 0)
                reult.iYLAvg4 = allCount / reult.iYLAvg4;
            if (reult.iYLAvg5 != 0)
                reult.iYLAvg5 = allCount / reult.iYLAvg5;
            if (reult.iYLAvg6 != 0)
                reult.iYLAvg6 = allCount / reult.iYLAvg6;
            if (reult.iYLAvg7 != 0)
                reult.iYLAvg7 = allCount / reult.iYLAvg7;
            if (reult.iYLAvg8 != 0)
                reult.iYLAvg8 = allCount / reult.iYLAvg8;
            if (reult.iYLAvg9 != 0)
                reult.iYLAvg9 = allCount / reult.iYLAvg9;
            //最大遗漏
            foreach (var item in reult.Records)
            {
                if (item.iYL0 > reult.iYLMax0)
                    reult.iYLMax0 = item.iYL0;

                if (item.iYL1 > reult.iYLMax1)
                    reult.iYLMax1 = item.iYL1;

                if (item.iYL2 > reult.iYLMax2)
                    reult.iYLMax2 = item.iYL2;

                if (item.iYL3 > reult.iYLMax3)
                    reult.iYLMax3 = item.iYL3;

                if (item.iYL4 > reult.iYLMax4)
                    reult.iYLMax4 = item.iYL4;

                if (item.iYL5 > reult.iYLMax5)
                    reult.iYLMax5 = item.iYL5;

                if (item.iYL6 > reult.iYLMax6)
                    reult.iYLMax6 = item.iYL6;

                if (item.iYL7 > reult.iYLMax7)
                    reult.iYLMax7 = item.iYL7;

                if (item.iYL8 > reult.iYLMax8)
                    reult.iYLMax8 = item.iYL8;

                if (item.iYL9 > reult.iYLMax9)
                    reult.iYLMax9 = item.iYL9;
                
            }
            


            AppGlobal.RenderResult<RecordParent>(ApiCode.Success, reult);

        }

        /// <summary>
        /// 获取前50条开奖记录
        /// </summary>
        private void GetLotteryTop50()
        {
            int lid = -1;
            if (!int.TryParse(Request.Params["gid"], out lid))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            var result = this.mLotteryIssueService.GetTop5OpendIssue(lid);
            AppGlobal.RenderResult<List<LotteryIssue>>(ApiCode.Success, result);
        }

        /// <summary>
        /// 获取前50条开奖记录
        /// </summary>
        private void GetLotteryTop100()
        {
            int lid = -1;
            if (!int.TryParse(Request.Params["gid"], out lid))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            var result = this.mLotteryIssueService.GetTop100OpendIssue(lid);
            AppGlobal.RenderResult<List<LotteryIssue>>(ApiCode.Success, result);
        }

        /// <summary>
        /// 获取所有列表
        /// </summary>
        private void GetLotteryOpens()
        {
            var result= this.mLotteryIssueService.GetAllLotteryOpens();
            AppGlobal.RenderResult<List<LotteryIssueDTO_Opens>>(ApiCode.Success, result);
        }

        /// <summary>
        /// 加载所有游戏类型
        /// </summary>
        private void GetAllLottTypes()
        {
            var source= this.mLotteryTypeService.GetAll().Where(c=>c.IsEnable).OrderBy(c=>c.Sort);
            AppGlobal.RenderResult<IEnumerable<LotteryType>>(ApiCode.Success, source);
        }
        /// <summary>
        /// 获取所有玩法类型，根据类型
        /// </summary>
        private void GetAllPlayType()
        {
            var source = this.mPlayTypeService.GetAll().ToList();
            AppGlobal.RenderResult<IEnumerable<PlayType>>(ApiCode.Success, source);
        }

        /// <summary>
        /// 获取所有玩法二类
        /// </summary>
        private void GetAllPlayNum()
        {
            var source = mPlayTypeNumService.GetAll();
            AppGlobal.RenderResult<IEnumerable<PlayTypeNum>>(ApiCode.Success, source);
        }

        /// <summary>
        /// 获取玩法单选
        /// </summary>
        private void GetPlayRadio()
        {
            AppGlobal.RenderResult<List<PlayTypeRadio>>(ApiCode.Success, Ytg.Service.Lott.BaseDataCatch.GetPalyTypeRadio(this.mPlayTypeRadioService));
        }

        /// <summary>
        /// 根据采种获取玩法
        /// </summary>
        public void GetPlayReduioForGame()
        {
            var result = this.mPlayTypeRadioService.GetRadios(this.LotteryId);
            //获取期号
            var issueSource = this.mLotteryIssueService.GetNowDayLotteryTypeIssue(this.LotteryId).ToList();
            string issueStr = string.Empty;
            if (issueSource != null && issueSource.Count > 0)
            {
                foreach (var item in issueSource)
                    issueStr += item.IssueCode + ",";
            }

            AppGlobal.RenderResult<List<PlayRadioHtmlDTO>>(ApiCode.Success, result, issueStr);
        }

        /// <summary>
        /// 获取玩法单选奖金
        /// </summary>
        private void GetPlayTypeRadiosBonus()
        {
            var source = this.mPlayTypeRadiosBonusService.GetAll().ToList();
            AppGlobal.RenderResult<List<PlayTypeRadiosBonus>>(ApiCode.Success, source);
        }
        #endregion
    }
}