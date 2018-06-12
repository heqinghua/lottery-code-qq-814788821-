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
    /// 活动
    /// </summary>
    public class ActivityService : CrudService<Activity>, IActivityService
    {

        public ActivityService(IRepo<Activity> repo)
            : base(repo)
        {

        }

        public IList<Activity> GetActivitys()
        {
            return this.Where(c => c.EndDate >= DateTime.Now && c.States == 0).ToList();
        }

        public Activity GetActivityItem(int id)
        {
            return this.Get(id);
        }

        public bool AddActivity(Activity item)
        {
            Activity ac = new Activity();
            ac.ActivityName = item.ActivityName;
            ac.ActivityUrl = item.ActivityUrl;
            ac.BeginDate = item.BeginDate;
            ac.EndDate = item.EndDate;
            ac.Remark = item.Remark;
            ac.States = item.States;
            this.Create(ac);
            this.Save();
            return true;
        }

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool EditActivity(Activity item)
        {
            if (item == null)
                return false;
            var upItem= this.Get(item.Id);
            upItem.ActivityName = item.ActivityName;
            upItem.ActivityUrl = item.ActivityUrl;
            upItem.BeginDate = item.BeginDate;
            upItem.EndDate = item.EndDate;
            upItem.Remark = item.Remark;
            upItem.States = item.States;
            this.Save();
            return true;
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveActivity(int id)
        {
            this.Delete(id);
            return true;
        }
    }
}
