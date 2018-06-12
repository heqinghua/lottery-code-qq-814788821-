using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.BasicModel.LotteryBasic;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;
using Ytg.ServerWeb.BootStrapper;
using Ytg.Service.Logic;

namespace Ytg.ServerWeb.Page.PageCode.Lott
{
    /// <summary>
    /// 投注明细
    /// </summary>
    public class BetDetailRequestManager : BaseRequestManager
    {
        private readonly IBetDetailService mBetDetailService;

        private readonly ISysUserService mSysUserService;

        private readonly ISysUserBalanceService mSysUserBalanceService;

        private readonly ISysUserBalanceDetailService mSysUserBalanceDetailService;

        private readonly ISysCatchNumService mSysCatchNumService;//追号

        private readonly ISysCatchNumIssueService mSysCatchNumIssueService;//追号详情

        private readonly ILotteryIssueService mLotteryIssueService;//期数

        private readonly IPlayTypeRadioService mPlayTypeRadioService;//玩法单选

        private readonly ILotteryTypeService mLotteryTypeService;//彩票种类

        private readonly IBuyTogetherService mBuyTogetherService;//合买详情

        private RebateHelper mRebateHelper = null;//返点辅助

        public const decimal ByMinBili = (decimal)10 / (decimal)100;//代购最低认购比例


        public BetDetailRequestManager(IBetDetailService betDetailService,
            ISysUserService sysUserService,
            ISysUserBalanceService sysUserBalanceService,
            ISysUserBalanceDetailService sysUserBalanceDetailService,
            ISysCatchNumService sysCatchNumService,
            ISysCatchNumIssueService sysCatchNumIssueService,
            ILotteryIssueService lotteryIssueService,
            IPlayTypeRadioService playTypeRadioService,ILotteryTypeService lotteryTypeService,
            IBuyTogetherService buyTogetherService)
        {
            this.mBetDetailService = betDetailService;
            this.mSysUserService = sysUserService;
            this.mSysUserBalanceService = sysUserBalanceService;
            this.mSysUserBalanceDetailService = sysUserBalanceDetailService;
            this.mSysCatchNumService = sysCatchNumService;
            this.mSysCatchNumIssueService = sysCatchNumIssueService;
            this.mLotteryIssueService = lotteryIssueService;
            this.mPlayTypeRadioService = playTypeRadioService;
            this.mLotteryTypeService = lotteryTypeService;
            this.mBuyTogetherService = buyTogetherService;

            this.mRebateHelper = new RebateHelper(this.mSysUserService, this.mSysUserBalanceService, this.mSysUserBalanceDetailService);
        
        }

        protected override bool Validation()
        {
            return true;
        }


        protected override void Process()
        {
            switch (this.ActionName.ToLower())
            {
                case "addbetdetail":
                    //投注
                   // this.AddBetDetails();
                    break;
                case "htdbetdetail"://html投注
                    this.HtmlAddBetDetails();
                    break;
                case "betlist"://投注记录
                    this.SelectBetList();
                    break;
                case "notopenbetlist"://获取当前用户未开奖的投注记录
                    this.GetNotOpenBetList();
                    break;
                case "catchnumlist"://追号记录查询
                    this.SelectCatchNumList();
                    break;
                case "getcatchissue"://根据追号编号获取追号详细
                    GetCatchIssue();
                    break;
                case "cannelcatchnum"://终止追号
                    this.CannelCatchNum();
                    break;
                case "cannelbethnum"://投注撤单
                    this.cannelBettNum();
                    break;
                case "betdetailbyuserbalanceseriano":
                    this.GetBetDetailByUserBalanceDetailSeriaNo();
                    break;
                case "getbetdetail"://根据投注编号获取投注信息
                    this.GetBetDetail();
                    break;
                case "catchinfo"://根据追号单和追号期号获取指定期号信息
                    GetCatchInfo();
                    break;
                case "nextperiod": //获取开奖剩余时间
                    GetNextPeriod();
                    break;
                case "nextperiodnew": //获取开奖剩余时间---2017/11/07
                    GetNextPeriodNew();
                    break;
                case "hmlst"://获取合买集合
                    GetHemaIndex();
                    break;
                case "hmlstfc"://合买fc
                    GetHemaFc();
                    break;

                case "hmitem"://获取单项合买
                    GetHemaIndexItem();
                    break;
                case "groupby":
                    GroupBy();//提交合买
                    break;
                case "groupbyuserlst":
                    this.GetBettGroupByUserLst();//获取合买用户列表
                    break;

            }
        }

        /// <summary>
        /// 获取投注合买集合
        /// </summary>
        private void GetBettGroupByUserLst()
        {
            int bettid = 0;
            if (!int.TryParse(Request.Params["bettid"], out bettid))
            {
                AppGlobal.RenderResult(ApiCode.Empty);
                return;
            }
            var res = this.mBuyTogetherService.GetForBettidLst(bettid);
            AppGlobal.RenderResult<List<BuyTogether>>(ApiCode.Success, res);
        }

        /// <summary>
        /// 合买
        /// </summary>
        private void GroupBy()
        {

            int betid = -1;
            decimal subscription = 0;
            if (!int.TryParse(Request.Params["betid"], out betid))
            {
                AppGlobal.RenderResult(ApiCode.Empty);
                return;
            }
            if (!decimal.TryParse(Request.Params["subscription"], out subscription) || subscription < 1)
            {
                AppGlobal.RenderResult(ApiCode.Empty);
                return;
            }
            var user = this.mSysUserService.GetUserAndZiJin(this.LoginUserId);//获取用户返点
            if (null == user || user.IsDelete)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            if (user.Status == 1)
            {
                //资金被禁用
                AppGlobal.RenderResult(ApiCode.DisabledMonery);
                return;
            }

            var item = new BuyTogether();
            item.BetDetailId = betid;
            item.BuyTogetherCode ="h"+Utils.BuilderNum();
            item.UserId = this.LoginUserId;
            item.Subscription = subscription;
            int state = this.mBuyTogetherService.AddTogether("d" + Utils.BuilderNum(), (int)TradeType.投注扣款, item);
            Ytg.Scheduler.Comm.LogManager.Info(" 提交合买："+ item + "状态：" + state);
            ApiCode errorType = ApiCode.Success;
            if (state == 1)
            {//超过投注时间，不允许投注
                errorType = ApiCode.Empty;
            }
            else if (state == 2)
            {
                //用户余额不够
                errorType = ApiCode.NotEnough;//余额不够本次
            }
            else if (state == 4)
            {
                errorType = ApiCode.RequestFail;//非未开奖状态，无法进行购买
            }
            else
            {
                //提交成功
            }
            AppGlobal.RenderResult(errorType);

        }

