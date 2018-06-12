
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Comm;
using Ytg.Scheduler.Comm;

namespace Ytg.Scheduler.Tasks.AutoGroupBy
{
    public class Run
    {
        static int sleepValue =Convert.ToInt32( System.Configuration.ConfigurationManager.AppSettings["autogroupsleepval"]);//休眠时间

        public static List<int> testUseridLst = null;//测试用户id集合

        static int UpdateCatchms = 60*10*1000;//10分钟更新一次

        public static void Start() {
            new Thread(Action).Start();
        }
        private static void Action()
        {
            LogManager.Info("启动自动满单服务");
            UpdateCatchUserid();//更新缓存用户id

            int wileCount = 0;
            while (true)
            {
                
                try
                {
                    if (wileCount >= UpdateCatchms)
                    {
                        UpdateCatchUserid();
                    }
                    Logic();
                }
                catch (Exception e)
                {
                    LogManager.Error("Ytg.Scheduler.Tasks.AutoGroupBy-Start", e);
                    Console.WriteLine(e.Message);
                }
                System.Threading.Thread.Sleep(sleepValue);
                wileCount++;
            }
        }

        /// <summary>
        /// 更新用户id缓存
        /// </summary>
        private static void UpdateCatchUserid()
        {
            testUseridLst = LotteryIssuesData.GetCatchUserLst();
        }

        private static void Logic()
        {
            DataTable table = LotteryIssuesData.GetNotGroupByState();
            Console.WriteLine("start--Logic");
            if (table ==null || table.Rows.Count < 0)
                return;
            Console.WriteLine("rowCount=" + table.Rows.Count);
            int[] userArray = new int[testUseridLst.Count];
            testUseridLst.CopyTo(userArray);
            var rdm = new Random();
            foreach (DataRow row in table.Rows)
            {

                int bettid = Convert.ToInt32(row["Id"]);
                decimal totalMonery = Convert.ToDecimal(row["TotalAmt"]);
                double Bili = Convert.ToDouble(row["Bili"]);
                decimal SurplusMonery = Convert.ToDecimal(row["SurplusMonery"]);//剩余认购金额
                                                                                //bet.Id,bet.IssueCode,bet.BetCode,bet.LotteryCode,TotalAmt,Bili,SurplusMonery

               
                decimal bySubscription = SurplusMonery;
                while (SurplusMonery > 0)
                {
                    var itemTotal = totalMonery * 0.5m;
                    if (SurplusMonery > itemTotal)
                    {
                        bySubscription = SurplusMonery * 0.5m;
                    }
                    else {
                        bySubscription = SurplusMonery;
                    }
                    SurplusMonery -= bySubscription;
                    int userIndex = rdm.Next(0, userArray.Length - 1);
                    Console.WriteLine("random index="+ userIndex);
                    int userid = userArray[userIndex];

                    var item = new BuyTogether();
                    item.BetDetailId = bettid;
                    item.BuyTogetherCode = "h" + Utils.BuilderNum();
                    item.UserId = userid;
                    item.Subscription = bySubscription;
                    new Thread(new ParameterizedThreadStart(AddTogetherAction)).Start(item);
                }
            }
        }

        public static void AddTogetherAction(object obj)
        {
            var item = obj as BuyTogether;
            int state = LotteryIssuesData.AddTogether("d" + Utils.BuilderNum(), (int)TradeType.投注扣款, item);
            Console.WriteLine(item.ToString()+ "  state="+ state);
            
        }

        

    }
}
