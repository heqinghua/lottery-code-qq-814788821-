using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Scheduler.Comm;
using Ytg.Scheduler.Tasks;

namespace Ytg.ServerWeb
{
    public class SchedulerManager
    {
        private IScheduler scheduler;
        //在这里把需要的Job添加进去
        //获取当前运行程序的路径
      //  readonly string dllPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);


        public IScheduler Initital()
        {

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler();

            string[] assemblyArray = new string[]{
             "Ytg.Scheduler.Tasks"
            };
            foreach (var assembly in assemblyArray)
            {

                var types = Assembly.Load(assembly).GetTypes().Where(item => item.IsClass && typeof(IYtgJob).IsAssignableFrom(item));
                foreach (Type tp in types)
                {
                    var job = tp.Assembly.CreateInstance(tp.FullName) as IYtgJob;
                    scheduler.ScheduleJobs(job.Initital(), true);
                }
            }
            return scheduler;

        }


       
    }
}
