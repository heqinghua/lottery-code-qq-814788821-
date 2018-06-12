using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Data.EfConfig
{
    /// <summary>
    /// 系统模块配置
    /// </summary>
   public class SysEfConfig
    {
       public static void Initital(DbModelBuilder modelBuilder)
       {
           modelBuilder.Entity<SysMenu>().HasMany(t => t.Operates).WithRequired(t => t.SysMenu);

           modelBuilder.Properties<decimal>().Configure(config => config.HasPrecision(18, 4));
       }
    }
}
