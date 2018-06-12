using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Core.Repository;
using Ytg.Core.Service.Lott;

namespace Ytg.Service.Lott
{
    /// <summary>
    /// 投注明细
    /// </summary>
    public class SysCatchNumIssueService : CrudService<CatchNumIssue>, ISysCatchNumIssueService
    {
        public SysCatchNumIssueService(IRepo<CatchNumIssue> repo)
            : base(repo)
        {

        }

        
        /// <summary>
        ///  获取当前期号后的所有期号
        /// </summary>
        /// <param name="catchNumCode"></param>
        /// <param name="issueCode"></param>
        /// <returns></returns>
        public List<CatchNumIssue> GetLastCatchNum(string catchNumCode, string issueCode)
        {
           // string sql = string.Format("select * from [CatchNumIssues] where issueCode>{0} and CatchNumCode='{1}' and IsMatch=0", issueCode, catchNumCode);
            //return this.GetSqlSource<CatchNumIssue>(sql);
            return this.Where(c => c.CatchNumCode == catchNumCode && c.Stauts== BasicModel.BetResultType.NotOpen).ToList();//获取未开奖的期数
        }


        /// <summary>
        /// 获取投注详细
        /// </summary>
        /// <param name="catchCode"></param>
        /// <returns></returns>
        public List<CatchNumIssue> GetCatchIssue(string catchCode)
        {
            return this.Where(item => item.CatchNumCode == catchCode).ToList();
        }


        /// <summary>
        /// 修改期数未开奖数据为当前时间
        /// </summary>
        /// <param name="catchNumCode"></param>
        public void UpdateNoOpenOccDateTime(string catchNumCode)
        {
            string sql = "update CatchNumIssues set OccDate=GETDATE() where CatchNumCode=@catchNumCode and Stauts in(3,4)";

            System.Data.SqlClient.SqlParameter[] paramerter = new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@CatchNumCode",System.Data.SqlDbType.NVarChar)
            };
            paramerter[0].Value = catchNumCode;
            this.mRepo.GetDbContext.Database.ExecuteSqlCommand(sql, paramerter);
        }


        /// <summary>
        /// 根据追号单号和开奖期数获取详情
        /// </summary>
        /// <param name="catchCode"></param>
        /// <param name="issueCode"></param>
        public BetList GetCatchIssueDetail(string catchCode, string issueCode)
        {
            string sql = string.Format(@"select 
              cn.PostionName,ci.Id,ci.IssueCode,ci.CatchNumCode as BetCode,cn.BetCount,ci.TotalAmt,
              ci.Multiple,cn.Model,cn.PrizeType,cn.BackNum,cn.BetContent,ci.OccDate,
              ci.WinMoney,ci.OpenResult,ci.Stauts,u.Code,lv.PlayTypeRadioName,cn.PalyRadioCode as RadioCode,
              lv.PlayTypeNumName,lv.PlayTypeName,lv.LotteryName
             from CatchNumIssues as ci
            inner join CatchNums as cn on ci.CatchNumCode=cn.CatchNumCode
            inner join SysYtgUser as u on u.id=cn.UserId
            inner join Lottery_Vw as lv on lv.RadioCode=cn.PalyRadioCode
            where ci.CatchNumCode='{0}' and ci.IssueCode={1}", catchCode, issueCode);

            return this.GetSqlSource<BetList>(sql).FirstOrDefault();
        }


        public void AddCatchIssue(CatchNumIssue issue, int lotteryid)
        {
            string spName = "sp_CreateCatchIssue";
            /*
               @CatchNumIssueCode nvarchar(100),--编号
  @CatchNumCode nvarchar(100),--追号编号
  @IssueCode nvarchar(100),--期号
  @lotteryid int,--彩种
  @Multiple int,--倍数
  @TotalAmt decimal(18, 4)--总金额
             */

            DbParameter[] pramers = new DbParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@CatchNumIssueCode",SqlDbType.NVarChar),
                    new System.Data.SqlClient.SqlParameter("@CatchNumCode",SqlDbType.NVarChar),
                    new System.Data.SqlClient.SqlParameter("@IssueCode",SqlDbType.NVarChar),
                    new System.Data.SqlClient.SqlParameter("@lotteryid",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@Multiple",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@TotalAmt",SqlDbType.Decimal),
            };
            pramers[0].Value = issue.CatchNumIssueCode;
            pramers[1].Value = issue.CatchNumCode;
            pramers[2].Value = issue.IssueCode;
            pramers[3].Value = lotteryid;
            pramers[4].Value = issue.Multiple;
            pramers[5].Value = issue.TotalAmt;
            this.ExProcNoReader(spName, pramers);
        }



        /// <summary>
        /// 追号  存储过程 
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="balanceDetail"></param>
        /// <returns></returns>
        public decimal? AddCatchBetting(CatchNum detail, SysUserBalanceDetail balanceDetail, int lotteryid, string issueStr, decimal detailMonery, ref int state)
        {
            string spName = "sp_catchBetting";
            DbParameter[] pramers = new DbParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@catchNumCode",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@BetContent",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@Model",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@PalyRadioCode",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@PrizeType",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@BackNum",SqlDbType.Decimal),
                    new System.Data.SqlClient.SqlParameter("@UserId",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@IsAutoStop",SqlDbType.Bit),
                    new System.Data.SqlClient.SqlParameter("@SumAmt",SqlDbType.Decimal),
                    new System.Data.SqlClient.SqlParameter("@BeginIssueCode",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@CatchIssue",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@BonusLevel",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@LotteryCode",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@BetCount",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@PostionName",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@HasState",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@detailMonery",SqlDbType.Decimal),
                    new System.Data.SqlClient.SqlParameter("@lotteryid",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@TradeType",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@SerialNo",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@catchIssueContent",SqlDbType.NVarChar),
                     new System.Data.SqlClient.SqlParameter("@state",SqlDbType.Int)
            };
            pramers[0].Value = detail.CatchNumCode;
            pramers[1].Value = detail.BetContent;
            pramers[2].Value = detail.Model;
            pramers[3].Value = detail.PalyRadioCode;
            pramers[4].Value = detail.PrizeType;
            pramers[5].Value = detail.BackNum;
            pramers[6].Value = detail.UserId;
            pramers[7].Value = detail.IsAutoStop;
            pramers[8].Value = detail.SumAmt;
            pramers[9].Value = detail.BeginIssueCode;
            pramers[10].Value = detail.CatchIssue;
            pramers[11].Value = detail.BonusLevel;
            pramers[12].Value = detail.LotteryCode;
            pramers[13].Value = detail.BetCount;
            pramers[14].Value = string.IsNullOrEmpty(detail.PostionName) ? "" : detail.PostionName;
            pramers[15].Value = detail.HasState;
            pramers[16].Value = detailMonery;
            pramers[17].Value = lotteryid;
            pramers[18].Value = balanceDetail.TradeType;
            pramers[19].Value = balanceDetail.SerialNo;
            pramers[20].Value = issueStr;
            pramers[21].Direction = ParameterDirection.Output;

            this.ExProcNoReader(spName, pramers);
            object parenter = pramers[21].Value;
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
        /// 根据追号单，获取彩票id
        /// </summary>
        /// <param name="catchCode"></param>
        /// <returns></returns>
        public int? GetLotteryId(string catchCode)
        {
            string sql = "select id from LotteryTypes where LotteryCode in(select LotteryCode from CatchNums where CatchNumCode='" + catchCode + "')";
            return this.GetSqlSource<int?>(sql).FirstOrDefault();
        }
    }
}
