using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.Manager
{
    /// <summary>
    /// 日结算报表
    /// </summary>
    public class Sp_DayJieSuanTable : Sp_JieSuanTable
    {
        public Sp_DayJieSuanTable() { }
        public Sp_DayJieSuanTable(bool isref)
            : base(isref)
        {
        }
        /// <summary>
        /// 天结算
        /// </summary>
        public string OccDay { get; set; }
    }
}
