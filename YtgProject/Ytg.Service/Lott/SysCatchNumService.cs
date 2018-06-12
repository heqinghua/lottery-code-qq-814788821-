using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.LotteryBasic;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service.Lott;
using Ytg.Data;

namespace Ytg.Service.Lott
{
    /// <summary>
    /// 追号数据服务对象
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SysCatchNumService : CrudService<CatchNum>, ISysCatchNumService
    {
        public SysCatchNumService(IRepo<CatchNum> repo)
            : base(repo)
        {

        }

        /// <summary>
        /// 查询追号记录
        /// </summary>
        /// <param name="paramer">参数</param>
        public List<CatchNumJsonDTO> FilterCatchNumList(int userId, FilterCatchNumListParamerDTO paramer, int pageIndex, ref int totalCount)
        {
            #region old code
            //StringBuilder sqlWhere = new StringBuilder();
            //if (paramer.BeginTime != null)
            //    sqlWhere.Append(string.Format(" and cn.OccDate between '{0}' and '{1}'", paramer.BeginTime, paramer.EndTime));//时间

            //if (!string.IsNullOrEmpty(paramer.CatchNumCode))
            //    sqlWhere.Append(string.Format(" and cn.CatchNumCode='{0}'", paramer.CatchNumCode));//投注编号
            //if (!string.IsNullOrEmpty(paramer.IssueCode))
            //    sqlWhere.Append(string.Format(" and cn.BeginIssueCode like '%{0}%'", paramer.IssueCode));
            //if (!string.IsNullOrEmpty(paramer.LotteryCode))
            //    sqlWhere.Append(string.Format(" and lv.LotteryCode='{0}'", paramer.LotteryCode));
            //if (paramer.Mode != -1)
            //    sqlWhere.Append(string.Format(" and cn.Model={0}", paramer.Mode));
            //if (paramer.PalyRadioCode != -1)
            //    sqlWhere.Append(string.Format(" and lv.RadioCode={0}", paramer.PalyRadioCode));
            //if (!string.IsNullOrEmpty(paramer.PalyUserCode))
            //    sqlWhere.Append(string.Format(" and ytguser.Code like '%{0}%'", paramer.PalyUserCode));
            //if (paramer.State != -1)
            //    sqlWhere.Append(string.Format(" and cn.Stauts={0}", paramer.State));

            //string procName = "sp_CatchNumList";//存储过程名称
            //var fc = IoC.Resolve<IDbContextFactory>();
            //var p = fc.GetDbParameter("@Where", System.Data.DbType.String);
            //p.Value = sqlWhere.ToString();

            //var p1 = fc.GetDbParameter("@loginId", System.Data.DbType.Int32);
            //p1.Value = loginUid;

            //var p2 = fc.GetDbParameter("@UserScope", System.Data.DbType.Int32);
            //p2.Value = paramer.UserScope;

            //return this.ExProc<CatchNumJsonDTO>(procName, p, p1, p2);
            #endregion
            int pageSize = Ytg.Comm.AppGlobal.ManagerDataPageSize;

            string sql = @"select  b.*,u.Code ,lv.PlayTypeRadioName ,lv.RadioCode,lv.PlayTypeNumName ,lv.PlayTypeName ,lv.LotteryName
 from CatchNums as b
 LEFT JOIN SysYtgUser as u ON u.Id = b.UserId
 LEFT JOIN Lottery_Vw as lv on lv.RadioCode= b.PalyRadioCode where 1=1 ";
            //order by b.OccDate desc

            //tradeType INT=1,--交易类型，当天1 历史记录2
            //@userType INT,-- 用户类型 -1所有 1 自己 2 直接下级 3 所有下级

            string userWhere = "";
            string putWhere = "";
            switch (paramer.UserScope)
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
                    putWhere = string.Format(" or  b.UserId={0}", userId);
                    break;
            }

            //账号
            if (!string.IsNullOrEmpty(paramer.PalyUserCode))
                sql += string.Format(" and u.Code ='{0}'", Utils.ChkSQL(paramer.PalyUserCode));
            else
                sql += string.Format(" and (b.UserId in ({0}) {1})", userWhere, putWhere);

            string beginDate = Utils.GetNowBeginDate().ToString("yyyy/MM/dd HH:mm:ss");
            string endDate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd HH:mm:ss");
            if (null != paramer.BeginTime && null != paramer.EndTime)
            {
                beginDate = Utils.GetNowBeginDate().ToString("yyyy/MM/dd ")+paramer.BeginTime.Value.ToString("HH:mm:ss");
                endDate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd ") + paramer.EndTime.Value.ToString("HH:mm:ss");
            }
            if (paramer.tradeType == 2)
            {
                beginDate = paramer.BeginTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                endDate = paramer.EndTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
            }
            sql += string.Format(" and b.OccDate between '{0}' and '{1}'", beginDate, endDate);
            //状态 -1 全部 1 已中奖、2 未中奖、3 未开奖、4 已撤单
            if (paramer.State!= -1)
                sql += string.Format(" and b.Stauts={0}", paramer.State);
            //采种
            if (!string.IsNullOrEmpty(paramer.LotteryCode))
                sql += string.Format(" and lv.LotteryCode='{0}'", Utils.ChkSQL(paramer.LotteryCode));
            //玩法
            if (paramer.PalyRadioCode != -1)
                sql += string.Format(" and b.PalyRadioCode={0}", paramer.PalyRadioCode);
            //期数
            if (!string.IsNullOrEmpty(paramer.IssueCode))
                sql += string.Format(" and b.BeginIssueCode='{0}'", Utils.ChkSQL(paramer.IssueCode));
            //所有模式-1 0元、1角、2分
            if (paramer.Mode != -1)
                sql += string.Format(" and b.Model={0}", paramer.Mode);
            //投注号
            if (!string.IsNullOrEmpty(paramer.CatchNumCode))
                sql += string.Format(" and b.CatchNumCode like '%{0}%'", Utils.ChkSQL(paramer.CatchNumCode));

            

            sql = "(" + sql + ") as t1";

            int pageCount = 0;
            return this.GetEntitysPage<CatchNumJsonDTO>(sql, "Id", "*", " OccDate desc", ESortType.DESC, pageIndex, pageSize, ref pageCount, ref totalCount);
        }


