using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Repository;
using Ytg.Core.Service.Lott;

namespace Ytg.Service.Lott
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LotteryCycleService : CrudService<LotteryCycle>, ILotteryCycleService
    {
        public LotteryCycleService(IRepo<LotteryCycle> repo)
            : base(repo)
        {

        }

        public IEnumerable<LotteryCycle> GetLotteryCycles()
        {
            return this.GetAll();
        }

        public IEnumerable<LotteryCycleVM> GetLotteryCycleVms(int? lotteryId, int pageSize, int pageIndex, ref int pageCount, ref int iRecordCount)
        {
            StringBuilder sb = new StringBuilder(" 1=1 ");
            if (lotteryId != null) sb.AppendFormat(" and c.LotteryId={0}", lotteryId.Value);


            string sql = string.Format("(select c.*,t.LotteryName from LotteryCycles c left join LotteryTypes t on t.Id=c.LotteryId where {0}) as t1 ", sb.ToString());

          //  var results = this.GetEntitysPage<LotteryCycleVM>(sql, "*", "LotteryId", Comm.ESortType.DESC, pageSize, pageIndex, ref pageCount, ref iRecordCount);
            //return results.OrderBy(c => c.LotteryId).ThenBy(c => c.TimeStart);
            return null;
        }


        public bool CreateLotteryCycle(LotteryCycle model)
        {
            if (null == model)
                return false;
            model.OccDate = DateTime.Now;
            base.Create(model);
            base.Save();
            return true;
        }


        public IEnumerator<LotteryIssue> GetLotteryIssues(int lotteryId)
        {
            return null;
        }
    }
}