        /// <summary>
        /// 获取合买
        /// </summary>
        private void GetHemaIndexItem()
        {
            int betid = 0;
            if (!int.TryParse(Request.Params["id"], out betid))
            {
                AppGlobal.RenderResult(ApiCode.Empty);
                return;
            }
            var item = mBetDetailService.GetBetItemBy(betid);
            if (item == null)
                AppGlobal.RenderResult(ApiCode.Fail);
            else
                AppGlobal.RenderResult<HeMaiDtoChildren>(ApiCode.Success, item);
        }
        /// <summary>
        /// 获取合买
        /// </summary>
        private void GetHemaIndex()
        {
            int pageIndex;
            int pageSize;
            int totalCount = 0;
            bool ispageIndex = int.TryParse(Request.Params["pageIndex"], out pageIndex);
            if (!ispageIndex) pageIndex = 1;
            bool ispageSize = int.TryParse(Request.Params["pageSize"], out pageSize);
            if (!ispageSize) pageSize = 20;
            int stauts = -1;
            string nickName = Request.Params["nickName"];
            if (!int.TryParse(Request.Params["stauts"], out stauts))
                stauts = -1;
            string lotteryCode = Request.Params["lotterycode"];
            var res = mBetDetailService.GetBetListBy(stauts, nickName, pageIndex, pageSize, lotteryCode, ref totalCount);
            AppGlobal.RenderResult<List<HeMaiDto>>(ApiCode.Success, res, "", pageIndex, totalCount);
        }

        public void GetHemaFc()
        {
            var res =mBetDetailService.GetBetListByFc();
            AppGlobal.RenderResult<List<HeMaiDto>>(ApiCode.Success, res);
        }


        private void GetNextPeriodNew()
        {
            var gid = Request.Params["gid"];
            if (string.IsNullOrEmpty(gid))
            {
                AppGlobal.RenderResult(ApiCode.Empty);
                return;
            }
            List<NextPeriodNewInfo> nextPeriods = new List<NextPeriodNewInfo>();
            var res = this.mLotteryIssueService.GetNextPreds(gid);

            /*组织数据*/
            foreach (var item in res)
            {
                var fs = nextPeriods.Where(x => x.LotteryId == item.LotteryId).FirstOrDefault();
                if (fs != null)
                {
                    //存在，对数据进行组织
                    if (fs.EndSaleTime <= DateTime.Now)
                    {
                        continue;
                    }
                    fs.LotteryId = item.LotteryId.Value;
                    fs.IssueCode = item.IssueCode;
                    fs.EndTime = item.EndTime.Value;
                    fs.EndSaleTime = item.EndSaleTime.Value;
                    fs.LotteryCode = item.LotteryName;
                    fs.Result = item.Result;
                    fs.SubTime = item.EndSaleTime.Value.Subtract(item.StartSaleTime).Ticks;

                }
                else
                {
                    NextPeriodNewInfo xd = new NextPeriodNewInfo();
                    xd.LotteryId = item.LotteryId.Value;
                    xd.IssueCode = item.IssueCode;
                    xd.EndTime = item.EndTime.Value;
                    xd.EndSaleTime = item.EndSaleTime.Value;
                    xd.LotteryCode = item.LotteryName;
                    xd.Result = item.Result;
                    xd.SubTime = item.EndSaleTime.Value.Subtract(item.StartSaleTime).Ticks;
                    //不存在，添加
                    nextPeriods.Add(xd);
                }

            }

            AppGlobal.RenderResult<List<NextPeriodNewInfo>>(ApiCode.Success, nextPeriods);

        }


        #region 获取开奖剩余时间
        private void GetNextPeriod()
        {
            var gid= Request.Params["gid"];
            if (string.IsNullOrEmpty(gid))
            {
                AppGlobal.RenderResult(ApiCode.Empty);
                return;
            }

            List<NextPeriod> nextPeriods = new List<NextPeriod>();
            var res= this.mLotteryIssueService.GetNextPreds(gid);

            /*组织数据*/
            foreach (var item in res)
            {
                var fs = nextPeriods.Where(x => x.iGameId == item.LotteryId).FirstOrDefault();
                if (fs != null)
                {
                    //存在，对数据进行组织

                    fs.iGameId = item.LotteryId.Value;
                    fs.sGamePeriod = item.IssueCode;
                    fs.iCloseTime = (item.EndTime.Value.Subtract(DateTime.Now).TotalSeconds);
                    fs.iStartTime = 0;
                    fs.iOpenTime = (item.EndTime.Value.Subtract(DateTime.Now).TotalSeconds);
                    fs.dStartTimeTEXT = item.StartTime;
                    fs.dCloseTimeTEXT = item.EndSaleTime;

                }
                else
                {
                    //不存在，添加
                    nextPeriods.Add(new NextPeriod()
                    {
                        iGameId = item.LotteryId.Value,
                        sGamePeriodPrior = item.IssueCode,
                        dStartTimePriorTEXT = item.StartTime,
                        dCloseTimePriorTEXT = item.EndSaleTime,
                        dOpenTimePriorTEXT = item.EndTime,
                    });
                }

            }
            /*
        "iGameId": 5,
           "sGamePeriod": "20170608104",
           "iCloseTime": 300, --封单时间
           "iStartTime": 0,
           "iOpenTime": 330,  --结束时间

           "sGamePeriodPrior": "20170608103",
           "dCloseTime": 42894.944097222,
           "dCloseTimeTEXT": "2017-06-08 22:39:30",
           "dStartTime": 42894.940625,
           "dStartTimeTEXT": "2017-06-08 22:34:30",

           "dOpenTimePrior": 42894.940972222,
           "dOpenTimePriorTEXT": "2017-06-08 22:35:00",
           "dCloseTimePrior": 42894.940625,
           "dCloseTimePriorTEXT": "2017-06-08 22:34:30",
           "dStartTimePrior": 42894.937152778,
           "dStartTimePriorTEXT": "2017-06-08 22:29:30",
           "sOpenNumPrior": ""
        */
            //103 2017-06-08 22:30:00.000  2017 - 06 - 08 22:35:40.000   2017 - 06 - 08 22:34:40.000
            //104 2017-06-08 22:35:00.000  2017 - 06 - 08 22:40:40.000    2017 - 06 - 08 22:39:40.000

            AppGlobal.RenderResult<List<NextPeriod>>(ApiCode.Success, nextPeriods);
           
        }
        #endregion


