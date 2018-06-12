using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ytg.Scheduler.Comm;

namespace Ytg.Scheduler.Tasks.AutoSubmit
{
    /// <summary>
    /// 自动提交程序
    /// </summary>
    public class AutoSubmitBetting
    {
        static List<SubmitConfig> siteConfigs = null;//config
        static Random rdm=new Random();//随机数对象
        static string[] sscCheckNumber = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };//ssc num
        static string[] x5CheckNumber = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11" };//11x5 num
        static Random userRdm = new Random();
        static int subCount = 2;//提交单数

        public static float builderBili = 0.5f;//随机填充百分比

        static AutoSubmitBetting()
        {
            siteConfigs = SubmitConfig.Load();
        }

        public static void Run(string lotterycode) {
            new System.Threading.Thread(new  System.Threading.ParameterizedThreadStart(Submit))
                .Start(lotterycode);
        }
        private static void Submit(object type)
        {
            string lotterycode = type.ToString();
            Console.WriteLine("lotterycode:"+ lotterycode);
            try
            {
                if (Ytg.Scheduler.Tasks.AutoGroupBy.Run.testUseridLst == null || Ytg.Scheduler.Tasks.AutoGroupBy.Run.testUseridLst.Count < 1)
                    return;
                SubmitConfig config = siteConfigs.Where(x => x.lotteryCode.ToLower() == lotterycode.ToLower()).FirstOrDefault();
                if (config == null)
                    return;
                string issuecode = LotteryIssuesData.GetNowIssue(config.LotteryId);
                int[] userArray = new int[Ytg.Scheduler.Tasks.AutoGroupBy.Run.testUseridLst.Count];
                Ytg.Scheduler.Tasks.AutoGroupBy.Run.testUseridLst.CopyTo(userArray);

                int index = 0;
                while (index < subCount)
                {

                    int userid = userArray[userRdm.Next(0, userArray.Length)];
                    LogManager.Info("自动发起订单：用户id=" + userid + " lotterycode：" + lotterycode + " issuecode：" + issuecode);
                    if (string.IsNullOrEmpty(issuecode))
                        return;

                    NameValueCollection pars = new NameValueCollection();
                    pars.Add("pmode", "0");//模式 0为元
                    pars.Add("lt_issue_start", issuecode);//购买期数
                    pars.Add("lt_trace_if", "no");//是否追号
                    pars.Add("lt_trace_stop", "no");//是否中奖后自动停止
                    pars.Add("lt_trace_issues[]", "");//追号期数
                    pars.Add("lotterycode", lotterycode);//彩种id
                    pars.Add("loginUserId", userid.ToString());//彩种id
                    pars.Add("operate2", "1");//合买 1
                    pars.Add("createrBuyPieces", "0");//最低购买金额若未合买，最低购买金额,默认10%
                    pars.Add("hidgame_tzallmon", "0");//注单总金额
                    pars.Add("baomi_hidden", "0");//保密状态为公开
                    pars.Add("action", "htdbetdetail");//action
                    pars.Add("f_N_robmt", "robmit");


                    pars.Add("lotteryid", config.LotteryId.ToString());//彩种id

                    var parContent = RandomLotteryContent(config);
                    pars.Add("lt_project[]", Newtonsoft.Json.JsonConvert.SerializeObject(parContent));//投注内容

                    string postResult = HttpPostRequest(Comm.ConfigHelper.RombmitUrl, pars);
                    LogManager.Info("post result ：" + postResult);

                    index++;
                }
            }
            catch (Exception e)
            {
                LogManager.Error("AutoSubmitBetting.Submit", e);
            }
        }

        /// <summary>
        /// http Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string HttpPostRequest(string url, NameValueCollection parameters)
        {
            string rString = string.Empty;
            StreamReader sr = null;
            HttpWebResponse res = null;
            try
            {
                Encoding myEncoding = Encoding.GetEncoding("gb2312");
                string param = RequestString(parameters, myEncoding);
                byte[] postBytes = Encoding.Default.GetBytes(param);
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded,charset=gb2312";
                req.ContentLength = postBytes.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(postBytes, 0, postBytes.Length);
                    reqStream.Close();
                }
                res = (HttpWebResponse)req.GetResponse();
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
                    rString = sr.ReadToEnd().ToString();
                }
            }
            catch
            { }
            finally
            {
                res.Close();
                sr.Close();
            }
            return rString;
        }

        /// <summary>
        /// URL参数处理方法
        /// </summary>
        /// <param name="parameters">URL参数</param>
        /// <param name="myEncoding">编码名称</param>
        /// <returns></returns>
        private static string RequestString(NameValueCollection parameters, Encoding myEncoding)
        {
            var param = "";
            foreach (string key in parameters.Keys)
            {
                param += string.Format("{0}={1}&", HttpUtility.UrlEncode(key, myEncoding), HttpUtility.UrlEncode(parameters[key], myEncoding));
            }
            if (param.Length > decimal.Zero)
            {
                param = param.Substring(0, param.Length - 1);
            }
            return param;
        }

        /// <summary>
        /// 随机生成投注内容
        /// </summary>
        /// <param name="siteConfig"></param>
        /// <returns></returns>
        private static BasicModel.DTO.HtmlParamDto RandomLotteryContent(SubmitConfig siteConfig)
        {
            var radioSize = siteConfig.radios.Count;
            var radioItem = siteConfig.radios[rdm.Next(0, radioSize)];
            //"codes": "0&1&2&3&4|5&6&7&8&9|0&1&2&3&4",
            var defCodes = radioItem.codes;
            var array = defCodes.Split('|');
            var itemArray = array[0].Split('&');
            var is11x5 = itemArray[0].Length == 2;//11x5

            var _11x5length = Convert.ToInt32(x5CheckNumber.Length * builderBili);//比例
            var ssclength = Convert.ToInt32(sscCheckNumber.Length * builderBili);//

            string[] contentArray = is11x5 ? x5CheckNumber : sscCheckNumber;
            var whileLength = is11x5 ? _11x5length : ssclength;

            Random numRandom = new Random();
            string newCodes = "";
            for (var i = 0; i < array.Length; i++)
            {
                string itemStr = "";
                for (var x = 0; x < whileLength; x++)
                {
                    var nextVal = numRandom.Next(0, whileLength);
                    if (itemStr.Contains(nextVal.ToString()))
                    {
                        x--;
                        continue;
                    }
                    if (x == whileLength - 1)
                        itemStr += nextVal;
                    else
                        itemStr += nextVal + "&";
                }
                if (i == array.Length - 1)
                    newCodes += itemStr;
                else
                    newCodes += itemStr + "|";
            }

            return new BasicModel.DTO.HtmlParamDto()
            {
                codes = newCodes,
                methodid = radioItem.methodid,
                mode = radioItem.mode,
                money = radioItem.money,
                nums = radioItem.nums,
                omodel = radioItem.omodel,
                poschoose = radioItem.poschoose,
                times = radioItem.times,
                type = radioItem.type
            };
        }

       
    }

}

