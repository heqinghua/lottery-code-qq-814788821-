using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Providers.Entities;
using System.Web.Security;
using Newtonsoft.Json;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;
using Ytg.ServerWeb.Page.PageCode.Market;
using Ytg.BasicModel.DTO;
using Ytg.ServerWeb.BootStrapper;
using Ytg.Core;

namespace Ytg.ServerWeb.Page.PageCode
{
    /// <summary>
    /// 初始化相关
    /// </summary>
    public class InitialRequestManager : BaseRequestManager
    {
        readonly ISysUserService mSysUserService;
        readonly ISysUserBalanceService mSysUserBalanceService;
        readonly ISysUserBalanceDetailService mSysUserBalanceDetailService;
        readonly INewsService mNewsService;
        readonly IHotLotteryService mHotLotteryService;
        readonly IBannerService mBannerService;
        readonly IMarketService mMarketService;
        readonly ISysQuotaService mSysQuotaService;//配额
        readonly ISysSettingService mSysSettingService;
        readonly ILockIpInfoService mLockIpInfoService;
        readonly ILotteryIssueService mLotteryIssueService;//期数

        const string DefaultPwd = "a123456";

        public InitialRequestManager(ISysUserService sysUserService,
            ISysUserBalanceService sysUserBalanceService,
            ISysUserBalanceDetailService SysUserBalanceDetailService,
            INewsService newsService, IHotLotteryService hotLotteryService,
            IBannerService bannerService, IMarketService marketservice, 
            ISysQuotaService sysQuotaService,
            ISysSettingService sysSettingService,
            ILockIpInfoService lockIpInfoService,
            ILotteryIssueService lotteryIssueService)
        {
            this.mSysUserService = sysUserService;
            this.mSysUserBalanceService = sysUserBalanceService;
            this.mSysUserBalanceDetailService = SysUserBalanceDetailService;
            this.mNewsService = newsService;
            this.mHotLotteryService = hotLotteryService;
            this.mBannerService = bannerService;
            this.mMarketService = marketservice;
            this.mSysQuotaService = sysQuotaService;
            this.mSysSettingService = sysSettingService;
            this.mLockIpInfoService = lockIpInfoService;
            this.mLotteryIssueService = lotteryIssueService;
        }

        protected override bool Validation()
        {
            return true;
        }
        /// <summary>
        /// 初始接口，无需登录用户信息
        /// </summary>
        /// <returns></returns>
        protected override bool ValidationLoginUserId()
        {
            return true;
        }

        //不验证
        protected override bool ValidationSession
        {
            get
            {
                return false;
            }
        }

        protected override void Process()
        {
            switch (this.ActionName.ToLower())
            {
                case "login":

                    this.Login();
                    break;
                case "verificationuser":
                    VerificationUser();
                    break;
                case "login_progress"://APP登录
                    this.Login(false);
                    break;
                case "logout"://注销
                    Logout();
                    break;
                case "userbalance":
                    this.GetUserBalance();
                    break;
                case "autoregist"://自助注册
                    AutoRegist();
                    break;
                case "news"://获取平台消息
                    this.GetNews();
                    break;
                case "alertnews":
                    this.GetAlertNews();
                    break;
                case "getuserwhy"://获取用户问候语
                    GetUserWhy();
                    break;
                case "settings"://获取配置信息
                    GetSettings();
                    break;
                case "hotlottery": //热门彩种
                    GetHotLottery();
                    break;
                case "banner":  //获取首页Banner列表
                    GetBanners();
                    break;
                case "nowtime":
                    this.Synchronization();
                    break;
                case "vdtailuserpwd"://修改密码  根据账号和资金密码验证
                    int uid = -1;
                    if (GetVdtailuserpwd(ref uid))
                        AppGlobal.RenderResult(ApiCode.Success);
                    break;
                case "updatepwd":
                    UpdateUserPwd();//修改密码
                    break;
                case "verification"://验证域名
                    Verification();
                    break;
                case "getdns"://获取所有有效的域名
                    GetDns();
                    break;
                case "topnews":
                    break;
                
            }
        }

