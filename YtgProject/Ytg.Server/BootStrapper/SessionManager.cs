using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ytg.ServerWeb.BootStrapper
{
    /// <summary>
    /// 用户登录session 管理类
    /// </summary>
    public static class SessionManager__
    {
        static object lockObj = new object();
        static string pth = System.Web.HttpContext.Current.Server.MapPath("~/session/") + "\\session.txt";
        private static Dictionary<int, Ytg.BasicModel.YtgSession> sessionList = new Dictionary<int, BasicModel.YtgSession>();


        /// <summary>
        /// 添加或修改session
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="session"></param>
        public static void AddOrUpdateSession(int userid, Ytg.BasicModel.YtgSession session)
        {

            lock (lockObj)
            {
                if (sessionList.ContainsKey(userid))
                {
                    sessionList[userid] = session;
                }
                else
                {
                    sessionList.Add(userid, session);
                }
            }
        }



        /// <summary>
        /// 根据用户id和sessionid验证是否能对应，否则t出
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        public static bool Has(int userid, string sessionid)
        {
            if (!sessionList.ContainsKey(userid))
                return false;
            return sessionList[userid].SessionId == sessionid;
        }


        /// <summary>
        /// 保存session信息
        /// </summary>
        public static void SaveSession()
        {
            var sessionJson = Newtonsoft.Json.JsonConvert.SerializeObject(sessionList);
            using (var stream = System.IO.File.Open(pth, System.IO.FileMode.Create))
            {
                byte[] bts = System.Text.ASCIIEncoding.Default.GetBytes(sessionJson);
                stream.Write(bts, 0, bts.Length);
            }
        }

        public static void LoadSession()
        {
            if (!System.IO.File.Exists(pth))
                return;
            using (var stream = System.IO.File.OpenText(pth))
            {
                string alltext = stream.ReadToEnd();
                if (!string.IsNullOrEmpty(alltext))
                {
                    try
                    {
                        sessionList = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, Ytg.BasicModel.YtgSession>>(alltext);
                    }
                    catch {

                    }
                }
            }

        }
    }
}