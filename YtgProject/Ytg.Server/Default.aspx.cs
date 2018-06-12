using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb
{
    public partial class Default : BasePage
    {
        public int newsid = -1;
        public string title = string.Empty;


        public string cqssc = "javascript:void(0)";   //重庆时时彩
        public string xjssc = "javascript:void(0)";   //新疆时时彩
        public string tjssc = "javascript:void(0)";   //天津时时彩
        public string xyffc = "javascript:void(0)";   //幸运分分彩
        public string xysfc = "javascript:void(0)";   //幸运三分彩
        public string xywfc = "javascript:void(0)";   //幸运五分彩


        public string gd11x5 = "javascript:void(0)";   //广东十一选五
        public string jx11x5 = "javascript:void(0)";   //江西十一选五
        public string sd11x5 = "javascript:void(0)";   //山东使用选五
        public string sf11x5 = "javascript:void(0)";   //三分十一选五
        public string wf11x5 = "javascript:void(0)";   //五分十一选五

        public string fc3d = "javascript:void(0)";    //福彩3D
        public string pl5 = "javascript:void(0)";     //排列三、五
        public string shssl = "javascript:void(0)";   //上海时时乐
        public string xglhc = "javascript:void(0)";   //香港六合彩
        public string jsk3 = "javascript:void(0)";    //江苏快三


        public string ajffc = "javascript:void(0)";   //埃及分分彩
        public string ajefc = "javascript:void(0)";   //埃及二分彩
        public string ajwfc = "javascript:void(0)";   //埃及五分彩
        public string hlssc = "javascript:void(0)";   //河内时时彩
        public string ynssc = "javascript:void(0)";   //印尼时时彩

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            if (!IsPostBack)
            {
                this.BindData();

                if (this.CookUserInfo.UserType != UserType.Main && this.CookUserInfo.UserType != UserType.BasicProy && CookUserInfo.Rebate > 0.2)
                {
                    cqssc = "/lottery.aspx?LotteryCode=cqssc&Id=1&LotteryName=%E9%87%8D%E5%BA%86%E6%97%B6%E6%97%B6%E5%BD%A9&ImageSource=http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281844403.png";   //重庆时时彩
                    xjssc = "/lottery.aspx?LotteryCode=xjssc&Id=4&LotteryName=%E6%96%B0%E7%96%86%E6%97%B6%E6%97%B6%E5%BD%A9&ImageSource=http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281844403.png";   //新疆时时彩
                    tjssc = "/lottery.aspx?LotteryCode=tjssc&Id=5&LotteryName=%E5%A4%A9%E6%B4%A5%E6%97%B6%E6%97%B6%E5%BD%A9&ImageSource=http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281844403.png";   //天津时时彩
                    xyffc = "/lottery.aspx?LotteryCode=yifencai&Id=11&LotteryName=%E5%B9%B8%E8%BF%90%E5%88%86%E5%88%86%E5%BD%A9&ImageSource=http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281844403.png";   //幸运分分彩
                    xysfc = "/lottery.aspx?LotteryCode=yifencai&Id=11&LotteryName=%E5%B9%B8%E8%BF%90%E5%88%86%E5%88%86%E5%BD%A9&ImageSource=http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281844403.png";   //幸运三分彩
                    xywfc = "/lottery.aspx?LotteryCode=yifencai&Id=11&LotteryName=%E5%B9%B8%E8%BF%90%E5%88%86%E5%88%86%E5%BD%A9&ImageSource=http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281844403.png";   //幸运五分彩

                    gd11x5 = "/lottery.aspx?LotteryCode=gd11x5&Id=6&LotteryName=%E5%B9%BF%E4%B8%9C11%E9%80%895&ImageSource=lottery_11_5.png";   //广东十一选五
                    jx11x5 = "/lottery.aspx?LotteryCode=jx11x5&Id=20&LotteryName=%E6%B1%9F%E8%A5%BF11%E9%80%895&ImageSource=lottery_11_5.png";   //江西十一选五
                    sd11x5 = "/lottery.aspx?LotteryCode=sd11x5&Id=19&LotteryName=%E5%B1%B1%E4%B8%9C11%E9%80%895&ImageSource=lottery_11_5.png";   //山东使用选五
                    sf11x5 = "/lottery.aspx?LotteryCode=sf11x5&Id=17&LotteryName=%E4%B8%89%E5%88%8611%E9%80%895&ImageSource=lottery_11_5.png";   //三分十一选五
                    wf11x5 = "/lottery.aspx?LotteryCode=wf11x5&Id=18&LotteryName=%E4%BA%94%E5%88%8611%E9%80%895&ImageSource=lottery_11_5.png";   //五分十一选五

                    fc3d = "/lottery.aspx?LotteryCode=fc3d&Id=7&LotteryName=%E7%A6%8F%E5%BD%A93D&ImageSource=lottery_3d.png";    //福彩3D
                    pl5 = "/lottery.aspx?LotteryCode=pl5&Id=9&LotteryName=%E6%8E%92%E5%88%97%E4%B8%89%E3%80%81%E4%BA%94&ImageSource=http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281844546.png";     //排列三、五
                    shssl = "/lottery.aspx?LotteryCode=shssl&Id=8&LotteryName=%E4%B8%8A%E6%B5%B7%E6%97%B6%E6%97%B6%E4%B9%90&ImageSource=lottery_ssl.png";   //上海时时乐
                    xglhc = "/lottery.aspx?LotteryCode=hk6&Id=21&LotteryName=%E9%A6%99%E6%B8%AF%E5%85%AD%E5%90%88%E5%BD%A9&ImageSource=lottery_xglhc.png";   //香港六合彩
                    jsk3 = "/lottery.aspx?LotteryCode=jsk3&Id=22&LotteryName=%E6%B1%9F%E8%8B%8F%E5%BF%AB%E4%B8%89&ImageSource=lottery_k3.png";    //江苏快三

                    ajffc = "/lottery.aspx?LotteryCode=FFC1&Id=13&LotteryName=%E5%9F%83%E5%8F%8A%E5%88%86%E5%88%86%E5%BD%A9&ImageSource=http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281844403.png";   //埃及分分彩
                    ajefc = "/lottery.aspx?LotteryCode=FFC2&Id=24&LotteryName=%E5%9F%83%E5%8F%8A%E4%BA%8C%E5%88%86%E5%BD%A9&ImageSource=http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281844403.png";   //埃及二分彩
                    ajwfc = "/lottery.aspx?LotteryCode=FFC5&Id=25&LotteryName=%E5%9F%83%E5%8F%8A%E4%BA%94%E5%88%86%E5%BD%A9&ImageSource=http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281844403.png";   //埃及五分彩
                    hlssc = "/lottery.aspx?LotteryCode=VIFFC5&Id=14&LotteryName=%E6%B2%B3%E5%86%85%E6%97%B6%E6%97%B6%E5%BD%A9&ImageSource=http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281844403.png";   //河内时时彩
                    ynssc = "/lottery.aspx?LotteryCode=INFFC5&Id=23&LotteryName=%E5%8D%B0%E5%B0%BC%E6%97%B6%E6%97%B6%E5%BD%A9&ImageSource=http://cfapu.img48.wal8.com/img48/545266_20160510012323/146281844403.png";   //印尼时时彩
                }
            }
        }

        private void BindData()
        {
            INewsService newsServices = IoC.Resolve<INewsService>();
            var result = newsServices.GetAll().OrderByDescending(c => c.OccDate).Take(20).ToList();
            var openNews=result.Where(x => x.IsShow == 1).FirstOrDefault();
            if (null != openNews)
            {
                title = openNews.Title;
                newsid = openNews.Id;
            }
            this.rptnews.DataSource = result;
            this.rptnews.DataBind();
        }
    }
}