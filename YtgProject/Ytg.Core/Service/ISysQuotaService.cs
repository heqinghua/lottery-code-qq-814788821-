using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;

namespace Ytg.Core.Service
{
    public interface ISysQuotaService : ICrudService<SysQuota>
    {
        /// <summary>
        /// 获取子父用户返点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        IEnumerable<SysQuota> GetUserQuota(int uid, int pid);

        /// <summary>
        /// 获取用户指定阶段的配额对象
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="remb"></param>
        /// <returns></returns>
        SysQuota GetUserQuota(int uid, double remb);

        /// <summary>
        /// 修改配额额度
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        int UpdateQuota(BindSysQuotaDTO dto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="parentUid"></param>
        /// <param name="quotaid"></param>
        /// <param name="value"></param>
        int UpdateQuota(int parentUid, int quotaId, int value);


        /// <summary>
        /// 修改用户配额信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="parentid"></param>
        /// <param name="remb"></param>
        /// <param name="detType"></param>
        /// <returns></returns>
        int UpdateQuota(int uid, int parentid, double remb, int type);

        /// <summary>
        /// 减少配额
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="quoValue"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        int SubtractQuota(int uid, double quoValue, ActionType actionType);

        /// <summary>
        /// 获取用户配额列表
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        List<SysQuota> GetUserQuota(int uid);

        /// <summary>
        /// 获取用户配额列表，配额值必须大于0
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        List<SysQuota> GetUserQuotaMax(int uid);

        /// <summary>
        /// 初始化代理配额
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="parentid">父用户id</param>
        /// <param name="remb">当前用户配额</param>
        int InintUserQuota(int uid, int? parentid, double remb);

        /// <summary>
        /// 回收所有配额
        /// </summary>
        void ClearQuota(int uid, int pid);


        /// <summary>
        /// 查询配额
        /// </summary>
        /// <param name="code"></param>
        /// <param name="quotaType"></param>
        /// <param name="quotaWhere"></param>
        /// <param name="quotaValue"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        List<FilterSysQuotaDTO> FilterQuota(int uid, string code, string quotaType, int quotaWhere, int quotaValue, int pageIndex, int pageSize, ref int totalCount, ref int pageCount);


        /// <summary>
        /// 初始化总代配额
        /// </summary>
        /// <param name="uid"></param>
        void InintPrxyQuota(SysUser user);

        void InintPrxyQuota(SysUser user, int? value);


        /// <summary>
        /// 还原父级返点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="parentId"></param>
        void Restore(int parentId, int type, double rebate);



        /// <summary>
        /// 行转列，配额查询
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="userType"></param>
        /// <param name="playType"></param>
        /// <param name="otherWhere"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<SysQuotas_View_Ref> FilterQuotas(string userCode, int userType, int playType, string otherWhere, int pageIndex, int pageSize, ref int totalCount);
    }
}
