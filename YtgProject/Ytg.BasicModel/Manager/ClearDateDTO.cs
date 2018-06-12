using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.Manager
{
    /// <summary>
    /// 清理数据实体
    /// </summary>
    public class ClearDateDTO
    {
        public int UserCount { get; set; }
        public int bettCount { get; set; }
        public int CusLoginLog { get; set; }
        public int OpenIssue { get; set; }
        public int SysLoginLog { get; set; }
        public int CusMessage { get; set; }
        public int ChartMsg { get; set; }
        public int rechangeCount { get; set; }
        public int TiXian { get; set; }
        public int ZhanBian { get; set; }
    }
}
