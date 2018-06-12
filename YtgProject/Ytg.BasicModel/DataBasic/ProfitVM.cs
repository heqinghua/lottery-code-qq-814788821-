using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.DataBasic
{
    public class ProfitVM
    {
        public DateTime Date { get; set; }

        //礼金
        public decimal GiftSUM { get; set; }
        //充值
        public decimal RechargeSUM { get; set; }
        //提现
        public decimal CashSUM { get; set; }
        //投注
        public decimal BettingSUM { get; set; }
        //返点
        public decimal RebateSUM { get; set; }
        //中奖
        public decimal PrizeSUM { get; set; }
        //分红
        public decimal BonusSUM { get; set; }
        //盈亏
        public decimal ProfitSUM { get; set; }
    }
}
