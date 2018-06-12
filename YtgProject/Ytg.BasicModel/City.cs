using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    [Table("S_City")]
    public class City
    {
        public City()
        {
            this.Districts = new HashSet<District>();
            this.Users = new HashSet<SysUser>();
        }
        [Key(), Column("CityID")]
        public int ID { get; set; }

        [MaxLength(50)]
        public string CityName { get; set; }

        [MaxLength(50)]
        public string ZipCode { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public int ProvinceID { get; set; }

        public virtual Province Province { get; set; }

        public virtual ICollection<District> Districts { get; set; }

        /// <summary>
        /// 区下的所有用户
        /// </summary>
        public virtual ICollection<SysUser> Users { get; set; }
    }
}
