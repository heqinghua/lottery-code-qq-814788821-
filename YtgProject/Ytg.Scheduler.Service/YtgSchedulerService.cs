using Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm;
using Ytg.Scheduler.Service.BootStrapper;

namespace Ytg.Scheduler.Service
{
    partial class YtgSchedulerService : ServiceBase
    {
        //private readonly ILog logger;
        readonly IScheduler scheduler;

        public YtgSchedulerService()
        {
            InitializeComponent();

            this.scheduler = new SchedulerManager().Initital();
        }

        protected override void OnStart(string[] args)
        {
            scheduler.Start();
        }

        protected override void OnStop()
        {
            scheduler.Shutdown();
        }

        protected override void OnPause()
        {
            scheduler.PauseAll();
        }

        protected override void OnContinue()
        {
            scheduler.ResumeAll();
        }
    }

}
