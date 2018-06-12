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
    /// 用户绑定银行卡
    /// </summary>
    [ServiceContract]
    public interface ISysUserBankService : ICrudService<SysUserBank>
    {
        RechargeMentionStatus GetUserBankAndBalancePwd(int uId);

        [OperationContract]
        List<SysUserBankVM> SelectUserBank(int userId, int bankId, string userCode, string userNickName, string bankNo, int isDelete, int pageIndex, ref int totalCount);

        bool CreateBank(SysUserBank item);

        /// <summary>
        /// 验证卡号和户名是否正确
        /// </summary>
        /// <param name="bankCard"></param>
        /// <param name="bankOwner"></param>
        /// <returns></returns>
        bool VidataCard(string bankCard, string bankOwner);

        /// <summary>
        /// 获取用户第一张银行卡
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        VdBankDTO GetUserBank(int userid);

        /// <summary>
        /// 获取用户第一张银行卡
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        List<VdBankDTO> GetUserBanks(int userid);

        List<UserMentionDTO> SelectMentionBank(int userId);

        int SubmitMention(int userBankId, decimal mentionAmt, int userId);

        List<MentionDTO> SelectMention(int userId, int pageIndex, string sDate, string eDate, ref int totalCount);

        /// <summary>
        /// 修改银行状态
        /// </summary>
        /// <param name="bankId"></param>
        /// <returns></returns>
        [OperationContract]
        bool ModiryUserBankStatus(int bankId);


        /// <summary>
        /// 查询用户绑定银行卡
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isLockCards"></param>
        /// <param name="bakNames"></param>
        /// <param name="bankOwner"></param>
        /// <param name="bankNo"></param>
        /// <param name="proName"></param>
        /// <param name="cityName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<UserBanksDto> FilterUserBanks(string code, int isLockCards, string bakNames, string bankOwner, string bankNo, string proName, string cityName, int pageIndex, ref int totalCount);

    }
}
