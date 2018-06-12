using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DTO
{
    public class Server_IssueResultDTO
    {
        /// <summary>
        /// 期号
        /// </summary>
        public string datesn { get; set; }

        /// <summary>
        /// 开奖结果
        /// </summary>
        public DateTime code { get; set; }
    }
}
