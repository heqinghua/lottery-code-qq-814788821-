using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.BasicModel.Report;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 用户余额明细表
    /// </summary>
    //[ServiceContract]
    public interface ISysUserBalanceDetailService : ICrudService<SysUserBalanceDetail>
    {
        /// <summary>
        /// 前端查询：帐变
        /// </summary>
        /// <param name="tradeType">类型</param>
        /// <param name="startTime">帐变开始时间</param>
        /// <param name="endTime">帐变结束日期</param>
        /// <param name="tradeDateTime">交易类型</param>
        /// <param name="account">用户名</param>
        /// <param name="userType">范围(用户类型)</param>
        /// <param name="codeType">编号查询</param>
        /// <param name="code">编号</param>
        /// <param name="lotteryCode">游戏名称</param>
        /// <param name="palyRadioCode">玩法</param>
        /// <param name="issueCode">期数</param>
        /// <param name="model">所有模式-1 0元、1角、2分</param>
        /// <param name="userId">登录用户</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<AmountChangeDTO> SelectBy(string tradeType, DateTime startTime, DateTime endTime, int tradeDateTime, string account,
            int userType, int codeType, string code, string lotteryCode, int palyRadioCode, string issueCode,
            int model, int userId, int pageIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 盈亏列表
        /// </summary>
        /// <param name="currenyUserId">当前用户ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="account">用户，精确查找</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<ProfitLossDTO> SelectProfitLossBy(int currenyUserId, DateTime startTime,
            DateTime endTime, string account, int pageIndex, int pageSize,
            ref int totalCount);

        [OperationContract]
        List<ProfitLossList> SelectProfitLossList(DateTime startTime, DateTime endTime, int type,
            int pageIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 统计报表
        /// </summary>
        /// <param name="currenyUserId">当前用户ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="account">账号</param>
        /// <param name="startRebt">起始返点</param>
        /// <param name="endRebt">结束返点</param>
        /// <param name="isSelf">自身 or 团队   自身1  团队2</param>
        /// <param name="type">投注 1 返点 2 奖金3</param>
        /// <param name="minNum">最小金额</param>
        /// <param name="maxNum">最大金额</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<StatisticsReportDTO> SelectStatisticsReportBy(int currenyUserId, DateTime startTime, DateTime endTime,
            string account, decimal startRebt, decimal endRebt, int isSelf, int type, string minNum, string maxNum, int pageIndex, int pageSize,
            ref int totalCount);

        [OperationContract]
        List<BasicModel.Report.PrizeList> SelectPrizeListBy(DateTime startTime, DateTime endTime, string lotteryCode,
            int palyRadioCode, string issueCode, int model, string betCode, string userAccount,
            TradeType tradeType, int pageIndex, int pageSize, ref int totalCount);

        [OperationContract]
        List<BalanceDetailsStatistical> SelectStatisticalListBy(DateTime startTime, DateTime endTime, TradeType tradeType,
            int type, int pageIndex, int pageSize, ref int totalCount, ref decimal totalAmt);

        /// <summary>
        /// 删除对应的记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[OperationContract]
        //bool DeleteById(int id);

        /// <summary>
        /// 撤消派奖
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool UndoAward(int id);

        bool Chedan(string relevanceNo);

        /// <summary>
        /// 后台 查看充值记录
        /// </summary>
        /// <param name="code"></param>
        /// <param name="serialNo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<RechargeRecodVM> SelectRechargeRecod(string code, string serialNo, string sdate, string edate, int pageIndex, ref int totalCount);

        /// <summary>
        /// 后台 查看充值记录
        /// </summary>
        /// <param name="code"></param>
        /// <param name="serialNo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<MentionRecodVM> SelectMentionRecod(string code, string serialNo, string sdate, string edate, int type, int pageIndex, ref int totalCount);

        /// <summary>
        /// 后台管理功能 报表管理 结算报表、消费报表 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <param name="isSettlementReport"></param>
        /// <param name="isMonth"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<SettlementReportVM> SelectSettlementReport(string code, DateTime sDate, DateTime eDate, bool isSettlementReport, bool isMonth, int pageIndex, ref int totalCount);

        /// <summary>
        /// 获取当未处理前提现,充值人数
        /// </summary>
        /// <param name="withdrawPeopleNumber">当前提现人数</param>
        /// <param name="rechargePeopleNumber">当前充值人数</param>
        [OperationContract]
        WithdrawRechargePersonNumberDTO GetWithdrawRechargePersonNumber();

        /// <summary>
        /// 获取下级投注金额
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        List<YongJinDTO> GetChildrensByMonery(int uid);

        /// <summary>
        /// 盈亏列表---后台管理
        /// </summary>
        /// <param name="currenyUserId">当前用户ID  -1为总代理</param>
        /// <param name="userCode">会员号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="account">用户，精确查找</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<ProfitLossDTOMan> SelectProfitLossByManager(int currenyUserId,string userCode, DateTime startTime, DateTime endTime, int pageIndex, int pageSize, ref int totalCount);


        /// <summary>
        /// 盈亏列表---后台管理20170221 by add ,优化后的盈亏统计查询
        /// </summary>
        /// <param name="currenyUserId">当前用户ID  -1为总代理</param>
        /// <param name="userCode">会员号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="account">用户，精确查找</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<ProfitLossDTOMan> SelectProfitLossByManagerNew(int currenyUserId, string userCode, DateTime startTime, DateTime endTime, int pageIndex, int pageSize, ref int totalCount);

         /// <summary>
         /// 系统充值记录
         /// </summary>
         /// <param name="nikeName"></param>
         /// <param name="code"></param>
         /// <param name="beginDate"></param>
         /// <param name="endDate"></param>
         /// <param name="state"></param>
         /// <param name="begindec"></param>
         /// <param name="enddec"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageSize"></param>
         /// <param name="totalCount"></param>
         /// <returns></returns>
        List<Ytg.BasicModel.Manager.SysUserBalanceDetailRechange> FilterUserBalanceDetails(string nikeName, string code, DateTime? beginDate, DateTime? endDate, int state, decimal? begindec, decimal? enddec, int tradeType, int pageIndex, int pageSize, ref int totalCount);


        /// <summary>
        /// 盈亏列表  20170315 by add
        /// </summary>
        /// <param name="currenyUserId">当前用户ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="account">用户，精确查找</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<ProfitLossDTO> SelectProfitLossByNew(int currenyUserId, DateTime startTime, DateTime endTime,string account, int pageIndex, int pageSize, ref int totalCount);
    }
}
