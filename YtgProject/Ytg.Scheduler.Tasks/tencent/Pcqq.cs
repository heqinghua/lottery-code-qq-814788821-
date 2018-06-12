
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ytg.Comm;
using Ytg.Scheduler.Comm;
using Ytg.Scheduler.Comm.Bets;

namespace Ytg.Scheduler.Tasks.tencent
{
    public class Pcqq
    {

        static readonly string tenctRequestUrl = System.Configuration.ConfigurationManager.AppSettings["tenct"];//分分彩请求地址

        private static int c1 = 0;
        private static int c2 = 0;

        static readonly LotteryIssuesData mLotteryIssuesData;

        static readonly BetDetailsCalculate mBetDetailsCalculate;

        static Pcqq()
        {
            mLotteryIssuesData = new LotteryIssuesData();
            mBetDetailsCalculate = new BetDetailsCalculate();
        }

        /// <summary>
        /// http Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string HttpPostRequest(string url)
        {
            string rString = string.Empty;
            StreamReader sr = null;
            HttpWebResponse res = null;
            try
            {
                Encoding myEncoding = Encoding.GetEncoding("gb2312");
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "GET";
                req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:46.0) Gecko/20100101 Firefox/46.0";

                res = (HttpWebResponse)req.GetResponse();
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
                    rString = sr.ReadToEnd().ToString();
                }
            }
            catch(Exception ex)
            {
                LogManager.Error("",ex);
            }
            finally
            {
                res.Close();
                sr.Close();
            }
            return rString;
        }

        public static void Run() {
            Thread thread = new Thread(Task);
            thread.Start();
        }

        private static void Task()
        {
            string s;
            while (true)
            {
                try
                {
                    s = DateTime.Now.ToString("HHmmss");
                    String hh = s.Substring(0, 2);
                    String mm = s.Substring(2, 2);
                    String ss = s.Substring(4, 2);
                    int c = 0;
                    c = Convert.ToInt32(mm);
                    int d = Convert.ToInt32(hh);
                    if (ss == "00")
                    {
                        if (Convert.ToInt32(ss) > 30)
                        {
                            c = c + 1;
                        }
                        int result = pcqqOnline(c, d);
                        while (result == 0)
                        {//当c1与c2的差为0时，其实是采集到了相同结果，因此循环直至采集到不同的结果为止，不做中断每秒会执行8次左右
                            result = pcqqOnline(c, d);
                        }

                        LogManager.Info("程序时间:" + DateTime.Now.ToString("hh:mm:ss"));
                    }
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    LogManager.Error("程序时间:" + DateTime.Now.ToString("hh:mm:ss"),ex);
                }
            }
        }

        private static int pcqqOnline(int c, int d)
        {
            int cha = 0;
            try
            {
                int code = 200;

                if (code == 200)
                {
                    //{"c":267762809,"ec":0,"h":281650131}
                    String result = HttpPostRequest(tenctRequestUrl);
                    if (string.IsNullOrEmpty(result))
                        return 0;
                    result = result.Substring(12);
                    result = result.Substring(0, result.Length - 1);
                    var jobj = JObject.Parse(result);
                    var rows = jobj.Property("c");
                    if (rows == null)
                        return 0;

                    int curr = Convert.ToInt32(rows.Value.ToString());
                    int l = curr;
                    int[] a = new int[9];
                    int i = 0;
                    int sum = 0;
                    if (c % 2 == 0)
                    {
                        c2 = l;
                    }
                    else
                    {
                        c1 = l;
                    }
                    if (c % 2 == 0)
                    {
                        cha = c2 - c1;
                    }
                    else
                    {
                        cha = c1 - c2;
                    }
                    while (true)
                    {
                        a[i] = l % 10;
                        sum = sum + a[i];
                        i++;
                        l = l / 10;
                        if (l / 10 == 0)
                        {
                            a[i] = l;
                            sum = sum + a[i];
                            break;
                        }
                    }
                    //System.out.println(Arrays.toString(a));

                    string s = curr.ToString();
                    string qs = DateTime.Now.ToString("yyyyMMdd");
                    int min = d * 60 + c;
                    String qs1 = "";
                    if (min < 100)
                    {
                        qs1 = "00" + min;
                    }
                    else if (min >= 100 && min < 1000)
                    {
                        qs1 = "0" + min;
                    }
                    else
                    {
                        qs1 = min + "";
                    }
                    String jieguo = sum % 10 + "," + a[3] + "," + a[2] + "," + a[1] + "," + a[0];
                    LogManager.Info("开奖结果:" + jieguo);
                    string dateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    LogManager.Info("期数:" + qs + "-" + qs1 + " " + DateTime.Now + " " + s + " " + (cha >= 0 ? "+" + cha : cha.ToString()));
                    if (cha != 0)
                    {
                        Online o = new Online();
                        o.issue = (qs + qs1);
                        o.time = (dateTime);
                        o.total = (s);
                        o.change = (cha >= 0 ? "+" + cha : cha + "");
                        o.result = (jieguo);
                        o.wan = (sum % 10);
                        o.qian = (a[3]);
                        o.bai = (a[2]);
                        o.shi = (a[1]);
                        o.ge = (a[0]);
                        //MysqlTool.insertPcqq(o);
                        string key = qs + qs1;//期数
                        //TenctOpenResult.CreateInstan().Put(key, o);
                        int whileCount = 0;
                        while (true)
                        {
                            try
                            {
                                whileCount++;
                                if (whileCount > 5)
                                    break;
                                IntoSql(o);
                                break;
                            }
                            catch (Exception instar)
                            {
                                LogManager.Error("腾讯分分彩运算错误---" + o.ToString(), instar);
                            }
                            System.Threading.Thread.Sleep(5);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                LogManager.Error("",e);
                return 0;
            }
            return cha;
        }

        private static void IntoSql(Online o)
        {
            string lotteryType = "tenct";
            int lotteryid = 11;
            bool isCompled = mLotteryIssuesData.UpdateResult(new OpenResultEntity()
            {
                expect = o.issue,
                opencode = o.result,
                opentime=DateTime.Now

            }, lotteryid);//保存开奖结果

            if (isCompled)
            {
                //取消延迟开奖
                mBetDetailsCalculate.Calculate(lotteryType, o.issue, o.result);//计算投注明细中奖金额
                LogManager.Info(string.Format("计算开奖成功： type={0},lotteryCode={1}", lotteryType, lotteryType));
            }
        }
    }
}
