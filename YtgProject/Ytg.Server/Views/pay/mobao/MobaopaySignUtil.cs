using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace com.mobaopay.merchant
{
    /// <summary>
    /// 类名： MobaopaySignUtil
    /// 功能： 工具类，提供签名和验证签名的方法
    /// 
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，
    /// 按照技术文档编写,并非一定要使用该代码。
    /// </summary>
    public sealed class MobaopaySignUtil
    {
        private static readonly MobaopaySignUtil instance = new MobaopaySignUtil();

        public static MobaopaySignUtil Instance
        {
            get { return instance; }
        }

        private MobaopaySignUtil()
        { }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="sourceData">签名源数据</param>
        /// <returns></returns>
        public string sign(string sourceData)
        {
            MD5 md5 = MD5.Create();
            byte[] data = Encoding.UTF8.GetBytes(sourceData + MobaopayConfig.Mbp_key);
            byte[] result = md5.ComputeHash(data);

            return GetbyteToString(result);
        }

        private static string GetbyteToString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="signData">签名数据</param>
        /// <param name="srcData">源数据</param>
        /// <returns></returns>
        public bool verifyData(string signData, string srcData)
        {
            string newSignData = sign(srcData);
            // 忽略字母的大小写
            return newSignData.ToUpper().Equals(signData.ToUpper());
        }
    }
}
