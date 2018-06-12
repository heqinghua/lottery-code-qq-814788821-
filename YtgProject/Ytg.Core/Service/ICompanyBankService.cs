using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    [ServiceContract]
    public interface ICompanyBankService : ICrudService<CompanyBank>
    {
        [OperationContract]
        IEnumerable<CompanyBankVM> CompanyBankSelectBy(int? bankID, int? status, int pageIndex, ref int totalCount);

        [OperationContract]
        bool Add(CompanyBank model);

        [OperationContract]
        bool Update(CompanyBank model);

        [OperationContract]
        bool SetStatus(int id, bool isEnable);

        [OperationContract]
        bool DeleteById(int id);

        [OperationContract]
        CompanyBank GetOne(int id);

        [OperationContract]
        List<CompanyBankVM> GetRechargeBank(int id, int userId, decimal tradeAmt);

        [OperationContract]
        List<CompanyBankVM> GetRechargeBankInfo(int id, int userId, decimal tradeAmt);

        /// <summary>
        /// 设置入金银行
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool SetIncomingBank(int id);

         /// <summary>
        /// 获取入金银行账号
        /// </summary>
        /// <returns></returns>
        CompanyBankVM GetCompanyBank(string payType);
    }
}
