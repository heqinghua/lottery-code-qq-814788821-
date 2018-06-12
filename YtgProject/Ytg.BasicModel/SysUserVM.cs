using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 后台会员列表VM
    /// </summary>
    [Serializable, DataContract]
    public class SysUserVM
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        public UserType UserType { get; set; }

        public string Sex { get; set; }

        [DataMember]
        public string Rebate { get; set; }

        [DataMember]
        public string UserAmt { get; set; }

        //[DataMember]
        //public string TotalChongzhi { get; set; }

        //[DataMember]
        //public string TotalXiaofei { get; set; }

        [DataMember]
        public DateTime OccDate { get; set; }

        [DataMember]
        public DateTime? LastLoginTime { get; set; }

        public int LoginCount { get; set; }

        public string NikeName { get; set; }

        /// <summary>
        /// 是否禁用 启用
        /// </summary>
        [DataMember]
        public bool IsDelete { get; set; }

        /// <summary>
        /// 是否开启充值
        /// </summary>
        [DataMember]
        public bool IsRecharge { get; set; }

        /// <summary>
        /// 资金状态
        /// </summary>
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 总共多少条记录
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }

        /// <summary>
        /// 用户玩法类型
        /// </summary>
        public int PlayType { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsLogin { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string MoTel { get; set; }

        /// <summary>
        /// qq
        /// </summary>
        public string Qq { get; set; }

        public int Head { get; set; }
    }
}
