using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;
using Ytg.ServerWeb.Page.Bank;

namespace Ytg.ServerWeb.Views.Activity.Dzp
{
    public partial class Dzp : BasePage
    {
        //总抽奖次数
        public int AllCount = 0;

        //剩余抽奖次数
        public int SubCount = 0;

        //无任何奖项
        const int None = 107;
        //一等奖
        const int QMonery = 101;
        //二等奖
        const int TenFee = 102;
        //3三等奖
        const int ThirtyFee = 103;
        //4三等奖
        const int Thirtyfirr = 104;
        //5三等奖
        const int Thirtyfore = 105;

        //6三等奖
        const int Thirtyffe = 106;

        public decimal GetMonery(string type)
        {
            double dm =0;
            switch (type)
            {
                case "106":
                    dm = 8.8;
                    break;
                case "105":
                    dm = 18;
                    break;
                case "104":
                    dm = 38;
                    break;
                case "103":
                    dm = 48;
                    break;
                case "102":
                    dm = 68;
                    break;
            }

            return Convert.ToDecimal(dm);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            
            //ajax抽奖
            if (Request.Params["action"] == "ajx")
            {
                string result = ExRotate();
                Response.Write(result);
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
               this.AllCount= this.Inint(ref SubCount);
            }
          

            //if ((DateTime.Now.Hour > 2 && DateTime.Now.Hour < 7))
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$(function(){$.alert('活动时间为每日07:30:00 – 次日凌晨02:00:00！');});</script>");
            //}

        }

        private int Inint(ref int subCount)
        {
            subCount = 0;
            //验证当前用户当天充值
            DateTime startTime = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " 03:00:00");
            DateTime endTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 03:00:00");
            ISysUserBalanceDetailService detailsService = IoC.Resolve<ISysUserBalanceDetailService>();
            
            var totalAmt = 0m;
            var sumResult = detailsService.Where(c => c.TradeType == BasicModel.TradeType.用户充值 && c.OccDate >= startTime && c.OccDate <= endTime && c.UserId == CookUserInfo.Id).OrderByDescending(x=>x.OccDate).FirstOrDefault();
            if (sumResult == null)
                return 0;
           totalAmt = sumResult.TradeAmt;

            decimal splitValue = 200;//RechargeConfig.LotteryMonery();
            if (splitValue < 0 || totalAmt < splitValue)
                return 0;

            AllCount =1;// (int)(totalAmt / splitValue);
            //获取大转盘剩余次数
            ILargeRotaryService mLargeRotaryService = IoC.Resolve<ILargeRotaryService>();
            var item = mLargeRotaryService.GetNowItem(CookUserInfo.Id);
            if (item == null)
            {
                item = new BasicModel.LargeRotary()
                {
                    ALlCount = AllCount,
                    SubCount = 0,
                    Uid = CookUserInfo.Id
                };
                //插入一条记录
                mLargeRotaryService.Create(item);
                mLargeRotaryService.Save();
            }
            else
            {
                //获取当前,修改当前值
                if (item.ALlCount != AllCount)
                    item.ALlCount = AllCount;
                mLargeRotaryService.Save();
            }
            subCount = AllCount - item.SubCount;
            return AllCount;
        }

        //开始抽奖
        private string ExRotate()
        {
           
            string result = "-1";
            ILargeRotaryService mLargeRotaryService = IoC.Resolve<ILargeRotaryService>();
            var item = mLargeRotaryService.GetNowItem(CookUserInfo.Id);
            int lastCount=0;
           
            if (item == null
                || item.SubCount >= item.ALlCount)
            {
                result = "-1,0,0";//已经没有抽奖的次数了
            }
            else
            {
                //插入一次
                item.SubCount += 1;
                mLargeRotaryService.Save();//保存
                lastCount=item.ALlCount-item.SubCount;
                //抽奖结果
                var iawards = Awards();
                if (iawards.Name != None.ToString())
                {
                    var monery = GetMonery(iawards.Name);
                    //存入账号

                    var details = new BasicModel.SysUserBalanceDetail()
                         {
                             RelevanceNo = CookUserInfo.Id.ToString(),
                             SerialNo = "q" + Utils.BuilderNum(),
                             Status = 0,
                             TradeAmt = monery,
                             TradeType = BasicModel.TradeType.幸运大转盘,
                             UserId = CookUserInfo.Id
                         };

                    //奖励金额
                    ISysUserBalanceService userBalanceServices = IoC.Resolve<ISysUserBalanceService>();
                 
                    if (userBalanceServices.UpdateUserBalance(details, monery) > 0)
                    {
                        

                    }
                    result = "1," + iawards.Name+","+lastCount;
                }
                else
                {
                    result = "0,0,"+lastCount;//未中奖
                }
            }
            return result + "," + lastCount;
        }


        #region 抽奖程序
        static List<Award> rotary = new List<Award>{
                new Award{ Name=ThirtyFee.ToString(), Chance=1},
                new Award{ Name=Thirtyfirr.ToString(), Chance=1},
                new Award{ Name=Thirtyfore.ToString(), Chance=5},
                new Award{ Name=Thirtyffe.ToString(), Chance=40},
                new Award{ Name=Thirtyffe.ToString(), Chance=40},
                new Award{ Name=None.ToString(), Chance=50}, 
                new Award{ Name=None.ToString(), Chance=50}, 
                new Award{ Name=None.ToString(), Chance=50}, 

        };

        /**
         //3三等奖
        const int ThirtyFee = 103;
        //4三等奖
        const int Thirtyfirr = 104;
        //5三等奖
        const int Thirtyfore = 105;

        //6三等奖
        const int Thirtyffe = 106;
             */

        static Random Rnd = new Random();
        private static Award Awards()
        {

            return (from x in Enumerable.Range(0, 1000000)  //最多支100万次骰子
                    let p = rotary[Rnd.Next(rotary.Count())]
                    let sz = Rnd.Next(0, 100)
                    where sz < p.Chance
                    select p).First();
        }



        #endregion
    }

    class Award
    {
        public string Name { get; set; }

        public int Chance { get; set; }
    }

}