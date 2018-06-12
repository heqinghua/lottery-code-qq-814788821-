using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Comm
{
    public class Global
    {

        /// <summary>
        /// 豹子
        /// </summary>
        public const int BaoZi = -10;
        /// <summary>
        /// 顺子
        /// </summary>
        public const int ShunZi = -11;
        /// <summary>
        /// 对子
        /// </summary>
        public const int DuiZi = -12;

        /// <summary>
        /// 大
        /// </summary>
        public const int Da = -13;

        /// <summary>
        /// 小
        /// </summary>
        public const int Xiao = -14;

        /// <summary>
        /// 单
        /// </summary>
        public const int Dan = -15;

        /// <summary>
        /// 双
        /// </summary>
        public const int Shuang = -16;

        /// <summary>
        /// 5单0双
        /// </summary>
        public const int WuDanLinShuang = -17;
        /// <summary>
        /// 4单1双
        /// </summary>
        public const int SiDanYiShuang = -18;
        /// <summary>
        /// 3单2双
        /// </summary>
        public const int SanDanErShuang = -19;
        /// <summary>
        /// 2单3双
        /// </summary>
        public const int ErDanSanShuang = -20;

        /// <summary>
        /// 1单4双
        /// </summary>
        public const int YiDanSiShuang = -21;

        /// <summary>
        /// 0单5双
        /// </summary>
        public const int LinDanWuShuang = -22;
        /// <summary>
        /// 上
        /// </summary>
        public const int Shang = -23;
        /// <summary>
        /// 中
        /// </summary>
        public const int Zhong = -24;
        /// <summary>
        /// 下
        /// </summary>
        public const int Xia = -25;
        /// <summary>
        /// 级
        /// </summary>
        public const int Ji = -26;
        /// <summary>
        /// 和
        /// </summary>
        public const int He = -27;
        /// <summary>
        /// 偶
        /// </summary>
        public const int Or = -28;

        /// <summary>
        /// 大.单
        /// </summary>
        public const int DaDan = -29;
        /// <summary>
        /// 大.双
        /// </summary>
        public const int DaShuang = -30;
        /// <summary>
        /// 小.单
        /// </summary>
        public const int XiaoDan = -31;
        /// <summary>
        /// 小.双
        /// </summary>
        public const int XiaoShuang = -32;


        #region 奖金小数智能转换

        public static decimal DecimalConvert(decimal value)
        {
            var x = value.ToString();
            var array = x.Split('.');
            if (array.Length != 2)
                return value;
            string newv = "";
            bool isOk = false;
            foreach (var s in array[1].Reverse())
            {
                if (s != '0' || isOk)
                {
                    isOk = true;
                    newv += s;
                }
            }

            string allValue = "";
            newv.Reverse().ToList().ForEach(c => allValue += c);

            return Convert.ToDecimal(allValue == "" ? array[0] : array[0] + "." + allValue);
        }
        #endregion
    }
}
