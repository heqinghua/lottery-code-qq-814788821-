using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Comm
{
    /// <summary>
    /// api 结果实体
    /// </summary>
    public class ApiResult
    {
        
        /// <summary>
        /// 操作状态编码
        /// </summary>
        public ApiCode Code { get; set;}

        /// <summary>
        /// 结果集
        /// </summary>
        public IEnumerable rows { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; set; }


        /// <summary>
        /// 输出json
        /// </summary>
        /// <returns></returns>
        public JObject ToJObject()
        {
            var jsn = new JObject();
            jsn["Code"] = (int)Code;
            if(null!=rows && rows!="")
              jsn["rows"] =JArray.FromObject(rows);

            jsn["total"] = total;
            return jsn;
        }

        /// <summary>
        /// 返回状态为成功
        /// </summary>
        /// <returns></returns>
        public static ApiResult SuccessResult()
        {
            return CreateResult("", ApiCode.Success);
        }

        /// <summary>
        /// 返回状态为成功的结果集
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static ApiResult SuccessResult(IEnumerable data)
        {
            return CreateResult(data, ApiCode.Success);
        }

        /// <summary>
        /// 返回状态为成功的结果集
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static ApiResult SuccessResult(IEnumerable data, int total)
        {
            return CreateResult(data, ApiCode.Success, total);
        }
        
        /// <summary>
        /// 失败
        /// </summary>
        /// <returns></returns>
        public static ApiResult FailResult(IEnumerable data)
        {
           return CreateResult(data, ApiCode.Fail);
            
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResult FailResult() {
            return CreateResult(null, ApiCode.Fail);
        }
       
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ApiResult FailResult(ApiCode code)
        {
            return CreateResult(null, code);
        }

        /// <summary>
        /// 构建result
        /// </summary>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static ApiResult CreateResult(IEnumerable data, ApiCode code, int total = 0)
        {
            return new ApiResult()
            {
                Code = code,
                total = total,
                rows=data
            };
        }
    }
}
