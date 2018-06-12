using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    [ServiceContract]
    public interface IActivityService : ICrudService<Activity>
    {
        /// <summary>
        /// 获取所有活动
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<Activity> GetActivitys();

        /// <summary>
        /// 新增活动
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool AddActivity(Activity item);


        /// <summary>
        /// 修改活动
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool EditActivity(Activity item);


        /// <summary>
        /// 删除活动
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool RemoveActivity(int id);

        /// <summary>
        /// 获取单个活动信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Activity GetActivityItem(int id);
    }

}
