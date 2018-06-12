using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class RechargeBankTypeDTO : SysBankTransferVM
    {
        /// <summary>
        /// 是否支持跨行
        /// </summary>
        public bool IsInterBank { get; set; }

        /// <summary>
        /// 是否开通充值功能
        /// </summary>
        public bool OpenAutoRecharge { get; set; }
    }
}
