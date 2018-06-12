using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Stat
{
    public partial class ProfitLossList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                this.txtBegin.Text = Utils.GetNowBeginDateStr();
                this.txtEnd.Text = Utils.GetNowEndDateStr();
                hidUserId.Value = "-1";
                Bind();
            }
        }

        private void Bind()
        {
            DateTime begin;
            DateTime end;
            if (!DateTime.TryParse(this.txtBegin.Text.Trim(), out begin) || !DateTime.TryParse(this.txtEnd.Text.Trim(), out end))
            {
                begin = Utils.GetNowBeginDate();
                end = Utils.GetNowEndDate();
            }

            int userid = Convert.ToInt32(this.hidUserId.Value);
            string userCode = txtUserCode.Text.Trim();

            ISysUserBalanceDetailService detailService = IoC.Resolve<ISysUserBalanceDetailService>();
            int totalCount = 1;
            var result = detailService.SelectProfitLossByManager(userid, userCode, begin, end, 1, 999, ref totalCount);
            //整理数据
            ProfitLossDTOMan man = new ProfitLossDTOMan();
            foreach (var item in result)
            {
                if (userid != -1 && item.Id != userid)
                {
                    man.Chongzhi += item.Chongzhi??0;
                    man.Tixian += item.Tixian ?? 0;
                    man.Touzhu += item.Touzhu ?? 0;
                    man.Zhuihaokoukuan += item.Zhuihaokoukuan ?? 0;
                    man.Zhuihaofankuan += item.Zhuihaofankuan??0;
                    man.Youxifandian += item.Youxifandian ?? 0;
                    man.Jiangjinpaisong += item.Jiangjinpaisong ?? 0;
                    man.Chedanfankuan += item.Chedanfankuan ?? 0;
                    man.Chedanshouxufei += item.Chedanshouxufei ?? 0;
                    man.Chexiaofandian += item.Chexiaofandian ?? 0;
                    man.Chexiaopaijiang += item.Chexiaopaijiang ?? 0;
                    man.Chongzhikoufei += item.Chongzhikoufei ?? 0;
                    man.ShangjiChongzhi += item.ShangjiChongzhi ?? 0;
                    man.Huodonglijin += item.Huodonglijin ?? 0;
                    man.Fenhong += item.Fenhong ?? 0;
                    man.Qita += item.Qita ?? 0;
                    man.TiXianShiBai += item.TiXianShiBai ?? 0;
                    man.CheXiaoTiXian += item.CheXiaoTiXian ?? 0;
                    man.ManZheng += item.ManZheng ?? 0;
                    man.QianDao += item.QianDao ?? 0;
                    man.ZhuChe += item.ZhuChe ?? 0;
                    man.ChongZhiActivity += item.ChongZhiActivity ?? 0;
                    man.YongJing += item.YongJing ?? 0;
                    man.XinYunDaZhuanPan += item.XinYunDaZhuanPan ?? 0;
                }
            }

            if (userid != -1)
            {
                var fs= result.Where(x => x.Id == userid).FirstOrDefault();
                if (null != fs && result.Count>1)
                {
                    fs.Chongzhi -= man.Chongzhi;
                    fs.Tixian -= man.Tixian;
                    fs.Touzhu -= man.Touzhu;
                    fs.Zhuihaokoukuan -= man.Zhuihaokoukuan;
                    fs.Zhuihaofankuan -= man.Zhuihaofankuan;
                    fs.Youxifandian -= man.Youxifandian;
                    fs.Jiangjinpaisong -= man.Jiangjinpaisong;
                    fs.Chedanfankuan -= man.Chedanfankuan;
                    fs.Chedanshouxufei -= man.Chedanshouxufei;
                    fs.Chexiaofandian -= man.Chexiaofandian;
                    fs.Chexiaopaijiang -= man.Chexiaopaijiang;
                    fs.Chongzhikoufei -= man.Chongzhikoufei;
                    fs.ShangjiChongzhi -= man.ShangjiChongzhi;
                    fs.Huodonglijin -= man.Huodonglijin;
                    fs.Fenhong -= man.Fenhong;
                    fs.Qita -= man.Qita;
                    fs.TiXianShiBai -= man.TiXianShiBai;
                    fs.CheXiaoTiXian -= man.CheXiaoTiXian;
                    fs.ManZheng -= man.ManZheng;
                    fs.QianDao -= man.QianDao;
                    fs.ZhuChe -= man.ZhuChe;
                    fs.ChongZhiActivity -= man.ChongZhiActivity;
                    fs.YongJing -= man.YongJing;
                    fs.XinYunDaZhuanPan -= man.XinYunDaZhuanPan;
                }
            }



            this.repList.DataSource = result;
            this.repList.DataBind();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.hidUserId.Value="-1";
            this.Bind();
        }

        protected void children_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
                return;
            this.txtUserCode.Text = string.Empty;
            this.hidUserId.Value = e.CommandArgument.ToString();//查询子统计
            this.Bind();
        }



        public string ShowChrenden(object id)
        {
            if (id == null)
                return string.Empty;
            if (id.ToString() == this.hidUserId.Value)
                return "display:none;";

            return string.Empty;
        }

        public string GetShangJiChongZhi(object shangJichongzhi, object chongzhiKoufei)
        {
            if (shangJichongzhi == null || chongzhiKoufei == null)
                return "0.00";
            if (this.hidUserId.Value == "-1")
                return chongzhiKoufei.ToString();
            return (Convert.ToDecimal(shangJichongzhi) + Convert.ToDecimal(chongzhiKoufei)).ToString();
        }
      
    }
}