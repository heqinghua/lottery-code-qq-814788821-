using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class Record
    {
        //{"sGamePeriod":"2017126","iOpenNum":"4","iYL0":2,"iYL1":11,"iYL2":6,"iYL3":15,"iYL4":0,"iYL5":7,"iYL6":8,"iYL7":9,"iYL8":1,"iYL9":20},
        public string sGamePeriod { get; set; }
        public int iOpenNum { get; set; }

        public int iYL0 { get; set; }
        public int iYL1 { get; set; }
        public int iYL2 { get; set; }
        public int iYL3 { get; set; }
        public int iYL4 { get; set; }
        public int iYL5 { get; set; }
        public int iYL6 { get; set; }
        public int iYL7 { get; set; }
        public int iYL8 { get; set; }
        public int iYL9 { get; set; }


        public void AppendYl(Record item)
        {
            this.iYL0=item.iYL0 +1;
            this.iYL1 = item.iYL1 +1;
            this.iYL2 = item.iYL2 + 1;
            this.iYL3 = item.iYL3 + 1;
            this.iYL4 = item.iYL4 + 1;
            this.iYL5 = item.iYL5 + 1;
            this.iYL6 = item.iYL6 + 1;
            this.iYL7 = item.iYL7 + 1;
            this.iYL8 = item.iYL8 + 1;
            this.iYL9 = item.iYL9 + 1;
        }
    }
}
