using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    /// <summary>
    /// 活动服务
    /// </summary>
    public class MarketService : CrudService<Market>, IMarketService
    {
        log4net.ILog log = null;
        public MarketService(IRepo<Market> repo)
            : base(repo)
        {
            log = log4net.LogManager.GetLogger("errorLog"); 
        }

        #region 活动列表

        /// <summary>
        /// 活动列表
        /// </summary>
        /// <returns></returns>
        public List<Market> GetMarkets()
        {
            string sqlstr = "select * from Markets";
            return this.GetSqlSource<Market>(sqlstr);
        }

        #endregion

        #region 获取活动菜单

        /// <summary>
        /// 获取活动菜单
        /// </summary>
        /// <returns></returns>
        public List<Market> GetMarketByMeun()
        {
            string sqlstr = "select * from Markets where IsMenu=1 and IsColse=0";
            return this.GetSqlSource<Market>(sqlstr);
        }

        #endregion

        #region 检查当前用户,当天是否已经参与过此活动

        /// <summary>
        /// 检查当前用户,当天是否已经参与过此活动
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool MarketIsExist(int uid,int tradeType)
        {
            string sqlStr = "select * from SysUserBalanceDetails  where UserId=" + uid + " and TradeType=" + tradeType + " and CONVERT(varchar(10), OccDate, 120)= CONVERT(varchar(10), GETDATE(), 120)";
            if (this.GetSqlSource<SysUserBalanceDetail>(sqlStr).FirstOrDefault() == null)
                return false;
            return true;
        }

        #endregion

        #region 检查当前活动是否关闭

        /// <summary>
        /// 检查当前活动是否关闭
        /// </summary>
        /// <returns></returns>
        public bool MarketIsClose(string marketType)
        {
            string sqlStr = "select * from Markets where MarketType='" + marketType + "'";
            if (this.GetSqlSource<SysUserBalanceDetail>(sqlStr).FirstOrDefault() == null)
                return false;
            return true;
        }

        #endregion

        #region 保存活动

        /// <summary>
        /// 保存活动
        /// </summary>
        /// <param name="market"></param>
        /// <returns></returns>
        public bool SaveMarket(ref Market mMarket)
        {
            try
            {
                Market market = this.Get(mMarket.Id);
                if (market != null)
                {
                    market.MarketName = mMarket.MarketName;
                    market.MarketRule = mMarket.MarketRule;
                    market.IsMenu = mMarket.IsMenu;
                    market.IsColse = mMarket.IsColse;
                    market.Description = mMarket.Description;
                }
                else
                {
                    this.Create(mMarket);
                }

                this.Save();
                return true;
            }
            catch(Exception ex)
            {
                log.Error("SaveMarket", ex);
                return false;
            }
        }

        #endregion

        #region 获取注册活动

        /// <summary>
        /// 获取注册活动
        /// </summary>
        /// <returns></returns>
        public Market GetZcMarket()
        {
            string sqlstr = "select * from Markets where MarketType='Zc'";
            return this.GetSqlSource<Market>(sqlstr).FirstOrDefault();
        }

        #endregion

        #region 获取满赠活动

        /// <summary>
        /// 获取满赠活动
        /// </summary>
        /// <returns></returns>
        public Market GetMzMarket()
        {
            string sqlstr = "select * from Markets where MarketType='Mz'";
            return this.GetSqlSource<Market>(sqlstr).FirstOrDefault();
        }

        #endregion

        #region 获取奇兵夺宝活动

        /// <summary>
        /// 获取奇兵夺宝活动
        /// </summary>
        /// <returns></returns>
        public Market GetQbdbMarket()
        {
            string sqlstr = "select * from Markets where MarketType='Qbdb'";
            return this.GetSqlSource<Market>(sqlstr).FirstOrDefault();
        }

        #endregion

        #region 获取充值返现

        /// <summary>
        /// 获取充值返现
        /// </summary>
        /// <returns></returns>
        public Market GetCzfxMarket()
        {
            string sqlstr = "select * from Markets where MarketType='Czfx'";
            return this.GetSqlSource<Market>(sqlstr).FirstOrDefault();
        }

        #endregion
    }
}
