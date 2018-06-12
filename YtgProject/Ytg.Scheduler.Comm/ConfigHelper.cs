using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm
{
   public class ConfigHelper
    {
       //api 地址模板
       static string ApiLotteryUrl = System.Configuration.ConfigurationManager.AppSettings["Api.Lottery.Url"] + "&code={0}";

       //api 数据行数
       static int APiLotteryRow =Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["APi.Lottery.Row"]);
       //api 数据格式
       static string APiLotteryFormat = System.Configuration.ConfigurationManager.AppSettings["APi.Lottery.Format"];
       //&rows=1&format=json

       //如未取到指定数据，轮询次数
       static int ApiLotteryRound =Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Api.Lottery.Round"]);

       //循环拉取数据，休眠间隔
       static int ApiLotterySleep = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Api.Lottery.Sleep"]);

       //生成期数时间 重庆时时彩
       static string ApiLotteryBuildIssue = System.Configuration.ConfigurationManager.AppSettings["Api.Lottery.BuildIssue"];

       //生成期数时间 江西时时彩
       static string ApiLotteryJXSSCBuildIssue = System.Configuration.ConfigurationManager.AppSettings["Api.Lottery.BuildIssueJxssc"];

       //开奖时间推出
       static int IssueEndTimeAppend = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IssueEndTimeAppend"]);
      

       //五分彩、十分彩封单时间
       static int EndSaleMinutes_wufenc = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["EndSaleMinutes_wufenc"]);
       

       //生成期数时间 上海时时乐
       static string ApiLotteryShangHaiShiShilBuildIssue = System.Configuration.ConfigurationManager.AppSettings["Api.Lottery.BuildIssueShangHaissl"];

       //北京快乐8期号前缀
       public static string Bjkl8IssueCode = System.Configuration.ConfigurationManager.AppSettings["20150421"];

       //福彩3d停止购买日期
       public static string Fc3d_StopDay = System.Configuration.ConfigurationManager.AppSettings["fc3d_StopDay"];

       //福彩3d开奖时间
       public static string Fc3d_openTime = System.Configuration.ConfigurationManager.AppSettings["fc3d_openTime"];


       //排列3、5停止购买日期
       public static string Pl_StopDay = System.Configuration.ConfigurationManager.AppSettings["Pl_StopDay"];

       //排列3、5开奖时间
       public static string Pl_openTime = System.Configuration.ConfigurationManager.AppSettings["Pl_openTime"];

       /// <summary>
       /// 分分彩最低利润
       /// </summary>
       private static string mProfitMargin = System.Configuration.ConfigurationManager.AppSettings["ProfitMargin"];

       /// <summary>
       /// 自动清除数据
       /// </summary>
       private static string mDatarecovery = System.Configuration.ConfigurationManager.AppSettings["datarecovery"];

       /// <summary>
       /// 香港六合彩倍数
       /// </summary>
       private static string Liuhecaibat = System.Configuration.ConfigurationManager.AppSettings["liuhecaibat"];

       //开奖轮询时间间隔，
       static string mOpenResultwhileOpenResult = System.Configuration.ConfigurationManager.AppSettings["whileOpenResult"];



       //开奖轮询时间间隔， 分分彩和两分彩票
       static string mOpenFenFenResultwhileOpenResult = System.Configuration.ConfigurationManager.AppSettings["whileOpenResult_FenFen"];

        /// <summary>
        /// 机器人投注提交参数
        /// </summary>
        static string robmitUrl = System.Configuration.ConfigurationManager.AppSettings["robmitUrl"];

        /// <summary>
        /// 机器人投注提交参数
        /// </summary>
        public static string RombmitUrl {
            get {return robmitUrl;}
        }

        
       /// <summary>
       /// 香港六合彩倍数
       /// </summary>
       public static int GetLiuhecaibat
       {
           get { return Convert.ToInt32(Liuhecaibat); }
       }

       /// <summary>
       /// 分分彩最低利润
       /// </summary>
       public static decimal ProfitMargin
       {
           get
           {
               return Convert.ToDecimal(mProfitMargin);
           }
       }

       /// <summary>
       /// 如未取到指定数据，轮询次数
       /// </summary>
       public static int GetApiLotteryRound
       {
           get { return ApiLotteryRound; }
       }

       /// <summary>
       /// 获取轮询休眠间隔
       /// </summary>
       public static int GetApiLotterySleep
       {
           get { return ApiLotterySleep; }
       }

       /// <summary>
       /// 获取生成下一天期数时间
       /// </summary>
       public static string GetApiLotteryBuildIssue
       {
           get
           {
               return ApiLotteryBuildIssue;
           }
       }

       /// <summary>
       /// 江西时时彩期数生成时间
       /// </summary>
       public static String GetApiLotteryJXSSCBuildIssue{
           get
           {
               return ApiLotteryJXSSCBuildIssue;
           }
       }
       /// <summary>
       /// 上海时时乐
       /// </summary>
       public static String GetApiLotteryShangHaiShiShilBuildIssue
       {
           get
           {
               return ApiLotteryShangHaiShiShilBuildIssue;
           }
       }

       /// <summary>
       /// 自动清除数据任务
       /// </summary>
       public static string GetDataRecovery
       {
           get { return mDatarecovery; }
       }

       /// <summary>
       /// 获取用户轮询开奖任务
       /// </summary>
       public static string OpenResultwhileOpenResult
       {

           get
           {
               return mOpenResultwhileOpenResult;
           }
       }

       /// <summary>
       /// 分分彩轮询开奖任务
       /// </summary>
       public static string OpenFenFenResultwhileOpenResult
       {
           get
           {

               return mOpenFenFenResultwhileOpenResult;
           }
       }

       private static Dictionary<int, string> mLotteryDictionary = null;
       public static Dictionary<int, string> GetLotteryDictionary()
       {
           if (mLotteryDictionary == null)
           {
               string result = System.Configuration.ConfigurationManager.AppSettings["lotteryConfig"];
               if (!string.IsNullOrEmpty(result))
               {
                   mLotteryDictionary = new Dictionary<int, string>();
                   var array = result.Split('|');
                   foreach (var a in array)
                   {
                       if (string.IsNullOrEmpty(a))
                           continue;
                       var lt = a.Split(',');
                       if (!mLotteryDictionary.ContainsKey(Convert.ToInt32(lt[0])))
                           mLotteryDictionary.Add(Convert.ToInt32(lt[0]), lt[1]);
                   }
               }

           }
           return mLotteryDictionary;
       }
       
       /// <summary>
       /// 构建彩票api地址
       /// </summary>
       /// <param name="type"></param>
       /// <returns></returns>
       public static string BuilderApiUrl(string type)
       {
           string apiUrl= string.Format(ApiLotteryUrl, type) + string.Format("&rows={0}&format={1}", APiLotteryRow, APiLotteryFormat);
           return apiUrl;
           //http://c.apiplus.cn/newly.do?token=a82956657c244b74&code=gd11x5&rows=1&format=json
       }

       /// <summary>
       /// 构建彩票api地址
       /// </summary>
       /// <param name="type"></param>
       /// <returns></returns>
       public static string BuilderApiUrl(int lotteryid)
       {
           string outLotteryType="";
           if (GetLotteryDictionary().TryGetValue(lotteryid, out outLotteryType))
           {
               return BuilderApiUrl(outLotteryType);
           }
           return string.Empty;
           
       }

        public static string GetLotteryName(string lotteryid)
        {
            if (string.IsNullOrEmpty(lotteryid))
                return string.Empty;
            int outId = 0;
            if (!int.TryParse(lotteryid, out outId))
                return "";
            //<add key="lotteryConfig" value="1,重庆时时彩|2,江西时时彩|3,黑龙江时时彩|4,新疆时时彩|5,天津时时彩|6,广东11选5|7,福彩3d|8,上海时时乐|9,排列35|10,北京快乐8|19,山东11选5|20,江西11选5|21,香港六合彩|22,江苏快3|18,五分11选5|13,埃及分分彩|14,河内时时彩|23,印尼时时彩|24,埃及二分彩|25,埃及五分彩|15,韩国1.5分彩|26,北京PK10"/>
            string outLotteryType = "";
            if (GetLotteryDictionary().TryGetValue(outId, out outLotteryType))
            {
                return outLotteryType;
            }
            return string.Empty;
        }

       /// <summary>
       /// 结束时间推迟秒
       /// </summary>
       public static int GetIssueEndTimeAppend
       {
           get
           {
               return IssueEndTimeAppend;
           }
       }

       /// <summary>
       /// 截止销售时间
       /// </summary>
       public static int GetEndSaleMinutes_wufencs
       {
           get
           {
               return EndSaleMinutes_wufenc;
           }
       }

       
    }
}
