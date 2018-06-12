using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 游戏记录/盈亏列表
    /// </summary>
    public class ProfitLossDTOMan
    {
        public ProfitLossDTOMan() {
            this.Rebate = 0;
            this.Chongzhi = 0;
            this.Tixian = 0;
            this.Touzhu = 0;
            this.Zhuihaokoukuan = 0;
            this.Zhuihaofankuan = 0;
            this.Youxifandian = 0;
            this.Jiangjinpaisong = 0;
            this.Chedanfankuan = 0;
            this.Chedanshouxufei = 0;
            this.Chexiaofandian = 0;
            this.Chexiaopaijiang = 0;
            this.Chongzhikoufei = 0;
            this.ShangjiChongzhi = 0;
            this.Huodonglijin = 0;
            this.Fenhong = 0;
            this.Qita = 0;
            this.TiXianShiBai = 0;
            this.CheXiaoTiXian = 0;
            this.ManZheng = 0;
            this.QianDao = 0;
            this.ZhuChe = 0;
            this.ChongZhiActivity = 0;
            this.YongJing = 0;
            this.XinYunDaZhuanPan = 0;
            this.Expr1 = 0;
        }

      

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
        public decimal? Rebate { get; set; }

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
        /// 提现失败
        /// </summary>
        public decimal? TiXianShiBai { get; set; }


        /// <summary>
        /// 撤销提现
        /// </summary>
        public decimal? CheXiaoTiXian { get; set; }

        /// <summary>
        /// 满赠
        /// </summary>
        public decimal? ManZheng { get; set; }


         
        /// <summary>
        /// 签到
        /// </summary>
        public decimal? QianDao { get; set; }

        /// <summary>
        /// 注册
        /// </summary>
        public decimal? ZhuChe { get; set; }

        /// <summary>
        /// 充值活动
        /// </summary>
        public decimal? ChongZhiActivity { get; set; }

        /// <summary>
        /// 佣金
        /// </summary>
        public decimal? YongJing { get; set; }

        /// <summary>
        /// 大转盘
        /// </summary>
        public decimal? XinYunDaZhuanPan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Expr1 { get; set; }


        /// <summary>
        /// 用户玩法类型  0 为1800 1 为 1700 玩法
        /// </summary>
        public int PlayType { get; set; }


    }
}
