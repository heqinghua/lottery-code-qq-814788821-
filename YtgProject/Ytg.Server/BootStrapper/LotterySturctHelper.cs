using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Ytg.ServerWeb.BootStrapper
{
    /// <summary>
    /// 采种结构数据组织
    /// </summary>
    public class LotterySturctHelper
    {
        /// <summary>
        /// 玩法大类
        /// </summary>
        static List<PlayType> mPlayTypes=null;

        //玩法二类
        static List<PlayTypeNum> mPlayTypeNums = null;

        //玩法奖金
        static List<PlayTypeRadio> mPlayTypeRadio = null;

        //玩法特殊奖金
        static List<PlayTypeRadiosBonus> mPlayTypeRadiosBonus = null;

        //玩法辅助
        static Dictionary<int[], RadioHelper> mRadioHelpers = null;

        //玩法VW
        static List<Lottery_VwDTO> mLottery_VwDTO = null;

        static string LiuHeCaiCode = "hk6";//六合彩CODE

        static string spLevel = System.Configuration.ConfigurationManager.AppSettings["spLevel"];//六合彩特码倍数

        //定位胆
        static int[] dingweidanArray = new int[] { 1000, 1001, 1002, 1003, 1004, 1009, 1006, 1007, 1005, 1397, 1497, 1597, 1697, 1797, 1910, 1873, 1836, 1799, 2014, 2112, 2210, 7260 };

        #region 布局数据模板原始格式
        /*
        const string LayoutTempLate = @"{isnew: '0', isdefault: '0', title:'五星', 
      label: [{gtitle: '五星直选', 
        label: [{
            methoddesc: '从万位、千位、百位、十位、个位中选择一个5位数号码组成一注',
            methodhelp: '从万位、千位、百位、十位、个位中选择一个5位数号码组成一注，所选号码与开奖号码全部相同，且顺序一致，即为中奖。',
            methodexample: '投注方案：23456；<br />开奖号码：23456，<br />即中五星直选',
            selectarea: {
                type: 'digital',
                layout: [
                    { title: '万位', no: '0|1|2|3|4|5|6|7|8|9', place: 0, cols: 1 },
                    { title: '千位', no: '0|1|2|3|4|5|6|7|8|9', place: 1, cols: 1 },
                    { title: '百位', no: '0|1|2|3|4|5|6|7|8|9', place: 2, cols: 1 },
                    { title: '十位', no: '0|1|2|3|4|5|6|7|8|9', place: 3, cols: 1 },
                    { title: '个位', no: '0|1|2|3|4|5|6|7|8|9', place: 4, cols: 1 }
                ],
                noBigIndex: 5,
                isButton: true
            },
            show_str: 'X,X,X,X,X',
            code_sp: '',
            methodid: 2274,
            name: '复式',
            prize: { 1: '180000.00' },
            nfdprize: { levs: '190000', defaultprize: 180000.00, userdiffpoint: 5 },
            modes: [{ modeid: 1, name: '元', rate: 1 }, { modeid: 2, name: '角', rate: 0.1 }, { modeid: 3, name: '分', rate: 0.01 }],
            desc: '复式', maxcodecount: 0
        }]
    }]}";*/
        #endregion

        /// <summary>
        /// 构建布局json数据对象
        /// </summary>
        /// <param name="lotteryId">彩票id</param>
        /// <param name="lotteryCode">彩票编号</param>
        /// <param name="userBackNum">用户返点级别</param>
        /// <param name="playType">用户奖金类型 0:1800 1:1700</param>
        /// <returns></returns>
        public static string Builder(int lotteryId, string lotteryCode, double userBackNum, int playType, string maxType, ref string lottery_methods)
        {
            List<PlayType> types = null;
            maxType= "-1";
            if (maxType != "-1")
                types = GetPlayType(lotteryCode).Where(p => p.GroupName == maxType).ToList();
            else
                types = GetPlayType(lotteryCode).ToList();

            if (types == null || types.Count < 1)
            {
                lottery_methods = "";
                return "";
            }
            bool isSp = lotteryCode == LiuHeCaiCode;//是否为六合彩
            bool isk3 = lotteryCode == "jsk3";//是否为江苏块3
            StringBuilder builder = new StringBuilder();
            List<int> ids = new List<int>();
            StringBuilder lotteryBuilder = new StringBuilder();

            //大类
            int maxIndex = 0;
            foreach (var item in types)
            {
                int isnew = item.IsNew ? 1 : 0;
                int isdefault = item.IsDefault ? 1 : 0;
                string title = item.PlayTypeName;

                builder.Append("{isnew: '" + isnew + "', isdefault:'" + isdefault + "', title:'" + title + "',");
                builder.Append("label: [");
                //子项
                var typeNums = GetPlayTypeNum(item.PlayCode);
                int cdIndex = 0;
                foreach (var typeNum in typeNums)
                {
                    builder.Append("{");
                    builder.Append("gtitle: '" + typeNum.PlayTypeNumName + "',");



                    builder.Append("label: [");
                    //添加玩法子项 start
                    var radios = GetPlayTypeRadio(typeNum.NumCode);
                    int radioIndex = 0;
                    foreach (var radio in radios)
                    {
                        var helper = GetRadioHelpers(radio.RadioCode);
                        if (helper == null)
                            continue;
                        if (!ids.Contains(radio.RadioCode))
                        {
                            ids.Add(radio.RadioCode);
                            lotteryBuilder.AppendFormat("{0}:'{1}',", radio.RadioCode, helper.methodid);
                            if (helper.Childrens != null)
                            {
                                foreach (var cd in helper.Childrens)
                                {

                                    foreach (var cdKey in cd.Key)
                                        lotteryBuilder.AppendFormat("{0}:'{1}',", cdKey, cd.methodid);
                                }
                            }
                        }

                        if (!isSp)
                        {
                            DefaultMeth(userBackNum, playType, builder, lotteryBuilder, radio, helper, isk3);
                            if (helper.Childrens != null)
                            {
                                foreach (var helperChildren in helper.Childrens)
                                {
                                    StringBuilder childrenBuilder = new StringBuilder();
                                    DefaultMeth(userBackNum, playType, childrenBuilder, lotteryBuilder, radio, helperChildren, isk3);
                                    builder.Append("," + childrenBuilder.ToString());
                                }
                            }
                        }
                        else
                        {
                            BuilderLiuHeCaiChildren(helper.methodid, radio.PlayTypeRadioName, radio.RadioCode.ToString(), helper.type, builder);
                        }

                        if (radioIndex != radios.Count - 1)
                            builder.Append(",");
                        radioIndex++;
                    }
                    //end
                    builder.Append("]");
                    builder.Append("}");
                    if (cdIndex != typeNums.Count() - 1)
                        builder.Append(",");
                    cdIndex++;
                }

                builder.Append("]");
                builder.Append("}");
                if (maxIndex != types.Count - 1)
                    builder.Append(",");
                maxIndex++;
            }

            string ls = lotteryBuilder.ToString();
            if (string.IsNullOrEmpty(ls))
                return string.Empty;
            lottery_methods = "{" + ls.Substring(0, ls.Length - 1) + "}";

            return builder.ToString();
        }

        private static void DefaultMeth(double userBackNum, int playType, StringBuilder builder, StringBuilder lotteryBuilder, PlayTypeRadio radio, RadioHelper helper,bool isk3=false)
        {
            builder.Append("{");
            int noBigIndex = 5;
            int maxcodecount = 0;
           

            builder.Append("methoddesc: '" + helper.methoddesc + "',");
            builder.Append("methodhelp: '" + helper.methodhelp + "',");
            builder.Append("methodexample: '" + helper.methodexample + "',");
            builder.Append("selectarea: {type: '" + helper.type + "',layout: [" + string.Join(",", helper.Layouts) + "],noBigIndex:" + noBigIndex + ",isButton: " + helper.isButton);
            if (!string.IsNullOrEmpty(helper.selPosition))
            {
                builder.Append(",'selPosition' : '" + helper.selPosition + "'");
            }
            builder.Append("},");
            builder.Append("show_str: '" + helper.show_str + "',code_sp: '" + helper.code_sp + "',methodid: " +( isk3?helper.Key[0]:radio.RadioCode) + ",name: '" + radio.PlayTypeRadioName + "',");//如果为k3的话，直接使用code
            if (!string.IsNullOrEmpty(helper.defaultposition))
            {
                builder.Append("'defaultposition' : '" + helper.defaultposition + "',");
            }
            builder.Append("prize: { 1: '" + Utils.DecimalConvert(playType == 0 ? radio.BonusBasic : radio.BonusBasic17) + "' },");
            if (!radio.IsFixed && !isk3)//非块3
                builder.Append("nfdprize:" + GetRebateInfo(playType, userBackNum, radio) + ",");
            else
                builder.Append("nfdprize:{},");
            builder.Append("modes: [{ modeid: 1, name: '元', rate: 1 }, { modeid: 2, name: '角', rate: 0.1 }, { modeid: 3, name: '分', rate: 0.01 },{ modeid: 4, name: '厘', rate: 0.001 }],");

            string desc = string.IsNullOrEmpty(helper.Desc) ? radio.PlayTypeRadioName : helper.Desc;
            builder.Append("desc: '" + desc + "', maxcodecount:" + maxcodecount + "");
            builder.Append("}");
        }

        /// <summary>
        /// 六合彩特码
        /// </summary>
        /// <param name="color"></param>
        /// <param name="num"></param>
        private static void BuilderLiuHeCaiChildren(string color, string num, string methodid, string type, StringBuilder lotteryBuilder)
        {
            string name = "特码" + num;
            lotteryBuilder.Append("{'color':'" + color + "','num':'" + num + "','type':'" + type + "',methodid : " + methodid + ",name:'" + name + "',prize:{1:'" + spLevel + "'},nfdprize:{},modes:[{modeid:1,name:'元',rate:1}],desc:'" + name + "',maxcodecount:0}");
        }


        /// <summary>
        /// 根据玩法单选获取返点信息
        /// </summary>
        /// <param name="playType">奖金类型 0:1800 1:1700</param>
        /// <param name="userRebate">用户返点</param>
        /// <param name="playRadio">玩法对象</param>
        /// <returns></returns>
        public static string GetRebateInfo(int playType, double userRebate, PlayTypeRadio playRadio)
        {
            decimal max;
            double userBackNum;
            decimal bonusBasic;
            if (playType == 0)//1800 玩法
            {
                max = TotalAmt(Convert.ToDecimal(playRadio.BonusBasic), playRadio.StepAmt, playRadio.MaxRebate - userRebate);//舍弃返点最高奖金
                userBackNum = playRadio.MaxRebate - userRebate;//用户返点
                bonusBasic = playRadio.BonusBasic;

            }
            else
            {
                max = TotalAmt(playRadio.BonusBasic17, playRadio.StepAmt1700, playRadio.MaxRebate1700 - userRebate);
                //1700 
                userBackNum = playRadio.MaxRebate1700 - userRebate;//用户返点
                bonusBasic = playRadio.BonusBasic17;
            }
            userBackNum = Math.Round(userBackNum, 1);//保留一位小数
            //if (userBackNum <= 0)
            //    return "{}";
            //else
                return "{ levs: '" + Utils.DecimalConvert(max) + "', defaultprize:" + Utils.DecimalConvert(bonusBasic) + ", userdiffpoint:" + userBackNum + "}";
        }

        private static decimal TotalAmt(decimal baseAmt, decimal stepAmt, double backNum)
        {
            if (backNum <= 0)
                return baseAmt;
            return baseAmt + (stepAmt * 10 * Math.Round((decimal)backNum,1));
        }

        #region 获取基础数据

        

        /// <summary>
        /// 获取玩法大类
        /// </summary>
        /// <returns></returns>
        static IEnumerable<PlayType> GetPlayType(string lotteryCode)
        {
            if (mPlayTypes == null)
            {
                //获取所有玩法大类
                var playTypeService = IoC.Resolve<IPlayTypeService>();
                mPlayTypes = playTypeService.GetAll().Where(c=>c.IsEnable).OrderBy(x=>x.OccDate).ToList();
            }

            return mPlayTypes.Where(c => c.LotteryCode == lotteryCode);
        }


        /// <summary>
        /// 获取玩法彩种
        /// </summary>
        /// <returns></returns>
       public static Lottery_VwDTO GetPlayType_Vw(int radioCode)
        {
            if (mLottery_VwDTO == null)
            {
                //获取所有玩法大类
                var playTypeService = IoC.Resolve<IPlayTypeRadioService>();
                mLottery_VwDTO = playTypeService.GetLottery_VwDTO();
            }

            return mLottery_VwDTO.Where(c => c.RadioCode==radioCode).FirstOrDefault();
        }

        /// <summary>
        /// 获取玩法二类
        /// </summary>
        /// <returns></returns>
        static List<PlayTypeNum> GetPlayTypeNum(int playCode)
        {
            if (mPlayTypeNums == null)
            {
                var playTypeNumService = IoC.Resolve<IPlayNumTypeService>();
                mPlayTypeNums = playTypeNumService.GetAll().Where(c => c.IsEnable).ToList();
            }
            return mPlayTypeNums.Where(c => c.PlayCode == playCode).ToList();
        }

        /// <summary>
        /// 获取玩法奖金
        /// </summary>
        /// <param name="numCode"></param>
        /// <returns></returns>
        static List<PlayTypeRadio> GetPlayTypeRadio(int numCode)
        {
            if (mPlayTypeRadio == null)
            {
                IPlayTypeRadioService playTypeRadioService = IoC.Resolve<IPlayTypeRadioService>();
                mPlayTypeRadio = playTypeRadioService.GetAll().Where(c => c.IsEnable).ToList();
            }
            return mPlayTypeRadio.Where(c=>c.NumCode==numCode).ToList();
        }

        /// <summary>
        /// 获取玩法多奖金
        /// </summary>
        /// <param name="radioCode"></param>
        /// <returns></returns>
        static IEnumerable<PlayTypeRadiosBonus> GetPlayTypeRadiosBonus(int radioCode)
        {
            if (mPlayTypeRadiosBonus == null)
            {
                IPlayTypeRadiosBonusService playTypeRadiosBonusService = IoC.Resolve<IPlayTypeRadiosBonusService>();
                mPlayTypeRadiosBonus = playTypeRadiosBonusService.GetAll().ToList();
            }
            return mPlayTypeRadiosBonus.Where(c => c.RadioCode == radioCode);
        }

        
        /// <summary>
        /// 获取所有玩法布局等信息
        /// </summary>
        /// <param name="radioNum"></param>
        /// <returns></returns>
        static RadioHelper GetRadioHelpers(int radioNum)
        {
            if (mRadioHelpers == null)
            {
                //解析xml
                string mFileName = System.Web.HttpContext.Current.Server.MapPath("/Bets/radioHelper.xml");
                System.Xml.Linq.XDocument xdocument = System.Xml.Linq.XDocument.Load(mFileName);
                GetMobileHelpers(xdocument);
            }

            var key = mRadioHelpers.Keys.Where(c => c.Contains(radioNum)).FirstOrDefault();
            if (key != null)
                return mRadioHelpers[key];

            return null;
        }

        /// <summary>
        /// 解析移动服务配置相关信息
        /// </summary>
        /// <param name="xdocument"></param>
        static void GetMobileHelpers(System.Xml.Linq.XDocument xdocument)
        {
            //解析普通配置项 xml: Mobile
            var element = xdocument.Element("items");
            if (null == element)
                return;
            var items = element.Elements("item");
            mRadioHelpers = new Dictionary<int[], RadioHelper>();
         
            foreach (var item in items)
            {
                var helperItem = new RadioHelper();
                Async(item, helperItem);

                var pattern = item.Element("pattern");
                if (pattern != null)
                {
                    helperItem.Childrens = new List<RadioHelper>();
                    var patternChildrens = pattern.Elements("item");
                    foreach (var cd in patternChildrens)
                    {
                        var cdItem = new RadioHelper();
                        Async(cd, cdItem);
                        helperItem.Childrens.Add(cdItem);
                    }
                }

                mRadioHelpers.Add(helperItem.Key, helperItem);

                
            }


        }

        private static void Async(System.Xml.Linq.XElement item, RadioHelper helperItem)
        {
            string key = item.Attribute("Code").Value;
           
            helperItem.Key = key.Split(',').Select(c => Convert.ToInt32(c.ToString())).ToArray();

            string defType = "digital";
            string defshow_str = "X,X,X,X,X";
            string defcode_sp = "";

            var type = item.Attribute("type");
            var show_str = item.Attribute("show_str");
            var code_sp = item.Attribute("code_sp");
            var methoddesc = item.Attribute("methoddesc");
            var methodhelp = item.Attribute("methodhelp");
            var methodexample = item.Attribute("methodexample");
            var methodid = item.Attribute("methodid");
            var isButton = item.Attribute("isButton");
            var selPosition = item.Attribute("selPosition");
            var defaultposition = item.Attribute("defaultposition");
            var name = item.Attribute("name");
            var desc = item.Attribute("desc");
          

            if (type != null)
                defType = type.Value;

            if (show_str != null)
                defshow_str = show_str.Value;

            if (code_sp != null)
                defcode_sp = code_sp.Value;

            helperItem.type = defType;
            helperItem.show_str = defshow_str;
            helperItem.code_sp = defcode_sp;

            helperItem.isButton = "true";
            if (isButton != null)
                helperItem.isButton = isButton.Value;
            helperItem.methodid = "0";
            if (methodid != null)
                helperItem.methodid = methodid.Value;

            if (methoddesc != null)
                helperItem.methoddesc = methoddesc.Value;
            if (methodhelp != null)
                helperItem.methodhelp = methodhelp.Value;
            if (methodexample != null)
                helperItem.methodexample = methodexample.Value;

            if (selPosition != null)
                helperItem.selPosition = selPosition.Value;
            if (defaultposition != null)
                helperItem.defaultposition = defaultposition.Value;

            if (name != null)
                helperItem.Name = name.Value;
            if(desc!=null)
                helperItem.Desc = desc.Value;

            helperItem.Layouts = new List<string>();
            var layouts = item.Elements("Layout");
            foreach (var layout in layouts)
            {
                var pageAttribute = layout.Value;
                if (!string.IsNullOrEmpty(pageAttribute))
                    helperItem.Layouts.Add(pageAttribute);
            }
        }

        #endregion
    }
}