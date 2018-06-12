using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.ServerWeb.Page.Bank;
using Ytg.ServerWeb.Views.Activity.YongJin;

namespace Ytg.ServerWeb.wap.activity
{
    public partial class yongj : BasePage
    {
        public string Content = string.Empty;
        public decimal UserAmt = 0;
        public bool isautoRefbanner = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*
                07:30:00 – 次日凌晨02:00:00
                */

                //if ((DateTime.Now.Hour > 2 && DateTime.Now.Hour < 7))
                //{
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('活动时间为每日07:30:00 – 次日凌晨02:00:00！');</script>");
                //}
            }
        }

        protected void btnME_Click(object sender, EventArgs e)
        {

            if (this.Master != null)
            {
                UserAmt = (this.Master as lotterySite).GetUserBalance();
            }

            if (CookUserInfo.UserType != BasicModel.UserType.Proxy &&
                    CookUserInfo.UserType != BasicModel.UserType.BasicProy
                    || !Utils.HasAvtivityDateTimes())
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('活动时间为每日07:30:00 – 次日凌晨02:00:00！');</script>");
                return;
            }


            ICommissionsService commissionsService = IoC.Resolve<ICommissionsService>();
            if (commissionsService.HasReceive(CookUserInfo.Id, Utils.GetActivityOccDay()))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('您今天已经领取过佣金，请明天再来！');</script>");
                // this.btnME.Visible = false;
                return;
            }
            try
            {
                ISysUserService userServices = IoC.Resolve<ISysUserService>();
                ISysUserBalanceDetailService userBalanceDetailService = IoC.Resolve<ISysUserBalanceDetailService>();
                var result = userBalanceDetailService.GetChildrensByMonery(CookUserInfo.Id);
                if (null == result || result.Count < 1)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('下级投注量未完成要求，领取奖励失败！');</script>");
                    return;
                }

                List<MessageEntity> messageEntitys = new List<MessageEntity>();
                decimal sumMonery = 0M;

                bool isCompled = false;
                foreach (var item in result)
                {
                    if (item.ParentId == null)
                        continue;
                    var sumValue = item.TradeAmt;//消费金额
                    int proxyLevel = -1;//1代表直属下级 2代表下下级  超过三级，不给于奖励
                    if (item.ParentId == CookUserInfo.Id)
                    {
                        //直属下级
                        proxyLevel = 1;
                    }
                    else
                    {
                        if (userServices.HasParentIsParentid(item.ParentId.Value, CookUserInfo.Id))
                        {
                            proxyLevel = 2;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    var monery = RechargeConfig.CommissionsMonery(sumValue, proxyLevel, messageEntitys);
                    if (monery < 1)
                    {
                        continue;
                    }
                    //用于组织数据
                    sumMonery += monery;
                    //插入佣金
                    commissionsService.Create(new BasicModel.Commission()
                    {
                        UserId = CookUserInfo.Id,
                        WinMonery = monery,
                        ChildrenByMonery = sumValue,
                        OccDaty = Utils.GetActivityOccDay()
                    });

                    commissionsService.Save();

                }
                //奖励金额
                if (sumMonery > 0)
                {
                    ISysUserBalanceService userBalanceServices = IoC.Resolve<ISysUserBalanceService>();
                    var details = new BasicModel.SysUserBalanceDetail()
                    {
                        RelevanceNo = CookUserInfo.Id.ToString(),
                        SerialNo = "q" + Utils.BuilderNum(),
                        Status = 0,
                        TradeAmt = sumMonery,
                        TradeType = BasicModel.TradeType.佣金大返利,
                        UserId = CookUserInfo.Id
                    };
                    if (userBalanceServices.UpdateUserBalance(details, sumMonery) > 0)
                    {
                        isCompled = true;
                    }
                }
                if (isCompled)
                {
                    Content = "领取佣金成功，领取金额为：" + sumMonery + "；<br/>" + BuilderMessage(messageEntitys) + "感谢你的参与，祝你游戏愉快！";
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.confirm('" + showMessgae + "');</script>");
                    isautoRefbanner = false;
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('下级投注量未完成要求，领取奖励失败！');</script>");
                }
                return;
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("btnME_Click", ex);
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">$.alert('奖励领取失败，请稍后再试！');</script>");
        }

        /// <summary>
        /// 组织提示消息
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        protected string BuilderMessage(List<MessageEntity> messages)
        {
            string str0 = "投注大于等于10000，直接下级人数为：{0}，下下级人数为：{1}<br/>";
            string str1 = "投注大于等于3000，直接下级人数为：{0}，下下级人数为：{1}<br/>";
            string str2 = "投注大于等于2000，直接下级人数为：{0}，下下级人数为：{1}<br/>";
            string str3 = "投注大于等于1000，直接下级人数为：{0}，下下级人数为：{1}<br/>";
            //str0 
            var str0Fst = messages.Where(x => x.monery == -10000 && x.ProxyLevel == 1).FirstOrDefault();
            var str0Lst = messages.Where(x => x.monery == -10000 && x.ProxyLevel == 2).FirstOrDefault();
            var ct0 = 0;
            var ct_0 = 0;
            if (str0Fst != null)
            {
                ct0 = str0Fst.Count;
            }
            if (str0Lst != null)
            {
                ct_0 = str0Lst.Count;
            }
            str0 = string.Format(str0, ct0, ct_0);//

            var str1Fst = messages.Where(x => x.monery == -3000 && x.ProxyLevel == 1).FirstOrDefault();
            var str1Lst = messages.Where(x => x.monery == -3000 && x.ProxyLevel == 2).FirstOrDefault();
            var ct = 0;
            var ct1 = 0;
            if (str1Fst != null)
            {
                ct = str1Fst.Count;
            }
            if (str1Lst != null)
            {
                ct1 = str1Lst.Count;
            }
            str1 = string.Format(str1, ct, ct1);

            var str2Fst = messages.Where(x => x.monery == -2000 && x.ProxyLevel == 1).FirstOrDefault();
            var str2Lst = messages.Where(x => x.monery == -2000 && x.ProxyLevel == 2).FirstOrDefault();
            var ct2 = 0;
            var ct21 = 0;
            if (str2Fst != null)
            {
                ct2 = str2Fst.Count;
            }
            if (str2Lst != null)
            {
                ct21 = str2Lst.Count;
            }
            str2 = string.Format(str2, ct2, ct21);


            var str3Fst = messages.Where(x => x.monery == -1000 && x.ProxyLevel == 1).FirstOrDefault();
            var str3Lst = messages.Where(x => x.monery == -1000 && x.ProxyLevel == 2).FirstOrDefault();
            var ct3 = 0;
            var ct31 = 0;
            if (str3Fst != null)
            {
                ct3 = str3Fst.Count;
            }
            if (str3Lst != null)
            {
                ct31 = str3Lst.Count;
            }
            str3 = string.Format(str3, ct3, ct31);

            return str0 + str1 + str2 + str3;
        }
    }

}
