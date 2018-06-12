using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 系统配置信息数据服务
    /// </summary>
    [ServiceContract]
    public interface ISysSettingService : ICrudService<SysSetting>
    {
        /// <summary>
        /// 获取系统配置列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<SysSetting> SeleteAll();

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool Update(IList<SysSetting> items);


          /// <summary>
        /// 获取配置项
        /// </summary>
        /// <returns></returns>
        SysSetting GetSetting(string key);
    }
}
