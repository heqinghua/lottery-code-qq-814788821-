using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;

namespace Utg.ServerWeb.Admin.pages
{
    public partial class SysSettings : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                this.Bind();
            }
        }

        private void Bind()
        {
            ISysSettingService sysSettingService = IoC.Resolve<ISysSettingService>();
            var rList = sysSettingService.SeleteAll();

            if (rList != null && rList.Count > 0)
            {
                foreach (var item in rList)
                {
                    switch (item.Key)
                    {
                        //平台名称
                        case "PTMC":
                            this.txtTitle.Text = item.Value;
                            break;

                        //网站开关
                        case "WZKG":
                            drpIsShowDialog.SelectedValue = item.Value;
                            break;
                        //网站关闭公告内容
                        case "WZGGKG":
                            this.txtContent.Text = item.Value;
                            break;
                        //中奖排行开关
                        case "ZJPHKG":
                            drpShangBanOpen.SelectedValue = item.Value;
                            break;
                        //会员上榜最低中奖金额
                        case "HYSPZDZJJE":
                            this.txtMinMonery.Text = item.Value;
                            break;
                        //虚拟上榜会员昵称
                        case "XNSPHYNC":
                            this.txtXuNiInContennt.Text = item.Value;
                            break;
                        case "CZXZ"://充值限制
                            if (!string.IsNullOrEmpty(item.Value)) {
                                var ary=item.Value.Split(',');
                                this.txtInMinMonery.Text = ary[0];
                                this.txtInMaxMonery.Text = ary[1];
                            }
                            break;
                        case "TXXZ"://提款限制
                            if (!string.IsNullOrEmpty(item.Value))
                            {
                                var ary = item.Value.Split(',');
                                this.txtOutMinMonery.Text = ary[0];
                                this.txtOutMaxMonery.Text = ary[1];
                            }
                            break;
                        case "ZCZSHD": //充值赠送活动
                            if (!string.IsNullOrEmpty(item.Value))
                            {
                                var des = Newtonsoft.Json.JsonConvert.DeserializeObject<SettingDTO>(item.Value);
                                if (null != des)
                                {
                                    chkopenZengSong.Checked = des.p1 == "0";//0为开启
                                    txtNewUserZenSong.Text = des.p2;
                                    txtNewUserBeiShu.Text = des.p3;
                                }
                            }
                            break;
                        case "chongzhiBili"://充值提款消费比例
                            if (!string.IsNullOrEmpty(item.Value))
                            {
                                txtRechangeMinBili.Text = item.Value;
                            }
                            break;
                        case "ZXLTPATH"://跳转地址
                            this.txtUrl.Text = string.IsNullOrEmpty(item.Value)?"http://":item.Value;
                            break;
                        case "KHLJ":
                            this.txtKfAddress.Text = string.IsNullOrEmpty(item.Value) ? "http://" : item.Value;
                            break;
                        case "QLZHGZ": //提现审核
                            this.txtShMonery.Text = item.Value;
                            break;
                        case "mobao_pay": //摩宝支付
                            this.drpMb.SelectedValue=item.Value;
                            break;
                        case "zhifu_pay": //智付支付
                            this.drpzhifu.SelectedValue = item.Value;
                            break;
                        case "my18_pay": //ny18
                            this.drpMy18.SelectedValue = item.Value;
                            break;
                        case "ti_xian_isopen": //是否开启提现
                            this.drptx.SelectedValue = item.Value;
                            break;
                        case "ti_xian_shi_bai_info": //关闭提现功能原因
                            this.txtTxErrorMsg.Text = item.Value;
                            break;
                        case "zhb_rect_url":
                            this.lbzfb.Text = item.Value;
                            break;
                        case "wx_rect_url":
                            this.lbwx.Text = item.Value;
                            break;
                    }


                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ISysSettingService sysSettingService = IoC.Resolve<ISysSettingService>();

            List<SysSetting> sysSettingList = new List<SysSetting>();
            //平台名称
            SysSetting sysSetting = new SysSetting();
            sysSetting.Key = "PTMC";
            sysSetting.Value = this.txtTitle.Text;
            sysSettingList.Add(sysSetting);

            //网站开关
            SysSetting sysSetting1 = new SysSetting();
            sysSetting1.Key = "WZKG";
            sysSetting1.Value = drpIsShowDialog.SelectedValue;
            sysSettingList.Add(sysSetting1);

            //网站公告开关
            SysSetting sysSetting2 = new SysSetting();
            sysSetting2.Key = "WZGGKG";
            sysSetting2.Value = this.txtContent.Text;
            sysSettingList.Add(sysSetting2);

            //中奖排行开关
            SysSetting sysSetting3 = new SysSetting();
            sysSetting3.Key = "ZJPHKG";
            sysSetting3.Value = drpShangBanOpen.SelectedValue;
            sysSettingList.Add(sysSetting3);


            //会员上榜最低中奖金额
            SysSetting sysSetting4 = new SysSetting();
            sysSetting4.Key = "HYSPZDZJJE";
            sysSetting4.Value = this.txtMinMonery.Text;
            sysSettingList.Add(sysSetting4);

            //虚拟上榜会员昵称
            CreateSettingItem("XNSPHYNC", this.txtXuNiInContennt.Text, sysSettingList);
            //充值限制
            SysSetting sysSetting5 = new SysSetting();
            sysSetting5.Key = "CZXZ";
            sysSetting5.Value = (string.IsNullOrEmpty(this.txtInMinMonery.Text.Trim()) ? "10" : this.txtInMinMonery.Text.Trim()) + "," + (string.IsNullOrEmpty(this.txtInMaxMonery.Text.Trim()) ? "50000" : this.txtInMaxMonery.Text.Trim());
            sysSettingList.Add(sysSetting5);
            //提现限制
            SysSetting sysSetting6 = new SysSetting();
            sysSetting6.Key = "TXXZ";
            sysSetting6.Value = (string.IsNullOrEmpty(this.txtOutMinMonery.Text.Trim()) ? "100" : this.txtOutMinMonery.Text.Trim()) + "," + (string.IsNullOrEmpty(this.txtOutMaxMonery.Text.Trim()) ? "50000" : this.txtOutMaxMonery.Text.Trim());
            sysSettingList.Add(sysSetting6);
            //注册赠送活动
            SettingDTO settingDto = new SettingDTO()
            {
                p1 = this.chkopenZengSong.Checked ? "0" : "1",
                p2 = txtNewUserZenSong.Text.Trim(),
                p3 = txtNewUserBeiShu.Text.Trim(),
            };
            var des = Newtonsoft.Json.JsonConvert.SerializeObject(settingDto);
            SysSetting sysSetting7 = new SysSetting();
            sysSetting7.Key = "ZCZSHD";
            sysSetting7.Value = des;
            sysSettingList.Add(sysSetting7);

            double outBili=5;
            if (double.TryParse(this.txtRechangeMinBili.Text.Trim(), out outBili))
            {
                SysSetting sysSetting8 = new SysSetting();
                sysSetting8.Key = "chongzhiBili";
                sysSetting8.Value = outBili.ToString();
                sysSettingList.Add(sysSetting8);
            }

             //登录失败时跳转
            SysSetting sysSetting9 = new SysSetting();
            sysSetting9.Key = "ZXLTPATH";
            sysSetting9.Value = this.txtUrl.Text.Trim();
            sysSettingList.Add(sysSetting9);
           

            //客服地址
            SysSetting sysSetting10 = new SysSetting();
            sysSetting10.Key = "KHLJ";
            sysSetting10.Value = this.txtKfAddress.Text.Trim();
            sysSettingList.Add(sysSetting10);

            //提现审核
            SysSetting sysSetting11 = new SysSetting();
            sysSetting11.Key = "QLZHGZ";
            sysSetting11.Value = this.txtShMonery.Text.Trim();
            sysSettingList.Add(sysSetting11);

            ////摩宝
            SysSetting sysSetting12 = new SysSetting();
            sysSetting12.Key = "mobao_pay";
            sysSetting12.Value = this.drpMb.SelectedValue;
            sysSettingList.Add(sysSetting12);

            ////智付
            SysSetting sysSetting13 = new SysSetting();
            sysSetting13.Key = "zhifu_pay";
            sysSetting13.Value = this.drpzhifu.SelectedValue;
            sysSettingList.Add(sysSetting13);

            ////my18
            SysSetting sysSetting14 = new SysSetting();
            sysSetting14.Key = "my18_pay";
            sysSetting14.Value = this.drpMy18.SelectedValue;
            sysSettingList.Add(sysSetting14);

            ////是否开启提现
            SysSetting sysSetting15 = new SysSetting();
            sysSetting15.Key = "ti_xian_isopen";
            sysSetting15.Value = this.drptx.SelectedValue;
            sysSettingList.Add(sysSetting15);

            ////关闭提现功能原因
            SysSetting sysSetting16 = new SysSetting();
            sysSetting16.Key = "ti_xian_shi_bai_info";
            sysSetting16.Value = this.txtTxErrorMsg.Text;
            sysSettingList.Add(sysSetting16);

            ////支付宝充值
            string zfbpath = SaveZfbImage();
            if (!string.IsNullOrEmpty(zfbpath)){
                SysSetting sysSetting17 = new SysSetting();
                sysSetting17.Key = "zhb_rect_url";
                sysSetting17.Value = zfbpath;
                sysSettingList.Add(sysSetting17);
            }

            ////微信充值
            string wxstr= SaveWxImage();
            if (!string.IsNullOrEmpty(wxstr))
            {
                SysSetting sysSetting18 = new SysSetting();
                sysSetting18.Key = "wx_rect_url";
                sysSetting18.Value = wxstr;
                sysSettingList.Add(sysSetting18);
            }

            if (sysSettingService.Update(sysSettingList))
                JsAlert("保存成功！");
            else
                JsAlert("保存失败！");
        }

        /// <summary>
        /// 保存支付宝充值二维码地址
        /// </summary>
        /// <returns></returns>
        private string SaveZfbImage()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["wx_zfb_path"];
            if (flzfb.HasFile)
            {
                //保存文件
                string fileext = System.IO.Path.GetExtension(flzfb.FileName);
                string filename = Guid.NewGuid().ToString() + fileext;
                string savefilepath = path + filename;
                flzfb.SaveAs(savefilepath);
                return filename;
            }

            return "";
        }
        /// <summary>
        /// 保存微信充值二维码地址
        /// </summary>
        /// <returns></returns>
        private string SaveWxImage()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["wx_zfb_path"];
            if (flwx.HasFile)
            {
                //保存文件
                string fileext = System.IO.Path.GetExtension(flzfb.FileName);
                string filename = Guid.NewGuid().ToString() + fileext;
                string savefilepath = path + filename;
                flwx.SaveAs(savefilepath);
                return filename;
            }

            return "";
        }

        private void CreateSettingItem(string key, string value, List<SysSetting> sysSettingList)
        {
            SysSetting sysSetting4 = new SysSetting();
            sysSetting4.Key = key;
            sysSetting4.Value = value;
            sysSettingList.Add(sysSetting4);
        }

        protected void btnSaveLhc_Click(object sender, EventArgs e)
        {
            BuilderLhc();
        }


        private void BuilderLhc()
        {
            string lhcContent = this.txtLhcQs.Text;
            if (string.IsNullOrEmpty(lhcContent))
            {
                JsAlert("请填写六合彩期数!");
                return;
            }
            bool isClear = chkCleal.Checked;
            string startIssue = this.txtStartIssue.Text;
            
            /**
             <row opentime="2016-03-03 21:36:52" opencode="16,05,45,07,02,01+33" expect="2016026"/>
             * <row opentime="2016-03-01 21:39:23" opencode="30,21,11,16,19,33+31" expect="2016025"/>
             * <row opentime="2016-02-27 21:42:22" opencode="27,06,11,20,44,09+42" expect="2016024"/>
             * <row opentime="2016-02-25 21:35:00" opencode="03,47,22,45,24,06+07" expect="2016023"/>
             * <row opentime="2016-02-23 21:35:18" opencode="15,27,45,44,09,24+47" expect="2016022"/>
             */
             ILotteryIssueService lotteryIssueService = IoC.Resolve<ILotteryIssueService>();
            int begInIssueCode =0;
            int lhcLotteryid = 21;//lhcs
            if (isClear && !int.TryParse(this.txtStartIssue.Text.Trim(), out begInIssueCode))
            {
                JsAlert("请填写期数期数！");
            }
            else if (!isClear)
            {
                var issueInfo = lotteryIssueService.GetLastIssue(lhcLotteryid);
                if (null != issueInfo)
                    begInIssueCode = Convert.ToInt32(issueInfo.IssueCode);
                else
                    begInIssueCode = Convert.ToInt32(startIssue);
            }
            if (isClear) {
                lotteryIssueService.ClearIssues(lhcLotteryid);//清除
            }

            string[] array = lhcContent.Split(',');
            string openTimes = System.Configuration.ConfigurationManager.AppSettings["lhcOpenTime"];//开奖时间
            int endsaleTime =Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["lhcSaleTime"]);//结束销售时间 分钟
            string year = DateTime.Now.ToString("yyyy");//当前年份

            foreach (var item in array)
            {
                string ymd = string.Format(year + "- " + item);//年-月-日
                DateTime openTime = DateTime.Parse(ymd + " " + openTimes);
                begInIssueCode++;//增加期数
                lotteryIssueService.AddLotteryIssueCode(new LotteryIssue()
                {
                    IssueCode = begInIssueCode.ToString(),
                    LotteryId = lhcLotteryid,
                    LotteryTime = openTime,
                    EndTime = openTime,
                    EndSaleTime = openTime.AddMinutes(-endsaleTime),
                    OccDate = DateTime.Now,
                    StartSaleTime = DateTime.Now,
                    StartTime = DateTime.Now
                });
            }
            
        }
    }
}