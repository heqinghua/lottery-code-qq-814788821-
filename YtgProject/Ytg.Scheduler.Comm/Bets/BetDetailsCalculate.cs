using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Data;
using Ytg.Scheduler.Comm.Bets.Calculate;
using Ytg.Service;
using Ytg.Service.Lott;

namespace Ytg.Scheduler.Comm.Bets
{
    /// <summary>
    /// 投注明细
    /// </summary>
    public class BetDetailsCalculate
    {
      
     


       public BetDetailsCalculate()
        {
        }

        
        /// <summary>
        /// 开始计算本期所有投注结果
        /// </summary>
        /// <param name="issueCode">期号</param>
        /// <param name="openResult">开奖结果</param>
       public virtual string Calculate(string lotteryType, string issueCode, string openResult)
       {
           string key = lotteryType + "_" + issueCode;

           // LogManager.Error(key+"----");
           //计算投注
           List<Running> outBettRunning = null;
           if (IssueBettingNumsing.CreateInstance().HasRunning(key, out outBettRunning))
               this.CalculateBetting(lotteryType, issueCode, openResult);

           //计算追号
           List<Running> outRunning = null;
           if (IssueCatchNumsing.CreateInstance().HasRunning(key, out outRunning))
           {
               this.CalculateCatchNums(lotteryType, issueCode, openResult);
           }
           else
           {
               //  LogManager.Error(key + "----"+"线程正在处理");
           }

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
            int pageSize=50;
            int totalCount=0;

            IDbContextFactory factory = new DbContextFactory();
            var betDetailService = new BetDetailService(new Repo<BetDetail>(factory));

            var result= betDetailService.GetIssuesBetDetail(lotteryCode, issueCode);
            totalCount = result.Count;
            LogManager.Info(lotteryCode + " 期数：" + issueCode + " 待计算总投注数:" + totalCount);
            if (totalCount < 1)
            {
                IssueCatchNumsing.CreateInstance().CompledRunning(lotteryCode + "_" + issueCode);
                return;

            }
            List<BetDetail> details = new List<BetDetail>();
            var curIndex = 1;
            for (var i = 0; i < totalCount; i++)
            {
                if (result[i].IsBuyTogether == 1 && result[i].GroupByState == 0)
                    continue;
                details.Add(result[i]);
                if (curIndex == pageSize || (i+1)==totalCount)
                {
                    PoolParam param = new PoolParam()
                    {
                        Issue = issueCode,
                        openResult = openResult,
                        PageSize = pageSize,
                        pageIndex = i,
                        lotteryCode = lotteryCode,
                        Details = details
                    };
                    IssueBettingNumsing.CreateInstance().PutRunning(lotteryCode + "_" + issueCode, new Running()
                    {
                        IsCompled = false,
                        PageIndex = i
                    });

                    ThreadPool.QueueUserWorkItem(CalculatePage, param);
                    details = new List<BetDetail>();
                    curIndex = 1;
                }
                curIndex++;
            }
             
        }

