using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel.LotteryBasic.DTO;

namespace Ytg.ServerWeb.Page.PageCode.Users
{
    public class NoRecoveryAttributeHelper
    {
        private static List<AddPointsAttribute> addPointsAttributes = null;//缓存集合

        /// <summary>
        /// 获取销售额升点配置信息集合
        /// </summary>
        /// <returns></returns>
        public static List<AddPointsAttribute> AddPointsAttributes()
        {
            if (null == addPointsAttributes)
            {
                string mFileName = System.Web.HttpContext.Current.Server.MapPath("/xml/NoRecovery.xml");
                System.Xml.Linq.XDocument xdocument = System.Xml.Linq.XDocument.Load(mFileName);
                Analysis(xdocument);
            }

            return addPointsAttributes;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="document"></param>
        private static void Analysis(System.Xml.Linq.XDocument document)
        {
            //解析普通配置项 xml: Mobile
            var element = document.Element("Items");
            if (null == element)
                return;
            var folders = element.Elements("Item");
            addPointsAttributes = new List<AddPointsAttribute>();

            foreach (var item in folders)
            {
                var folderNameAttribute = item.Attribute("Day");
                var titleAttribute = item.Attribute("Title");

                string day = folderNameAttribute.Value;
                var title = titleAttribute.Value;

                var nameItems = item.Elements("Children");
                foreach (var nameItem in nameItems)
                {
                    var salesStandAttribute = nameItem.Attribute("SalesStand");
                    var targer = nameItem.Attribute("targer");

                    AddPointsAttribute addPoints = new AddPointsAttribute();
                    if (targer != null)
                        addPoints.targer = Convert.ToDouble(targer.Value);
                    addPoints.Day = day;
                    addPoints.Title = title;
                    addPoints.SalesStand = salesStandAttribute.Value;
                    addPointsAttributes.Add(addPoints);
                }
            }
        }

        /// <summary>
        /// 根据用户返点获取相关值  是否允许降点
        /// </summary>
        /// <param name="rebate"></param>
        /// <returns></returns>
        public static bool GetPointsAttribuyes(double rebate,decimal groupAmt)
        {
            var fst= AddPointsAttributes().Where(item => item.targer==rebate).FirstOrDefault();
            if (fst == null)
                return true;
            if (Math.Abs(groupAmt) >= ParseSalesStand(fst.SalesStand))
                return false;
            return true;
        }


        /// <summary>
        /// 将量标准字符转换为decimal
        /// </summary>
        /// <param name="salesStandStr"></param>
        /// <returns></returns>
        public static decimal ParseSalesStand(string salesStandStr)
        {//SalesStand="5W"
            if (string.IsNullOrEmpty(salesStandStr))
                return decimal.Zero;
            salesStandStr = salesStandStr.Replace("W", "");
            int sp = 1;
            if (salesStandStr.IndexOf(".") != -1)
            {
                salesStandStr = salesStandStr.Replace(".", "");
                sp = 10;
            }
            return decimal.Parse(salesStandStr + "0000")/sp;
        }

    }
}