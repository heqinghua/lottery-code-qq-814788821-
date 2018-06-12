using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    public class VipMentionApplyVM
    {
        public int Id { get; set; }

        public string UserCode { get; set; }

        public string IsOpenVipDesc { get; set; }

        public DateTime OccDate { get; set; }

        public int TotalCount { get; set; }
    }
}
