using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    /// <summary>
    /// 佣金大返利
    /// </summary>
   public class YongJinDTO
    {
        //my1.id,ParentId,SUM(TradeAmt) as TradeAmt

       /// <summary>
       /// id
       /// </summary>
       public int id { get; set; }

       /// <summary>
       /// 父用户id
       /// </summary>
       public int? ParentId { get; set; }

       /// <summary>
       /// 消费金额
       /// </summary>
       public decimal TradeAmt { get; set; }
    }
}
