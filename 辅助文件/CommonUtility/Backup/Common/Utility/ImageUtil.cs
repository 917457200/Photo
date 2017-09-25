using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Common.Utility
{
    public class ImageUtil
    {

        #region Pictures


        #region Size

        /// <summary>
        /// 获取把图片按比例缩放到规定的大小以内的大小
        /// </summary>
        /// <param name="Image">目标图片</param>
        /// <param name="maxSize">最大大小</param>
        public static Size GetPictureLimitSize(Image img, Size maxSize)
        {
            Size size = new Size();
            //如果需要缩放
            if (img.Width > maxSize.Width ||
                img.Height > maxSize.Height)
            {
                //获取高宽比
                double hwRateNow = (Convert.ToDouble(img.Height) / img.Width);
                double hwRateMax = (Convert.ToDouble(maxSize.Height) / maxSize.Width);

                //如果高度要缩小
                if (hwRateNow > hwRateMax)
                {
                    size.Height = maxSize.Height;
                    size.Width = Convert.ToInt32(size.Height / hwRateNow);
                }
                //缩小宽度
                else
                {
                    size.Width = maxSize.Width;
                    size.Height = Convert.ToInt32(size.Width * hwRateNow);
                }

            }
            //否则图片大小不变
            else
            {
                size = img.Size;
            }

            return size;
        }

        /// <summary>
        /// Resize图片
        /// </summary>
        /// <param name="bmp">原始Bitmap</param>
        /// <param name="newW">新的宽度</param>
        /// <param name="newH">新的高度</param>
        /// <param name="Mode">保留着，暂时未用</param>
        /// <returns>处理以后的图片</returns>
        public static Image KiResizeImage(Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                //如果大小没变，则原样返回
                if (bmp.Size.Width == newH && bmp.Height == newH)
                {
                    return bmp;
                }
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();

                return b;
            }
            catch
            {
                return null;
            }
        }



        // ===============================

        /// <summary>
        /// 剪裁 -- 用GDI+
        /// </summary>
        /// <param name="b">原始Bitmap</param>
        /// <param name="StartX">开始坐标X</param>
        /// <param name="StartY">开始坐标Y</param>
        /// <param name="iWidth">宽度</param>
        /// <param name="iHeight">高度</param>
        /// <returns>剪裁后的Bitmap</returns>
        public static Image KiCut(Image b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }

            int w = b.Width;
            int h = b.Height;

            if (StartX >= w || StartY >= h)
            {
                return null;
            }
            if (StartX == 0 && StartY == 0 && iWidth == w && iHeight == h)
            {
                return b;
            }

            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }

            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }

            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();

                return bmpOut;
            }
            catch
            {
                return null;
            }
        }


        #endregion

        #endregion
    }
}