        #region html 投注

        /// <summary>
        /// 验证彩票种类是否在销售时间段类
        /// </summary>
        private bool VdaLotterySacllTime(int lotteryId)
        {
            var item = this.mLotteryTypeService.GetLottery(lotteryId);
            if (item == null)
                return false;
            var beginTime = item.BeginScallDate;
            var endTime = item.endSAcallDate;
            if (null == beginTime || null == endTime)
            {
                return true;
            }
            return Utils.AsyncScalle(beginTime.Value, endTime.Value);
        }

        /// <summary>
        /// html 版投注处理
        /// </summary>
        private void HtmlAddBetDetails()
        {
            /*
loginUserId=63&lotterycode=cqssc&lotteryid=1&play_source=&pmode=2&lt_project%5b%5d=%7b%27type%27%3a%27digital%27%2c%27methodid%27%3a0%2c%27codes%27%3a%274%7c4%7c4%7c4%7c4%27%2c%27nums%27%3a1%2c%27
             * omodel%27%3a2%2c%27times%27%3a1%2c%27money%27%3a2%2c%27mode%27%3a1%2c%27desc%27%3a%27%5b%u4e94%u661f_%u76f4%u9009%u590d%u5f0f%5d+4%2c4%2c4%2c4%2c4%27%7d&
             * lt_issue_start=20150707010&lt_total_nums=1&lt_total_money=2&lt_trace_if=yes&lt_trace_stop=yes&lt_trace_times_margin=1&lt_trace_margin=50&lt_trace_times_same=1&
             * lt_trace_diff=1&lt_trace_times_diff=2&lt_trace_count_input=10&lt_trace_money=20&lt_trace_issues%5b%5d=20150707011&lt_trace_issues%5b%5d=20150707012&lt_trace_issues%5b%5d=
             * 20150707013&lt_trace_issues%5b%5d=20150707014&lt_trace_times_20150707011=1&lt_trace_times_20150707012=2&lt_trace_times_20150707013=3&lt_trace_times_20150707014=4&randomNum=4371
           */

            int pmode = -1;
            int.TryParse(Request.Params["pmode"], out pmode);
            string projects =Request.Params["lt_project[]"];//投注内容
            string issueCode = Request.Params["lt_issue_start"];//投注期号
            bool lt_trace_if = Request.Params["lt_trace_if"]=="yes";//是否追号
            string lt_trace_stop = Request.Params["lt_trace_stop"];//是否中奖后自动停止
            string lt_trace_issues = Request.Params["lt_trace_issues[]"];//追号期数
            string lotteryStr = Request.Params["lotteryid"];//彩种。。
            int byType = 0;//0为代购 1为合买
            if (!int.TryParse(Request.Params["operate2"], out byType))
                byType = 0;
            decimal createrBuyPieces = 0;//最低购买金额若未合买，最低购买金额
            if (!decimal.TryParse(Request.Params["createrBuyPieces"], out createrBuyPieces))
                createrBuyPieces = 0;
            
            decimal bybili = 0.1m;//最低认购10%,
            decimal hidgame_tzallmon = 0;//总金额
            if (decimal.TryParse(Request.Params["hidgame_tzallmon"], out hidgame_tzallmon) && createrBuyPieces > 0)
            {
                bybili = createrBuyPieces / hidgame_tzallmon;//计算认购比例
            }
            Ytg.Scheduler.Comm.LogManager.Info(projects);
            int baomi = 0;
            if (!int.TryParse(Request.Params["baomi_hidden"], out baomi))
                baomi = 0;
            int lotteryid = -1;
            if (!int.TryParse(lotteryStr, out lotteryid))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            if (!VdaLotterySacllTime(lotteryid))
            {
                AppGlobal.RenderResult(ApiCode.NotSell);//不存在指定期数或者超过投注时间，不允许投注
                return;
            }
          
            //追号倍数  lt_trace_times_期号
            if (string.IsNullOrEmpty(projects)
                || string.IsNullOrEmpty(issueCode))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            projects = "[" + projects + "]";
            var bettingInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HtmlParamDto>>(projects);//投注内容
            if (bettingInfo.Count < 1)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var user = this.mSysUserService.GetUserAndZiJin(this.LoginUserId);//获取用户返点
            if (null == user || user.IsDelete )
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            if (user.Status == 1)
            {
                //资金被禁用
                AppGlobal.RenderResult(ApiCode.DisabledMonery);
                return;
            }
            
            BettingDTO betting = new BettingDTO();
            betting.BetDetails = new List<BetDetailDTO>();
            betting.IsAutoStop = lt_trace_stop == "yes" ;//是否自动停止追号
          
            try
            {
                foreach (var item in bettingInfo)
                {
                    if (string.IsNullOrEmpty(item.codes))
                        continue;
                    if (!string.IsNullOrEmpty(item.poschoose))
                        item.codes += "_" + item.poschoose;
                    
                    var details = new BetDetailDTO()
                    {
                        PalyRadioCode = item.methodid,
                        Model = item.mode - 1,
                        Multiple = item.times<0?1:item.times,
                        PrizeType = item.omodel == 2 ? 0 : 1,//omodel为2则表示舍弃返点的最高奖金 奖金类型 1 为有返点 0为无返点
                        IssueCode = issueCode,
                        BetContent = item.codes,
                        ByType= byType,
                        CreaterBuyPieces= createrBuyPieces,
                        Secrecy=baomi,
                        ByBili= bybili
                    };
                    List<BetDetailDTO> appendSource = PostionSplitHelper.Split(details);
                    betting.BetDetails.AddRange(appendSource);
                }
                //组织追号数据
                if (lt_trace_if && !string.IsNullOrEmpty(lt_trace_issues))
                { //追号，组织追号数据
                    betting.CatchDtos = new List<CatchDto>();
                    foreach (var item in lt_trace_issues.Split(','))
                    {
                        string reusetValue=Request.Params["lt_trace_times_" + item];
                        int outMultiple = 0;
                        if (!int.TryParse(reusetValue, out outMultiple))
                            continue;
                        betting.CatchDtos.Add(new CatchDto()
                        {
                            IssueCode = item,
                            Multiple = outMultiple < 1 ? 1 : outMultiple
                        });
                    }
                }

                ApiCode errorCode = ApiCode.Success;
                bool isCompled = false;
                if (lt_trace_if)//追号
                    isCompled = this.CatchBetting(user, betting, ref errorCode, lotteryid, true);
                else
                    isCompled = DefaultDetails(user, betting.BetDetails, ref errorCode, lotteryid, true);//普通投注

                if (isCompled)
                    AppGlobal.RenderResult(ApiCode.Success);
                else
                    AppGlobal.RenderResult(errorCode);

            }
            catch(Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("HtmlAddBetDetails", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }
        #endregion

        #region action

        #region 根据余额明细序列号得到投注记录
        private void GetBetDetailByUserBalanceDetailSeriaNo()
        {
            var serialNo = Request.QueryString["serialNo"];
            if (string.IsNullOrEmpty(serialNo)) AppGlobal.RenderResult(ApiCode.ParamEmpty);
            var result = this.mBetDetailService.GetBetDetailByUserBalanceDetailSeriaNo(serialNo);
            AppGlobal.RenderResult<List<BetList>>(ApiCode.Success, result, "");
        }
        #endregion

        #region 投注
        /// <summary>
        /// 投注明细 支持添加一条或多条 json格式
        /// </summary>
        private void AddBetDetails()
        {
            int userId = this.LoginUserId;
            string jsonDataStr = Request.Params["data"];
            if (string.IsNullOrEmpty(jsonDataStr))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var user = this.mSysUserService.GetUserAndZiJin(this.LoginUserId);//获取用户返点
            if (null == user)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            
            try
            {

                var bettingInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<BettingDTO>(jsonDataStr);
                if (bettingInfo.BetDetails == null || bettingInfo.BetDetails.Count < 1)
                {
                    AppGlobal.RenderResult(ApiCode.ParamEmpty);
                    return;
                }
               // var issueCode = bettingInfo.BetDetails.FirstOrDefault().IssueCode;//当期编号，
                //
                //if (!VdaActionEndTime(bettingInfo.))
                //{
                //    AppGlobal.RenderResult(ApiCode.NotSell);//不存在指定期数或者超过投注时间，不允许投注
                //    return;
                //}
                
                // -2 为可能中奖的金额超过限额
                //
                ApiCode errorType= ApiCode.Success;
                bool isCompled = false;
                if (bettingInfo.CatchDtos == null || bettingInfo.CatchDtos.Count < 1)
                {
                    isCompled = DefaultDetails(user, bettingInfo.BetDetails, ref errorType, -1);//普通投注
                }
                else
                    //追号
                    isCompled = this.CatchBetting(user, bettingInfo, ref errorType, -1);
                if (isCompled)
                {
                    AppGlobal.RenderResult(ApiCode.Success);
                }
                else
                {
                    AppGlobal.RenderResult(errorType);
                }

            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("AddBetDetails", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
                //log
            }
        }

        #region 普通投注

        private bool DefaultDetails(BettUserDto user, List<BetDetailDTO> details, ref ApiCode errorType, int lotteryid, bool ishtml = false)
        {
            int index = 0;
            foreach (var detail in details)
            {
                var radioContent = Ytg.Scheduler.Comm.Bets.RadioContentFactory.CreateInstance(detail.PalyRadioCode);
                var lv_vw = LotterySturctHelper.GetPlayType_Vw(detail.PalyRadioCode);
                if (null == radioContent || lv_vw==null)
                {
                    errorType = ApiCode.Exception;//服务器错误
                }
                this.LotteryId = lv_vw.Id;
                this.LotteryCode = lv_vw.LotteryCode;
                if (ishtml)
                    detail.BetContent = radioContent.HtmlContentFormart(detail.BetContent);
                if (string.IsNullOrEmpty(detail.BetContent))
                {
                    errorType = ApiCode.Fail;//服务器错误 非法请求
                    return false;
                }
                //计算总注数
                int betCount = radioContent.TotalBetCount(new BetDetail()
                {
                    BetContent = detail.BetContent
                });
                if (betCount <= 0)//超过指定注数，不允许投注
                {
                    errorType = ApiCode.Fail;//服务器错误 非法请求
                    return false;
                }
                var fs = Ytg.Service.Lott.BaseDataCatch.GetPalyTypeRadio().Where(radio => radio.RadioCode == detail.PalyRadioCode).FirstOrDefault();
                if (fs == null || (fs.MaxBetCount > 0 && betCount > fs.MaxBetCount))
                {
                    errorType = ApiCode.GoBeyond;
                    return false;
                }

                //计算本注需用金额
                decimal sumMonery = CalculateMonery(betCount, detail.Model, (int)detail.Multiple, lotteryid);
                if (lotteryid == 21)//六合彩
                    detail.Multiple = 1;
                //验证可能中奖的最大金额，若超过该限制，则不允许投注
                if (WinMoneryHelper.GetAutoWinMonery(detail, user, fs) > WinMoneryHelper.GetMaxReboMonery())
                {
                    errorType = ApiCode.NotScope;//超过限额
                    return false;
                }

                var subscription = sumMonery * detail.ByBili;//认购金额
               // subscription = detail.CreaterBuyPieces > subscription ? detail.CreaterBuyPieces : subscription;
                var surplusMonery = sumMonery - subscription;//剩余金额
                var surbili = (subscription/ sumMonery *100);
                var bet = new BetDetail()
                {
                    BetCode = "b" + Utils.BuilderNum(),
                    IsMatch = false,
                    IssueCode = detail.IssueCode,
                    BetContent = detail.BetContent,
                    Model = detail.Model,
                    Multiple = (int)detail.Multiple,
                    PalyRadioCode = detail.PalyRadioCode,
                    PrizeType = detail.PrizeType,
                    UserId = user.Id,
                    TotalAmt = sumMonery,
                    LotteryCode = this.LotteryCode,
                    BetCount = betCount,
                    BackNum = (decimal)(detail.PrizeType == 0 ? user.Rebate : 0),
                    BonusLevel = user.PlayType == 0 ? 1800 : 1700,
                    PostionName = detail.PostionName,
                    HasState = betCount <= detail.MaxBetCount ? 1 : 0,
                    IsBuyTogether=detail.ByType,
                    Subscription= subscription,
                    SurplusMonery= surplusMonery,
                    Bili= (double)surbili,
                    Secrecy= detail.Secrecy
                };

                //if (bet.IsBuyTogether != 0)
                //{
                //    //合买
                //    if (bet.Subscription >= bet.TotalAmt)
                //        bet.GroupByState = 1;
                //    else
                //        bet.GroupByState = 0;
                //}

               /**最新投注方法 提高性能*/
               int state = 0;
                var userAmt = this.mBetDetailService.AddBetting(bet, CreateUserBalanceDetial(user.Id, (bet.IsBuyTogether==0?- sumMonery:-bet.Subscription), TradeType.投注扣款, 0, bet.BetCode), lotteryid, ref state);
                //
                Ytg.Scheduler.Comm.LogManager.Info(bet.ToString()+ " ----" + state);
                if (state == 1)
                {//超过投注时间，不允许投注
                    errorType = ApiCode.Empty;
                    return false;
                }
                else if (state == 2)
                {
                    //用户余额不够
                    errorType = ApiCode.NotEnough;//余额不够本次
                    return false;
                }
                else
                {
                    //提交成功
                }
           
                index++;
            }
            return true;

        }

        #endregion

        #region 追号投注

        /// <summary>
        /// 追号投注
        /// </summary>
        private bool CatchBetting(BettUserDto user, BettingDTO dto, ref ApiCode errorType, int lotteryid, bool ishtml = false)
        {
            var details = dto.BetDetails;
            int index = 0;
            foreach (var detail in details)
            {
                var radioContent = Ytg.Scheduler.Comm.Bets.RadioContentFactory.CreateInstance(detail.PalyRadioCode);
                var lv_vw = LotterySturctHelper.GetPlayType_Vw(detail.PalyRadioCode);
                if (null == radioContent || lv_vw==null)
                {
                    errorType = ApiCode.Exception;//服务器错误
                    return false;
                }
                this.LotteryId = lv_vw.Id;
                this.LotteryCode = lv_vw.LotteryCode;
                if (ishtml)
                    detail.BetContent = radioContent.HtmlContentFormart(detail.BetContent);
                //计算总注数
                int betCount = radioContent.TotalBetCount(new BetDetail()
                {
                    BetContent = detail.BetContent
                });
                if (betCount < 0)//超过指定注数，不允许投注
                {
                    errorType = ApiCode.Fail;//服务器错误
                    return false;
                }
                var fs = Ytg.Service.Lott.BaseDataCatch.GetPalyTypeRadio().Where(radio => radio.RadioCode == detail.PalyRadioCode).FirstOrDefault();
                if (fs == null || (fs.MaxBetCount > 0 && betCount > fs.MaxBetCount))
                {
                    errorType = ApiCode.GoBeyond;
                    return false;
                }

                decimal detailMonery = CalculateMonery(betCount, detail.Model,1, lotteryid);
                if(detailMonery<=0)
                {
                    errorType = ApiCode.Fail;//超过限额
                    return false;
                }
                //追号最大倍数
                int maxMulite = dto.CatchDtos.Max(x => x.Multiple);
                if (maxMulite < 1)
                    maxMulite = 1;
                int oldMulite = (int)detail.Multiple;
                detail.Multiple = maxMulite;
                //验证可能中奖的最大金额，若超过该限制，则不允许投注
                if (WinMoneryHelper.GetAutoWinMonery(detail, user, fs) > WinMoneryHelper.GetMaxReboMonery())
                {
                    errorType = ApiCode.NotScope;//超过限额
                    return false;
                }
                detail.Multiple = 1;//还原初始倍数 始终只有一倍
                //追号
                string catchNumCode = "c" + Utils.BuilderNum();

                var fstCode = string.Empty;
                string issueStr = string.Empty;
                //计算本次追号需用的总金额
                decimal sumMonery = CatchIssue(catchNumCode, detailMonery, dto.CatchDtos, lotteryid, ref fstCode, ref issueStr);
                if (string.IsNullOrEmpty(issueStr) || sumMonery<=0)
                {
                    errorType = ApiCode.Fail;//服务器错误
                    return false;
                }
                var bet = new CatchNum()
                {
                    CatchNumCode = catchNumCode,
                    BetContent = detail.BetContent,
                    Model = detail.Model,
                    PalyRadioCode = detail.PalyRadioCode,
                    PrizeType = detail.PrizeType,
                    BackNum = (decimal)(detail.PrizeType == 0 ? user.Rebate : 0),
                    UserId = user.Id,
                    IsAutoStop = dto.IsAutoStop,
                    Stauts = CatchNumType.Runing,
                    SumAmt = sumMonery,
                    BeginIssueCode = fstCode,
                    CatchIssue = dto.CatchDtos.Count,
                    BonusLevel = user.PlayType == 0 ? 1800 : 1700,
                    LotteryCode = this.LotteryCode,
                    BetCount = betCount,
                    PostionName = detail.PostionName,
                    HasState = betCount <= detail.MaxBetCount ? 1 : 0
                };

                //插入追号记录
                int state = -1;
                this.mSysCatchNumIssueService.AddCatchBetting(bet, CreateUserBalanceDetial(user.Id, -sumMonery, TradeType.追号扣款, 0, bet.CatchNumCode), lotteryid, issueStr, detailMonery, ref state);
                if (state == 1)
                {//超过投注时间，不允许投注
                    errorType = ApiCode.Empty;
                    return false;
                }
                else if (state == 2)
                {
                    //用户余额不够
                    errorType = ApiCode.NotEnough;//余额不够本次
                    return false;
                }
                else
                {
                    //提交成功
                }

                index++;
            }
           
            return true;
        }

        /// <summary>
        /// 插入单条追号
        /// </summary>
        private decimal CatchIssue(string catchNumCode, decimal detailMonery, List<CatchDto> source, int lotteryid, ref  string fsIssueCode, ref string issueStr)
        {

            // bool isFirst = true;
            decimal sumMonery = 0;
            foreach (var s in source)
            {
                CatchNumIssue item = new CatchNumIssue();
                item.CatchNumIssueCode = "i" + Utils.BuilderNum();
                item.CatchNumCode = catchNumCode;
                item.IsMatch = false;
                item.IssueCode = s.IssueCode;
                item.Multiple = s.Multiple<1?1:s.Multiple;
                item.Stauts = BetResultType.NotOpen;
                item.TotalAmt = detailMonery * item.Multiple;
                item.WinMoney = 0;
                issueStr += string.Format("{0}|{1},", s.IssueCode, s.Multiple);
                //保存至数据库
                //this.mSysCatchNumIssueService.AddCatchIssue(item,lotteryid);
                if (string.IsNullOrEmpty(fsIssueCode))
                    fsIssueCode = s.IssueCode;
                sumMonery += item.TotalAmt;
            }
            if (!string.IsNullOrEmpty(issueStr))
            {
                issueStr += "-1|-1";
            }

            return sumMonery;
        }

        #endregion

        /// <summary>
        /// 计算总金额,
        /// </summary>
        /// <param name="betCount">总注数</param>
        /// <param name="mode">模式 元/角/分</param>
        /// <param name="multiple">倍数</param>
        /// <param name="PrizeType">是否返点</param>
        private decimal CalculateMonery(int betCount, int mode, int multiple,int lotteryid)
        {
            if (lotteryid == 21)
                return multiple;
            decimal monery = 2;
            switch (mode)
            {
                case 1://角
                    monery = 0.2M;
                    break;
                case 2://分
                    monery = 0.02M;
                    break;
                case 3:
                    monery = 0.002M;
                    break;
            }
            //计算共投注多少钱 倍数*钱*注数
            return multiple * monery * betCount;
        }


        /// <summary>
        /// 创建消费记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tradeAmt">交易金额</param>
        /// <param name="tp">交易类型</param>
        /// <param name="userAmt">交易前余额</param>
        /// <returns></returns>
        private SysUserBalanceDetail CreateUserBalanceDetial(int? uid, decimal tradeAmt, TradeType tp, decimal userAmt, string relevanceNo)
        {
            //扣除笨注彩票
            return new SysUserBalanceDetail()
            {
                OpUserId = uid,
                SerialNo = "d" + Utils.BuilderNum(),
                Status = 0,
                TradeAmt = tradeAmt,
                TradeType = tp,
                UserAmt = userAmt,
                UserId = uid.Value,
                RelevanceNo = relevanceNo
            };
        }

        #endregion

        #region 获取用户投注返点
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playType">玩家奖金类型 1700/1800</param>
        /// <param name="userBackNum"></param>
        /// <param name="radioCode"></param>
        /// <returns></returns>
        private double GetUserBackNum(UserPlayType playType, double userBackNum, int radioCode)
        {
            var radios = Ytg.Service.Lott.BaseDataCatch.GetPalyTypeRadio(this.mPlayTypeRadioService);
            if (null == radios)
                return -1;
            var fs = radios.Where(c => c.RadioCode == radioCode).FirstOrDefault();
            if (null == fs)
                return -1;
            return playType == UserPlayType.P1800 ? fs.MaxRebate - userBackNum : fs.MaxRebate1700 - userBackNum;
        }
        #endregion
        #region 游戏记录，投注记录
        private void SelectBetList()
        {
           
            int pageIndex;
            int pageSize;
            int totalCount = 0;
            bool ispageIndex = int.TryParse(Request.Params["pageIndex"], out pageIndex);
            if (!ispageIndex) pageIndex = 1;
            bool ispageSize = int.TryParse(Request.Params["pageSize"], out pageSize);
            if (!ispageSize) pageSize = 15;
            
            try
            {
                 var palyRadioCode=-1;
                 if (!string.IsNullOrEmpty(Request.Params["palyRadioCode"]))
                     palyRadioCode = Convert.ToInt32(Request.Params["palyRadioCode"]);

                var search = new BetListDTO
                   {
                       lotteryCode = Request.Params["SellotteryCode"],
                       palyRadioCode = palyRadioCode,
                       issueCode = Request.Params["issueCode"],
                       betCode = Request.Params["betCode"],
                       userAccount = Request.Params["userAccount"],
                      
                   };
                if (!string.IsNullOrEmpty(Request.Params["startTime"]))
                    search.startTime = Convert.ToDateTime(Request.Params["startTime"]);
                if (!string.IsNullOrEmpty(Request.Params["endTime"]))
                    search.endTime = Convert.ToDateTime(Request.Params["endTime"]);
                if (!string.IsNullOrEmpty(Request.Params["status"]))
                    search.status = Convert.ToInt32(Request.Params["status"]);
                else
                    search.status = -1;

                if (!string.IsNullOrEmpty(Request.Params["tradeType"]))
                    search.tradeType = Convert.ToInt32(Request.Params["tradeType"]);
                else
                    search.tradeType = -1;

                if (!string.IsNullOrEmpty(Request.Params["model"]))
                    search.model = Convert.ToInt32(Request.Params["model"]);
                else
                    search.model = -1;

                if (!string.IsNullOrEmpty(Request.Params["userType"]))
                    search.userType = Convert.ToInt32(Request.Params["userType"]);
                else
                    search.userType = -1;

                var result = this.mBetDetailService.GetBetListBy(search.startTime, search.endTime, search.status,
                    search.tradeType, search.lotteryCode, search.palyRadioCode, search.issueCode, search.model,
                    search.betCode, search.userAccount, search.userType, this.LoginUserId, pageIndex, pageSize, ref totalCount);
                AppGlobal.RenderResult<List<BetList>>(ApiCode.Success, result, "", pageIndex, totalCount);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("SelectBetList", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }
        #endregion

        #region 追号记录

      

        /// <summary>
        /// 追号记录查询
        /// </summary>
        private void SelectCatchNumList()
        {
            string jsonDataStr = Request.Params["data"];
            if (string.IsNullOrEmpty(jsonDataStr))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            FilterCatchNumListParamerDTO parameter = null;
            try
            {
                parameter = Newtonsoft.Json.JsonConvert.DeserializeObject<FilterCatchNumListParamerDTO>(jsonDataStr);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("SelectCatchNumList", ex);
            }
            if (parameter == null)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            int pageIndex;
            int totalCount = 0;
            bool ispageIndex = int.TryParse(Request.Params["pageIndex"], out pageIndex);
            if (!ispageIndex) pageIndex = 1;

            var source = this.mSysCatchNumService.FilterCatchNumList(this.LoginUserId, parameter, pageIndex, ref totalCount);

            AppGlobal.RenderResult<List<CatchNumJsonDTO>>(ApiCode.Success, source, "", pageIndex, totalCount);
        }

        #endregion

        #region 获取当前用户未开奖的用户投注记录 包括追号
        private void GetNotOpenBetList()
        {
            string lotterycode = Request.Params["lotterycode"];//彩种id

            //var source = this.mBetDetailService.GetNotOpenBetDetail(this.LoginUserId, lotteryId);
            //AppGlobal.RenderResult<List<NotOpenBetDetailDTO>>(ApiCode.Success, source);

            int pageIndex = 1;
            int pageSize = 10;
            int totalCount = 0;

            try
            {
                var palyRadioCode = -1;
                var search = new BetListDTO
                   {
                       startTime = DateTime.Now.AddDays(-1),
                       endTime = DateTime.Now,
                       status = -1,//Convert.ToInt32(Request.Params["status"]),
                       tradeType = 2,//Convert.ToInt32(Request.Params["tradeType"]),
                       lotteryCode = lotterycode,// Request.Params["SellotteryCode"],
                       palyRadioCode = palyRadioCode,
                       issueCode = "",// Request.Params["issueCode"],
                       model = -1,//Convert.ToInt32(Request.Params["model"]),
                       betCode = "", //Request.Params["betCode"],
                       userAccount = this.LoginUser.Code,//Request.Params["userAccount"],
                       userType = -1//, Convert.ToInt32(Request.Params["userType"])
                   };


                var result = this.mBetDetailService.GetBetListBy(search.startTime, search.endTime, search.status,
                    search.tradeType, search.lotteryCode, search.palyRadioCode, search.issueCode, search.model,
                    search.betCode, search.userAccount, search.userType, this.LoginUserId, pageIndex, pageSize, ref totalCount);
                AppGlobal.RenderResult<List<BetList>>(ApiCode.Success, result, "", pageIndex, totalCount);
            }
            catch (Exception ex) { }
        }
        #endregion

        #region 根据追号编号获取追号详细

        private void GetCatchIssue()
        {
            string catchCode = Request.Params["catchCode"];
            if (string.IsNullOrEmpty(catchCode))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var source = this.mSysCatchNumIssueService.GetCatchIssue(catchCode);
            AppGlobal.RenderResult<List<CatchNumIssue>>(ApiCode.Success, source);
        }

        #endregion

        #region 终止追号

        /// <summary>
        /// 终止追号
        /// </summary>
        private void CannelCatchNum()
        {
            string catchNum = Request.Params["catchCode"];//追号编号
            string data = Request.Params["data"];//撤单详细信息
            if (string.IsNullOrEmpty(catchNum) || string.IsNullOrEmpty(data))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            //验证当前单号是否正在进行
            var fs = this.mSysCatchNumService.Where(item => item.CatchNumCode == catchNum && item.UserId == this.LoginUserId && item.Stauts == CatchNumType.Runing).FirstOrDefault();
            if (fs == null)
            {
                //非法撤单
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            //获取注单详情
            var source = this.mSysCatchNumIssueService.GetCatchIssue(catchNum);
            var cannelArray = data.Split(',');
            if (source == null || source.Count < 1 || cannelArray.Length < 1)
            {
                AppGlobal.RenderResult(ApiCode.ValidationFails);
                return;
            }

            //获取用户余额
            //var userbalance = this.mSysUserBalanceService.GetUserBalance(this.LoginUserId);

            //int cannelCount = 0;//当前取消期数
            //decimal cannelmonery = 0; //当前取消金额
            int noCannelCount = 0;
            foreach (var item in source)
            {
               
                if (item.Stauts == BetResultType.NotOpen)
                {
                    
                    //未开奖的状态才允许撤单
                    if (cannelArray.Where(s => s == item.IssueCode).Count() > 0)
                    {
                        if (!VdaActionEndTime(item.IssueCode))
                        {
                            AppGlobal.RenderResult(ApiCode.ValidationFails);
                            return;
                        }

                        fs.UserCannelIssue++;
                        fs.UserCannelMonery += item.TotalAmt;
                        item.Stauts = BetResultType.Cancel; //对本注进行撤单
                        // if (fs.PrizeType == 1)
                        //撤单的进行返点回收
                        this.mRebateHelper.BettingCannelIssue(this.LoginUserId, item.TotalAmt, item.CatchNumIssueCode, mRebateHelper.GetRadioMaxRemo(fs.PalyRadioCode, fs.BonusLevel));
                        // this.mRebateHelper.UpdateUserBanance(this.LoginUserId, item.TotalAmt, TradeType.撤单返款, item.CatchNumIssueCode, 0);

                        //cannelCount++;
                        //cannelmonery += item.TotalAmt;
                    }
                }
                if(item.Stauts== BetResultType.NotOpen)
                    noCannelCount++;
            }
            //撤单
            this.mSysCatchNumIssueService.Save();
            if (noCannelCount<=0) //全部撤销
                this.mSysCatchNumService.CannelCatch(catchNum);
            //fs.UserCannelIssue = fs.UserCannelIssue + cannelCount;
            //fs.UserCannelMonery = fs.UserCannelMonery + cannelmonery;
           // this.mSysUserBalanceService.Save();
            mSysCatchNumService.Save();
            //刷新数据
            GetCatchIssue();
        }

        #endregion
        
        #region 投注撤单

        /// <summary>
        /// 撤销所有追号
        /// </summary>
        private void CannelAllCatch(string code)
        {
            string catchNum = code;//追号编号
           
            if (string.IsNullOrEmpty(catchNum))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            //验证当前单号是否正在进行
            var fs = this.mSysCatchNumService.Where(item => item.CatchNumCode == catchNum && item.UserId == this.LoginUserId && item.Stauts == CatchNumType.Runing).FirstOrDefault();
            if (fs == null)
            {
                //非法撤单
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            //获取注单详情
            var source = this.mSysCatchNumIssueService.GetCatchIssue(catchNum);
            //获取用户余额
            //var userbalance = this.mSysUserBalanceService.GetUserBalance(this.LoginUserId);

            if (source == null || source.Count < 1)
            {
                AppGlobal.RenderResult(ApiCode.ValidationFails);
                return;
            }

            //int cannelCount = 0;//当前取消期数
           // decimal cannelmonery = 0; //当前取消金额
            foreach (var item in source)
            {
                if (item.Stauts == BetResultType.NotOpen)
                {
                    //未开奖的状态才允许撤单
                    fs.UserCannelIssue++;
                    fs.UserCannelMonery += item.TotalAmt;
                    item.Stauts = BetResultType.Cancel; //对本注进行撤单
                    // if (fs.PrizeType == 1)
                    //撤单的进行返点回收
                    this.mRebateHelper.BettingCannelIssue(this.LoginUserId, item.TotalAmt, item.CatchNumIssueCode, mRebateHelper.GetRadioMaxRemo(fs.PalyRadioCode, fs.BonusLevel));
                    //this.mRebateHelper.UpdateUserBanance(this.LoginUserId, item.TotalAmt, TradeType.撤单返款, item.CatchNumIssueCode, 0);

                    //cannelCount++;
                    //cannelmonery += item.TotalAmt;
                }
            }
            //撤单
            this.mSysCatchNumIssueService.Save();
            this.mSysCatchNumService.CannelCatch(catchNum);
            AppGlobal.RenderResult(ApiCode.Success);
            //fs.UserCannelIssue = fs.UserCannelIssue + cannelCount;
            //fs.UserCannelMonery = fs.UserCannelMonery + cannelmonery;
            //this.mSysUserBalanceService.Save();
            this.mSysCatchNumService.Save();
        }

        private void cannelBettNum()
        {
            string betcode = Request.Params["bettCode"];//投注编号
            string betType = Request.Params["betType"];//类型
            
            if (string.IsNullOrEmpty(betcode))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            if (betType == "1") {
                //追号撤单
                CannelAllCatch(betcode);
                return;
            }
            //验证请求参数是否正确
            var fs = this.mBetDetailService.Where(item => item.BetCode == betcode && item.Stauts == BetResultType.NotOpen && item.UserId==this.LoginUserId).FirstOrDefault();
            if (null == fs || fs.Stauts!= BetResultType.NotOpen)
            {
                AppGlobal.RenderResult(ApiCode.ValidationFails);
                return;
            }
            if (!VdaActionEndTime(fs.IssueCode))
            {
                AppGlobal.RenderResult(ApiCode.ValidationFails);
                return;
            }
          
            //撤单处理
            fs.Stauts = BetResultType.Cancel;
            this.mBetDetailService.Save();
            //处理撤单返点 
            this.mRebateHelper.BettingCannelIssue(this.LoginUserId, fs.TotalAmt, fs.BetCode, mRebateHelper.GetRadioMaxRemo(fs.PalyRadioCode, fs.BonusLevel));
            AppGlobal.RenderResult(ApiCode.Success);
        }
        #endregion

        #region 验证操作时间是否超过期数的结束销售时间
        /// <summary>
        /// 验证操作时间是否超过期数的结束销售时间
        /// </summary>
        /// <param name="issueCode">期数</param>
        /// <returns>false 验证失败</returns>
        private bool VdaActionEndTime(string issueCode)
        {
            
            //结束销售后，不允许撤单
            var curIssue = this.mLotteryIssueService.Where(c => c.IssueCode == issueCode && (c.LotteryId == this.LotteryId )).FirstOrDefault();
            if (null == curIssue || DateTime.Now >= curIssue.EndSaleTime.Value.AddSeconds(-1))
                return false;

            return true;
        }
        #endregion

        #region 根据投注编号获取投注信息
        private void GetBetDetail()
        {
            string betCode = Request.Params["betCode"];//投注编号
            if (string.IsNullOrEmpty(betCode))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var item = this.mBetDetailService.GetBetDetailForBetCode(betCode);
            AppGlobal.RenderResult<BetList>(ApiCode.Success, item);
        }
        #endregion

        #region 根据追号单和追号期数获取追号期数信息

        private void GetCatchInfo()
        {
            string catchCode = Request.Params["catchcode"];//追号单
            string issueCode = Request.Params["issuecode"];//追号期数
            if (string.IsNullOrEmpty(catchCode) || string.IsNullOrEmpty(issueCode))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var item = this.mSysCatchNumIssueService.GetCatchIssueDetail(catchCode, issueCode);
            AppGlobal.RenderResult<BetList>(ApiCode.Success, item);
        }

        #endregion

        #endregion


    }
}