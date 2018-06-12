using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.BasicModel.LotteryBasic;
using Ytg.BasicModel.Report;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;
using Ytg.Data;

namespace Ytg.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SysUserBalanceDetailService : CrudService<SysUserBalanceDetail>, ISysUserBalanceDetailService
    {
        readonly ISysUserBalanceService mSysUserBalanceService;
        //readonly IBetDetailService mBetDetailService;
        public SysUserBalanceDetailService(IRepo<SysUserBalanceDetail> repo, ISysUserBalanceService sysUserBalanceService)
            : base(repo)
        {
            this.mSysUserBalanceService = sysUserBalanceService;
            //this.mBetDetailService = betDetailService;
        }

        /// <summary>
        /// 前端查询：帐变
        /// </summary>
        /// <param name="tradeType">类型</param>
        /// <param name="startTime">帐变开始时间</param>
        /// <param name="endTime">帐变结束日期</param>
        /// <param name="tradeDateTime">交易类型</param>
        /// <param name="account">用户名</param>
        /// <param name="userType">范围(用户类型)</param>
        /// <param name="codeType">编号查询</param>
        /// <param name="code">编号</param>
        /// <param name="lotteryCode">游戏名称</param>
        /// <param name="palyRadioCode">玩法</param>
        /// <param name="issueCode">期数</param>
        /// <param name="model">所有模式-1 0元、1角、2分</param>
        /// <param name="userId">登录用户</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<AmountChangeDTO> SelectBy(string tradeType, DateTime startTime, DateTime endTime, int tradeDateTime,
            string account, int userType, int codeType, string code, string lotteryCode, int palyRadioCode,
            string issueCode, int model, int userId, int pageIndex, int pageSize, ref int totalCount)
        {
            pageSize = AppGlobal.ManagerDataPageSize;
            #region old
            /*
            #region 设置参数
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@tradeType",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@startTime",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@endTime",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@tradeDateTime",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@account",System.Data.DbType.String),
                
                new DbContextFactory().GetDbParameter("@userType",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@codeType",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@code",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@lotteryCode",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@palyRadioCode",System.Data.DbType.Int32),
                
                new DbContextFactory().GetDbParameter("@issueCode",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@model",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@userId",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@trade",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output)
            };

            parameters[0].Value = string.IsNullOrEmpty(tradeType) ? "0" : tradeType;
            parameters[1].Value = startTime;
            parameters[2].Value = endTime.AddSeconds(1);
            parameters[3].Value = tradeDateTime;
            parameters[4].Value = account;
            parameters[5].Value = userType;

            parameters[6].Value = codeType;
            parameters[7].Value = code;
            parameters[8].Value = lotteryCode;
            parameters[9].Value = palyRadioCode;
            parameters[10].Value = issueCode;
            parameters[11].Value = model;
            parameters[12].Value = userId;
            parameters[13].Value = pageIndex;
            parameters[14].Value = pageSize;
            parameters[15].Value = string.IsNullOrEmpty(tradeType)?-1:0;
            #endregion
            var list = this.ExProc<AmountChangeDTO>("sp_SysUserBlanceDetails_SelectBy", parameters);
            totalCount = Convert.ToInt32(parameters[16].Value);
            return list;
             */
            #endregion

            string beginDate = Utils.GetNowBeginDate().ToString("yyyy/MM/dd ") + startTime.ToString(" HH:mm:ss");
            string endDate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd ") + endTime.ToString(" HH:mm:ss");
            if (tradeDateTime == 2)//历史记录
            {
                beginDate = startTime.ToString("yyyy/MM/dd HH:mm:ss");
                endDate = endTime.ToString("yyyy/MM/dd HH:mm:ss");
            }
            
            string fmt= string.Format("  OccDate between '{0}' and '{1}'", beginDate, endDate);
            string fmt1 = string.Format("  cni.OccDate between '{0}' and '{1}'", beginDate, endDate);
            

            string sql = string.Format(@"select t8.PostionName,t8.Id,t8.SerialNo,u.Code as UserAccount,t8.OccDate,t8.TradeType,
		lv.LotteryName,lv.PlayTypeName,lv.PlayTypeNumName,lv.PlayTypeRadioName,
		t8.IssueCode,t8.Model,(CASE WHEN t8.TradeAmt>0 THEN t8.TradeAmt ELSE 0 END) AS InAmt,
		(CASE WHEN t8.TradeAmt<0 THEN t8.TradeAmt ELSE 0 END) AS OutAmt,
		t8.UserAmt,t8.RelevanceNo
	  from (
		  SELECT Id,bu.UserId,SerialNo,TradeAmt,UserAmt,TradeType,Status,RelevanceNo,OpUserId,OccDate,BankId,
	  IssueCode,Model,BetCode,PalyRadioCode,PostionName FROM (
			  select * from dbo.SysUserBalanceDetails where  {0}
			) bu
			inner JOIN (
					select UserId,IssueCode,Model,BetCode,PalyRadioCode,PostionName from BetDetails where  {0}
				union all
					select UserId,beginissueCode as IssueCode,Model,CatchNumCode as BetCode,PalyRadioCode,PostionName from CatchNums as cn where {0}
                union all
					select UserId,IssueCode,Model,BuyTogetherCode as BetCode,PalyRadioCode,PostionName from BuyTogethers where {0}
				union all
					select UserId,cni.IssueCode,Model,cni.CatchNumIssueCode as CatchNumCode,PalyRadioCode,PostionName from CatchNums as cn
					inner join  CatchNumIssues as cni on cni.CatchNumCode=cn.CatchNumCode
					where cni.Stauts<>3 and {1}
			 ) as bd on bd.BetCode=bu.RelevanceNo
			 union all
			 select *,null,null,null,null,'' from dbo.SysUserBalanceDetails where TradeType not in(3,4,5,6,7,8,9,10,11) and {0}
     ) as t8 
	LEFT JOIN Lottery_Vw as lv on lv.RadioCode=t8.PalyRadioCode
	LEFT JOIN dbo.SysYtgUser u ON u.Id=t8.UserId where 1=1", fmt, fmt1);

            string userWhere = "";
            string putWhere = "";
            switch (userType)
            {
                case 1:
                    userWhere = string.Format("SELECT Id FROM SysYtgUser where Id={0}", userId);
                    break;
                case 2:
                    userWhere = string.Format("SELECT Id FROM SysYtgUser where ParentId={0}", userId);
                    break;
                case 3:
                    userWhere = string.Format("SELECT * FROM f_SysYtgUser_GetChildren({0})", userId);
                    break;
                case -1://所有
                    userWhere = string.Format("SELECT * FROM f_SysYtgUser_GetChildren({0})", userId);
                    putWhere = string.Format(" or  t8.UserId={0}", userId);
                    break;
            }

            
            if (!string.IsNullOrEmpty(account))
                sql += " and u.Code = '" + account + "'";
            else
                sql += string.Format(" and( t8.UserId in ({0}) {1} )", userWhere, putWhere);

            if (!string.IsNullOrEmpty(tradeType)) //类型
                sql += " and t8.TradeType in(" + tradeType + ")";

            
            
            if (codeType ==1||
                codeType == 2)
                sql += " and t8.RelevanceNo like '%" + code + "%'";//编号查询 注单编号 追号编号
            else if(codeType==3)
                sql += " and t8.SerialNo like '%" + code + "%'";//帐变编号

            if (!string.IsNullOrEmpty(lotteryCode))
                sql += " and lv.LotteryCode='" + lotteryCode + "'";
            if(palyRadioCode!=-1)
                sql += " and lv.RadioCode='" + palyRadioCode + "'";
            if(!string.IsNullOrEmpty(issueCode))
                sql += " and t8.IssueCode='" + issueCode + "'";
            if (model != -1)
                sql += " and t8.Model=" + model;

            sql = "(" + sql + ") as t1";
            int pageCount = 0;
            return this.GetEntitysPage<AmountChangeDTO>(sql, "Id", "*", " IssueCode desc", ESortType.DESC, pageIndex, pageSize, ref pageCount, ref totalCount);
        }


        /// <summary>
        /// 盈亏列表
        /// </summary>
        /// <param name="currenyUserId">当前用户ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="account">用户，精确查找</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<ProfitLossDTO> SelectProfitLossBy(int currenyUserId, DateTime startTime, DateTime endTime,
            string account, int pageIndex, int pageSize, ref int totalCount)
        {
            #region 设置参数
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@currenyUserId",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@account",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@startTime",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@endTime",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output)
            };

            parameters[0].Value = currenyUserId;
            parameters[1].Value = account;
            parameters[2].Value = startTime;
            parameters[3].Value = endTime.AddSeconds(1);
            parameters[4].Value = pageIndex;
            parameters[5].Value = pageSize;

            #endregion
            //那个和是到这里拆分 还是页面上拆分呢？
            var list = this.ExProc<ProfitLossDTO>("sp_SysUserBalance_ProfitLoss", parameters);

            totalCount = parameters[6].Value != DBNull.Value ? Convert.ToInt32(parameters[6].Value) : 0;
           
            return list;
        }

        /// <summary>
        /// 盈亏列表  20170315 by add
        /// </summary>
        /// <param name="currenyUserId">当前用户ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="account">用户，精确查找</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<ProfitLossDTO> SelectProfitLossByNew(int currenyUserId, DateTime startTime, DateTime endTime,
            string account, int pageIndex, int pageSize, ref int totalCount)
        {


            /**判断是否为查询当天数据，若只查询当天数据，若为当天数据，使用原有方法进行统计*/
            //获取当天日期查询的天数
            DateTime curBegin = Utils.GetNowBeginDate();
            DateTime curEnd = Utils.GetNowEndDate();
            int curDay = Convert.ToInt32(curBegin.ToString("yyyyMMdd"));
            int curEndDay = Convert.ToInt32(curEnd.ToString("yyyyMMdd"));


            //查询条件日期
            int nowDay = Convert.ToInt32(startTime.ToString("yyyyMMdd"));
            int preDay = Convert.ToInt32(endTime.ToString("yyyyMMdd"));

            List<ProfitLossDTO> result = new List<ProfitLossDTO>();

            //当天查询的开始结束日期相等，只需要查询当天的结果
            long rticks = endTime.Subtract(curBegin).Ticks;
            long sticks = startTime.Subtract(curBegin).Ticks;
            if (rticks >= 0 //查询条件结束时间大于等于当天默认结束时间
                &&
                 sticks >= 0  //开始时间大于等于当天默认开始时间
                )
            {
                result = SelectProfitLossBy(currenyUserId, startTime, endTime, account, pageIndex, pageSize, ref totalCount);
            }
            else
            {
                //非当天的查询结果，需对查询条件进行拆分
                preDay = Convert.ToInt32(endTime.AddDays(-1).ToString("yyyyMMdd"));

                string newWhere = "";
                if (currenyUserId == -1)
                {
                    newWhere = " ParentId is null ";
                }
                else
                {
                    if (string.IsNullOrEmpty(account))
                        newWhere = " UserId in(select id from SysYtgUser where Id=" + currenyUserId + " or ParentId=" + currenyUserId + ") ";
                    else
                        newWhere = " UserId in(select id from SysYtgUser where Code='" + account + "' or ParentId in(select id from SysYtgUser where code='" + account + "')) ";
                }

                /* string sql = @"select tl.*,CONVERT(decimal,sy.Rebate) from 
 (select 
 UserId  as Id,UserId,Code,ParentId,PlayType,
 sum([Chongzhi]) as [Chongzhi],sum([Tixian]) as [Tixian],sum([Touzhu]) as [Touzhu],sum([Zhuihaokoukuan]) as [Zhuihaokoukuan],sum([Zhuihaofankuan]) as [Zhuihaofankuan],
 sum([Youxifandian]) as [Youxifandian],sum([Jiangjinpaisong]) as [Jiangjinpaisong],sum([Chedanfankuan]) as [Chedanfankuan],sum([Chedanshouxufei]) as [Chedanshouxufei],
 sum([Chexiaofandian]) as [Chexiaofandian],sum([Chexiaopaijiang]) as [Chexiaopaijiang],sum([Chongzhikoufei]) as [Chongzhikoufei],sum([ShangjiChongzhi]) as [ShangjiChongzhi],
 sum([Huodonglijin]) as [Huodonglijin],sum([Fenhong]) as [Fenhong],sum([Qita]) as [Qita],sum([TiXianShiBai]) as [TiXianShiBai],sum([CheXiaoTiXian]) as  [CheXiaoTiXian],sum([ManZheng]) as [ManZheng],
 sum([QianDao]) as [QianDao],sum([ZhuChe]) as [ZhuChe],sum([ChongZhiActivity]) as [ChongZhiActivity],sum([YongJing]) as [YongJing],sum([XinYunDaZhuanPan]) as [XinYunDaZhuanPan],sum([Expr1]) as [Expr1]
 from [dbo].[ProfitLossDTOManTasks] where " + newWhere + " and occday between " + nowDay + " and " + preDay + " group by UserId,Code,ParentId,PlayType) as tl left join SysYtgUser as sy on sy.Id=tl.id";
                 */
                string sql = @"
select tl.*,CONVERT(decimal,sy.Rebate) from 
(select 
UserId  as Id,UserId,Code,ParentId,PlayType,
((SUM(Chongzhi)+SUM(ShangjiChongzhi)+SUM(Huodonglijin)+SUM(FenHong)+SUM(ManZheng)+SUM(QianDao)+SUM(ZhuChe)+SUM(ChongZhiActivity)+SUM(YongJing)+SUM(XinYunDaZhuanPan)+SUM(Chongzhikoufei))) as [Chongzhi],sum([Tixian]) as [Tixian],sum([Touzhu]) as [Touzhu],sum([Zhuihaokoukuan]) as [Zhuihaokoukuan],sum([Zhuihaofankuan]) as [Zhuihaofankuan],
sum([Youxifandian]) as [Youxifandian],sum([Jiangjinpaisong]) as [Jiangjinpaisong],sum([Chedanfankuan]) as [Chedanfankuan],sum([Chedanshouxufei]) as [Chedanshouxufei],
sum([Chexiaofandian]) as [Chexiaofandian],sum([Chexiaopaijiang]) as [Chexiaopaijiang],sum([Chongzhikoufei]) as [Chongzhikoufei],sum([ShangjiChongzhi]) as [ShangjiChongzhi],
sum([Huodonglijin]) as [Huodonglijin],sum([Qita]) as [Qita]
from [dbo].[ProfitLossDTOManTasks] where " + newWhere + " and occday between " + nowDay + " and " + preDay + " group by UserId,Code,ParentId,PlayType) as tl left join SysYtgUser as sy on sy.Id=tl.id order by Code desc";
                result = this.GetSqlSource<ProfitLossDTO>(sql);

                //排序，将总账号排到第一
                if (result != null) {
                    var nowCu= result.Where(x => x.Id == currenyUserId).FirstOrDefault();
                    if (nowCu != null)
                    {
                        result.Remove(nowCu);
                        result.Insert(0,nowCu);
                    }
                }

                if (rticks >= 0)
                {
                    //查询当天结果,
                    //此方法用于查询当天数据，
                    var cbd = curBegin.AddDays(-1);
                    if (preDay < curEndDay)
                        curEnd = endTime;
                    var nowResult = SelectProfitLossBy(currenyUserId, cbd, curEnd,account, pageIndex, pageSize, ref totalCount);
                    
                    //result.AddRange(nowResult);
                    foreach (var item in nowResult)
                    {
                        var lsItem = result.Where(x => x.Code == item.Code).FirstOrDefault();
                        if (lsItem == null)
                        {
                            result.Add(item);
                        }
                        else
                        {
                            lsItem.Chedanfankuan = lsItem.Chedanfankuan + (item.Chedanfankuan ?? 0);
                            lsItem.Chedanshouxufei = lsItem.Chedanshouxufei + (item.Chedanshouxufei ?? 0);
                            lsItem.Chexiaofandian = lsItem.Chexiaofandian + (item.Chexiaofandian ?? 0);
                            lsItem.Chexiaopaijiang = lsItem.Chexiaopaijiang + (item.Chexiaopaijiang ?? 0);
                            //                            lsItem.CheXiaoTiXian = lsItem.CheXiaoTiXian + (item.CheXiaoTiXian ?? 0);
                            lsItem.Chongzhi = lsItem.Chongzhi + (item.Chongzhi ?? 0);
                            //                          lsItem.ChongZhiActivity = lsItem.ChongZhiActivity + (item.ChongZhiActivity ?? 0);
                            lsItem.Chongzhikoufei = lsItem.Chongzhikoufei + (item.Chongzhikoufei ?? 0);
                            //                        lsItem.Expr1 = lsItem.Expr1 + (item.Expr1 ?? 0);
                            lsItem.Fenhong = lsItem.Fenhong + (item.Fenhong ?? 0);
                            lsItem.Huodonglijin = lsItem.Huodonglijin + (item.Huodonglijin ?? 0);
                            lsItem.Jiangjinpaisong = lsItem.Jiangjinpaisong + (item.Jiangjinpaisong ?? 0);
                            //                      lsItem.ManZheng = lsItem.ManZheng + (item.ManZheng ?? 0);
                            //                    lsItem.PlayType = lsItem.PlayType;
                            //                  lsItem.QianDao = lsItem.QianDao + (item.QianDao ?? 0);
                            lsItem.Qita = lsItem.Qita + (item.Qita ?? 0);
                            // lsItem.Rebate = lsItem.Rebate;
                            lsItem.ShangjiChongzhi = lsItem.ShangjiChongzhi + (item.ShangjiChongzhi ?? 0);
                            lsItem.Tixian = lsItem.Tixian + (item.Tixian ?? 0);
                            //  lsItem.TiXianShiBai = lsItem.TiXianShiBai + (item.TiXianShiBai ?? 0);
                            lsItem.Touzhu = lsItem.Touzhu + (item.Touzhu ?? 0);
                            // lsItem.XinYunDaZhuanPan = lsItem.XinYunDaZhuanPan + (item.XinYunDaZhuanPan ?? 0);
                            // lsItem.YongJing = lsItem.YongJing + (item.YongJing ?? 0);
                            lsItem.Youxifandian = lsItem.Youxifandian + (item.Youxifandian ?? 0);
                            // lsItem.ZhuChe = lsItem.ZhuChe + (item.ZhuChe ?? 0);
                            lsItem.Zhuihaofankuan = lsItem.Zhuihaofankuan + (item.Zhuihaofankuan ?? 0);
                            lsItem.Zhuihaokoukuan = lsItem.Zhuihaokoukuan + (item.Zhuihaokoukuan ?? 0);
                        }

                    }
                }

            }

            return result;

        }

        /// <summary>
        /// 盈亏列表---后台管理
        /// </summary>
        /// <param name="currenyUserId">当前用户ID  -1为总代理</param>
        /// <param name="userCode">会员号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="account">用户，精确查找</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<ProfitLossDTOMan> SelectProfitLossByManager(int currenyUserId, string userCode, DateTime startTime, DateTime endTime, int pageIndex, int pageSize, ref int totalCount)
        {
            #region 设置参数
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@currenyUserId",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@code",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@startTime",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@endTime",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output)
            };

            parameters[0].Value = currenyUserId;
            parameters[1].Value = userCode;
            parameters[2].Value = startTime;
            parameters[3].Value = endTime.AddSeconds(1);
            parameters[5].Value = pageIndex;
            parameters[6].Value = pageSize;

            #endregion
            //那个和是到这里拆分 还是页面上拆分呢？
            var list = this.ExProc<ProfitLossDTOMan>("sp_SysUserBalance_ProfitLoss_man", parameters);

            totalCount = parameters[6].Value != DBNull.Value ? Convert.ToInt32(parameters[6].Value) : 0;

            return list;
        }


        /// <summary>
        /// 盈亏列表---后台管理20170221 by add ,优化后的盈亏统计查询
        /// </summary>
        /// <param name="currenyUserId">当前用户ID  -1为总代理</param>
        /// <param name="userCode">会员号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="account">用户，精确查找</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<ProfitLossDTOMan> SelectProfitLossByManagerNew(int currenyUserId, string userCode, DateTime startTime, DateTime endTime, int pageIndex, int pageSize, ref int totalCount)
        {
            //#region 设置参数
            //DbParameter[] parameters = new DbParameter[]{
            //    new DbContextFactory().GetDbParameter("@currenyUserId",System.Data.DbType.Int32),
            //    new DbContextFactory().GetDbParameter("@code",System.Data.DbType.String),
            //    new DbContextFactory().GetDbParameter("@startTime",System.Data.DbType.DateTime),
            //    new DbContextFactory().GetDbParameter("@endTime",System.Data.DbType.DateTime),
            //    new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
            //    new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32),
            //    new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output)
            //};

            //parameters[0].Value = currenyUserId;
            //parameters[1].Value = userCode;
            //parameters[2].Value = startTime;
            //parameters[3].Value = endTime.AddSeconds(1);
            //parameters[5].Value = pageIndex;
            //parameters[6].Value = pageSize;

            //#endregion
            /**判断是否为查询当天数据，若只查询当天数据，若为当天数据，使用原有方法进行统计*/
            //获取当天日期查询的天数
            DateTime curBegin = Utils.GetNowBeginDate();
            DateTime curEnd = Utils.GetNowEndDate();
            int curDay = Convert.ToInt32(curBegin.ToString("yyyyMMdd"));
            int curEndDay = Convert.ToInt32(curEnd.ToString("yyyyMMdd"));


            //查询条件日期
            int nowDay = Convert.ToInt32(startTime.ToString("yyyyMMdd"));
            int preDay = Convert.ToInt32(endTime.ToString("yyyyMMdd"));

            List<ProfitLossDTOMan> result = new List<ProfitLossDTOMan>();

            //当天查询的开始结束日期相等，只需要查询当天的结果
            long rticks = endTime.Subtract(curBegin).Ticks;
            long sticks = startTime.Subtract(curBegin).Ticks;
            if (rticks >= 0 //查询条件结束时间大于等于当天默认结束时间
                &&
                 sticks >= 0  //开始时间大于等于当天默认开始时间
                )
            {
                result = SelectProfitLossByManager(currenyUserId, userCode, startTime, endTime, pageIndex, pageSize, ref totalCount);
            }
            else
            {
                //非当天的查询结果，需对查询条件进行拆分
                preDay = Convert.ToInt32(endTime.AddDays(-1).ToString("yyyyMMdd"));

                string newWhere = "";
                if (currenyUserId == -1)
                {
                    newWhere = " ParentId is null ";
                }
                else
                {
                    newWhere = " UserId in(select id from SysYtgUser where Id=" + currenyUserId + " or ParentId=" + currenyUserId + ") ";
                }
                if (!string.IsNullOrEmpty(userCode))
                    newWhere = " code='"+userCode+"' ";

                string sql = @"select tl.*,CONVERT(decimal,sy.Rebate) from 
(select 
UserId  as Id,UserId,Code,ParentId,PlayType,
sum([Chongzhi]) as [Chongzhi],sum([Tixian]) as [Tixian],sum([Touzhu]) as [Touzhu],sum([Zhuihaokoukuan]) as [Zhuihaokoukuan],sum([Zhuihaofankuan]) as [Zhuihaofankuan],
sum([Youxifandian]) as [Youxifandian],sum([Jiangjinpaisong]) as [Jiangjinpaisong],sum([Chedanfankuan]) as [Chedanfankuan],sum([Chedanshouxufei]) as [Chedanshouxufei],
sum([Chexiaofandian]) as [Chexiaofandian],sum([Chexiaopaijiang]) as [Chexiaopaijiang],sum([Chongzhikoufei]) as [Chongzhikoufei],sum([ShangjiChongzhi]) as [ShangjiChongzhi],
sum([Huodonglijin]) as [Huodonglijin],sum([Fenhong]) as [Fenhong],sum([Qita]) as [Qita],sum([TiXianShiBai]) as [TiXianShiBai],sum([CheXiaoTiXian]) as  [CheXiaoTiXian],sum([ManZheng]) as [ManZheng],
sum([QianDao]) as [QianDao],sum([ZhuChe]) as [ZhuChe],sum([ChongZhiActivity]) as [ChongZhiActivity],sum([YongJing]) as [YongJing],sum([XinYunDaZhuanPan]) as [XinYunDaZhuanPan],sum([Expr1]) as [Expr1]
from [dbo].[ProfitLossDTOManTasks] where " + newWhere + " and occday between " + nowDay + " and " + preDay + " group by UserId,Code,ParentId,PlayType) as tl left join SysYtgUser as sy on sy.Id=tl.id";

                result = this.GetSqlSource<ProfitLossDTOMan>(sql);

                if (rticks >= 0)
                {
                    //查询当天结果,
                    //此方法用于查询当天数据，
                    var cbd = curBegin.AddDays(-1);
                    if (preDay < curEndDay)
                        curEnd = endTime;
                    var nowResult = SelectProfitLossByManager(currenyUserId, userCode, cbd, curEnd, pageIndex, pageSize, ref totalCount);
                    //result.AddRange(nowResult);
                    foreach (var item in nowResult)
                    {
                        var lsItem = result.Where(x => x.Code == item.Code).FirstOrDefault();
                        if (lsItem == null)
                        {
                            result.Add(item);
                        }
                        else
                        {
                            lsItem.Chedanfankuan = lsItem.Chedanfankuan + (item.Chedanfankuan ?? 0);
                            lsItem.Chedanshouxufei = lsItem.Chedanshouxufei + (item.Chedanshouxufei ?? 0);
                            lsItem.Chexiaofandian = lsItem.Chexiaofandian + (item.Chexiaofandian ?? 0);
                            lsItem.Chexiaopaijiang = lsItem.Chexiaopaijiang + (item.Chexiaopaijiang ?? 0);
                            lsItem.CheXiaoTiXian = lsItem.CheXiaoTiXian + (item.CheXiaoTiXian ?? 0);
                            lsItem.Chongzhi = lsItem.Chongzhi + (item.Chongzhi ?? 0);
                            lsItem.ChongZhiActivity = lsItem.ChongZhiActivity + (item.ChongZhiActivity ?? 0);
                            lsItem.Chongzhikoufei = lsItem.Chongzhikoufei + (item.Chongzhikoufei ?? 0);
                            lsItem.Expr1 = lsItem.Expr1 + (item.Expr1 ?? 0);
                            lsItem.Fenhong = lsItem.Fenhong + (item.Fenhong ?? 0);
                            lsItem.Huodonglijin = lsItem.Huodonglijin + (item.Huodonglijin ?? 0);
                            lsItem.Jiangjinpaisong = lsItem.Jiangjinpaisong + (item.Jiangjinpaisong ?? 0);
                            lsItem.ManZheng = lsItem.ManZheng + (item.ManZheng ?? 0);
                            lsItem.PlayType = lsItem.PlayType;
                            lsItem.QianDao = lsItem.QianDao + (item.QianDao ?? 0);
                            lsItem.Qita = lsItem.Qita + (item.Qita ?? 0);
                            // lsItem.Rebate = lsItem.Rebate;
                            lsItem.ShangjiChongzhi = lsItem.ShangjiChongzhi + (item.ShangjiChongzhi ?? 0);
                            lsItem.Tixian = lsItem.Tixian + (item.Tixian ?? 0);
                            lsItem.TiXianShiBai = lsItem.TiXianShiBai + (item.TiXianShiBai ?? 0);
                            lsItem.Touzhu = lsItem.Touzhu + (item.Touzhu ?? 0);
                            lsItem.XinYunDaZhuanPan = lsItem.XinYunDaZhuanPan + (item.XinYunDaZhuanPan ?? 0);
                            lsItem.YongJing = lsItem.YongJing + (item.YongJing ?? 0);
                            lsItem.Youxifandian = lsItem.Youxifandian + (item.Youxifandian ?? 0);
                            lsItem.ZhuChe = lsItem.ZhuChe + (item.ZhuChe ?? 0);
                            lsItem.Zhuihaofankuan = lsItem.Zhuihaofankuan + (item.Zhuihaofankuan ?? 0);
                            lsItem.Zhuihaokoukuan = lsItem.Zhuihaokoukuan + (item.Zhuihaokoukuan ?? 0);
                        }

                    }
                }

            }

            return result;
        }


        /// <summary>
        /// 后台管理 总盈亏列表
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="type">1按日统计 2按月统计</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<ProfitLossList> SelectProfitLossList(DateTime startTime, DateTime endTime, int type,
            int pageIndex, int pageSize, ref int totalCount)
        {
            #region 设置参数
            DbParameter[] parameters = new DbParameter[]{
               new DbContextFactory().GetDbParameter("@startTime",System.Data.DbType.DateTime),
               new DbContextFactory().GetDbParameter("@endTime",System.Data.DbType.DateTime),
               new DbContextFactory().GetDbParameter("@type",System.Data.DbType.Int32),

               new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
               new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32),
               new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output)
            };

            parameters[0].Value = startTime;
            parameters[1].Value = endTime;
            parameters[2].Value = type;
            parameters[3].Value = pageIndex;
            parameters[4].Value = pageSize;
            #endregion
            var list = this.ExProc<BasicModel.Report.ProfitLossList>("sp_SysUserBalanceDetails_SelectProfitLoss", parameters);
            totalCount = Convert.ToInt32(parameters[5].Value ?? 0);
            return list;
        }

        /// <summary>
        /// 统计报表
        /// </summary>
        /// <param name="currenyUserId">当前用户ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="account">账号</param>
        /// <param name="startRebt">起始返点</param>
        /// <param name="endRebt">结束返点</param>
        /// <param name="isSelf">自身 or 团队   自身1  团队2</param>
        /// <param name="type">投注 1 返点 2 奖金3</param>
        /// <param name="minNum">最小金额</param>
        /// <param name="maxNum">最大金额</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<StatisticsReportDTO> SelectStatisticsReportBy(int currenyUserId, DateTime startTime, DateTime endTime,
            string account, decimal startRebt, decimal endRebt, int isSelf, int type, string minNum, string maxNum,
            int pageIndex, int pageSize, ref int totalCount)
        {
            #region 设置参数
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@currenyUserId",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@startTime",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@endTime",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@account",System.Data.DbType.String),

                new DbContextFactory().GetDbParameter("@startRebt",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@endRebt",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@isSelf",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@type",System.Data.DbType.Int32),

                new DbContextFactory().GetDbParameter("@minNum",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@maxNum",System.Data.DbType.String),

                new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output)
            };

            parameters[0].Value = currenyUserId;
            parameters[1].Value = startTime;
            parameters[2].Value = endTime.AddDays(1).AddMilliseconds(-1);
            parameters[3].Value = account;
            parameters[4].Value = startRebt;
            parameters[5].Value = endRebt;
            parameters[6].Value = isSelf;
            parameters[7].Value = type;
            parameters[8].Value = minNum;
            parameters[9].Value = maxNum;
            parameters[10].Value = pageIndex;
            parameters[11].Value = pageSize;
            #endregion
            var list = this.ExProc<StatisticsReportDTO>("sp_SysUserBalanceDetails_Statistics", parameters);
            totalCount = parameters[12].Value != DBNull.Value ? Convert.ToInt32(parameters[12].Value) : 0;
            if (list != null && list.Count > 0)
            {
                list.ForEach(m =>
                {
                    m.TuanTouzhu = (m.TuanTouzhu + m.TuanChedanfankuan + m.TuanZhuihaokoukuan + m.TuanZhuihaofankuan) - (m.Touzhu + m.Chedanfankuan + m.Zhuihaokoukuan + m.Zhuihaofankuan);
                    m.TuanYouxifandian = (m.TuanYouxifandian + m.TuanChexiaofandian) - (m.Youxifandian + m.Chexiaofandian);
                    m.TuanJiangjinpaisong = (m.TuanJiangjinpaisong + m.TuanChexiaopaijiang) - (m.Jiangjinpaisong + m.Chexiaopaijiang);
                    m.Touzhu = m.Touzhu + m.Chedanfankuan + m.Zhuihaokoukuan + m.Zhuihaofankuan;
                    m.Youxifandian = m.Youxifandian + m.Chexiaofandian;
                    m.Jiangjinpaisong = m.Jiangjinpaisong + m.Chexiaopaijiang;
                });
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="lotteryCode">彩票</param>
        /// <param name="palyRadioCode">玩法Id</param>
        /// <param name="issueCode">期数</param>
        /// <param name="model">模式：0元 1角 2分</param>
        /// <param name="serialNo">账单编号</param>
        /// <param name="userAccount">用户</param>
        /// <param name="tradeType">交易类型</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<BasicModel.Report.PrizeList> SelectPrizeListBy(DateTime startTime, DateTime endTime, string lotteryCode,
            int palyRadioCode, string issueCode, int model, string serialNo, string userAccount,
            TradeType tradeType, int pageIndex, int pageSize, ref int totalCount)
        {
            #region 设置参数
            DbParameter[] parameters = new DbParameter[]{
               new DbContextFactory().GetDbParameter("@startTime",System.Data.DbType.DateTime),
               new DbContextFactory().GetDbParameter("@endTime",System.Data.DbType.DateTime),
              
               new DbContextFactory().GetDbParameter("@lotteryCode",System.Data.DbType.String),
               new DbContextFactory().GetDbParameter("@palyRadioCode",System.Data.DbType.Int32),

               new DbContextFactory().GetDbParameter("@issueCode",System.Data.DbType.String),
               new DbContextFactory().GetDbParameter("@model",System.Data.DbType.Int32),
               new DbContextFactory().GetDbParameter("@serialNo",System.Data.DbType.String),
               new DbContextFactory().GetDbParameter("@userAccount",System.Data.DbType.String),

               new DbContextFactory().GetDbParameter("@tradeType",System.Data.DbType.Int32),
               new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
               new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32),
               new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output)
            };

            parameters[0].Value = startTime;
            parameters[1].Value = endTime;
            parameters[2].Value = lotteryCode;
            parameters[3].Value = palyRadioCode;

            parameters[4].Value = issueCode;
            parameters[5].Value = model;
            parameters[6].Value = serialNo;
            parameters[7].Value = userAccount;
            parameters[8].Value = Convert.ToInt32(tradeType);
            parameters[9].Value = pageIndex;
            parameters[10].Value = pageSize;
            #endregion
            var list = this.ExProc<BasicModel.Report.PrizeList>("sp_SysUserBalanceDetails_Prize", parameters);
            totalCount = Convert.ToInt32(parameters[11].Value);
            return list;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="tradeType">交易类型</param>
        /// <param name="type">1按日统计 2按月统计</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount">总条数</param>
        /// <param name="totalAmt">总金额</param>
        /// <returns></returns>
        public List<BasicModel.Report.BalanceDetailsStatistical> SelectStatisticalListBy(DateTime startTime,
            DateTime endTime, TradeType tradeType, int type, int pageIndex, int pageSize, ref int totalCount,
            ref decimal totalAmt)
        {
            #region 设置参数
            DbParameter[] parameters = new DbParameter[]{
               new DbContextFactory().GetDbParameter("@startTime",System.Data.DbType.DateTime),
               new DbContextFactory().GetDbParameter("@endTime",System.Data.DbType.DateTime),
               new DbContextFactory().GetDbParameter("@tradeType",System.Data.DbType.Int32),
               new DbContextFactory().GetDbParameter("@type",System.Data.DbType.Int32),

               new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
               new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32),
               new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output),
                new DbContextFactory().GetDbParameter("@totalAmt",System.Data.DbType.Decimal,System.Data.ParameterDirection.Output)
            };

            parameters[0].Value = startTime;
            parameters[1].Value = endTime;
            parameters[2].Value = Convert.ToInt32(tradeType);
            parameters[3].Value = type;
            parameters[4].Value = pageIndex;
            parameters[5].Value = pageSize;
            #endregion
            var list = this.ExProc<BasicModel.Report.BalanceDetailsStatistical>("sp_SysUserBalanceDetails_Statistical", parameters);
            totalCount = Convert.ToInt32(parameters[6].Value ?? 0);
            totalAmt = Convert.ToDecimal(parameters[7].Value ?? 0.0M);
            return list;
        }

        //public bool DeleteById(int id)
        //{
        //    var context = new YtgDbContext();
        //    var item = context.SysUserBalanceDetails.SingleOrDefault(m => m.Id == id);
        //    context.SysUserBalanceDetails.Remove(item);
        //    if (this.mSysUserBalanceService.ChangeUserBalance(item.UserId, item.TradeAmt))
        //    {
        //        context.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}


        /// <summary>
        /// 撤消派奖
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UndoAward(int id)
        {
            var item = this.Get(id);
            if (item != null)
            {
                item.TradeType = TradeType.撤销派奖;
                //还需要把用户余额给退回来
                //if (this.mSysUserBalanceService.ChangeUserBalance(item.UserId, item.TradeAmt))
                //{
                //    this.Save();
                //    return true;
                //}
            }
            return false;
        }

        public bool Chedan(string relevanceNo)
        {
            string sql = "select * from SysUserBalanceDetails where RelevanceNo='" + relevanceNo + "'";
            var items = this.GetSqlSource<SysUserBalanceDetail>(sql);
            if (items != null && items.Count > 0)
            {
                var context = new YtgDbContext();
                foreach (var item in items)//对这个编号下所有的数据都进行相应的操作
                {
                    //if (this.mSysUserBalanceService.ChangeUserBalance(item.UserId, item.TradeAmt))
                    //{
                    //    var detail = context.SysUserBalanceDetails.SingleOrDefault(m => m.Id == item.Id);
                    //    context.SysUserBalanceDetails.Remove(detail);
                    //    context.SaveChanges();
                    //}
                }
                return true;
            }
            return false;
        }

        public List<RechargeRecodVM> SelectRechargeRecod(string code, string serialNo, string sdate, string edate, int pageIndex, ref int totalCount)
        {
            var sql = string.Format(@"WITH result AS(
	SELECT u.Id,yu.Code,u.SerialNo,u.OccDate,b.BankName,IsNull(r.TradeAmt,0) RechargeAmt,Isnull((r.TradeAmt-u.TradeAmt),0) Poundage,u.TradeAmt,(case when u.Status=1 then '失败' else '成功' end) StatusDes
	FROM dbo.SysUserBalanceDetails u
	LEFT JOIN dbo.RecordTemp r ON r.Id=u.BankId and r.UserId=u.userId
	LEFT JOIN dbo.SysBankTypes b ON b.Id=r.BankId
	LEFT JOIN dbo.SysYtgUser yu ON yu.Id=u.UserId
	WHERE u.TradeType=1 AND ('{0}' = '' or yu.Code LIKE '%{0}%') AND ('{1}' ='' or u.SerialNo LIKE '%{1}%')
    and ('{2}'='' or u.Occdate >='{2}') and ('{3}'='' or u.Occdate<'{3}')
),totalCount AS(
	SELECT COUNT(1) TotalCount FROM result
)
select * from (
	select *, ROW_NUMBER() OVER(Order by a.Id desc) AS RowNumber from result as a,totalCount
) as b
where RowNumber BETWEEN ({4}-1)*{5}+1 AND {4}*{5}", code, serialNo, sdate, edate, pageIndex, AppGlobal.ManagerDataPageSize);

            var list = this.GetSqlSource<RechargeRecodVM>(sql);
            totalCount = list.Count > 0 ? list.First().TotalCount : 0;
            return list;
        }

        public List<MentionRecodVM> SelectMentionRecod(string code, string serialNo, string sdate, string edate,int type, int pageIndex, ref int totalCount)
        {
            //            var sql = string.Format(@"WITH result AS(
            //	SELECT u.Id,yu.Code,u.SerialNo,u.OccDate,m.SendTime,b.BankName,
            //		p.ProvinceName,c.CityName,ub.Branch,ub.BankOwner,ub.BankNo,IsNull(m.MentionAmt,0) MentionAmt,IsNull(m.Poundage,0) Poundage,ABS(u.TradeAmt) TradeAmt,
            //		(case m.Status WHEN 2 then '提现失败' WHEN 1 THEN '提现成功'  WHEN 0 THEN '排队中' else '用户撤销' end) StatusDes,u.RelevanceNo
            //	FROM dbo.SysUserBalanceDetails u
            //	LEFT JOIN dbo.MentionQueus m ON m.MentionCode=u.RelevanceNo
            //	LEFT JOIN dbo.SysUserBanks ub ON ub.id=m.UserBankId AND ub.UserId=m.UserId
            //	LEFT JOIN dbo.S_Province p ON p.ProvinceID=ub.ProvinceId
            //	LEFT JOIN dbo.S_City c ON c.CityID=ub.CityId
            //	LEFT JOIN dbo.SysBankTypes b ON b.Id=ub.BankId
            //	LEFT JOIN dbo.SysYtgUser yu ON yu.Id=u.UserId
            //	WHERE (u.TradeType=2 OR u.TradeType=16 OR u.TradeType=17) AND ('{0}' = '' or yu.Code LIKE '%{0}%') AND ('{1}' ='' or u.SerialNo LIKE '%{1}%')
            //    and ('{2}'='' or u.Occdate >='{2}') and ('{3}'='' or u.Occdate<'{3}')
            //),totalCount AS(
            //	SELECT COUNT(1) TotalCount FROM result
            //)
            //select * from (
            //	select *, ROW_NUMBER() OVER(Order by a.Id desc) AS RowNumber from result as a,totalCount
            //) as b
            //where RowNumber BETWEEN ({4}-1)*{5}+1 AND {4}*{5}", code, serialNo, sdate, edate, pageIndex, AppGlobal.ManagerDataPageSize);

            //            var list = this.GetSqlSource<MentionRecodVM>(sql);
            //            totalCount = list.Count > 0 ? list.First().TotalCount : 0;
            //            return list;
            string sql = string.Format(@"SELECT u.Id,yu.Code,u.SerialNo,u.OccDate,m.SendTime,b.BankName,
		p.ProvinceName,c.CityName,ub.Branch,ub.BankOwner,ub.BankNo,IsNull(m.MentionAmt,0) MentionAmt,IsNull(m.Poundage,0) Poundage,ABS(u.TradeAmt) TradeAmt,
		(case m.Status WHEN 2 then '提现失败' WHEN 1 THEN '提现成功'  WHEN 0 THEN '排队中' else '用户撤销' end) StatusDes,u.RelevanceNo
	FROM dbo.SysUserBalanceDetails u
	LEFT JOIN dbo.MentionQueus m ON m.MentionCode=u.RelevanceNo
	LEFT JOIN dbo.SysUserBanks ub ON ub.id=m.UserBankId AND ub.UserId=m.UserId
	LEFT JOIN dbo.S_Province p ON p.ProvinceID=ub.ProvinceId
	LEFT JOIN dbo.S_City c ON c.CityID=ub.CityId
	LEFT JOIN dbo.SysBankTypes b ON b.Id=ub.BankId
	LEFT JOIN dbo.SysYtgUser yu ON yu.Id=u.UserId
	WHERE (u.TradeType=2 OR u.TradeType=16 OR u.TradeType=17) AND ('{0}' = '' or yu.Code LIKE '%{0}%') AND ('{1}' ='' or u.SerialNo LIKE '%{1}%')
    and ('{2}'='' or u.Occdate >='{2}') and ('{3}'='' or u.Occdate<'{3}') and ({4}=-1 or m.Status={4})", code, serialNo, sdate, edate, type);

            sql = "(" + sql + ") as t1";
            int iPageCount = 0;
            return GetEntitysPage<MentionRecodVM>(sql,"OccDate","*","OccDate Desc",ESortType.DESC,pageIndex,AppGlobal.ManagerDataPageSize,ref iPageCount,ref totalCount);
        }

        public List<SettlementReportVM> SelectSettlementReport(string code, DateTime sDate, DateTime eDate, bool isSettlementReport, bool isMonth, int pageIndex, ref int totalCount)
        {
            #region 设置参数
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@code",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@sDate",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@eDate",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@isSettlementReport",System.Data.DbType.Int32),

                new DbContextFactory().GetDbParameter("@isMonth",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32)
            };

            parameters[0].Value = code;
            parameters[1].Value = sDate;
            parameters[2].Value = eDate.AddDays(1).AddMilliseconds(-1);
            parameters[3].Value = isSettlementReport ? 0 : 1;
            parameters[4].Value = isMonth ? 1 : 0;
            parameters[5].Value = pageIndex;
            parameters[6].Value = AppGlobal.ManagerDataPageSize;
            #endregion
            var list = this.ExProc<SettlementReportVM>("sp_SysUserBalanceDetails_SettlementReport", parameters);
            if (list.Count > 1) totalCount = list.Last().TotalCount;
            if (list != null && list.Count > 1)
            {
                list.ForEach(m =>
                {
                    //这个总盈亏是相对于平台而言的
                    //总盈亏=投注总额-返点总额-中奖,-表示赚 +表示亏
                    m.YingKui = m.Touzhu + m.Fandian + m.Zhongjiang;
                });
            }
            return list;
        }

        ///// <summary>
        ///// 获取当未处理前提现,充值人数
        ///// </summary>
        ///// <param name="withdrawPeopleNumber">当前提现人数</param>
        ///// <param name="rechargePeopleNumber">当前充值人数</param>
        //public WithdrawRechargePersonNumberDTO GetWithdrawRechargePersonNumber()
        //{
        //    string sqlStr = "select (select count(1) from MentionQueus  where status=0) as WithdrawPeopleNumber,ISNULL(SUM(case when TradeType=1 then 1 else 0 end),0) as RechargePersonNumber "
        //        + "from SysUserBalanceDetails  where TradeType=1 or TradeType=2 and Status=0";

        //    return this.GetSqlSource<WithdrawRechargePersonNumberDTO>(sqlStr).FirstOrDefault();
        //}
         /// <summary>
        /// 获取当未处理前提现,充值人数
        /// </summary>
        /// <param name="withdrawPeopleNumber">当前提现人数</param>
        /// <param name="rechargePeopleNumber">当前充值人数</param>
        /// /// <param name="rechargePeopleNumber">当前充值人数</param>
        public WithdrawRechargePersonNumberDTO GetWithdrawRechargePersonNumber()
        {
            string procName = "sp_managerNothf";
            var result= this.ExProc<WithdrawRechargePersonNumberDTO>(procName, null);
            if (null == result)
                return null;
            return result.FirstOrDefault();
        }

        /// <summary>
        /// 获取所有下级用户的消费金额，当天领取前一天的
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<YongJinDTO> GetChildrensByMonery(int uid)
        {
            DateTime beginDate;
            DateTime endDate;
            //var hour = DateTime.Now.Hour;
            //if (hour < 3 && hour >= 0)
            //{
            //    beginDate = Convert.ToDateTime(DateTime.Now.AddDays(-2).ToString("yyyy/MM/dd") + " 03:00:00");
            //}
            //beginDate = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " 03:00:00");
            //endDate = beginDate.AddDays(1); //Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " 00:03:00");

            beginDate = Utils.GetNowBeginDate().AddDays(-1);
            endDate = beginDate.AddDays(1);
            string sql =string.Format(@"with my1 as 
	        (
	           select * from SysYtgUser where id = {0} 
	           union all select SysYtgUser.* from my1,SysYtgUser where my1.id = SysYtgUser.ParentId
	        )
	        select my1.id,ParentId,SUM(TradeAmt) as TradeAmt from my1 
	        inner join SysUserBalanceDetails as bd on my1.Id=bd.UserId
	        where TradeType in(3,4,8,9,5) and bd.OccDate between '{1}' and '{2}'
			group by my1.id,ParentId", uid,beginDate,endDate);

            return this.GetSqlSource<YongJinDTO>(sql);
        }

        /// <summary>
        /// 系统充值记录
        /// </summary>
        /// <param name="nikeName"></param>
        /// <param name="code"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="state"></param>
        /// <param name="begindec"></param>
        /// <param name="enddec"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<Ytg.BasicModel.Manager.SysUserBalanceDetailRechange> FilterUserBalanceDetails(string sernum, string code, DateTime? beginDate, DateTime? endDate, int state, decimal? begindec, decimal? enddec,int tradeType, int pageIndex, int pageSize, ref int totalCount)
        {
            string sql = "select su.Code,bd.* from SysUserBalanceDetails as bd inner join SysYtgUser as su on bd.UserId=su.Id where 1=1";
            if (!string.IsNullOrEmpty(sernum))
            {
                sql += " and bd.SerialNo = '" + sernum + "'";
            }
            if (!string.IsNullOrEmpty(code))
                sql += " and su.Code='" + code + "'";
            if (beginDate != null && endDate != null)
                sql += " and bd.OccDate between '" + beginDate + "' and '" + endDate + "'";
            if (state != -1)
                sql += " and bd.Status=" + state;
            if (begindec != null && enddec != null)
                sql += " and bd.TradeAmt between " + begindec + " and " + enddec;
            if (tradeType == -1)
                sql += " and tradeType in(24,15)";
            else
                sql += " and tradeType =" + tradeType;
            
            sql = "(" + sql + ") as t1";
            int iPageCount = 0;
            return this.GetEntitysPage<Ytg.BasicModel.Manager.SysUserBalanceDetailRechange>(sql, "OccDate", "*", "OccDate", ESortType.DESC, pageIndex, pageSize, ref iPageCount, ref totalCount);
        }

       
      
    }
}
