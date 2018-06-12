using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using System.ServiceModel;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 转账设置
    /// </summary>
    [ServiceContract]
    public interface ISysBankTransferService : ICrudService<SysBankTransfer>
    {
        /// <summary>
        /// 新增转账记录 wcf
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool CreateBankTransfer(SysBankTransfer item);

        [OperationContract]
        IEnumerable<SysBankTransferVM> GetBankTransferAll(int bankID, int isEnable, int pageIndex, ref int totalCount);
        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SysBankTransfer GetForId(int id);

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool UpdateItem(int id, SysBankTransfer item);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteById(int id);

        /// <summary>
        /// 获得所有的充值银行交易设置信息
        /// </summary>
        /// <param name="isRecharge"></param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [OperationContract]
        List<SysBankTransferVM> SelectAll(bool isRecharge, int userId);

        [OperationContract]
        bool SetEnable(int id, bool isenable);
    }
}
