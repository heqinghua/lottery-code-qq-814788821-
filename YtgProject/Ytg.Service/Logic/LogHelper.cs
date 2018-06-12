using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.Service.Logic
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 添加登录日志
        /// </summary>
        /// <param name="referenceCode"></param>
        /// <param name="des"></param>
        public static void AddLoginLog(string referenceCode,string des)
        {
            AddLog(0, referenceCode, des);
        }
        
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="type"></param>
        /// <param name="referenceCode"></param>
        /// <param name="des"></param>
        public static void AddLog(int type, string referenceCode, string des)
        {
            SysLog log = new SysLog();
            log.ReferenceCode = referenceCode;
            log.Type = type;
            log.Descript = des;
            log.Ip = Ytg.Comm.Utils.GetIp();
            log.OccDate = DateTime.Now;
            log.UseSource = HttpContext.Current.Request.Params["usesource"];
            log.ServerSystem = Ytg.Comm.Utils.GetUserSystem();
            //判断是否为移动设备访问
            bool isMobile = Utils.IsMobile();
            if (isMobile)
                log.UseSource = "移动设备";

            AddLog(log);
            //修改登录次数
            //ISysUserService userService = IoC.Resolve<ISysUserService>();
            //userService.AppendUserLoginCount(referenceCode);
        }

        public static void AddLog(SysLog log)
        {
            ISysLogService service = IoC.Resolve<ISysLogService>();
            service.Create(log);
            service.Save();
        }

      

        



    }
}
