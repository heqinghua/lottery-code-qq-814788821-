using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Comm;
using Ytg.Data;
using Ytg.Scheduler.Comm.Bets.Calculate;
using Ytg.Service;
using Ytg.Service.Lott;

namespace Ytg.Scheduler.Comm.Bets
{
    /// <summary>
    /// 投注明细 用于自助开奖
    /// </summary>
    public class BetDetailsCalculate_Auto
    {
        protected readonly BetDetailService mBetDetailService;

        protected readonly SysCatchNumService mSysCatchNumService;//追号

        protected readonly SysCatchNumIssueService mSysCatchNumIssueService;//追号详情

        protected readonly SysUserBalanceDetailService mSysUserBalanceDetailService;//账变详情

        protected readonly SysUserBalanceService mSysUserBalanceService;//账号余额表

        protected readonly Ytg.Service.Logic.RebateHelper mRebateHelper;//返点辅助类

        protected readonly MessageService mMessageService;//消息处理类
        public BetDetailsCalculate_Auto()
        {
            IDbContextFactory factory = new DbContextFactory();
            this.mBetDetailService = new BetDetailService(new Repo<BetDetail>(factory));
            this.mSysCatchNumService = new SysCatchNumService(new Repo<CatchNum>(factory));
            this.mSysCatchNumIssueService = new SysCatchNumIssueService(new Repo<CatchNumIssue>(factory));

            Ytg.Comm.IHasher hasher = new Ytg.Comm.Hasher();

            var sysUser = new SysUserService(new Repo<SysUser>(factory), hasher);
            mMessageService = new MessageService(new Repo<Message>(factory));

            this.mSysUserBalanceService = new SysUserBalanceService(new Repo<SysUserBalance>(factory), hasher, sysUser);
            this.mSysUserBalanceDetailService = new SysUserBalanceDetailService(new Repo<SysUserBalanceDetail>(factory), this.mSysUserBalanceService);

            mRebateHelper = new Service.Logic.RebateHelper(sysUser, this.mSysUserBalanceService, this.mSysUserBalanceDetailService);

        }


        /// <summary>
        /// 开始计算本期所有投注结果
        /// </summary>
        /// <param name="issueCode">期号</param>
        /// <param name="openResult">开奖结果</param>
        public virtual string Calculate(string lotteryType, string issueCode, string openResult)
        {
            //计算投注
            this.CalculateBetting(lotteryType, issueCode, openResult);
            //计算追号
            this.CalculateCatchNums(lotteryType, issueCode, openResult);

            return "";
        }

        #region 普通投注

