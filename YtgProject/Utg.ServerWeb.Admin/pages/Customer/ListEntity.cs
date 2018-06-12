using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utg.ServerWeb.Admin.pages.Customer
{
   public class ListEntity
    {
        public int QuoId { get; set; }

        public string QuoType { get; set; }

        public double QuoValue { get; set; }

        public double ChildQuoValue { get; set; }
    }
}
