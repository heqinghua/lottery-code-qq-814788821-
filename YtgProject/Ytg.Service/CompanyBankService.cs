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
    public class CompanyBankService : CrudService<CompanyBank>, ICompanyBankService
    {
        protected readonly IHasher mHasher;
        public CompanyBankService(IRepo<CompanyBank> repo, IHasher hasher)
            : base(repo)
        {
            this.mHasher = hasher;
            this.mHasher.SaltSize = 8;
        }

        public IEnumerable<CompanyBankVM> CompanyBankSelectBy(int? bankID, int? status, int pageIndex, ref int totalCount)
        {
            //#region 设置参数
            //DbParameter[] parameters = new DbParameter[]{
            //    new DbContextFactory().GetDbParameter("@bankId",System.Data.DbType.Int32),
            //    new DbContextFactory().GetDbParameter("@status",System.Data.DbType.Int32),
            //    new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
            //    new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32)
            //};

            //parameters[0].Value = bankID.HasValue ? bankID.Value : -1;
            //parameters[1].Value = status.HasValue ? status.Value : -1;
            //parameters[2].Value = pageIndex;
            //parameters[3].Value = AppGlobal.ManagerDataPageSize;
            //#endregion
            //var list = this.ExProc<CompanyBankVM>("sp_CompanyBank_SelectBy", parameters);
            //totalCount = list.Count > 0 ? list.First().TotalCount : 0;
            //return list;

            string sql = string.Format(@"SELECT p.*,b.BankName BankName,(CASE WHEN p.IsEnable=1 THEN '启用' ELSE '禁用'  END) StatusDesc FROM dbo.CompanyBanks p LEFT JOIN dbo.SysBankTypes b ON b.Id=p.BankId WHERE 1=1");
            if(bankID!=null)
                sql+=string.Format(" ({0}=-1 OR p.BankId={0}) ",bankID);
            if(status!=null)
                sql += string.Format(" AND ({1}=-1 OR p.IsEnable={1}) ", status);
             

            sql = "(" + sql + ") as t1";
            int pageCount = 0;
            return this.GetEntitysPage<CompanyBankVM>(sql, "Id", "*", "OccDate Desc", ESortType.DESC, pageIndex, AppGlobal.ManagerDataPageSize, ref pageCount, ref totalCount);
        }

        /// <summary>
        /// 获取入金银行账号
        /// </summary>
        /// <returns></returns>
        public CompanyBankVM GetCompanyBank(string payType)
        {
            string sqlStr = "select a1.BankId from CompanyBanks as a1"
                            + " left join SysBankTypes as a2 on a1.BankId=a2.Id"
                            + " where a2.BankDesc='" + payType + "'";

            return this.GetSqlSource<CompanyBankVM>(sqlStr).FirstOrDefault();
        }

        public bool Add(CompanyBank model)
        {
            if (model == null) return false;
            this.Create(model);
            this.Save();
            return true;
        }

        public bool Update(CompanyBank model)
        {
            if (model == null) return false;
            var item = this.Get(model.Id);
            if (item == null) return false;
            item.BankId = model.BankId;
            item.BankNo = model.BankNo;
            item.BankOwner = model.BankOwner;
            item.Branch = model.Branch;
            item.IsEnable = model.IsEnable;
            item.Province = model.Province;
            item.OccDate = model.OccDate;
            this.Save();
            return true;
        }

        public bool SetStatus(int id, bool isEnable)
        {
            var item = this.Get(id);
            if (item == null) return false;
            item.IsEnable = isEnable;
            this.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            this.Delete(id);
            this.Save();
            return true;
        }

        public CompanyBank GetOne(int id)
        {
            return this.Get(1);
        }

        public List<CompanyBankVM> GetRechargeBank(int id, int userId, decimal tradeAmt)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"SELECT top 1 p.*,b.BankName BankName,b.BankLogo,b.BankWebUrl,(CASE WHEN p.IsEnable=1 THEN '启用' ELSE '禁用'  END) IsEnableDesc
                                FROM dbo.CompanyBanks p
                                LEFT JOIN dbo.SysBankTypes b ON b.Id=p.BankId
                                WHERE IsEnable=1 and BankId={0} order by id desc", id);


            var list = this.GetSqlSource<CompanyBankVM>(sb.ToString());

            //在这里对充值进行记录，看用户选择了哪个银行，充值了多少钱
            var sql = string.Format(@"INSERT INTO RecordTemp([BankId],[UserId],[TradeAmt],[IsEnable],[OccDate],[Guid],[IsCompled])
VALUES ({0},{1},{2},0,GETDATE(),'{3}','false');select SCOPE_IDENTITY()", id, userId, tradeAmt.ToString("f0"), Guid.NewGuid().ToString());

            var ids = this.GetSqlSource<decimal>(sql);
            list.First().Num = ids.FirstOrDefault().ToString();
            return list;
        }

        public List<CompanyBankVM> GetRechargeBankInfo(int id, int userId, decimal tradeAmt)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"SELECT top 1 p.*,b.BankName BankName,b.BankLogo,b.BankWebUrl,(CASE WHEN p.IsEnable=1 THEN '启用' ELSE '禁用'  END) IsEnableDesc
                                FROM dbo.CompanyBanks p
                                LEFT JOIN dbo.SysBankTypes b ON b.Id=p.BankId
                                WHERE p.IsIncomingBank=1 and IsEnable=1 and BankId={0} order by id desc", id);


            var list = this.GetSqlSource<CompanyBankVM>(sb.ToString());

            //在这里对充值进行记录，看用户选择了哪个银行，充值了多少钱
            var sql = string.Format(@"INSERT INTO RecordTemp([BankId],[UserId],[TradeAmt],[IsEnable],[OccDate],[Guid],[IsCompled])
VALUES ({0},{1},{2},0,GETDATE(),'{3}','false');select SCOPE_IDENTITY()", id, userId, tradeAmt.ToString("f0"), Guid.NewGuid().ToString());

            var ids = this.GetSqlSource<decimal>(sql);
            list.First().Num = ids.FirstOrDefault().ToString();
            return list;
        }

        /// <summary>
        /// 设置入金银行
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SetIncomingBank(int id)
        {
            CompanyBank companyBank = this.Get(id);
            //if (companyBank.BankId == 20 || companyBank.BankId == 21)
            //{
                this.GetAll().Where(m => m.BankId == companyBank.BankId).ToList().ForEach(m =>
                {
                    m.IsIncomingBank = false;
                });
                this.Get(id).IsIncomingBank = true;
                this.Save();
                return true;
           // }
            return false;
        }
    }
}
