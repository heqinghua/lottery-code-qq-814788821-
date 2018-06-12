using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using System.Web.UI;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

/// <summary>
///ValidateNumber 生成验证码
/// </summary>
public class ValidateNumber
{
    //产生验证码的字符集 （易混淆的字符去掉）
    private string charcode = "2,3,4,5,6,8,9,A,B,C,D,E,F,G,H,J,K,M,N,P,R,S,U,W,X,Y,a,b,c,d,e,f,g,h,j,k,m,n,p,r,s,u,w,x,y";

    /// <summary>
    /// 验证码的最大长度
    /// </summary>
    public int MaxLength
    {
        get { return 10; }
    }

    /// <summary>
    /// 验证码的最小长度
    /// </summary>
    public int MinLength
    {
        get { return 1; }
    }

    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <param name="length">指定验证码的长度</param>
    /// <returns></returns>
    public string CreateValidateNumber(int length)
    {
        string[] CharArray = charcode.Split(',');//将字符串转换为字符数组
        string randomCode = "";
        int temp = -1;

        Random rand = new Random();
        for (int i = 0; i < length; i++)
        {
            if (temp != -1)
            {
                rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
            }
            int t = rand.Next(CharArray.Length - 1);
            if (temp != -1 && temp == t)
            {
                return CreateValidateNumber(length);
            }
            temp = t;
            randomCode += CharArray[t];
        }
        return randomCode;
    }

    /// <summary>
    /// 创建验证码的图片
    /// </summary>
    /// <param name="context">context对象</param>
    /// <param name="validateNum">验证码</param>
    public void CreateValidateGraphic(HttpContext context,string validateNum)
    {
        int iwidth = (int)(validateNum.Length * 14);
        Bitmap image = new Bitmap(iwidth, 22);
        Graphics g = Graphics.FromImage(image);
        try
        {
            //生成随机生成器
            Random random = new Random();
            //清空图片背景色
            g.Clear(Color.White);
            //画图片的干扰线
            for (int i = 0; i < 25; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
             Color.Blue, Color.DarkRed, 1.2f, true);
            g.DrawString(validateNum, font, brush, 3, 2);
            //画图片的前景干扰点
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            //画图片的边框线
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
            //保存图片数据
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            //输出图片
            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";
            context.Response.BinaryWrite(stream.ToArray());
        }
        finally
        {
            g.Dispose();
            image.Dispose();
        }
    }
    /// <summary>
    /// 得到验证码图片的长度
    /// </summary>
    /// <param name="validateNumLength">验证码的长度</param>
    /// <returns></returns>
    public static int GetImageWidth(int validateNumLength)
    {
        return (int)(validateNumLength * 14);
    }
    /// <summary>
    /// 得到验证码图片的高度
    /// </summary>
    /// <returns></returns>
    public static double GetImageHeight()
    {
        return 22;
    }
}