        private void CalculatePage(object obj)
        {
            
            PoolParam param=obj as PoolParam;
            string key = param.lotteryCode + "_" + param.Issue;
            if (param == null) {
                LogManager.Info("PoolParam 为null,直接返回！");
                return;
            }
            if (param.WhileIndex > 3)
            {
                string betCodes = "";
                param.Details.ForEach(x => betCodes += x.BetCode);
                LogManager.Error("投注超过三次计算，结束计算:" + betCodes);
                return;
            }
            string issueCode=param.Issue;
            string openResult=param.openResult;

            LogManager.Info("开始计算结果:"+param.ToString());
            //IDbContextFactory factory = new DbContextFactory();
            //var betDetailService = new BetDetailService(new Repo<BetDetail>(factory));
            //var messageService = new MessageService(new Repo<Message>(factory));

            List<BetDetail> source = param.Details;
            List<BetDetail> errorSource = new List<BetDetail>();
            //循环进行计算
            foreach (var item in source)
            {
                int code = item.PalyRadioCode;
                try
                {
                    ICalculate calculate = RadioContentFactory.CreateInstance(code);
                    LogManager.Info(string.Format("创建ICalculate code={0}; 对象为{1}", code, calculate.GetType().ToString()));
                    calculate.Calculate(issueCode, openResult, item);
                    item.OpenResult = openResult;
                    if (item.IsMatch)
                    {
                        item.Stauts = BasicModel.BetResultType.Winning;
                        //插入中奖消息
                       // messageService.Create(CreateMsg(item.BetCode, item.WinMoney, item.UserId, 0));
                        //messageService.Save();
                        LotteryIssuesData.CreateMessage(CreateMsg(item.BetCode, item.WinMoney, item.UserId, 0));

                    }
                    else
                    {
                        item.Stauts = BasicModel.BetResultType.NotWinning;
                        if (item.IsBuyTogether == 1)
                        {
                            //修改子项为未中奖
                            LotteryIssuesData.UpdateBuyTogerher(item.Id, 2,0m);
                        }
                    }

                    OpenOfficialQueue.CreateInstance().Put(item);//添加至更新队列 计算返点以及余额
                    //保存状态
                    // betDetailService.UpdateOpenState(item);
                    if (!LotteryIssuesData.UpdateOpenState(item))
                        errorSource.Add(item);
                }
                catch (Exception ex)
                {
                    LogManager.Error(string.Format("第{0}期，投注明细项id为{1} 计算过程中发生异常", issueCode, item.Id), ex);
                    errorSource.Add(item);//失败项目，添加至列表中，稍后更新
                }
            }
            int errorCount=errorSource.Count;
            if (errorCount > 0)
            {
                param.Details = errorSource;
                param.WhileIndex += 1;
                //继续计算
                CalculatePage(param);
                LogManager.Info("计算失败项总数为"+errorCount+" 系统将继续计算！");
            }

            //betDetailService.Save();
            LogManager.Info("计算结果结束:" + param.ToString());

      
            //完成计算
            IssueBettingNumsing.CreateInstance().CompledRunning(key, param.pageIndex);
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
            int pageSize = 50;
            int totalCount = 0;

            IDbContextFactory factory = new DbContextFactory();
            SysCatchNumService mSysCatchNumService= new SysCatchNumService(new Repo<CatchNum>(factory));
            var source = mSysCatchNumService.GetNotCompledCatchNumList(lotteryCode, issueCode);
            totalCount = source.Count;
            LogManager.Info(lotteryCode + " 期数：" + issueCode + " 待计算总追号数:" + totalCount);
            if (totalCount < 1)
            {
                //移除
                IssueCatchNumsing.CreateInstance().CompledRunning(lotteryCode+"_"+issueCode);
                return;
            }
            //循环进行计算
            List<NotCompledCatchNumListDTO> details = new List<NotCompledCatchNumListDTO>();
            var curIndex = 1;
            for (var i = 0; i < totalCount; i++)
            {
                details.Add(source[i]);
                if (curIndex == pageSize || (i + 1) == totalCount)
                {
                    PoolCatchParam param = new PoolCatchParam()
                    {
                        Issue = issueCode,
                        openResult = openResult,
                        PageSize = pageSize,
                        pageIndex = i,
                        lotteryCode = lotteryCode,
                        Details = details
                    };
                    IssueCatchNumsing.CreateInstance().PutRunning(lotteryCode + "_" + issueCode, new Running()
                    {
                        IsCompled = false,
                        PageIndex = i
                    });

                    ThreadPool.QueueUserWorkItem(CalculateCatchPage, param);
                    details = new List<NotCompledCatchNumListDTO>();
                    curIndex = 1;
                }
                curIndex++;
            }


        }

        /// <summary>
        /// 追号处理模块
        /// </summary>
        /// <param name="obj"></param>
        protected void CalculateCatchPage(object obj)
        {
            PoolCatchParam param= obj as PoolCatchParam;
            if (param.WhileIndex > 3)
            {
                string betCodes = "";
                param.Details.ForEach(x => betCodes += x.CatchNumCode);
                LogManager.Error("追号超过三次计算，结束计算:" + betCodes);
                return;
            }

            string issueCode=param.Issue;
            string openResult=param.openResult;
            string key = param.lotteryCode + "_" + param.Issue;

            List<BasicModel.LotteryBasic.DTO.NotCompledCatchNumListDTO> source = param.Details;
            List<BasicModel.LotteryBasic.DTO.NotCompledCatchNumListDTO> errorSource = new List<NotCompledCatchNumListDTO>();

            IDbContextFactory factory = new DbContextFactory();
            var sysCatchNumIssueService = new SysCatchNumIssueService(new Repo<CatchNumIssue>(factory));
           var sysCatchNumService = new SysCatchNumService(new Repo<CatchNum>(factory));

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
                       // WinMoney = item.WinMoney,
                   };
                   calculate.Calculate(issueCode, openResult, betDetail);
                   //获取追号信息
                   var catchItem = sysCatchNumService.Get(item.Id);
                   //获取追号当期信息
                   var catchNumItem = sysCatchNumIssueService.Get(item.CuiId);
                   if (betDetail.IsMatch)
                   {
                       catchNumItem.Stauts = BasicModel.BetResultType.Winning;
                       catchNumItem.WinMoney = betDetail.WinMoney;
                       catchItem.WinIssue += 1;

                       string content = string.Format("\t编号为【{0}】的方案已中奖 <span style=\"color:red;\">{1}</span> 元,请注意查看您的帐变信息，如有任何疑问请联系在线客服。\t\n", item.CatchNumCode, string.Format("{0:N}", catchNumItem.WinMoney));
                       //messageService.Create();
                       //messageService.Save();
                       LotteryIssuesData.CreateMessage(CreateMsg(content, 0, item.UserId, 1));
                   }
                   else
                   {
                       catchNumItem.Stauts = BasicModel.BetResultType.NotWinning;
                   }

                   //
                   //catchNumItem.OccDate = DateTime.Now;
                   catchNumItem.IsMatch = betDetail.IsMatch;
                   catchNumItem.OpenResult = openResult;



