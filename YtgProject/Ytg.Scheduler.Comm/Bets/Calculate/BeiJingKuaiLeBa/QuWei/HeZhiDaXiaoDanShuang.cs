using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.BeiJingKuaiLeBa.QuWei
{
    /// <summary>
    /// 和值大小单双 验证通过 2015/05/25
    ///  
    /// </summary>
    public class HeZhiDaXiaoDanShuang : BaseCalculate
    {
        

        /// <summary>
        /// 大.单
        /// </summary>
        public const int DaDan = -29;
        /// <summary>
        /// 大.双
        /// </summary>
        public const int DaShuang = -30;
        /// <summary>
        /// 小.单
        /// </summary>
        public const int XiaoDan = -31;
        /// <summary>
        /// 小.双
        /// </summary>
        public const int XiaoShuang = -32;

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            decimal winMonery = 0;
            var items = openResult.Split(',');
            var sum = items.Sum(m => Convert.ToInt32(m));
            string[] list = item.BetContent.Split(' ');

            if (sum > 810 && (sum % 2 != 0) && list[0] == DaDan.ToString()) winMonery += GetBaseAmtLstItem(1, item);
            if (sum > 810 && (sum % 2 == 0) && list[1] == DaShuang.ToString()) winMonery += GetBaseAmtLstItem(2, item);
            if (sum <= 810 && (sum % 2 != 0) && list[2] == XiaoDan.ToString()) winMonery += GetBaseAmtLstItem(3, item);
            if (sum <= 810 && (sum % 2 == 0) && list[3] == XiaoShuang.ToString()) winMonery += GetBaseAmtLstItem(4, item);

            if (winMonery != 0)
            {
                item.IsMatch = true;
                item.WinMoney = winMonery;
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(' ').Length;
        }
    }
}