        /// <summary>
        /// 验证用户是否被禁用
        /// </summary>
        private void VerificationUser() {
            ValidationLoginUserId();
            var sings = FormsPrincipal<CookUserInfo>.TryParsePrincipal(Request);
            if (null == sings)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            var loginuser = sings.UserData;
            if (null == loginuser)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }


            if (loginuser != null)
            {
                //验证是否被禁用
                
                var item = mSysUserService.Get(loginuser.Id);
                if (null == item)
                {
                    AppGlobal.RenderResult(ApiCode.Fail);
                    return;
                }
                if (item.IsLockCards || item.IsDelete)
                {
                    AppGlobal.RenderResult(ApiCode.Success);
                    Logout();//注销
                }
                else
                {
                    AppGlobal.RenderResult(ApiCode.Fail);
                }
            }
            else {
                AppGlobal.RenderResult(ApiCode.Success);
            }
        }


        #region 同步期数时间
        private void Synchronization()
        {
            int lotteryid = -1;
            if (!int.TryParse(Request.Params["lotteryid"], out lotteryid))
            {
                Response.Write("-1");
                Response.End();
                return;
            }
            string issue = Request.Params["issue"];
            if (string.IsNullOrEmpty(issue))
            {
                Response.Write("-1");
                Response.End();
                return;
            }
            //计算剩余时间

            var item = mLotteryIssueService.SqlGetIssueCode(lotteryid, issue);
            if (null == item)
            {
                Response.Write("-1");
                Response.End();
                return;
            }

            //计算剩余时间
            var sec = (int)item.EndSaleTime.Value.Subtract(DateTime.Now).TotalMilliseconds / 1000;
            if (sec < 0)
                sec = -1;
            Response.Write(sec);
            Response.End();
        }

        #endregion

        private void GetDns()
        {
            string str = Request.Params["form"];
            if (string.IsNullOrEmpty(str))
            {
                Response.Write("");
                Response.End();
                return;
            }
            //获取有效域名
            IDnsService dnsService = IoC.Resolve<IDnsService>();
            var fs = dnsService.GetAll().Where(x=>x.IsShowAutoRegist).ToList();
            string dnsStr = "";
            foreach (var item in fs)
            {
                dnsStr += item.SiteDnsUrl + ",";
            }
            Response.Write(dnsStr);
            Response.End();

        }

        /// <summary>
        /// 验证域名
        /// </summary>
        private void Verification()
        {
            string dns = Request.Params["account"];
            string code = Request.Params["code"];
            if (string.IsNullOrEmpty(dns)
                || string.IsNullOrEmpty(code))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            if (System.Web.HttpContext.Current.Session["verification_dns"]==null || code != System.Web.HttpContext.Current.Session["verification_dns"].ToString())
            {
                AppGlobal.RenderResult(ApiCode.Security);
                return;
            }
            //验证
            IDnsService dnsService=IoC.Resolve<IDnsService>();
            var fs = dnsService.GetAll().ToList();
            if (fs == null)
            {
                AppGlobal.RenderResult(ApiCode.NotSell);
                return;
            }
            else
            {
                dns = dns.ToLower();
                bool fis = false;
                foreach (var item in fs)
                {
                    if (dns.IndexOf(item.SiteDnsUrl.ToLower()) != -1)
                    {
                        fis = true;
                        break;
                    }
                }
                if (fis)
                    AppGlobal.RenderResult(ApiCode.Success);
                else
                    AppGlobal.RenderResult(ApiCode.NotSell);
            }
        }

        /// <summary>
        /// 注销登陆
        /// </summary>
        private void Logout()
        {
            FormsPrincipal<CookUserInfo>.SignOut();
            AppGlobal.RenderResult(ApiCode.Success);
        }

