using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm
{
   public class TaskTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 编码，这个是唯一的
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 这个是基于Quartz.net 设计的时间
        /// </summary>
        public string TimeCron { get; set; }


        public override string ToString()
        {
            return string.Format("GroupName={0},FullName={1},TimeCron={2}",GroupName,FullName,TimeCron);
        }
    }
}
