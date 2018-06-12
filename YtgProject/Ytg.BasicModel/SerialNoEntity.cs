using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 具有SerialNo 字段的实体
    /// </summary>
    public abstract class SerialNoEntity:BaseEntity
    {
        public  int SerialNo { get; set; }
    }
}
