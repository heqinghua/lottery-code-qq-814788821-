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
    /// 体现、充值
    /// </summary>
    [ServiceContract]
    public interface ISysBankPoundageService : ICrudService<SysBankPoundage>
    {
        /// <summary>
        /// 新增 wcf
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        bool CreateBankPoundage(SysBankPoundage item);


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="code"></param>
        /// <param name="nickName"></param>
        /// <param name="isDel"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<SysBankPoundageVM> GetBankPoundage(int? bankID, int? toBankId, int? iStatus, bool? isDel, int pageIndex, ref int totalCount);

        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SysBankPoundage GetForId(int id);

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool UpdateItem(int id, SysBankPoundage item);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteItem(int id);

        /// <summary>
        /// 启用禁用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [OperationContract]
        bool SetStatus(int id, int status);
    }
}
