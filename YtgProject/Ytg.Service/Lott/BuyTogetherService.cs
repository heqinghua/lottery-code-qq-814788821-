using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Core.Repository;
using Ytg.Core.Service.Lott;

namespace Ytg.Service.Lott
{
    public class BuyTogetherService: CrudService<BuyTogether>, IBuyTogetherService
    {
        public BuyTogetherService(IRepo<BuyTogether> repo)
            : base(repo)
        {
        }

        /// <summary>
        /// 修改投注相关的合买数据
        /// </summary>
        /// <param name="bettid"></param>
        /// <param name="state"></param>
        public int UpdateBuyTogetherState(int bettid, int state)
        {
            string sql = "update BuyTogethers set Stauts={0} where BetDetailId={1}";
            return this.mRepo.GetDbContext.Database.ExecuteSqlCommand(sql, state, bettid);

        }

        /// <summary>
        /// 根据投注id获取开奖内容
        /// </summary>
        /// <param name="bettid"></param>
        /// <returns></returns>
        public List<BuyTogether> GetForBettid(int bettid)
        {
            string sql = "select * from BuyTogethers where Stauts=3 and BetDetailId=" + bettid;
            return this.GetSqlSource(sql);
        }

        /// <summary>
        /// 根据投注id获取开奖内容
        /// </summary>
        /// <param name="bettid"></param>
        /// <returns></returns>
        public List<BuyTogether> GetForBettidLst(int bettid)
        {
            string sql = "select * from BuyTogethers where  BetDetailId=" + bettid+" order by occdate desc";
            return this.GetSqlSource(sql);
        }

        public int AddTogether(string SerialNo, int TradeType, BuyTogether together)
        {
            string spName = "sp_addoUpdateBuyTogether";

            DbParameter[] pramers = new DbParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@BetDetailId",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@UserId",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@Subscription",SqlDbType.Decimal),
                    new System.Data.SqlClient.SqlParameter("@BuyTogetherCode",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@SerialNo",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@TradeType",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@state",SqlDbType.Int),
            };
            pramers[0].Value = together.BetDetailId;
            pramers[1].Value = together.UserId;
            pramers[2].Value = together.Subscription;
            pramers[3].Value = together.BuyTogetherCode;
            pramers[4].Value = SerialNo;
            pramers[5].Value = TradeType;            
            pramers[6].Direction = ParameterDirection.Output;

            this.ExProcNoReader(spName, pramers);
            object parenter = pramers[6].Value;
            int state = 0;
            if (parenter != null)
            {
                state = Convert.ToInt32(parenter);
            }
            else
            {
                state = -1;
            }

            return state;
        }
    }
}
