using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Data;

namespace Ytg.Service
{
    /// <summary>
    /// 用户绑定银行卡信息
    /// </summary>
    public class SysUserBankService : CrudService<SysUserBank>, ISysUserBankService
    {
        public SysUserBankService(IRepo<SysUserBank> repo)
            : base(repo)
        {

        }

        /// <summary>
        /// 验证卡号和户名是否正确
        /// </summary>
        /// <param name="bankCard"></param>
        /// <param name="bankOwner"></param>
        /// <returns></returns>
        public bool VidataCard(string bankCard, string bankOwner)
        {
            return this.Where(c => c.BankNo == bankCard && c.BankOwner == bankOwner).Count() > 0;
        }

        /// <summary>
        /// 获取银行卡信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public VdBankDTO GetUserBank(int userid)
        {
            string sql = "select bt.BankName,bk.BankNo,bk.BankOwner from SysUserBanks as bk inner join SysBankTypes as bt on bk.BankId=bt.Id where userid=" + userid + " and bk.IsDelete=0 order by bk.OccDate";
            return this.GetSqlSource<VdBankDTO>(sql).FirstOrDefault();
        }

        public List<VdBankDTO> GetUserBanks(int userid)
        {

            string sql = "select bk.BankId,bt.BankName,bk.BankNo,bk.BankOwner from SysUserBanks as bk inner join SysBankTypes as bt on bk.BankId=bt.Id where userid=" + userid + " and bk.IsDelete=0 order by bk.OccDate";
            return this.GetSqlSource<VdBankDTO>(sql);
        }



