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
    /// <summary>
    /// 投注明细
    /// </summary>
    [ServiceContract]
    public interface IBetDetailService : ICrudService<BetDetail>
    {
        /// <summary>
        /// 修改投注明细开奖状态
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        int UpdateOpenState(BetDetail detail);


        /// <summary>
        /// 获取当期所有投注总数
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <param name="issues"></param>
        /// <returns></returns>
        int GetIssuesBetDetailCount(string lotteryCode, string issues);
        /// <summary>
        /// 获取指定期数的所有投注信息
        /// </summary>
        /// <param name="issues"></param>
        /// <returns></returns>
        List<BetDetail> GetIssuesBetDetail(string lotteryCode, string issues);

        /// <summary>
        /// 获得当前用户投注记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="status">状态 -1 全部 1 已中奖、2 未中奖、3 未开奖、4 已撤单</param>
        /// <param name="tradeType">交易类型，当天1 历史记录2</param>
        /// <param name="lotteryCode">彩种</param>
        /// <param name="palyRadioCode">玩法</param>
        /// <param name="issueCode">期数</param>
        /// <param name="model">所有模式-1 0元、1角、2分</param>
        /// <param name="betCode">投注号</param>
        /// <param name="userAccount">用户账号</param>
        /// <param name="userType">用户类型 -1所有 1 自己 2 直接下级 3 所有下级</param>
        /// <param name="userId">当前登录用户Id</param>
        /// <returns></returns>
        [OperationContract]
        List<BetList> GetBetListBy(DateTime startTime, DateTime endTime, int status, int tradeType,
            string lotteryCode, int palyRadioCode, string issueCode, int model, string betCode,
            string userAccount, int userType, int userId, int pageIndx, int pageSize, ref int totalCount);

        [OperationContract]
        List<BetList> GetBetDetailByUserBalanceDetailSeriaNo(string seriaNo);

        /// <summary>
        /// 获取未开奖的投注详情
        /// </summary>
        /// <param name="uid">登录用户id</param>
        /// <param name="lotteryCode">彩种</param>
        /// <returns></returns>
        List<NotOpenBetDetailDTO> GetNotOpenBetDetail(int uid, int lotteryId);

        /// <summary>
        /// 删除某条记录,这个是后台管理员用户可以直接删除对应的记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteById(int id);

        /// <summary>
        /// 撤单，把之前的金额进行恢复
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag">flag=0本人撤单，flag=1系统撤单</param>
        /// <returns></returns>
        [OperationContract]
        bool Undo(int id, int flag = 0);

        /// <summary>
        /// 修改投注内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="betContent"></param>
        /// <returns></returns>
        [OperationContract]
        bool Edit(int id, string betContent);

         /// <summary>
        /// 修改投注内容，期号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="issueCode">期号</param>
        /// <param name="betContent">内容</param>
        /// <returns></returns>
        [OperationContract]
        bool Edit(int id, string issueCode, string betContent);

        [OperationContract]
        BetList SelectById(int id);

        /// <summary>
        /// 根据投注编号获取投注详细信息
        /// </summary>
        /// <param name="bettCode"></param>
        /// <returns></returns>
        BetList GetBetDetailForBetCode(string bettCode);

        /// <summary>
        /// 获取最近中奖20期数据
        /// </summary>
        /// <returns></returns>
        List<RecentlyWinDTO> GetRecentlyWin();


        /// <summary>
        /// 方法说明: 获取投注记录
        /// 创建时间：2015-05-21
        /// 创建者：GP
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="status"></param>
        /// <param name="lotteryCode"></param>
        /// <param name="palyRadioCode"></param>
        /// <param name="issueCode"></param>
        /// <param name="model"></param>
        /// <param name="betCode"></param>
        /// <param name="userAccount"></param>
        /// <param name="userType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        [OperationContract]
        List<BetList> GetBetRecord(DateTime startTime, DateTime endTime, int status, string lotteryCode, int palyRadioCode, string issueCode,
            int model, string betCode, string userAccount, int userType, int pageIndex, int pageSize, ref int totalCount);


        /// <summary>
        /// 根据用户id和时间获取投注总额
        /// </summary>
        /// <param name="?"></param>
        decimal GetUserBettMonery(int userId, DateTime beginDate, DateTime endDate);

         /// <summary>
        /// 投注  存储过程 
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="balanceDetail"></param>
        /// <returns></returns>
        decimal? AddBetting(BetDetail detail, SysUserBalanceDetail balanceDetail, int lotteryid, ref int state);

 /// <summary>
        /// 修改用户投注内容以及用户追号内容返点信息，在用户频繁修改返点时修改 2017/02/11
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        void UpdateCatchBett(int userid, float backnum);
		
        /// <summary>
        /// 根据用户id和时间获取团队投注总额和有效投注人数
        /// </summary>
        /// <param name="?"></param>
        decimal GetUserGroupBettMonery(int userId, DateTime beginDate, DateTime endDate,ref int bettUserCount);


        List<HeMaiDto> GetBetListBy(int status,string userAccount,  int pageIndx, int pageSize,string lotteryCode, ref int totalCount);

        /// <summary>
        /// 获取合买单项内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        HeMaiDtoChildren GetBetItemBy(int id);


        BetList GetBetDetailForAhyCode(string ahiCode);

        /// <summary>
        /// 获取所有未满的合买单
        /// </summary>
        /// <returns></returns>
        List<HeMaiDto> GetBetListByFc();
    }
}
