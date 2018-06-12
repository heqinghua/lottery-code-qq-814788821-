using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Service;
using Ytg.Data;

namespace Ytg.Service.Logic
{
    /// <summary>
    /// 系统配置数据
    /// </summary>
    public class SysSettingHelper
    {
        #region basic

        static List<SysSetting> mSysSettings = null;
        /// <summary>
        /// 获取系统设置信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sysSettingService"></param>
        /// <returns></returns>
        public static SysSetting GetSysSetting(string key)
        {
            if (mSysSettings == null)
            {
                mSysSettings=GetAllSettings();
            }
            return mSysSettings.Where(item => item.Key == key).FirstOrDefault();
        }

        /// <summary>
        /// 获取所有的系统配置信息
        /// </summary>
        /// <returns></returns>
        public static List<SysSetting> GetAllSysSetting()
        {
            if (mSysSettings == null)
            {
                mSysSettings = GetAllSettings();
            }

            return mSysSettings;
        }

        static List<SysSetting> GetAllSettings()
        {
            ISysSettingService sysSettingService = null;
            IDbContextFactory factory = new DbContextFactory();
            sysSettingService = new SysSettingService(new Repo<SysSetting>(factory));

            return sysSettingService.GetAll().ToList();
        }

        #endregion

        #region 获取最大限额

        /// <summary>
        /// 最大限额
        /// </summary>
        public static decimal MaxBettMonery
        {
            get
            {
               return Convert.ToDecimal( GetSysSetting("MaxBettMonery").Value);
            }
        }
        #endregion

        #region 封单提前时间

        /// <summary>
        /// 封单提前秒数
        /// </summary>
        public static int EndSaleMinutes
        {
            get
            {
                return Convert.ToInt32(GetSysSetting("EndSaleMinutes").Value);
            }
        }
        #endregion

    }
}
