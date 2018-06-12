using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanSi
{
    /// <summary>
    /// 组选4 2016 01 21
    /// </summary>
    public class RenXuanZu4 : BaseRenXuan
    {
        protected override void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);
            openResult = openResult.Replace(",", "");

            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 4);
            List<string> winRes = new List<string>();
            foreach (var b in v._combinations)
            {
                var res = string.Join("", openResult[b[0]-1], openResult[b[1]-1], openResult[b[2]-1], openResult[b[3]-1]).OrderBy(c => c);
                winRes.Add(string.Join("", res));
            }

            var count = TotalBet(content, winRes);
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

            string postionStr = "";
            string content = SplitRenXuanContent(item.BetContent, ref postionStr);

            Combinations<int> v = new Combinations<int>(postionStr.Select(c => Convert.ToInt32(c.ToString())).ToList(), 4);
            int count = TotalBet(content, null);
            int notes = 0;
            foreach (var b in v._combinations)
            {
                notes += count;
            }
            return notes;
        }

        //#region 计算具体组合及注数

        //private int TotalBet(List<int> sanchonghao, List<int> danhao)
        //{
        //    List<string> list = new List<string>();
        //    if (sanchonghao.Count >= 1 && danhao.Count >= 1)
        //    {
        //        if (sanchonghao.Union(danhao).Distinct().Count() >= 2)
        //        {
        //            sanchonghao.ForEach(m =>
        //            {
        //                var v = string.Format("{0}{0}{0}", m);
        //                var arr = danhao.Where(n => n != m).ToList();
        //                if (arr.Count >= 1)
        //                {
        //                    var count = 1;
        //                    while (count <= arr.Count)
        //                    {
        //                        var i = arr[0];
        //                        list.Add(string.Format("{0}{1}", v, string.Join("", arr.Take(1))));
        //                        arr.Remove(i);
        //                        arr.Add(i);
        //                        count++;
        //                    }
        //                }
        //            });
        //        }
        //    }
        //    return list.Count;
        //}

        //#endregion


        private int TotalBet(string content, List<string> winRes)
        {

            var bets = content.Split(',');
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
                                if (winRes == null)
                                {//这个表示是计算注数，不是开奖
                                    list.Add(string.Format("{0}{1}", v, string.Join("", arr.Take(2))));
                                }
                                else
                                {//计算开奖
                                    //根据openresult判断中奖情况
                                    if (winRes.Count(w => w.Count(n => Convert.ToInt32(n.ToString()) == m) == 3 && w.Contains(i.ToString())) > 0)
                                    {
                                        list.Add(string.Format("{0}{1}", v, string.Join("", arr.Take(2))));
                                    }
                                    //if (res.Count(n => Convert.ToInt32(n.ToString()) == m) == 3 && res.Contains(i.ToString()))
                                    //{
                                    //    list.Add(string.Format("{0}{1}", v, string.Join("", arr.Take(2))));
                                    //}
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

        protected override int PostionLen
        {
            get
            {
                return 4;
            }
        }

        public override string HtmlContentFormart(string betContent)
        {
            if (string.IsNullOrEmpty(betContent))
                return string.Empty;
            betContent = betContent.Replace("|", ",");
            string postionStr = string.Empty;
            string contentCenter = this.SplitRenXuanContent(betContent, ref postionStr);
            if (!this.VerificationPostion(postionStr)
                || string.IsNullOrEmpty(contentCenter))
                return string.Empty;


            var contentArray = contentCenter.Split(',');
            if (contentArray.Length != 2 ||
                contentArray[0].Replace("&", "").Length < 1
                || contentArray[1].Replace("&", "").Length < 1)
                return string.Empty;

            if (VCompled(contentArray[0])
                && VCompled(contentArray[1]))
                return betContent.Replace("&", "");
            return string.Empty;
        }

        private bool VCompled(string items)
        {
            var contentArray = items.Split('&').Select(x => x);
            foreach (var item in contentArray)
            {
                int num = Convert.ToInt32(item);
                if (num < 0 || num > 9) return false;
            }
            return true;
        } 

    }

    
}
