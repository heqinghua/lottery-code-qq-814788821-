using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;
using Ytg.ServerWeb.Views.Activity.Dzp;

namespace Ytg.ServerWeb.Views.Activity.QianDao
{
    public partial class QianDao :BasePage
    {
        public List<int> sgins;
        public int MaxDay = 30;
        public decimal UserAmt = 0;
        public bool isautoRefbanner=true;

        protected void Page_Load(object sender, EventArgs e)
        {
            sgins = new List<int>();
            if (!string.IsNullOrEmpty(Request.Params["ajax"]))
            {
                string result = "";
                switch (Request.Params["action"])
                {
                    case "sign":
                        result = Sign();
                        break;
                }

                Response.Write(result);
                Response.End();
                return;
            }

            //ajax抽奖
            if (Request.Params["action"] == "ajx")
            {
                string result = ExRotate();
                Response.Write(result);
                Response.End();
                return;
            }
          
            
            MaxDay = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/")+" 01").AddMonths(1).AddDays(-1).Day;
            if (!IsPostBack)
            {
                BindSgins();
                
            }

            
        }

        private void BindSgins()
        {
            //获取当前用户签到列表
            ISignService signServices = IoC.Resolve<ISignService>();
            sgins = signServices.Where(c => c.Uid == CookUserInfo.Id && c.IsBack==false).ToList().OrderBy(c => c.OccDay).Select(c => c.OccDay).ToList();
            if (sgins == null)
                sgins = new List<int>();
        }



        /// <summary>
        /// 签到
        /// </summary>
        private string Sign()
        {
            int state = 1;
            if (!Utils.HasAvtivityDateTimes())
            {
                state = 5;
                return state.ToString();
            }

           
            //验证当前用户当天投注是否达到条件
            DateTime startTime = Utils.GetNowBeginDate();
            DateTime endTime=Utils.GetNowEndDate();
            IBetDetailService betDetailServices = IoC.Resolve<IBetDetailService>();
            var totalAmt = 0m;
            int[] whereStates = new int[] { 1,2};
            var sumResult = betDetailServices.Where(c =>c.UserId==this.CookUserInfo.Id &&  c.OccDate >= startTime && c.OccDate <= endTime && whereStates.Contains((int)c.Stauts));
            if (sumResult.Any())
                totalAmt = sumResult.Sum(c => c.TotalAmt);
            if (totalAmt > Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["sign"]))
            {
                ISignService signServices = IoC.Resolve<ISignService>();
                //查询今天是否已经签到过
                int occday = Utils.GetActivityOccDay();
                if (signServices.Where(c => c.Uid == CookUserInfo.Id && c.OccDay == occday).Count() < 1)
                {
                    signServices.Create(new BasicModel.Act.Sign()
                    {
                        OccDay = occday,
                        Uid = CookUserInfo.Id
                    });
                    signServices.Save();
                    state = 0;
                    
                }
                else
                {
                    state = 3;
                }
            }
            else
            {
                state = 2;
            }
          
            return state.ToString();
        }

        /// <summary>
        /// 领取奖励  1 为活动未开始 2签到时间未完成要求 -1为异常
        /// </summary>
        /// <returns></returns>
        protected void btnME_Click(object sender, EventArgs e)
        {
            if (this.Master != null) {
                UserAmt = (this.Master as lotterySite).GetUserBalance();
            }
            if (!Utils.HasAvtivityDateTimes())
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('活动时间为每日07:30:00 – 次日凌晨02:00:00！');</script>");
                return;
            }
            //领取奖励,获取当前签到次数
            ISignService signServices = IoC.Resolve<ISignService>();
            int count = signServices.Where(c => c.Uid == CookUserInfo.Id && c.IsBack == false).Count();//当前签到总数
            try
            {

                decimal monery = 0m;//奖励
                if (count >= 28)
                    monery = 188;
                else if (count >= 20)
                    monery = 88;
                else if (count >= 14)
                    monery = 58;
                else if (count >= 7)
                    monery = 18;

                if (monery < 1)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('签到时间未完成要求，领取奖励失败！'); </script>");
                    return;
                }
                var result = signServices.Where(c => c.Uid == CookUserInfo.Id);
                foreach (var item in result)
                {
                    item.IsBack = true;
                    signServices.Save();
                }
                signServices.Save();
                var details = new BasicModel.SysUserBalanceDetail()
                    {
                        RelevanceNo = CookUserInfo.Id.ToString(),
                        SerialNo = "q" + Utils.BuilderNum(),
                        Status = 0,
                        TradeAmt = monery,
                        TradeType = BasicModel.TradeType.签到有你,
                        UserId = CookUserInfo.Id
                    };
                //奖励金额
                ISysUserBalanceService userBalanceServices = IoC.Resolve<ISysUserBalanceService>();
                if (userBalanceServices.UpdateUserBalance(details, monery) > 0)
                {
                    string mesggage = "领取奖金成功，共签到" + count + "天领取金额为：" + monery + "<br/>";
                    mesggage += "感谢你的参与，祝你游戏愉快！";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('" + mesggage + "',2,function(){refchangemonery();});</script>");
                    isautoRefbanner = false;
                }
               
                BindSgins();
                return;
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("btnME_Click", ex);
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('奖励领取失败，请稍后再试！');</script>");
            
            
        }


         
        #region 大转盘
        //总抽奖次数
        public int AllCount = 0;
        //剩余抽奖次数
        public int SubCount = 0;
        //1快
        const int None = 1;
        //5块
        const int QMonery = 5;
        //4快
        const int TenFee = 4;
        //3块
        const int ThirtyFee = 3;
        //2块
        const int ThirtyTwo = 2;

        //开始抽奖
        private string ExRotate()
        {
          
            string result = "";
            //验证今天是否签到
            ISignService signServices = IoC.Resolve<ISignService>();
            //查询今天是否已经签到过
            int occday = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            var fs=signServices.Where(c => c.Uid == CookUserInfo.Id && c.OccDay == occday).FirstOrDefault();
            if (fs!=null)
            {
                if (fs.IsDap)
                {
                    return result = "-3,0,0";//已经抽过了
                }
                fs.IsDap = true;
                signServices.Save();//存储状态
                var iawards = Awards();
                var monery =Convert.ToDecimal(iawards.Name);//抽取的金额 //RechargeConfig.LotteryLevelMonery(Convert.ToInt32(iawards.Name));
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
                result = "1," + iawards.Name + "," + 0;
            }
            else { 
              //今天没签到呢
                result = "-1,0,0";//已经没有抽奖的次数了
            }
            return result + ",0";
        }


        #region 抽奖程序
        static List<Award> rotary = new List<Award>{
            //  new Award{ Name=QMonery.ToString(), Chance=1},
                new Award{ Name=TenFee.ToString(), Chance=1},
                new Award{ Name=ThirtyFee.ToString(), Chance=5},
                new Award{ Name=ThirtyTwo.ToString(), Chance=30},
                new Award{ Name=None.ToString(), Chance=50}, 
                 new Award{ Name=None.ToString(), Chance=50}, 
                new Award{ Name=None.ToString(), Chance=50}, 
                new Award{ Name=None.ToString(), Chance=50}, 
                new Award{ Name=None.ToString(), Chance=50}, 
        };

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

     
        #endregion
    }
}