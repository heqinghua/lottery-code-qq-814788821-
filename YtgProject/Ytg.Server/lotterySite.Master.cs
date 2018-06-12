using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb
{
    public partial class lotterySite : System.Web.UI.MasterPage
    {
        /// <summary>
        /// dengl yonghu xinxi 
        /// </summary>
        public CookUserInfo CookUserInfo { get; set; }

        public string UserAmt = "0.0000";

        public string CustomerServceUrl = string.Empty;//客服连接地址

        public List<LotteryType> LotteryTypes = new List<LotteryType>();

        public string IsMobile = "block";

        public string playStr = "";

        protected override void OnInit(EventArgs e)
        {
            if (!ValidateLoginUser())
            {
                //not login
                Response.Redirect("~/login.html");
                return;
            }

            if (CookUserInfo.UserType == UserType.General)
            {
                lottery_dl.Visible = false;
            }
            //if (CookUserInfo.UserType == UserType.Main 
            //    || CookUserInfo.UserType == UserType.BasicProy)// || CookUserInfo.Rebate <= 0.1 7.8以及以上的用户不允许投注
            //{
            //    lottery_li.Visible = false;
            //}
            //if (CookUserInfo.Rebate<1)
            //{
            //    lottery_game.Visible = false;//非直属，总代无法看到工资
            //}


            //验证是否移动端访问（是：隐藏下载按钮）
            if (Utils.IsMobile())
                IsMobile = "none";
            
        }

        public virtual bool ValidateLoginUser()
        {
            var cookUser = FormsPrincipal<CookUserInfo>.TryParsePrincipal(Request);
            if (null == cookUser || cookUser.UserData == null)
                return false;
            this.CookUserInfo = cookUser.UserData;
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ViewStateMode = System.Web.UI.ViewStateMode.Enabled;
            ISysSettingService settingService = IoC.Resolve<ISysSettingService>();
            if (!IsPostBack)
            {
                //huoqu yong hurt yue    
                //GetUserBalance();
                //获取首页新闻
                //GetTopNews();
                var mobao_pay = settingService.GetSetting("mobao_pay");
                
                if (mobao_pay != null)
                {
                    if (mobao_pay.Value == "0") {
                        playStr = "/Views/pay/PayIndex.aspx";
                    }
                }
                //智付
                var zhifu_pay = settingService.GetSetting("zhifu_pay");
                if (zhifu_pay != null)
                {
                    if(zhifu_pay.Value=="0")
                        playStr = "/Views/pay/zhifu/PayIndex.aspx";
                }
                if (string.IsNullOrEmpty(playStr))
                    playStr = "/Views/pay/Payment.aspx";

                this.InintLotterys();
            }
            //获取在线客服信息
           
            var fs = settingService.GetSetting("KHLJ");
            if (fs != null)
                this.CustomerServceUrl = fs.Value;
        }

        public decimal GetUserBalance()
        {
            if (CookUserInfo == null)
                return 0;
            ISysUserBalanceService balanceService = IoC.Resolve<ISysUserBalanceService>();
            var item = balanceService.GetUserBalance(CookUserInfo.Id);
            if (item.UserAmt >= 0)
            {
                return item.UserAmt;
            }
            return 0;
        }

        /// <summary>
        /// 获取首页公告
        /// </summary>
        private void GetTopNews()
        {
            //INewsService newsServices = IoC.Resolve<INewsService>();
            //var result = newsServices.GetAll().Where(c=>c.IsShow==1).OrderByDescending(c => c.OccDate).ToList();
            //this.rptNews.DataSource = result;
            //this.rptNews.DataBind();

        }

        public string GetLotteryUrl(object lotteryCode)
        {

            string lt = "";
            switch (lotteryCode.ToString())
            {
                case "hk6":
                    lt = "GameLxc";
                    break;
                case "jsk3":
                    lt = "GameK3";
                    break;
                default:
                    lt = "GameCenter";
                    break;
            }
            return lt;
        }

        private void InintLotterys()
        {
            //IGroupNameTypeService groupNameTypeService = IoC.Resolve<IGroupNameTypeService>();
            //ILotteryTypeService lotteryService = IoC.Resolve<ILotteryTypeService>();

            //LotteryTypes = lotteryService.GetEnableLotterys();

            //var groupResult = groupNameTypeService.GetAll().OrderBy(c => c.OrderNo);
            //rptMenus.DataSource = groupResult;
            //rptMenus.DataBind();

        }

        protected void rptMenus_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var gnt = e.Item.DataItem as GroupNameType;
            if (null == gnt)
                return;

            var rptChildre = (e.Item.FindControl("rptChildren") as Repeater);
            rptChildre.DataSource = LotteryTypes.Where(c => c.GroupName == gnt.Id).OrderBy(x => x.Sort);
            rptChildre.DataBind();
        }
    }
}