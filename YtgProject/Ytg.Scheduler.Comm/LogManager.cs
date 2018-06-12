using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm
{
    public class LogManager
    {
      

        /// <summary>
        /// info
        /// </summary>
        public static void Info(string content)
        {
            var log = log4net.LogManager.GetLogger("infoLog");
            log.Info(content);
        }

     

        /// <summary>
        /// error
        /// </summary>
        /// <param name="content"></param>
        public static void Error(string content)
        {
            var log = log4net.LogManager.GetLogger("errorLog");
            log.Error(content);
        }
        /// <summary>
        /// error
        /// </summary>
        /// <param name="content"></param>
        public static void Error(string content,Exception ex)
        {
            var log = log4net.LogManager.GetLogger("errorLog");
            log.Error(content,ex);
        }
    }

}
