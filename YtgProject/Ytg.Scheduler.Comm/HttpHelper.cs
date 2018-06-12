using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm
{
    public class HttpHelper
    {
        //请求超时时间 10秒
        private int mTimeout = 1000 * 10;

        /// <summary>
        /// 请求超时时间
        /// </summary>
        public int TimeOut { get { return mTimeout; } set { mTimeout = value; } }

        /// <summary>
        /// get 请求
        /// </summary>
        /// <param name="url"></param>
        public List<OpenResultEntity> DoGet(string url)
        {
            try
            {
                var httpRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpRequest.ContentType = "application/json";
                httpRequest.ContentType = "application/x-www-from-urlencoded";
                httpRequest.Method = "GET";
                httpRequest.Timeout = mTimeout;

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                StringBuilder sb = null;
                List<OpenResultEntity> result = null;
                //成功
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        sb = new StringBuilder();
                        sb.Append(reader.ReadToEnd());
                        result= Analysis(sb.ToString());
                    }

                }
                
                return result;

            }
            catch (Exception ex)
            {
                LogManager.Error(string.Format("url={0}  Exception={1}",url, ex.Message));
            }

            return null;
        }

        private List<OpenResultEntity> Analysis(string result)
        {
            //{"rows":"5","info":"免费接口随机延迟1-8分钟。购买或试用付费接口请访问api.opencai.net或加QQ:9564384(注明彩票API)","code":"cqssc","data":[{"expect":"20141124118","opencode":"1,6,2,0,7","opentime":"2014-11-24 23:50:55","opentimestamp":"1416844255113"},{"expect":"20141124117","opencode":"1,6,0,7,1","opentime":"2014-11-24 23:45:59","opentimestamp":"1416843959020"},{"expect":"20141124116","opencode":"6,9,9,7,1","opentime":"2014-11-24 23:41:03","opentimestamp":"1416843663083"},{"expect":"20141124115","opencode":"2,9,9,3,0","opentime":"2014-11-24 23:35:58","opentimestamp":"1416843358957"},{"expect":"20141124114","opencode":"1,9,2,3,8","opentime":"2014-11-24 23:31:02","opentimestamp":"1416843062927"}]}
            var jobj = JObject.Parse(result);
            var rows = jobj.Property("rows");
            if (rows == null)
                return null;

            var data = jobj.Property("data").Value.ToString();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<OpenResultEntity>>(data);
        }
    }


    public class OpenResultEntity
    {
        /// <summary>
        /// 期数
        /// </summary>
        public string expect { get; set; }//expect":"20141124116","opencode":"6,9,9,7,1","opentime":"2014-11-24 23:41:03","opentimestamp":"1416843663083"

        /// <summary>
        /// 开奖结果
        /// </summary>
        public string opencode { get; set; }

        /// <summary>
        /// 开奖时间
        /// </summary>
        public DateTime opentime { get; set; }

    }
}
