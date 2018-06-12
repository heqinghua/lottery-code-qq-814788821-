using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Ytg.Scheduler.Comm.Bets
{
    public class RadioContentFactory
    {
        private static System.Collections.Generic.Dictionary<string, ValueEntity> mRadioConfigs = null;

        private static System.Collections.Generic.Dictionary<string, ICalculate> mCalculates = new System.Collections.Generic.Dictionary<string,ICalculate>();
        public static ICalculate CreateInstance(int radioCode)
        {
            string fullName = FindFullName(radioCode);
            if (string.IsNullOrEmpty(fullName))
                return null;

            ICalculate outCalculate;
            if (!mCalculates.TryGetValue(fullName, out outCalculate))
            {

                outCalculate = typeof(RadioContentFactory).Assembly.CreateInstance(fullName) as ICalculate;
                mCalculates.Add(fullName, outCalculate);
            }

            return outCalculate;
        }

        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <param name="radioCode"></param>
        /// <returns></returns>
        private static string FindFullName(int radioCode)
        {
            if (mRadioConfigs == null)
            {
                mRadioConfigs = new System.Collections.Generic.Dictionary<string, ValueEntity>();

                XDocument xdocument = null;
                if (System.Web.HttpContext.Current == null)
                    xdocument = XDocument.Load(@"Bets/RadioConfig.xml");
                else
                    xdocument = XDocument.Load(System.Web.HttpContext.Current.Server.MapPath("/Bets/RadioConfig.xml"));


                //解析普通配置项 xml:add
                var elements = xdocument.Element("RadioConfig").Elements("Radio");

                foreach (var item in elements)
                {
                    string key = item.Attribute("Code").Value;

                    if (!mRadioConfigs.ContainsKey(key))
                    {
                        ValueEntity entity = new ValueEntity();
                        entity.FullName = item.Attribute("FullName").Value;
                        var pos = item.Attribute("Postion");
                        var minPos = item.Attribute("MinPos");
                        var maxBetCount = item.Attribute("MaxBetCount");
                        if (pos != null)
                            entity.Postion = pos.Value;
                        if (minPos != null)
                            entity.MinPos =Convert.ToInt32(minPos.Value);
                        if (maxBetCount != null)
                            entity.MaxBetCount = Convert.ToInt32(maxBetCount.Value);
                        mRadioConfigs.Add(key, entity);
                    }
                }


            }

            string outKey = "";
            foreach (var item in mRadioConfigs.Keys)
            {
                var fs = item.Split(',').Where(k => k == radioCode.ToString()).FirstOrDefault();
                if (fs != null)
                {
                    outKey = item;
                    continue;
                }
            }

            if (string.IsNullOrEmpty(outKey))
                return null;
            return mRadioConfigs[outKey].FullName;
        }

        /// <summary>
        /// 获取该玩法是否根据位置拆分成多注
        /// </summary>
        /// <param name="radioCode"></param>
        /// <returns></returns>
        public static ValueEntity PostionLottery(int radioCode)
        {
            if (mRadioConfigs == null)
                FindFullName(radioCode);
            string outKey = "";
            foreach (var item in mRadioConfigs.Keys)
            {
                var fs = item.Split(',').Where(k => k == radioCode.ToString()).FirstOrDefault();
                if (fs != null)
                {
                    outKey = item;
                    continue;
                }
            }
            if (string.IsNullOrEmpty(outKey))
                return null;
            return mRadioConfigs[outKey];
        }

    }

    public class ValueEntity
    {
        public string FullName { get; set; }

        /// <summary>
        /// 投注内容是否需要拆分成多注
        /// </summary>
        public string Postion { get; set; }

        /// <summary>
        /// 最小位数
        /// </summary>
        public int MinPos { get; set; }

        /// <summary>
        /// 玩法最大注数
        /// </summary>
        public int MaxBetCount { get; set; }

    }
}
