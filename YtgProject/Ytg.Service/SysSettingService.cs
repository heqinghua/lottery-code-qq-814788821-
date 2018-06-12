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
    /// 系统设置
    /// </summary>
    public class SysSettingService : CrudService<SysSetting>, ISysSettingService
    {
        public SysSettingService(IRepo<SysSetting> repo)
            : base(repo)
        {

        }

        /// <summary>
        /// 获取系统配置列表
        /// </summary>
        /// <returns></returns>
        public IList<SysSetting> SeleteAll()
        {
            return this.GetAll().ToList();
        }

        /// <summary>
        /// 修改系统配置
        /// </summary>
        /// <returns></returns>
        public bool Update(IList<SysSetting> items)
        {            
            if (null == items && items.Count > 0)
                return false;

            var query = this.GetAll().ToList();

            foreach (var item in query)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Key == item.Key)
                    {
                        item.Value = items[i].Value;
                        break;
                    }
                }
            }

            this.Save();
            return true;
        }

        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <returns></returns>
        public SysSetting GetSetting(string key)
        {
            string sql = "select * from SysSettings where [key]='" + key + "'";
            return this.GetSqlSource<SysSetting>(sql).FirstOrDefault();
        }
    }
}
