using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Page.PageCode.Bank
{
    /// <summary>
    /// 充值相关api
    /// </summary>
    public class BankRequestManager : BaseRequestManager
    {
        private readonly ISysBankTransferService mSysBankTransferService;

        private readonly ICompanyBankService mCompanyBankService;

        private readonly ISysUserBankService mSysUserBankService;

        private readonly ISysBankType mSysBankTypeService;

        private readonly IVipMentionApplyService mVipMentionApplyService;

        private readonly ISysUserService mSysUserService;

        private readonly ISysUserBalanceService mSysUserBalanceService;

        private readonly IMentionQueusService mMentionQueusService;

        public BankRequestManager(ISysBankTransferService sysBankTransferService,
            ICompanyBankService companyBankService,
            ISysUserBankService sysUserBankService,
            ISysBankType sysBankTypeService,
            IVipMentionApplyService vipMentionApplyService,
            ISysUserService sysUserService,
            ISysUserBalanceService sysUserBalanceService, IMentionQueusService mentionQueusService)
        {
            this.mSysBankTransferService = sysBankTransferService;
            this.mCompanyBankService = companyBankService;
            this.mSysUserBankService = sysUserBankService;
            this.mSysBankTypeService = sysBankTypeService;
            this.mVipMentionApplyService = vipMentionApplyService;
            this.mSysUserService = sysUserService;
            this.mSysUserBalanceService = sysUserBalanceService;
            this.mMentionQueusService = mentionQueusService;
        }

        protected override bool Validation()
        {
            return true;
        }

        protected override void Process()
        {
            switch (this.ActionName.ToLower())
            {
                case "selectallbanktransfer":
                    SelectAllBankTransfer();
                    break;
                case "selectcompanybank":
                    SelectCompanyBank();
                    break;
                case "getuserbankandbalancepwd":
                    GetUserBankAndBalancePwd();
                    break;
                case "getuserbanks":
                    GetUserBanks();
                    break;
                case "getallbank":
                    GetAllBanks();
                    break;
                case "getprovinces":
                    GetProvinces();
                    break;
                case "getcitys":
                    GetCitys();
                    break;
                case "createuserbank":
                    CreateUserBank();
                    break;
                case "getmetionuserbank":
                    GetUserMetionBank();
                    break;
                case "submitmention":
                    SubmitMention();
                    break;
                case "selectmention":
                    SelectMentions();
                    break;
                case "openvip"://关闭、开通vip充提申请
                    OpenVip();
                    break;
                case "existapply":
                    ExistMentionApply();
                    break;
                case "suoding"://锁定银行卡
                    this.SuoDingCard();
                    break;
                case "unlook"://解除锁定，只允许用户自身操作一次
                    UnLook();
                    break;
                case "getbanktypes"://获取支持充值的银行类型
                    GetRechargeBankTypes();
                    break;
            }
        }

        /// <summary>
        /// 获得所有的收款银行帐号
        /// </summary>
        private void SelectAllBankTransfer()
        {
            int userId;
            bool isUserId = int.TryParse(Request.QueryString["uid"], out userId);
            if (!isUserId)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            try
            {
                var result = mSysBankTransferService.SelectAll(true, userId);
                AppGlobal.RenderResult<List<SysBankTransferVM>>(ApiCode.Success, result, "", 1, result.Count);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("SelectAllBankTransfer", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        /// <summary>
        /// 获得公司的银行帐号信息
        /// </summary>
        private void SelectCompanyBank()
        {
            int userId;
            bool isUserId = int.TryParse(Request.QueryString["uid"], out userId);
            if (!isUserId)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            int bankId;
            bool isBankId = int.TryParse(Request.QueryString["bankId"], out bankId);
            if (!isBankId)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            //验证码判断
            var sCode = System.Web.HttpContext.Current.Session["mRecharge"];
            if (sCode == null || sCode.ToString() != Request.Params["code"])
            {
                AppGlobal.RenderResult(ApiCode.Security);
                return;
            }
            try
            {
                var amt = 0m;
                decimal.TryParse(Request.QueryString["amt"], out amt);
                var result = mCompanyBankService.GetRechargeBank(bankId, userId, amt);


                AppGlobal.RenderResult<List<CompanyBankVM>>(ApiCode.Success, result, "", 1, 1);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("SelectCompanyBank", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        /// <summary>
        /// 获得用户是否设置了资金密码、绑定银行帐号，是否符合充值条件
        /// </summary>
        private void GetUserBankAndBalancePwd()
        {
            int userId;
            bool isUserId = int.TryParse(Request.QueryString["uid"], out userId);
            if (!isUserId)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            try
            {
                var result = mSysUserBankService.GetUserBankAndBalancePwd(userId);
                AppGlobal.RenderResult<RechargeMentionStatus>(ApiCode.Success, result);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("GetUserBankAndBalancePwd", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        /// <summary>
        /// 得到用户的提现银行帐号信息
        /// </summary>
        private void GetUserBanks()
        {
            int userId = this.LoginUserId;
            int pageIndex;
            if (!int.TryParse(Request.Params["pageIndex"], out pageIndex)) pageIndex = 1;
            try
            {
                int totalCount = 0;
                var userCode = string.Empty;
                var userNikeName = string.Empty;
                var bankNo = string.Empty;
                var result = mSysUserBankService.SelectUserBank(userId, -1, userCode, userNikeName, bankNo, -1, pageIndex, ref totalCount);

                foreach (var c in result)
                {
                    //string last = string.Join("", c.BankNo.Skip(c.BankNo.Length - 3));
                    //c.BankNo = "******" + last;
                    c.BankNo = Utils.PaseShowBankNum(c.BankNo);
                }

                //获取用户银行绑定状态
                var user = this.mSysUserService.Get(userId);
                if (user == null)
                {
                    AppGlobal.RenderResult(ApiCode.Fail);
                    return;
                }
                AppGlobal.RenderResult<List<SysUserBankVM>>(ApiCode.Success, result, user.IsLockCards.ToString(), 1, totalCount);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("GetUserBanks", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        /// <summary>
        /// 绑定银行帐号的时候 获得系统里面的所有银行基本设置信息
        /// </summary>
        private void GetAllBanks()
        {
            try
            {
                var result = mSysBankTypeService.SelectAllBankType();
                var totalCount = 0;
                AppGlobal.RenderResult<List<SysBankTypeDTO>>(ApiCode.Success, result, "", 1, totalCount);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("GetAllBanks", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        /// <summary>
        /// 省份信息
        /// </summary>
        private void GetProvinces()
        {
            try
            {
                var result = mSysBankTypeService.SelectAllProvinces();
                var totalCount = 0;
                AppGlobal.RenderResult<List<ProvinceDTO>>(ApiCode.Success, result, "", 1, totalCount);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("GetProvinces", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        /// <summary>
        /// 城市
        /// </summary>
        private void GetCitys()
        {
            int pId;
            if (!int.TryParse(Request.Params["pId"], out pId))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            try
            {
                var result = mSysBankTypeService.SelectAllCitys(pId);
                var totalCount = 0;
                AppGlobal.RenderResult<List<CityDTO>>(ApiCode.Success, result, "", 1, totalCount);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("GetCitys", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        /// <summary>
        /// 绑定银行卡
        /// </summary>
        private void CreateUserBank()
        {
            int userId;
            bool isUserId = int.TryParse(Request.QueryString["uid"], out userId);
            if (!isUserId)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            int bankId;
            if (!int.TryParse(Request.QueryString["bId"], out bankId))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            int pId;
            if (!int.TryParse(Request.QueryString["pId"], out pId))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            int cId;
            if (!int.TryParse(Request.QueryString["cId"], out cId))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            var branch = Request.QueryString["branch"];
            if (string.IsNullOrEmpty(Request.QueryString["bankOwner"]))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var bankOwner = Request.QueryString["bankOwner"];

            if (string.IsNullOrEmpty(Request.QueryString["bankNo"]))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var bankNo = Request.QueryString["bankNo"];
            try
            {
                var bank = new SysUserBank
                {
                    BankNo = bankNo,
                    BankId = bankId,
                    BankOwner = bankOwner,
                    Branch = branch,
                    CityId = cId,
                    IsDelete = false,
                    OccDate = DateTime.Now,
                    ProvinceId = pId,
                    UserId = userId
                };
                var result = this.mSysUserBankService.CreateBank(bank);
                AppGlobal.RenderResult<bool>(ApiCode.Success, result);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("CreateUserBank", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        /// <summary>
        /// 得到提现银行卡信息
        /// </summary>
        private void GetUserMetionBank()
        {
            int userId;
            bool isUserId = int.TryParse(Request.QueryString["uid"], out userId);
            if (!isUserId)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            try
            {
                var result = mSysUserBankService.SelectMentionBank(userId);
                AppGlobal.RenderResult<List<UserMentionDTO>>(ApiCode.Success, result);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("GetUserMetionBank", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        /// <summary>
        /// 发起提现
        /// </summary>
        private void SubmitMention()
        {
            int userId;
            bool isUserId = int.TryParse(Request.Params["uid"], out userId);
            if (!isUserId)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            int bankId;
            if (!int.TryParse(Request.Params["bId"], out bankId))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            decimal mAmt;
            if (!decimal.TryParse(Request.Params["mAmt"], out mAmt))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }

            string pwd = Request.Params["pwd"];//资金密码
            if (string.IsNullOrEmpty(pwd))
            {
                AppGlobal.RenderResult(ApiCode.Security);
                return;
            }
            if (!this.mSysUserBalanceService.VdUserBalancePwd(this.LoginUserId, pwd))//验证资金密码失败
            {
                AppGlobal.RenderResult(ApiCode.Security);
                return;
            }

            try
            {
                var result = this.mSysUserBankService.SubmitMention(bankId, mAmt, userId);
                string msg = "提现申请成功";
                if (result == -1) msg = "余额不足，提现申请不成功";
                else if (result == -2) msg = "系统异常，稍后再试";
                AppGlobal.RenderResult<string>(ApiCode.Success, msg, result);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("SubmitMention", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        /// <summary>
        /// 查看提现信息
        /// </summary>
        private void SelectMentions()
        {
            int userId = this.LoginUserId;

            int pageIndex;
            int totalCount = 0;
            if (!int.TryParse(Request.Params["pageIndex"], out pageIndex)) pageIndex = 1;
            try
            {
                var sDate = string.Empty;
                var eDate = string.Empty;
                sDate = Request.Params["sDate"];
                eDate = Request.Params["eDate"];
                int type = -1;
                if (!int.TryParse(Request.Params["tp"], out type))
                    type = -1;

                List<Ytg.BasicModel.DTO.MentionDTO> result = this.mMentionQueusService.SelectBy(this.LoginUser.Code, "", sDate, eDate, -1, -1, type, pageIndex, ref totalCount);
                foreach (var item in result)
                {
                    //string last = string.Join("", item.BankNo.Skip(item.BankNo.Length - 4));
                    //item.BankNo = "******" + last;
                    item.BankNo = Utils.PaseShowBankNum(item.BankNo);
                }
                
                AppGlobal.RenderResult<List<MentionDTO>>(ApiCode.Success, result, "", pageIndex, totalCount);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("SelectMentions", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        #region 关闭、开通VIP充提申请
        private void OpenVip()
        {
            int userId;
            if (!int.TryParse(Request.QueryString["uid"], out userId))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            try
            {
                var isOpenVip = Convert.ToBoolean(Request.QueryString["isOpenVip"]);
                var result = mVipMentionApplyService.CreateApply(isOpenVip, userId);
                AppGlobal.RenderResult<bool>(ApiCode.Success, result);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("OpenVip", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }
        #endregion

        #region 看是否存在VIP充提申请记录
        private void ExistMentionApply()
        {
            int userId;
            if (!int.TryParse(Request.QueryString["uid"], out userId))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            try
            {
                var result = mVipMentionApplyService.Exist(userId);
                AppGlobal.RenderResult<bool>(ApiCode.Success, result);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("ExistMentionApply", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }
        #endregion

        #region 锁定银行卡

        private void SuoDingCard()
        {
            string password = Request.Params["pwd"];
            if (string.IsNullOrEmpty(password))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            //验证资金密码
            if (!this.mSysUserBalanceService.VdUserBalancePwd(this.LoginUserId, password))
            {
                AppGlobal.RenderResult(ApiCode.Security);
                return;
            }
            //修改为锁定
            if (this.mSysUserService.LockUserCards(this.LoginUserId))
                AppGlobal.RenderResult(ApiCode.Success);
            else
                AppGlobal.RenderResult(ApiCode.Fail);

        }
        #endregion

        #region 用户解锁银行卡

        private void UnLook()
        {
            string data = Request.Params["data"];
            if (string.IsNullOrEmpty(data))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
            }
            var pars = Newtonsoft.Json.JsonConvert.DeserializeObject<UnLockParamsDTO>(data);
            if (null == pars)
            {
                AppGlobal.RenderResult(ApiCode.Fail);
                return;
            }
            var user = this.mSysUserService.Get(this.LoginUserId);
            if (null == user || user.UserLockCount > 0)//用户已解锁过
            {
                AppGlobal.RenderResult(ApiCode.NotScope);
                return;
            }
            //验证输入的银行卡信息是否正确，资金密码是否正确
            var source = this.mSysUserBankService.Where(c => c.UserId == this.LoginUserId).ToList();
            //验证资金密码
            var vdZjmm = this.mSysUserBalanceService.VdUserBalancePwd(this.LoginUserId, pars.Password);
            if (!vdZjmm || source.Count != pars.Cards.Count)
            {
                AppGlobal.RenderResult(ApiCode.Security);
                return;
            }
            foreach (var card in source)
            {
                if (card.BankOwner != pars.UserName ||
                    !pars.Cards.Contains(card.BankNo))
                {
                    AppGlobal.RenderResult(ApiCode.Security);
                    return;
                }
            }
            //可以修改了
            user.IsLockCards = false;
            user.UserLockCount++;
            this.mSysUserService.Save();

            AppGlobal.RenderResult(ApiCode.Success);
        }
        #endregion

        #region 获取支持充值的银行类型

        private void GetRechargeBankTypes()
        {
            var source = this.mSysBankTypeService.GetRechargeBankTypes(true);
            AppGlobal.RenderResult<List<RechargeBankTypeDTO>>(ApiCode.Success, source);
        }
        #endregion
    }
}