using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Web.Security;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Ytg.BasicModel;

namespace Ytg.Comm.Security
{
    public class CookUserInfo : IPrincipal
    {
        public int Id { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public int Head { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NikeName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }


        /// <summary>
        /// 用户返点
        /// </summary>
        public double Rebate { get; set; }

        /// <summary>
        /// 用户奖金类型
        /// </summary>
        public UserPlayType PlayType { get; set; }

        public int ProxyLevel { get; set; }
        
        

       public override string ToString()
       {
           return string.Format("Code:{0},NickName:{1}", this.Code,this.NikeName);
       }



       [ScriptIgnore, JsonIgnore]
       public IIdentity Identity
       {
           get { return null; }
       }

       public bool IsInRole(string role)
       {
           return true;
       }
       /// <summary>
       /// 是否具有充值功能
       /// </summary>
       public bool IsRecharge { get; set; }
    }
}
