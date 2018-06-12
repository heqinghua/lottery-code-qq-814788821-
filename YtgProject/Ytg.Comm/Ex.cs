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
    /// 扩展类
    /// </summary>
    public static class Ex
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 使用json net序列化
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Json(this IEnumerable source)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(source);
        }

        /// <summary>
        /// 将baseentity 转换为json对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string Json(this BaseEntity entity)
        {
            if (null == entity)
                return string.Empty;
            return Newtonsoft.Json.JsonConvert.SerializeObject(entity);
        }
    }
}
