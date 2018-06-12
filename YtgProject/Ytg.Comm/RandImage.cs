using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Comm
{
    /// <summary>
    /// 产生随即图片
    /// </summary>
    public sealed class RandImage
    {
        private const string RandCharString = "0123456789";
        private int width;
        private int height;
        private int length;
        /// <summary>
        /// 默认构造函数，生成的图片宽度为48×24，随即字符串字符个数
        /// </summary>
        public RandImage()
            : this(48, 24, 4)
        {
        }
        /// <summary>
        /// 指定生成图片的宽和高，默认生成图片的字符串长度为4个字符
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public RandImage(int width, int height)
            : this(width, height, 4)
        {
        }
        /// <summary>
        /// 指定生成图片的宽和高以及生成图片的字符串字符个数
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="length"></param>
        public RandImage(int width, int height, int length)
        {
            this.width = width;
            this.height = height;
            this.length = length;
        }
        /// <summary>
        /// 以默认的大小和默认的字符个数产生图片
        /// </summary>
        /// <returns></returns>
        public Image GetImage(ref string randString)
        {
            Bitmap image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);
            Random random = new Random();
            do
            {

                //使用DateTime.Now.Millisecond作为生成随机数的参数，增加随机性
                randString += RandCharString.Substring(random.Next(DateTime.Now.Millisecond) % RandCharString.Length, 1);
            }
            while (randString.Length < 4);
            float emSize = (float)width / randString.Length;
            Font font = new Font("arial", emSize, ( System.Drawing.FontStyle.Italic));
            
            #region 画图片的背景噪音线
            int x1, y1, x2, y2;
            for (int i = 0; i < 25; i++)
            {
                Pen pen = new Pen(Color.FromArgb(random.Next(Int32.MaxValue)));
                x1 = random.Next(image.Width);
                y1 = random.Next(image.Height);
                x2 = random.Next(image.Width);
                y2 = random.Next(image.Height);
                g.DrawLine(pen, x1, y1, x2, y2);
            }
            #endregion

            //#region 画图片的前景噪音点
            //for (int i = 0; i < 100; i++)
            //{
            //    x1 = random.Next(image.Width);
            //    y1 = random.Next(image.Height);
            //    image.SetPixel(x1, y1, Color.FromArgb(random.Next(Int32.MaxValue)));
            //}
            //#endregion
            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);//
            #region 绘制边框


            #endregion
            g.TranslateTransform(image.Height, 0);//偏移量
            g.RotateTransform(45);
            SolidBrush brush = new SolidBrush(Color.FromArgb(38, 38, 38));
            g.DrawString(randString, font, brush,2, 2);
            g.Dispose();
            return image;

        }
    }

}
