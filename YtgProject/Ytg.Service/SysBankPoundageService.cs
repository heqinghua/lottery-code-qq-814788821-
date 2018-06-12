using System;
using System.Collections.Generic;
using System.Data.Common;
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
using Ytg.Data;

namespace Ytg.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SysBankPoundageService : CrudService<SysBankPoundage>, ISysBankPoundageService
    {
        protected readonly IHasher mHasher;
        public SysBankPoundageService(IRepo<SysBankPoundage> repo, IHasher hasher)
            : base(repo)
        {
            this.mHasher = hasher;
            this.mHasher.SaltSize = 8;
        }

        public bool CreateBankPoundage(SysBankPoundage item)
        {
            if (null == item)
                return false;
            var mode = this.Where(m => m.FromBankName == item.FromBankName && m.ToBankName == item.ToBankName && m.IsRecharge == item.IsRecharge).FirstOrDefault();
            if (mode != null)
            {
                if (mode.IsDelete)
                    mode.IsDelete = false;
            }
            else
            {
                this.Create(item);
            }
            this.Save();
            return true;
        }

        public IEnumerable<SysBankPoundageVM> GetBankPoundage(int? bankID, int? toBankId, int? iStatus, bool? isDel, int pageIndex, ref int totalCount)
        {
            #region 设置参数
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@fromBankId",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@toBankId",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@status",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@isDelete",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32)
            };

            parameters[0].Value = bankID.HasValue ? bankID.Value : -1;
            parameters[1].Value = toBankId.HasValue ? toBankId.Value : -1;
            parameters[2].Value = iStatus.HasValue ? iStatus.Value : -1;
            parameters[3].Value = isDel.HasValue ? (isDel.Value ? 1 : 0) : -1;
            parameters[4].Value = pageIndex;
            parameters[5].Value = AppGlobal.ManagerDataPageSize;
            #endregion
            var list = this.ExProc<SysBankPoundageVM>("sp_SysBankPoundage_SelectBy", parameters);
            totalCount = list.Count > 0 ? list.First().TotalCount : 0;
            return list;
        }

        public SysBankPoundage GetForId(int id)
        {
            return this.Get(id);
        }

        public bool UpdateItem(int id, SysBankPoundage item)
        {
            if (null == item)
                return false;
            var model = this.Get(id);
            model.Days = item.Days;
            model.FromBankName = item.FromBankName;
            model.IsDelete = item.IsDelete;
            model.IsRecharge = item.IsRecharge;
            model.LimitAmt = item.LimitAmt;
            model.OccDate = item.OccDate;
            model.OpTime = item.OpTime;
            model.OpUser = item.OpUser;
            model.Percent = item.Percent;
            model.Status = item.Status;
            model.ToBankName = item.ToBankName;
            this.Save();
            return true;
        }

        public bool DeleteItem(int id)
        {
            var item = this.Get(id);
            item.IsDelete = true;
            this.Save();
            return true;

        }

        public bool SetStatus(int id, int status)
        {
            var item = this.Get(id);
            item.Status = status;
            this.Save();
            return true;
        }
    }
}
