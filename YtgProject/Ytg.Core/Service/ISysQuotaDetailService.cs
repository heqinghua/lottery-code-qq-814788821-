using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 配额账边
    /// </summary>
    public interface ISysQuotaDetailService : ICrudService<SysQuotaDetail>
    {

        SysQuotaDetail AddDetail(int quotaId, ActionType actionType, int oldNum, int nowNum, string opUser);


        /// <summary>
        /// 获取账变列表
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="includeChildren"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<SysQuotaDetaiDTO> GetAll(int uid, bool includeChildren, ActionType? actype, string quotype, string userCode, DateTime? beginDat, DateTime? endDate, int pageIndex, int pageSize, ref int totalCount, ref int pageCount);
    }
}
