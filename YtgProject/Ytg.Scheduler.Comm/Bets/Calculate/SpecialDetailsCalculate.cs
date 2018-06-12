using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 特殊号  顺子，对子，豹子
    /// </summary>
    public  abstract class SpecialDetailsCalculate:BaseCalculate
    {
        public override string HtmlContentFormart(string betContent)
        {
            //豹子&顺子&对子'
            string newContent = string.Empty;
            foreach (var s in betContent.Split('&'))
            {
                string nv = "";
                switch (s)
                {
                    case "豹子":
                        nv = "-10";
                        break;
                    case "顺子":
                        nv = "-11";
                        break;
                    case "对子":
                        nv = "-12";
                        break;
                }
                newContent += nv;
            }

            return newContent;
        }
    }
}
