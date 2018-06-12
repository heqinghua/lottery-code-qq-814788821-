using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.BasicModel.LotteryBasic;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;
using Ytg.Data;

namespace Ytg.Service.Lott
{
    /// <summary>
    /// 投注明细
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BetDetailService : CrudService<BetDetail>, IBetDetailService
    {
        readonly ISysUserBalanceDetailService mSysUserBalanceDetailService;

        public BetDetailService(IRepo<BetDetail> repo)
            : base(repo)
        {
        }

        public BetDetailService(IRepo<BetDetail> repo, ISysUserBalanceDetailService sysUserBalanceDetailService)
            : base(repo)
        {
            mSysUserBalanceDetailService = sysUserBalanceDetailService;
        }

        public int UpdateOpenState(BetDetail detail)
        {
            string sql = "update BetDetails set IsMatch={0},WinMoney={1},Stauts={2},OpenResult={3} where id={4}";
            return this.mRepo.GetDbContext.Database.ExecuteSqlCommand(sql,detail.IsMatch,detail.WinMoney,detail.Stauts,detail.OpenResult,detail.Id);
          
        }

        /// <summary>
        /// 获取当期所有投注总数
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <param name="issues"></param>
        /// <returns></returns>
        public int GetIssuesBetDetailCount(string lotteryCode, string issues)
        {
            return this.Where(item => item.LotteryCode == lotteryCode && item.IssueCode == issues && item.Stauts == BetResultType.NotOpen).Count();

        }

        /// <summary>
        /// 获取当期所有投注总数
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <param name="issues"></param>
        /// <returns></returns>
        public List<BetDetail> GetIssuesBetDetail(string lotteryCode, string issues)
        {
           //return this.Where(item =>item.LotteryCode==lotteryCode &&  item.IssueCode == issues && item.Stauts == BetResultType.NotOpen);
            string sql = "select * from BetDetails where LotteryCode='" + lotteryCode + "' and IssueCode='" + issues + "' and Stauts=3 ";
            return this.mRepo.GetDbContext.Database.SqlQuery<BetDetail>(sql).ToList();
        }
       
        
        public List<BetList> GetManagerBetList()
        {
            return null;
        }


        public List<BetList> GetBetListBy(DateTime startTime, DateTime endTime, int status, int tradeType,
            string lotteryCode, int palyRadioCode, string issueCode, int model, string betCode,
            string userAccount, int userType, int userId, int pageIndex, int pageSize, ref int totalCount)
        {
            pageSize = Ytg.Comm.AppGlobal.ManagerDataPageSize;
 
//            string sql = @"select  b.Id,b.IssueCode,b.BetCode,b.BetCount,b.TotalAmt,b.Multiple,
// b.Model,b.PrizeType,b.BackNum,b.BetContent,b.OccDate,b.WinMoney,b.OpenResult,
// b.Stauts,u.Code ,lv.PlayTypeRadioName ,lv.RadioCode,lv.PlayTypeNumName ,lv.PlayTypeName ,lv.LotteryName
// from BetDetails as b
// LEFT JOIN SysYtgUser as u ON u.Id = b.UserId
// LEFT JOIN Lottery_Vw as lv on lv.RadioCode= b.PalyRadioCode where 1=1 ";
//            string sql = @"select b.Id,b.IssueCode,b.BetCode,b.BetCount,b.TotalAmt,b.Multiple,b.Model,b.PrizeType,b.BackNum,b.BetContent,b.OccDate,b.WinMoney,b.OpenResult,b.Stauts,
//u.Code ,lv.PlayTypeRadioName ,lv.RadioCode,lv.PlayTypeNumName ,lv.PlayTypeName ,lv.LotteryName
//from(
//	select PalyRadioCode,UserId,Id,IssueCode,BetCode,'' as CatchNumIssueCode,BetCount,TotalAmt,Multiple,
//	 Model,PrizeType,BackNum,BetContent,OccDate,WinMoney,OpenResult,
//	 Stauts,0 as tp from BetDetails 
//	union all
//	select cn.PalyRadioCode,cn.UserId,cni.id,cni.IssueCode,cni.CatchNumCode as BetCode,cni.CatchNumIssueCode,cn.BetCount,cni.TotalAmt,cni.Multiple,
//	cn.Model,cn.PrizeType,cn.BackNum,cn.BetContent,cni.OccDate,cni.WinMoney,cni.OpenResult,cni.Stauts,1 as tp from CatchNums as cn 
//	inner join CatchNumIssues as cni
//	on cn.CatchNumCode=cni.catchNumCode
//) as b
//LEFT JOIN SysYtgUser as u ON u.Id = b.UserId
//LEFT JOIN Lottery_Vw as lv on lv.RadioCode= b.PalyRadioCode where 1=1";
            string sql = "select * from Bett_View where 1=1";
            //order by b.OccDate desc

            //tradeType INT=1,--交易类型，当天1 历史记录2
            //@userType INT,-- 用户类型 -1所有 1 自己 2 直接下级 3 所有下级

            string userWhere = "";
            string putWhere = "";
            switch (userType)
            {
                case 1:
                    userWhere = string.Format("SELECT Id FROM SysYtgUser where Id={0}", userId);
                    break;
                case 2:
                    userWhere = string.Format("SELECT Id FROM SysYtgUser where ParentId={0}", userId);
                    break;
                case 3:
                    userWhere = string.Format("SELECT * FROM f_SysYtgUser_GetChildren({0})", userId);
                    break;
                case -1://所有
                    userWhere = string.Format("SELECT * FROM f_SysYtgUser_GetChildren({0})", userId);
                    putWhere =string.Format( " or  UserId={0}",userId);
                    break;
            }

            //账号
            if (!string.IsNullOrEmpty(userAccount))
                sql += string.Format(" and Code = '{0}'", Utils.ChkSQL(userAccount));
            else
                sql += string.Format(" and (UserId in ({0}) {1})", userWhere, putWhere);

            string beginDate = Utils.GetNowBeginDate().ToString("yyyy/MM/dd ") + startTime.ToString(" HH:mm:ss");
            string endDate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd ") + endTime.ToString(" HH:mm:ss"); 
            if (tradeType == 2)
            {
                beginDate = startTime.ToString("yyyy/MM/dd HH:mm:ss");
                endDate = endTime.ToString("yyyy/MM/dd HH:mm:ss");
            }
            sql += string.Format(" and OccDate between '{0}' and '{1}'", beginDate, endDate);
            //状态 -1 全部 1 已中奖、2 未中奖、3 未开奖、4 已撤单
            if (status != -1)
                sql += string.Format(" and Stauts={0}", status);
            //采种
            if (!string.IsNullOrEmpty(lotteryCode))
                sql += string.Format(" and LotteryCode='{0}'", Utils.ChkSQL(lotteryCode));
            //玩法
            if (palyRadioCode != -1)
                sql += string.Format(" and PalyRadioCode={0}", palyRadioCode);
            //期数
            if (!string.IsNullOrEmpty(issueCode))
                sql += string.Format(" and IssueCode='{0}'", Utils.ChkSQL(issueCode));
            //所有模式-1 0元、1角、2分
            if (model != -1)
                sql += string.Format(" and Model={0}", model);
            //投注号
            if (!string.IsNullOrEmpty(betCode))
                sql += string.Format(" and BetCode like '%{0}%'", Utils.ChkSQL(betCode));
            
   
            sql = "(" + sql + ") as t1";
            int pageCount = 0;
            return this.GetEntitysPage<BetList>(sql, "OccDate", "*", " OccDate desc", ESortType.Auto, pageIndex, pageSize, ref pageCount, ref totalCount);
        }

        public List<BetList> GetBetDetailByUserBalanceDetailSeriaNo(string seriaNo)
        {
            var sql = string.Format(@"SELECT  b.Id,b.IssueCode,b.BetCode,b.BetCount,b.TotalAmt,b.Multiple,
                    b.Model,b.PrizeType,b.BackNum,b.BetContent,b.OccDate,b.WinMoney,b.OpenResult,
		            b.Stauts,
                    u.Code ,
                    CASE WHEN b.PalyRadioCode>=0 THEN pr.PlayTypeRadioName ELSE pt.PlayTypeName END PlayTypeRadioName ,
		CASE WHEN b.PalyRadioCode>=0 THEN pr.RadioCode ELSE -1 END RadioCode,
		CASE WHEN b.PalyRadioCode>=0 THEN pn.PlayTypeNumName ELSE pt.PlayTypeName END PlayTypeNumName ,
                    pt.PlayTypeName ,
                    lt.LotteryName
	            FROM    dbo.BetDetails b
                LEFT JOIN dbo.SysYtgUser u ON u.Id = b.UserId
                LEFT JOIN dbo.PlayTypeRadios pr ON pr.RadioCode = b.PalyRadioCode
                LEFT JOIN dbo.PlayTypeNums pn ON pn.NumCode = pr.NumCode
                LEFT JOIN dbo.PlayTypes pt ON pt.PlayCode = CASE WHEN b.PalyRadioCode>=0 THEN pn.PlayCode ELSE -b.PalyRadioCode END
                LEFT JOIN dbo.LotteryTypes lt ON lt.LotteryCode = pt.LotteryCode
                LEFT JOIN dbo.SysUserBalanceDetails bd ON bd.RelevanceNo=b.BetCode
                WHERE bd.SerialNo='{0}'", seriaNo);
            var list = this.GetSqlSource<BetList>(sql);
            return list;
        }


        /// <summary>
        /// 根据投注编号编号获取投注详细信息
        /// </summary>
        /// <param name="bettCode"></param>
        /// <returns></returns>
        public BetList GetBetDetailForBetCode(string bettCode)
        {
            string sql = string.Format(@"select bd.*,su.Code,lv.PlayTypeName,CASE WHEN bd.PalyRadioCode>=0 THEN lv.PlayTypeRadioName ELSE lv.PlayTypeName END PlayTypeRadioName,lv.PlayTypeName,lv.LotteryName 
from BetDetails as bd
LEFT join SysYtgUser as su on bd.UserId=su.Id
LEFT join Lottery_Vw as lv on lv.RadioCode=bd.PalyRadioCode OR lv.PlayCode=(-bd.PalyRadioCode) 
where bd.BetCode='{0}'", bettCode);

            return this.GetSqlSource<BetList>(sql).FirstOrDefault();
        }

        public BetList GetBetDetailForAhyCode(string ahiCode)
        {
            string sql = string.Format(@"select bd.Id,bd.IssueCode,bd.BetCode,bd.UserId,bd.BetContent,bd.TotalAmt,bd.PalyRadioCode,bd.Multiple,bd.Model,bd.PrizeType,bd.BackNum,bd.BetContent,bt.WinMonery as WinMoney,bd.IsMatch,bd.OpenResult,bd.Stauts,bd.BonusLevel,bd.OccDate,bd.LotteryCode,bd.PostionName,bd.HasState,bd.IsUseState,bd.IsBuyTogether,bt.Subscription,bd.Secrecy,bd.PartakeUserCount,bd.PartakeMonery,bd.SurplusMonery,bd.Bili,bd.GroupByState,
su.Code,lv.PlayTypeName,CASE WHEN bd.PalyRadioCode >= 0 THEN lv.PlayTypeRadioName ELSE lv.PlayTypeName END PlayTypeRadioName,lv.PlayTypeName,lv.LotteryName
  from BetDetails as bd
LEFT join SysYtgUser as su on bd.UserId = su.Id
LEFT join Lottery_Vw as lv on lv.RadioCode = bd.PalyRadioCode OR lv.PlayCode = (-bd.PalyRadioCode)
left join BuyTogethers as bt on bt.BetDetailId = bd.Id
where bt.BuyTogetherCode ='{0}'", ahiCode);
            return this.GetSqlSource<BetList>(sql).FirstOrDefault();
        }

        public bool DeleteById(int id)
        {
            var item = this.Get(id);
            if (item != null)
            {
                //删除投注记录
                this.Delete(id);

                //删除账变记录,同时将钱返还给用户
                mSysUserBalanceDetailService.Chedan(item.BetCode);
                return true;
            }
            return false;
        }


        /// <summary>
        /// 获取投注记录 状态 -1 全部 1 已中奖、2 未中奖、3 未开奖、4 已撤单
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="lotteryCode"></param>
        /// <returns></returns>
        public List<NotOpenBetDetailDTO> GetNotOpenBetDetail(int uid, int lotteryId)
        {

            string endDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string beginDate = DateTime.Now.AddDays(-5).ToString("yyyy/MM/dd HH:mm:ss");


            string sql = string.Format(@"select top 10 * from (select bet.PostionName,bet.Id,bet.IssueCode,bet.BetCode,bet.BetCount,bet.UserId,bet.TotalAmt,bet.PalyRadioCode,bet.Multiple,bet.Model,
bet.PrizeType,bet.BackNum,bet.BetContent,bet.WinMoney,bet.IsMatch,bet.OpenResult,bet.Stauts,bet.BonusLevel,bet.OccDate,lv.PlayTypeName+lv.PlayTypeRadioName as PlayTypeRadioName,0 as BetType from BetDetails as bet inner join Lottery_Vw as lv on bet.PalyRadioCode=lv.RadioCode
                    where UserId={0} and lv.Id={1} and OccDate between '{2}' and '{3}'
                    union 
                    select cn.PostionName,cn.Id,cn.BeginIssueCode as IssueCode,cn.CatchNumCode as BetCode,cn.BetCount,cn.UserId,cn.SumAmt as TotalAmt,cn.PalyRadioCode,
                    1 as Multiple,cn.Model,cn.PrizeType,cn.BackNum,cn.BetContent,cn.WinMoney,'false' as isMatch,NULL as OpenResult,cn.Stauts,cn.BonusLevel,
                    cn.OccDate,lv.PlayTypeName+lv.PlayTypeRadioName as PlayTypeRadioName,1 as BetType
                    from CatchNums  as cn inner join Lottery_Vw as lv on cn.PalyRadioCode=lv.RadioCode
                    where UserId={0} and lv.Id={1} and OccDate between '{2}' and '{3}') as t1 order by OccDate desc", uid, lotteryId, beginDate, endDate);
            return this.GetSqlSource<NotOpenBetDetailDTO>(sql);
        }


        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Undo(int id,int flag=0)
        {
            var item = this.Get(id);
            if (item != null)
            {
                if (flag == 0)
                    item.Stauts = BasicModel.BetResultType.Cancel;  //本人撤单
                else
                    item.Stauts = BasicModel.BetResultType.SysCancel; //系统撤单
                //撤单，删除余额明细。还需要把钱给退给用户,先这样考虑吧，以后有问题再说。
                //还要找到对应的那条是否有返点记录的数据
                this.Save();
                mSysUserBalanceDetailService.Chedan(item.BetCode);
                return true;
            }
            return false;
        }


       
        /// <summary>
        /// 修改投注内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="betContent">内容</param>
        /// <returns></returns>
        public bool Edit(int id, string betContent)
        {
            var item = this.Get(id);
            if (item != null)
            {
                item.BetContent = betContent;
                this.Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改投注内容，期号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="issueCode">期号</param>
        /// <param name="betContent">内容</param>
        /// <returns></returns>
        public bool Edit(int id, string issueCode, string betContent)
        { 
            var item = this.Get(id);
            if (item != null)
            {
                item.IssueCode=issueCode;
                item.BetContent = betContent;
                this.Save();
                return true;
            }
            return false;
        }

        public BetList SelectById(int id)
        {
            string sql = @"SELECT lt.LotteryName, pr.PlayTypeRadioName,b.BetContent,b.Id
                            FROM dbo.BetDetails b
                            LEFT JOIN dbo.SysYtgUser u ON u.Id = b.UserId
                            LEFT JOIN dbo.PlayTypeRadios pr ON pr.RadioCode = b.PalyRadioCode
                            LEFT JOIN dbo.PlayTypeNums pn ON pn.NumCode = pr.NumCode
                            LEFT JOIN dbo.PlayTypes pt ON pt.PlayCode = pn.PlayCode
                            LEFT JOIN dbo.LotteryTypes lt ON lt.LotteryCode = pt.LotteryCode
                            WHERE b.Id=" + id;
            return this.GetSqlSource<BetList>(sql).FirstOrDefault();
        }


        /// <summary>
        /// 获取最近中奖的20条数据
        /// </summary>
        public List<RecentlyWinDTO> GetRecentlyWin()
        {
            decimal minWinMonery = Convert.ToDecimal(Ytg.Service.Logic.SysSettingHelper.GetSysSetting("HYSPZDZJJE").Value);//会员上榜最低中奖金额
                                                                                                                           //            string sql = string.Format(@"select users.NikeName as Code,t1.WinMoney,t1.IssueCode,lt.LotteryName,t1.OccDate from (select top 20 * from BetDetails where WinMoney>={0} order by occDate desc) as t1 
                                                                                                                           //inner join  SysYtgUser as users on users.id=t1.UserId
                                                                                                                           //inner join  LotteryTypes as lt on lt.LotteryCode=t1.LotteryCode order by  OccDate  desc", minWinMonery);

            string sql = string.Format(@"select users.NikeName as Code,t1.WinMoney,t1.IssueCode,lt.LotteryName,t1.OccDate from (
	select cn.UserId,cn.LotteryCode,t2.WinMoney,t2.IssueCode,t2.OccDate from (
	select top 10 CatchNumCode,IssueCode,WinMoney,OccDate from CatchNumIssues where WinMoney>={0} order by OccDate desc
	) as t2
	left join CatchNums as cn on cn.CatchNumCode=t2.CatchNumCode
	union all
	select * from (
	select top 40 UserId,LotteryCode,WinMoney,IssueCode,OccDate from BetDetails where WinMoney>={0} order by occDate desc
	) as t3
) as t1 left join  SysYtgUser as users on users.id=t1.UserId left join  LotteryTypes as lt on lt.LotteryCode=t1.LotteryCode order by  OccDate  desc", minWinMonery);
            return this.GetSqlSource<RecentlyWinDTO>(sql);
        }


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
        public List<BetList> GetBetRecord(DateTime startTime, DateTime endTime, int status, string lotteryCode, int palyRadioCode, string issueCode, 
            int model, string betCode, string userAccount, int userType, int pageIndex, int pageSize, ref int totalCount)
        {
            //参数设置
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@beginTime",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@endTime",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@status",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@lotteryCode",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@palyRadioCode",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@issueCode",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@model",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@betCode",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@userCode",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@userType",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output)
            };

            parameters[0].Value = startTime;
            parameters[1].Value = endTime;
            parameters[2].Value = status;
            parameters[3].Value = lotteryCode;
            parameters[4].Value = palyRadioCode;
            parameters[5].Value = issueCode;
            parameters[6].Value = model;
            parameters[7].Value = betCode;
            parameters[8].Value = userAccount;
            parameters[9].Value = userType;
            parameters[10].Value = pageIndex;
            parameters[11].Value = pageSize;

            List<BetList> list = this.ExProc<BetList>("sp_GetBetRecord", parameters);
            totalCount = Convert.ToInt32(parameters[12].Value);

            return list;
        }

        /// <summary>
        /// 根据用户id和时间获取投注总额
        /// </summary>
        /// <param name="?"></param>
        public decimal GetUserBettMonery(int userId, DateTime beginDate, DateTime endDate)
        {

            string spName = "sp_UserBettMonery";
            /*
                @userId int, --用户Id
 @beginDate datetime,--开始时间
 @endDate datetime, --结束时间
 @sumMonery decimal(18, 2) output --输出统计金额
             */

            DbParameter[] pramers = new DbParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@userId",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@beginDate",SqlDbType.DateTime),
                    new System.Data.SqlClient.SqlParameter("@endDate",SqlDbType.DateTime),
                    new System.Data.SqlClient.SqlParameter("@sumMonery",SqlDbType.Decimal),
            };
            pramers[0].Value = userId;
            pramers[1].Value = beginDate;
            pramers[2].Value = endDate;
            pramers[3].Direction = ParameterDirection.Output;
            this.ExProcNoReader(spName, pramers);
            object parenter=pramers[3].Value;
            if(parenter==null)
                return 0;
            return (decimal)parenter;
        }


        /// <summary>
        /// 根据用户id和时间获取团队投注总额和有效投注人数
        /// </summary>
        /// <param name="?"></param>
        public decimal GetUserGroupBettMonery(int userId, DateTime beginDate, DateTime endDate,ref int bettUserCount)
        {

            string spName = "sp_wages";
            /*
                @userId int, --用户Id
 @beginDate datetime,--开始时间
 @endDate datetime, --结束时间
 @sumMonery decimal(18, 2) output --输出统计金额
             */

            DbParameter[] pramers = new DbParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@userId",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@beginDate",SqlDbType.DateTime),
                    new System.Data.SqlClient.SqlParameter("@endDate",SqlDbType.DateTime),
                    new System.Data.SqlClient.SqlParameter("@sumMonery",SqlDbType.Decimal),
                    new System.Data.SqlClient.SqlParameter("@bettUserCount",SqlDbType.Int),
                    
            };
            pramers[0].Value = userId;
            pramers[1].Value = beginDate;
            pramers[2].Value = endDate;
            pramers[3].Direction = ParameterDirection.Output;
            pramers[4].Direction = ParameterDirection.Output;
            this.ExProcNoReader(spName, pramers);
            object parenter = pramers[3].Value;
            if (parenter == null)
                return 0;
            bettUserCount = pramers[4].Value == DBNull.Value ? 0 : Convert.ToInt32(pramers[4].Value);
            return (decimal)parenter;
        }

