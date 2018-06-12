using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 游戏记录/盈亏列表
    /// </summary>
    public class ProfitLossDTO
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户帐号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 返点
        /// </summary>
        public decimal Rebate { get; set; }

        /// <summary>
        /// 充值
        /// </summary>
        public decimal? Chongzhi { get; set; }

        /// <summary>
        /// 提现
        /// </summary>
        public decimal? Tixian { get; set; }

        /// <summary>
        /// 投注
        /// </summary>
        public decimal? Touzhu { get; set; }

        /// <summary>
        /// 追号扣款
        /// </summary>
        public decimal? Zhuihaokoukuan { get; set; }

        /// <summary>
        /// 追号返款
        /// </summary>
        public decimal? Zhuihaofankuan { get; set; }

        /// <summary>
        /// 游戏返点
        /// </summary>
        public decimal? Youxifandian { get; set; }

        /// <summary>
        /// 奖金派送
        /// </summary>
        public decimal? Jiangjinpaisong { get; set; }

        /// <summary>
        /// 撤单返款
        /// </summary>
        public decimal? Chedanfankuan { get; set; }

        /// <summary>
        /// 撤单手续费
        /// </summary>
        public decimal? Chedanshouxufei { get; set; }

        /// <summary>
        /// 撤销返点
        /// </summary>
        public decimal? Chexiaofandian { get; set; }

        /// <summary>
        /// 撤销派奖
        /// </summary>
        public decimal? Chexiaopaijiang { get; set; }

        /// <summary>
        /// 充值扣费
        /// </summary>
        public decimal? Chongzhikoufei { get; set; }

        /// <summary>
        /// 上级充值
        /// </summary>
        public decimal? ShangjiChongzhi { get; set; }

        /// <summary>
        /// 活动礼金
        /// </summary>
        public decimal? Huodonglijin { get; set; }

        /// <summary>
        /// 分红
        /// </summary>
        public decimal? Fenhong { get; set; }

        /// <summary>
        /// 其他金额
        /// </summary>
        public decimal? Qita { get; set; }

        /// <summary>
        /// 用户表示自身盈亏
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 0表示自身 1表示团队
        /// </summary>
        public int IsTotal { get; set; }

        public decimal? TotalJiangjin { get; set; }

        public decimal? TotalYingkui { get; set; }

        public List<ProfitLossDTO> SelfProfitLoss { get; set; }
    }
}