        //玩家登陆
        private void Login(bool isfig=true)
        {
            var isLock = this.mLockIpInfoService.IsLockIp(Utils.GetIp());
            if (isLock)
            {
                //获取跳转地址
                AppGlobal.RenderResult(ApiCode.DisabledIp, this.GetRefDns());
                return;
            }

            //登陆名
            string loginCode = Request.Params["M_LOGINCODE"];
            //登陆密码
            string pwd = Request.Params["M_LOGINPWD"];
            //验证码
            string code = Request.Params["M_LOGINVIDACODE"];
            if (string.IsNullOrEmpty(loginCode) || string.IsNullOrEmpty(pwd))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            if (string.IsNullOrEmpty(code) && isfig) {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }


            var item = this.mSysUserService.Get(loginCode, pwd);
            if (null == item)
            {
                AppGlobal.RenderResult(ApiCode.ValidationFails);
                return;
            }
            if (item.IsDelete) {
                AppGlobal.RenderResult(ApiCode.DisabledCode, this.GetRefDns());
                return;
            }


            string sessionid = Guid.NewGuid().ToString();
            CookUserInfo info = new CookUserInfo();
            info.Id = item.Id;
            info.Code = item.Code;
            info.NikeName = item.NikeName;
            info.Sex = sessionid;
            info.Head = item.Head;
            info.Rebate = item.Rebate;
            info.UserType = item.UserType;
            info.IsRecharge = item.IsRecharge;
            info.PlayType = item.PlayType;
            info.ProxyLevel = item.ProxyLevel;

            FormsPrincipal<CookUserInfo>.SignIn(loginCode, info, FormsAuthentication.Timeout.Minutes);
            //Ytg.ServerWeb.BootStrapper.SessionManager.AddOrUpdateSession(item.Id,new YtgSession()
            //{
            //    UserId = item.Id,
            //    SessionId = sessionid,
            //    OccDate = DateTime.Now,
            //    Code = item.Code
            //});
          

            string loginIpAddress = Ytg.Comm.Utils.GetIp();
            int state = this.mSysUserSessionService.UpdateInsertUserSession(new UserSession()
            {
                UserId = item.Id,
                SessionId = sessionid,
                LoginIp = loginIpAddress,
                LoginClient= Ytg.Comm.Utils.GetLoginClientType(),
            });
            string loginCityName = "";//Utils.GetCityByIp(item.LastLoginIp);//获取所在城市
            string useSource = System.Web.HttpContext.Current.Request.Params["usesource"];
            //判断是否为移动设备访问
            bool isMobile = Utils.IsMobile();
            if (isMobile)
                useSource = "移动设备";

            /**记录日志*/
            SysLog log = new SysLog()
            {
                Descript = loginCityName,
                Ip = loginIpAddress,
                OccDate = DateTime.Now,
                ReferenceCode = info.Code,
                ServerSystem = Ytg.Comm.Utils.GetUserSystem(),
                Type = 0,
                UserId = info.Id,
                UseSource = useSource
            };
            /**记录日志 end*/

            //修改用户登录时间
            item.LastLoginTime = DateTime.Now;
            item.LastLoginIp = loginIpAddress;
            item.IsLogin = true;
            item.IsLineLogin = true;
            item.LastCityName = log.Descript;
            item.ServerSystem = log.ServerSystem;
            item.UseSource = log.UseSource;
            item.LoginCount++;//登录次数增加
            this.mSysUserService.Save();
            //单点登录
           // DanDianLogin(info.Id.ToString());
            
            //输出结果
            AppGlobal.RenderResult<CookUserInfo>(ApiCode.Success, info);
            //添加登录日志
            Ytg.Service.Logic.LogHelper.AddLog(log);
        }

