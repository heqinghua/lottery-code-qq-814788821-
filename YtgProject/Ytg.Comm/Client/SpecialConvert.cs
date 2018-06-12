using System;
using System.Net;
using System.Windows;
namespace Ytg.Comm
{
    public class SpecialConvert
    {
        /// <summary>
        /// 将特殊数字转换为友好字符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertTo(int value)
        {
            string numValue = "";
            switch (value)
            {
                case Global.BaoZi:
                    numValue = "豹子";
                    break;
                case Global.DuiZi:
                    numValue = "对子";
                    break;
                case Global.ShunZi:
                    numValue = "顺子";
                    break;
                case Global.Da:
                    numValue = "大";
                    break;
                case Global.Xiao:
                    numValue = "小";
                    break;
                case Global.Dan:
                    numValue = "单";
                    break;
                case Global.Shuang:
                    numValue = "双";
                    break;
                case Global.WuDanLinShuang:
                    numValue = "5单0双";
                    break;
                case Global.SiDanYiShuang:
                    numValue = "4单1双";
                    break;
                case Global.SanDanErShuang:
                    numValue = "3单2双";
                    break;
                case Global.ErDanSanShuang:
                    numValue = "2单3双";
                    break;
                case Global.YiDanSiShuang:
                    numValue = "1单4双";
                    break;
                case Global.LinDanWuShuang:
                    numValue = "0单5双";
                    break;
                case Global.Shang :
                    numValue = "上";
                    break;
                case Global.Zhong:
                    numValue = "中";
                    break;
                case Global.Xia:
                    numValue = "下";
                    break;
                case Global.Ji:
                    numValue = "奇";
                    break;
                case Global.He:
                    numValue = "和";
                    break;
                case Global.Or:
                    numValue = "偶";
                    break;
                case Global.DaDan :
                    numValue = "大.单";
                    break;
                case Global.DaShuang:
                    numValue = "大.双";
                    break;
                case Global.XiaoDan:
                    numValue = "小.单";
                    break;
                case Global.XiaoShuang:
                    numValue = "小.双";
                    break;
            }
            return numValue;
        }

        /// <summary>
        /// 将友好字符转换为数字
        /// </summary>
        /// <param name="valueStr"></param>
        /// <returns></returns>
        public static int ConvertBack(string  valueStr)
        {
            int mNumValue=-1;
            switch (valueStr)
            {
                case "豹子":
                    mNumValue = Global.BaoZi;
                    break;
                case "对子":
                    mNumValue = Global.DuiZi;
                    break;
                case "顺子":
                    mNumValue = Global.ShunZi;
                    break;
                case "大":
                    mNumValue = Global.Da;
                    break;
                case "小":
                    mNumValue = Global.Xiao;
                    break;
                case "单":
                    mNumValue = Global.Dan;
                    break;
                case "双":
                    mNumValue = Global.Shuang;
                    break;
                case "5单0双":
                    mNumValue = Global.WuDanLinShuang;
                    break;
                case "4单1双":
                    mNumValue = Global.SiDanYiShuang;
                    break;
                case "3单2双":
                    mNumValue = Global.SanDanErShuang;
                    break;
                case "2单3双":
                    mNumValue = Global.ErDanSanShuang;
                    break;
                case "1单4双":
                    mNumValue = Global.YiDanSiShuang;
                    break;
                case "0单5双":
                    mNumValue = Global.LinDanWuShuang;
                    break;
                case "上":
                    mNumValue = Global.Shang;
                    break;
                case "中":
                    mNumValue = Global.Zhong;
                    break;
                case "下":
                    mNumValue = Global.Xia;
                    break;
                case "奇":
                    mNumValue = Global.Ji;
                    break;
                case "和":
                    mNumValue = Global.He;
                    break;
                case "偶":
                    mNumValue = Global.Or;
                    break;
                case "大.单":
                    mNumValue = Global.DaDan;
                    break;
                case "大.双":
                    mNumValue = Global.DaShuang;
                    break;
                case "小.单":
                    mNumValue = Global.XiaoDan;
                    break;
                case "小.双":
                    mNumValue = Global.XiaoShuang;
                    break;
            }

            return mNumValue;
        }


        public static string ShowBetContent(string betContent)
        {
            string newContent = string.Empty;
            if (!string.IsNullOrEmpty(betContent) && betContent.IndexOf("-") != -1)
            {
                var array = betContent.Split(',');

                foreach (var s in array)
                {
                    var sArray = s.Split('-');
                    foreach (var sv in sArray)
                    {
                        if (string.IsNullOrEmpty(sv)) continue;
                        newContent +=SpecialConvert.ConvertTo(Convert.ToInt32("-" + sv));
                        newContent += ",";
                    }
                   
                }
                if (newContent.EndsWith(","))
                    newContent = newContent.Substring(0, newContent.Length - 1);
            }
            else {
                newContent = betContent;
            }

            return newContent;
        }


    }
}
