using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel;

namespace Utg.ServerWeb.Admin.pages
{
    public class CommTypeHelper
    {
        public static string GetModelStr(object stat) {
            if (stat == null)
                return string.Empty;
            var modelStr = "元";
            switch (Convert.ToInt32(stat.ToString()))
            {
                case 1:
                    modelStr = "角";
                    break;
                case 2:
                    modelStr = "分";
                    break;
                case 3:
                    modelStr = "厘";
                    break;
            }

            return modelStr;
        }

        public static string GetCatchStauts(object stauts)
        {
            if (stauts == null)
                return string.Empty;
            CatchNumType tp;
            if (!Enum.TryParse<CatchNumType>(stauts.ToString(),out tp))
                return string.Empty;
            var modelStr = "正在进行";
            switch (tp)
            {
                case CatchNumType.Compled:
                    modelStr = "已完成";
                    break;
                case CatchNumType.Cancel:
                    modelStr = "已撤单";
                    break;
            }
             
            return modelStr;
        }

        public static string GetTradeTypeStr(object TradeType)
        {
            if (null == TradeType)
                return string.Empty;
            string result = "";
            switch (Convert.ToInt32(TradeType))
            {
                case 1:
                    result = "用户充值";
                    break;
                case 2:
                    result = "用户提现";
                    break;
                case 3:
                    result = "投注";
                    break;
                case 4:
                    result = "追号扣款";
                    break;
                case 5:
                    result = "追号返款";
                    break;
                case 6:
                    result = "游戏返点";
                    break;
                case 7:
                    result = "奖金派送";
                    break;
                case 8:
                    result = "撤单返款";
                    break;
                case 9:
                    result = "撤单手续费";
                    break;
                case 10:
                    result = "撤销返点";
                    break;
                case 11:
                    result = "撤销派奖";
                    break;
                case 12:
                    result = "充值扣费";
                    break;
                case 13:
                    result = "上级充值";
                    break;
                case 14:
                    result = "活动礼金";
                    break;
                case 15:
                    result = "分红";
                    break;
                case 16:
                    result = "提现失败";
                    break;
                case 17:
                    result = "撤销提现";
                    break;
                case 99:
                    result = "其他";
                    break;
                case 18:
                    result = "满赠活动";
                    break;
                case 19:
                    result = "签到有你";
                    break;
                case 20:
                    result = "注册活动";
                    break;
                case 21:
                    result = "充值活动";
                    break;
                case 22:
                    result = "佣金大返利";
                    break;
                case 23:
                    result = "幸运大转盘";
                    break;
                case 24:
                    result = "系统充值";
                    break;
                case 25:
                    result = "投注送礼包";
                    break;
                case 26:
                    result = "分红扣费";
                    break;
            }
            return result;
        }

    }
}