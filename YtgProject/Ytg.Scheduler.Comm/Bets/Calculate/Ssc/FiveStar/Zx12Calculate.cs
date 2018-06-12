using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 四星/四星组合/组选12 验证通过 2015/05/23
    /// </summary>
    public class Zx12Calculate : BaseCalculate
    {

        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            var count = TotalBet(item, openResult);
            if (count > 0)
            {
                item.IsMatch = true;
                decimal stepAmt = 0;
                item.WinMoney = TotalWinMoney(item, GetBaseAmt(item, ref stepAmt), stepAmt, count);
            }
        }

        public override int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            return TotalBet(item, "");
        }




        private int TotalBet(BasicModel.LotteryBasic.BetDetail betDetail, string openResult)
        {
            var res = new List<string>();
            if (!string.IsNullOrEmpty(openResult))
            {
                var result = openResult.Remove(0, 2);//得到千位、百、十、个位的数字
                res = result.Split(',').ToList();
            }
            var bets = betDetail.BetContent.Split(',');
            List<int> erchonghao = bets[0].Select(m => Convert.ToInt32(m.ToString())).ToList();
            List<int> danhao = bets[1].Select(m => Convert.ToInt32(m.ToString())).ToList();
            List<string> list = new List<string>();
            if (erchonghao.Union(danhao).Distinct().Count() >= 3)
            {
                foreach (var item in erchonghao)
                {
                    string v = string.Format("{0}{0}", item);
                    var arr = danhao.Where(n => n != item).ToList();
                    if (arr.Count() < 2) continue;
                    else
                    {
                        var count = 2;
                        if (count < arr.Count)
                            count = 1;
                        while (count <= arr.Count)
                        {
                            var i = arr[0];
                            if (string.IsNullOrEmpty(openResult))
                            {//这个表示是计算注数，不是开奖
                                list.Add(string.Format("{0}{1}", v, string.Join("", arr.Take(2))));
                            }
                            else
                            {//计算开奖
                                //根据openresult判断中奖情况
                                if (res.Count(n => Convert.ToInt32(n.ToString()) == item) == 2 && res.Count(n => n == arr[0].ToString()) > 0 && res.Count(n => n == arr[1].ToString()) > 0)
                                {
                                    list.Add(string.Format("{0}{1}", v, string.Join("", arr.Take(2))));
                                }
                            }
                            arr.Remove(i);
                            arr.Add(i);
                            count++;
                        }
                    }
                }
            }
            return list.Count;
        }
    }
}
