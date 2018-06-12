using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    /// <summary>
    /// 提现，充值
    /// </summary>
    [Serializable, DataContract]
    public class WithdrawRechargePersonNumberDTO : EnaEntity
    {
        [DataMember]
        public int WithdrawPeopleNumber { get; set; }

        [DataMember]
        public int RechargePersonNumber { get; set; }

        /// <summary>
        /// 最新投注
        /// </summary>
        [DataMember]
        public int notOpen { get; set; }
    }
}
