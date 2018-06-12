using System;
using System.Net;
using System.Windows;

namespace Ytg.BasicModel.LotteryBasic.DTO
{
    public class LotteryIssueDTO
    {
        /// <summary>
        /// 期数
        /// </summary>
        public string IssueCode { get; set; }

        /// <summary>
        ///结束时间（期数）
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 结束销售时间 在什么时间不允许在购买
        /// </summary>
        public DateTime EndSaleTime { get; set; }

        /// <summary>
        /// 开奖结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 类型 1为历史开奖数据  0为未开奖数据
        /// </summary>
        public int tp { get; set; }

    }
}
