using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Page.PageCode.Repot
{
    /// <summary>
    /// 帐变记录
    /// </summary>
    public class AmountChangeRequestManager : BaseRequestManager
    {
        private readonly ISysUserBalanceDetailService mSysUserBalanceDetailService;

        public AmountChangeRequestManager(ISysUserBalanceDetailService sysUserBalanceDetailService)
        {
            this.mSysUserBalanceDetailService = sysUserBalanceDetailService;
        }

        protected override bool Validation()
        {
            return true;
        }

        
        protected override void Process()
        {
            switch (this.ActionName.ToLower())
            {
                case "selectamountchanglist":
                    //投注
                    this.GetAmoutChangeList();
                    break;
                case "selectprofitlossslist":
                    this.GetProfitLossList();
                    break;
                case "selectprofitlossslist1":
                    this.GetProfitLossList_0();
                    break;
                case "selectstatisticslist":
                    this.GetStatisticsList();
                    break;
            }
        }

        private void GetAmoutChangeList()
        {
            int userId = this.LoginUserId;
            int pageIndex;
            int pageSize;
            int totalCount = 0;
            bool ispageIndex = int.TryParse(Request.Params["pageIndex"], out pageIndex);
            if (!ispageIndex) pageIndex = 1;
            bool ispageSize = int.TryParse(Request.Params["pageSize"], out pageSize);
            if (!ispageSize) pageSize = 20;
            
            try
            {
                string tradeTypes = Request.Params["tradeType"];
                string tradeType=string.Empty;
                if (!string.IsNullOrEmpty(tradeTypes) && tradeTypes.IndexOf("-1") < 0)
                {
                    var arrays = tradeTypes.Split(',');
                    foreach (var item in arrays)
                    {
                        int outValue;
                        if (int.TryParse(item, out outValue))
                            tradeType += item + ",";
                    }
                    if (tradeType.EndsWith(","))
                        tradeType = tradeType.Substring(0, tradeType.Length - 1);
                }

                var startTime = Convert.ToDateTime(Request.Params["startTime"]);
                var endTime = Convert.ToDateTime(Request.Params["endTime"]);
                var tradeDateTime = Convert.ToInt32(Request.Params["tradeDateTime"]);
                var account = Utils.ChkSQL(Request.Params["account"]);

                var userType = Convert.ToInt32(Request.Params["userType"]);
                var codeType = Convert.ToInt32(Request.Params["codeType"]);
                var code =Utils.ChkSQL(Request.Params["code"]);

                var lotteryCode =Utils.ChkSQL(Request.Params["SellotteryCode"]);
                var palyRadioCode = Convert.ToInt32(Request.Params["palyRadioCode"]);
                var issueCode =Utils.ChkSQL(Request.Params["issueCode"]);
                var model = Convert.ToInt32(Request.Params["model"]);
                

                var result = mSysUserBalanceDetailService.SelectBy(tradeType, startTime, endTime, tradeDateTime, account,
                    userType, codeType, code, lotteryCode, palyRadioCode, issueCode, model, userId, pageIndex, pageSize, ref totalCount);
                result = result.OrderByDescending(x => x.OccDate).ToList();
                AppGlobal.RenderResult<List<AmountChangeDTO>>(ApiCode.Success, result, "", pageIndex, totalCount);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("GetAmoutChangeList", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        private void GetProfitLossList()
        {
            int userId= this.LoginUserId;
            int pageIndex;
            int pageSize;
            int totalCount = 0;
            bool ispageIndex = int.TryParse(Request.Params["pageIndex"], out pageIndex);
            if (!ispageIndex) pageIndex = 1;
            bool ispageSize = int.TryParse(Request.Params["pageSize"], out pageSize);
            if (!ispageSize) pageSize = 10;
          
            try
            {
                var startTime = Convert.ToDateTime(Request.Params["startTime"]);
                var endTime = Convert.ToDateTime(Request.Params["endTime"]);
                var account = Request.Params["account"];
                var result1 = mSysUserBalanceDetailService.SelectProfitLossBy(userId, startTime, endTime, account, pageIndex, pageSize, ref totalCount);
                //var result = mSysUserBalanceDetailService.SelectProfitLossByNew(userId, startTime, endTime, account, pageIndex, pageSize, ref totalCount);

                AppGlobal.RenderResult<List<ProfitLossDTO>>(ApiCode.Success, result1, "", pageIndex, totalCount);

                //AppGlobal.RenderResult<List<ProfitLossDTO>>(ApiCode.Success, result, "", pageIndex, totalCount);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("GetProfitLossList", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        private void GetProfitLossList_0()
        {
            int userId = this.LoginUserId;
            int pageIndex;
            int pageSize;
            int totalCount = 0;
            bool ispageIndex = int.TryParse(Request.Params["pageIndex"], out pageIndex);
            if (!ispageIndex) pageIndex = 1;
            bool ispageSize = int.TryParse(Request.Params["pageSize"], out pageSize);
            if (!ispageSize) pageSize = 10;

            try
            {
                var startTime = Convert.ToDateTime(Request.Params["startTime"]);
                var endTime = Convert.ToDateTime(Request.Params["endTime"]);
                var account = Request.Params["account"];
                var result = mSysUserBalanceDetailService.SelectProfitLossByNew(userId, startTime, endTime, account, pageIndex, pageSize, ref totalCount);


                AppGlobal.RenderResult<List<ProfitLossDTO>>(ApiCode.Success, result, "", pageIndex, totalCount);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("GetProfitLossList", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        private void GetStatisticsList()
        {
            int userId;
            bool isUserId = int.TryParse(Request.QueryString["uid"], out userId);
            int pageIndex;
            int pageSize;
            int totalCount = 0;
            bool ispageIndex = int.TryParse(Request.QueryString["pageIndex"], out pageIndex);
            if (!ispageIndex) pageIndex = 1;
            bool ispageSize = int.TryParse(Request.QueryString["pageSize"], out pageSize);
            if (!ispageSize) pageSize = 10;
            if (!isUserId)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            try
            {
                var startTime = Convert.ToDateTime(Request.QueryString["startTime"]);
                var endTime = Convert.ToDateTime(Request.QueryString["endTime"]);
                var account = Request.QueryString["account"];
                var startRebt = Convert.ToDecimal(Request.QueryString["startRebt"]);
                var endRebt = Convert.ToDecimal(Request.QueryString["endRebt"]);
                var isSelf = Convert.ToInt32(Request.QueryString["isSelf"]);
                var type = Convert.ToInt32(Request.QueryString["type"]);
                var minNum = Request.QueryString["minNum"];
                var maxNum = Request.QueryString["maxNum"];
                var result = mSysUserBalanceDetailService.SelectStatisticsReportBy(userId, startTime, endTime,account,startRebt,
                    endRebt,isSelf,type,minNum,maxNum,pageIndex,pageSize,ref totalCount);
                AppGlobal.RenderResult<List<StatisticsReportDTO>>(ApiCode.Success, result, "", pageIndex, totalCount);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("GetStatisticsList", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }
    }
}