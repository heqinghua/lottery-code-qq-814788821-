using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.Views.Activity.TouZhu
{
    public partial class TouZhu : BasePage
    {
        //投注金额配置金额
        static string PercentageValue = System.Configuration.ConfigurationManager.AppSettings["PercentageValue"];

        public decimal UserAmt = 0;
        public bool isautoRefbanner = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                /*
                  07:30:00 – 次日凌晨02:00:00
                  */
                
            }
        }

        protected void btnMe_Click(object sender, EventArgs e)
        {
            if (this.Master != null)
            {
                UserAmt = (this.Master as lotterySite).GetUserBalance();
            }

            if (!Utils.HasAvtivityDateTimes())
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('活动时间为每日07:30:00 – 次日凌晨02:00:00！');</script>");
                return;
            }
            //验证当天是否领取过礼包
            int occDay = Utils.GetActivityOccDay();
            ILargeRotaryService mLargeRotaryService = IoC.Resolve<ILargeRotaryService>();
            var fst = mLargeRotaryService.Where(x => x.ALlCount == occDay && x.Uid==CookUserInfo.Id).FirstOrDefault();
            if (fst != null) {
                Alert("今天已经领取过礼包了，明天再来吧！");
                return;
            }
            DateTime beginDate=Utils.GetNowBeginDate().AddDays(-1);
            DateTime endDate = beginDate.AddDays(1);
            
            //获取用户当日有效投注量
            IBetDetailService detailService = IoC.Resolve<IBetDetailService>();
            var monery = detailService.GetUserBettMonery(this.CookUserInfo.Id, beginDate, endDate);
            if (monery <= 0)
            {
                Alert("没有可领取的礼包，投注未达标！");
                return;
            }
            var dm = GetMonery(monery);
            if (dm <= 0)
            {
                Alert("没有可领取的礼包，投注未达标！");
                return;
            }
            //插入账变
            //存入账号
            var details = new BasicModel.SysUserBalanceDetail()
            {
                RelevanceNo = CookUserInfo.Id.ToString(),
                SerialNo = "q" + Utils.BuilderNum(),
                Status = 0,
                TradeAmt = dm,
                TradeType = BasicModel.TradeType.投注送礼包,
                UserId = CookUserInfo.Id
            };

            //奖励金额
            ISysUserBalanceService userBalanceServices = IoC.Resolve<ISysUserBalanceService>();

            if (userBalanceServices.UpdateUserBalance(details, dm) > 0)
            {
                string message = "领取礼包成功，领取金额为：" + dm + "<br/>";
                message += "昨日投注金额" + string.Format("{0:N2}", monery)+"<br>";
                message += "感谢你的参与，祝你游戏愉快!";
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert(\"" + message + "\",1,function(){refchangemonery();});</script>");

                //添加领取记录
                mLargeRotaryService.Create(new BasicModel.LargeRotary()
                {
                    Uid=CookUserInfo.Id,
                    ALlCount = Utils.GetActivityOccDay()
                });
                mLargeRotaryService.Save();
                isautoRefbanner = false;
            }
            else
            {
                Alert("礼包领取失败，请稍后再试！");
            }
        }

        private decimal GetMonery(decimal bettMonery)
        {
            if (string.IsNullOrEmpty(PercentageValue))
                return 0;
            var array = PercentageValue.Split(',');
            foreach (var ay in array)
            {
                if (string.IsNullOrEmpty(ay))
                    continue;
                var item = ay.Split('|');
                if (item.Length != 2)
                    continue;
                if (bettMonery >= Convert.ToDecimal(item[0]))
                    return Convert.ToDecimal(item[1]);
            }
            return 0;
        }
    }
}