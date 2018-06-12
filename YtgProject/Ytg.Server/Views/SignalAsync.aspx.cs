using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.Views
{
    public partial class SignalAsync : BasePage
    {
        public string ContentStr = "";

        public string isThree = "k3";

        private int topValue = 30;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                this.Inint();
            }
        }

        private void Inint()
        {
            int lid = 1;
            if (!int.TryParse(Request.QueryString["lotteryid"], out lid))
            {
                Response.End();
                return;
            }
          
            if(!int.TryParse(Request.QueryString["issuecount"],out topValue))
                topValue=30;

            string btimes = Request.QueryString["starttime"];
            string etimes = Request.QueryString["endtime"];

            DateTime? outBegin=null;
            DateTime? outEnd=null;
            DateTime begin;
            DateTime end;
            if (DateTime.TryParse(btimes, out begin))
                outBegin = begin;
            if (DateTime.TryParse(etimes, out end))
                outEnd = end;

            ILotteryIssueService issueService = IoC.Resolve<ILotteryIssueService>();
            var result = issueService.GetHisIssue(lid, topValue, outBegin, outEnd).OrderBy(x => x.EndTime);


            StringBuilder builer = new StringBuilder();

            //构建head
            int minNum=0;
            int maxNum=6;
            string testOpenResult = "0,1,2,3,4";
            
            switch (lid)
            {
                case 22://快三
                    minNum = 1; maxNum = 6;
                    testOpenResult = "3,4,5";
                    isThree = "k3";
                    break;
                case 8://上海时时乐
                case 7://福彩3d
               // case 9://排列3/5
                    minNum = 0;
                    maxNum = 9;
                    testOpenResult = "3,4,5";
                    isThree = "ssl";
                    break;
                case 20://十一选5
                case 19:
                case 18:
                case 17:
                case 6:
                    minNum = 1;
                    maxNum = 11;
                    isThree = "11x5";
                    break;
                case 26:
                    testOpenResult = "01,02,03,04,05,06,07,08,09,10";
                    minNum = 1;
                    maxNum = 10;
                    isThree = "pk10";
                    break;
                default:
                    minNum = 0;
                    maxNum = 9;
                    isThree="ssc";
                    break;
            }
            if (lid == 26) {
                this.BuilderPk10(builer,testOpenResult,result);
                this.ContentStr = builer.ToString();
                return  ;
            }

            BuilderHead(builer, testOpenResult, minNum, maxNum);//头部
            BuilderTitle(builer, testOpenResult, minNum, maxNum);//num
            List<ResultEntity> statResult = new List<ResultEntity>();
            
            foreach (var res in result)
            {
                if (!string.IsNullOrEmpty(res.Result))
                {
                    var repResult = res.Result.Split(',').Select(x=>Convert.ToInt32(x)).ToArray();
                    int resLen = repResult.Length;
                    ResultEntity entity = new ResultEntity();
                    entity._1 = repResult[0];
                    entity._2 = repResult[1];
                    entity._3 = repResult[2];
                    if (resLen == 5)
                    {
                        //开奖数字为5位数
                        entity._4 = repResult[3];
                        entity._5 = repResult[4];
                    }
                    else
                    { //开奖数为3位数
                        entity._4 = -1;
                        entity._5 = -1;
                    }
                    statResult.Add(entity);
                }
                BuilderContent(res.IssueCode, res.Result, builer, minNum, maxNum);
            }
            BuilderAvgNum(testOpenResult,minNum,maxNum, builer, statResult);//出现次数
            BuilderTitle(builer, testOpenResult, minNum, maxNum,false);//num
            BuilderHead(builer, testOpenResult, minNum, maxNum,false);//头部
            this.ContentStr = builer.ToString();
        }

        #region 构建Pk10
        /// <summary>
        /// 构建头部
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="minNum"></param>
        /// <param name="maxNum"></param>
        private void BuilderPk10(StringBuilder builder, string openResult, IOrderedEnumerable<LotteryIssue> result)
        {
            int len = openResult.Split(',').Length;
            int colSpan = 10;

            builder.Append("<tr id=\"title\">");
            builder.Append("<td rowspan=\"2\"><strong>期号</strong></td>");
            builder.Append("<td rowspan=\"2\" colspan=\"" + colSpan + "\" class=\"redtext\"><strong>开奖号码</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>冠军</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>亚军</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>季军</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第四名</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第五名</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第六名</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第七名</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第八名</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第九名</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第十名</strong></td>");
            builder.Append("</tr>");

            builder.Append("<tr id=\"head\">");
           // builder.Append("<td rowspan=\"2\"><strong>期号</strong></td>");
           // builder.Append("<td rowspan=\"2\" colspan=\"" + len + "\" class=\"redtext\"><strong>开奖号码</strong></td>");
            for (var j = 0; j < len; j++)
            {
                for (var i = 1; i <= 10; i++)
                {
                    builder.Append("<td class=\"wdh\" align=\"center\"><strong>" + i + "</strong></td>");
                }
            }
            builder.Append("</tr>");

            List<ResultEntity> statResult = new List<ResultEntity>();

            foreach (var res in result)
            {
                if (!string.IsNullOrEmpty(res.Result))
                {
                    var repResult = res.Result.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                    int resLen = repResult.Length;
                    ResultEntity entity = new ResultEntity();
                    entity._1 = repResult[0];
                    entity._2 = repResult[1];
                    entity._3 = repResult[2];
                    if (resLen == 5)
                    {
                        //开奖数字为5位数
                        entity._4 = repResult[3];
                        entity._5 = repResult[4];
                    }
                    else if (resLen == 10) {
                        entity._4 = repResult[3];
                        entity._5 = repResult[4];
                        entity._6 = repResult[5];
                        entity._7 = repResult[6];
                        entity._8 = repResult[7];
                        entity._9 = repResult[8];
                        entity._10 = repResult[9];
                    }
                    else
                    { //开奖数为3位数
                        entity._4 = -1;
                        entity._5 = -1;
                    }
                    statResult.Add(entity);
                }
                BuilderContent(res.IssueCode, res.Result, builder, 1, 10);
            }

            /**平均遗漏*/
            int rowSpan = len;
            StringBuilder yilouBuilder = new StringBuilder();
            builder.Append("<tr>");
            builder.Append("<td nowrap>出现总次数</td>");
            builder.Append("<td align=\"center\" colspan=\"" + rowSpan + "\">&nbsp;</td>");

            var opens = statResult;
            var fs = opens.FirstOrDefault();
            if (fs != null)
            {
                //万位
                var _1 = opens.GroupBy(x => x._1).Select(v => new ResultEntity
                {
                    _1 = v.Key,
                    _2 = v.Count()
                });
                BuilderAvgItem(1, 10, _1, builder, yilouBuilder);
                //千位
                var _2 = opens.GroupBy(x => x._2).Select(v => new ResultEntity
                {
                    _1 = v.Key,
                    _2 = v.Count()
                });
                BuilderAvgItem(1, 10, _2, builder, yilouBuilder);
                //百位
                var _3 = opens.GroupBy(x => x._3).Select(v => new ResultEntity
                {
                    _1 = v.Key,
                    _2 = v.Count()
                });
                BuilderAvgItem(1, 10, _3, builder, yilouBuilder);

                //5位开奖数字 十位
                var _4 = opens.GroupBy(x => x._4).Select(v => new ResultEntity
                {
                    _1 = v.Key,
                    _2 = v.Count()
                });
                BuilderAvgItem(1, 10, _4, builder, yilouBuilder);
                //个位
                var _5 = opens.GroupBy(x => x._5).Select(v => new ResultEntity
                {
                    _1 = v.Key,
                    _2 = v.Count()
                });
                BuilderAvgItem(1, 10, _5, builder, yilouBuilder);
                //
                var _6 = opens.GroupBy(x => x._6).Select(v => new ResultEntity
                {
                    _1 = v.Key,
                    _2 = v.Count()
                });
                BuilderAvgItem(1, 10, _6, builder, yilouBuilder);

                //
                var _7 = opens.GroupBy(x => x._7).Select(v => new ResultEntity
                {
                    _1 = v.Key,
                    _2 = v.Count()
                });
                BuilderAvgItem(1, 10, _7, builder, yilouBuilder);
                //
                var _8 = opens.GroupBy(x => x._8).Select(v => new ResultEntity
                {
                    _1 = v.Key,
                    _2 = v.Count()
                });
                BuilderAvgItem(1, 10, _8, builder, yilouBuilder);

                //
                var _9 = opens.GroupBy(x => x._9).Select(v => new ResultEntity
                {
                    _1 = v.Key,
                    _2 = v.Count()
                });
                BuilderAvgItem(1, 10, _9, builder, yilouBuilder);

                //
                var _10 = opens.GroupBy(x => x._10).Select(v => new ResultEntity
                {
                    _1 = v.Key,
                    _2 = v.Count()
                });
                BuilderAvgItem(1, 10, _10, builder, yilouBuilder);
            }
            builder.Append("</tr>");
            /**平均遗漏*/
            builder.Append("<tr>");
            builder.Append("<td nowrap>平均遗漏</td>");
            builder.Append("<td align=\"center\" colspan=\"" + rowSpan + "\">&nbsp;</td>");
            builder.Append(yilouBuilder.ToString());
            builder.Append("</tr>");
            /**平均遗漏*/


            BuilderTitle(builder, openResult, 1, 10, false);//num
            //BuilderHead(builder, openResult, 1, 10, false);//头部

            builder.Append("<tr id=\"title\">");
           // builder.Append("<td rowspan=\"2\"><strong>期号</strong></td>");
           // builder.Append("<td rowspan=\"2\" colspan=\"" + colSpan + "\" class=\"redtext\"><strong>开奖号码</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>冠军</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>亚军</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>季军</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第四名</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第五名</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第六名</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第七名</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第八名</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第九名</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>第十名</strong></td>");
            builder.Append("</tr>");
        }
        #endregion



        /// <summary>
        /// 构建头部
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="minNum"></param>
        /// <param name="maxNum"></param>
        private void BuilderHead(StringBuilder builder,string openResult,int minNum,int maxNum,bool isHead=true)
        {
            int len = openResult.Split(',').Length;
            int colSpan = minNum == 0 ? maxNum + 1 : maxNum;

            builder.Append("<tr id=\"title\">");
            if (isHead)
            {
                builder.Append("<td rowspan=\"2\"><strong>期号</strong></td>");
                builder.Append("<td rowspan=\"2\" colspan=\"" + len + "\" class=\"redtext\"><strong>开奖号码</strong></td>");
            }
            if (len == 5)
            {
                builder.Append("<td colspan=\"" + colSpan + "\"><strong>万位</strong></td>");
                builder.Append("<td colspan=\"" + colSpan + "\"><strong>千位</strong></td>");
            }
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>百位</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>十位</strong></td>");
            builder.Append("<td colspan=\"" + colSpan + "\"><strong>个位</strong></td>");
            builder.Append("</tr>");

            

            
        }

        private static void BuilderTitle(StringBuilder builder, string openResult, int minNum, int maxNum,bool isHead=true)
        {
            int len = openResult.Split(',').Length;
            builder.Append("<tr id=\"head\">");
            if (!isHead)
            {
                builder.Append("<td rowspan=\"2\"><strong>期号</strong></td>");
                builder.Append("<td rowspan=\"2\" colspan=\"" + len + "\" class=\"redtext\"><strong>开奖号码</strong></td>");
            }
            for(var j=0;j<len;j++)
            {
                for (var i = minNum; i <= maxNum; i++)
                {
                    builder.Append("<td class=\"wdh\" align=\"center\"><strong>" + i + "</strong></td>");
                }
            }
            builder.Append("</tr>");
        }


        /// <summary>
        /// 构建百 十 个 位 遗漏
        /// </summary>
        private void BuilderContent(string issue, string openResult, StringBuilder builer, int minNum = 1, int MaxNum = 6)
        {
           
            builer.Append("<tr>");

            builer.Append("<td id=\"title\">" + issue + "</td>");
            /**构建开奖号码*/
            if (!string.IsNullOrEmpty(openResult))
            {
                var openResultArray = openResult.Split(',');
                foreach (var o in openResultArray)
                {
                    builer.Append("<td class=\"wdh\" align=\"center\">");
                    builer.Append("<div class=\"ball02\">" + o + "</div>");
                    builer.Append("</td>");
                }
                /**构建开奖号码 end*/
                int sty = 0;
                foreach (var item in openResultArray)
                {
                    int res = Convert.ToInt32(item.ToString());
                    //根据最大值和最小值生成td
                    for (var i = minNum; i <= MaxNum; i++)
                    {
                        string showValue = res == i ? res.ToString() : "&nbsp;";
                        if (sty == 0)
                        {
                            BuilderBall01(showValue, builer);
                            if (showValue != "&nbsp;")
                                sty = 1;
                        }
                        else
                        {
                            this.BuilderBall02(showValue, builer);
                            if (showValue != "&nbsp;")
                                sty = 0;
                        }
                    }
                }
            }
                /**构建*/
            

            builer.Append("</tr>");

        }

        private void BuilderBall01(string value, StringBuilder builer)
        {
            string className = value == "&nbsp;" ? "ball03" : "ball01";
            this.BuilderBall(className, value, builer);
        }

        private void BuilderBall02(string value, StringBuilder builer)
        {
            string className = value == "&nbsp;" ? "ball04" : "ball02";
            this.BuilderBall(className, value, builer);
        }

        private void BuilderBall(string className, string value, StringBuilder builer)
        {
            string tdClass = (className == "ball01" || className == "ball02") ? "charball" : "wdh";
            builer.Append("<td class=\"" + tdClass + "\" align=\"center\">");
            builer.Append("<div class=\"" + className + "\">" + value + "</div>");
            builer.Append("</td>");
        }

        /// <summary>
        /// 平均遗漏
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        private void BuilderAvgNum(string openResult,int minVal,int maxVal,StringBuilder builder, List<ResultEntity> opens)
        {
            int rowSpan= openResult.Split(',').Length;
            StringBuilder yilouBuilder = new StringBuilder();
            builder.Append("<tr>");
            builder.Append("<td nowrap>出现总次数</td>");
            builder.Append("<td align=\"center\" colspan=\"" + rowSpan + "\">&nbsp;</td>");
           
            var fs = opens.FirstOrDefault();
            if (fs != null)
            {
                //万位
                var _1 = opens.GroupBy(x => x._1).Select(v => new ResultEntity
                {
                    _1 = v.Key,
                    _2 = v.Count()
                });
                BuilderAvgItem(minVal, maxVal, _1, builder, yilouBuilder);
                //千位
                var _2 = opens.GroupBy(x => x._2).Select(v => new ResultEntity
                {
                    _1 = v.Key,
                    _2 = v.Count()
                });
                BuilderAvgItem(minVal, maxVal, _2, builder, yilouBuilder);
                //百位
                var _3 = opens.GroupBy(x => x._3).Select(v => new ResultEntity
                {
                    _1 = v.Key,
                    _2 = v.Count()
                });
                BuilderAvgItem(minVal, maxVal, _3, builder, yilouBuilder);
                if (fs._4 != -1 || fs._5 != -1)
                {
                    //5位开奖数字 十位
                    var _4 = opens.GroupBy(x => x._4).Select(v => new ResultEntity
                    {
                        _1 = v.Key,
                        _2 = v.Count()
                    });
                    BuilderAvgItem(minVal, maxVal, _4, builder, yilouBuilder);
                    //个位
                    var _5 = opens.GroupBy(x => x._5).Select(v => new ResultEntity
                    {
                        _1 = v.Key,
                        _2 = v.Count()
                    });
                    BuilderAvgItem(minVal, maxVal, _5, builder, yilouBuilder);
                }


            }
             builder.Append("</tr>");
            /**平均遗漏*/
             builder.Append("<tr>");
             builder.Append("<td nowrap>平均遗漏</td>");
             builder.Append("<td align=\"center\" colspan=\"" + rowSpan + "\">&nbsp;</td>");
             builder.Append(yilouBuilder.ToString());
             builder.Append("</tr>");
            /**平均遗漏*/
        }

        /// <summary>
        /// 构建平均项
        /// </summary>
        /// <param name="len"></param>
        /// <param name="value"></param>
        private void BuilderAvgItem(int minVal, int len, IEnumerable<ResultEntity> value, StringBuilder builder,StringBuilder yilou)
        {
            for (var i = minVal; i <= len; i++)
            {
                var fs= value.Where(x => x._1 == i).FirstOrDefault();
                int showValue = 0;
                if (fs != null)
                    showValue = fs._2;
                builder.Append("<td align=\"center\">" + (showValue.ToString()) + "</td>");

                yilou.Append("<td align=\"center\"><script>document.write(" + showValue + " > 0 ? Math.floor(" + topValue + " / " + showValue + ") : " + topValue + " + 1);</script></td>");
            }
        }

    }

    public class ResultEntity
    {
        /// <summary>
        /// 万
        /// </summary>
        public int _1;
        /// <summary>
        /// 千
        /// </summary>
        public int _2;
        /// <summary>
        /// 百
        /// </summary>
        public int _3;
        /// <summary>
        /// 十
        /// </summary>
        public int _4;
        /// <summary>
        /// 个
        /// </summary>
        public int _5;

        /// <summary>
        /// 个
        /// </summary>
        public int _6;

        /// <summary>
        /// 个
        /// </summary>
        public int _7;

        /// <summary>
        /// 个
        /// </summary>
        public int _8;

        /// <summary>
        /// 个
        /// </summary>
        public int _9;

        /// <summary>
        /// 个
        /// </summary>
        public int _10;

    }
}
