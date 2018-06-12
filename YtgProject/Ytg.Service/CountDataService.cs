using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.Manager;
using Ytg.BasicModel.Report;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    /// <summary>
    /// 报表汇总
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CountDataService : CrudService<CountData>, ICountDataService
    {
        log4net.ILog log = null;
        public CountDataService(IRepo<CountData> repo, IHasher hasher)
            : base(repo)
        {
            log = log4net.LogManager.GetLogger("errorLog");
        }

        public CountData GetCountData(string beginDate,string endDate)
        {
            CountData countData = new CountData();
            try
            {
                StringBuilder sqlStr = new StringBuilder();
                //获取用户数据报表
                //sqlStr.Append("select SUM(case when a.UserType<>2 then 1 else 0 end) as UserCount,");
                //sqlStr.Append("SUM(case when a.UserType<>2 and a.IsLogin=1 then 1 else 0 end) as OnLineNum,");
                //sqlStr.Append("SUM(case when CONVERT(varchar(10),a.OccDate,120)=CONVERT(varchar(10),GETDATE(),120) then 1 else 0 end) as DayRegisterNum,");
                //sqlStr.Append("SUM(case when a.UserType=1 or a.UserType=3 then 1 else 0 end) as ProxyNum,");
                //sqlStr.Append("SUM(case when a.UserType=0 then 1 else 0 end) as MemberNum,");
                //sqlStr.Append("SUM(case when a.UserType<>2 then b.UserAmt else 0 end) as RemainingBalance");
                //sqlStr.Append(" from SysYtgUser as a");
                //sqlStr.Append(" left join SysUserBalances as b on a.Id=b.UserId");
                //sqlStr.Append(" where b.OccDate  between '" + beginDate + "' and '" + endDate + "'");

                string sql = @"select SUM(case when a.UserType<>2 then 1 else 0 end) as UserCount,
SUM(case when a.UserType<>2 and a.IsLogin=1 then 1 else 0 end) as OnLineNum,
SUM(case when CONVERT(varchar(10),a.OccDate,120)=CONVERT(varchar(10),GETDATE(),120) then 1 else 0 end) as DayRegisterNum,
SUM(case when CONVERT(varchar(6),a.OccDate,112)=CONVERT(varchar(6),GETDATE(),112) then 1 else 0 end) as MonRegisterNum,
SUM(case when CONVERT(varchar(6),a.OccDate,112)=CONVERT(varchar(6),dateadd(dd,-day(dateadd(month,-1,getdate()))+1,dateadd(month,-1,getdate())),112) then 1 else 0 end) as PreRegisterNum,
SUM(case when a.UserType=1 or a.UserType=3 then 1 else 0 end) as ProxyNum,
SUM(case when a.UserType=0 then 1 else 0 end) as MemberNum,
SUM(case when a.UserType<>2 then b.UserAmt else 0 end) as RemainingBalance
from SysYtgUser as a
left join SysUserBalances as b on a.Id=b.UserId";

                UserData userData = this.GetSqlSource<UserData>(sql).FirstOrDefault();
                if (userData != null)
                    countData.userData = userData;
                    

                //获取彩票数据报表
                sqlStr = new StringBuilder();
                sqlStr.AppendFormat(@"select lv.LotteryName,ISNULL(SUM(t1.TotalAmt),0) as BetMoney from (
	                select totalAmt,PalyRadioCode from BetDetails where OccDate  between '{0}' and '{1}'
	                union all
	                select SumAmt as totalAmt,PalyRadioCode  from CatchNums where OccDate  between '{0}' and '{1}'
	                ) as t1
                right join Lottery_Vw as lv on lv.RadioCode=t1.PalyRadioCode
                group by lv.LotteryName", beginDate, endDate);

                List<LotteryTypeData> lotteryTypeDataList = this.GetSqlSource<LotteryTypeData>(sqlStr.ToString());
                if (lotteryTypeDataList != null && lotteryTypeDataList.Count > 0)
                    countData.lotteryTypeDataList = lotteryTypeDataList;
                else
                    countData.lotteryTypeDataList = new List<LotteryTypeData>();

                //获取彩票种类
                sqlStr = new StringBuilder();
                sqlStr.Append("select LotteryName,LotteryCode from LotteryTypes");
                List<LotteryData> lotteryDataList = this.GetSqlSource<LotteryData>(sqlStr.ToString());
                if (lotteryDataList != null && lotteryDataList.Count > 0)
                {
                    //获取彩票单选数据报表
                    foreach (var item in lotteryDataList)
                    {
                        sqlStr = new StringBuilder();
                        sqlStr.AppendFormat(@"select lv.LotteryCode,lv.PlayTypeNumName,lv.PlayTypeRadioName as RadioName,ISNULL(SUM(t1.TotalAmt),0) as BetMoney,ISNULL(SUM(t1.BetCount),0) as BetNum from (
	                        select totalAmt,PalyRadioCode,BetCount,LotteryCode from BetDetails where OccDate  between '{1}' and '{2}'
	                        union all
	                        select SumAmt as totalAmt,PalyRadioCode,(BetCount*CatchIssue) as BetCount,LotteryCode   from CatchNums where OccDate  between '{1}' and '{2}'
                        ) as t1
                        right join Lottery_Vw as lv on lv.RadioCode=t1.PalyRadioCode
                         where lv.LotteryCode='{0}'
                        group by lv.LotteryCode,lv.PlayTypeNumName,lv.PlayTypeRadioName", item.LotteryCode, beginDate, endDate);
             
                        List<PlayTypeRadioData> playTypeRadioDataList = this.GetSqlSource<PlayTypeRadioData>(sqlStr.ToString());
                        if (playTypeRadioDataList != null && playTypeRadioDataList.Count > 0)
                            item.playTypeRadioDataList = playTypeRadioDataList;
                        else
                            item.playTypeRadioDataList = new List<PlayTypeRadioData>();
                    }
                    countData.lotteryDataList = lotteryDataList;
                }


                ////获取彩票单选数据报表
                //sqlStr = new StringBuilder();
                //sqlStr.Append("select a.PlayTypeNumName,b.PlayTypeRadioName as RadioName,ISNULL(SUM(c.TotalAmt),0) as BetMoney,ISNULL(SUM(c.BetCount),0) as BetNum from PlayTypeNums as a ");
                //sqlStr.Append("left join PlayTypeRadios as b on a.NumCode=b.NumCode ");
                //sqlStr.Append("left join BetDetails as c on b.RadioCode=c.PalyRadioCode ");
                //sqlStr.Append("group by a.PlayTypeNumName,b.PlayTypeRadioName,a.Id");

                //List<PlayTypeRadioData> playTypeRadioDataList = this.GetSqlSource<PlayTypeRadioData>(sqlStr.ToString());
                //if (playTypeRadioDataList != null && playTypeRadioDataList.Count > 0)
                //    countData.playTypeRadioDataList = playTypeRadioDataList;

                //获取月报表数据
                //sqlStr = new StringBuilder();
                //sqlStr.Append("select top 1 year(OccDate) as [Year],month(OccDate) as [Month],SUM(TotalAmt) as BetMoney,SUM(TotalAmt-WinMoney) as ProfitAndLossMoney ");
                //sqlStr.Append("from BetDetails ");
                //sqlStr.Append("group  by  year(OccDate),month(OccDate) ");
                //sqlStr.Append("order by  year(OccDate),month(OccDate) desc");
                //List<MonthData> monthDataList = this.GetSqlSource<MonthData>(sqlStr.ToString());
                //if (monthDataList != null && monthDataList.Count > 0)
                //    countData.monthDataList = monthDataList;

                //获取当天报表数据
                sqlStr = new StringBuilder();
                sqlStr.Append("select SUM(TotalAmt) as BetMoney,SUM(TotalAmt-WinMoney) as ProfitAndLossMoney,CONVERT(varchar(10),OccDate,120) as OccDate from BetDetails ");
                sqlStr.Append("where OccDate  between '" + beginDate + "' and '" + endDate + "'");
                //sqlStr.Append("where CONVERT(varchar(10),OccDate,120)=CONVERT(varchar(10),GETDATE(),120) ");
                sqlStr.Append("group by CONVERT(varchar(10),OccDate,120)");
                countData.toDayData  = this.GetSqlSource<ToDayData>(sqlStr.ToString());
                if (countData.toDayData == null)
                    countData.toDayData = new List<ToDayData>();
                

                ////获取昨天数据报表
                //sqlStr = new StringBuilder();
                //sqlStr.Append("select SUM(TotalAmt) as BetMoney,SUM(TotalAmt-WinMoney) as ProfitAndLossMoney from BetDetails ");
                //sqlStr.Append("where CONVERT(varchar(10),OccDate,120)=convert(varchar(10),dateadd(dd,-1,getdate()),120) ");
                //sqlStr.Append("group by CONVERT(varchar(10),OccDate,120)");
                //YesterDayData yesterDayData = this.GetSqlSource<YesterDayData>(sqlStr.ToString()).FirstOrDefault();
                //if (yesterDayData != null)
                //    countData.yesterDayData = yesterDayData;

            }
            catch (Exception ex)
            {
                log.Error("GetCountData", ex);
            }
            return countData;
        }

       /// <summary>
       /// 按日统计报表
       /// </summary>
       /// <param name="beginDate"></param>
       /// <param name="endDate"></param>
       /// <returns></returns>
        public List<Sp_DayJieSuanTable> FilterSp_DayJieSuanTable(DateTime beginDate, DateTime endDate)
        {
            string procName = "sp_TianJieSuanTable";
            System.Data.SqlClient.SqlParameter[] dbParams = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@beginDate",System.Data.SqlDbType.DateTime),
                new System.Data.SqlClient.SqlParameter("@endDate",System.Data.SqlDbType.DateTime)
            };
            dbParams[0].Value = beginDate;
            dbParams[1].Value = endDate;
            return this.ExProc<Sp_DayJieSuanTable>(procName, dbParams);
        }


        /// <summary>
        /// 按日统计报表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Sp_YueJieSuanTable> FilterSp_MonthJieSuanTable(DateTime beginDate, DateTime endDate)
        {
            string procName = "sp_YueJieSuanTable";
            System.Data.SqlClient.SqlParameter[] dbParams = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@beginDate",System.Data.SqlDbType.DateTime),
                new System.Data.SqlClient.SqlParameter("@endDate",System.Data.SqlDbType.DateTime)
            };
            dbParams[0].Value = beginDate;
            dbParams[1].Value = endDate;
            return this.ExProc<Sp_YueJieSuanTable>(procName, dbParams);
        }

        /// <summary>
        /// 统计报表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<StatDto> FilterSp_userBannerTypeSuanTable(DateTime beginDate, DateTime endDate, string tradeType, int tableType)
        {
            string procName = "sp_userBannerTypeSuanTable";
            System.Data.SqlClient.SqlParameter[] dbParams = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@beginDate",System.Data.SqlDbType.DateTime),
                new System.Data.SqlClient.SqlParameter("@endDate",System.Data.SqlDbType.DateTime),
                new System.Data.SqlClient.SqlParameter("@tradeType",System.Data.SqlDbType.NVarChar),
                new System.Data.SqlClient.SqlParameter("@tableType",System.Data.SqlDbType.Int),
            };
            dbParams[0].Value = beginDate;
            dbParams[1].Value = endDate;
            dbParams[2].Value = tradeType;
            dbParams[3].Value = tableType;
            return this.ExProc<StatDto>(procName, dbParams);
        }


        /// <summary>
        /// 可清理数据统计
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<ClearDateDTO> GetClearDataStat()
        {
            string procName = "sp_StatClearData";
            System.Data.SqlClient.SqlParameter[] dbParams = new System.Data.SqlClient.SqlParameter[] { };
            return this.ExProc<ClearDateDTO>(procName, dbParams);
        }


        /// <summary>
        /// 清理数据
        /// </summary>
        /// <param name="type">清理类型</param>
        /// <param name="minMonery">最低金额，清理用户数据</param>
        /// <param name="recDay">保留天数</param>
        public void ClearData(int type, decimal minMonery, int recDay)
        {
            string procName = "sp_cleardata";
            System.Data.SqlClient.SqlParameter[] dbParams = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@type",System.Data.SqlDbType.Int),
                new System.Data.SqlClient.SqlParameter("@minMonery",System.Data.SqlDbType.Decimal),
                new System.Data.SqlClient.SqlParameter("@recDay",System.Data.SqlDbType.Int)
            };
            dbParams[0].Value = type;
            dbParams[1].Value = minMonery;
            dbParams[2].Value = recDay;
            this.ExProcNoReader(procName, dbParams);
        }
    }
}
