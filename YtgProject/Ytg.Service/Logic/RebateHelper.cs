using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Service;

namespace Ytg.Service.Logic
{
    /// <summary>
    /// 返点辅助类
    /// </summary>
    public class RebateHelper
    {
        private ISysUserService mSysUserService;//用户

        private ISysUserBalanceService mSysUserBalanceService;//用户余额

        private ISysUserBalanceDetailService mSysUserBalanceDetailService;//用户余额明细

        public RebateHelper(ISysUserService sysUserService,
            ISysUserBalanceService sysUserBalanceService,
            ISysUserBalanceDetailService sysUserBalanceDetailService)
        {
            this.mSysUserService = sysUserService;
            this.mSysUserBalanceService = sysUserBalanceService;
            this.mSysUserBalanceDetailService = sysUserBalanceDetailService;
        }


        #region 获取玩法最高返点

        public double GetRadioMaxRemo(int palyRadioCode, int bonusLevel)
        {
            var source = Ytg.Service.Lott.BaseDataCatch.GetPalyTypeRadio();
            var palyTypeRadio = source.Where(c => c.RadioCode == palyRadioCode).FirstOrDefault();
            if (null == palyTypeRadio)
                return Ytg.Comm.Utils.MaxRemo;
            return bonusLevel == 1700 ? palyTypeRadio.MaxRebate1700 : palyTypeRadio.MaxRebate;
        }
        #endregion

        /// <summary>
        /// 投注返点
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="sumMonery">投注消费总金额</param>
        /// <param name="prizeType">返点类型 0有返点,1无返点</param>
        /// <param name="rebate">当前用户返点值</param>
        public void BettingCalculate(int prizeType,int userid, decimal sumMonery, string receNo, double maxRebate)
        {
                //根据用户id获取所有上级用户返点
            this.CalcuateParent(prizeType, userid, sumMonery, TradeType.游戏返点, receNo, maxRebate);
        }


        /// <summary>
        /// 追号返点
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="sumMonery">投注消费总金额</param>
        /// <param name="prizeType">返点类型 0有返点,1无返点</param>
        /// <param name="rebate">当前用户返点值</param>
        public void CatchCalculate(int prizeType, int userid, decimal sumMonery, string receNo, double maxRebate)
        {
            //根据用户id获取所有上级用户返点
            this.CalcuateParent(prizeType,userid, sumMonery, TradeType.追号返款, receNo, maxRebate);
        }

        /// <summary>
        /// 撤单回收返点
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="sumMonery"></param>
        /// <param name="receNo"></param>
        public void BettingCannelIssue(int userid, decimal sumMonery, string receNo, double maxRebate)
        {
            //撤销全额退还
            UpdateUserBanance(userid, sumMonery, TradeType.撤单返款,receNo,0);
            //
        }


        #region 获取所有上级用户,并增加/减少对应的返点

        /// <summary>
        /// 获取所有上级用户,并增加/减少对应的返点
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sumMonery"></param>
        /// <param name="tp"></param>
        /// <param name="relevanceNo"></param>
        private void CalcuateParent(int prizeType, int userId, decimal sumMonery, TradeType tp, string relevanceNo, double maxRebate)
        {
            this.CalcuateParent(prizeType,userId,sumMonery,tp,relevanceNo,0,maxRebate);
        }
      
        /// <summary>
        /// 获取所有上级用户,并增加/减少对应的返点
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sumMonery"></param>
        /// <param name="tp"></param>
        /// <param name="relevanceNo"></param>
        /// <param name="moneryType">0 为正数，1为负数</param>
        private void CalcuateParent(int prizeType, int userId, decimal sumMonery, TradeType tp, string relevanceNo, int moneryType, double maxRebate)
        {
            var source = this.mSysUserService.GetParentUsers(userId);
            if (null == source || source.Count() < 1)
                return;
            //计算当前用户的返点值
            var user = source.Where(item => item.Id == userId).FirstOrDefault();

            //投注用户返点
            var backMonery = this.CalculateMoneryRebate(double.Parse(sumMonery.ToString()), user.Rebate, maxRebate);
            source.Remove(user);

            var log = log4net.LogManager.GetLogger("errorLog");
            log.Error(string.Format("CalcuateParent: prizeType={0}, userId={1}, sumMonery={2},TradeType={3},relevanceNo={4},moneryType={5},maxRebate={6},backMonery={7}", prizeType, userId, sumMonery, tp, relevanceNo, moneryType, maxRebate, backMonery));

            if ((double)backMonery > Math.Round((double.Parse(sumMonery.ToString()) * (maxRebate / 100)),4))
            {
                return;
            }
            if (prizeType == 1 && backMonery > 0)
            { //用户投注，选择低奖金，则有返点
                UpdateUserBanance(userId, backMonery, tp, relevanceNo, moneryType);//修改用户余额
            }
            if (user.ParentId == null)
                return;
            //递归计算
            EachParent(source, user.ParentId.Value, Convert.ToDouble(sumMonery), backMonery, tp, relevanceNo, moneryType, maxRebate);
        }

        private void EachParent(List<SysUser> source, int parentid, double sumMonery, decimal sumBackMonery, TradeType tp, string relevanceNo, int moneryType, double maxRebate)
        {
            foreach (var item in source)
            {
                if (item.Id == parentid)
                {
                    var m = this.CalculateMoneryRebate(sumMonery, item.Rebate, maxRebate);
                    if ((double)m > Math.Round((double.Parse(sumMonery.ToString()) * (maxRebate / 100)),4))
                    {
                        return;
                    }
                    var itemMonery = m - sumBackMonery;
                    if (itemMonery > 0)
                        UpdateUserBanance(item.Id, itemMonery, tp, relevanceNo, moneryType);//修改用户余额
                    //继续递归
                    if (item.ParentId != null)
                        EachParent(source, item.ParentId.Value, sumMonery, (sumBackMonery + itemMonery), tp, relevanceNo, moneryType, maxRebate);
                }
            }
        }

        /// <summary>
        /// 修改用户余额
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="amt"></param>
        public void UpdateUserBanance(int uid, decimal amt, TradeType tp, string relevanceNo, int moneryType)
        {
            //修改当前用户余额

            var item = new SysUserBalanceDetail()
            {
                OpUserId = uid,
                SerialNo = "d" + Ytg.Comm.Utils.BuilderNum(),
                Status = 0,
                TradeType = tp,
                UserId = uid,
                RelevanceNo = relevanceNo
            };

            mSysUserBalanceService.UpdateUserBalance(item, moneryType == 0 ? amt : -amt);

        }

        



        /// <summary>
        /// 计算返点值
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="summonery"></param>
        /// <param name="PrizeType"></param>
        /// <param name="rebate"></param>
        /// <returns></returns>
        private decimal CalculateMoneryRebate(double summonery, double rebate, double maxRebate)
        {
            var rm=(maxRebate-rebate);
           
            //计算返点钱
            return Math.Round( Convert.ToDecimal(summonery * (rm / 100)),4);
        }

        #endregion 
    }
}
