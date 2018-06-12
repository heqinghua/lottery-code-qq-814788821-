using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.ShiYiXuanWu.QuWeiXing
{
    /// <summary>
    /// 广东十一选五/趣味定单双/趣味_定单双 验证通过 2015/05/25
    /// </summary>
    public class QuWeiDingDanShuang : BaseCalculate
    {
        /// <summary>
        /// 5单0双
        /// </summary>
        public const int WuDanLinShuang = -17;
        /// <summary>
        /// 4单1双
        /// </summary>
        public const int SiDanYiShuang = -18;
        /// <summary>
        /// 3单2双
        /// </summary>
        public const int SanDanErShuang = -19;
        /// <summary>
        /// 2单3双
        /// </summary>
        public const int ErDanSanShuang = -20;

        /// <summary>
        /// 1单4双
        /// </summary>
        public const int YiDanSiShuang = -21;

        /// <summary>
        /// 0单5双
        /// </summary>
        public const int LinDanWuShuang = -22;

        public string[] mInStrArray = new string[]{
            "5单0双",
            "4单1双",
            "3单2双",
            "2单3双",
            "1单4双",
            "0单5双"
        };

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            //0单5双
            var _0d5s = this.GetBaseAmtLstItem(1, item);//* Ytg.Comm.Utils.MaxRemoLv
            _0d5s = _0d5s - _0d5s * 0.06m;
            //5单0双
            var _5d0s = this.GetBaseAmtLstItem(2, item) ;
            _5d0s = _5d0s - _5d0s * 0.06m;
            //1单4双
            var _1d4s = this.GetBaseAmtLstItem(3, item) ;
            _1d4s = _1d4s - _1d4s * 0.06m;
            //4单1双
            var _4d1s = this.GetBaseAmtLstItem(4, item) ;
            _4d1s = _4d1s - _4d1s * 0.06m;
            //2单3双
            var _2d3s = this.GetBaseAmtLstItem(5, item) ;
            _2d3s = _2d3s - _2d3s * 0.06m;
            ////3单2双
            var _3d2s = this.GetBaseAmtLstItem(6, item) ;
            _3d2s = _3d2s - _3d2s * 0.06m;
           // List<int> resultList = new List<int>();
            var resultArray= openResult.Split(',');

            var shuangCount = resultArray.Where(r => Convert.ToInt32(r) % 2 == 0).Count();
            var danCount = resultArray.Length - shuangCount;

            var items = item.BetContent.Replace(mInStrArray[0], WuDanLinShuang.ToString()).Replace(mInStrArray[1], SiDanYiShuang.ToString())
                .Replace(mInStrArray[2], SanDanErShuang.ToString()).Replace(mInStrArray[3], ErDanSanShuang.ToString()).Replace(mInStrArray[4], YiDanSiShuang.ToString())
                .Replace(mInStrArray[5], LinDanWuShuang.ToString()).Split(',');
            var winMonery = 0m;

            foreach (string c in items)
            {
                switch (Convert.ToInt32(c))
                {
                    case WuDanLinShuang://
                        if (danCount == 5 && shuangCount == 0)
                            winMonery += _5d0s;
                        break;
                    case SiDanYiShuang:
                        if (danCount == 4 && shuangCount == 1)
                            winMonery += _4d1s;
                        break;
                    case SanDanErShuang:
                        if (danCount == 3 && shuangCount == 2)
                            winMonery += _3d2s;
                        break;
                    case ErDanSanShuang:
                        if (danCount == 2 && shuangCount == 3)
                            winMonery += _2d3s;
                        break;
                    case YiDanSiShuang:
                        if (danCount == 1 && shuangCount == 4)
                            winMonery += _1d4s;
                        break;
                    case LinDanWuShuang:
                        if (danCount == 0 && shuangCount == 5)
                            winMonery += _0d5s;
                        break;
                }
            }

            if (winMonery != 0)
            {
                item.IsMatch = true;
                item.WinMoney = BiaoZhunTotalWinMoney(item, winMonery);
            }

        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.BetContent.Split(',').Length;
        }


        public override string HtmlContentFormart(string betContent)
        {
            //5单0双&4单1双&3单2双&2单3双&1单4双&0单5双
            betContent=betContent.Replace('&', ',');
            var array = betContent.Split(',');
            foreach (var a in array)
            {
                if (!mInStrArray.Contains(a))
                    return "";
            }
            return betContent;
        }

     

        
    }
}
