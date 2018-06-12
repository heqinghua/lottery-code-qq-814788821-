using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.Page.PageCode.Users
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UsersRequestManager : BaseRequestManager
    {

        readonly ISysUserService mSysUserService;

        readonly ISysQuotaService mSysQuotaService;

        readonly ISysUserBalanceService mSysUserBalanceService;

        readonly ISysQuotaDetailService mSysQuotaDetailService;//账变详细信息

        private readonly ISysUserBalanceDetailService mSysUserBalanceDetailService;

        readonly ISysSettingService mSysSettingService;

        readonly IBetDetailService mBetDetailService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysUserService"></param>
        /// <param name="sysQuotaService"></param>
        /// <param name="sysUserBalanceService"></param>
        /// <param name="sysQuotaDetailService"></param>
        /// <param name="sysUserBalanceDetailService"></param>
        /// <param name="sysSettingService"></param>
        public UsersRequestManager(ISysUserService sysUserService,
            ISysQuotaService sysQuotaService,
            ISysUserBalanceService sysUserBalanceService,
            ISysQuotaDetailService sysQuotaDetailService,
             ISysUserBalanceDetailService sysUserBalanceDetailService, ISysSettingService sysSettingService,
             IBetDetailService betDetailService)
        {
            this.mSysUserService = sysUserService;
            this.mSysQuotaService = sysQuotaService;
            this.mSysUserBalanceService = sysUserBalanceService;
            this.mSysQuotaDetailService = sysQuotaDetailService;
            this.mSysUserBalanceDetailService = sysUserBalanceDetailService;
            this.mSysSettingService = sysSettingService;
            this.mBetDetailService = betDetailService;
        }

        protected override bool Validation()
        {
            return true;
        }

        protected override void Process()
        {
            switch (ActionName.ToLower())
            {
                case "haslogin":
                    this.HasLogin();
                    break;
                case "childrenusers"://获取我的下级用户列表
                    this.GetChildrenUsers();
                    break;
                case "existsusercode"://是否存在指定用户id
                    this.ExistsUser();
                    break;
                case "adduser"://添加用户
                    this.AddUser();
                    break;
                case "getuser"://获取单个用户信息
                    this.GetUser();
                    break;
                case "edituser"://编辑用户信息
                    this.EditUser();
                    break;
                case "getquota"://获取用户配额
                    this.GetUserQuota();
                    break;
                case "getquotamax"://获取用户配额，配额值大于0
                    this.GetUserQuotaMax();
                    break;
                case "updateuserremo"://修改用户返点
                    this.UpdateUserRemo();
                    break;
                case "groupuseramt"://用户团队余额
                    this.GroupUserAmt();
                    break;
                case "userchildrens":
                    this.GetUserChildrens();
                    break;
                case "userrembparent"://获取子用户，父用户的所有返点
                    this.GetUserRembParent();
                    break;
                case "updateuserquota"://修改用户配额
                    this.UpdateUserQuota();
                    break;
                case "recovery"://回收
                    this.Recovery();
                    break;
                case "addpoints"://升点
                    AddPoints();
                    break;
                case "addpointsattribute"://获取升点按销售额说明数据
                    AppGlobal.RenderResult<List<AddPointsAttribute>>(ApiCode.Success, PointsAttributeHelper.AddPointsAttributes());
                    break;
                case "updaterecharge":
                    this.UpdateUserRecharge();
                    break;
                case "recharge"://给下级充值
                    Recharge();
                    break;
                case "quotabill"://获取配额账变列表
                    GetQuotaBill();
                    break;
                case "qupotafilter"://配额查询
                    QuotaFilter();
                    break;
                case "autosetting"://自动注册返点设置
                    AutoRegistSetting();
                    break;
                case "updategreetings"://修改问候语
                    this.UpdateGreetings();
                    break;
                case "updadwwdasp"://修改用户密码
                    this.UpdateUserPassword();
                    break;
                case "updatezjmpwp"://修改资金密码
                    UpdateUserBalancePassword();
                    break;
                case "inintzjp"://初始化资金密码
                    InintUserBalancePassword();
                    break;
                case "vdzjp"://验证资金密码是否正确
                    VdUserBalancePassword();
                    break;
                case "vdempty"://验证资金密码是否为空
                    VdUserBalanceEmptyPwd();
                    break;
                case "updatenickname"://修改昵称
                    UpdateNickName();
                    break;
                case "getdailybusinesstransaction": //获取用户当天消费额
                    GetDailyBusinessTransaction();
                    return;
                case "fenpei":
                    FenPeiQu();
                    break;
                case "loginlog":
                    GetUserLoginLogs();
                    break;
                case "paruser":
                    GetParentUserList();//获取所有上级用户
                    break;
            }
        }

        private void HasLogin()
        {
            if (this.LoginUser == null)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
            }
            else {
                AppGlobal.RenderResult(ApiCode.Success);
            }
        }

        #region 分配开户额
        private void FenPeiQu()
        {
            string data = Request.Params["data"];
            if (string.IsNullOrEmpty(data))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var array = data.Split(',');
            int compledCount = 0;

            foreach (var item in array)
            {
                //处理
                if (string.IsNullOrEmpty(item))
                    continue;
                var res = item.Split('_');
                int qid;//开户额id
                int quoValue;//开户额值
                if (!int.TryParse(res[0], out qid) || !int.TryParse(res[1], out quoValue))
                    continue;
                compledCount += this.mSysQuotaService.UpdateQuota(LoginUserId, qid, quoValue);
            }
            if (compledCount > 0)
                AppGlobal.RenderResult(ApiCode.Success);
            else
                AppGlobal.RenderResult(ApiCode.Fail);
        }

        #endregion
        #region action

        private void AutoRegistSetting()
        {
            string rembStr = Request["remb"];
            double outRemb = 0.0;
            if (!double.TryParse(rembStr, out outRemb))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            bool isCompled = this.mSysUserService.UpdateAutoRegRebate(this.LoginUserId, outRemb);
            if (isCompled)
                AppGlobal.RenderResult(ApiCode.Success);
            else
                AppGlobal.RenderResult(ApiCode.Fail);
        }

        /// <summary>
        /// 修改用户配额信息
        /// </summary>
        private void UpdateUserQuota()
        {
            string data = Request.QueryString["data"];
            if (string.IsNullOrEmpty(data))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            try
            {
                //解析需要修改的用户配额信息
                var source = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BindSysQuotaDTO>>(data);
                if (null == source)
                    return;

                //修改数据
                foreach (var item in source)
                {
                    //验证
                    if (item.AppendNum > 0 && item.AppendNum + item.SurplusNum < item.ParentSurplusNum)
                        this.mSysQuotaService.UpdateQuota(item);
                }

                AppGlobal.RenderResult(ApiCode.Success);
            }
            catch(Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("UpdateUserQuota", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        /// <summary>
        /// 获取子父用户返点信息
        /// </summary>
        private void GetUserRembParent()
        {
            int uid;
            int parentid;
            if (!int.TryParse(Request.QueryString["uid"], out uid)
                || !int.TryParse(Request.QueryString["pid"], out parentid))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            var result = this.mSysQuotaService.GetUserQuota(uid, parentid);

            AppGlobal.RenderResult<IEnumerable<SysQuota>>(ApiCode.Success, result);
        }

        /// <summary>
        /// 获取用户下的所有子用户
        /// 这里要看要不要把客服、上级的用户给加载出来
        /// </summary>
        private void GetUserChildrens()
        {
            int uid;
            if (!int.TryParse(Request.QueryString["uid"], out uid))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            bool hasCustomer = false; bool hasParent = false;
            if (!bool.TryParse(Request.QueryString["hascustomer"], out hasCustomer)) hasCustomer = false;
            if (!bool.TryParse(Request.QueryString["hasparent"], out hasParent)) hasParent = false;
            var source = this.mSysUserService.GetChildrens(uid, hasCustomer, hasParent);
            AppGlobal.RenderResult<List<TreeDTO>>(ApiCode.Success, source);
        }

        /// <summary>
        /// 获取团队余额
        /// </summary>
        private void GroupUserAmt()
        {
            int uid ;
            if (!int.TryParse(Request.Params["uid"], out uid))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            
            decimal? rendValue = this.mSysUserService.GroupUserAmt(uid);

            if (null == rendValue)
                rendValue = 0;
            AppGlobal.RenderResult<decimal>(ApiCode.Success, rendValue.Value);
        }

        /// <summary>
        /// 获取我的下级用户列表
        /// </summary>
        private void GetChildrenUsers()
        {
            int uid;
            if (!int.TryParse(Request.Params["uid"], out uid))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            string code = Request.Params["code"];//登录名
            decimal startmonery;//账户余额开始
            decimal endmonery;//账户余额结束
            if (!decimal.TryParse(Request.Params["startmonery"], out startmonery))
                startmonery = -1;
            if (!decimal.TryParse(Request.Params["endmonery"], out endmonery))
                endmonery = -1;
            string order = Utils.ChkSQL(Request.Params["order"]);//排序字段
            int orderType;
            if (!int.TryParse(Request.Params["orderType"], out orderType))
                orderType = 0;
            int level;//用户级别
            if (!int.TryParse(Request.Params["level"], out level))
                level = -1;
            bool isSelf = true;
            if (!bool.TryParse(Request.Params["isSelf"], out isSelf)) isSelf = true;

            double startRemb;//开始返点值
            double endRemb;//结束返点值
            if (!double.TryParse(Request.Params["startRemb"], out startRemb))
                startRemb = -1;
            if (!double.TryParse(Request.Params["endRemb"], out endRemb))
                endRemb = -1;
            int pageIndex;//页码
            if (!int.TryParse(Request.Params["pageIndex"], out pageIndex))
                pageIndex = 1;
            int playType = -1;//玩法 0为1800 1为1700
            if (!int.TryParse(Request.Params["playType"], out playType))
                playType = -1;
            

            int totalCount = 0;
            int pageCount = 0;
            var source = this.mSysUserService.GetChildrens(uid, isSelf, code, startmonery, endmonery, order, orderType, level, startRemb, endRemb,playType, pageIndex, ref totalCount, ref pageCount);
            AppGlobal.RenderResult<List<SysUserDTO>>(ApiCode.Success, source, "", pageCount, totalCount);
        }

        /// <summary>
        /// 回收功能
        /// </summary>
        private void Recovery()
        {
            int uid;
            double remb;
            //回收配额
            int quoClear;//是否清除配额
            int chkJd;
            if (!int.TryParse(Request.Params["uid"], out uid))
                uid = -1;
            if (!double.TryParse(Request.Params["rmb"], out remb))
                remb = -1;
            if (!int.TryParse(Request.Params["quoClear"], out quoClear))
                quoClear = -1;
            if (!int.TryParse(Request.Params["chkJd"], out chkJd))
                chkJd = -1;
            if (uid == -1)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var curUser = this.mSysUserService.Get(uid);
            if (curUser == null) {
                AppGlobal.RenderResult(ApiCode.RequestFail);
                return;
            }
            //是否清楚配额
            bool isQuoclear = false;
            if (quoClear == 0)
            {
                
                if (null != curUser)
                {
                    //清除配额
                    this.mSysQuotaService.ClearQuota(uid, curUser.ParentId.Value);
                    isQuoclear = true;
                }
            }
            if (remb > curUser.Rebate && chkJd == 0) //当选择降点时，传输过来的配额值要大于当前用户的配额值
            {
                //验证销售额是否允许降点 获取用户10天的销售额
                var statEndDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 03:00:00");
                DateTime statStartDate = Convert.ToDateTime(statEndDate.AddDays(-10).ToString("yyyy/MM/dd") + " 03:00:00");
                decimal? groupSales = this.mSysUserService.GroupUserSales(uid, statStartDate, statEndDate);//获取团队销售额
                //根据用户返点、获取条件
                if (groupSales != null && !NoRecoveryAttributeHelper.GetPointsAttribuyes(curUser.Rebate, groupSales.Value))
                {
                    //达到销售预期，不允许降点
                    AppGlobal.RenderResult(ApiCode.DisabledCode);
                    return;
                }
                //验证当前是否小于子用户最大返点，小于则修改失败
                var childMax = this.mSysUserService.GetChildMaxRebate(uid);

                if (childMax != -1 && Math.Round(Utils.GetUserRemo(this.LoginUser) - childMax.Value, 1) > Math.Round(Utils.GetUserRemo(this.LoginUser) - remb, 1))
                {
                    //修改失败
                    AppGlobal.RenderResult(ApiCode.ValidationFails);
                    return;
                }
                //获取父用户id
                var item = this.mSysUserService.Get(uid);
                if (null == item)
                {
                    AppGlobal.RenderResult(ApiCode.Fail);
                    return;
                }

                var oldRenbate = Math.Round(Utils.MaxRemo - item.Rebate, 1);//用户原有返点值
                item.Rebate = remb;
                //修改用户配额
                this.mSysQuotaService.UpdateQuota(uid, item.ParentId.Value, remb, 0);
                this.mSysUserService.Save();
                //当用户返点修改成功后，他的投注内容返点也应该跟着改变  2017/02/10 
                mBetDetailService.UpdateCatchBett(uid, (float)(remb));
                //end 
                //还原父用户原有配额值
                this.mSysQuotaService.Restore(LoginUserId, 1, oldRenbate);//增加原来父级配合
                this.mSysQuotaService.SubtractQuota(LoginUserId,  (Math.Round(Utils.MaxRemo-item.Rebate,1)),ActionType.降点增加);//减少父级别当前配合
            }
            else if (!isQuoclear)
            {
                AppGlobal.RenderResult(ApiCode.Security);
                return;
            }
            if (quoClear == -1 && chkJd==-1)
            {
                AppGlobal.RenderResult(ApiCode.Security);
                return;
            }

            AppGlobal.RenderResult(ApiCode.Success);
        }

        #region 升点功能

        /// <summary>
        /// 升点功能
        /// </summary>
        private void AddPoints()
        {
            int uid;//操作用户
            double remb;//升点到值
            int pointsType;//升点类型 0为以量升点 1为配额升点
            if (!int.TryParse(Request.Params["uid"], out uid))
                uid = -1;
            if (!double.TryParse(Request.Params["rmb"], out remb))
                remb = -1;
            if (!int.TryParse(Request.Params["pointsType"], out pointsType))
                pointsType = -1;

            if (uid == -1 || remb == -1 || pointsType == -1)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            ApiCode apicode = ApiCode.Fail;
            switch (pointsType)
            {
                case 0://以量升点
                    apicode = QuoSales(uid, remb);
                    break;
                case 1://配额升点
                    apicode = this.QuoAddPoints(uid, remb);
                    break;
            }

            AppGlobal.RenderResult(apicode);
        }

        /// <summary>
        /// 按量升点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="remb"></param>
        /// <returns></returns>
        private ApiCode QuoSales(int uid, double remb)
        {
            string statCountStr = Request.Params["statcount"];//统计天数
            string statEndDateStr = Request.Params["enddate"];//统计结束日期
            int statCount;
            DateTime statEndDate;
            if (!int.TryParse(statCountStr, out statCount)
                || !DateTime.TryParse(statEndDateStr, out statEndDate)
                || statEndDate > DateTime.Now)
                return ApiCode.ParamEmpty;
            int addDay = 3;//默认三天量
            switch (statCount)
            {
                case 1://七天量
                    addDay = 7;
                    break;
                case 2://十天量
                    addDay = 10;
                    break;
            }
            statEndDate = Convert.ToDateTime(statEndDate.AddDays(1).ToString("yyyy/MM/dd") + " 03:00:00");
            DateTime statStartDate = Convert.ToDateTime(statEndDate.AddDays(-addDay).ToString("yyyy/MM/dd") + " 03:00:00");

            decimal? groupSales = this.mSysUserService.GroupUserSales(uid, statStartDate, statEndDate);//获取团队销售额
            if (null == groupSales)
                groupSales = decimal.Zero;
            var pointsAttributes = PointsAttributeHelper.FindNowPoints(addDay, groupSales.Value);//获取团队销量对应的配额数据
            if (pointsAttributes == null || pointsAttributes.Count<1)
                return ApiCode.ValidationFails;
            var auto = pointsAttributes.Where(x => x.targer == remb).FirstOrDefault();
            if(auto==null)
                return ApiCode.ValidationFails;
            double arrtibuyeRemo = PointsAttributeHelper.ParseRemo(auto.SSC);//验证用户选择返点级别是否和量配置返点级别是否一致
            if (remb != arrtibuyeRemo)
                return ApiCode.ValidationFails;
            //修改用户配额
            var uitem = this.mSysUserService.Get(uid);
            if (uitem == null)
                return ApiCode.Fail;
            uitem.Rebate = remb;
            this.mSysUserService.Save();//满足条件，升级
            return ApiCode.Success;
        }

        /// <summary>
        /// 配额升点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="remb"></param>
        private ApiCode QuoAddPoints(int uid, double remb)
        {
            //获取用户最大上级配额 验证本次修改配额是小于等于上级最大配额
            var curNo = Math.Round(Utils.MaxRemo - remb, 1);
            var parentRebate = this.mSysUserService.GetParentMaxRebate(uid);
            if (parentRebate == null || (Utils.MaxRemo - parentRebate.Value) < curNo)
                return ApiCode.ValidationFails;
            //修改配额
            var item = this.mSysUserService.Get(uid);
            if (null == item)
                return ApiCode.Fail;
            if (Math.Round(Utils.MaxRemo - item.Rebate, 1) == curNo)
                return ApiCode.Security;
            var oldRenbate = Math.Round(Utils.MaxRemo - item.Rebate, 1);//用户原有返点值
            //修改用户配额
            var result = this.mSysQuotaService.UpdateQuota(uid, LoginUserId, remb, 1);
            if (result == 1)
            {
                return ApiCode.ValidationFails;
            }
            item.Rebate = remb;
            this.mSysUserService.Save();
            //当用户返点修改成功后，他的投注内容返点也应该跟着改变  2017/02/10 
            mBetDetailService.UpdateCatchBett(uid, (float)(remb));
            //end 

            //还原父用户原有配额值
            this.mSysQuotaService.Restore(LoginUserId, 1, oldRenbate);
            return ApiCode.Success;
        }

        #endregion
        /// <summary>
        /// 修改用户返点
        /// </summary>
        private void UpdateUserRemo()
        {
            int uid;
            int pid=this.LoginUserId;
            double remb;//返点
            if (!int.TryParse(Request.Params["uid"], out uid)
                || !double.TryParse(Request.Params["rmb"], out remb))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var iscompled = this.mSysUserService.UpdateUserRebate(uid, remb);
            if (!iscompled)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            this.mSysQuotaService.UpdateQuota(uid, pid, remb, 0);

            AppGlobal.RenderResult(ApiCode.Success);
        }

        /// <summary>
        /// 修改开启或者取消用户充值
        /// </summary>
        private void UpdateUserRecharge()
        {
            int uid;
            int isRecharge;
            if (!int.TryParse(Request.Params["uid"], out uid)
                || !int.TryParse(Request.Params["iRecharge"], out isRecharge))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var iscompled = this.mSysUserService.UpdateRecharge(uid, isRecharge == 0 ? false : true);
            if (!iscompled)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            AppGlobal.RenderResult(ApiCode.Success);
        }

        /// <summary>
        /// 获取单个用户信息
        /// </summary>
        public void GetUser()
        {
            int uid;
            if (!int.TryParse(Request.QueryString["uid"], out uid))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var user = this.mSysUserService.Get(uid);
            if (null == user)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
            }


            AppGlobal.RenderResult<SysUser>(ApiCode.Success, user);
        }

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        public void ExistsUser()
        {
            string code = Request.QueryString["code"];
            if (string.IsNullOrEmpty(code))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            var user = this.mSysUserService.Get(code);
            if (null == user)
            {
                AppGlobal.RenderResult(ApiCode.Success);
                return;
            }
            //已经存在该用户
            AppGlobal.RenderResult(ApiCode.Empty);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        private void AddUser()
        {

            int userType;
            if (!int.TryParse(Request.Params["userType"], out userType))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            int parentId = this.LoginUserId;
            string code = Request.Params["code"];
            if (string.IsNullOrEmpty(code))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            //验证用户是否存在
            var exituser = this.mSysUserService.Get(code);
            if (exituser != null)
            {
                AppGlobal.RenderResult(ApiCode.NotScope);
                return;
            }
            string password = Request.Params["password"];
            if (string.IsNullOrEmpty(password))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            string nickName = Request.Params["nickName"];
            double remb;
            if (!double.TryParse(Request.Params["rmb"], out remb))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            if (remb < 0 || remb > 8)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            var oldremb = remb;
            remb = Math.Round(remb, 1) + LoginUser.Rebate;
            UserPlayType playType = LoginUser.PlayType;
            if (LoginUser.UserType == UserType.BasicProy)
            {
                //总代，允许选择下级玩法
                int outPlayType = 0;
                if (int.TryParse(Request.Params["playType"], out outPlayType))
                {
                    playType = outPlayType == 0 ? UserPlayType.P1800 : UserPlayType.P1700;
                    //总代理，1700模式
                    if(playType== UserPlayType.P1700)
                        remb = Math.Round(oldremb, 1) + Convert.ToDouble(Utils.ParseShowRebateName1700(Math.Round(LoginUser.Rebate, 1).ToString()));
                }
            }
            if (LoginUser.UserType == UserType.Main)
            {
                MainAddUser(remb,code,nickName,password);
                return;
            }
            var minRemo = Convert.ToDouble(YtgConfig.GetItem("NotQuotaNum") ?? "5.9");
            SysQuota parentQuota = null;

            if ((Ytg.Comm.Utils.MaxRemo - remb) > minRemo)
            {
                //验证父用户是否有指定阶段配额的额度,并且剩余额度大于1
                parentQuota = this.mSysQuotaService.GetUserQuota(parentId, (Ytg.Comm.Utils.MaxRemo - remb));
                if (parentQuota == null || parentQuota.MaxNum < 1)
                {
                    AppGlobal.RenderResult(ApiCode.ValidationFails);
                    return;
                }
            }

            SysUser user = new SysUser()
            {
                Rebate = Math.Round(remb, 1),
                Code = code,
                NikeName = nickName,
                Password = password,
                UserType = userType == 0 ? UserType.General : UserType.Proxy,
                ParentId = parentId,
                ProxyLevel = this.mSysUserService.Get(parentId).ProxyLevel + 1,
                PlayType = playType,
                Head=LoginUser.Head
            };

            this.mSysUserService.Create(user);
            this.mSysUserService.Save();
            //用户余额插入数据
            UserComm.InintNewUserBalance(user, this.mSysSettingService, this.mSysUserBalanceService, this.mSysUserBalanceDetailService, this.mSysUserService);//初始化新用户金额
            //设置用户配额
            this.mSysQuotaService.InintUserQuota(user.Id, parentId, Math.Round(Ytg.Comm.Utils.MaxRemo - remb,1));
            AppGlobal.RenderResult(ApiCode.Success);
        }
        #region 总管新建用户
        
        /// <summary>
        /// 总管新增总代理
        /// </summary>
        private void MainAddUser(double remb,string code,string nickName,string password)
        {
            SysUser user = new SysUser()
            {
                Rebate = Math.Round(remb, 1),
                Code = code,
                NikeName = nickName,
                Password = password,
                UserType = UserType.BasicProy,
                ParentId = this.LoginUser.Id,
                ProxyLevel = 0,
                PlayType = UserPlayType.P1800
            };
            this.mSysUserService.Create(user);
            this.mSysUserService.Save();
            //用户余额插入数据
            UserComm.InintNewUserBalance(user, this.mSysSettingService, this.mSysUserBalanceService, this.mSysUserBalanceDetailService, this.mSysUserService);//初始化新用户金额
            //设置用户配额
            this.mSysQuotaService.InintUserQuota(user.Id, this.LoginUserId, (Ytg.Comm.Utils.MaxRemo - remb));
            AppGlobal.RenderResult(ApiCode.Success);
        }
        #endregion

        /// <summary>
        /// 修改用户信息
        /// </summary>
        private void EditUser()
        {

            int uid;
            if (!int.TryParse(Request.QueryString["uid"], out uid))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            //获取用户信息
            var user = this.mSysUserService.Get(uid);
            if (null == user)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            int userType;
            if (int.TryParse(Request.QueryString["userType"], out userType))
            {
                user.UserType = userType == 0 ? UserType.General : UserType.Proxy;
            }

            string nickName = Request.QueryString["nickName"];
            user.NikeName = nickName;
            double remb;
            if (double.TryParse(Request.QueryString["rmb"], out remb))
            {
                user.Rebate = remb;
            }
            this.mSysUserService.Save();


            AppGlobal.RenderResult(ApiCode.Success);

        }

        /// <summary>
        /// 开通和关闭充值功能
        /// </summary>
        private void EditRecharge()
        {
            int uid;
            if (!int.TryParse(Request.QueryString["uid"], out uid))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            //获取用户信息
            var user = this.mSysUserService.Get(uid);
            if (null == user)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            int isRecharge;
            if (!int.TryParse(Request.QueryString["iRecharge"], out isRecharge))
            {
                user.IsRecharge = isRecharge == 0 ? false : true;
            }
            this.mSysUserService.Save();
        }

        #region 用户配额

        /// <summary>
        /// 获取用户配额
        /// </summary>
        private void GetUserQuota()
        {
            int uid;
            if (!int.TryParse(Request.QueryString["uid"], out uid))
            {
                AppGlobal.RenderResult(ApiCode.RequestFail);
            }
            var result = this.mSysQuotaService.GetUserQuota(uid);
            AppGlobal.RenderResult<List<SysQuota>>(ApiCode.Success, result);
        }

        /// <summary>
        /// 获取用户配额，配额值大于0
        /// </summary>
        private void GetUserQuotaMax()
        {
            int uid;
            if (!int.TryParse(Request.Params["uid"], out uid))
            {
                AppGlobal.RenderResult(ApiCode.RequestFail);
            }

            //返点类型，为空则获取的是子级别最大返点值.否则回去父级最大返点值
            string type = Request.Params["type"];

            var result = this.mSysQuotaService.GetUserQuotaMax(uid);
            if (null == result || result.Count < 1)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            //获取子用户最大返点
            double? maxRemb = 0;
            try
            {
                if (string.IsNullOrEmpty(type))
                    maxRemb = this.mSysUserService.GetChildMaxRebate(uid);
                else
                    maxRemb = this.mSysUserService.GetParentMaxRebate(uid);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("GetUserQuotaMax", ex);
            }

            AppGlobal.RenderResult<List<SysQuota>>(ApiCode.Success, result, maxRemb.ToString());
        }


        #endregion

        #endregion

        #region 获取配额账变列表
        private void GetQuotaBill()
        {
            int uid;
            bool hasChildren;
            int pageIndex = 1;
            string typeStr = Request.Params["type"];
            string quotypeStr = Request.Params["quotype"];
            string beginDateStr = Request.Params["begindate"];
            string endDateStr = Request.Params["enddate"];
            string userCode = Request.Params["code"];

            if (!int.TryParse(Request.Params["uid"], out uid)
                || !bool.TryParse(Request.Params["hasChildren"], out hasChildren))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            if (!int.TryParse(Request.Params["pageIndex"], out pageIndex))
                pageIndex = 1;

            ActionType? parAcType = null;
            ActionType outActype;
            if (Enum.TryParse<ActionType>(typeStr, out outActype))
                parAcType = outActype;
            DateTime? begindate = null;
            DateTime? endData = null;
            DateTime outBeginDate;
            DateTime outEndDate;
            if (DateTime.TryParse(beginDateStr, out outBeginDate))
                begindate = outBeginDate;
            if (DateTime.TryParse(endDateStr, out outEndDate))
                endData = outEndDate;

            int totalCount = 0;
            int pageCount = 0;
            var source = this.mSysQuotaDetailService.GetAll(uid, hasChildren, parAcType, quotypeStr, userCode, begindate, endData, pageIndex, 20, ref totalCount, ref pageCount);

            AppGlobal.RenderResult<List<SysQuotaDetaiDTO>>(ApiCode.Success, source, "", pageCount, totalCount);
        }

        #endregion

        #region 配额查询
        private void QuotaFilter()
        {
            string userCode = Request.Params["code"];
            string quotaType = Request.Params["quotaType"];
            int quotaWhere = -1;
            if (!int.TryParse(Request.Params["quotaWhere"], out quotaWhere))
                quotaWhere = -1;
            int quotaValue;
            if (!int.TryParse(Request.Params["quotaValue"], out quotaValue))
                quotaValue = -1;

            int uid = this.LoginUserId;
            int pageIndex = 1;
            if (!int.TryParse(Request.Params["pageIndex"], out pageIndex))
                pageIndex = 1;
            int totalCount = 0;
            int pageCount = 0;
            var source = this.mSysQuotaService.FilterQuota(uid, userCode, quotaType, quotaWhere, quotaValue, pageIndex, 999, ref totalCount, ref pageCount);
            AppGlobal.RenderResult<List<FilterSysQuotaDTO>>(ApiCode.Success, source, "", pageCount, totalCount);
        }
        #endregion

        #region 修改问候语
        /// <summary>
        /// 修改问候语
        /// </summary>
        private void UpdateGreetings()
        {
            string greetings = Request.Params["greetings"];
            if (string.IsNullOrEmpty(greetings))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            bool fig = this.mSysUserService.UpdateUserGreetings(this.LoginUserId, greetings);

            if (fig) AppGlobal.RenderResult(ApiCode.Success);
            else AppGlobal.RenderResult(ApiCode.Fail);
        }
        #endregion

        #region 修改登录密码

        /// <summary>
        /// 修改登录密码
        /// </summary>
        private void UpdateUserPassword()
        {
            string oldPassword = Request.Params["oldpwd"];
            string newPassword = Request.Params["newpwd"];

            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || oldPassword == newPassword)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            bool fig = this.mSysUserService.UpdatePassword(this.LoginUserId, newPassword, oldPassword);
            if (fig) AppGlobal.RenderResult(ApiCode.Success);
            else AppGlobal.RenderResult(ApiCode.Fail);
        }

        #endregion

        #region 修改资金密码
        /// <summary>
        /// 修改资金密码
        /// </summary>
        private void UpdateUserBalancePassword()
        {
            string oldPassword = Request.Params["oldpwd"];
            string newPassword = Request.Params["newpwd"];
            if (string.IsNullOrEmpty(newPassword) )
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            bool fig = false;
            if (string.IsNullOrEmpty(oldPassword))
            {
                var iban = this.mSysUserBalanceService.GetUserBalance(this.LoginUserId);
                if (iban == null || !string.IsNullOrEmpty(iban.Pwd))
                {
                    AppGlobal.RenderResult(ApiCode.Fail);
                    return;
                }
                //修改资金密码验证资金密码是否和登陆密码一致，
                if (this.mSysUserService.Get(this.LoginUserId, newPassword) != null)
                {
                    AppGlobal.RenderResult(ApiCode.NotScope);
                    return;
                }

                fig = this.mSysUserBalanceService.InintUserBalancePwd(this.LoginUserId, newPassword);
            }
            else
            {
                //初始化资金密码，验证是否和登录密码一致
                if (this.mSysUserService.Get(this.LoginUserId,newPassword) != null)
                {
                    AppGlobal.RenderResult(ApiCode.NotScope);
                    return;
                }
                fig = this.mSysUserBalanceService.UpdateUserBalancePwd(this.LoginUserId, oldPassword, newPassword);
            }

            if (fig) AppGlobal.RenderResult(ApiCode.Success);
            else AppGlobal.RenderResult(ApiCode.Fail);
        }
        #endregion

        #region 初始化资金密码
        private void InintUserBalancePassword()
        {
            string password = Request.Params["zjm"];//
            if (string.IsNullOrEmpty(password))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var isFig = this.mSysUserBalanceService.InintUserBalancePwd(this.LoginUserId, password);
            if (isFig) AppGlobal.RenderResult(ApiCode.Success);
            else AppGlobal.RenderResult(ApiCode.Fail);
        }
        #endregion

        #region 验证资金密码
        private void VdUserBalancePassword()
        {
            string password = Request.Params["zjm"];//
            if (string.IsNullOrEmpty(password))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var fig = this.mSysUserBalanceService.VdUserBalancePwd(this.LoginUserId, password);
            if (fig) AppGlobal.RenderResult(ApiCode.Success);
            else AppGlobal.RenderResult(ApiCode.Fail);
        }
        #endregion

        #region 验证资金密码是否为空

        private void VdUserBalanceEmptyPwd()
        {
            var fs = this.mSysUserBalanceService.GetAll().Where(item => item.UserId == this.LoginUserId).FirstOrDefault();
            if (null == fs)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }

            if (string.IsNullOrEmpty(fs.Pwd))
            {
                AppGlobal.RenderResult(ApiCode.Success);
            }
            else
            {
                AppGlobal.RenderResult(ApiCode.Success, "0");
            }
        }

        #endregion

        #region 给下级充值
        private void Recharge()
        {
            string inUserCode = Request.Params["incode"];//充值用户账号
            string password = Request.Params["pwd"];//充值用户账号
            string czpq = Request.Params["czpq"];//充值类型 1为分红充值
            decimal inMonery = 0m;//充值金额
            
            if (!decimal.TryParse(Request.Params["inmonery"], out inMonery) ||
                inMonery < 10 || // 充值金额必须大于10 小于等于10000
                string.IsNullOrEmpty(password))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            if (!this.mSysUserBalanceService.VdUserBalancePwd(this.LoginUserId, password))
            {
                //验证资金密码失败
                AppGlobal.RenderResult(ApiCode.Security);
                return;
            }

            //根据用户账号获取用户信息
            var inUserInfo = this.mSysUserService.Get(inUserCode);
            if (null == inUserInfo)//|| inUserInfo.ParentId != this.LoginUserId
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            var baseUser = this.mSysUserService.Get(this.LoginUserId);
            int maxMonery=10000;
            if (baseUser.UserType == UserType.Main || baseUser.UserType == UserType.BasicProy)
                maxMonery = 100000;
            if (inMonery > maxMonery)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            
            //获取当前登录用户信息
            var loginUserBalance = this.mSysUserBalanceService.GetUserBalance(this.LoginUserId);
            if (loginUserBalance.Status == 1) {
                AppGlobal.RenderResult(ApiCode.DisabledMonery);
                return;
            }
            if (null == loginUserBalance ||//金额是否禁用 并且充值的金额不能大于余额
                loginUserBalance.UserAmt < inMonery)
            {
                AppGlobal.RenderResult(ApiCode.Security);
                return;
            }
            var dailiDetails = new SysUserBalanceDetail()
                     {
                         OccDate = DateTime.Now,
                         OpUserId = LoginUserId,
                         RelevanceNo = inUserCode,
                         SerialNo = "d" + Utils.BuilderNum(),
                         Status = 0,
                         TradeAmt = inMonery,
                         TradeType = czpq == "1" ? TradeType.分红 : TradeType.上级充值,
                         UserAmt = loginUserBalance.UserAmt,
                         UserId = inUserInfo.Id,
                     };
            if (this.mSysUserBalanceService.UpdateUserBalance(dailiDetails, inMonery) > 0)
            {
                var removeDetails = new SysUserBalanceDetail()
                     {
                         OccDate = DateTime.Now,
                         OpUserId = this.LoginUserId,
                         RelevanceNo = inUserCode,
                         SerialNo = "d" + Utils.BuilderNum(),
                         Status = 0,
                         TradeAmt = -inMonery,
                         TradeType = TradeType.充值扣费,
                         UserAmt = loginUserBalance.UserAmt,
                         UserId = this.LoginUserId,
                     };
                if (czpq == "1")
                {
                    //算分红扣款
                    removeDetails.TradeType = TradeType.分红扣款;
                }
                //充值成功，减少当前用户余额
                if (this.mSysUserBalanceService.UpdateUserBalance(removeDetails, -inMonery) > 0)
                {
                    //若未上级普通充值，需处理充值流水逻辑
                    if (czpq != "1")
                    {
                        if (!Ytg.ServerWeb.Page.PageCode.UserComm.ManagerRecharge(inMonery, dailiDetails.UserId,this.LoginUser.Id))
                        {
                            AppGlobal.RenderResult(ApiCode.Empty);//充值成功，但流水限制错误
                            return;
                        }
                        //减少自身流水
                    }
                    AppGlobal.RenderResult(ApiCode.Success);//充值成功
                    return;
                }
                else
                {
                    //充值失败，撤销
                    dailiDetails.TradeAmt = -inMonery;
                    this.mSysUserBalanceService.UpdateUserBalance(dailiDetails, inMonery);
                }
            }

        }
        #endregion

        #region 修改用户昵称
        private void UpdateNickName()
        {
            string nickName = Request.Params["nickName"];
            if (string.IsNullOrEmpty(nickName) || nickName.Length>15)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            var loginUser = this.mSysUserService.Get(this.LoginUserId);
            if (null == loginUser)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            loginUser.NikeName = nickName;
            this.mSysUserService.Save();//更新cookie
      
            AppGlobal.RenderResult(ApiCode.Success);
        }
        #endregion

        #region 获取用户当天消费额

        /// <summary>
        /// 获取用户当天消费额
        /// </summary>
        /// <returns></returns>
        public void GetDailyBusinessTransaction()
        {
            //return this.mSysUserService.GetDailyBusinessTransaction(this.LoginUserId);

            int uid;
            if (!int.TryParse(Request.QueryString["uid"], out uid))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            decimal? rendValue = this.mSysUserService.GetDailyBusinessTransaction(uid);
            if (null == rendValue)
                rendValue = 0;
            AppGlobal.RenderResult<decimal>(ApiCode.Success, rendValue.Value);
        }

        #endregion

        #region 获取用户登录日志
        
        /// <summary>
        /// 获取用户登录日志
        /// </summary>
        private void GetUserLoginLogs()
        {
            ISysLogService sysLogService = IoC.Resolve<ISysLogService>();
            int pageIndex = 1;
            if (!int.TryParse(Request.Params["pageindex"], out pageIndex))
                pageIndex = 1;
            string beginStr = "";
            string endStr = "";
            DateTime begin;
            DateTime end;
            if (!DateTime.TryParse(Request.Params["beginDate"], out begin)
                || !DateTime.TryParse(Request.Params["endDate"], out end))
            {
                beginStr = string.Empty;
                endStr = string.Empty;
            }
            else
            {
                beginStr = begin.ToString("yyyy/MM/dd HH:mm:ss");
                endStr = end.ToString("yyyy/MM/dd HH:mm:ss");
            }

            int totalCount = 0;
            var result = sysLogService.SelectLoginLogs(beginStr, endStr, -1, this.LoginUser.Code, pageIndex, ref totalCount);
            AppGlobal.RenderResult<List<SysLoginLogVM>>(ApiCode.Success, result, "", 0, totalCount);
        }
        #endregion

        #region  获取用户所有上级
        private void GetParentUserList()
        {
            string parentId = Request.Params["parentId"];
            List<CatchEntity> newSource = new List<CatchEntity>();

            ISysUserService userService = IoC.Resolve<ISysUserService>();
            var rx = userService.GetParentUsers(Convert.ToInt32(parentId));
            foreach (var item in rx)
            {
                newSource.Add(new CatchEntity()
                {
                    Code = item.Code,
                    Uid = item.Id
                });
            }

            AppGlobal.RenderResult<List<CatchEntity>>(ApiCode.Success, newSource);

        }
        #endregion
    }

    public class CatchEntity
    {
        public string Code { get; set; }

        public int Uid { get; set; }
    }
}