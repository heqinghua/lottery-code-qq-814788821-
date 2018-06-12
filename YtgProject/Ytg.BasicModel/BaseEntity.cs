using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    [Serializable, DataContract]
    public abstract class BaseEntity
    {

        [DataMember,Key(),System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        private DateTime mOccDate = DateTime.Now;

        /// <summary>
        /// 系统当前时间
        /// </summary>
        [DataMember]
        public virtual DateTime OccDate
        {
            get { return this.mOccDate; }
            set { this.mOccDate = value; }
        }


    }
}
