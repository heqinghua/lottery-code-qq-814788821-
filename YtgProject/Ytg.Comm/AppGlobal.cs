using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
namespace Ytg.Comm
{
    /// <summary>
    /// 系统全局对象
    /// </summary>
    public static class AppGlobal
    {
        /// <summary>
        /// 菜单操作字典
        /// </summary>
        public const string MenuAction = "MenuAction";

        /// <summary>
        /// 管理模块默认页大小
        /// </summary>
        public const int ManagerDataPageSize = 20;

        #region 输出
        
        /// <summary>
        /// 输出字节数组
        /// </summary>
        /// <param name="byteArray"></param>
        public static void RenderByteArray(byte[] byteArray)
        {
            System.Web.HttpContext.Current.Response.ContentType = "image/png";
            System.Web.HttpContext.Current.Response.BinaryWrite(byteArray);
        }

        /// <summary>
        /// 输出结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="page"></param>
        public static void RenderResult(ApiCode code, string errmsg)
        {
            RenderResult<object>(code, null, errmsg, 0);
        }


        /// <summary>
        /// 输出结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="page"></param>
        public static void RenderResult<T>(T data, int page)
        {
            RenderResult<T>(ApiCode.Success, data, "", page);
        }

        /// <summary>
        /// 输出结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="data"></param>
        /// <param name="page"></param>
        public static void RenderResult<T>(ApiCode code, T data, int page)
        {
            RenderResult<T>(code, data, "", page);
        }

        /// <summary>
        /// 输出结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="data"></param>
        /// <param name="errmsg"></param>
        public static void RenderResult<T>(ApiCode code, T data,string errmsg)
        {
            RenderResult<T>(code, data, errmsg,0);
        }

        /// <summary>
        /// 输出结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="data"></param>
        public static void RenderResult<T>(ApiCode code,T data)
        {
            RenderResult<T>(code, data, string.Empty,0);
        }

        /// <summary>
        /// 输出结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        public static void RenderResult(ApiCode code)
        {
            RenderResult<object>(code,null,string.Empty,0);
        }

        /// <summary>
        /// 输出结果集
        /// </summary>
        /// <param name="byteArray"></param>
        public static void RenderResult<T>(ApiCode code, T data, string errorMsg, int page = 0,int total=0)
        {
            RequestResult<T> result = new RequestResult<T>();
            result.Code = code;
            result.Data = data;
            result.ErrMsg = errorMsg;
            result.Page = page;
            result.Total = total;

            string jsonString = Utils.ToJson(result);

            System.Web.HttpContext.Current.Response.ContentType ="text/html";// "application/json";
            System.Web.HttpContext.Current.Response.Write(jsonString);
        }
        #endregion

    }
}
