using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Scheduler.Comm.Bets.AutoLogic;

namespace Ytg.Scheduler.Comm.Bets
{
    /// <summary>
    /// 分分彩
    /// </summary>
    public class FenBetDetailsCalculate : BetDetailsCalculate_Auto
    {
        private readonly string[] _11xwType =  { "sf11x5", "wf11x5" };//11选5Type

        private readonly Random random = new Random();

        /// <summary>
        /// 生成开奖数字
        /// </summary>
        /// <param name="lotteryType"></param>
        private string RandomResult(string lotteryType)
        {
            string result = string.Empty;
            if (_11xwType.Contains(lotteryType))
            {
                //11选5生成号码   生成的5位开奖结果必须唯一
                List<string> builArray = new List<string>();
                while (true)
                {
                    string randomNum = random.Next(1, 12).ToString("d2");
                    if (builArray.Contains(randomNum))
                        continue;
                    builArray.Add(randomNum);
                    if (builArray.Count == 5)
                        break;
                }
                result = string.Join(",", builArray);
            }
            else { 
                //时时彩号码
                string randomVaue = "";
                while (true)
                {
                    randomVaue = random.Next(00000, 99999).ToString();
                    if (randomVaue.Length != 5)
                        continue;
                    result = string.Join(",", randomVaue.Select(c => c.ToString()));
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 分分彩必须保证利润,目前的做法是随机五个号码，计算中奖金额然后保证利润后才开指定数字
        /// </summary>
        /// <param name="lotteryType"></param>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        public override string Calculate(string lotteryType, string issueCode, string openResult)
        {
            //获取本期所有投注数据，进行计算
            var source = this.mBetDetailService.GetIssuesBetDetail(lotteryType, issueCode);
            //获取追号数据，进行计算
            var zhuiHaoSource = this.mSysCatchNumService.GetNotCompledCatchNumList(lotteryType, issueCode);
            if ((source == null || source.Count < 1) && (zhuiHaoSource == null || zhuiHaoSource.Count < 1))
            {
                //没有投注，随机开
                openResult= RandomResult(lotteryType);
                LogManager.Info(string.Format("自主开奖=====没有投注和追号项，随机开奖={0}",openResult));
                return openResult;
            }
            int sourceCount=source.Count+zhuiHaoSource.Count;
            bool isMaxBaseAmt=false;//投注金额是否大于奖金
             decimal[] models = { 1M, 0.1M, 0.01M,0.001M };
            //计算本期最低保证利润
            var sum = source.Sum(c => c.TotalAmt) + zhuiHaoSource.Sum(c => c.TotalAmt);
           
            var total = sum * (1 - ConfigHelper.ProfitMargin) +OpendHistoryLogic.CreateInstance().GetWinMonery(lotteryType) ;//当期可以释放的金额
            LogManager.Error(lotteryType + "利润空间：" + total);
            List<string> openHistory = new List<string>();//开奖历史记录，开过的不冲去计算

            string bettMsg = "";
            /***检测代码，正式上线注释**/
            Stopwatch stopwatch = new Stopwatch();//计时
            stopwatch.Start(); //  开始监视代码运行时间 
            /***检测代码，正式上线注释**/

            int whileCount = 0;
            while (true)
            {
                string randomOpen = RandomResult(lotteryType);
                if (openHistory.Contains(randomOpen))
                    continue;
                openHistory.Add(randomOpen);

                //投注中奖金额
                var bettingWinTotal = 0m;
                openResult = randomOpen; //string.Join(",", randomOpen.Select(c => c.ToString()));

                //循环进行计算普通投注中奖金额
                foreach (var item in source)
                {
                    int code = item.PalyRadioCode;
                    item.WinMoney = 0;
                    item.IsMatch = false;
                    Ytg.Scheduler.Comm.Bets.ICalculate calculate = Ytg.Scheduler.Comm.Bets.RadioContentFactory.CreateInstance(code);
                    calculate.Calculate(issueCode, openResult, item);
                    item.OpenResult = openResult;
                    if (item.IsMatch)
                    {
                        item.Stauts = BasicModel.BetResultType.Winning;
                        bettingWinTotal += item.WinMoney;
                    }
                    else
                    {
                        item.Stauts = BasicModel.BetResultType.NotWinning;
                    }
                    isMaxBaseAmt = item.TotalAmt >= calculate.GetBaseAmt(item) * models[item.Model];
                }
                //追号
                foreach (var item in zhuiHaoSource)
                {
                    var betDetail = new BetDetail()
                    {
                        BackNum = item.BackNum,
                        BetContent = item.BetContent,
                        BetCount = item.BetCount,
                        BonusLevel = item.BonusLevel,
                        IsMatch = item.IsMatch,
                        IssueCode = item.IssueCode,
                        Model = item.Model,
                        Multiple = item.Multiple,
                        OpenResult = item.OpenResult,
                        PalyRadioCode = item.PalyRadioCode,
                        PrizeType = item.PrizeType,
                        TotalAmt = item.TotalAmt,
                        UserId = item.UserId,
                        WinMoney = item.WinMoney,
                    };
                    ICalculate calculate = RadioContentFactory.CreateInstance(betDetail.PalyRadioCode);
                    calculate.Calculate(issueCode, openResult, betDetail);
                    item.OpenResult = openResult;
                    item.WinMoney = betDetail.WinMoney;
                    if (betDetail.IsMatch)
                    {
                        bettingWinTotal += betDetail.WinMoney;
                    }
                    isMaxBaseAmt = item.TotalAmt >= calculate.GetBaseAmt(betDetail);
                }

                LogManager.Error(string.Format("尝试自主开奖 issueCode:{0} bettingWinTotal:{1} total:{2} isMaxBaseAmt:{3} sourceCount:{4} whileCount:{5} lotteryType:{6}", issueCode, bettingWinTotal, total, isMaxBaseAmt, source, whileCount, lotteryType));
                whileCount++;

                if (
                    (total - bettingWinTotal > 0) ||
                    (isMaxBaseAmt && sourceCount < 1)
                    || (whileCount > 50 && sum >= bettingWinTotal))//符合指定利润，开奖
                {//=开奖信息=返钱：33218.00000000RMB  投注总额：4250.0000RMB  最低40%利润：-21768.00000000RMB  
                    bettMsg = "返钱：" + bettingWinTotal + "RMB  投注总额：" + sum + "RMB  最低40%利润：" + (total - bettingWinTotal) + "RMB  wileCount" + whileCount + " " + lotteryType + " \t\n";
                    //修改期数
                    //处理开奖逻辑 普通投注
                    CalculateBetting(source, issueCode, openResult);
                    //追号
                    CalculateCatchNums(zhuiHaoSource, issueCode, openResult);
                    OpendHistoryLogic.CreateInstance().PutLotteryData(lotteryType, sum, bettingWinTotal);
                    LogManager.Error(string.Format("自主开奖成功，开奖结果={0}=========开奖信息={1}", openResult, bettMsg));
                    break;
                }
                if (whileCount > 50)
                    sum += 500;
            }
            /***检测代码，正式上线注释**/
            TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间            
            double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数
            stopwatch.Stop();
            /***检测代码，正式上线注释**/

            //bettMsg += "分分彩：" + lotteryType + " 期数：" + issueCode + " 总毫秒数：" + milliseconds;
          //  LogManager.Error(bettMsg);

            return openResult;
        }



        #region 普通投注

        /// <summary>
        /// 计算投注结果
        /// </summary>
        /// <param name="issueCode">当前期号</param>
        /// <param name="openResult">开奖结果</param>
        private void CalculateBetting(List<BetDetail> source, string issueCode, string openResult)
        {
            //循环进行计算
            foreach (var item in source)
            {
                try
                {
                    int code = item.PalyRadioCode;
                    item.WinMoney = 0;
                    item.IsMatch = false;
                    Ytg.Scheduler.Comm.Bets.ICalculate calculate = Ytg.Scheduler.Comm.Bets.RadioContentFactory.CreateInstance(code);
                    calculate.Calculate(issueCode, openResult, item);
                    item.OpenResult = openResult;

                    if (item.IsMatch)
                    {
                        //记录，更新用户余额
                        this.mRebateHelper.UpdateUserBanance(item.UserId, item.WinMoney, TradeType.奖金派送, item.BetCode, 0);

                        //插入中奖消息
                        mMessageService.Create(CreateMsg(item.BetCode, item.WinMoney, item.UserId, 0));
                        this.mMessageService.Save();
                    }
                    else
                    {
                        item.Stauts = BasicModel.BetResultType.NotWinning;
                    }

                    //计算返点 
                    //if (item.PrizeType == 1)
                    //{
                        this.mRebateHelper.BettingCalculate(item.PrizeType,item.UserId, item.TotalAmt, item.BetCode, mRebateHelper.GetRadioMaxRemo(item.PalyRadioCode, item.BonusLevel));
                    //}

                    //保存状态
                    this.mBetDetailService.UpdateOpenState(item);
                }
                catch (Exception ex)
                {
                    LogManager.Error("分分彩，普通投注" + ex.Message);
                }
            }

         //   this.mBetDetailService.Save();
        }

        #endregion


        #region 追号
        /// <summary>
        /// 计算投注结果
        /// </summary>
        /// <param name="issueCode">当前开奖期号</param>
        /// <param name="openResult">当前开奖结果</param>
        private void CalculateCatchNums(List<NotCompledCatchNumListDTO> source,string issueCode,string openResult)
        {
            //循环进行计算
            foreach (var item in source)
            {
                try
                {
                    item.WinMoney = 0;
                    var betDetail = new BetDetail()
                    {
                        BackNum = item.BackNum,
                        BetContent = item.BetContent,
                        BetCount = item.BetCount,
                        BonusLevel = item.BonusLevel,
                        IsMatch = item.IsMatch,
                        IssueCode = item.IssueCode,
                        Model = item.Model,
                        Multiple = item.Multiple,
                        OpenResult = item.OpenResult,
                        PalyRadioCode = item.PalyRadioCode,
                        PrizeType = item.PrizeType,
                        TotalAmt = item.TotalAmt,
                        UserId = item.UserId,
                       // WinMoney = item.WinMoney,
                    };
                    ICalculate calculate = RadioContentFactory.CreateInstance(betDetail.PalyRadioCode);
                    calculate.Calculate(issueCode, openResult, betDetail);
                    item.OpenResult = openResult;
                    item.WinMoney = betDetail.WinMoney;
                    item.IsMatch = betDetail.IsMatch;

                    //获取追号信息
                    var catchItem = this.mSysCatchNumService.Get(item.Id);
                    // //获取追号当期信息
                    var catchNumItem = this.mSysCatchNumIssueService.Get(item.CuiId);
                    if (item.IsMatch)
                    {
                        catchNumItem.Stauts = BasicModel.BetResultType.Winning;
                        catchNumItem.WinMoney = item.WinMoney;
                        catchItem.WinIssue += 1;

                        this.mRebateHelper.UpdateUserBanance(item.UserId, item.WinMoney, TradeType.奖金派送, catchNumItem.CatchNumIssueCode, 0);

                        string content = string.Format("\t编号为【{0}】的方案已中奖 <span style=\"color:red;\">{1}</span> 元,请注意查看您的帐变信息，如有任何疑问请联系在线客服。\t\n", item.CatchNumCode, string.Format("{0:N}", catchNumItem.WinMoney));
                        this.mMessageService.Create(CreateMsg(content, 0, item.UserId, 1));
                        this.mMessageService.Save();
                    }
                    else
                    {
                        catchNumItem.Stauts = BasicModel.BetResultType.NotWinning;
                    }

                    //计算返点 游戏返点
                   //if (item.PrizeType == 1)
                    this.mRebateHelper.BettingCalculate(item.PrizeType,item.UserId, item.TotalAmt, catchNumItem.CatchNumIssueCode, mRebateHelper.GetRadioMaxRemo(item.PalyRadioCode, item.BonusLevel));
                    //
                    //catchNumItem.OccDate = DateTime.Now;
                    catchNumItem.IsMatch = item.IsMatch;
                    catchNumItem.OpenResult = item.OpenResult;

                    
                    if (item.IsMatch && item.IsAutoStop)//中奖，并且设置为中奖后自动结束
                        catchItem.Stauts = BasicModel.CatchNumType.Compled;


                    catchItem.WinMoney = catchItem.WinMoney + item.WinMoney;
                    catchItem.CompledIssue = catchItem.CompledIssue + 1;
                    catchItem.CompledMonery = catchItem.CompledMonery + catchNumItem.TotalAmt;

                    //修改信息
                    this.mSysCatchNumIssueService.Save();
                    //修改未开奖追号期数occdate时间
                    this.mSysCatchNumIssueService.UpdateNoOpenOccDateTime(item.CatchNumCode);


                    if (catchItem.Stauts == CatchNumType.Compled)
                    {
                        //结束本次追号，对未完成追号的撤单
                        var exitNums = this.mSysCatchNumIssueService.GetLastCatchNum(item.CatchNumCode, catchNumItem.IssueCode);
                        if (null == exitNums)
                            return;

                        int exitCount = exitNums.Count;
                        catchItem.SysCannelIssue = exitCount;//保存中奖后结束期数
                        //this.mSysCatchNumService.Save();

                        for (var i = 0; i < exitCount; i++)
                        {
                            var exit = exitNums[i];
                            exit.Stauts = BetResultType.SysCancel;
                            catchItem.UserCannelMonery += exit.TotalAmt;
                            this.mRebateHelper.UpdateUserBanance(item.UserId, exit.TotalAmt, TradeType.追号返款, exit.CatchNumIssueCode, 0);
                        }
                        
                    }
                    else if ((catchItem.CompledIssue + catchItem.SysCannelIssue + catchItem.UserCannelIssue) >= catchItem.CatchIssue)
                    {
                        //验证是否有剩余期数，若无剩余期数，本次追号完成
                        catchItem.Stauts = CatchNumType.Compled;
                       
                    }
                    mSysCatchNumIssueService.Save();//保存状态
                    this.mSysCatchNumService.Save();    
                    
                }
                catch (Exception ex)
                {
                    LogManager.Error("分分彩异常" + ex.Message);
                }

            }

        }


        #endregion

    }
}
