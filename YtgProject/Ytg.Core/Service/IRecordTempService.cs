using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRecordTempService : ICrudService<RecordTemp>
    {
        /// <summary>
        /// 获取充值数据
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="rechangeBet"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="isCompled"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<RecordTempDTO> GetRechingHis(string userCode, string rechangeBet, string beginTime, string endTime, int isCompled, int pageIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 完成订单，调用存储过程，避免重复提交
        /// </summary>
        /// <returns></returns>
        RecordTemp Compled_RecordTemp(string orderNo, string MY18oid, decimal MY18M, string MY18DT,out int stauts);
    }
}
