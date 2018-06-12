using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;

namespace Ytg.Core.Service.Lott
{
    /// <summary>
    /// 追号详情
    /// </summary>
    public interface ISysCatchNumIssueService : ICrudService<CatchNumIssue>
    {
        /// <summary>
        /// 获取当前期号后未开奖的期号
        /// </summary>
        /// <param name="catchNumCode"></param>
        /// <param name="issueCode"></param>
        /// <returns></returns>
        List<CatchNumIssue> GetLastCatchNum(string catchNumCode, string issueCode);

        /// <summary>
        /// 获取追号详情
        /// </summary>
        /// <param name="catchCode"></param>
        /// <returns></returns>
        List<CatchNumIssue> GetCatchIssue(string catchCode);

        /// <summary>
        /// 根据追号单号和开奖期数获取详情
        /// </summary>
        /// <param name="catchCode"></param>
        /// <param name="issueCode"></param>
        BetList GetCatchIssueDetail(string catchCode, string issueCode);

        /// <summary>
        /// 添加期数
        /// </summary>
        /// <param name="issue"></param>
        /// <param name="lotteryid"></param>
        void AddCatchIssue(CatchNumIssue issue, int lotteryid);


         /// <summary>
        /// 修改期数未开奖数据为当前时间
        /// </summary>
        /// <param name="catchNumCode"></param>
        void UpdateNoOpenOccDateTime(string catchNumCode);

        /// <summary>
        /// 追号  存储过程 
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="balanceDetail"></param>
        /// <returns></returns>
        decimal? AddCatchBetting(CatchNum detail, SysUserBalanceDetail balanceDetail, int lotteryid, string issueStr, decimal detailMonery, ref int state);


        /// <summary>
        /// 根据追号单，获取彩票id
        /// </summary>
        /// <param name="catchCode"></param>
        /// <returns></returns>
        int? GetLotteryId(string catchCode);
    }
}
