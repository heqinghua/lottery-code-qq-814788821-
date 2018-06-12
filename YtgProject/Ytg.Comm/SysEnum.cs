using System;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Ytg.Comm
{
    /// <summary>
    /// 系统错误编码
    /// </summary>
    public enum ApiCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 未知错误 失败
        /// </summary>
        Fail = 1,
        /// <summary>
        /// 编号001代表 参数错误
        /// </summary>
        ParamEmpty = 1001,

        /// <summary>
        /// 验证失败
        /// </summary>
        ValidationFails = 1002,

        /// <summary>
        /// 账号禁用
        /// </summary>
        DisabledCode = 1003,
    }

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 普通会员
        /// </summary>
        General = 0,

        /// <summary>
        /// 代理用户
        /// </summary>
        Proxy = 1,

        /// <summary>
        /// 管理用户
        /// </summary>
        Manager = 2
    }


}
