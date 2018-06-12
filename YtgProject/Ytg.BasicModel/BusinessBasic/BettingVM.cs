using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.BusinessBasic
{
    public class BettingVM
    {
        /// <summary>
        /// 注单编号
        /// </summary>
        public string NoteNum { get; set; }
        /// <summary>
        /// 追号编号
        /// </summary>
        public string ChaseNum { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public int GameType { get; set; }
        public int PlayType { get; set; }
        /// <summary>
        /// 期号
        /// </summary>
        public int PeriodNum { get; set; }
        public string BettingContent { get; set; }
        /// <summary>
        /// 倍数
        /// </summary>
        public int Multiple { get; set; }
        public string Pattern { get; set; }
        public decimal MoneySum { get; set; }
        public decimal Bonus { get; set; }
        /// <summary>
        /// 开奖号码
        /// </summary>
        public int LotteryNum { get; set; }
        public int Status { get; set; }
    }
}
