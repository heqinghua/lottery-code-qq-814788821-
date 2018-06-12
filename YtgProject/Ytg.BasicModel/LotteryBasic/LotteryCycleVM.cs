using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    public class LotteryCycleVM
    {
        public int Id { get; set; }

        public int LotteryId { get; set; }
        public string LotteryName { get; set; }

        public string TimeStart { get; set; }

        public string TimeEnd { get; set; }

        public int TimeInterval { get; set; }

        public int BeforeSaleDeadline { get; set; }
        
        public DateTime OccDate { get; set; }
    }
}