        /// <summary>
        /// 单点登录
        /// </summary>
        /// <param name="userid"></param>
        private void DanDianLogin(string userid)
        {
            Hashtable hOnline = (Hashtable)System.Web.HttpContext.Current.Application["Online"];
            if (hOnline != null)
            {
                IDictionaryEnumerator idE = hOnline.GetEnumerator();
                string strKey = "";
                while (idE.MoveNext())
                {
                    if (idE.Value != null && idE.Value.ToString().Equals(userid))
                    {
                        //already login 
                        strKey = idE.Key.ToString();
                        hOnline.Remove(strKey);
                        hOnline.Add(strKey, "OUTCHECK");
                        break;
                    }
                }
            }
            else
            {
                hOnline = new Hashtable();
            }

            hOnline[System.Web.HttpContext.Current.Session.SessionID] = userid;
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["Online"] = hOnline;
            System.Web.HttpContext.Current.Application.UnLock();
        }


        /// <summary>
        /// 根据用户id获取用余额信息
        /// </summary>
        private void GetUserBalance()
        {
            //登陆名
            int uid;
            if (!int.TryParse(Request.Params["uid"], out uid))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            var item = this.mSysUserBalanceService.GetUserBalance(uid);

            AppGlobal.RenderResult<SysUserBalance>(ApiCode.Success, item);
        }

        /// <summary>
        /// 获取跳转的dns
        /// </summary>
        /// <returns></returns>
        private string GetRefDns()
        {
            var fs = this.mSysSettingService.GetAll().Where(x => x.Key == "ZXLTPATH").FirstOrDefault();
            if (fs == null)
                return string.Empty;
            return fs.Value;
        }

        /// <summary>
        /// 自动注册
        /// </summary>
        private void AutoRegist()
        {
            //验证当前IP是否锁定
            var isLock = this.mLockIpInfoService.IsLockIp(Utils.GetIp());
            if (isLock)
            {
                //获取跳转地址
                AppGlobal.RenderResult(ApiCode.DisabledIp, this.GetRefDns());
                return;
            }
            //登陆名
            string account = Request.Params["Code"];
            account = account.Replace(" ", "");
            if (account.Length < 6)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            string nickName = Request.Params["NickName"];
            //验证码
            string vcode = Request.Params["VdaCode"];
            int mloginUserId;
            bool isreg = int.TryParse(this.Request.Params["params"], out mloginUserId);
            if (!isreg)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            var isCode = System.Web.HttpContext.Current.Session["ValidateCode"] == null || System.Web.HttpContext.Current.Session["ValidateCode"].ToString().ToLower() != vcode.ToLower();
            if (isCode || string.IsNullOrEmpty(account) || string.IsNullOrEmpty(vcode))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            string qq = Request.Params["qq"];
            if (string.IsNullOrEmpty(qq))
                qq = "";
            string phone = Request.Params["phone"];
            if (string.IsNullOrEmpty(phone))
                phone = "";

            var parentItem = this.mSysUserService.Get(mloginUserId);
            if (null == parentItem || parentItem.UserType == UserType.General || parentItem.UserType == UserType.Manager)
            {
                AppGlobal.RenderResult(ApiCode.Fail);//非法请求，无法继续
                return;
            }

            //验证账号是否存在
            var item = this.mSysUserService.Get(account);
            if (null != item)
            {
                AppGlobal.RenderResult(ApiCode.ValidationFails);//非法请求，无法继续
                return;
            }
            string psw = Request.Params["lgpwd"];
            if (string.IsNullOrEmpty(psw))
                psw = DefaultPwd;
            //注册
            var outuser = new SysUser()
            {

                Code = account,
                ParentId = parentItem.Id,
                Sex=string.IsNullOrEmpty(parentItem.Sex)?"0":"1",
                NikeName = nickName,
                Password = psw,
                PlayType = parentItem.PlayType,
                Rebate = (parentItem.Rebate + parentItem.AutoRegistRebate),
                UserType = UserType.General,
                ProxyLevel = parentItem.ProxyLevel + 1,
                Qq = qq,
                MoTel = phone,
                Head = parentItem.Head
            };
            if (outuser.Rebate < 0)
            {
                outuser.Rebate = parentItem.Rebate;
            }
            this.mSysUserService.Create(outuser);
            this.mSysUserService.Save();
            UserComm.InintNewUserBalance(outuser, this.mSysSettingService, this.mSysUserBalanceService, this.mSysUserBalanceDetailService, this.mSysUserService);//初始化新用户金额
            //初始化配额
            this.mSysQuotaService.InintUserQuota(outuser.Id, outuser.ParentId, Math.Round(Ytg.Comm.Utils.MaxRemo - outuser.Rebate, 1));
            //初始化资金密码
            InintUserBalancePassword(outuser.Id,Request.Params["zjpwd"]);
            AppGlobal.RenderResult(ApiCode.Success);//非法请求，无法继续
        }


