using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.ServerWeb.Views.Activity.YongJin;

namespace Ytg.ServerWeb.Page.Bank
{
    /// <summary>
    /// 充值，佣金配置类
    /// </summary>
    public class RechargeConfig
    {
        static string mRechargeSwitch = System.Configuration.ConfigurationManager.AppSettings["RechargeSwitch"];//1为开启
        static string[] mRechargeValue = System.Configuration.ConfigurationManager.AppSettings["RechargeValue"].Split(',');

        static string mCommissionSwitch = System.Configuration.ConfigurationManager.AppSettings["CommissionSwitch"];//1为开启
        static string[] mCommissionValue = System.Configuration.ConfigurationManager.AppSettings["CommissionValue"].Split(',');
        

        static string mLotterySwitch = System.Configuration.ConfigurationManager.AppSettings["LotterySwitch"];//1为开启
        static string mLotteryValue = System.Configuration.ConfigurationManager.AppSettings["LotteryValue"];//投注量多少允许一次抽奖
        static string[] mLotteryLevel = System.Configuration.ConfigurationManager.AppSettings["LotteryLevel"].Split(',');//奖金级别
        

        /// <summary>
        /// 返回附加金币
        /// </summary>
        /// <param name="rechare"></param>
        /// <returns></returns>
        public static decimal AppendMonery(decimal rechare)
        {
            if (mRechargeSwitch == "0")
                return 0;
            foreach (var item in mRechargeValue)
            {
                var subItem = item.Split('|');
                if (rechare >= Convert.ToDecimal(subItem[0]))
                    return Convert.ToDecimal(subItem[1]);
            }

            return 0;
        }

        /// <summary>
        /// 返回投注，领取金额
        /// </summary>
        /// <param name="bettingMonery">当天下级投注金额</param>
        /// <param name="level">当前代理用户级别</param>
        /// <returns></returns>
        public static decimal CommissionsMonery(decimal? bettingMonery,int level,List<MessageEntity> messageEntity)
        {
           
            if (bettingMonery == null)
                return 0;
            if (mCommissionSwitch == "0")
                return 0;
            foreach (var item in mCommissionValue)
            {
                var subItem = item.Split('|');
                var decm= Convert.ToDecimal(subItem[0]);
                if (bettingMonery <=decm)
                {
                    var fst=messageEntity.Where(x=>x.monery==decm && x.ProxyLevel==level).FirstOrDefault();
                    if (fst != null)
                    {
                        fst.Count++;
                    }
                    else
                    {
                        messageEntity.Add(new MessageEntity()
                        {
                            Count = 1,
                            monery = decm,
                            ProxyLevel = level
                        });
                    }

                    return Convert.ToDecimal(subItem[level]);
                }
            }

            return 0;
        }

        /// <summary>
        /// 获取大转盘，多少钱允许抽奖一次
        /// </summary>
        /// <returns></returns>
        public static decimal LotteryMonery()
        {
            if (mLotterySwitch == "0")
                return -1;
            return Convert.ToDecimal(mLotteryValue);
        }

        /// <summary>
        /// 打转盘奖金级别对应的奖金
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static decimal LotteryLevelMonery(int level)
        {
            return Convert.ToDecimal(mLotteryLevel[level - 1]);
        }
    }
}