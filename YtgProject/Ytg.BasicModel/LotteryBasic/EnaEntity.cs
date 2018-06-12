using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 是否启用
    /// </summary>
    [Serializable, DataContract]
    public  abstract class EnaEntity : BaseEntity, IEnaEntity
    {
        private bool isEnable = true;
        /// <summary>
        /// 是否启用
        /// </summary>
        [DataMember]
        public bool IsEnable
        {
            get
            {
                return isEnable;
            }
            set
            {
                isEnable = value;
            }
        }
    }
}