        /// <summary>
        /// 投注  存储过程 
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="balanceDetail"></param>
        /// <returns></returns>
        public decimal? AddBetting(BetDetail detail, SysUserBalanceDetail balanceDetail, int lotteryid, ref int state)
        {
            string spName = "sp_addBetting";
          
            DbParameter[] pramers = new DbParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@BetCode",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@IssueCode",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@BetContent",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@Model",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@Multiple",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@PalyRadioCode",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@PrizeType",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@UserId",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@TotalAmt",SqlDbType.Decimal),
                    new System.Data.SqlClient.SqlParameter("@LotteryCode",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@BetCount",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@BackNum",SqlDbType.Decimal),
                    new System.Data.SqlClient.SqlParameter("@BonusLevel",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@PostionName",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@HasState",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@lotteryid",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@TradeType",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@SerialNo",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@nowDate",SqlDbType.VarChar),

                    new System.Data.SqlClient.SqlParameter("@IsBuyTogether",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@Subscription",SqlDbType.Decimal),
                    new System.Data.SqlClient.SqlParameter("@SurplusMonery",SqlDbType.Decimal),
                    new System.Data.SqlClient.SqlParameter("@Bili",SqlDbType.Float),
                    new System.Data.SqlClient.SqlParameter("@Secrecy",SqlDbType.Int),
                    

                    new System.Data.SqlClient.SqlParameter("@state",SqlDbType.Int)
            };
            pramers[0].Value = detail.BetCode;
            pramers[1].Value = detail.IssueCode;
            pramers[2].Value = detail.BetContent;
            pramers[3].Value = detail.Model;
            pramers[4].Value = detail.Multiple;
            pramers[5].Value = detail.PalyRadioCode;
            pramers[6].Value = detail.PrizeType;
            pramers[7].Value = detail.UserId;
            pramers[8].Value = detail.TotalAmt;
            pramers[9].Value = detail.LotteryCode;
            pramers[10].Value = detail.BetCount;
            pramers[11].Value = detail.BackNum;
            pramers[12].Value = detail.BonusLevel;
            pramers[13].Value = string.IsNullOrEmpty(detail.PostionName) ? "" : detail.PostionName;
            pramers[14].Value = detail.HasState;
            pramers[15].Value = lotteryid;
            pramers[16].Value = balanceDetail.TradeType;
            pramers[17].Value = balanceDetail.SerialNo;
            pramers[18].Value =DateTime.Now;

            pramers[19].Value = detail.IsBuyTogether;
            pramers[20].Value = detail.Subscription;
            pramers[21].Value = detail.SurplusMonery;
            pramers[22].Value = detail.Bili;
            pramers[23].Value = detail.Secrecy;

            pramers[24].Direction = ParameterDirection.Output;

           this.ExProcNoReader(spName, pramers);
            object parenter = pramers[24].Value;
            if (parenter != null)
            {
                state = Convert.ToInt32(parenter);
            }
            else
            {
                state = -1;
            }

            return 0;
        }

