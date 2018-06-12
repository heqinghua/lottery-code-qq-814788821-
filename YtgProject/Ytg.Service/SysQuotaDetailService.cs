using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Data;

namespace Ytg.Service
{
    /// <summary>
    /// 账变详细信息
    /// </summary>
    public class SysQuotaDetailService : CrudService<SysQuotaDetail>, ISysQuotaDetailService
    {
        public SysQuotaDetailService(IRepo<SysQuotaDetail> repo)
            : base(repo)
        {

        }

        public SysQuotaDetail AddDetail(int quotaId, ActionType actionType, int oldNum, int nowNum, string opUser)
        {
            var item = this.Create(new SysQuotaDetail()
            {
                ActionType = actionType,
                SysQuotaId = quotaId,
                OldNum = oldNum,
                NowNum = nowNum,
                OpUser = opUser
            });
            this.Save();

            return item;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="includeChildren">是否包含下级</param>
        /// <returns></returns>
        public List<SysQuotaDetaiDTO> GetAll(int uid, bool includeChildren,ActionType? actype,string quotype,string userCode,DateTime? beginDat,DateTime? endDate,int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            string sqlWhere = string.Empty;
            if (actype != null)
                sqlWhere += string.Format(" and sqd.ActionType={0}", (int)actype.Value);
            if (!string.IsNullOrEmpty(quotype))
                sqlWhere += string.Format(" and sq.QuotaType='{0}'", quotype);
            if(!string.IsNullOrEmpty(userCode))
                sqlWhere += string.Format(" and syu.Code like '%{0}%'", userCode);
            if(beginDat!=null && endDate!=null)
                sqlWhere += string.Format(" and sq.OccDate between '{0}' and  '{1}'", beginDat.Value,endDate.Value);

            if (!includeChildren)
                return NotChildrenSource(uid,sqlWhere,pageIndex, pageSize, ref totalCount, ref pageCount);
            return ChildrenSource(uid, sqlWhere, pageIndex, pageSize, ref totalCount, ref pageCount);
        }

        /// <summary>
        /// 包含下级所有记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        private List<SysQuotaDetaiDTO> ChildrenSource(int uid,string sqlWhere,int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            string procName = "sp_QuotaDetailFilter";
            
            var fc = IoC.Resolve<IDbContextFactory>();
            System.Data.Common.DbParameter[] dbParameters = new System.Data.Common.DbParameter[]{
                fc.GetDbParameter("@uid", System.Data.DbType.Int32),
                fc.GetDbParameter("@pageSize", System.Data.DbType.Int32),
                fc.GetDbParameter("@pageIndex", System.Data.DbType.Int32),
                fc.GetDbParameter("@sqlWhere", System.Data.DbType.String),
                fc.GetDbParameter("@totalCount", System.Data.DbType.Int32),
            };
            dbParameters[0].Value = uid;
            dbParameters[1].Value = pageSize;
            dbParameters[2].Value = pageIndex;
            dbParameters[3].Value = sqlWhere;
            dbParameters[4].Value = 0;
            dbParameters[4].Direction = System.Data.ParameterDirection.Output;


            var source = this.ExProc<SysQuotaDetaiDTO>(procName, dbParameters);
            totalCount = Convert.ToInt32(dbParameters[4].Value);
            pageCount = totalCount % pageSize == 0 ? totalCount / pageSize : (totalCount / pageSize) + 1;
            return source;
        }

        /// <summary>
        /// 不包含下级所有记录
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        private List<SysQuotaDetaiDTO> NotChildrenSource(int uid, string sqlWhere, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            string sql =string.Format( @"select syu.Code,sq.QuotaType,sqd.* from SysQuotas as sq 
	                        inner join SysYtgUser as syu on syu.Id=sq.SysUserId
	                        inner join SysQuotaDetails as sqd on sq.Id=sqd.SysQuotaId where syu.Id={0}",uid);
            sql += sqlWhere;
            sql = "(" + sql + ") as t1";
            return GetEntitysPage<SysQuotaDetaiDTO>(sql,"Id","*", "OccDate", Comm.ESortType.DESC, pageIndex, pageSize, ref pageCount, ref totalCount);
        }
    }
}
