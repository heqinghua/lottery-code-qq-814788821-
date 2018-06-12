using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.BasicModel.LotteryBasic;
using Ytg.ServerWeb.Lottery;
using Ytg.Service.Lott;

namespace Ytg.ServerWeb.Page.PageCode
{
    public static class WinMoneryHelper
    {
        static decimal mMaxReboMonery = 400000;
        static WinMoneryHelper()
        {
            mMaxReboMonery = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["maxReboMonery"]);
        }

        /// <summary>
        /// 获取允许最大可能中奖金额
        /// </summary>
        /// <returns></returns>
        public static decimal GetMaxReboMonery()
        {
            return mMaxReboMonery;
        }

        /// <summary>
        /// 可能中奖的金额
        /// </summary>
        public static decimal GetAutoWinMonery(Ytg.BasicModel.LotteryBasic.DTO.BetDetailDTO item, BettUserDto cookUserInfo, PlayTypeRadio fs)
        {
            //可能中的奖金   
            if (null == fs)
                return 99999999;

            decimal sumWinMonery = 0;
            if (!fs.HasMoreBonus)//普通奖金
            {
                //返点/舍弃返点
                sumWinMonery+=TotalWinMoney(item, 1, fs, cookUserInfo);
            }
            else
            {//拥有更多中奖方式的

                List<string> ct = new List<string>();
                if (item.BetContent.IndexOf("-") >= 0)
                {
                    var cts = item.BetContent.Split('-');
                    foreach (var c in cts)
                    {
                        if (string.IsNullOrEmpty(c))
                            continue;
                        ct.Add(Ytg.Comm.SpecialConvert.ConvertTo(Convert.ToInt32("-" + c)));
                    }
                }
                var wins = PlayTypeRadiosBonusServiceCatch.GetAll().Where(c => c.RadioCode == fs.RadioCode).ToList();
                //是否为特殊玩法
                for (var i = 0; i < wins.Count; i++)
                {
                    var itemw = wins[i];
                    if (ct.Count > 0 && !ct.Contains(itemw.BonusTitle))
                        continue;
                    decimal[] models = { 1M, 0.1M, 0.01M,0.001M };
                    sumWinMonery +=(Ytg.Comm.Global.DecimalConvert(cookUserInfo.PlayType == 0 ? itemw.BonusBasic : itemw.BonusBasic17) * models[item.Model] * (int)item.Multiple);
                }
            }

            return sumWinMonery;
        }

        /// <summary>
        /// 计算中奖金额,stepAmt * 10 * item.BackNum *10得意思是：除以0.1相当于*10
        /// </summary>
        /// <param name="item">投注详情</param>
        /// <param name="baseAmt">基础奖金</param>
        /// <param name="stepAmt">每增加0.1的返点 增加多少奖金</param>
        /// <param name="count">中多少注</param>
        /// <returns></returns>
        private static decimal TotalWinMoney(Ytg.BasicModel.LotteryBasic.DTO.BetDetailDTO item, int count, PlayTypeRadio fs, BettUserDto cookUserInfo)
        {
            decimal[] models = { 1M, 0.1M, 0.01M,0.001M };
            //计算
            decimal stepAmt = 0;
            decimal baseAmt = 0;
            //返点/舍弃返点
            if (cookUserInfo.PlayType == 0)//1800
            {
                baseAmt = fs.MaxBonus - Convert.ToDecimal(cookUserInfo.Rebate) * 10 * fs.StepAmt;
                if (item.PrizeType == 1)
                    baseAmt = fs.BonusBasic;
            }
            else
            {
                baseAmt = fs.MaxBonus17 - Convert.ToDecimal(cookUserInfo.Rebate) * 10 * fs.StepAmt1700;
                if (item.PrizeType == 1)
                    baseAmt = fs.BonusBasic17;
            }

            var total = count * (baseAmt - stepAmt) * models[item.Model] * (int)item.Multiple;

            return Math.Round(total, 4);
        }

    }
}