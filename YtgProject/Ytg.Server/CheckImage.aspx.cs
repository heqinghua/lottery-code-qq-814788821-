using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;

namespace Ytg.ServerWeb
{
    public partial class CheckImage : System.Web.UI.Page
    {
        private const string RandCharString = "0123456789";

        protected void Page_Load(object sender, EventArgs e)
        {
            string refCode = string.Empty;
            Random random = new Random();
            do
            {

                //使用DateTime.Now.Millisecond作为生成随机数的参数，增加随机性
                refCode += RandCharString.Substring(random.Next(DateTime.Now.Millisecond) % RandCharString.Length, 1);
            }
            while (refCode.Length < 4);
            string type = Request.Params["tp"];
            switch (type)
            {
                case "login"://登陆
                    Session["mLogin"] = refCode;
                    break;
                case "recharge"://充值
                    Session["mRecharge"] = refCode;
                    break;
                case "autoRegist"://自动注册验证码
                    Session["ValidateCode"] = refCode;
                    break;
                case "updatePwd"://找回密码验证码
                    Session["updatePwd_code"] = refCode;
                    break;
                case "dns"://验证域名
                    Session["verification_dns"] = refCode;
                    break;

            }
            if (type != "login" && type != "autoRegist")
                BuildBigImage(refCode);
            else
                BuildBigImageLogin(refCode);
            //image.Dispose();
            Response.End();


            //  Response.End();
        }


        /// <summary>
        /// 生成单个小图
        /// </summary>
        /// <param name="s">数字</param>
        /// <param name="c">数字颜色</param>
        /// <param name="py">旋转偏移量</param>
        /// <returns></returns>
        public System.Drawing.Image BuildBitmap(string s, Color c, float py)
        {
            c = Color.FromArgb(36, 36, 36);
            System.Drawing.Bitmap bitmap = new Bitmap(12, 24);
            var g = System.Drawing.Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            //设置画板的坐标原点为中点
            g.TranslateTransform(bitmap.Width / 2, bitmap.Height / 2);
            //以指定角度对画板进行旋转
            g.RotateTransform(py);
            var size = g.MeasureString(s, new Font("arial", 12, FontStyle.Regular));
            //让文字变得平滑
           // g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            //把数字画到画板的中点位置
            g.DrawString(s, new Font("arial", 12, FontStyle.Regular), new SolidBrush(c), (bitmap.Width - size.Width) / 2 - bitmap.Width / 2, (bitmap.Height - size.Height) / 2 - bitmap.Height / 2);
            return bitmap;
        }

        /// <summary>
        /// 生成单个小图
        /// </summary>
        /// <param name="s">数字</param>
        /// <param name="c">数字颜色</param>
        /// <param name="py">旋转偏移量</param>
        /// <returns></returns>
        public System.Drawing.Image BuildBitmap_login(string s, Color c, float py)
        {
            c = Color.FromArgb(36, 36, 36);
            System.Drawing.Bitmap bitmap = new Bitmap(18, 41);
            var g = System.Drawing.Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            //设置画板的坐标原点为中点
            g.TranslateTransform(bitmap.Width / 2, bitmap.Height / 2);
            //以指定角度对画板进行旋转
            g.RotateTransform(py);
            var size = g.MeasureString(s, new Font("arial", 18, FontStyle.Regular));
            //让文字变得平滑
            // g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            //把数字画到画板的中点位置
            g.DrawString(s, new Font("arial", 18, FontStyle.Regular), new SolidBrush(c), (bitmap.Width - size.Width) / 2 - bitmap.Width / 2, (bitmap.Height - size.Height) / 2 - bitmap.Height / 2);
            return bitmap;
        }

        /// <summary>
        /// 把小图合并为一个大图
        /// </summary>
        public void BuildBigImageLogin(string content)
        {
           // Color[] c = new Color[9] { Color.Red, Color.Green, Color.Maroon, Color.Blue, Color.BurlyWood, Color.Orange, Color.Lime, Color.MediumTurquoise, Color.Olive };
            Bitmap bigImage = new Bitmap(80, 41);//创建一个大位图
            Graphics g = Graphics.FromImage(bigImage);//创建大图的画板
            g.Clear(Color.Orange);
            var newcon = "";
            for (var i = 0; i < content.Length; i++)
            {
                newcon += content[i];
                if (i < content.Length - 1)
                    newcon += " ";

            }
            content = newcon;
            var size = g.MeasureString(content, new Font("arial", 18, FontStyle.Regular));
            
            //把数字画到画板的中点位置
            g.DrawString(content, new Font("arial", 18, FontStyle.Regular), new SolidBrush(Color.White), (bigImage.Width/2 - size.Width / 2),(bigImage.Height / 2 - size.Height / 2));

            System.IO.MemoryStream sm = new System.IO.MemoryStream();
            bigImage.Save(sm, System.Drawing.Imaging.ImageFormat.Png);
            Response.ContentType = "image/png";
            Response.BinaryWrite(sm.ToArray());
        }


        /// <summary>
        /// 把小图合并为一个大图
        /// </summary>
        public void BuildBigImage(string content)
        {
            Color[] c = new Color[9] { Color.Red, Color.Green, Color.Maroon, Color.Blue, Color.BurlyWood, Color.Orange, Color.Lime, Color.MediumTurquoise, Color.Olive };
            Bitmap bigImage = new Bitmap(60, 24);//创建一个大位图
            Graphics g = Graphics.FromImage(bigImage);//创建大图的画板
            Random r = new Random(DateTime.Now.Millisecond);
            var x = 0;
            var y = 0;
            var color = c[r.Next(0, 8)];
            float py = r.Next(1, 45);//长生随机的偏移量
            var img = BuildBitmap(content[0].ToString(), color, py);
            var point = new Point(x, y);//小图画到大图上的位置
            g.DrawImage(img, point);//把小图画到大图上
            x = x + 15;

            img = BuildBitmap(content[1].ToString(), color, py);
            py = r.Next(1, 45);//长生随机的偏移量
            point = new Point(x, y);
            g.DrawImage(img, point);//把小图画到大图上
            x = x + 15;

            img = BuildBitmap(content[2].ToString(), color, py);
            point = new Point(x, y);
            g.DrawImage(img, point);//把小图画到大图上
            x = x + 15;

            img = BuildBitmap(content[3].ToString(), color, py);
            point = new Point(x, y);
            g.DrawImage(img, point);//把小图画到大图上
            Random random = new Random();
            int x1, y1, x2, y2;
            for (int i = 0; i < 25; i++)
            {
                Pen pen = new Pen(Color.FromArgb(random.Next(Int32.MaxValue)));
                x1 = random.Next(bigImage.Width);
                y1 = random.Next(bigImage.Height);
                x2 = random.Next(bigImage.Width);
                y2 = random.Next(bigImage.Height);
                g.DrawLine(pen, x1, y1, x2, y2);
            }


            //绘制边框
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, bigImage.Width - 1, bigImage.Height - 1);//

            System.IO.MemoryStream sm = new System.IO.MemoryStream();
            bigImage.Save(sm, System.Drawing.Imaging.ImageFormat.Png);
            Response.ContentType = "image/png";
            Response.BinaryWrite(sm.ToArray());
        }

    }
}