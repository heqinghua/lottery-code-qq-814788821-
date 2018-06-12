using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.LotteryBasic;
using Ytg.BasicModel.LotteryBasic.DTO;

namespace Ytg.Core.Service.Lott
{
    /// <summary>
    /// 追号数据服务对象
    /// </summary>
    [ServiceContract]
    public interface ISysCatchNumService : ICrudService<CatchNum>
    {

        /// <summary>
        /// 追号查询
        /// </summary>
        /// <param name="loginUid">登录用户id</param>
        /// <param name="paramer">查询参数</param>
        /// <returns></returns>
        List<CatchNumJsonDTO> FilterCatchNumList(int userId, FilterCatchNumListParamerDTO paramer, int pageIndex, ref int totalCount);


        /// <summary>
        /// 获取指定期号未完成的追号
        /// </summary>
        /// <param name="issueCode">期号</param>
        /// <returns></returns>
        List<NotCompledCatchNumListDTO> GetNotCompledCatchNumList(string lotteryCode, string issueCode);

        /// <summary>
        /// 追号查询 wcf 服务，用户后台查询
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<CatchNumsVM> SelectCatchNumList(string sDate, string eDate, string lotteryCode, string catchNumCode,
            string userCode, int pageIndex, ref int totalCount);

        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="catchCode"></param>
        /// <returns></returns>
        bool CannelCatch(string catchCode);


        /// <summary>
        /// 根据编号获取项
        /// </summary>
        /// <param name="catchCode"></param>
        /// <returns></returns>
        CatchNumJsonDTO GetItemForCode(string catchCode);

    }
}
