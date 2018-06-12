using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Comm;
using Ytg.Core.Service.Lott;
using Ytg.Data;
using Ytg.Service.Lott;

namespace Ytg.Scheduler.Comm
{
    public class DataRecoveryInfo
    {
        public int Id { get; set; }
    }

    public class LotteryIssuesData
    {
        private ILotteryIssueService mLotteryIssueService;

        private IBuyTogetherService mBuyTogetherService;


        static List<LotteryType> mLotterys = null;

        //分分彩  包括五分11选5、三分11选5 埃及分分彩
        List<int> fenfid = "12,17,18,13,24".Split(',').Select(x => Convert.ToInt32(x.ToString())).ToList();
        string autoFen = "13,15,24";//官方API开奖 1分和2分彩
                                    //非当天期号
        List<int> notId = new List<int>(); //"7,9".Split(',').Select(c => Convert.ToInt32(c.ToString())).ToList();

        string notIdStr = "";
        public LotteryIssuesData()
        {
            notId.AddRange(fenfid);
            foreach (var ni in notId)
            {
                notIdStr += ni + ",";
            }
            notIdStr = notIdStr.Substring(0, notIdStr.Length - 1);
            var db = new DbContextFactory();
            this.mLotteryIssueService = new LotteryIssueService(new Repo<LotteryIssue>(db));
            this.mBuyTogetherService = new BuyTogetherService(new Repo<BuyTogether>(db));

        }

        public Ytg.BasicModel.DTO.Server_IssueResultDTO GetIssueResultDTO(int lotteryid)
        {
            return this.mLotteryIssueService.Server_TopIssueResult(lotteryid);
        }


        /// <summary>
        /// 修改合买内容
        /// </summary>
        /// <param name="bettid"></param>
        /// <param name="state"></param>
        public void UpdateBuyTogether(int bettid, int state)
        {
            this.mBuyTogetherService.UpdateBuyTogetherState(bettid, state);
        }

       /// <summary>
       /// 获取当天所有开奖数据 不包括福彩3d 排列3、五
       /// </summary>
       /// <returns></returns>
        public IEnumerable<LotteryIssue> GetNowDayIssue()
       {
           var result = this.mLotteryIssueService.GetNowDayIssue().ToList();
           var x = result.Where(c=>c.LotteryId==17);
           return result.Where(c => !notId.Contains(c.LotteryId.Value));
       }

       /// <summary>
       /// 获取当前待开奖期数  int type = groupName == "openresult_task" ? 0 : 1;//0为普通 1 为一分彩盒2两分
       /// </summary> 
       /// <returns></returns>
       public List<int> GetNowOpenIssue(int type = 0)
       {
           if (type == 0)
               return this.mLotteryIssueService.GetNotOpenIssues(notIdStr);
           else
               return this.mLotteryIssueService.GetInIssues(autoFen);
       }

       /// <summary>
       /// 保存期数状态
       /// </summary>
       /// <returns></returns>
       public bool SaveChanges()
       {
           this.mLotteryIssueService.Save();
           return true;
       }

       /// <summary>
       /// 获取福彩3d 排列35期号
       /// </summary>
       /// <returns></returns>
       public IEnumerable<LotteryIssue> GetAllIssue()
       {
           return this.mLotteryIssueService.GetAll().Where(c => notId.Contains(c.LotteryId.Value) && c.StartTime>=DateTime.Now);
       }

       /// <summary>
       /// 获取分分彩期号
       /// </summary>
       /// <returns></returns>
       public IEnumerable<LotteryIssue> GetFenFenCaiIssue()
       {
           var result = this.mLotteryIssueService.GetNowDayIssue().ToList();
           return result.Where(c => fenfid.Contains(c.LotteryId.Value));
       }

       /// <summary>
       /// 清除数据
       /// </summary>
       public void DataRecovery()
       {
           mLotteryIssueService.ExProc<DataRecoveryInfo>("sp_DataRecovery", null);
       }

       #region 更新开奖数据
       /// <summary>
       /// 更新数据
       /// </summary>
       /// <param name="entity"></param>
       public bool UpdateResult(OpenResultEntity newItem,string lotteryType)
       {
           try
           {
               //"sp_UpdateOpenResult"

               var lotteryId = LotteryIssuesData.GetAllLotterys().Where(item => item.LotteryCode == lotteryType).FirstOrDefault().Id;

               return UpdateResult(newItem,lotteryId);
           }
           catch (Exception ex)
           {
               LogManager.Error(string.Format("保存开奖结果异常，期号{0}  类型{1}", newItem.expect, lotteryType), ex);
           }
           return false;
       }

       /// <summary>
       /// 更新数据
       /// </summary>
       /// <param name="entity"></param>
       public bool UpdateResult(OpenResultEntity newItem, int lotteryid)
       {
           try
           {

               //var dataItem = this.mLotteryIssueService.Where(item => item.IssueCode == newItem.expect && item.LotteryId == lotteryid).FirstOrDefault();
               //if (null == dataItem)
               //    return false;
               //dataItem.Result = newItem.opencode;
               //dataItem.LotteryTime = newItem.opentime;
               //this.mLotteryIssueService.Save();
                return this.mLotteryIssueService.UpdateOpenResult(newItem.expect, newItem.opencode, newItem.opentime, lotteryid);
           }
           catch (Exception ex)
           {
               LogManager.Error(string.Format("保存开奖结果异常，期号{0}  类型{1}", newItem.expect, lotteryid), ex);
           }
           return false;
       }
       #endregion


