using System;
using System.Net;
using System.Windows;


namespace Ytg.BasicModel.LotteryBasic.DTO
{
    /// <summary>
    /// 追号期数
    /// </summary>
    public class CatchDto
    {
        /// <summary>
        /// 期数编号
        /// </summary>
        public string IssueCode { get; set; }

        /// <summary>
        /// 倍数
        /// </summary>
        public int Multiple { get; set; }

        /// <summary>
        /// 追号所需要金额
        /// </summary>
        public double Monery { get; set; }
    }
}
