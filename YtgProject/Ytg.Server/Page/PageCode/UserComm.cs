using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Page.PageCode
{
    public class UserComm
    {

        #region 新用户赠送金额活动

        /// <summary>
        /// 初始化新用户账户金额
        /// </summary>
        public static void InintNewUserBalance(SysUser outuser, ISysSettingService sysSettingService,ISysUserBalanceService sysUserBalanceService,ISysUserBalanceDetailService sysUserBalanceDetailService,ISysUserService sysUserService)
        {

            #region 注册活动
            //新用户注册赠送金额
            var defaultMoneuy = 0m;//赠送金额
            var minOutBettMonery=0m;//最低提款金额
            var settingItem = sysSettingService.Where(x => x.Key == "ZCZSHD").FirstOrDefault();
            if (null != settingItem && !string.IsNullOrEmpty(settingItem.Value))
            {
                try
                {
                    var dto = Newtonsoft.Json.JsonConvert.DeserializeObject<Ytg.BasicModel.DTO.SettingDTO>(settingItem.Value);
                    if (dto != null)
                    {
                        if (dto.p1 == "0")
                        {
                            if (!decimal.TryParse(dto.p2, out defaultMoneuy)){
                                defaultMoneuy = 0;
                            }
                            if(!decimal.TryParse(dto.p3,out minOutBettMonery)){
                                minOutBettMonery=1;
                            }
                        }
                    }
                }
                catch
                {

                }
            }
            #endregion

            sysUserBalanceService.Create(new SysUserBalance()
            {
                Status = 0,
                UserAmt = defaultMoneuy,
                UserId = outuser.Id,
            });
            sysUserBalanceService.Save();
            if (defaultMoneuy > 0 && outuser.UserType!= UserType.BasicProy)
            {
                /**插入账变*/
                sysUserBalanceDetailService.Create(new SysUserBalanceDetail()
                {
                    SerialNo = "b" + Utils.BuilderNum(),
                    TradeAmt = defaultMoneuy,
                    TradeType = TradeType.注册活动,
                    UserAmt = defaultMoneuy,
                    UserId = outuser.Id,
                });
                /**插入账变*/
                sysUserBalanceDetailService.Save();
                //修改提款最低流水
                sysUserService.UpdateUserMinMinBettingMoney(outuser.Id, defaultMoneuy * minOutBettMonery);
            }
        }
        #endregion

        #region 充值流水处理逻辑

        public static bool ManagerRecharge(decimal tradeAmt,int userid)
        {
            double bili = 5;
            ISysSettingService settingService = IoC.Resolve<ISysSettingService>();
            var fs = settingService.GetAll().Where(x => x.Key == "chongzhiBili").FirstOrDefault();
            if (null != fs)
            {
                if (!double.TryParse(fs.Value, out bili))
                    bili = 5;
            }
            ISysUserService userServices = IoC.Resolve<ISysUserService>();
            ISysUserBalanceService userBalanceService = IoC.Resolve<ISysUserBalanceService>();
            var balance= userBalanceService.GetUserBalance(userid);//获取当前用户余额
            
            var minOutMonery = ((tradeAmt+ balance.UserAmt) * (decimal)(bili / 100));
            if (userServices.UpdateUserMinMinBettingMoney(userid, minOutMonery) > 0)
            {
                //更新用户提款流水要求
                return true;
            }
            return false;
        }

        #endregion

        #region 充值流水逻辑，且需减少自身流水逻辑
        public static bool ManagerRecharge(decimal tradeAmt, int userid, int rectUserId)
        {
            double bili = 5;
            ISysSettingService settingService = IoC.Resolve<ISysSettingService>();
            var fs = settingService.GetAll().Where(x => x.Key == "chongzhiBili").FirstOrDefault();
            if (null != fs)
            {
                if (!double.TryParse(fs.Value, out bili))
                    bili = 5;
            }
            ISysUserService userServices = IoC.Resolve<ISysUserService>();
            var minOutMonery = (tradeAmt * (decimal)(bili / 100));
            if (userServices.UpdateUserMinMinBettingMoney(userid, minOutMonery) > 0)
            {
                //更新用户提款流水要求
                //减少自身流水
                userServices.UpdateUserMinMinBettingMoney(rectUserId, (0 - minOutMonery));//减少投注流水
                return true;
            }
            return false;
        }
        #endregion
    }
}