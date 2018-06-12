using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.Act;
using Ytg.BasicModel.LotteryBasic;

namespace Ytg.Data
{
    /// <summary>
    /// 彩票周期
    /// </summary>
    public partial class YtgDbContext
    {
        /// <summary>
        /// 彩票
        /// </summary>
        public DbSet<Lottery> Lottery { get; set; }

        /// <summary>
        /// 彩票周期数据
        /// </summary>
        public DbSet<LotteryCycle> LotteryCycle { get; set; }

        /// <summary>
        /// 彩票期数
        /// </summary>
        public DbSet<LotteryIssue> LotteryIssue { get; set; }

        /// <summary>
        /// lottery group 
        /// </summary>
        public DbSet<GroupNameType> GroupNameTypes { get; set; }

        /// <summary>
        /// 彩票类型
        /// </summary>
        public DbSet<LotteryType> LotteryType { get; set; }

        /// <summary>
        /// 玩法
        /// </summary>
        public DbSet<PlayType> PlayType { get; set; }

        /// <summary>
        /// 玩法类型编号
        /// </summary>
        public DbSet<PlayTypeNum> PlayTypeNum { get; set; }

        /// <summary>
        /// 玩法单选
        /// </summary>
        public DbSet<PlayTypeRadio> PlayTypeRadio { get; set; }

        /// <summary>
        /// 投注详细
        /// </summary>
        public DbSet<BetDetail> BetDetails { get; set; }


        /// <summary>
        /// 玩法单选奖金表
        /// </summary>
        public DbSet<PlayTypeRadiosBonus> PlayTypeRadiosBonus { get; set; }


        ///// <summary>
        ///// 充值赠送金额列表
        ///// </summary>
        //public DbSet<SendHister> SendHisters { get; set; }

        #region 追号
        /// <summary>
        /// 追号表
        /// </summary>
        public DbSet<CatchNum> CatchNums { get; set; }

        /// <summary>
        /// 追号详情表（期数表）
        /// </summary>
        public DbSet<CatchNumIssue> CatchNumIssues { get; set; }
        #endregion

        #region 活动

        public DbSet<Activity> Activitys { get; set; }

        /// <summary>
        /// 签到
        /// </summary>
        public DbSet<Sign> Signs { get; set; }

        /// <summary>
        /// 佣金
        /// </summary>
        public DbSet<Commission> Commissions { get; set; }

        /// <summary>
        /// 大转盘
        /// </summary>
        public DbSet<LargeRotary> LargeRotarys { get; set; }

        /// <summary>
        /// 系统消息表
        /// </summary>
        //public DbSet<SysMessages> SysMessagess { get; set; }

        /// <summary>
        /// 数据库备份表
        /// </summary>
        public DbSet<DataBaseBak> DataBaseBaks { get; set; }

        /// <summary>
        /// 锁定IP列表
        /// </summary>
        public DbSet<LockIpInfo> LockIpInfos { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public DbSet<SiteDns> SiteDns { get; set; }
        #endregion

        #region 优化盈亏报表  2017/01/02 
        public DbSet<ProfitLossDTOManTasks> ProfitLossDTOManTaskss { get; set; }
        #endregion


        #region 合买相关
        public DbSet<BuyTogether> BuyTogether { get; set; }


        /// <summary>
        /// 中奖排行
        /// </summary>
        public DbSet<Rankings> Rankings { get; set; }
        #endregion



    }
}
