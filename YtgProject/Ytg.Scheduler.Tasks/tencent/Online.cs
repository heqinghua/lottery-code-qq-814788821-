using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Tasks.tencent
{
    public class Online
    {
        public string issue { get; set; }
        public string time { get; set; }
        public string total { get; set; }
        public string change { get; set; }
        public string result { get; set; }
        public int wan { get; set; }
        public int qian { get; set; }
        public int bai { get; set; }
        public int shi { get; set; }
        public int ge { get; set; }

        public override string ToString()
        {
            return "issue:" + issue +
                "time:" + time +
                "total:" + total +
                "change:" + change +
                "result:" + result +
                "wan:" + wan +
                "qian:" + qian +
                "bai:" + bai +
                "shi:" + shi +
                "ge:" + ge;
        }
    }
}
