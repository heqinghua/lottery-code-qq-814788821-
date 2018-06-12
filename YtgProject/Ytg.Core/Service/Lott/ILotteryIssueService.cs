using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.BasicModel.LotteryBasic;
using Ytg.BasicModel.LotteryBasic.DTO;

namespace Ytg.Core.Service.Lott
{
    [ServiceContract]
    public interface ILotteryIssueService : ICrudService<LotteryIssue>
    {
        [OperationContract]
        IEnumerable<LotteryIssue> GetLotteryIssues(int lotteryId, DateTime? beginDate, DateTime? endDate,int topCount=-1);

        [OperationContract]
        bool CreateAllLotteryIssue();

        /// <summary>
        /// 获取当天所有未开奖期数数据
        /// </summary>
        /// <param name="lotteryId">彩票类型</param>
        /// <returns></returns>
        [OperationContract]
        List<LotteryIssueDTO> GetOccDayNoOpenLotteryIssue(int lotteryId);

        /// <summary>
        /// 获取最后一条期数数据
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        LotteryIssue GetLastIssue(int lotteryid);

        /// <summary>
        /// 手动开奖
        /// </summary>
        /// <param name="issueCode">期号</param>
        /// <param name="openResult">开奖结果</param>
        /// <returns></returns>
        [OperationContract]
        void ManualOpen(string lotteryCode, string issueCode, string openResult);


         /// <summary>
         /// 修改开奖结果
         /// </summary>
         /// <param name="expect"></param>
         /// <param name="openCode"></param>
         /// <param name="lotteryid"></param>
         /// <returns></returns>
        bool UpdateOpenResult(string expect, string openCode, int lotteryid);

        /// <summary>
        /// 修改开奖结果
        /// </summary>
        /// <param name="expect"></param>
        /// <param name="openCode"></param>
        /// <param name="openTime"></param>
        /// <param name="lotteryid"></param>
        /// <returns></returns>
        bool UpdateOpenResult(string expect, string openCode, DateTime openTime, int lotteryid);

        /// <summary>
        /// 生成彩票期数
        /// </summary>
        /// <param name="issue"></param>
        [OperationContract]
        void AddLotteryIssueCode(LotteryIssue issue);

        /// <summary>
        /// 获取历史当前记录
        /// </summary>
        /// <param name="lotteryid"></param>
        /// <returns></returns>
        List<LotteryIssue> GetHisIssue(int lotteryid, int topValue, DateTime? begin, DateTime? end);

         /// <summary>
         /// 获取当前50分钟内未开奖的期数
         /// </summary>
         /// <returns></returns>
        List<int> GetNotOpenIssues(string notLotteryIds);

          /// <summary>
        /// 获取当前50分钟内未开奖的期数
        /// </summary>
        /// <returns></returns>
        List<int> GetInIssues(string lotteryIds);

        ///// <summary>
        ///// 获取当前待开奖期数
        ///// </summary>
        ///// <param name="notLotteryIdArray"></param>
        ///// <returns></returns>
        //IEnumerable<LotteryIssue> GetNowOpenIssues(string notLotteryIds);

        /// <summary>
        /// 根据期数获取开奖结果
        /// </summary>
        /// <returns></returns>
        LotteryIssueDTO GetIssueOpenResult(string issue, int lotteryId);

        /// <summary>
        /// 获取当天数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<LotteryIssue> GetNowDayIssue();

         /// <summary>
        ///  获取当天所有对象，根据彩种
         /// </summary>
         /// <param name="lotteryId"></param>
         /// <returns></returns>
        IEnumerable<LotteryIssue> GetNowDayIssue(int lotteryId);

        /// <summary>
        /// 获取指定菜种当天所有期数
        /// </summary>
        /// <param name="lotteryid"></param>
        /// <returns></returns>
        IEnumerable<LotteryIssue> GetNowDayLotteryTypeIssue(int lotteryid);

        /// <summary>
        /// 生成明天期数数据
        /// </summary>
        void BuilderNextDayIssues();

        /// <summary>
        /// 获取当前已开奖的前11条记录
        /// </summary>
        /// <returns></returns>
        List<LotteryIssue> GetTopOpendIssue(int lotteryid);

        /// <summary>
        /// 获取当前已开奖的前5条记录
        /// </summary>
        /// <returns></returns>
        List<LotteryIssue> GetTop5OpendIssue(int lotteryid);

        /// <summary>
        /// 获取当前已开奖的前50条记录
        /// </summary>
        /// <returns></returns>
        List<LotteryIssue> GetTop50OpendIssue(int lotteryid);

        /// <summary>
        /// 获取当前已开奖的前100条记录
        /// </summary>
        /// <returns></returns>
        List<LotteryIssue> GetTop100OpendIssue(int lotteryid);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateLottIssue(LotteryIssue item);

         /// <summary>
         /// 修改彩种期号时间
         /// </summary>
         /// <param name="item"></param>
         /// <returns></returns>
        bool UpdateLotteryIssueTime(LotteryIssue item);


        /// <summary>
        /// 获取当前销售的期数信息
        /// </summary>
        /// <param name="lotteryId"></param>
        /// <returns></returns>
        [OperationContract]
        LotteryIssue GetNowSalesIssue(int lotteryId);

        /// <summary>
        /// 根据期号获取期数信息
        /// </summary>
        /// <param name="issueCode"></param>
        /// <returns></returns>
        LotteryIssue Get(string issueCode);

        /// <summary>
        /// 根据彩种和期号获取期号信息
        /// </summary>
        /// <param name="lotteryid"></param>
        /// <param name="issueCode"></param>
        /// <returns></returns>
        LotteryIssue Get(int lotteryid, string issueCode);

        /// <summary>
        /// 清除期数
        /// </summary>
        /// <param name="lotteryid"></param>
        void ClearIssues(int lotteryid);

        ///// <summary>
        ///// 获取期数投注数据  --- GP
        ///// </summary>
        ///// <returns></returns>
        //[OperationContract]
        //List<LotteryResultDTO> GetLotteryIssueResult(string lotteryCode, string issueCode, DateTime date, int pageIndex, int pageSize, ref int totalCount); 


        /// <summary>
        /// 根据彩种id,行数获取开奖结果
        /// </summary>
        /// <param name="lotteryId"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        List<IssueResultDTO> FilterResult(int lotteryId, int rows);


        /// <summary>
        /// 根据彩票id获取期数信息
        /// </summary>
        /// <param name="lotteryid"></param>
        /// <returns></returns>
        Ytg.BasicModel.DTO.Server_IssueResultDTO Server_TopIssueResult(int lotteryid);


        /// <summary>
        /// 设置开奖结果为null
        /// </summary>
        /// <param name="lotteryid"></param>
        /// <param name="issueCode"></param>
        /// <returns></returns>
        bool SetResultNull(int lotteryid, string issueCode);

        /// <summary>
         /// 获取期数
         /// </summary>
         /// <param name="lotteryid"></param>
         /// <param name="issueCode"></param>
         /// <returns></returns>
        LotteryIssue SqlGetIssueCode(int lotteryid, string issueCode);

        /**
        获取所有彩种的开奖记录
            */
        List<LotteryIssueDTO_Opens> GetAllLotteryOpens();


        /// <summary>
        /// 获取开奖历史
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<LotteryIssueDTO_Opens> GetNextPreds(string ids);
    }
}
