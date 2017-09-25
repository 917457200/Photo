using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace ImageClassLib
{
    public class ImageCut
    {
        /// <summary> 
        /// 缩放 -- 用GDI+ ，定义缩放图片方法，返回值为位图Bitmap
	    /// </summary> 
	    /// <param name="b">原始Bitmap</param> 
	    /// <param name="StartX">开始坐标X</param> 
	    /// <param name="StartY">开始坐标Y</param> 
	    /// <param name="iWidth">宽度</param> 
	    /// <param name="iHeight">高度</param> 
	    /// <returns>缩放后的Bitmap</returns> 
	    public Bitmap KiCut(Bitmap b) 
	    { 
	        if (b == null) 
	        { 
	            return null; 
	        } 
	   
	        int w = b.Width; 
	        int h = b.Height; 
	   
	        if (X >= w || Y >= h) 
	        { 
	            return null; 
	        } 
	   
	        if (X + Width > w) 
	        { 
	            Width = w - X; 
	        } 
	   
	        if (Y + Height > h) 
	        { 
	            Height = h - Y; 
	        } 
	   
	        try
	        { 
	            Bitmap bmpOut = new Bitmap(Width, Height, PixelFormat.Format24bppRgb); 
	   
	            Graphics g = Graphics.FromImage(bmpOut);
                // Create rectangle for displaying image.
                Rectangle destRect = new Rectangle(0, 0, Width, Height);        //所画的矩形正确

                // Create rectangle for source image.
                Rectangle srcRect = new Rectangle(0, 0, w, h);      //原矩形不对
                g.DrawImage(b, destRect, srcRect, GraphicsUnit.Pixel);
                //resultG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, side, side), new System.Drawing.Rectangle(0, 0, initWidth, initHeight), System.Drawing.GraphicsUnit.Pixel);
	            g.Dispose(); 
	            return bmpOut; 
	        } 
	        catch
	        { 
	            return null; 
	        } 
	    } 
	    public int X = 0; 
	    public int Y = 0; 
	    public int Width; 
	    public int Height;
        /// <summary>
        /// ImageCut类的构造函数
        /// </summary>
        /// <param name="x表示图片左上角X轴的坐标"></param>
        /// <param name="y表示图片左上角Y轴的坐标"></param>
        /// <param name="width表示图片的宽"></param>
        /// <param name="heigth表示图片的高"></param>
        public ImageCut(int x, int y, int width, int heigth)
        {
            X = x;
            Y = y;
            Width = width;
            Height = heigth;
        } 
    }
}
