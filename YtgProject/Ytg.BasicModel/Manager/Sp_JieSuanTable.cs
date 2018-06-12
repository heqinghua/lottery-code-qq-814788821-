using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.Manager
{
    /// <summary>
    /// 天结算报表 月结算报表
    /// </summary>
    public class Sp_JieSuanTable
    {
        private bool mIsRef = true;
        public Sp_JieSuanTable(bool isref=true) {
            this.mIsRef = isref;
        }

        private decimal mYingKui;
        /// <summary>
        /// 盈亏
        /// </summary>
        public decimal? YingKui
        {
            get { return mYingKui; }
            set
            {
                if (!this.mIsRef)
                {
                    this.mYingKui = value.Value;
                }
                else
                {
                    if (value == null)
                        value = 0;
                    if (value.Value > 0)
                        mYingKui = 0 - value.Value;
                    else
                        mYingKui = Math.Abs(value.Value);
                }
            }
        }

        /// <summary>
        /// 充值
        /// </summary>
        public decimal? Chongzhi { get; set; }

        /// <summary>
        /// 活动
        /// </summary>
        public decimal? Activity { get; set; }

        private decimal? mtiXian = 0;
        /// <summary>
        /// 提现
        /// </summary>
        public decimal? tiXian
        {
            get { return mtiXian; }
            set
            {
                if (!this.mIsRef)
                {
                    this.mtiXian = value.Value;
                }
                else
                {
                    if (null != value)
                        mtiXian = Math.Abs(value.Value);
                }
            }
        }


        private decimal? mTouZhu=0;
        /// <summary>
        /// 投注
        /// </summary>
        public decimal? TouZhu
        {
            get { return mTouZhu; }
            set
            {
                if (!this.mIsRef)
                {
                    this.mTouZhu = value.Value;
                }
                else
                {
                    if (null != value)
                        mTouZhu = Math.Abs(value.Value);
                }
            }
        }

        /// <summary>
        /// 返点
        /// </summary>
        public decimal? Youxifandian { get; set; }

        /// <summary>
        /// 奖金
        /// </summary>
        public decimal? Jiangjinpaisong { get; set; }

        /// <summary>
        /// 分红
        /// </summary>
        public decimal? FenHong { get; set; }


        /// <summary>
        /// 签到活动
        /// </summary>
        public decimal? QianDao { get; set; }
        /// <summary>
        /// 注册活动
        /// </summary>
        public decimal? ZhuChe { get; set; }
        /// <summary>
        /// 佣金
        /// </summary>
        public decimal? YongJing { get; set; }
    }
}
