using System;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;
using System.Linq;
using System.Drawing;
using System.Text.RegularExpressions;
using DinpayRSAAPI.COM.Dinpay.RsaUtils;
using Ytg.ServerWeb.Views.pay.zhifu;
using Ytg.Core.Service;
using Ytg.Comm;

namespace CSharpTestPay.wechart
{
    public partial class _Default : System.Web.UI.Page
    {
        public static string dinpayPubKey = System.Configuration.ConfigurationManager.AppSettings["merchant_public_key"];//"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCyitJIC7ZOojBCESQL+yqPEIRPS8CVMtBwmNpmaJbEg/ooTq9c1LOH3vBvWhhP9VGV9u+40jGmcGarA3KWN67sgw2Z3Qg/HxKx+wN7o4g3qWpMJfxmgvlB9A1vYFSpxrq8FK29otF+kCknWcZN4wMGzh3UDsvcG6BGYHRladiEdQIDAQAB";

        public string fileName = string.Empty;

        public string showProce = "0.0";
        protected void Page_Load(object sender, EventArgs e)
        {

         


            if (string.IsNullOrEmpty(Request.QueryString["tok"]))
            {
                Response.End();
                return;
            }
            //构建支付链接
            //根据订单唯一id获取订单信息
            IRecordTempService recordService = IoC.Resolve<IRecordTempService>();
            var item = recordService.GetAll().Where(c => c.Guid == Request.QueryString["tok"]).FirstOrDefault();
            if (null == item)
            {

                Response.End();
                return;
            }
            if (item.IsCompled || !item.IsEnable)
            {
                Response.Write("请勿重复提交订单！");
                return;
            }
            var userAmt = Math.Round(item.TradeAmt, 2).ToString();
            showProce = userAmt;
            if (userAmt.IndexOf(".") < 0)
                userAmt = userAmt + ".00";

            if (!IsPostBack)
            {
                
                try
                {
                    /////////////////////////////////接收表单提交参数////////////////////////////////////
                    ////////////////////////To receive the parameter form HTML form//////////////////////
                    
                    string interface_version = "V3.0";//Request.Form["interface_version"].ToString().Trim();
                    string service_type = "wxpay";//Request.Form["service_type"].ToString().Trim();
                    string sign_type = "RSA-S";//Request.Form["sign_type"].ToString().Trim();
                    string merchant_code = Ytg.ServerWeb.Views.pay.zhifu.ZhiFuPayConfig.Merchant_code;// Request.Form["merchant_code"].ToString().Trim();
                    string order_no = item.MY18FY;//Request.Form["order_no"].ToString().Trim();
                    string order_time = item.OccDate.ToString("yyyy-MM-dd HH:mm:ss"); //Request.Form["order_time"].ToString().Trim();
                    string order_amount = userAmt;// Request.Form["order_amount"].ToString().Trim();
                    string product_name = "乐诚网在线充值";//Request.Form["product_name"].ToString().Trim();
                    string product_code = "mhpro";//Request.Form["product_code"].ToString().Trim();
                    string product_num = "1";// Request.Form["product_num"].ToString().Trim();
                    string product_desc = "";//Request.Form["product_desc"].ToString().Trim();
                    string extra_return_param = "";// Request.Form["extra_return_param"].ToString().Trim();
                    string extend_param = "";//Request.Form["extend_param"].ToString().Trim();
                    string notify_url = Ytg.ServerWeb.Views.pay.zhifu.ZhiFuPayConfig.Notify_url;//Request.Form["notify_url"].ToString().Trim();

                    ////////////////组装签名参数//////////////////
                    string signStr = "";

                    if (extend_param != "")
                    {
                        signStr = signStr + "extend_param=" + extend_param + "&";
                    }
                    if (extra_return_param != "")
                    {
                        signStr = signStr + "extra_return_param=" + extra_return_param + "&";
                    }
                    if (interface_version != "")
                    {
                        signStr = signStr + "interface_version=" + interface_version + "&";
                    }
                    if (merchant_code != "")
                    {
                        signStr = signStr + "merchant_code=" + merchant_code + "&";
                    }
                    if (notify_url != "")
                    {
                        signStr = signStr + "notify_url=" + notify_url + "&";
                    }
                    if (order_amount != "")
                    {
                        signStr = signStr + "order_amount=" + order_amount + "&";
                    }
                    if (order_no != "")
                    {
                        signStr = signStr + "order_no=" + order_no + "&";
                    }
                    if (order_time != "")
                    {
                        signStr = signStr + "order_time=" + order_time + "&";
                    }
                    if (product_code != "")
                    {
                        signStr = signStr + "product_code=" + product_code + "&";
                    }
                    if (product_desc != "")
                    {
                        signStr = signStr + "product_desc=" + product_desc + "&";
                    }
                    if (product_name != "")
                    {
                        signStr = signStr + "product_name=" + product_name + "&";
                    }
                    if (product_num != "")
                    {
                        signStr = signStr + "product_num=" + product_num + "&";
                    }
                    if (service_type != "")
                    {
                        signStr = signStr + "service_type=" + service_type;
                    }

                    if (sign_type == "RSA-S")//RSA-S签名方法
                    {
                        //商家私钥
                        string merPriKey = ZhiFuPayConfig.Merchant_private_key;//"MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBALf/+xHa1fDTCsLYPJLHy80aWq3djuV1T34sEsjp7UpLmV9zmOVMYXsoFNKQIcEzei4QdaqnVknzmIl7n1oXmAgHaSUF3qHjCttscDZcTWyrbXKSNr8arHv8hGJrfNB/Ea/+oSTIY7H5cAtWg6VmoPCHvqjafW8/UP60PdqYewrtAgMBAAECgYEAofXhsyK0RKoPg9jA4NabLuuuu/IU8ScklMQIuO8oHsiStXFUOSnVeImcYofaHmzIdDmqyU9IZgnUz9eQOcYg3BotUdUPcGgoqAqDVtmftqjmldP6F6urFpXBazqBrrfJVIgLyNw4PGK6/EmdQxBEtqqgXppRv/ZVZzZPkwObEuECQQDenAam9eAuJYveHtAthkusutsVG5E3gJiXhRhoAqiSQC9mXLTgaWV7zJyA5zYPMvh6IviX/7H+Bqp14lT9wctFAkEA05ljSYShWTCFThtJxJ2d8zq6xCjBgETAdhiH85O/VrdKpwITV/6psByUKp42IdqMJwOaBgnnct8iDK/TAJLniQJABdo+RodyVGRCUB2pRXkhZjInbl+iKr5jxKAIKzveqLGtTViknL3IoD+Z4b2yayXg6H0g4gYj7NTKCH1h1KYSrQJBALbgbcg/YbeU0NF1kibk1ns9+ebJFpvGT9SBVRZ2TjsjBNkcWR2HEp8LxB6lSEGwActCOJ8Zdjh4kpQGbcWkMYkCQAXBTFiyyImO+sfCccVuDSsWS+9jrc5KadHGIvhfoRjIj2VuUKzJ+mXbmXuXnOYmsAefjnMCI6gGtaqkzl527tw=";
                                                                               //私钥转换成C#专用私钥
                        merPriKey = testOrder.wechart.HttpHelp.RSAPrivateKeyJava2DotNet(merPriKey);
                        //签名
                        string signData = testOrder.wechart.HttpHelp.RSASign(signStr, merPriKey);
                        //将signData进行UrlEncode编码
                        signData = HttpUtility.UrlEncode(signData);
                        //组装字符串
                        string para = signStr + "&sign_type=" + sign_type + "&sign=" + signData;
                        //将字符串发送到Dinpay网关
                        string _xml = testOrder.wechart.HttpHelp.HttpPost("https://api.dinpay.com/gateway/api/weixin", para);

                        //将同步返回的xml中的参数提取出来
                        var el = XElement.Load(new StringReader(_xml));
                        //将XML中的参数逐个提取出来
                        var qrcode1 = el.XPathSelectElement("/response/trade/qrcode");
                        var resp_code1 = el.XPathSelectElement("/response/resp_code");
                        var resp_desc1 = el.XPathSelectElement("/response/resp_desc");
                        var dinpaysign1 = el.XPathSelectElement("/response/sign");
                        if (qrcode1 == null)
                        {
                            //Response.Write("状态:" + _xml + "<br/>");
                            Response.End();
                        }
                        //去掉首尾的标签并转换成string
                        string qrcode = Regex.Match(qrcode1.ToString(), "(?<=>).*?(?=<)").Value;   //二维码链接
                        string resp_code = Regex.Match(resp_code1.ToString(), "(?<=>).*?(?=<)").Value;
                        string resp_desc = Regex.Match(resp_desc1.ToString(), "(?<=>).*?(?=<)").Value;
                        string dinpaysign = Regex.Match(dinpaysign1.ToString(), "(?<=>).*?(?=<)").Value;
                        string signsrc = "qrcode=" + qrcode + "&resp_code=" + resp_code + "&resp_desc=" + resp_desc;
                        //使用智付公钥对返回的数据验签

                        try
                        {
                            //将智付公钥转换成C#专用格式
                            dinpayPubKey = testOrder.wechart.HttpHelp.RSAPublicKeyJava2DotNet(dinpayPubKey);
                        }
                        catch (Exception ex)
                        {
                          //  Response.Write(ex.Message);
                        }
                        //验签
                        bool validateResult = testOrder.wechart.HttpHelp.ValidateRsaSign(signsrc, dinpayPubKey, dinpaysign);

                        if (validateResult == false)
                        {
                            Response.Write("失败");
                            Response.End();
                        }
                        //验签成功后将支付链接生成二维码
                        My.Utility.QRCodeHandler qr = new My.Utility.QRCodeHandler();
                        string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"/Views/pay/zhifu/wechart/qrcode/";  //文件目录   
                        string qrString = qrcode;                                                            //二维码字符串  
                        string logoFilePath = path + "my.png";//商家Logo文件 
                        fileName = "myCode" + Guid.NewGuid().ToString() + ".jpg";
                        string filePath = path + fileName;                                             //二维码文件名  
                        qr.CreateQRCode(qrcode, "Byte", 6, 6, "H", filePath, true, logoFilePath);       //生成二维码
                        //Response.Write(qrcode+"<br/>");
                        //Response.Write("结果:" + signsrc + "<br/>");
                       // Response.Write("验签结果:" + validateResult + "<br/>");
                        //Response.End();

                    }
                    else  //RSA签名方法
                    {
                        RSAWithHardware rsa = new RSAWithHardware();
                        string merPubKeyDir = "D:/1111110166.pfx";   //证书路径
                        string password = "87654321";                //证书密码
                        RSAWithHardware rsaWithH = new RSAWithHardware();
                        rsaWithH.Init(merPubKeyDir, password, "D:/dinpayRSAKeyVersion");//初始化(version路径需跟证书一致，证书会自动生成version)
                        string signData = rsaWithH.Sign(signStr);    //签名
                        signData = HttpUtility.UrlEncode(signData);  //将signData进行UrlEncode编码

                        //组装字符串
                        string para = signStr + "&sign_type=" + sign_type + "&sign=" + signData;
                        //将字符串发送到Dinpay网关
                        string _xml = testOrder.wechart.HttpHelp.HttpPost("https://api.dinpay.com/gateway/api/weixin", para);

                        //将同步返回的xml中的参数提取出来
                        var el = XElement.Load(new StringReader(_xml));
                        //将XML中的参数逐个提取出来
                        var qrcode1 = el.XPathSelectElement("/response/trade/qrcode");
                        var resp_code1 = el.XPathSelectElement("/response/resp_code");
                        var resp_desc1 = el.XPathSelectElement("/response/resp_desc");
                        var dinpaysign1 = el.XPathSelectElement("/response/sign");
                        if (qrcode1 == null)
                        {
                            Response.Write("错误:" + _xml + "<br/>");
                            Response.End();
                        }
                        //去掉首尾的标签并转换成string
                        string qrcode = Regex.Match(qrcode1.ToString(), "(?<=>).*?(?=<)").Value;   //二维码链接
                        string resp_code = Regex.Match(resp_code1.ToString(), "(?<=>).*?(?=<)").Value;
                        string resp_desc = Regex.Match(resp_desc1.ToString(), "(?<=>).*?(?=<)").Value;
                        string dinpaysign = Regex.Match(dinpaysign1.ToString(), "(?<=>).*?(?=<)").Value;

                        //组装验签字符串
                        string signsrc = "qrcode=" + qrcode + "&resp_code=" + resp_code + "&resp_desc=" + resp_desc;
                        //RSA验签
                        bool result = rsaWithH.VerifySign("1111110166", signsrc, dinpaysign);
                        if (result == false)
                        {
                            Response.Write("验签失败");
                            Response.End();
                        }
                        //验签成功后将支付链接生成二维码
                        //My.Utility.QRCodeHandler qr = new My.Utility.QRCodeHandler();
                        //string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"/Views/pay/zhifu/wechart/qrcode/";  //文件目录  
                        //string qrString = qrcode;                                                             //二维码字符串  
                        //string logoFilePath = path + "my.jpg";                                               //商家Logo路径  
                        //string filePath = path + "myCode.jpg";                                              //二维码文件名  
                        //qr.CreateQRCode(qrString, "Byte", 6, 6, "H", filePath, true, logoFilePath);        //生成二维码

                        //Response.Write("结果:" + signsrc + "<br/>");
                        //Response.Write("验签结果:" + result + "<br/>");
                        //Response.End();
                    }
                }
                finally
                {
                }
            }
        }
    }
}

