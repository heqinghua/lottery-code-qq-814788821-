using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
   public class UnLockParamsDTO
    {
        public string UserName { get; set; }

        public List<string> Cards { get; set; }

        public string Password { get; set; }
    }
}