        /// <summary>
        /// 查看用户有没有设置资金密码和绑定银行卡
        /// 返回0表示都没有绑定，返回1表示设置了资金密码，返回2表示绑定了银行卡，返回3表示全部设置了
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        public RechargeMentionStatus GetUserBankAndBalancePwd(int uId)
        {
            var result = RechargeMentionStatus.None;
            var sql = string.Format("SELECT TOP 1 u.Id,u.UserId,u.IsOpenVip,u.Pwd,U.UserAmt,u.OpTime,u.OccDate,IsNull(b.Id,-1) Status FROM dbo.SysUserBalances u LEFT JOIN dbo.SysUserBanks b ON b.UserId=u.UserId WHERE u.UserId={0}", uId);
            var banks = this.GetSqlSource<SysUserBalance>(sql);
            if (banks != null && banks.Count > 0)
            {
                var first = banks.FirstOrDefault();
                if (!string.IsNullOrEmpty(first.Pwd)) result = result | RechargeMentionStatus.SetBalancePwd;
                if (first.Status > -1) result = result | RechargeMentionStatus.BindBank;
                if (((result & RechargeMentionStatus.SetBalancePwd) == RechargeMentionStatus.SetBalancePwd) && ((result & RechargeMentionStatus.BindBank) == RechargeMentionStatus.BindBank))
                {
                    //在这里判断用户的投注金额是不是大于充值金额的5%
                    sql = string.Format(@"SELECT ISNULL(SUM(CASE TradeType WHEN 1 THEN TradeAmt WHEN 8 THEN TradeAmt ELSE 0 END),0) Chongzhi,
	ISNULL(SUM(CASE TradeType WHEN 3 THEN TradeAmt WHEN 4 THEN TradeAmt WHEN 5 THEN TradeAmt WHEN 12 THEN TradeAmt ELSE 0 END),0) Touzhu 
FROM dbo.SysUserBalanceDetails
WHERE UserId={0} AND (TradeType=1 OR TradeType=3 OR TradeType=4 OR TradeType=5 OR TradeType=8 OR TradeType=12)
	AND OccDate BETWEEN '{1}' AND '{2}'
", uId, DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
                    var items = this.GetSqlSource<MentionConditionsDTO>(sql);
                    if (items != null && items.Count > 0)
                    {
                        var item = items.FirstOrDefault();
                        if (item.Touzhu != 0)
                        {
                            if ((item.Chongzhi * 5 / 100m) <= (-item.Touzhu))
                            {
                                result = result | RechargeMentionStatus.AllMention;
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 修改银行状态
        /// </summary>
        /// <param name="bankId"></param>
        /// <returns></returns>
        public bool ModiryUserBankStatus(int bankId)
        {
            SysUserBank userBank = this.Get(bankId);
            if (userBank != null)
            {
                userBank.IsDelete = userBank.IsDelete == true ? false : true;
                this.Save();
                return true;
            }
            return false;
        }

        public List<SysUserBankVM> SelectUserBank(int userId, int bankId, string userCode, string userNickName, string bankNo, int isDelete, int pageIndex, ref int totalCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"SELECT ub.Id,ub.BankId,b.BankName,ub.ProvinceId,p.ProvinceName Province,
	ub.CityId,c.CityName City,ub.Branch,ub.BankNo,
	ub.BankOwner,ub.UserId,u.Code UserName,ub.OccDate,(CASE ub.IsDelete WHEN 1 THEN '禁用' ELSE '启用' END) IsDeleteDesc
FROM dbo.SysUserBanks ub
LEFT JOIN dbo.SysBankTypes b ON b.Id=ub.BankId
LEFT JOIN dbo.SysYtgUser u ON u.Id=ub.UserId
LEFT JOIN dbo.S_Province p ON p.ProvinceID=ub.ProvinceId
LEFT JOIN dbo.S_City c ON c.CityID=ub.CityId AND c.ProvinceID=p.ProvinceID
where ({0}=-1 or ub.UserId={0}) AND ({1}=-1 or ub.BankId={1}) AND ({2}=-1 or ub.IsDelete={2})
    and ('{3}'='' or u.Code like '%{3}%') and ('{4}'='' or u.NikeName like '%{4}%')
    and ('{5}'='' or ub.BankNo like '%{5}%')", userId, bankId, isDelete, userCode, userNickName, bankNo);
            string sql = "(" + sb.ToString() + ") as t1";
            int pageCount = 0;
            return this.GetEntitysPage<SysUserBankVM>(sql, "OccDate", "*", "OccDate DESC", ESortType.DESC, pageIndex, AppGlobal.ManagerDataPageSize, ref pageCount, ref totalCount);
        }

        public bool CreateBank(SysUserBank item)
        {
            var flag = false;

            var count = this.Where(m => m.UserId == item.UserId).Count();
            if (count >= 5) return false;
            this.Create(item);
            this.Save();
            flag = true;

            return flag;
        }

        public List<UserMentionDTO> SelectMentionBank(int userId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"SELECT ub.Id,ub.OccDate,ub.BankNo,bt.BankName,b.MentionCount,u.UserAmt,t.MinAmt,t.MaxAmt,t.VipMaxAmt,t.VipMinAmt,u.IsOpenVip
FROM dbo.SysUserBanks ub
LEFT JOIN (
	SELECT COUNT(1) MentionCount,UserId={0} FROM dbo.SysUserBalanceDetails
	WHERE TradeType=2 AND UserId={0} and OccDate BETWEEN '{1}' and '{2}'
) b ON b.UserId=ub.UserId
LEFT JOIN dbo.SysUserBalances u ON u.UserId=ub.UserId
LEFT JOIN dbo.SysYtgBankTransfer t ON t.BankId=ub.BankId AND t.IsRecharge=0
LEFT JOIN dbo.SysBankTypes bt ON bt.Id=ub.BankId
WHERE ub.UserId={0}", userId, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            var list = this.GetSqlSource<UserMentionDTO>(sb.ToString());
            return list;
        }

        /// <summary>
        /// 新增提现记录数据
        /// </summary>
        /// <param name="userBankId"></param>
        /// <param name="mentionAmt"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int SubmitMention(int userBankId, decimal mentionAmt, int userId)
        {
            var errorCode = 1;
            #region 设置参数
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@userId",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@userBankId",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@mentionAmt",System.Data.DbType.Decimal),
                new DbContextFactory().GetDbParameter("@mentionCode",System.Data.DbType.String),                
                new DbContextFactory().GetDbParameter("@errorCode",System.Data.DbType.Int32,System.Data.ParameterDirection.ReturnValue)
            };

            parameters[0].Value = userId;
            parameters[1].Value = userBankId;
            parameters[2].Value = mentionAmt;
            parameters[3].Value = string.Format("Me{0}", DateTime.Now.ToString("MMddHHmmssfff"));
            #endregion
            var list = this.ExProc<AmountChangeDTO>("sp_MentionQueus_Create", parameters);
            errorCode = Convert.ToInt32(parameters[4].Value);
            return errorCode;
        }

        public List<MentionDTO> SelectMention(int userId, int pageIndex, string sDate, string eDate, ref int totalCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"WITH result AS(
SELECT m.Id, m.MentionAmt,m.QueuNumber,m.MentionCode,m.SendTime,m.Poundage,m.RealAmt,(CASE m.Status WHEN 1 THEN '提现成功' when 2 then '提现失败' when 3 then '用于撤销' ELSE '排队中' END) IsEnableDesc,m.OccDate,
	u.Code,ub.BankNo,ub.BankOwner,b.BankName,b.BankWebUrl
FROM dbo.MentionQueus m
LEFT JOIN dbo.SysYtgUser u ON u.Id=m.UserId
LEFT JOIN dbo.SysUserBanks ub ON ub.Id=m.UserBankId
LEFT JOIN dbo.SysBankTypes b ON b.Id=ub.BankId
where m.UserId={0} and m.SendTime>'{3}' {4}
),totalCount as(
	SELECT COUNT(1) TotalCount FROM result
)
select * from (
	select *, ROW_NUMBER() OVER(Order by a.QueuNumber) AS RowNumber from result as a,totalCount
) as b
where RowNumber BETWEEN ({1}-1)*{2}+1 AND {1}*{2}", userId, pageIndex, AppGlobal.ManagerDataPageSize, sDate,
                                                  string.IsNullOrEmpty(eDate) ? "" : string.Format("and m.SendTime<'{0}'", eDate));
            var list = this.GetSqlSource<MentionDTO>(sb.ToString());
            totalCount = list.Count > 0 ? list.First().TotalCount : 0;
            list.ForEach(m =>
            {
                var len = m.BankNo.Length;
                var count = Convert.ToInt32(len / 2);
                if (count > 3)
                {
                    m.BankNoShort = new string('*', len - 3) + m.BankNo.Substring(len - 3);
                }
                else
                {
                    m.BankNoShort = m.BankNo.Replace(m.BankNo.Substring(0, 2), "**");
                }
            });
            return list;
        }


        /// <summary>
        /// 查询用户绑定银行卡
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isLockCards"></param>
        /// <param name="bakNames"></param>
        /// <param name="bankOwner"></param>
        /// <param name="bankNo"></param>
        /// <param name="proName"></param>
        /// <param name="cityName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<UserBanksDto> FilterUserBanks(string code, int isLockCards, string bakNames, string bankOwner, string bankNo, string proName, string cityName, int pageIndex, ref int totalCount)
        {
            string sql = @"select sub.*,sy.Code,sy.UserLockCount,sy.IsLockCards,sbt.BankName,sp.ProvinceName,sc.CityName from [SysUserBanks] as sub
left join SysYtgUser as sy on sub.userid=sy.id
left join SysBankTypes as sbt on sbt.Id=sub.BankId
left join S_Province as sp on sp.ProvinceID=sub.ProvinceId
left join S_City as sc on sc.CityID=sub.CityId where 1=1";
            if (!string.IsNullOrEmpty(code))
                sql += " and sy.Code='" + code + "'";
            if (isLockCards != -1)
                sql += " and sy.IsLockCards=" + isLockCards;
            if (!string.IsNullOrEmpty(bakNames))
                sql += " and sbt.BankName='" + bakNames + "'";
            if (!string.IsNullOrEmpty(bankOwner))
                sql += " and BankOwner='" + bankOwner + "'";
            if (!string.IsNullOrEmpty(bankNo))
                sql += " and sub.BankNo like '%" + bankNo + "%'";
            if (!string.IsNullOrEmpty(proName))
                sql += " and sp.ProvinceName='" + proName + "'";
            if (!string.IsNullOrEmpty(cityName))
                sql += "  and sc.CityName='" + cityName + "'";

            sql = "(" + sql + ") as t1";
            int pageCount = 0;
            return this.GetEntitysPage<UserBanksDto>(sql, "OccDate", "*", "OccDate desc", ESortType.DESC, pageIndex, AppGlobal.ManagerDataPageSize, ref pageCount, ref totalCount);
        }


    }
}