        /// <summary>
        /// 获取指定期号未完成的追号
        /// </summary>
        public List<NotCompledCatchNumListDTO> GetNotCompledCatchNumList(string lotteryCode,string issueCode)
        {
            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append("select cn.[Id]");
            sqlBuilder.Append(",cn.[CatchNumCode]");
            sqlBuilder.Append(",cn.[UserId]");
            sqlBuilder.Append(",cn.[BetCount]");
            sqlBuilder.Append(",cn.[PalyRadioCode]");
            sqlBuilder.Append(",cn.[SumAmt]");
            sqlBuilder.Append(",cn.[Model]");
            sqlBuilder.Append(",cn.[PrizeType]");
            sqlBuilder.Append(",cn.[BackNum]");
            sqlBuilder.Append(",cn.[BetContent]");
            sqlBuilder.Append(",cn.[Stauts]");
            sqlBuilder.Append(",cn.[BonusLevel]");
            sqlBuilder.Append(",cn.[IsAutoStop]");
            sqlBuilder.Append(",cn.[CompledIssue]");
            sqlBuilder.Append(",cn.[CompledMonery]");
            sqlBuilder.Append(",cn.[WinMoney]");
            sqlBuilder.Append(",cni.[Id] as CuiId");
            sqlBuilder.Append(",cni.[IssueCode]");
            sqlBuilder.Append(",cni.[Multiple]");
            sqlBuilder.Append(",cni.[TotalAmt]");
            sqlBuilder.Append(",cni.[Stauts] as CuiStauts");
            sqlBuilder.Append(",cni.[WinMoney] as CuiWinMoney");
            sqlBuilder.Append(",cni.[IsMatch]");
            sqlBuilder.Append(",cni.[OpenResult]");
            sqlBuilder.Append(" from CatchNums  as cn");
            sqlBuilder.Append(" inner join CatchNumIssues as cni on cn.CatchNumCode=cni.CatchNumCode");
            sqlBuilder.Append(" where ");
            // sqlBuilder.Append(" cn.Stauts=0 ");
            //sqlBuilder.Append(" and cni.Stauts=3 ");
            sqlBuilder.Append("  cni.Stauts=3 ");
            sqlBuilder.Append(string.Format(" and cni.IssueCode='{0}' and cn.LotteryCode='{1}'", issueCode,lotteryCode));


            return GetSqlSource<NotCompledCatchNumListDTO>(sqlBuilder.ToString());
        }


