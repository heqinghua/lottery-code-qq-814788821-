using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class NextPeriod
    {
        /*
         "iGameId": 5,
            "sGamePeriod": "20170608104",
            "iCloseTime": 300, --封单时间
            "iStartTime": 0,
            "iOpenTime": 330,  --结束时间
            "sGamePeriodPrior": "20170608103",
            "dCloseTime": 42894.944097222,
            "dCloseTimeTEXT": "2017-06-08 22:39:30",
            "dStartTime": 42894.940625,
            "dStartTimeTEXT": "2017-06-08 22:34:30",
            "dOpenTimePrior": 42894.940972222,
            "dOpenTimePriorTEXT": "2017-06-08 22:35:00",
            "dCloseTimePrior": 42894.940625,
            "dCloseTimePriorTEXT": "2017-06-08 22:34:30",
            "dStartTimePrior": 42894.937152778,
            "dStartTimePriorTEXT": "2017-06-08 22:29:30",
            "sOpenNumPrior": ""
         */

        /// <summary>
        /// 游戏id
        /// </summary>
        public int iGameId { get; set; }

        /// <summary>
        /// 期数
        /// </summary>
        public string sGamePeriod { get; set; }

        /// <summary>
        /// 封单时间
        /// </summary>
        public double iCloseTime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public double iStartTime { get; set; }


        /// <summary>
        /// 开奖时间
        /// </summary>
        public double iOpenTime { get; set; }

        /// <summary>
        /// 上一期数据
        /// </summary>
        public string sGamePeriodPrior { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double dCloseTime { get; set; }


        public DateTime? dCloseTimeTEXT { get; set; }

        public double dStartTime { get; set; }

        public DateTime dStartTimeTEXT { get; set; }

        public double dCloseTimePrior { get; set; }

        public DateTime? dCloseTimePriorTEXT { get; set; }

        public double dStartTimePrior { get; set; }

        public DateTime dStartTimePriorTEXT { get; set; }

        public string sOpenNumPrior { get; set; }

        public DateTime? dOpenTimePriorTEXT { get; set; }



    }
}
