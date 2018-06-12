using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SysBankTypeService : CrudService<SysBankType>, ISysBankType
    {
        protected readonly IHasher mHasher;
        public SysBankTypeService(IRepo<SysBankType> repo, IHasher hasher)
            : base(repo)
        {
            this.mHasher = hasher;
            this.mHasher.SaltSize = 8;
        }

        public virtual bool CreateBankType(SysBankType item)
        {
            if (null == item)
                return false;
            this.Create(item);
            this.Save();
            return true;
        }

        public virtual IEnumerable<SysBankType> GetBankType(string sBankName, bool? isDel, int pageIndex, ref int totalCount)
        {
            var source = this.GetAll();
            if (!string.IsNullOrEmpty(sBankName))
                source = source.Where(item => item.BankName.IndexOf(sBankName) != -1);
            if (isDel.HasValue)
                source = source.Where(item => item.IsDelete == isDel.Value);

            totalCount = source.Count();
            return source.Page(pageIndex, AppGlobal.ManagerDataPageSize);
        }

        public IEnumerable<SysBankType> GetAllBankType()
        {
            var soure = this.GetAll();
            return soure;
        }

        public List<SysBankTypeDTO> SelectAllBankType()
        {
            var soruce = this.Where(m => m.IsDelete == false).ToList();
            if (soruce != null && soruce.Count > 0)
            {
                return soruce.ConvertAll(m => new SysBankTypeDTO
                {
                    BankDesc = m.BankDesc,
                    BankLogo = m.BankLogo,
                    BankName = m.BankName,
                    BankWebUrl = m.BankWebUrl,
                    Id = m.Id,
                    IsShowZhiHang=m.IsShowZhiHang,
                    IsInterBank=m.IsInterBank
                });
            }
            return new List<SysBankTypeDTO>();
        }

        public virtual SysBankType GetForId(int id)
        {
            return this.Get(id);
        }

        public virtual bool UpdateItem(int id, SysBankType item)
        {
            if (null == item)
                return false;
            var model = this.Get(id);
            model.BankDesc = item.BankDesc;
            model.BankLogo = item.BankLogo;
            model.BankName = item.BankName;
            model.BankWebUrl = item.BankWebUrl;
            model.OccDate = item.OccDate;
            model.OpTime = item.OpTime;
            model.OpUser = item.OpUser;
            model.IsShowZhiHang = item.IsShowZhiHang;
            model.OpenAutoRecharge = item.OpenAutoRecharge;
            model.IsInterBank = item.IsInterBank;
            this.Save();
            return true;
        }

        public List<ProvinceDTO> SelectAllProvinces()
        {
            var sql = "SELECT ProvinceID,ProvinceName FROM dbo.S_Province";
            var result = this.GetSqlSource<ProvinceDTO>(sql);
            return result;
        }

        public List<CityDTO> SelectAllCitys(int provinceId)
        {
            var sql = string.Format("SELECT CityID,CityName,ProvinceID FROM dbo.S_City WHERE ProvinceID={0}", provinceId);
            var result = this.GetSqlSource<CityDTO>(sql);
            return result;
        }

        /// <summary>
        /// 获取所有支持充值银行类型
        /// 是否充值
        /// 提现：false
        ///充值：true
        /// </summary>
        public List<RechargeBankTypeDTO> GetRechargeBankTypes(bool IsRecharge)
        {
            string sql = string.Format(@"select  btf.*,bt.BankName BankName,bt.BankLogo,bt.BankWebUrl,bt.OpenAutoRecharge,bt.IsInterBank from SysYtgBankTransfer as btf
inner join SysBankTypes as bt on btf.BankId=bt.Id
where OpenAutoRecharge='true'  and btf.IsEnable='true'", IsRecharge);
            return this.GetSqlSource<RechargeBankTypeDTO>(sql);
        }
    }
}
