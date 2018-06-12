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
using Ytg.Core.Service.Lott;
using Ytg.Data;

namespace Ytg.Service.Lott
{
     [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LotteryIssueService : CrudService<LotteryIssue>, ILotteryIssueService
    {
        public LotteryIssueService(IRepo<LotteryIssue> repo)
            : base(repo)
        {

        }

         /// <summary>
         /// 生成彩票期数
         /// </summary>
         /// <param name="?"></param>
        public void AddLotteryIssueCode(LotteryIssue issue)
        {

            string spName = "AddLotteryIssueCode";
            /*
               @IssueCode nvarchar(100),--期数编号
  @LotteryId int,--彩票id
  @StartTime datetime,--开始时间
  @EndTime datetime,--结束时间
  @StartSaleTime datetime,--开始销售时间
  @EndSaleTime datetime,--结束销售时间
  @LotteryTime datetime,--彩票时间
             */

            DbParameter[] pramers = new DbParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@IssueCode",SqlDbType.NVarChar),
                    new System.Data.SqlClient.SqlParameter("@LotteryId",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@StartTime",SqlDbType.DateTime),
                    new System.Data.SqlClient.SqlParameter("@EndTime",SqlDbType.DateTime),
                    new System.Data.SqlClient.SqlParameter("@StartSaleTime",SqlDbType.DateTime),
                    new System.Data.SqlClient.SqlParameter("@EndSaleTime",SqlDbType.DateTime),
                    new System.Data.SqlClient.SqlParameter("@LotteryTime",SqlDbType.DateTime),
            };
            pramers[0].Value = issue.IssueCode;
            pramers[1].Value = issue.LotteryId;
            pramers[2].Value = issue.StartTime;
            pramers[3].Value = issue.EndTime;
            pramers[4].Value = issue.StartSaleTime;
            pramers[5].Value = issue.EndSaleTime;
            pramers[6].Value = issue.LotteryTime;
            this.ExProcNoReader(spName, pramers);

        }

        /// <summary>
        /// 手动开奖
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <returns></returns>
        public void ManualOpen(string lotteryCode, string issueCode, string openResult)
        {
            //参数设置
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@lotteryCode",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@issueCode",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@openResult",System.Data.DbType.String)
            };
            parameters[0].Value = lotteryCode;
            parameters[1].Value = issueCode;
            parameters[2].Value = openResult;
            this.ExProc<Object>("SP_ManualOpen", parameters);
        }

         /// <summary>
         /// 修改开奖结果
         /// </summary>
         /// <param name="expect"></param>
         /// <param name="openCode"></param>
         /// <param name="lotteryid"></param>
         /// <returns></returns>
        public bool UpdateOpenResult(string expect, string openCode, int lotteryid)
        {
            string spName = "sp_UpdateOpenResult1";
            /*
               @IssueCode nvarchar(100),--开奖期数
            @LotteryId int,--彩种id
            @Opencode nvarchar(100),--开奖结果
            @Opentime dateTime --开奖时间
                         */

            DbParameter[] pramers = new DbParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@IssueCode",SqlDbType.NVarChar),
                    new System.Data.SqlClient.SqlParameter("@LotteryId",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@Opencode",SqlDbType.NVarChar)
            };
            pramers[0].Value = expect;
            pramers[1].Value = lotteryid;
            pramers[2].Value = openCode;
            this.ExProcNoReader(spName, pramers);
            return true;
        }

        public bool UpdateOpenResult(string expect, string openCode,DateTime openTime, int lotteryid)
        {
            string spName = "sp_UpdateOpenResult";
            /*
               @IssueCode nvarchar(100),--开奖期数
            @LotteryId int,--彩种id
            @Opencode nvarchar(100),--开奖结果
            @Opentime dateTime --开奖时间
                         */

            DbParameter[] pramers = new DbParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@IssueCode",SqlDbType.NVarChar),
                    new System.Data.SqlClient.SqlParameter("@LotteryId",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@Opencode",SqlDbType.NVarChar),
                    new System.Data.SqlClient.SqlParameter("@Opentime",SqlDbType.Date)
            };
            pramers[0].Value = expect;
            pramers[1].Value = lotteryid;
            pramers[2].Value = openCode;
            pramers[3].Value = openTime;
            this.ExProcNoReader(spName, pramers);
            return true;
        }

         /// <summary>
         /// 获取历史开奖结果
         /// </summary>
         /// <returns></returns>
        public List<LotteryIssue> GetHisIssue(int lotteryid, int topValue, DateTime? begin, DateTime? end)
        {
            string sql = "select top "+topValue+" * from [dbo].[LotteryIssues] where lotteryid=" + lotteryid;
            if (begin != null && end != null)
            {
                sql += " and EndTime between '" + begin + "' and '" + end + "'";
            }
            else
            {
                sql += " and EndTime<GETDATE() ";
            }
            sql += " order by EndTime DESC";
                
            return this.mRepo.GetSqlSource<LotteryIssue>(sql);
        }

         /// <summary>
         /// 获取最后一条期数数据
         /// </summary>
         /// <param name="lotteryid"></param>
         /// <returns></returns>
        public LotteryIssue GetLastIssue(int lotteryid)
        {
            return this.Where(x => x.LotteryId == lotteryid).OrderByDescending(c => c.EndTime).FirstOrDefault();
        }

         /// <summary>
         /// 根据期数获取期数信息
         /// </summary>
         /// <param name="issueCode"></param>
         /// <returns></returns>
        public LotteryIssue Get(string issueCode)
        {
            return this.GetAll().Where(x => x.IssueCode == issueCode).FirstOrDefault();
        }

         /// <summary>
        /// 获取当前50分钟内未开奖的期数    int type = groupName == "openresult_task" ? 0 : 1;//0为普通 1 为一分彩盒2两分
         /// </summary>
         /// <returns></returns>
        public List<int> GetNotOpenIssues(string notLotteryIds)
        {
            
            string sql = "select LotteryId from [dbo].[LotteryIssues]  where EndTime between  dateadd(mi,-300,getdate()) and getdate() and Result is null and lotteryid not in(" + notLotteryIds + ") group by LotteryId";
            return this.mRepo.GetSqlSource<int>(sql);
        }

        /// <summary>
        /// 获取当前50分钟内未开奖的期数
        /// </summary>
        /// <returns></returns>
        public List<int> GetInIssues(string lotteryIds)
        {

            string sql = "select LotteryId from [dbo].[LotteryIssues]  where EndTime between  dateadd(mi,-300,getdate()) and getdate() and Result is null and lotteryid in(" + lotteryIds + ") group by LotteryId";
            return this.mRepo.GetSqlSource<int>(sql);
        }

        // /// <summary>
        // /// 获取当前时间前4分钟待开奖期数
        // /// </summary>
        // /// <param name="notLotteryId"></param>
        // /// <returns></returns>
        //public IEnumerable<LotteryIssue> GetNowOpenIssues(string notLotteryIds)
        //{
        //    string sql = "select * from [dbo].[LotteryIssues] where EndTime between  dateadd(mi,-4,getdate()) and getdate() and Result is null and lotteryid not in(" + notLotteryIds + ")";

        //   // DateTime beginDate = DateTime.Now.AddMinutes(-4);
        //    //DateTime endDate = DateTime.Now;

        //    //var items = this.GetNoTracking();//.Where(m => m.LotteryId == lotteryId);
        //    //items = items.Where(m =>!notLotteryIdArray.Contains(m.LotteryId.Value) && m.EndTime >=beginDate && m.EndTime<=endDate && string.IsNullOrEmpty(m.Result));

        //    //return items;
        //    return this.mRepo.GetSqlSource<LotteryIssue>(sql);
        //}


        public IEnumerable<LotteryIssue> GetLotteryIssues(int lotteryId, DateTime? beginDate, DateTime? endDate, int topCount = -1)
        {
            //if (lotteryId == 0)
            //    return null;
            //var items = this.GetNoTracking();//.Where(m => m.LotteryId == lotteryId);
            //items = items.Where(m => m.LotteryId == lotteryId);
            //if (beginDate != null && endDate != null)
            //    items = items.Where(m => m.LotteryTime >= beginDate && m.LotteryTime <= endDate);

            //return items;

            string sql = " * from lotteryIssues  where LotteryId=" + lotteryId;
            if (beginDate != null && endDate != null)
            {
                sql += " and LotteryTime between '" + beginDate + "' and '" + endDate + "'";
            }
            if (topCount != -1)
            {
                sql = "select top " + topCount + " " + sql;
            }
            else
            {
                sql = "select " + sql;
            }

            return this.GetSqlSource<LotteryIssue>(sql);
        }

        public bool CreateAllLotteryIssue()
        {
            this.ExProc("sp_LotteryIssue_Create", null);
            return true;
        }


        /// <summary>
        /// 获取当前未开奖的期数，如遇官方未开奖，则过滤掉
        /// </summary>
        /// <param name="lotteryId"></param>
        /// <returns></returns>
        public List<BasicModel.LotteryBasic.DTO.LotteryIssueDTO> GetOccDayNoOpenLotteryIssue(int lotteryId)
        {
            //查询当前所有未开奖信息
            var nowTime = DateTime.Now;
            var endTime = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd 00:00:59");
            if (lotteryId == 7 || lotteryId == 9)
                endTime = DateTime.Now.AddYears(2).ToString("yyyy/MM/dd 00:00:59");
            else if (lotteryId == 21) { 
               //lhc ds
                endTime = DateTime.Now.AddYears(2).ToString("yyyy/MM/dd 23:59:59");
            }
            else
            {
                string times = LotteryIssueTimerHelper.GetTimes(lotteryId);
                if (string.IsNullOrEmpty(times))
                    endTime = DateTime.Now.AddYears(2).ToString("yyyy/MM/dd " + times);
                else
                    endTime = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd " + times);
            }
            //else if(lotteryId==8)//上海时时乐
            //    endTime = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd 10:31:59");
            //else if (lotteryId == 6)//广东11选5 
            //{
            //    endTime = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd 09:11:50");
            //}
            //else if (lotteryId == 5)//天津时时彩
            //{
            //    endTime = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd 09:13:41");
            //}
            //else if (lotteryId == 22) //k3
            //{
            //    endTime = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd 08:40:40");
            //}
            //else if (lotteryId == 20)//江西11选5
            //{
            //    endTime = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd 09:10:50");
            //}
            //else if (lotteryId == 20)//江西11选5
            //{
            //    endTime = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd 09:10:50");
            //}
            //else if (lotteryId == 19)//山东11选5
            //{
            //    endTime = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd 09:04:40");
            //}
            //else if (lotteryId == 4) //新疆时时彩
            //{
            //    endTime = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd 10:10:50");
            //}
            //else if (lotteryId == 1) {//重庆 
            //    endTime = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd 09:59:59");
            //}

            string sql = string.Format(" select EndSaleTime,EndTime,IssueCode,Result,1 as tp from (select top 5 * from LotteryIssues where LotteryId={0} and  EndSaleTime<'{1}' order by EndTime desc) as t1 " +
                " union all " +
                " select EndSaleTime,EndTime,IssueCode,Result,0 as tp from LotteryIssues where LotteryId={0} and  EndSaleTime between '{1}' and '{2}' order by EndTime", lotteryId, nowTime, endTime);
            return this.mRepo.GetSqlSource<BasicModel.LotteryBasic.DTO.LotteryIssueDTO>(sql);
        }

        public LotteryIssue GetNowSalesIssue(int lotteryId)
        {
            return this.Where(c => c.LotteryId == lotteryId && c.StartSaleTime > DateTime.Now && c.Result==null).OrderBy(c => c.EndTime).FirstOrDefault();
        }

        public LotteryIssueDTO GetIssueOpenResult(string issue,int lotteryId)
        {

            string sql = string.Format(@"SELECT [Extent1].[Id] AS [Id], 
    [Extent1].[IssueCode] AS [IssueCode], 
    [Extent1].[LotteryId] AS [LotteryId], 
    [Extent1].[StartTime] AS [StartTime], 
    [Extent1].[EndTime] AS [EndTime], 
    [Extent1].[StartSaleTime] AS [StartSaleTime], 
    [Extent1].[EndSaleTime] AS [EndSaleTime], 
    [Extent1].[Result] AS [Result], 
    [Extent1].[LotteryTime] AS [LotteryTime], 
    [Extent1].[Remark] AS [Remark], 
    [Extent1].[OccDate] AS [OccDate]
    FROM [dbo].[LotteryIssues] AS [Extent1] WHERE [Extent1].IssueCode='{0}' and [Extent1].Result IS NOT NULL and [Extent1].[LotteryId]={1}", issue, lotteryId);

            return this.GetSqlSource<LotteryIssueDTO>(sql).FirstOrDefault();
        }

        

        /// <summary>
        /// 获取当天所有对象
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LotteryIssue> GetNowDayIssue()
        {
            DateTime beginDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime endDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

            return this.Where(item => item.StartTime >= beginDate && item.EndTime <= endDate);
        }

         /// <summary>
        ///  获取当天所有对象，根据彩种
         /// </summary>
         /// <param name="lotteryId"></param>
         /// <returns></returns>
        public IEnumerable<LotteryIssue> GetNowDayIssue(int lotteryId)
        {
            DateTime beginDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime endDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

            return this.Where(item => item.StartTime >= beginDate && item.EndTime <= endDate && item.LotteryId == lotteryId);
        }


         /// <summary>
         /// 获取指定菜种当天所有期数
         /// </summary>
         /// <param name="lotteryid"></param>
         /// <returns></returns>
        public IEnumerable<LotteryIssue> GetNowDayLotteryTypeIssue(int lotteryid)
        {

            DateTime beginDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime endDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            return this.Where(item => item.StartTime >= beginDate && item.EndTime <= endDate && item.LotteryId==lotteryid);
        }

         
       
        public void BuilderNextDayIssues()
        {

            DbParameter[] parameters = new DbParameter[]{
               new DbContextFactory().GetDbParameter("@i",System.Data.DbType.Int32)   
            };

            parameters[0].Value = 1;

            this.mRepo.ExProc("sp_LotteryIssue_Create", parameters);
        }

        /// <summary>
        /// 获取当前已开奖的前11条记录
        /// </summary>
        /// <returns></returns>
        public List<LotteryIssue> GetTopOpendIssue(int lotteryid)
        {
            string sql = string.Format("select  top 11 * from LotteryIssues where Result is not null and LotteryId={0} order by EndTime desc", lotteryid);
            return this.GetSqlSource<LotteryIssue>(sql);
        }

         /// <summary>
         /// 获取当前5条记录
         /// </summary>
         /// <param name="lotteryid"></param>
         /// <returns></returns>
        public List<LotteryIssue> GetTop5OpendIssue(int lotteryid)
        {
            string sql = string.Format("select  top 15 * from LotteryIssues where Result is not null and LotteryId={0} order by EndTime desc", lotteryid);
            return this.GetSqlSource<LotteryIssue>(sql);
        }

        /// <summary>
        /// 获取当前5条记录
        /// </summary>
        /// <param name="lotteryid"></param>
        /// <returns></returns>
        public List<LotteryIssue> GetTop50OpendIssue(int lotteryid)
        {
            string sql = string.Format("select  top 50 * from LotteryIssues where Result is not null and LotteryId={0} order by EndTime desc", lotteryid);
            return this.GetSqlSource<LotteryIssue>(sql);
        }


        /// <summary>
        /// 获取当前100条记录
        /// </summary>
        /// <param name="lotteryid"></param>
        /// <returns></returns>
        public List<LotteryIssue> GetTop100OpendIssue(int lotteryid)
        {
            string sql = string.Format("select  top 100 * from LotteryIssues where Result is not null and LotteryId={0} order by EndTime desc", lotteryid);
            return this.GetSqlSource<LotteryIssue>(sql);
        }


        public bool UpdateLottIssue(LotteryIssue item)
        {
            var c= this.Get(item.Id);
            if (null == c)
                return false;

            c.IsEnable = item.IsEnable;
            c.StartTime = item.StartTime;
            c.EndTime = item.EndTime;
            item.LotteryTime = item.LotteryTime;
            this.Save();
            return true;
        }

         /// <summary>
         /// 修改彩种期号时间
         /// </summary>
         /// <param name="item"></param>
         /// <returns></returns>
        public bool UpdateLotteryIssueTime(LotteryIssue item)
        {
            var c = this.Get(item.Id);
            if (null == c)
                return false;
            c.StartTime = item.StartTime;
            c.EndTime = item.EndTime;
            item.LotteryTime = item.LotteryTime;
            this.Save();
            return true;
        }

        /// <summary>
        /// 根据彩种和期号获取期号信息
        /// </summary>
        /// <param name="lotteryid"></param>
        /// <param name="issueCode"></param>
        /// <returns></returns>
        public LotteryIssue Get(int lotteryid, string issueCode)
        {
            return this.GetAll().Where(x => x.IssueCode == issueCode && x.LotteryId == lotteryid).FirstOrDefault();
        }

         /// <summary>
         /// 清除期数
         /// </summary>
         /// <param name="lotteryid"></param>
        public void ClearIssues(int lotteryid)
        {
            var s=this.GetAll().Where(x=>x.LotteryId==lotteryid);
            foreach (var x in s)
                this.Delete(x);
            this.Save();
        }

         /// <summary>
         /// 根据彩种id,行数获取开奖结果
         /// </summary>
         /// <param name="lotteryId"></param>
         /// <param name="rows"></param>
         /// <returns></returns>
        public List<IssueResultDTO> FilterResult(int lotteryId, int rows)
        {
            string sql = "select top " + rows + " IssueCode as datesn,(REPLACE(ISNULL(Result,''),',','')) as code from LotteryIssues where LotteryId=" + lotteryId + " and Result is not null and Result<>'' order by OccDate desc ";
            return this.GetSqlSource<IssueResultDTO>(sql);
        }

         /// <summary>
         /// 根据彩票id获取期数信息
         /// </summary>
         /// <param name="lotteryid"></param>
         /// <returns></returns>
        public Ytg.BasicModel.DTO.Server_IssueResultDTO Server_TopIssueResult(int lotteryid)
        {
            string sql = "select IssueCode as datesn,EndTime as code from LotteryIssues where LotteryId=" + lotteryid + " and EndTime >= GETDATE() order by EndTime";
            return this.GetSqlSource<Ytg.BasicModel.DTO.Server_IssueResultDTO>(sql).FirstOrDefault();
        }


         /// <summary>
         /// 设置开奖结果为null
         /// </summary>
         /// <param name="lotteryid"></param>
         /// <param name="issueCode"></param>
         /// <returns></returns>
        public bool SetResultNull(int lotteryid, string issueCode)
        {
            DbParameter[] pramers = new DbParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@IssueCode",SqlDbType.NVarChar),
                    new System.Data.SqlClient.SqlParameter("@LotteryId",SqlDbType.Int),
            };
            pramers[0].Value = issueCode;
            pramers[1].Value = lotteryid;
            string sql = "update LotteryIssues set result=null where IssueCode=@IssueCode and  LotteryId=@lotteryid";
            return this.mRepo.GetDbContext.Database.ExecuteSqlCommand(sql, pramers) > 0;
        }


         /// <summary>
         /// 获取期数
         /// </summary>
         /// <param name="lotteryid"></param>
         /// <param name="issueCode"></param>
         /// <returns></returns>
        public LotteryIssue SqlGetIssueCode(int lotteryid, string issueCode)
        {
            string sql = "select * from LotteryIssues  where LotteryId="+lotteryid+" and IssueCode='"+issueCode+"'";
            return this.mRepo.GetSqlSource<LotteryIssue>(sql).FirstOrDefault();
        }

        /**
         获取所有彩种的开奖记录
             */
        public List<LotteryIssueDTO_Opens> GetAllLotteryOpens()
        {
            string sql = @"select t.* from 
(select *, row_number() over(partition by lotteryid order by EndSaleTime desc) rn
from vw_lotteryGroup
) t
where rn = 1";

            return this.mRepo.GetSqlSource<LotteryIssueDTO_Opens>(sql);
        }


        /// <summary>
        /// 获取开奖信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<LotteryIssueDTO_Opens> GetNextPreds(string ids)
        {
            string sql = @"select t.* from 
(select *, row_number() over(partition by lotteryid order by EndSaleTime asc) rn
from vw_lotteryGroup_Two
) t
where rn in(1,2)  and lotteryid in(" + ids + ")";
            return this.mRepo.GetSqlSource<LotteryIssueDTO_Opens>(sql);

        }
        

      
        ///// <summary>
        ///// 方法说明: 获取期数投注数据
        ///// 创建时间：2015-05-23
        ///// 创建者：GP
        ///// </summary>
        ///// <param name="lotteryCode"></param>
        ///// <param name="issueCode"></param>
        ///// <param name="date"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="totalCount"></param>
        ///// <returns></returns>
        //public List<LotteryResultDTO> GetLotteryIssueResult(string lotteryCode, string issueCode, DateTime date, int pageIndex, int pageSize, ref int totalCount)
        //{
        //    //参数设置
        //    DbParameter[] parameters = new DbParameter[]{
        //        new DbContextFactory().GetDbParameter("@lotteryCode",System.Data.DbType.String),
        //        new DbContextFactory().GetDbParameter("@issueCode",System.Data.DbType.String),
        //        new DbContextFactory().GetDbParameter("@date",System.Data.DbType.DateTime),
        //        new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
        //        new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32),
        //        new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output)
        //    };

        //    parameters[0].Value = lotteryCode;
        //    parameters[1].Value = issueCode;
        //    parameters[2].Value = date;
        //    parameters[3].Value = pageIndex;
        //    parameters[4].Value = pageSize;

        //    List<LotteryResultDTO> list = this.ExProc<LotteryResultDTO>("sp_GetLotteryIssueResult", parameters);
        //    totalCount = Convert.ToInt32(parameters[5].Value);

        //    return list;
        //}
    }
}
