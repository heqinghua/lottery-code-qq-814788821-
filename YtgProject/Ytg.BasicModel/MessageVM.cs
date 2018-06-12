using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    [Serializable,DataContract]
    public class MessageVM
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ToUserId { get; set; }

        [DataMember]
        public string ToUser { get; set; }

        [DataMember]
        public int FromUserId { get; set; }

        [DataMember]
        public string FromUser { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int MessageType { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string MessageContent { get; set; }

        [DataMember]
        public DateTime OccDate { get; set; }

        [DataMember]
        public int TotalCount { get; set; }
    }
}