        /// <summary>
        /// 计算投注结果
        /// </summary>
        /// <param name="issueCode">当前期号</param>
        /// <param name="openResult">开奖结果</param>
        private void CalculateBetting(string lotteryCode, string issueCode, string openResult)
        {
            //获取本期所有投注数据，进行计算
            var source = this.mBetDetailService.GetIssuesBetDetail(lotteryCode, issueCode);
            if (null == source)
                return;

            //循环进行计算
            foreach (var item in source)
            {
                int code = item.PalyRadioCode;
                try
                {
                    ICalculate calculate = RadioContentFactory.CreateInstance(code);
                    LogManager.Info(string.Format("创建ICalculate code={0}; 对象为{1}", code, calculate.GetType().ToString()));
                    calculate.Calculate(issueCode, ManagerOpenResult(openResult), item);
                    item.OpenResult = openResult;
                    if (item.IsMatch)
                    {
                        item.Stauts = BasicModel.BetResultType.Winning;
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
                    this.mRebateHelper.BettingCalculate(item.PrizeType, item.UserId, item.TotalAmt, item.BetCode, mRebateHelper.GetRadioMaxRemo(item.PalyRadioCode, item.BonusLevel));
                    //}
                }
                catch (Exception ex)
                {
                    LogManager.Error(string.Format("第{0}期，投注明细项id为{1} 计算过程中发生异常；Exception={2}", issueCode, item.Id, ex.Message));
                }
            }
            this.mBetDetailService.Save();
        }

        #endregion

        #region 追号
        /// <summary>
        /// 计算投注结果
        /// </summary>
        /// <param name="issueCode">当前开奖期号</param>
        /// <param name="openResult">当前开奖结果</param>
        private void CalculateCatchNums(string lotteryCode, string issueCode, string openResult)
        {
            //获取所有未完成的追号记录
            var source = this.mSysCatchNumService.GetNotCompledCatchNumList(lotteryCode, issueCode);
            if (null == source)
                return;
            //循环进行计算
            foreach (var item in source)
            {
                int code = item.PalyRadioCode;
                try
                {
                    ICalculate calculate = RadioContentFactory.CreateInstance(code);

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
                    calculate.Calculate(issueCode, openResult, betDetail);

                    //获取追号当期信息
                    var catchNumItem = this.mSysCatchNumIssueService.Get(item.CuiId);
                    if (betDetail.IsMatch)
                    {
                        catchNumItem.Stauts = BasicModel.BetResultType.Winning;
                        catchNumItem.WinMoney = betDetail.WinMoney;

                        string content = string.Format("\t恭喜您，编号为【{0}】的追号方案已中奖,期号为{1},中奖金额为 <span style=\"color:red;\">{1}</span> 元,请注意查看您的帐变信息，如果有任何疑问请联系在线客服。\t\n", item.CatchNumCode, issueCode, catchNumItem.WinMoney);
                        this.mMessageService.Create(CreateMsg(content, 0, item.UserId, 1));
                        this.mMessageService.Save();
                    }
                    else
                    {
                        catchNumItem.Stauts = BasicModel.BetResultType.NotWinning;
                    }

                    //计算返点 游戏返点
                    //if ( == 1)
                    this.mRebateHelper.BettingCalculate(item.PrizeType, item.UserId, item.TotalAmt, catchNumItem.CatchNumIssueCode, mRebateHelper.GetRadioMaxRemo(item.PalyRadioCode, item.BonusLevel));
                    //
                    catchNumItem.IsMatch = betDetail.IsMatch;
                    catchNumItem.OpenResult = openResult;

                    //获取追号信息
                    var catchItem = this.mSysCatchNumService.Get(item.Id);
                    if (betDetail.IsMatch && item.IsAutoStop)//中奖，并且设置为中奖后自动结束
                        catchItem.Stauts = BasicModel.CatchNumType.Compled;


                    catchItem.WinMoney = catchItem.WinMoney + betDetail.WinMoney;
                    catchItem.CompledIssue = catchItem.CompledIssue + 1;
                    catchItem.CompledMonery = catchItem.CompledMonery + catchNumItem.TotalAmt;

                    //修改信息
                    this.mSysCatchNumIssueService.Save();
                    this.mSysCatchNumService.Save();

                    if (catchItem.Stauts == CatchNumType.Compled)
                    {
                        //结束本次追号，对未完成追号的撤单
                        var exitNums = this.mSysCatchNumIssueService.GetLastCatchNum(item.CatchNumCode, catchNumItem.IssueCode);
                        if (null == exitNums)
                            return;
                        //用户余额

                        int exitCount = exitNums.Count;
                        catchItem.SysCannelIssue = exitCount;//保存中奖后结束期数
                        this.mSysCatchNumService.Save();

                        for (var i = 0; i < exitCount; i++)
                        {
                            var exit = exitNums[i];
                            if (catchItem.PrizeType == 1)
                                mRebateHelper.BettingCannelIssue(item.UserId, exit.TotalAmt, exit.CatchNumIssueCode, mRebateHelper.GetRadioMaxRemo(item.PalyRadioCode, item.BonusLevel));//处理返点
                            else
                                this.mRebateHelper.UpdateUserBanance(item.UserId, item.TotalAmt, TradeType.追号返款, exit.CatchNumIssueCode, 0);
                        }
                    }


                }
                catch (Exception ex)
                {
                    LogManager.Error(string.Format("第{0}期，追号项id为{1} 计算过程中发生异常；Exception={2}", issueCode, item.Id, ex.Message));
                }
            }

        }


        #endregion

        #region  创建中奖消息
        /// <summary>
        /// 创建消息
        /// </summary>
        /// <param name="betCode"></param>
        /// <param name="winMonery"></param>
        /// <param name="touserid"></param>
        /// <param name="betType">0 为投注，1为追号</param>
        /// <returns></returns>
        protected Message CreateMsg(string betCode, decimal winMonery, int touserid, int betType)
        {
            string title = string.Format("恭喜您，编号为【{0}】的方案已中奖。", betCode);
            string content = "";
            if (betType == 0)
                content += string.Format("\t编号为【{0}】的方案已中奖 <span style=\"color:red;\">{1}</span> 元,请注意查看您的帐变信息，如有任何疑问请联系在线客服。\t\n", betCode, Math.Round(winMonery, 2));
            else
                content += betCode;
         

            // content += "感谢您的支持，祝您游戏愉快！\t\t\t\t\t\t\t\t\t\n-----信誉保证，用心服务";
            return new Message()
            {
                FormUserId = -1,
                MessageType = 4,
                Status = 0,
                ToUserId = touserid,
                MessageContent = content,
                Title = title
            };
        }
        #endregion

        #region 福彩3d 上海时时乐 开奖号码特殊处理

        private string ManagerOpenResult(string result)
        {
            if (result.Split(',').Length == 3)
            {
                //福彩3d,上海时时乐
                return "-1,-1," + result;
            }
            return result;
        }
        #endregion

    }
}
