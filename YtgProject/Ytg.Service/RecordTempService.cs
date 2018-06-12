using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Data;

namespace Ytg.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class RecordTempService : CrudService<RecordTemp>, IRecordTempService
    {
        public RecordTempService(IRepo<RecordTemp> repo)
            : base(repo)
        {

        }

        /// <summary>
        /// 获取充值历史记录数据
        /// </summary>
        /// <returns></returns>
        public List<RecordTempDTO> GetRechingHis(string userCode, string rechangeBet, string beginTime, string endTime, int isCompled, int pageIndex, int pageSize, ref int totalCount)
        {
            string sql = "select su.Code,su.UserType,su.Rebate,rt.* from RecordTemp as rt inner join  SysYtgUser as su on rt.UserId=su.Id where 1=1";

            if (!string.IsNullOrEmpty(userCode))
                sql += " and su.Code like '%" + userCode + "%'";
            if (!string.IsNullOrEmpty(rechangeBet))//摩宝支付订单
                sql += " and rt.MY18oid like '%" + rechangeBet + "%'";
            if (!string.IsNullOrEmpty(beginTime) && !string.IsNullOrEmpty(endTime))
                sql += string.Format(" and rt.OccDate between '{0}' and '{1}'", beginTime, endTime);
            if (isCompled != -1)
                sql += " and rt.IsCompled=" + isCompled;

            sql = "(" + sql + ") as t1";

            int pageCount = 0;
            return GetEntitysPage<RecordTempDTO>(sql, "OccDate", "*", " OccDate DESC", Comm.ESortType.DESC, pageIndex, pageSize, ref pageCount, ref totalCount);
        }


        /// <summary>
        /// 完成订单，调用存储过程，避免重复提交
        /// </summary>
        /// <returns></returns>
        public RecordTemp Compled_RecordTemp(string orderNo, string MY18oid, decimal MY18M, string MY18DT, out int stauts)
        {
            string procName = "sp_Compled_RecordTemp";
            var fc = IoC.Resolve<IDbContextFactory>();
            var p = fc.GetDbParameter("@orderNo", System.Data.DbType.String);
            p.Value = orderNo;

            var p1 = fc.GetDbParameter("@MY18oid", System.Data.DbType.String);
            p1.Value = MY18oid;

            var p2 = fc.GetDbParameter("@MY18M", System.Data.DbType.Decimal);
            p2.Value = MY18M;

            var p3 = fc.GetDbParameter("@MY18DT", System.Data.DbType.String);
            p3.Value = MY18DT;

            var p4 = fc.GetDbParameter("@stauts", System.Data.DbType.Int32);
            p4.Direction = System.Data.ParameterDirection.Output;

            var result = this.ExProc<RecordTemp>(procName, p, p1, p2, p3, p4);
            if (p4.Value != null)
                stauts = Convert.ToInt32(p4.Value);
            else
                stauts = -1;
            return result.FirstOrDefault();
        }

        /// <summary>
        /// 获取用户充值成功的笔数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetCompledCount(int userId)
        {
            return this.GetAll().Where(x => x.IsCompled && x.UserId == userId).Count();
        }
    }
}
