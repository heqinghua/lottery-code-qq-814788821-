using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Ytg.BasicModel
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("SysYtgUser")]
    public  class SysUser:DelEntity
    {
        public SysUser()
        {
            this.PlayType = UserPlayType.P1800;
            this.UserType = BasicModel.UserType.General;
            this.AutoRegistRebate = 0;
            this.IsRecharge = false;
            this.IsLogin = false;
            this.LoginCount = 0;
            this.ProxyLevel = 1;
            this.MinBettingMoney = 0;//最低提款金额
            this.MinBettingMoneryTime = DateTime.Now;
        }
        
        /// <summary>
        /// 用户姓名
        /// </summary>
        [MaxLength(100), DataMember]
        public  string NikeName { get; set; }

        /// <summary>
        /// 登陆名
        /// </summary>
        [MaxLength(50),DataMember]
        public  string Code { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        [MaxLength(50), JsonIgnore]
        public  string Password { get; set; }

        /// <summary>
        /// 头像 存在图片表中
        /// </summary>
        [DataMember]
        public  int Head { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        [MaxLength(4), DataMember]
        public  string Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [DataMember]
        public  DateTime? Birthday { get; set; }

      
        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(50)]
        public  string MoTel { get; set; }

        /// <summary>
        /// qq
        /// </summary>
        [MaxLength(50)]
        public  string Qq { get; set; }


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

        [JsonIgnore]
        public virtual SysUser Parent { get; set; }

        /// <summary>
        /// 游戏保留返点 相对于总代理
        /// </summary>
        [DataMember]
        public double Rebate { get; set; }

        /// <summary>
        /// 自动注册保留返点 相对于总代理
        /// </summary>
        [DataMember]
        public double AutoRegistRebate { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最后登录ip
        /// </summary>
        public string LastLoginIp { get; set; }
        /// <summary>
        /// 最后登录Ip区域
        /// </summary>
        public string LastCityName { get; set; }

        /// <summary>
        /// 最后登录操作系统
        /// </summary>
        public string ServerSystem { get; set; }

        /// <summary>
        /// 最后登录设备
        /// </summary>
        public string UseSource { get; set; }

        /// <summary>
        /// 是否登陆   聊天程序
        /// </summary>
        /// <returns></returns>
        public bool IsLogin { get; set; }

        /// <summary>
        /// 是否登录系统，可能尚未连接聊天程序
        /// </summary>
        public bool IsLineLogin { get; set; }

        /// <summary>
        /// 是否具有充值功能
        /// </summary>
        [DataMember]
        public bool IsRecharge { get; set; }

        /// <summary>
        /// 用户奖金类型
        /// </summary>
        [DataMember]
        public UserPlayType PlayType { get; set; }

        /// <summary>
        /// 问候语
        /// </summary>
        [DataMember]
        public string Greetings { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [DataMember]
        public int LoginCount { get; set; }

        [DataMember]
        public int? RoleId { get; set; }

        /// <summary>
        /// 是否锁定银行卡
        /// </summary>
        public bool IsLockCards { get; set; }

        /// <summary>
        /// 用户自己解锁次数
        /// </summary>
        public int UserLockCount { get; set; }

        /// <summary>
        /// 最低提款金额
        /// </summary>
        public decimal MinBettingMoney { get; set; }

        /// <summary>
        /// 最近一次提款时间
        /// </summary>
        public DateTime MinBettingMoneryTime { get; set; }

        
    }
}
