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
    /// 省
    /// </summary>
    [Table("S_Province")]
    public class Province
    {
        public Province()
        {
            this.Citys = new HashSet<City>();
            this.Users = new HashSet<SysUser>();
        }
        [Key,Column("ProvinceID")]
        public int ID { get; set; }

        [MaxLength(50)]
        public string ProvinceName { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public virtual ICollection<City> Citys { get; set; }

        /// <summary>
        /// 省下的所有用户
        /// </summary>
        public virtual ICollection<SysUser> Users { get; set; }
    }
}
