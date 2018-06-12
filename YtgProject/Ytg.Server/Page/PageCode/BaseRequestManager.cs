
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Core;

namespace Ytg.ServerWeb.Page.PageCode
{
    /// <summary>
    /// 处理基类
    /// </summary>
    public abstract class BaseRequestManager
    {
        /// <summary>
        /// 操作key
        /// </summary>
        const string QueryKey = "action";

        /// <summary>
        /// 操作名称
        /// </summary>
        protected virtual string ActionName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取页面
        /// </summary>
        protected MobilePage Page { get; private set; }

        /// <summary>
        /// 获取http上下文
        /// </summary>
        protected System.Web.HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
        }


        /// <summary>
        /// 获取当前 HTTP 请求的 System.Web.HttpRequest 对象。
        /// </summary>
        protected  HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }

        /// <summary>
        /// 获取当前 HTTP 响应的 System.Web.HttpResponse 对象。
        /// </summary>
        protected HttpResponse Response
        {
            get
            {
                return HttpContext.Current.Response;
            }
        }

        private int mloginUserId;

        /// <summary>
        /// 登录用户id， heqinghua 2015/1/12 by add
        /// </summary>
        protected int LoginUserId
        {
            get
            {
                return this.mloginUserId;
            }
        }

        /// <summary>
        /// session 管理
        /// </summary>
        protected readonly ISysUserSessionService mSysUserSessionService=IoC.Resolve<ISysUserSessionService>();
       

        private CookUserInfo mCookUserInfo = null;
        /// <summary>
        /// 用户登陆cookie值
        /// </summary>
        protected CookUserInfo LoginUser
        {
            get {
                return mCookUserInfo;
            }
        }

        private int mClientType = 0;
        /// <summary>
        /// 获取客户端来源，0代表为网页 1代表为客户端
        /// </summary>
        public int ClientType
        {
            get { return this.mClientType; }
            set { this.mClientType = value; }
        }


        private int mLotteryId=-1;

        /// <summary>
        /// 彩票id
        /// </summary>
        protected int LotteryId
        {
            get
            {
                return this.mLotteryId;
            }
            set {
                this.mLotteryId = value;
            }
        }

        private string mLotteryCode;

        /// <summary>
        /// 彩票Code
        /// </summary>
        protected string LotteryCode
        {
            get
            {
                return this.mLotteryCode;
            }
            set
            {
                this.mLotteryCode = value;
            }
        }

        public void ProcessRequest(MobilePage page)
        {
            this.Page = page;

            //单点登录处理
            //if (this.ValidationSession)
            //{
            //    //单点登录
            //    Hashtable hOnline = (Hashtable)System.Web.HttpContext.Current.Application["Online"];
            //    if (hOnline != null)
            //    {
            //        IDictionaryEnumerator idE = hOnline.GetEnumerator();
            //        while (idE.MoveNext())
            //        {
            //            if (idE.Key != null && idE.Key.ToString().Equals(System.Web.HttpContext.Current.Session.SessionID))
            //            {
            //                //already login
            //                if (idE.Value != null && "OUTCHECK".Equals(idE.Value.ToString()))
            //                {
            //                    hOnline.Remove(System.Web.HttpContext.Current.Session.SessionID);
            //                    System.Web.HttpContext.Current.Application.Lock();
            //                    System.Web.HttpContext.Current.Application["Online"] = hOnline;
            //                    System.Web.HttpContext.Current.Application.UnLock();
            //                    //移除cookie
            //                    FormsPrincipal<CookUserInfo>.SignOut();
            //                    //由于您长时间未操作,为确保安全,请重新登录！
            //                    AppGlobal.RenderResult(ApiCode.ExitLogin);
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //}

             var  rm=Request.Params["f_N_robmt"];

            if (rm != "robmit")
            {
                //身份验证，及登录用户Id是否为空，为空则直接返回，
                //为安全起见，不给出任何错误提示
                if (!Validation() || !ValidationLoginUserId())
                    return;
            }
            else
            {
                //机器人提交无需身份验证
                int.TryParse(this.Request.Params["loginUserId"], out mloginUserId);
                this.mCookUserInfo = new CookUserInfo()
                {
                    Id = mloginUserId
                };
            }

            this.mLotteryCode = this.Request.Params["lotterycode"];
            int.TryParse(this.Request.Params["lotteryid"], out this.mLotteryId);
            this.ActionName = this.Request.Params[QueryKey];
            if (string.IsNullOrEmpty(ActionName))
            {
                //传人参数错误
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            this.Process();
        }

        /// <summary>
        /// 身份验证
        /// </summary>
        /// <returns></returns>
        protected virtual bool Validation()
        {
            //身份验证
            FormsPrincipal<CookUserInfo> source = FormsPrincipal<CookUserInfo>.TryParsePrincipal(this.Request);
            if (null == source || source.UserData == null)
            {
                AppGlobal.RenderResult(ApiCode.Security);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证是否有用户id参数
        /// </summary>
        /// <returns></returns>
        protected virtual bool ValidationLoginUserId()
        {
            var sings=FormsPrincipal<CookUserInfo>.TryParsePrincipal(Request);
            if (null == sings)
                return false;
            this.mCookUserInfo = sings.UserData;
            if (null != this.mCookUserInfo)
            {
                this.mloginUserId = this.mCookUserInfo.Id;
                var sessionitem= mSysUserSessionService.GetUserId(this.mCookUserInfo.Id);
                
                if (sessionitem==null || sessionitem.SessionId!= mCookUserInfo.Sex)
                {
                    //清除cookie ,禁止登录,
                    FormsPrincipal<CookUserInfo>.SignOut();
                    AppGlobal.RenderResult(ApiCode.ExitLogin);
                    return false;
                }
            }
            else
            {
                int.TryParse(this.Request.Params["loginUserId"], out mloginUserId);
            }

            return mloginUserId > 0;
        }

        /// <summary>
        /// 是否验证session
        /// </summary>
        protected virtual bool ValidationSession
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 具体处理方法
        /// </summary>
        protected abstract void Process();


        
    }
}