       /// <summary>
       /// 生成期数
       /// </summary>
       /// <param name="item"></param>
       /// <returns></returns>
       public void AddLotteryIssueCode(LotteryIssue item)
       {
           //var fs = this.mLotteryIssueService.GetAll().Where(i => i.IssueCode == item.IssueCode && i.LotteryId==item.LotteryId).FirstOrDefault();
           //if (fs == null)
           //{
           //    this.mLotteryIssueService.Create(item);
           //    this.mLotteryIssueService.Save();
           //}
           this.mLotteryIssueService.AddLotteryIssueCode(item);
       }

       /// <summary>
       /// 调用存储过程，生成后一天期号数据 (过期，不使用该方法)
       /// </summary>
       public void BuilderIssue()
       {
           this.mLotteryIssueService.BuilderNextDayIssues();
       }

       /// <summary>
       /// 获取所有玩法类别
       /// </summary>
       /// <returns></returns>
       public static List<LotteryType> GetAllLotterys()
       {
           if (mLotterys == null)
           {
               var lotteryService = new LotteryTypeService(new Repo<LotteryType>(new DbContextFactory()));
               mLotterys = lotteryService.GetAll().ToList();
           }

           return mLotterys;
       }

       /// <summary>
       /// 修改状态
       /// </summary>
       /// <param name="detail"></param>
       /// <returns></returns>
       public static bool UpdateOpenState(BetDetail detail)
       {
           string sql = "update BetDetails set IsMatch='" + detail.IsMatch + "',WinMoney=" + detail.WinMoney + ",Stauts=" + ((int)detail.Stauts) + ",OpenResult='" + detail.OpenResult + "' where id=" + detail.Id + "";
           //return this.mRepo.GetDbContext.Database.ExecuteSqlCommand(sql, detail.IsMatch, detail.WinMoney, detail.Stauts, detail.OpenResult, detail.Id);
           return SqlHelper.ExecteNonQuery(System.Data.CommandType.Text, sql, null) > 0;
       }

       /// <summary>
       /// 创建中奖消息
       /// </summary>
       /// <returns></returns>
       public static bool CreateMessage(Message message)
       {
           string sql = "INSERT INTO [dbo].[Messages]([FormUserId],[ToUserId],[MessageType],[MessageContent],[Status],[IsDelete],[OccDate],[Title])VALUES(" + message.FormUserId + "," + message.ToUserId + "," + message.MessageType + "," +
           "'" + message.MessageContent + "','" + message.Status + "','" + message.IsDelete + "','" + message.OccDate + "','" + message.Title + "')";
           return SqlHelper.ExecteNonQuery(System.Data.CommandType.Text, sql)>0;
       }


        /// <summary>
        /// 创建中奖消息
        /// </summary>
        /// <returns></returns>
        public static bool UpdateBuyTogerher(int bettid,int state,decimal winmonery)
        {
            string sql = "update BuyTogethers set Stauts="+ state + ",WinMonery="+ winmonery + " where BetDetailId=" + bettid;
            return SqlHelper.ExecteNonQuery(System.Data.CommandType.Text, sql) > 0;
        }

        /// <summary>
        /// 获取未满员的投注订单
        /// </summary>
        /// <returns></returns>
        public static DataTable GetNotGroupByState()
        {
            string procName = "sp_autoGroupByData";
            var set= SqlHelper.ExecuteDataSet(CommandType.StoredProcedure, procName, null);
            if (set.Tables.Count > 0)
                return set.Tables[0];
            return null;
        }


        /// <summary>
        /// 获取测试账号用户id集合
        /// </summary>
        /// <returns></returns>
        public static List<int> GetCatchUserLst()
        {
            string sql = "select id from SysYtgUser  where code like '%heladiv%' and IsDelete=0";
            DataTable tab = SqlHelper.ExecuteDataSet(CommandType.Text, sql, null).Tables[0];
            if (null == tab || tab.Rows.Count < 1)
            {
                return new List<int>();
            }
            List<int> res = new List<int>();
            foreach (DataRow row in tab.Rows)
            {
                res.Add(Convert.ToInt32(row["id"]));
            }
            return res;
        }

        /// <summary>
        /// 获取下一期数据
        /// </summary>
        /// <param name="lotteryid"></param>
        /// <returns></returns>
        public static string  GetNowIssue(int lotteryid) {
            string sql = "select * from LotteryIssues where LotteryId="+ lotteryid + " and Result is null and EndTime  between GETDATE()  and DATEADD(mi, 4, GETDATE())";
            DataTable tab = SqlHelper.ExecuteDataSet(CommandType.Text, sql, null).Tables[0];
            if (tab.Rows.Count < 1)
                return "";
            return tab.Rows[0]["IssueCode"].ToString();
        }

        public static int AddTogether(string SerialNo, int TradeType, BuyTogether together)
        {
            string spName = "sp_addoUpdateBuyTogether";

            SqlParameter[] pramers = new SqlParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@BetDetailId",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@UserId",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@Subscription",SqlDbType.Decimal),
                    new System.Data.SqlClient.SqlParameter("@BuyTogetherCode",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@SerialNo",SqlDbType.VarChar),
                    new System.Data.SqlClient.SqlParameter("@TradeType",SqlDbType.Int),
                    new System.Data.SqlClient.SqlParameter("@state",SqlDbType.Int),
            };
            pramers[0].Value = together.BetDetailId;
            pramers[1].Value = together.UserId;
            pramers[2].Value = together.Subscription;
            pramers[3].Value = together.BuyTogetherCode;
            pramers[4].Value = SerialNo;
            pramers[5].Value = TradeType;
            pramers[6].Direction = ParameterDirection.Output;

            SqlHelper.ExecteNonQuery(CommandType.StoredProcedure,spName, pramers);
            
            object parenter = pramers[6].Value;
            int state = 0;
            if (parenter != null)
            {
                state = Convert.ToInt32(parenter);
            }
            else
            {
                state = -1;
            }

            return state;
        }


    }
}
