using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
   public class SysQuotaDTO
    {
       /// <summary>
       /// id
       /// </summary>
       public int Id { get; set; }


       /// <summary>
       /// 隶属用户
       /// </summary>
       public int UserId { get; set; }

   

       /// <summary>
       /// 配额名称
       /// </summary>
       public string QuotaName { get; set; }

       /// <summary>
       /// 配额最大数量
       /// </summary>
       public int MaxNum { get; set; }

       /// <summary>
       /// 已经使用开户额
       /// </summary>
       public int CurNum { get; set; }

       /// <summary>
       /// 父用户剩余配额
       /// </summary>
       public int ParentCurNum { get; set; }
    }
}
