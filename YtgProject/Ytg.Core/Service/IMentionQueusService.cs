using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 提现请求
    /// </summary>
    [ServiceContract]
    public interface IMentionQueusService : ICrudService<MentionQueu>
    {
        [OperationContract]
        List<MentionDTO> SelectBy(string userCode, string mentionCode, string sDate, string eDate,decimal beginMonery,decimal endMonery,int type, int pageIndex, ref int totalCount);

        [OperationContract]
        bool MentionDone(int id, int status, decimal realAmt, decimal poundage, string errorMsg);

        /// <summary>
        /// 提现审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        bool Audit(int id);
    }
}
