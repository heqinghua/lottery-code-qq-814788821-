
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Data;
using Ytg.Scheduler.Comm;
using Ytg.Scheduler.Comm.Bets.Calculate;
using Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanSi;
using Ytg.Scheduler.Comm.IssueBuilder;
using Ytg.Scheduler.Service.BootStrapper;
using Ytg.Service.Lott;
using System.Diagnostics;
using System.Windows.Forms;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.BasicModel;
using Ytg.Service;

namespace Ytg.Scheduler.Service
{
    class Program
    {


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main(string[] args)
        {
            // new Generatesd11x5LotteryIssue().Generate(DateTime.Now);

            /*windows service*/
            /*
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new YtgSchedulerService() 
            };
            ServiceBase.Run(ServicesToRun);
            */
            // new GrnerateFc3dLotteryIssue().Generate();
            /*************
             * 开发中多次修改数据实体和数据库结构
             * 加入该句代码可自动同步实体与数据库版本
             * 实际发布中将注释该代码
             * ************/
            /*
           Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr.RenXuanZuXuanDanShi bz = new Comm.Bets.Calculate.Ssc.RenXuanEr.RenXuanZuXuanDanShi();

           var detail = new BetDetail();
           detail.PalyRadioCode = 1547;
           detail.IssueCode = "1001";
           detail.BetContent = bz.HtmlContentFormart("01&02&03&04&05&06&07&08&09&12&13&14&15&16&17&18&19&23&24&25&26&27&28&29&34&35&36&37&38&39&45&47&58&59&67&68&69&78&79&89_45");
           Console.WriteLine(detail.BetContent);
           bz.Calculate("1001", "6,4,4,4,8", detail);
           Console.WriteLine(detail.IsMatch + "  " + detail.WinMoney);
           Console.ReadKey();
           */
           
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<YtgDbContext, YtgDbContextConfiguration>());
            LogManager.Info("同步数据库结构成功!");
            // ISysUserBalanceService mSysUserBalanceService;//用户余额

            Ytg.Scheduler.Tasks.AutoGroupBy.Run.Start();
            
            InintData.Initital();


            //初始化任务数据
            var iScheduler = new SchedulerManager().Initital();
            iScheduler.Start();
            LogManager.Info("scheduler start !");
            LogManager.Info("服务启动成功!");
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmtest());
            Console.Read();

            

        }

        //static void show()
        //{
        //    var db = new DbContextFactory();
        //    Hasher hs = new Hasher();
        //   var mSysUserBalanceService = new SysUserBalanceService(new Repo<SysUserBalance>(db), hs, new SysUserService(new Repo<SysUser>(db), hs));
        //    var item = new SysUserBalanceDetail()
        //    {
        //        OpUserId = 5765,
        //        SerialNo = "d" + Ytg.Comm.Utils.BuilderNum(),
        //        Status = 0,
        //        TradeType = TradeType.投注扣款,
        //        UserId = 5765,
        //        RelevanceNo = "bC9C8026F4A7D25D4"
        //    };

        //    mSysUserBalanceService.UpdateUserBalance(item,100);

        //}




    }
}