                   if (betDetail.IsMatch && item.IsAutoStop)//中奖，并且设置为中奖后自动结束
                   {

                       catchItem.Stauts = BasicModel.CatchNumType.Compled;
                   }

                   catchItem.WinMoney = catchItem.WinMoney + betDetail.WinMoney;
                   catchItem.CompledIssue = catchItem.CompledIssue + 1;
                   catchItem.CompledMonery = catchItem.CompledMonery + catchNumItem.TotalAmt;

                   //修改信息
                   sysCatchNumIssueService.Save();
                   // sysCatchNumService.Save();
                   //修改未开奖追号期数occdate时间
                   sysCatchNumIssueService.UpdateNoOpenOccDateTime(item.CatchNumCode);

                   OpenOfficialCatchQueueParam queqeParam = new OpenOfficialCatchQueueParam();//处理队列
                   queqeParam.CatchDetail = item;
                   queqeParam.CatchNumIssue = catchNumItem;//当前追号期数
                   if (catchItem.Stauts == CatchNumType.Compled)
                   {
                       //结束本次追号，对未完成追号的撤单
                       var exitNums = sysCatchNumIssueService.GetLastCatchNum(item.CatchNumCode, catchNumItem.IssueCode);
                       if (null == exitNums)
                           return;
                       int exitCount = exitNums.Count;
                       catchItem.SysCannelIssue = exitCount;//保存中奖后结束期数

                       //修改结束期数状态
                       foreach (var exit in exitNums)
                       {
                           exit.Stauts = BetResultType.SysCancel;
                           catchItem.UserCannelMonery += exit.TotalAmt;
                       }
                       sysCatchNumIssueService.Save();//保存状态
                       queqeParam.ExitCatNumIssues = exitNums;//终止追号期数
                   }
                   else if ((catchItem.CompledIssue + catchItem.SysCannelIssue + catchItem.UserCannelIssue) >= catchItem.CatchIssue)
                   {
                       //验证是否有剩余期数，若无剩余期数，本次追号完成
                       catchItem.Stauts = CatchNumType.Compled;
                   }
                   sysCatchNumService.Save();
                   //添加至队列
                   OpenOfficialCatchQueue.CreateInstance().Put(queqeParam);
               }
               catch (Exception ex)
               {
                   LogManager.Error(string.Format("第{0}期，追号项id为{1} 计算过程中发生异常；", issueCode, item.Id), ex);
                   errorSource.Add(item);
               }

               int errorCount = errorSource.Count;
               if (errorCount > 0)
               {
                   param.Details = errorSource;
                   //继续计算
                   param.WhileIndex += 1;
                   CalculatePage(param);
                   LogManager.Info("追号计算失败项总数为" + errorCount + " 系统将继续计算！");
               }
               //完成计算
               IssueCatchNumsing.CreateInstance().CompledRunning(key, param.pageIndex);
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
        protected Message CreateMsg(string betCode, decimal winMonery, int touserid,int betType)
        {
            string title = string.Format("恭喜您，编号为【{0}】的方案已中奖。", betCode);
            string content = "";
            if (betType == 0)
                content += string.Format("\t编号为【{0}】的方案已中奖 <span style=\"color:red;\">{1}</span> 元,请注意查看您的帐变信息，如有任何疑问请联系在线客服。\t\n", betCode, Math.Round(winMonery,2));
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


    }


    /// <summary>
    /// 投注参数
    /// </summary>
    public class PoolParam
    {
        public int pageIndex { get; set; }

        public int PageSize { get; set; }

        public string openResult { get; set; }//开奖结构

        public string lotteryCode { get; set; }//类型

        public string Issue { get; set; }//当前期号

        public List<BetDetail> Details { get; set; }

        /// <summary>
        /// 错误次数，一旦超过三次，系统不进行计算
        /// </summary>
        public int WhileIndex { get; set; }

        public override string ToString()
        {
            return string.Format("pageIndex:{0} PageSize:{1} openResult:{2} lotteryCode:{3} Issue:{4}", pageIndex, PageSize, openResult, lotteryCode, Issue);
        }
    }

    /// <summary>
    /// 追号参数
    /// </summary>
    public class PoolCatchParam
    {
        public int pageIndex { get; set; }

        public int PageSize { get; set; }

        public string openResult { get; set; }//开奖结构

        public string lotteryCode { get; set; }//类型

        public string Issue { get; set; }//当前期号

        public List<NotCompledCatchNumListDTO> Details { get; set; }

        /// <summary>
        /// 错误次数，一旦超过三次，系统不进行计算
        /// </summary>
        public int WhileIndex { get; set; }

        public override string ToString()
        {
            return string.Format("pageIndex:{0} PageSize:{1} openResult:{2} lotteryCode:{3} Issue:{4}", pageIndex, PageSize, openResult, lotteryCode, Issue);
        }
    }

    public class SyncContextParam
    {
        public BetDetail BetDetail { get; set; }
    }
}
