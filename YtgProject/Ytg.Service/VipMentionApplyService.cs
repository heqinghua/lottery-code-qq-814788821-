using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Data;

namespace Ytg.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class VipMentionApplyService : CrudService<VipMentionApply>, IVipMentionApplyService
    {
        protected readonly IHasher mHasher;
        public VipMentionApplyService(IRepo<VipMentionApply> repo, IHasher hasher)
            : base(repo)
        {
            this.mHasher = hasher;
            this.mHasher.SaltSize = 8;
        }


        public bool CreateApply(bool isOpenVip, int userId)
        {
            //这个表里面已经受理的记录就删除掉
            this.Create(new VipMentionApply
            {
                IsOpenVip = isOpenVip,
                UserId = userId,
                OccDate = DateTime.Now,
                IsEnable = false
            });
            this.Save();
            return true;
        }

        public bool Exist(int userId)
        {
            var result = this.GetAll().Where(m => m.UserId == userId && m.IsEnable == false).FirstOrDefault();
            return result != null;
        }


        public List<VipMentionApplyVM> SelectBy(int isOpenVip, string sDate, string eDate, int pageIndex, ref int totalCount)
        {
            var sql = string.Format(@"WITH result AS(
SELECT v.Id,(CASE v.IsOpenVip WHEN 1 THEN '关闭VIP充提' ELSE '开通VIP充提' END) IsOpenVipDesc,u.Code UserCode,v.OccDate
FROM dbo.VipMentionApplies v
LEFT JOIN dbo.SysYtgUser u ON u.Id=v.UserId
where ({0}=-1 or v.IsOpenVip={0}) and v.OccDate BETWEEN '{1}' AND '{2}'
),totalCount as(
	SELECT COUNT(1) TotalCount FROM result
)
select * from (
	select *, ROW_NUMBER() OVER(Order by a.OccDate DESC ) AS RowNumber from result as a,totalCount
) as b
where RowNumber BETWEEN ({3}-1)*{4}+1 AND {3}*{4}", isOpenVip, sDate, eDate, pageIndex, AppGlobal.ManagerDataPageSize);
            var reslut = this.GetSqlSource<VipMentionApplyVM>(sql);
            if (reslut != null && reslut.Count > 0) totalCount = reslut.FirstOrDefault().TotalCount;
            return reslut;
        }


        /// <summary>
        /// 审核 需要将VipMentionApply里面的记录IsEnable设置为true，并发送消息给客户
        /// </summary>
        /// <param name="applyId"></param>
        /// <returns></returns>
        public bool Audit(int applyId, bool pass)
        {
            int result = 1;
            #region 设置参数
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@applyId",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pass",System.Data.DbType.Int32),               
                new DbContextFactory().GetDbParameter("@result",System.Data.DbType.Int32,System.Data.ParameterDirection.ReturnValue)
            };

            parameters[0].Value = applyId;
            parameters[1].Value = pass;
            #endregion
            this.ExProc<int>("sp_VipMentionApplies_Audit", parameters);
            result = Convert.ToInt32(parameters[2].Value);
            return result > 0;
        }
    }
}
