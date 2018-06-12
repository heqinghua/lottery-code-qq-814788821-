using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    /// <summary>
    /// 大转盘
    /// </summary>
    public class LargeRotaryService : CrudService<LargeRotary>, ILargeRotaryService
    {
        public LargeRotaryService(IRepo<LargeRotary> repo)
            : base(repo)
        {

        }

        /// <summary>
        /// 获取当天数据
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public LargeRotary GetNowItem(int uid)
        {
            DateTime beginDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd") + " 07:00:00");
            DateTime endDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 02:00:00");
            return this.Where(c => c.Uid == uid && c.OccDate >= beginDate && c.OccDate <= endDate).FirstOrDefault();
        }

        public bool HasNowItem(int uid)
        {
            DateTime beginDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd") + " 07:00:00");
            DateTime endDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 02:00:00");
            return this.Where(c => c.Uid == uid && c.OccDate >= beginDate && c.OccDate <= endDate).Count()>0;
        }
    }
}
