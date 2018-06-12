using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using System.ServiceModel;
using Ytg.BasicModel.DTO;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 银行表
    /// </summary>
    [ServiceContract]
    public interface ISysBankType : ICrudService<SysBankType>
    {
        /// <summary>
        /// 新增 wcf
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool CreateBankType(SysBankType item);


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="code"></param>
        /// <param name="nickName"></param>
        /// <param name="isDel"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<SysBankType> GetBankType(string sBankName, bool? isDel, int pageIndex, ref int totalCount);

        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SysBankType GetForId(int id);

        [OperationContract]
        IEnumerable<SysBankType> GetAllBankType();

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool UpdateItem(int id, SysBankType item);

        List<SysBankTypeDTO> SelectAllBankType();

        List<ProvinceDTO> SelectAllProvinces();

        List<CityDTO> SelectAllCitys(int provinceId);

        /// <summary>
        /// 获取所有支持充值银行类型
        /// 是否充值
        /// 提现：false
        ///充值：true
        /// </summary>
        List<RechargeBankTypeDTO> GetRechargeBankTypes(bool IsRecharge);
    }
}
