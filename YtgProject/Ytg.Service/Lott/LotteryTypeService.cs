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
    public class LotteryTypeService : CrudService<LotteryType>, ILotteryTypeService
    {
        public LotteryTypeService(IRepo<LotteryType> repo)
            : base(repo)
        {

        }


        public IEnumerable<LotteryType> GetLotteryTypes()
        {
            return this.GetNoTracking();
        }

        /// <summary>
        /// 修改彩种状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public bool UpdateLotteryTypeState(int id, bool isEnable)
        {
            var item = this.Get(id);
            if (null == item)
                return false;
            item.IsEnable = isEnable;
            this.Save();
            return true;
        }

        /// <summary>
        /// 修改彩种
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UpdateLotteryType(LotteryType item)
        {
            var c = this.Get(item.Id);
            if (c == null)
                return false;
            c.IsEnable = item.IsEnable;
            c.LotteryCode = item.LotteryCode;
            c.LotteryName = item.LotteryName;
            c.Sort = item.Sort;
            c.SplitBettingSecond = item.SplitBettingSecond;
            c.TotalCount = item.TotalCount;
            this.Save();
            return true;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="lotteryId"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public bool Sort(int lotteryId, int sort)
        {
            var lottery = this.Get(lotteryId);
            if (lottery == null)
                return false;
            lottery.Sort = sort;
            this.Save();
            return true;
        }

        public LotteryType GetLottery(int lotteryId)
        {
            string sql = "select * from [LotteryTypes] where id=" + lotteryId;
            return this.GetSqlSource<LotteryType>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取启用的彩种
        /// </summary>
        /// <returns></returns>
        public List<LotteryType> GetEnableLotterys()
        {
            string sql = "select * from [dbo].[LotteryTypes] where IsEnable=1";
            return this.GetSqlSource<LotteryType>(sql);
        }
    }
}
