using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.Views.UserGroup
{
    public partial class UserBonus : BasePage
    {

        public bool isLhc =false;
        public string hideJj = string.Empty;
        public double lhcBackNum = 0.0;//六合彩返点

        public override bool ValidateLoginUser()
        {
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                this.BindData();
               
            }
            else
            {
                this.BindList(Request.Params["hdlotteryType"], new SysUser()
                {
                    Rebate = Convert.ToDouble(userrebate.Value),
                    PlayType = hidUserPlayType.Value == "0" ? UserPlayType.P1800 : UserPlayType.P1700
                });
            }
            

        }

        private void BindData()
        {
            int uid;
            if (!int.TryParse(Request.Params["id"], out uid))
            {
                Response.End();
                return;
            }
            string LotteryCode = Request.Params["LotteryCode"];
            if (string.IsNullOrEmpty(LotteryCode))
                LotteryCode = "cqssc";


            ISysUserService sysUserServices = IoC.Resolve<ISysUserService>();
            var user = sysUserServices.Get(uid);
            if (null == user)
            {
                Response.End();
                return;
            }
            if (user.UserType == UserType.BasicProy)
            {
                liyer.Visible = true;
                lbUserType.Visible = true;
            }

            this.lbCode.Text = user.Code;
            this.lbNickName.Text = user.NikeName;

            BindList(LotteryCode,user);
            if (!string.IsNullOrEmpty(Request.Params["from"]))
                dspan.Visible = false;
        }

        private void BindList(string lotteryCode, SysUser user)
        {
            var rebate = user.Rebate;
            this.userrebate.Value = rebate.ToString();
            this.hidUserPlayType.Value = user.PlayType == UserPlayType.P1800 ? "0" : "1";
            if (lotteryCode == "hk6")
            {
                isLhc = true;
                lhcBackNum = (9 - rebate) < 0 ? 0 : Math.Round((9 - rebate), 1);
                return;
            }


            bool isHideJj = ((rebate >= Utils.MaxRemo && user.PlayType== UserPlayType.P1800) || (rebate>=Utils.MaxRemo1700 && user.PlayType== UserPlayType.P1700));//是否隐藏奖金列
            if (isHideJj)
                hideJj = "style='display:none;'";
            
            //构建玩法奖金数据
            IPlayTypeService mPlayTypeService = IoC.Resolve<IPlayTypeService>();
            IPlayTypeRadioService mPlayTypeRadioService = IoC.Resolve<IPlayTypeRadioService>();
            IPlayNumTypeService mPlayTypeNumService = IoC.Resolve<IPlayNumTypeService>();
            ILotteryTypeService mLotteryTypeService = IoC.Resolve<ILotteryTypeService>();
            IPlayTypeRadiosBonusService playTypeRadiosBonusService = IoC.Resolve<IPlayTypeRadiosBonusService>();
            IGroupNameTypeService groupServices = IoC.Resolve<IGroupNameTypeService>();

            string actionStr = string.Empty;
            var lotteryTypes = mLotteryTypeService.GetAll().Where(c => c.IsEnable == true);
            var xsource= groupServices.GetAll().OrderBy(x=>x.OrderNo).ToList();
            foreach (var cf in xsource)
            {
                var xcs = lotteryTypes.Where(v=>v.GroupName==cf.Id) .ToList().OrderBy(x => x.Sort);
                foreach (var lt in xcs)
                {
                    string classStr = "btn action";
                    //checkBtn
                    if (lt.LotteryCode == lotteryCode)
                    {
                        classStr = "checkBtn ";
                    }
                    actionStr += "<input type=\"submit\" onclick='setHidden(\"" + lt.LotteryCode + "\")' class=\"" + classStr + "\" id=\"" + lt.LotteryCode + "\" value=\"" + lt.LotteryName + "\" />";
                }
            }
           this.ltActions.Text = actionStr;
            

            var allPlayTypes = mPlayTypeService.GetAll().Where(m=>m.LotteryCode==lotteryCode).ToList();
            var typeNums = mPlayTypeNumService.GetAll().ToList();
            var typeRadios = mPlayTypeRadioService.GetAll().ToList();
            var radiosBonuss = playTypeRadiosBonusService.GetAll().ToList();


            StringBuilder builder = new StringBuilder();
            foreach (var item in allPlayTypes)
            {
                var numList = typeNums.Where(n => n.PlayCode == item.PlayCode).Select(n => n.NumCode).ToList();


                var radioList = typeRadios.Where(c => numList.Contains(c.NumCode)).ToList();

                var sltRadios = radioList.Select(r => r.RadioCode).ToList();
                int rowSpan = radioList.Count + radiosBonuss.Where(x => sltRadios.Contains(x.RadioCode)).Count();
                var groupCt = radiosBonuss.Where(x => sltRadios.Contains(x.RadioCode)).GroupBy(x => x.RadioCode).Count();
                rowSpan = rowSpan - groupCt;

             
                bool isApp = false;
                
                foreach (var radio in radioList)
                {
                    var bonuss = radiosBonuss.Where(c => c.RadioCode == radio.RadioCode);
                    var maxRebate = user.PlayType == 0 ? radio.MaxRebate - rebate : radio.MaxRebate1700 - rebate;
                    maxRebate = Math.Round(maxRebate, 1);
                    maxRebate = maxRebate < 0 ? 0 : maxRebate;
                    if (bonuss.Count() > 0)
                    {
                       // maxRebate = CookUserInfo.PlayType == 0 ? radio.MaxRebate : radio.MaxRebate1700;
                        foreach (var b in bonuss)
                        {
                            var bonuValue = user.PlayType == 0 ? b.BonusBasic : b.BonusBasic17;
                            //获取奖金级
                            builder.Append("<tr>");
                            if (!isApp || rowSpan < 1)
                            {
                                builder.Append("<td rowspan='" + rowSpan + "'>" + item.PlayTypeName + "</td>");
                                isApp = true;
                            }
                            //玩法名称
                            builder.Append("<td>" + GetCountName(b.BonusCount) + "【" + radio.PlayTypeRadioName + " - " + b.BonusTitle + "】</td>");
                            builder.Append("<td>" + string.Format("{0:N2}", bonuValue) + "</td>");
                            if (!isHideJj)
                            {
                              
                                builder.Append("<td>" + maxRebate + "</td>");
                            }
                            builder.Append("<td>正常</td>");
                            builder.Append("</tr>");
                        }
                    }
                    else
                    {


                        //获取奖金级
                        builder.Append("<tr>");
                        if (!isApp || rowSpan < 1)
                        {
                            builder.Append("<td rowspan='" + rowSpan + "'>" + item.PlayTypeName + "</td>");
                        }
                        var bonuValue = user.PlayType == 0 ? radio.BonusBasic : radio.BonusBasic17;
                        
                        builder.Append("<td>" + radio.PlayTypeRadioName + "</td>");
                        builder.Append("<td>" + string.Format("{0:N2}", bonuValue) + "</td>");
                        if (!isHideJj)
                        {
                            builder.Append("<td>" + maxRebate + "</td>");
                        }
                        builder.Append("<td>" + (radio.IsEnable ? "正常" : "禁用") + "</td>");
                        builder.Append("</tr>");
                    }
                    isApp = true;
                }

            }

            ltTBody.Text = builder.ToString();
        }


        private string GetCountName(int bc)
        {
            string ti = "";
            switch (bc)
            {
                case 5:
                    ti = "一等奖";
                    break;
                case 4:
                    ti = "二等奖";
                    break;
                case 3:
                    ti = "三等奖";
                    break;
                case 2:
                    ti = "四等奖";
                    break;
                case 1:
                    ti = "五等奖";
                    break;
            }
            return ti;
        }
    }

}