using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 区
    /// </summary>
    [Table("S_District")]
    public class District
    {
        public District()
        {
            this.Users = new HashSet<SysUser>();
        }

        [Key, Column("DistrictID")]
        public int ID { get; set; }

        [MaxLength(50)]
        public string DistrictName { get; set; }


        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public int CityID { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<SysUser> Users { get; set; }
    }
}
