using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel.Manager;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Stat
{
    public partial class JieSuanTable : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Bind();

            }

        }

        public string FilterType()
        {
            if (ViewState["FilterType"] == null)
                return "0";
            return ViewState["FilterType"].ToString();
        }

        private void Bind()
        {
            DateTime? beginDate = null;
            DateTime? endDate = null;
            DateTime b;
            DateTime e;
            if (!DateTime.TryParse(this.txtBeginDate.Text.Trim(), out b) || !DateTime.TryParse(this.txtEndDate.Text.Trim(), out e))
            {
                if (this.FilterType() == "0")
                {
                    //按天
                    //string year = DateTime.Now.ToString("yyyy");
                    //string month = DateTime.Now.ToString("MM");
                    //beginDate = Convert.ToDateTime(year + "-" + month + "-01 03:00:00");
                    //endDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 03:00:00");
                    beginDate = Utils.GetNowBeginDate();//  Convert.ToDateTime(year + "-" + month + "-01 03:00:00");
                    endDate = Utils.GetNowEndDate(); //Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 03:00:00");
                }
                else
                {
                    //年
                    beginDate = DateTime.Now.AddYears(-1);
                    endDate = DateTime.Now;
                }
            }
            else
            {
                beginDate =Convert.ToDateTime(b.ToString("yyyy/MM/dd")+" 03:00:00");
                endDate = Convert.ToDateTime(e.ToString("yyyy/MM/dd") + " 03:00:00");
            }
            ICountDataService dataService = IoC.Resolve<ICountDataService>();
            if (this.FilterType() == "0")
            {
                //按日
                var dayRes=dataService.FilterSp_DayJieSuanTable(beginDate.Value, endDate.Value);
                this.repList.DataSource = dayRes;
                this.repList.DataBind();
                this.repList.Visible = true;
                this.RepeaterMonth.Visible = false;
                this.rptSum.DataSource = SumDayRes(dayRes);
                this.rptSum.DataBind();
            }
            else
            {
                //按月
                var res= dataService.FilterSp_MonthJieSuanTable(beginDate.Value, endDate.Value);
                this.RepeaterMonth.DataSource = res;
                this.RepeaterMonth.DataBind();
                this.repList.Visible = false;
                this.RepeaterMonth.Visible = true;
                this.rptSum.DataSource = SumRes(res);
                this.rptSum.DataBind();
                
            }
        }

        public List<SumEntity> SumDayRes(List<Sp_DayJieSuanTable> res)
        {
            var item = new SumEntity()
            {
                Activity = 0,
                Chongzhi = 0,
                FenHong = 0,
                Jiangjinpaisong = 0,
                QianDao = 0,
                tiXian = 0,
                TouZhu = 0,
                YingKui = 0,
                YongJing = 0,
                Youxifandian = 0,
                ZhuChe = 0,
            };
            foreach (var r in res)
            {
                item.Activity += r.Activity;
                item.Chongzhi += r.Chongzhi;
                item.FenHong += r.FenHong;
                item.Jiangjinpaisong += r.Jiangjinpaisong;
                item.QianDao += r.QianDao;
                item.tiXian += r.tiXian;
                item.TouZhu += r.TouZhu;
                item.YingKui += r.YingKui;
                item.YongJing += r.YongJing;
                item.Youxifandian += r.Youxifandian;
                item.ZhuChe += r.ZhuChe;
            }
            List<SumEntity> s = new List<SumEntity>();
            s.Add(item);
            return s;
        }

        public List<SumEntity> SumRes(List<Sp_YueJieSuanTable> res)
        {
            var item = new SumEntity()
            {
                Activity=0,
                Chongzhi=0,
                FenHong=0,
                Jiangjinpaisong=0,
                QianDao=0,
                tiXian=0,
                TouZhu=0,
                YingKui=0,
                YongJing=0,
                Youxifandian=0,
                ZhuChe=0,
            };
            foreach (var r in res)
            {
                item.Activity += r.Activity;
                item.Chongzhi += r.Chongzhi;
                item.FenHong += r.FenHong;
                item.Jiangjinpaisong += r.Jiangjinpaisong;
                item.QianDao += r.QianDao;
                item.tiXian += r.tiXian;
                item.TouZhu += r.TouZhu;
                item.YingKui += r.YingKui;
                item.YongJing += r.YongJing;
                item.Youxifandian += r.Youxifandian;
                item.ZhuChe += r.ZhuChe;
            }
            List<SumEntity> mewS = new List<SumEntity>();
            mewS.Add(item);
            return mewS;
        }

        protected void lkDay_Click(object sender, EventArgs e)
        {
            ViewState["FilterType"] = "0";
            this.ltTitle.Text = "日结算报表";
            this.ltHead.Text = "统计日期";
            this.Bind();
        }

        protected void lkMon_Click(object sender, EventArgs e)
        {
            ViewState["FilterType"] = "1";
            this.ltTitle.Text = "月结算报表";
            this.ltHead.Text = "统计月份";

            this.Bind();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.Bind();
        }
    }


    public class SumEntity
    {

        /// <summary>
        /// 盈亏
        /// </summary>
        public decimal? YingKui
        {
          get;set;
        }

        /// <summary>
        /// 充值
        /// </summary>
        public decimal? Chongzhi { get; set; }

        /// <summary>
        /// 活动
        /// </summary>
        public decimal? Activity { get; set; }

        /// <summary>
        /// 提现
        /// </summary>
        public decimal? tiXian
        {
           get;set;
        }


        /// <summary>
        /// 投注
        /// </summary>
        public decimal? TouZhu
        {get;set;
        }

        /// <summary>
        /// 返点
        /// </summary>
        public decimal? Youxifandian { get; set; }

        /// <summary>
        /// 奖金
        /// </summary>
        public decimal? Jiangjinpaisong { get; set; }

        /// <summary>
        /// 分红
        /// </summary>
        public decimal? FenHong { get; set; }


        /// <summary>
        /// 签到活动
        /// </summary>
        public decimal? QianDao { get; set; }
        /// <summary>
        /// 注册活动
        /// </summary>
        public decimal? ZhuChe { get; set; }
        /// <summary>
        /// 佣金
        /// </summary>
        public decimal? YongJing { get; set; }
    }
}