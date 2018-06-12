using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    /// <summary>
    /// 投注时用户信息
    /// </summary>
    public class BettUserDto
    {
        public int Id { get; set; }

        public double Rebate { get; set; }

        public string Code { get; set; }

        public int UserType { get; set; }

        public int PlayType { get; set; }

        /// <summary>
        /// 1表示该用户被禁用
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 1表示禁用资金
        /// </summary>
        public int Status { get; set; }

            /**
             sy.id,sy.Code,sy.Rebate,sy.UserType,sy.IsDelete,b.Status
             */
    }
}
