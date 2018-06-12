using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.wap
{
    public partial class GameCenter : BasePage
    {
        public string JsonData = "";

        public string lottery_methods = "";

        public string NextIssues = string.Empty;//下面未开奖期数

        public string NowIssue = string.Empty;//当前期数

        public string NextDayIssues = string.Empty;//明天的

        public string HisOpenIssue = string.Empty;//历史开奖期号

        //**已开奖结果*/
        public string OpenIssueCode = string.Empty;//开奖期号
        public string OpenResult = string.Empty;//开奖结果
        //**已开奖结果*/

        //前5条开奖数据
        public string Top5OpenResult = "";

        /**
         任选玩法切换
         */
        public string RenXuanHref = ""; //"<a href=\"?type={0}&ltcode={1}&ltid={2}&ico={3}&ln={4}\">{5}</a>";

        readonly string[] notRenXuanids = { "9", "8", "7", "6", "17", "18", "19", "20", "21" };


        public int showType = 0;//0为显示 1为不显示

        protected override void OnInit(EventArgs e)
        {
            this.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            if (!InintLotteryParam())
            {
                // yan zheng shi bai 
                
            }
            base.OnInit(e);
        }

        protected virtual bool InintLotteryParam()
        {
            this.LotteryCode = Request.Params["ltcode"];
            int outlottid;
            if (int.TryParse(Request.Params["ltid"], out outlottid))
                LotteryId = outlottid;


            return !string.IsNullOrEmpty(this.LotteryCode) && LotteryId > 0;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BuilderDataJSon();
                GetIssueCode();

                //if (!this.VdaLotterySacllTime())
                //{
                //    Alert();
                //}
                if (CookUserInfo.Rebate <1)
                {
                    showType = 1;
                }
               
            }

        }

        /// <summary>
        /// 验证彩票种类是否在销售时间段类
        /// </summary>
        private bool VdaLotterySacllTime(int lotteryId)
        {
            var lotteryTypeService = IoC.Resolve<ILotteryTypeService>();
            var item = lotteryTypeService.Get(lotteryId);
            if (item == null)
                return false;
            var beginTime = item.BeginScallDate;
            var endTime = item.endSAcallDate;
            if (null == beginTime || null == endTime)
            {
                return true;
            }
            if (null == beginTime || endTime == null)
                return true;
            TimeSpan nowSpan = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
            return nowSpan >= beginTime && nowSpan <= endTime;
        }

        /// <summary>
        /// 组织结构JSON对象
        /// </summary>
        public virtual void BuilderDataJSon()
        {
            string type = Request.Params["type"];//当前玩法类型， 普通玩法还是任选玩法  renxuan 为任选玩法

            //string key = this.LotteryId.ToString();

            //JsonData = Ytg.ServerWeb.BootStrapper.RadioJsonCatch.CreateIntance().Get(key);
            //if (string.IsNullOrEmpty(JsonData))
            JsonData = Ytg.ServerWeb.BootStrapper.LotterySturctHelper.Builder(this.LotteryId, this.LotteryCode, CookUserInfo.Rebate, (int)CookUserInfo.PlayType, "-1", ref lottery_methods);
        }

        /// <summary>
        /// 获取当前期数
        /// </summary>
        private void GetIssueCode()
        {
            ILotteryIssueService issueService = IoC.Resolve<ILotteryIssueService>();
            var result = issueService.GetOccDayNoOpenLotteryIssue(this.LotteryId);
            if (result == null || result.Count() < 1)
            {
                Alert("获取基础数据失败，请刷新后重试！");//基础数据失败,
                return;
            }
            /**移除历史开奖数据，抓取最后一条有开奖结果的数据*/
            var hisResult = result.Where(x => x.tp == 1).Reverse().ToList();//获取历史开奖数据
            /****/
            LotteryIssueDTO pre = hisResult.FirstOrDefault();
            for (var i = 0; i < hisResult.Count; i++)
            {
                var his = hisResult[i];
                if (pre == null || string.IsNullOrEmpty(pre.Result))
                {
                    if (!string.IsNullOrEmpty(his.Result))
                        pre = his;
                }

                result.Remove(his);
            }
            //获取上期开奖结果
            this.OpenIssueCode = pre.IssueCode;
            this.OpenResult = pre.Result;// this.GetOpenResult(pre.IssueCode);
            //
            GetTop5OpenResult(this.OpenIssueCode);
            // result.Remove(pre);//移除掉已开奖数据
            if (result.Count < 1)
            {
                Alert("获取期数基础信息失败，请刷新后重试！");
                return;
            }
            var nowIssueInfo = result[0];//为当前期
            this.NowIssue = "{issue:'" + nowIssueInfo.IssueCode + "',scendtime:'" + nowIssueInfo.EndSaleTime.ToString("yyyy-MM-dd HH:mm:ss") + "',endtime:'" + nowIssueInfo.EndTime.ToString("yyyy-MM-dd HH:mm:ss") + "'}";//{ issue: '20150615-108', endtime: '2015-06-15 22:59:30' },
            //移除掉第一期
            //result.Remove(nowIssueInfo);

            //接下来期数
            StringBuilder builder = new StringBuilder();
            foreach (var item in result)
            {
                builder.Append("{issue:'" + item.IssueCode + "',scendtime:'" + item.EndSaleTime.ToString("yyyy-MM-dd HH:mm:ss") + "',endtime:'" + item.EndTime.ToString("yyyy-MM-dd HH:mm:ss") + "'},");
            }

            this.NextIssues = builder.ToString();
            if (!string.IsNullOrEmpty(this.NextIssues))
                this.NextIssues = this.NextIssues.Substring(0, this.NextIssues.Length - 1);
            else
                this.NextIssues = this.NowIssue;

            //获取下一天彩票期数
            DateTime nextDay = DateTime.Now.AddDays(1);
            string nextDate = nextDay.ToString("yyyy/MM/dd");
            string catchKey = nextDate + this.LotteryId;
            if (Cache[catchKey] == null)
            {
                DateTime beginDate = Convert.ToDateTime(nextDate + " 00:04:20");
                DateTime endDate = Convert.ToDateTime(nextDay.AddDays(1).ToString("yyyy/MM/dd") + " 00:00:59");
                var tomIssues = issueService.GetLotteryIssues(this.LotteryId, beginDate, endDate, -1);
                if (null != tomIssues && tomIssues.Count() > 0)
                {
                    //明天
                    builder = new StringBuilder();
                    foreach (var item in tomIssues)
                    {
                        builder.Append("{issue:'" + item.IssueCode + "',scendtime:'" + item.EndSaleTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',endtime:'" + item.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'},");
                    }
                    string tomStr = builder.ToString();
                    if (!string.IsNullOrEmpty(tomStr))
                        Cache[catchKey] = tomStr.Substring(0, tomStr.Length - 1);
                }
            }

            NextDayIssues = Cache[catchKey] == null ? "" : Cache[catchKey].ToString();

        }




        /// <summary>
        /// 获取开奖结果
        /// </summary>
        private string GetOpenResult(string issue)
        {
            ILotteryIssueService lotteryIssueService = IoC.Resolve<ILotteryIssueService>();
            var item = lotteryIssueService.GetIssueOpenResult(issue, this.LotteryId);
            GetTop5OpenResult(issue);
            if (null != item)
            {
                return item.Result;
            }
            return string.Empty;
        }


        private void GetTop5OpenResult(string notOpenIssue)
        {
            ILotteryIssueService lotteryIssueService = IoC.Resolve<ILotteryIssueService>();
            var opens = lotteryIssueService.GetTop5OpendIssue(this.LotteryId);
            var nt = opens.Where(c => c.IssueCode == notOpenIssue).FirstOrDefault();
            if (nt != null)
                opens.Remove(nt);
            StringBuilder builder = new StringBuilder();
            foreach (var item in opens)
            {
                builder.Append("<ul>");
                builder.Append("<li>" + item.IssueCode + "</li>");
                builder.Append("<li class='haoma1'>");
                if (!string.IsNullOrEmpty(item.Result))
                {
                    var openArray = item.Result.Split(',');
                    foreach (var o in openArray)
                    {
                        builder.Append("<span>" + o + "</span>");
                    }
                }
                builder.Append("</li>");
                builder.Append("</ul>");
            }
            Top5OpenResult = builder.ToString();
        }
    }
}