        #region 获取新闻

        public void GetNews()
        {
            var source = this.mNewsService.GetAll().Where(item => item.IsDelete == false).OrderByDescending(item => item.OccDate).ToList();

            AppGlobal.RenderResult<List<SysNews>>(ApiCode.Success, source);
        }

        /// <summary>
        /// 获取弹出新闻内容
        /// </summary>
        public void GetAlertNews()
        {
            var source = this.mNewsService.GetAll().Where(item => item.IsDelete == false && item.IsShow==1).FirstOrDefault();
            AppGlobal.RenderResult<SysNews>(ApiCode.Success, source);
        }
        #endregion

        #region 获取问候语
        private void GetUserWhy()
        {
            var isLock = this.mLockIpInfoService.IsLockIp(Utils.GetIp());
            if (isLock)
            {
                //获取跳转地址
                AppGlobal.RenderResult(ApiCode.DisabledIp, this.GetRefDns());
                return;
            }

            string account = Request.Params["account"];
            string code = Request.Params["code"];//获取验证码
            if (string.IsNullOrEmpty(account))
            {
                System.Web.HttpContext.Current.Session["mLogin"] = null;
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            //验证是否与session中code一致
            var seeCode=System.Web.HttpContext.Current.Session["mLogin"];
            if (seeCode==null || seeCode.ToString() != code)
            {
                System.Web.HttpContext.Current.Session["mLogin"] = null;
                //验证码错误
                AppGlobal.RenderResult(ApiCode.Security);
                return;
            }

            var fs= this.mSysUserService.GetAll().Where(item=>item.Code==account).FirstOrDefault();
            if (null == fs)
            {
                System.Web.HttpContext.Current.Session["mLogin"] = null;
                AppGlobal.RenderResult(ApiCode.Fail);
            }
            else if (fs.IsDelete)
            {
                System.Web.HttpContext.Current.Session["mLogin"] = null;
                AppGlobal.RenderResult(ApiCode.DisabledCode, this.GetRefDns());
            }
            else
            {
                AppGlobal.RenderResult(ApiCode.Success, fs.Greetings);
            }
        
        }
        #endregion

        #region 获取配置信息
        private void GetSettings()
        {
            var source = Ytg.Service.Logic.SysSettingHelper.GetAllSysSetting();
            AppGlobal.RenderResult<List<SysSetting>>(ApiCode.Success, source);
        }
        #endregion

        #region 获取热门彩种

        /// <summary>
        /// 获取热门彩种
        /// </summary>
        public void GetHotLottery()
        {
            var source = this.mHotLotteryService.GetHotLottery();
            AppGlobal.RenderResult<IList<HotLottery>>(ApiCode.Success, source);
        }

        #endregion

        #region 获取首页Banner列表

        /// <summary>
        /// 获取首页Banner列表
        /// </summary>
        public void GetBanners()
        {
            var source = this.mBannerService.GetBanners();
            AppGlobal.RenderResult<IList<Banner>>(ApiCode.Success, source);
        }

        #endregion

        #region 注册活动

        ///// <summary>
        ///// 注册活动
        ///// </summary>
        //private void RegistMarket(int uid,decimal giftMoney)
        //{
        //    try
        //    {
        //        //获取用户余额
        //        decimal userAmt = 0;
        //        var userbalance = this.mSysUserBalanceService.GetUserBalance(uid);
        //        if (null != userbalance)
        //            userAmt = userbalance.UserAmt;//获取用户余额

        //        //保存用户余额
        //        userbalance.UserAmt = userAmt + giftMoney;

        //        //投注消费记录
        //        this.mSysUserBalanceDetailService.Create(new SysUserBalanceDetail()
        //        {
        //            OpUserId = uid,
        //            SerialNo = "m" + DateTime.Now.ToString("MMddHHmmssff"),
        //            Status = 0,
        //            TradeAmt = userAmt,
        //            TradeType = tradeType,
        //            UserAmt = userAmt,
        //            UserId = uid
        //        });
        //        this.mSysUserBalanceDetailService.Save();
        //        AppGlobal.RenderResult(ApiCode.Success);
        //    }
        //    catch (Exception ex)
        //    {
        //        AppGlobal.RenderResult(ApiCode.Exception);
        //    }
        //}

        #endregion

        #region 根据账号和资金密码获取用户信息是否正确
        private bool GetVdtailuserpwd(ref int uid,bool hasCode=false)
        {
            var seesionCode = System.Web.HttpContext.Current.Session["updatePwd_code"];
            string account = System.Web.HttpContext.Current.Request["account"];
            string code = System.Web.HttpContext.Current.Request["code"];
            string cdPwd = System.Web.HttpContext.Current.Request["cdpwd"];

            if (string.IsNullOrEmpty(account)
                || string.IsNullOrEmpty(code)
                || string.IsNullOrEmpty(cdPwd))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);//非法请求，无法继续
                return false;
            }
            if (!hasCode)
            {
                if (seesionCode != null && seesionCode.ToString() != code)
                {
                    AppGlobal.RenderResult(ApiCode.Security);
                    return false;
                }
            }
            var userInfo = this.mSysUserService.Get(account);
            if (null == userInfo)
            {
                AppGlobal.RenderResult(ApiCode.ValidationFails);
                return false;
            }
            if (userInfo.IsDelete)
            {
                AppGlobal.RenderResult(ApiCode.DisabledCode);
                return false;
            }
            var userBalance = this.mSysUserBalanceService.VdUserBalancePwd(userInfo.Id, cdPwd);
            if (!userBalance)
            {
                AppGlobal.RenderResult(ApiCode.ValidationFails);
                return false;
            }
            uid = userInfo.Id;
            return true;
          //  AppGlobal.RenderResult(ApiCode.Success);
        }
        #endregion

        #region 修改密码
        private void UpdateUserPwd()
        {
            int outUid=-1;
            if (GetVdtailuserpwd(ref outUid,true))
            {
                //验证通过
                string newPwd = System.Web.HttpContext.Current.Request["newPwd"];
                string newConformPwd = System.Web.HttpContext.Current.Request["newConformPwd"];
               string cdPwd= System.Web.HttpContext.Current.Request["cdpwd"];
                if (string.IsNullOrEmpty(newPwd)
                    || string.IsNullOrEmpty(newConformPwd)
                    || newPwd != newConformPwd)
                {
                    AppGlobal.RenderResult(ApiCode.ParamEmpty);//非法请求，无法继续
                    return;
                }
                if (cdPwd == newPwd) {
                    AppGlobal.RenderResult(ApiCode.RequestFail);//非法请求，无法继续
                    return;
                }
                //修改密码
                if (this.mSysUserService.UpdatePassword(outUid, newPwd))
                {
                    AppGlobal.RenderResult(ApiCode.Success);//
                }
                else {
                    AppGlobal.RenderResult(ApiCode.Fail);//
                }
            }
        }
        #endregion


        #region def 初始化资金密码
        private bool InintUserBalancePassword(int userid,string password)
        {
            
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }
            var isFig = this.mSysUserBalanceService.InintUserBalancePwd(userid, password);

            return isFig;
        }
        #endregion

    }
}
