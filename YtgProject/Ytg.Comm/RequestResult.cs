using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Comm
{
    /// <summary>
    /// 输出结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class RequestResult<T>
    {
        public RequestResult()
        {
            this.ResponseTime = DateTime.Now;
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public ApiCode Code { get; set; }

        /// <summary>
        /// 结果集
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 异常消息
        /// </summary>
        public string ErrMsg { get;set;}

        /// <summary>
        /// 当前响应时间
        /// </summary>
        public DateTime ResponseTime { get; set; }
    }
}
