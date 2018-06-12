using System;
using System.Net;
using System.Windows;

namespace Ytg.BasicModel
{
    public class SysUserDTO
    {
        public virtual int Id { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string NikeName { get; set; }

        /// <summary>
        /// 登陆名
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 头像 存在图片表中
        /// </summary>
        public int Head { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }


        /// <summary>
        /// 用户类型，会员/代理
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// 代理用户级别 1为一级代理，2为二级代理，3为三级代理，此属性只正对代理用户
        /// </summary>
        public int ProxyLevel { get; set; }

        /// <summary>
        /// 父用户
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime OccDate { get; set; }

        /// <summary>
        /// 用户余额
        /// </summary>
        public decimal? UserAmt { get; set; }

        /// <summary>
        /// 游戏返点 
        /// </summary>
        public double? Rebate { get; set; }


        public virtual bool IsDelete
        {
            get;
            set;
        }

        /// <summary>
        /// 是否具有充值功能
        /// </summary>

        public bool IsRecharge { get; set; }

        /// <summary>
        /// 玩法类型 1700/1800  0 1800  1 1700 
        /// </summary>
        public int PlayType { get; set; }

        /// <summary>
        /// 资金状态
        /// </summary>
        public int Status { get; set; }

    }
}