        public List<CatchNumsVM> SelectCatchNumList(string sDate, string eDate, string lotteryCode, string catchNumCode,
            string userCode, int pageIndex, ref int totalCount)
        {
            //            var sql = string.Format(@"WITH result AS(
            //	
            //),totalCount AS(
            //	SELECT COUNT(1) TotalCount FROM result
            //)
            //select * from (
            //	select *, ROW_NUMBER() OVER(Order by a.Id desc) AS RowNumber from result as a,totalCount
            //) as b
            //where RowNumber BETWEEN ({5}-1)*{6}+1 AND {5}*{6}", sDate, eDate, lotteryCode, catchNumCode, userCode, pageIndex, AppGlobal.ManagerDataPageSize);
            //            var list = this.GetSqlSource<CatchNumsVM>(sql);
            //            totalCount = list.Count > 0 ? list.FirstOrDefault().TotalCount : 0;

            //            return list;

            string sql = string.Format(@"select cn.*,lv.LotteryName,lv.PlayTypeRadioName,ytguser.Code,lv.PlayTypeNumName
from [CatchNums] as cn
inner join [Lottery_Vw] as lv on cn.PalyRadioCode=lv.RadioCode
inner join [SysYtgUser] as ytguser on ytguser.Id=cn.UserId
WHERE ('{0}'='' OR cn.OccDate>='{0}') AND ('{1}'='' OR cn.OccDate<'{1}')
AND ('{2}'='' OR lv.LotteryCode='{2}') AND ('{3}'='' OR cn.CatchNumCode='{3}')
AND ('{4}'='' OR ytguser.Code='{4}')", sDate, eDate, lotteryCode, catchNumCode, userCode);
            sql = "(" + sql + ") as t1";
            int pageCount = 0;
            return this.GetEntitysPage<CatchNumsVM>(sql, "OccDate", "*", "OccDate Desc", ESortType.DESC, pageIndex, AppGlobal.ManagerDataPageSize, ref pageCount, ref totalCount);
        }


        public bool CannelCatch(string catchCode)
        {
            var fs = this.GetAll().Where(x => x.CatchNumCode == catchCode).FirstOrDefault();
            if (fs == null)
                return false;
            fs.Stauts = BasicModel.CatchNumType.Compled;
            this.Save();
            return true;
        }

        /// <summary>
        /// 根据编号获取项
        /// </summary>
        /// <param name="catchCode"></param>
        /// <returns></returns>
        public CatchNumJsonDTO GetItemForCode(string catchCode)
        {
            string sql = @"select  b.*,u.Code ,lv.PlayTypeRadioName ,lv.RadioCode,lv.PlayTypeNumName ,lv.PlayTypeName ,lv.LotteryName
 from CatchNums as b
 LEFT JOIN SysYtgUser as u ON u.Id = b.UserId
 LEFT JOIN Lottery_Vw as lv on lv.RadioCode= b.PalyRadioCode where  b.CatchNumCode = '" + catchCode + "'";

            return this.GetSqlSource<CatchNumJsonDTO>(sql).FirstOrDefault();
        }
    }
}
