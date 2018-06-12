using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SysBankTransferService : CrudService<SysBankTransfer>, ISysBankTransferService
    {
        protected readonly IHasher mHasher;
        public SysBankTransferService(IRepo<SysBankTransfer> repo, IHasher hasher)
            : base(repo)
        {
            this.mHasher = hasher;
            this.mHasher.SaltSize = 8;
        }

        public virtual bool CreateBankTransfer(SysBankTransfer item)
        {
            if (null == item)
                return false;
            this.Create(item);
            this.Save();
            return true;
        }

        public IEnumerable<SysBankTransferVM> GetBankTransferAll(int bankID, int isEnable, int pageIndex, ref int totalCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"WITH result AS(
SELECT p.*,b.BankName BankName,b.BankLogo,b.BankWebUrl,(CASE WHEN p.IsEnable=1 THEN '启用' ELSE '禁用'  END) IsEnableDesc
FROM dbo.SysYtgBankTransfer p
LEFT JOIN dbo.SysBankTypes b ON b.Id=p.BankId
WHERE ({0}=-1 OR p.BankId={0})
	AND ({1}=-1 OR p.IsEnable={1})
),totalCount as(
	SELECT COUNT(1) TotalCount FROM result
)
select * from (
	select *, ROW_NUMBER() OVER(Order by a.OccDate DESC ) AS RowNumber from result as a,totalCount
) as b
where RowNumber BETWEEN ({2}-1)*{3}+1 AND {2}*{3}", bankID, isEnable, pageIndex, AppGlobal.ManagerDataPageSize);
            var list = this.GetSqlSource<SysBankTransferVM>(sb.ToString());
            totalCount = list.Count > 0 ? list.First().TotalCount : 0;
            return list;
        }

        public virtual SysBankTransfer GetForId(int id)
        {
            return this.Get(id);
        }

        public virtual bool UpdateItem(int id, SysBankTransfer item)
        {
            if (null == item)
                return false;
            var model = this.Get(id);
            if (model == null) return false;
            model.BankId = item.BankId;
            model.BeginTime = item.BeginTime;
            model.EndTime = item.EndTime;
            model.IsEnable = item.IsEnable;
            model.IsRecharge = item.IsRecharge;
            model.MaxAmt = item.MaxAmt;
            model.MinAmt = item.MinAmt;
            model.VipMaxAmt = item.VipMaxAmt;
            model.VipMinAmt = item.VipMinAmt;
            model.OccDate = item.OccDate;
            model.OpUser = item.OpUser;
            model.Poundage = item.Poundage;
            this.Save();
            return true;
        }

        public bool SetEnable(int id, bool isenable)
        {
            var item = this.Get(id);
            if (null == item) return false;
            item.IsEnable = isenable;
            this.Save();
            return true;
        }


        public bool DeleteById(int id)
        {
            this.Delete(id);
            this.Save();
            return true;
        }

        /// <summary>
        /// 获取是充值交易设置 并获取该用户的用户充提vip权限
        /// </summary>
        /// <param name="isRecharge"></param>
        /// <param name="userId">登录用户</param>
        /// <returns></returns>
        public List<SysBankTransferVM> SelectAll(bool isRecharge, int userId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"SELECT bp.*,u.IsOpenVip FROM(
SELECT p.*,b.BankName BankName,b.BankLogo,b.BankWebUrl,(CASE WHEN p.IsEnable=1 THEN '启用' ELSE '禁用'  END) IsEnableDesc
FROM dbo.SysYtgBankTransfer p
LEFT JOIN dbo.SysBankTypes b ON b.Id=p.BankId
WHERE IsEnable=1 and IsRecharge={0}
) bp,(SELECT IsOpenVip FROM dbo.SysUserBalances WHERE UserId={1}) u
ORDER BY bp.Id", isRecharge ? 1 : 0, userId);
            var list = this.GetSqlSource<SysBankTransferVM>(sb.ToString());
            return list;
        }
    }
}
