using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 统计报表
    /// </summary>
    public class StatisticsReportDTO
    {
        #region Old

        ///// <summary>
        ///// 用户Id
        ///// </summary>
        //public int Id { get; set; }

        ///// <summary>
        ///// 账号
        ///// </summary>
        //public string Code { get; set; }

        ///// <summary>
        ///// 返点
        ///// </summary>
        //public decimal Rebate { get; set; }

        ///// <summary>
        ///// 自身投注
        ///// </summary>
        //public decimal Touzhu { get; set; }

        ///// <summary>
        ///// 团队投注（含自身）
        ///// </summary>
        //public decimal TuanTouzhu { get; set; }

        ///// <summary>
        ///// 自身返点
        ///// </summary>
        //public decimal Fandian { get; set; }

        ///// <summary>
        ///// 团队返点（含自身）
        ///// </summary>
        //public decimal TuanFandian { get; set; }

        ///// <summary>
        ///// 自身奖金
        ///// </summary>
        //public decimal Jiangjinpaisong { get; set; }

        ///// <summary>
        ///// 团队奖金（含自身）
        ///// </summary>
        //public decimal TuanZhongjiang { get; set; }

        #endregion

        #region new
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 返点
        /// </summary>
        public decimal Rebate { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        //public int ParentId { get; set; }
        public decimal TuanChongzhi { get; set; }
        public decimal TuanTixian { get; set; }
        public decimal TuanTouzhu { get; set; }

        public decimal TuanZhuihaokoukuan { get; set; }
        public decimal TuanZhuihaofankuan { get; set; }
        public decimal TuanYouxifandian { get; set; }

        public decimal TuanJiangjinpaisong { get; set; }
        public decimal TuanChedanfankuan { get; set; }
        public decimal TuanChedanshouxufei { get; set; }

        public decimal TuanChexiaofandian { get; set; }
        public decimal TuanChexiaopaijiang { get; set; }
        public decimal TuanChongzhikoufei { get; set; }

        public decimal TuanShangjiChongzhi { get; set; }
        public decimal TuanHuodonglijin { get; set; }
        public decimal TuanQita { get; set; }

        public decimal Chongzhi { get; set; }
        public decimal Tixian { get; set; }
        public decimal Touzhu { get; set; }

        public decimal Zhuihaokoukuan { get; set; }
        public decimal Zhuihaofankuan { get; set; }
        public decimal Youxifandian { get; set; }

        public decimal Jiangjinpaisong { get; set; }
        public decimal Chedanfankuan { get; set; }
        public decimal Chedanshouxufei { get; set; }

        public decimal Chexiaofandian { get; set; }
        public decimal Chexiaopaijiang { get; set; }
        public decimal Chongzhikoufei { get; set; }

        public decimal ShangjiChongzhi { get; set; }
        public decimal Huodonglijin { get; set; }
        public decimal Qita { get; set; }

        #endregion
    }
}
