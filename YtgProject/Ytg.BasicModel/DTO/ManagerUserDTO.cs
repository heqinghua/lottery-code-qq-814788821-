using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class ManagerUserDTO
    {

        public int Id { get; set; }

        public string Code { get; set; }

        public int? RoleId { get; set; }

        public DateTime OccDate { get; set; }

        public bool IsDelete { get; set; }

        public string RoleName { get; set; }
    }
}
