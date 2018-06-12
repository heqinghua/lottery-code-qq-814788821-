using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    //[Table("Lottery")]
    public class Lottery : BaseEntity
    {
        public Lottery()
        {

        }
  
        public int LotteryId { get; set; }

        [MaxLength(100)]
        public string LotteryName { get; set; }

        public bool IsHighFrequency { get; set; }

        public int TotalCount { get; set; }

        public byte Status { get; set; }

        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
