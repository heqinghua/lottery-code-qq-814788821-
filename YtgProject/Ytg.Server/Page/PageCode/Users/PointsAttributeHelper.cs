using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel.LotteryBasic.DTO;

namespace Ytg.ServerWeb.Page.PageCode.Users
{
    public class PointsAttributeHelper
    {
        private static List<AddPointsAttribute> addPointsAttributes=null;//缓存集合

        /// <summary>
        /// 获取销售额升点配置信息集合
        /// </summary>
        /// <returns></returns>
        public static List<AddPointsAttribute> AddPointsAttributes()
        {
            if (null == addPointsAttributes)
            {
                string mFileName = System.Web.HttpContext.Current.Server.MapPath("/xml/AddPointsAttribute.xml");
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
                    var sSCAttribute = nameItem.Attribute("SSC");
                    var elevenAttribute = nameItem.Attribute("Eleven");
                    var tdAttribute = nameItem.Attribute("Td");
                    var targer = nameItem.Attribute("targer");

                    AddPointsAttribute addPoints = new AddPointsAttribute();
                    if (targer != null)
                        addPoints.targer = Convert.ToDouble(targer.Value);
                    addPoints.Day = day;
                    addPoints.Title = title;
                    addPoints.SalesStand = salesStandAttribute.Value;
                    addPoints.SSC = sSCAttribute.Value;
                    addPoints.Eleven = elevenAttribute.Value;
                    addPoints.Td = tdAttribute.Value;
                    addPointsAttributes.Add(addPoints);
                }
            }
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
            salesStandStr=salesStandStr.Replace("W", "");
            return decimal.Parse(salesStandStr + "0000");
        }


        /// <summary>
        /// 根据销售天数和返点获取规则
        /// </summary>
        /// <param name="day"></param>
        /// <param name="rebate"></param>
        /// <returns></returns>
        public static List<AddPointsAttribute> GetDayResult(int day,double rebate)
        {
            var result= AddPointsAttributes();
            return result.Where(x=>x.Day==day.ToString() && x.targer>=rebate).ToList();
        }
        
        /// <summary>
        /// 将返点字符转换为double
        /// </summary>
        /// <returns></returns>
        public static double ParseRemo(string rmoStr)
        {
            //5.8(1778)
            if (string.IsNullOrEmpty(rmoStr))
                return 0.0;
            var val= rmoStr.Substring(0,rmoStr.IndexOf('('));
            return double.Parse(val);
        }

        /// <summary>
        /// 根据团队销量找寻配对的升点信息(重庆时时彩)
        /// </summary>
        /// <param name="groupSales"></param>
        /// <returns></returns>
        public static List<AddPointsAttribute> FindNowPoints(int day, decimal groupSales)
        {
            var source = AddPointsAttributes().Where(item => item.Day == day.ToString()).ToArray();
            groupSales = Math.Abs(groupSales);
            List<AddPointsAttribute> returns = new List<AddPointsAttribute>();
            for (var i = 0; i < source.Count(); i++)
            {

                var curItem = source[i];
                decimal sValue = ParseSalesStand(curItem.SalesStand);
                if (groupSales >= sValue)
                    returns.Add(curItem);
            }

            return returns;
        }

        /// <summary>
        /// 根据返点查询数据
        /// </summary>
        /// <param name="day"></param>
        /// <param name="targer"></param>
        /// <returns></returns>
        public static AddPointsAttribute FindNoPoints(int day, double targer)
        {
            return  AddPointsAttributes().Where(item => item.Day == day.ToString() && item.targer==targer).FirstOrDefault();
        }


    }
}