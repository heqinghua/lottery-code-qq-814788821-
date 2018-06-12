using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    public abstract class BaseDxds : BaseCalculate
    {
        /// <summary>
        /// 大
        /// </summary>
        public const int Da = -13;

        /// <summary>
        /// 小
        /// </summary>
        public const int Xiao = -14;

        /// <summary>
        /// 单
        /// </summary>
        public const int Dan = -15;

        /// <summary>
        /// 双
        /// </summary>
        public const int Shuang = -16;

        /// <summary>
        /// 计算投注数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            if (null == item || string.IsNullOrEmpty(item.BetContent)) return 1;
            else
            {
                var count = 1;
                item.BetContent.Split(',').ToList().ForEach(m => count = count * (m.Length / 3));
                return count;
            }
        }

        public override string HtmlContentFormart(string betContent)
        {
            //豹子&顺子&对子'
            string newContent = string.Empty;
            var items= betContent.Split('|');
            foreach (var item in items)
            {
                foreach (var s in item.Split('&'))
                {
                    string nv = "";
                    switch (s)
                    {
                        case "大":
                            nv = Da.ToString();
                            break;
                        case "小":
                            nv = Xiao.ToString();
                            break;
                        case "单":
                            nv = Dan.ToString();
                            break;
                        case "双":
                            nv = Shuang.ToString();
                            break;
                    }
                    newContent += nv;
                }
                newContent += ",";
            }

            if (newContent.Length > 0)
                newContent = newContent.Substring(0, newContent.Length - 1);
            return newContent;
        }
    }
}
