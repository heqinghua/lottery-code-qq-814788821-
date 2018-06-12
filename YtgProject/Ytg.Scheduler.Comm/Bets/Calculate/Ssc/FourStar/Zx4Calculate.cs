using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.FourStar
{
    /// <summary>
    /// 四星组选4 验证通过 2016 01 18
    /// </summary>
    public class Zx4Calculate : ZhiXuanFushiDetailsCalculate
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
            //这里计算得到多少注
            //注数
            if (string.IsNullOrEmpty(item.BetContent))
                return 0;
            var array = item.BetContent.Split(',');
            var sanchonghao = array[0].Select(c=>Convert.ToInt32(c.ToString())).ToList();
            var danhao = array[1].Select(c => Convert.ToInt32(c.ToString())).ToList(); ;
            if (sanchonghao == null || danhao == null || sanchonghao.Count == 0 || danhao.Count == 0)
                return 0;

            return TotalBet(sanchonghao, danhao);
        }

        #region 计算具体组合及注数
        private int TotalBet(List<int> sanchonghao, List<int> danhao)
        {
            List<string> list = new List<string>();
            if (sanchonghao.Count >= 1 && danhao.Count >= 1)
            {
                if (sanchonghao.Union(danhao).Distinct().Count() >= 2)
                {
                    sanchonghao.ForEach(m =>
                    {
                        var v = string.Format("{0}{0}{0}", m);
                        var arr = danhao.Where(n => n != m).ToList();
                        if (arr.Count >= 1)
                        {
                            var count = 1;
                            while (count <= arr.Count)
                            {
                                var i = arr[0];
                                list.Add(string.Format("{0}{1}", v, string.Join("", arr.Take(1))));
                                arr.Remove(i);
                                arr.Add(i);
                                count++;
                            }
                        }
                    });
                }
            }
            return list.Count;
        }
        #endregion

        private int TotalBet(BasicModel.LotteryBasic.BetDetail betDetail, string openResult)
        {
            var res = new List<string>();
            if (!string.IsNullOrEmpty(openResult))
            {
                var result = openResult.Remove(0, 2);//得到千位、百、十、个位的数字
                res = result.Split(',').ToList();
            }
            var bets = betDetail.BetContent.Split(',');
            List<int> sanchonghao = bets[0].Select(m => Convert.ToInt32(m.ToString())).ToList();
            List<int> danhao = bets[1].Select(m => Convert.ToInt32(m.ToString())).ToList();
            List<string> list = new List<string>();
            if (sanchonghao.Count >= 1 && danhao.Count >= 1)
            {
                if (sanchonghao.Union(danhao).Distinct().Count() >= 2)
                {
                    sanchonghao.ForEach(m =>
                    {
                        var v = string.Format("{0}{0}{0}", m);
                        var arr = danhao.Where(n => n != m).ToList();
                        if (arr.Count >= 1)
                        {
                            var count = 1;
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
                                    if (res.Count(n => Convert.ToInt32(n.ToString()) == m) == 3 && res.Contains(i.ToString()))
                                    {
                                        list.Add(string.Format("{0}{1}", v, string.Join("", arr.Take(2))));
                                    }
                                }
                                arr.Remove(i);
                                arr.Add(i);
                                count++;
                            }
                        }
                    });
                }
            }
            return list.Count;
        }

        protected override int GetLen
        {
            get
            {
                return 2;
            }
        }

        public override string HtmlContentFormart(string betContent)
        {
            betContent= base.HtmlContentFormart(betContent);
            if (string.IsNullOrEmpty(betContent))
                return string.Empty;
            var array= betContent.Split(',');
            if (array[0].Length < 1
                || array[1].Length < 1)
                return string.Empty;
            return betContent;
        }
    }
}