namespace My.Utility
{
    /// <summary>  
    /// 二维码处理类  
    /// </summary>  
    public class QRCodeHandler
    {
        /// <summary>  
        /// 创建二维码  
        /// </summary>  
        /// <param name="QRString">二维码字符串</param>  
        /// <param name="QRCodeEncodeMode">二维码编码(Byte、AlphaNumeric、Numeric)</param>  
        /// <param name="QRCodeScale">二维码尺寸(Version为0时，1：26x26，每加1宽和高各加25</param>  
        /// <param name="QRCodeVersion">二维码密集度0-40</param>  
        /// <param name="QRCodeErrorCorrect">二维码纠错能力(L：7% M：15% Q：25% H：30%)</param>  
        /// <param name="filePath">保存路径</param>  
        /// <param name="hasLogo">是否有logo(logo尺寸50x50，QRCodeScale>=5，QRCodeErrorCorrect为H级)</param>  
        /// <param name="logoFilePath">logo路径</param>  
        /// <returns></returns>  
        public bool CreateQRCode(string QRString, string QRCodeEncodeMode, short QRCodeScale, int QRCodeVersion, string QRCodeErrorCorrect, string filePath, bool hasLogo, string logoFilePath)
        {
            bool result = true;

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

            switch (QRCodeEncodeMode)
            {
                case "Byte":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
                case "AlphaNumeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                    break;
                case "Numeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                    break;
                default:
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
            }

            qrCodeEncoder.QRCodeScale = QRCodeScale;
            qrCodeEncoder.QRCodeVersion = QRCodeVersion;

            switch (QRCodeErrorCorrect)
            {
                case "L":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                    break;
                case "M":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    break;
                case "Q":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                    break;
                case "H":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
                default:
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
            }

            try
            {
                Image image = qrCodeEncoder.Encode(QRString, System.Text.Encoding.UTF8);
                System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                fs.Close();

                if (hasLogo)
                {
                    Image copyImage = System.Drawing.Image.FromFile(logoFilePath);
                    Graphics g = Graphics.FromImage(image);
                    int x = image.Width / 2 - copyImage.Width / 2;
                    int y = image.Height / 2 - copyImage.Height / 2;
                    g.DrawImage(copyImage, new Rectangle(x, y, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
                    g.Dispose();

                    image.Save(filePath);
                    copyImage.Dispose();
                    
                }
                image.Dispose();

            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}