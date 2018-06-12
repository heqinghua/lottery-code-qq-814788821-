using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    public class SysAccountLogsMV
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int TotalCount{get;set;}
        public string UseSource { get; set; }
        public string UserId { get; set; }
        public string Ip { get; set; }
        public string ServerSystem { get; set; }
        public string Descript { get; set; }
        public DateTime OccDate { get; set; }
    }
}
