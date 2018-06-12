using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.AutoLogic
{
    /// <summary>
    /// 开奖历史纪录表，
    /// 用户纪录开奖总利润 
    /// </summary>
    public class OpendHistory
    {
        

        /// <summary>
        /// 投注总额
        /// </summary>
        public decimal BettMonery { get; set; }

        /// <summary>
        /// 中奖总额
        /// </summary>
        public decimal WinMonery { get; set; }

        /// <summary>
        /// 当前利润率
        /// </summary>
        //public decimal ProfitMargin
        //{
        //    get
        //    {
        //        //当前利润率
        //        return Math.Round((BettMonery-WinMonery) / BettMonery, 2)*100;
        //    }
        //}

        /// <summary>
        /// 获取释放金额
        /// </summary>
        /// <returns></returns>
        public decimal DisposeMonery()
        {
            decimal levMonery = (BettMonery * (1-ConfigHelper.ProfitMargin));//计算利润比率
            return levMonery - WinMonery;//可释放的金额
        }

        public void Inint()
        {
            this.BettMonery = 0;
            this.WinMonery = 0;
        }

        public override string ToString()
        {
            return string.Format("BettMonery:{0} WinMonery{1}  DisposeMonery:{2}", this.BettMonery, this.WinMonery, this.DisposeMonery());
        }
    }
}
