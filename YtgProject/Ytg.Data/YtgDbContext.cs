using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Data.EfConfig;

namespace Ytg.Data
{
    /// <summary>
    /// 数据上下文
    /// </summary>
    public partial class YtgDbContext : DbContext
    {
        /*************
            * 开发中多次修改数据实体和数据库结构
            * 加入该句代码可自动同步实体与数据库版本
            * 实际发布中将注释该代码
            * ************/
        public YtgDbContext()     
            : base("name=YtgConnection")
        {
            //this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            //this.Messages.me = MergeOption.NoTracking;
        }
        /*************
         end
         */

        public YtgDbContext(string connectionString)
            : base(connectionString)
        {
            //this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //系统模块配置
            SysEfConfig.Initital(modelBuilder);
        }


        #region 基础信息

        /// <summary>
        /// 省
        /// </summary>
        public DbSet<Province> Provinces { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public DbSet<City> Citys { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public DbSet<District> Districts { get; set; }

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<SysUser> SysUsers { get; set; }

        /// <summary>
        /// 用户余额
        /// </summary>
        public DbSet<SysUserBalance> SysUserBalances { get; set; }

        /// <summary>
        /// 用户余额明细
        /// </summary>
        public DbSet<SysUserBalanceDetail> SysUserBalanceDetails { get; set; }

        /// <summary>
        /// 操作表
        /// </summary>
        public DbSet<SysRolePermission> SysRolePermissions { get; set; }

        /// <summary>
        /// 系统菜单
        /// </summary>
        public DbSet<SysMenu> SysMenus { get; set; }

        /// <summary>
        /// 组织架构
        /// </summary>
        public DbSet<SysOrganize> SysOrganizes { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<SysRole> SysRoles { get; set; }

        /// <summary>
        /// 银行信息
        /// </summary>
        public DbSet<SysBankType> SysBankType { get; set; }

        /// <summary>
        /// 公司银行信息
        /// </summary>
        public DbSet<CompanyBank> CompanyBank { get; set; }

        /// <summary>
        /// 字典表
        /// </summary>
        public DbSet<SysDictionary> SysDictionary { get; set; }

        /// <summary>
        /// 菜单操作
        /// </summary>
        public DbSet<SysOperate> SysOperate { get; set; }

        /// <summary>
        /// 银行转账设置
        /// </summary>
        public DbSet<SysBankTransfer> SysBankTransfer { get; set; }

        /// <summary>
        /// 提现、充值
        /// </summary>
        public DbSet<SysBankPoundage> SysBankPoundage { get; set; }

        /// <summary>
        /// 公告管理
        /// </summary>
        public DbSet<SysNotice> SysNotice { get; set; }

        /// <summary>
        /// 配额
        /// </summary>
        public DbSet<SysQuota> SysQuotas { get; set; }

        /// <summary>
        /// 配额账变记录
        /// </summary>
        public DbSet<SysQuotaDetail> SysQuotaDetails { get; set; }

        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// 新闻
        /// </summary>
        public DbSet<SysNews> News { get; set; }

        /// <summary>
        /// 用户对应银行卡
        /// </summary>
        public DbSet<SysUserBank> SysUserBanks { get; set; }

        /// <summary>
        /// 用户体现记录
        /// </summary>
        public DbSet<MentionQueu> MentionQueus { get; set; }

        /// <summary>
        /// 系统参数设置表
        /// </summary>
        public DbSet<SysSetting> SysSettings { get; set; }

        /// <summary>
        /// 用户充提VIP申请
        /// </summary>
        public DbSet<VipMentionApply> VipMentionApplys { get; set; }

        /// <summary>
        /// 充值记录临时表信息
        /// </summary>
        public DbSet<RecordTemp> RecordTemps { get; set; }

        /// <summary>
        /// 系统日志表
        /// </summary>
        public DbSet<SysLog> SysLogs { get; set; }

        /// <summary>
        /// 首页Banner图
        /// </summary>
        public DbSet<Banner> Banners { get; set; }

        /// <summary>
        /// 活动
        /// </summary>
        public DbSet<Market> Markets { get; set; }

        /// <summary>
        /// 及时聊天信息
        /// </summary>
        public DbSet<ChartMessage> ChartMessages { get; set; }

        /// <summary>
        /// 系统操作日志
        /// </summary>
        public DbSet<SysOperationLog> SysOperationLogs { get; set; }

        /// <summary>
        /// 系统管理员
        /// </summary>
        public DbSet<SysAccount> SysAccounts { get; set; }

        /// <summary>
        /// 后台登录日志
        /// </summary>
        public DbSet<SysAccountLog> SysAccountLogs { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// 角色权限
        /// </summary>
        public DbSet<RolePermission> RolePermissions { get; set; }

        /// <summary>
        /// 管理员角色
        /// </summary>
        public DbSet<AccountRole> AccountRoles { get; set; }


        /// <summary>
        /// 用户session 表
        /// </summary>
        //public DbSet<YtgSession> YtgSessions { get; set; }
        #endregion


        
           //session 
        public DbSet<UserSession> UserSessions { get; set; }

    }
}