        /// <summary>
        /// 修改用户投注内容以及用户追号内容返点信息，在用户频繁修改返点时修改 2017/02/11
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public void UpdateCatchBett(int userid,float backnum)
        {
            string spName = "sp_updatebett";
       
            DbParameter[] pramers = new DbParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@userid",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@backnum",SqlDbType.Float),
                    
            };
            pramers[0].Value = userid;
            pramers[1].Value = backnum;
            this.ExProcNoReader(spName, pramers);
        }

        /// <summary>
        /// 获取合买投注列表
        /// </summary>
        /// <param name="status"></param>
        /// <param name="userAccount"></param>
        /// <param name="pageIndx"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<HeMaiDto> GetBetListBy(int status, string userAccount, int pageIndx, int pageSize,string lotteryCode, ref int totalCount)
        {
            string sql = @"select us.Code,us.NikeName,bet.* from BetDetails as bet inner join SysYtgUser as us on bet.UserId = us.Id where IsBuyTogether = 1";
            if (!string.IsNullOrEmpty(userAccount))
                sql += " and us.NikeName like '%" + userAccount + "%'";
            if (status >= 0)
                sql += " and bet.Stauts =" + status;
            if (!string.IsNullOrEmpty(lotteryCode))
            {
                sql += " and bet.LotteryCode='" + lotteryCode + "'";
               
            }
            if (lotteryCode == "fc3d" || lotteryCode == "pl5")
            {

            }
            else
            {
                sql += " and bet.LotteryCode not in('fc3d','pl5')";
            }

            // 
            sql = "(" + sql + ") as t1";
            int pageCount = 0;
            return this.GetEntitysPage<HeMaiDto>(sql, "OccDate", "*", " OccDate desc", ESortType.Auto, pageIndx, pageSize, ref pageCount, ref totalCount);
        }

        /// <summary>
        /// 获取所有未满的合买单
        /// </summary>
        /// <returns></returns>
        public List<HeMaiDto> GetBetListByFc()
        {
            string sql = @"select us.Code,us.NikeName,bet.* from BetDetails as bet inner join SysYtgUser as us on bet.UserId = us.Id where IsBuyTogether = 1 and LotteryCode in('fc3d','pl5') and bet.Stauts=3";
            return this.GetSqlSource<HeMaiDto>(sql);
        }

        /// <summary>
        /// 获取合买单项内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HeMaiDtoChildren GetBetItemBy(int id)
        {
            string sql = @"select us.Code,us.NikeName,bet.*,lv.PlayTypeRadioName,lv.PlayTypeName from BetDetails as bet inner join SysYtgUser as us on bet.UserId = us.Id inner join Lottery_Vw as lv on bet.PalyRadioCode=lv.RadioCode where bet.Id = " + id;
            var res = this.GetSqlSource<HeMaiDtoChildren>(sql);
            return res.FirstOrDefault();
        }
    }
}
