using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel.Manager;
using Ytg.BasicModel.Report;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Service;

namespace Utg.ServerWeb.Admin.pages.Stat
{
    public partial class CountData : BasePage
    {
        public UserData mUserData;

        public string mStatDataStr = string.Empty;//统计图

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.txtBeginDate.Text = Utils.GetNowBeginDateStr();
                //this.txtEndDate.Text = Utils.GetNowEndDateStr();
                BindData();
                this.ltLoginCode.Text = LoginUser.Code;
                //显示IP
                if (!string.IsNullOrEmpty(LoginUser.NikeName))
                {
                    //curLoginIp+","+preLoginIp,
                    var array = LoginUser.NikeName.Split(',');
                    if (array.Length == 2)
                    {
                        this.lbIp.Text = array[0];
                        this.lbPreIp.Text = array[1];
                    }
                }
                this.lbPreLoginTime.Text = LoginUser.Sex;


                //设置用户角色
                IAccountRoleService accountRoleService = IoC.Resolve<IAccountRoleService>();
                var item = accountRoleService.GetUserRoleName(this.LoginUser.Code).FirstOrDefault();
                if (null != item)
                {
                    ltRole.Text = item.Name;
                }
            }
        }

        private void BindData()
        {

            string beginDate =Utils.GetNowBeginDateStr(); //业务开始日期
            string endDate = Utils.GetNowEndDateStr();     //业务截止日期

            ICountDataService countDataService = IoC.Resolve<ICountDataService>();
            var result = countDataService.GetCountData(beginDate, endDate);
            if (result == null)
                return;
            //盈亏统计
//            result.yesterDayData
            
            //统计
            this.mUserData = result.userData;
            if (this.mUserData == null)
                mUserData = new UserData();

            //rptList.DataSource = result.playTypeRadioDataList;//玩法
            //rptList.DataSource = result.lotteryDataList;//彩种
            //rptList.DataBind();

            //彩票投注金额
            rptlotteryType.DataSource = result.lotteryTypeDataList.OrderByDescending(x=>x.BetMoney);//彩票投注
            rptlotteryType.DataBind();

            //树图
            //mStatDataStr
            StringBuilder builder = new StringBuilder();
            foreach (var item in result.toDayData) {
                string period = item.OccDate;
                builder.Append("{");
                builder.AppendFormat("period:'{0}',yk: {1},tz:{2}", period, item.ProfitAndLossMoney, item.BetMoney);
                builder.Append("},");
            }           
            mStatDataStr = builder.ToString();
            if (mStatDataStr.EndsWith(","))
            {
                mStatDataStr ="{data:["+ mStatDataStr.Substring(0, mStatDataStr.Length - 1)+"]}";
            }

            BindYingKuiStat();//盈亏报表
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //判断里层repeater处于外层repeater的哪个位置（ AlternatingItemTemplate，FooterTemplate，  
            //HeaderTemplate，，ItemTemplate，SeparatorTemplate）  
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rep = e.Item.FindControl("rptList1") as Repeater;//找到里层的repeater对象  
                rep.DataSource = ((LotteryData)e.Item.DataItem).playTypeRadioDataList.OrderByDescending(x=>x.BetMoney);//找到分类Repeater关联的数据项   
                rep.DataBind();
            }  
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            BindData();
        }


        /// <summary>
        /// 盈亏统计
        /// </summary>
        private void BindYingKuiStat()
        {
            List<Sp_DayJieSuanTable> showResult = new List<Sp_DayJieSuanTable>();
            ICountDataService dataService = IoC.Resolve<ICountDataService>();
            //获取今日盈亏统计
            DateTime beginDate = Utils.GetNowBeginDate();
            DateTime endDate = Utils.GetNowEndDate();
            var nowResult = dataService.FilterSp_DayJieSuanTable(beginDate, endDate);
            if (null != nowResult && nowResult.Count > 0)
            {
                var nowStat= this.EmptySp_DayJieSuanTable("今日统计");
                foreach (var item in nowResult)
                {
                    nowStat.Activity += item.Activity;
                    nowStat.Chongzhi += item.Chongzhi;
                    nowStat.FenHong += item.FenHong;
                    nowStat.Jiangjinpaisong += item.Jiangjinpaisong;
                    nowStat.QianDao += item.QianDao;
                    nowStat.tiXian += item.tiXian;
                    nowStat.TouZhu += item.TouZhu;
                    nowStat.YingKui += item.YingKui;
                    nowStat.YongJing += item.YongJing;
                    nowStat.Youxifandian += item.Youxifandian;
                    nowStat.ZhuChe += item.ZhuChe;
                }
                showResult.Add(nowStat);
            }
            else
            {
                showResult.Add(EmptySp_DayJieSuanTable("今日统计"));
            }
            //昨日统计
            var preResult = dataService.FilterSp_DayJieSuanTable(beginDate.AddDays(-1), endDate.AddDays(-1));
            if (null != preResult && preResult.Count > 0)
            {

                var nowStat = this.EmptySp_DayJieSuanTable("昨日统计");
                foreach (var item in preResult)
                {
                    nowStat.Activity += item.Activity;
                    nowStat.Chongzhi += item.Chongzhi;
                    nowStat.FenHong += item.FenHong;
                    nowStat.Jiangjinpaisong += item.Jiangjinpaisong;
                    nowStat.QianDao += item.QianDao;
                    nowStat.tiXian += item.tiXian;
                    nowStat.TouZhu += item.TouZhu;
                    nowStat.YingKui += item.YingKui;
                    nowStat.YongJing += item.YongJing;
                    nowStat.Youxifandian += item.Youxifandian;
                    nowStat.ZhuChe += item.ZhuChe;
                }
                showResult.Add(nowStat);
            }
            else
            {
                showResult.Add(EmptySp_DayJieSuanTable("昨日统计"));
            }
            //当月统计
            beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 3, 0).AddDays(-1);//当月第一天
            endDate = beginDate.AddMonths(1).AddDays(-1);
            var nowMonthResult = dataService.FilterSp_MonthJieSuanTable(beginDate, endDate);
            if (null != nowMonthResult && nowMonthResult.Count > 0)
            {
                var nowStat = this.EmptySp_DayJieSuanTable("当月统计");
                foreach (var item in nowMonthResult)
                {
                    nowStat.Activity += item.Activity;
                    nowStat.Chongzhi += item.Chongzhi;
                    nowStat.FenHong += item.FenHong;
                    nowStat.Jiangjinpaisong += item.Jiangjinpaisong;
                    nowStat.QianDao += item.QianDao;
                    nowStat.tiXian += item.tiXian;
                    nowStat.TouZhu += item.TouZhu;
                    nowStat.YingKui += item.YingKui;
                    nowStat.YongJing += item.YongJing;
                    nowStat.Youxifandian += item.Youxifandian;
                    nowStat.ZhuChe += item.ZhuChe;
                }
                showResult.Add(nowStat);
            }
            else
            {
                showResult.Add(EmptySp_DayJieSuanTable("当月统计"));
            }

            beginDate = beginDate.AddMonths(-1);
            endDate = endDate.AddMonths(-1);
            //上月统计
            var preMonthResult = dataService.FilterSp_MonthJieSuanTable(beginDate, endDate);
            if (null != preMonthResult && preMonthResult.Count > 0)
            {
                var nowStat = this.EmptySp_DayJieSuanTable("上月统计");
                foreach (var item in preMonthResult)
                {
                    nowStat.Activity += item.Activity;
                    nowStat.Chongzhi += item.Chongzhi;
                    nowStat.FenHong += item.FenHong;
                    nowStat.Jiangjinpaisong += item.Jiangjinpaisong;
                    nowStat.QianDao += item.QianDao;
                    nowStat.tiXian += item.tiXian;
                    nowStat.TouZhu += item.TouZhu;
                    nowStat.YingKui += item.YingKui;
                    nowStat.YongJing += item.YongJing;
                    nowStat.Youxifandian += item.Youxifandian;
                    nowStat.ZhuChe += item.ZhuChe;
                }
                showResult.Add(nowStat);
            }
            else
            {
                showResult.Add(EmptySp_DayJieSuanTable("上月统计"));
            }

            this.repListYingKui.DataSource = showResult;
            this.repListYingKui.DataBind();
        }

        private Sp_DayJieSuanTable EmptySp_DayJieSuanTable(string title)
        {
            return new Sp_DayJieSuanTable(false)
                    {
                        OccDay = title,
                        Activity = 0.0000m,
                        Chongzhi = 0.0000m,
                        FenHong = 0.0000m,
                        Jiangjinpaisong = 0.0000m,
                        QianDao = 0.0000m,
                        tiXian = 0.0000m,
                        TouZhu = 0.0000m,
                        YingKui = 0.0000m,
                        YongJing = 0.0000m,
                        Youxifandian = 0.0000m,
                        ZhuChe = 0.0000m,
                    };
        }
    }
}