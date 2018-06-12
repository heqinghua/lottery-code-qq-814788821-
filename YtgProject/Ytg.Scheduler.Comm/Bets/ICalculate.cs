using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.LotteryBasic;

namespace Ytg.Scheduler.Comm.Bets
{
    /// <summary>
    /// 数据计算接口
    /// </summary>
    public interface ICalculate
    {

        /// <summary>
        /// 计算中奖金额，并更新实体
        /// </summary>
        /// <param name="issueCode">当前开奖期号</param>
        /// <param name="openResult">当前中奖结果</param>
        /// <param name="item">投注明细</param>
        void Calculate(string issueCode, string openResult, BetDetail item);

        /// <summary>
        /// 计算具体多少注
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int TotalBetCount(BetDetail item);

        /// <summary>
        /// html 投注内容格式化
        /// </summary>
        /// <param name="item"></param>
        string HtmlContentFormart(string content);

        /// <summary>
        /// 获取奖金
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        decimal GetBaseAmt(BasicModel.LotteryBasic.BetDetail item);

       
    }
}
