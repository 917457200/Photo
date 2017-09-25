using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PicCut
{
    public partial class MainWindow : Form
    {
        private const int AreaWidth = 512;       //pictureBox控件的最大宽度
        private const int AreaHeight = 384;     //pictureBox控件的最大高度
        //此宽高比为6:10，因具体需要而设，实际上可无高宽比限制

        private Rectangle m_Rect;                    // 矩形选框，用于截取图片

        private bool canMove = false;              //指定鼠标移动事件是否需要响应

        private int mouseLocationX = 0;          //初始化鼠标当前X坐标
        private int mouseLocationY = 0;          //初始化鼠标当前Y坐标

        private string picPath = null;               //图片路径
       
        public MainWindow()
        {
            InitializeComponent();
        }

        /*
         * 图片裁剪方法
         * param b:需裁剪的图片位图
         * param StartX：裁剪起始X坐标
         * param StartY：裁剪起始Y坐标
         * param iWidth：裁剪宽度
         * param iHeight：裁剪高度
         * */
        public static Bitmap KiCut(Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
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

        /*
         * 图片载入按钮单击事件响应
         * */
        private void load_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDlg=new OpenFileDialog())
            {
                openDlg.InitialDirectory = ".";
                openDlg.Filter = "JPG File(*.jpg)|*.jpg|JPEG File(*.jpeg)|*.jpeg|PNG File(*.png)|*.png|BMP File(*.bmp)|*.bmp";
                openDlg.RestoreDirectory = true;
                openDlg.FileName = "sourcePic";
                if (openDlg.ShowDialog() == DialogResult.OK)
                {
                    picPath = openDlg.FileName;
                    if (picPath != null && System.IO.File.Exists(picPath))
                    {
                        //根据图片原始尺寸调整pictureBox控件尺寸，保证图片在控件中保持原高宽比显示
                        Bitmap bmp = new Bitmap(Image.FromFile(picPath));
                        if (bmp.Width > AreaWidth)
                        {
                            picArea.Width = AreaWidth;
                            picArea.Height = bmp.Height * AreaWidth / bmp.Width;
                            maxPic.Checked = true;
                        }
                        if (bmp.Height > AreaHeight)
                        {
                            picArea.Height = AreaHeight;
                            picArea.Width = bmp.Width * AreaHeight / bmp.Height;
                            maxPic.Checked = true;
                        }
                        if (bmp.Width <= picArea.Width)
                            picArea.Width = bmp.Width;
                        if (bmp.Height <= picArea.Height)
                            picArea.Height = bmp.Height;
                        if (picArea.Width <AreaWidth  && picArea.Height<AreaHeight)
                        {
                            maxPic.Checked = false;
                        }

                        m_Rect = new Rectangle(0, 0, 0, 0);           //初始化矩形裁剪选框

                        track.Value = (int)(20 * 0.8);                      //初始化选框大小调整滑块

                        //根据pictureBox控件尺寸调整矩形选框尺寸，保证选框始终在pictureBox控件内部
                        m_Rect.Width = (int)(picArea.Width * 0.8f);    
                        m_Rect.Height = m_Rect.Width * 800 / 480;
                        if (m_Rect.Height > picArea.Height)
                        {
                            m_Rect.Height =(int)( picArea.Height * 0.8f);
                            m_Rect.Width = m_Rect.Height * 480 / 800;
                        }
                         
                        //以文件流的形式向pictureBox中加载图片
                        FileStream fs = File.OpenRead(picPath);
                        picArea.Image = Image.FromStream(fs);
                        fs.Close();

                        if (picArea.Image == null)
                        {
                            MessageBox.Show("Failed to load the picture！");
                        }

                        picArea.Invalidate();   //重绘pictureBox控件，触发paint事件
                    }
                    else
                    {
                        MessageBox.Show("The picture is not exist!");
                    }
                }
            }
        }

        //裁剪图片保存按钮单击事件响应
        private void ok_Click(object sender, EventArgs e)
        {
            if (picArea.Image != null)
            {
                Bitmap map = new Bitmap(picArea.Image);
                using (SaveFileDialog saveDlg = new SaveFileDialog())
                {
                    saveDlg.InitialDirectory = ".";
                    saveDlg.Filter = "JPG File(*.jpg)|*.jpg";
                    saveDlg.RestoreDirectory = true;
                    saveDlg.FileName = "targetPic";
                    if (saveDlg.ShowDialog() == DialogResult.OK)
                    {
                        map = KiCut(map, m_Rect.X * map.Width / picArea.Width, m_Rect.Y * map.Height / picArea.Height, map.Width * m_Rect.Width / picArea.Width, map.Height * m_Rect.Height / picArea.Height);  //通过比例换算裁剪图片保证裁剪结果的正确
                        map.Save(saveDlg.FileName, ImageFormat.Jpeg);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please load a picture first！");
            }
        }

        //pictureBox控件重绘
        private void picArea_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if(m_Rect!=null)
                g.DrawRectangle(new Pen(new SolidBrush(Color.Red),2),this.m_Rect);
        }

        //pictureBox内鼠标单击事件响应，保证鼠标在矩形选框内才可响应鼠标移动事件
        private void picArea_MouseDown(object sender, MouseEventArgs e)
        {
            Point pt = picArea.PointToClient(Control.MousePosition);
            if (pt.X < m_Rect.X || pt.X > m_Rect.X + m_Rect.Width || pt.Y < m_Rect.Y || pt.Y > m_Rect.Y + m_Rect.Height)
                canMove = false;
            else
            {
                canMove = true;
                mouseLocationX = pt.X;    //更新当前鼠标X坐标
                mouseLocationY = pt.Y;    //更新当前鼠标Y坐标
            }
        }

        //鼠标矩形选框内移动事件响应，保证矩形选框始终在pictureBox控件内部
        private void picArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove)
            {
                Point pt = picArea.PointToClient(Control.MousePosition);    //获取鼠标相对pictureBox控件的坐标
                m_Rect.X +=pt.X-mouseLocationX;
                m_Rect.Y += pt.Y - mouseLocationY;
                mouseLocationX = pt.X;
                mouseLocationY = pt.Y;
                
                m_Rect.X = m_Rect.X < 0 ? 0 : m_Rect.X;
                m_Rect.Y = m_Rect.Y < 0 ? 0 : m_Rect.Y;
                m_Rect.X = m_Rect.X + m_Rect.Width > picArea.Width ? picArea.Width - m_Rect.Width :m_Rect.X ;
                m_Rect.Y = m_Rect.Y + m_Rect.Height > picArea.Height ? picArea.Height - m_Rect.Height : m_Rect.Y;
                 /*
                if (m_Rect.X < 0)
                    m_Rect.X = 0;
                if (m_Rect.Y < 0)
                    m_Rect.Y = 0;
                if (m_Rect.X + m_Rect.Width > picArea.Width)
                    m_Rect.X = picArea.Width - m_Rect.Width;
                if (m_Rect.Y + m_Rect.Height > picArea.Height)
                    m_Rect.Y = picArea.Height - m_Rect.Height;
                 * */
                picArea.Invalidate();     //重绘pictureBox控件
            }
        }

        //鼠标按键弹起事件响应，保证鼠标没有按键按下时无法响应鼠标移动事件
        private void picArea_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = false;
        }

        //保证pictureBox控件始终居中对齐
       private void picArea_SizeChanged(object sender, EventArgs e)
       {
           picArea.Left = panel.Left + panel.Width / 2 - picArea.Width / 2;
           picArea.Top = panel.Top + panel.Height / 2 - picArea.Height / 2;
          
       }

       //使用滑块动态调整矩形选框大小

        //最大化图片和恢复图片原始尺寸显示方式的切换
       private void maxPic_Click(object sender, EventArgs e)
       {
           if (maxPic.Checked)
           {
               
               if (picArea.Width <AreaWidth  && picArea.Height<AreaHeight)
               {
                   int tmp=picArea.Width;
                   picArea.Width = AreaWidth;
                   picArea.Height = picArea.Height * AreaWidth / tmp;

                   if (picArea.Height > AreaHeight)
                   {
                       tmp = picArea.Height;
                       picArea.Height = AreaHeight;
                       picArea.Width = picArea.Width * AreaHeight / tmp;
                   }

                   float ch = track.Value / 20.0f;
                   if ((picArea.Width - m_Rect.Width) * 1.0f / (picArea.Height - m_Rect.Height) < 0.6)  //从最快趋近于pictureBox边界的一边调节
                   {
                       m_Rect.Width = (int)(picArea.Width * ch);
                       m_Rect.Height = m_Rect.Width * 800 / 480;
                   }
                   else
                   {
                       m_Rect.Height = (int)(picArea.Height * ch);
                       m_Rect.Width = m_Rect.Height * 480 / 800;
                   }
                   //保证矩形选框不越界
                   if (m_Rect.X + m_Rect.Width > picArea.Width)
                       m_Rect.X = picArea.Width - m_Rect.Width;
                   if (m_Rect.Y + m_Rect.Height > picArea.Height)
                       m_Rect.Y = picArea.Height - m_Rect.Height;
               }
           }
           else
           {
               if (picArea.Width == AreaWidth || picArea.Height == AreaHeight)
               {
                   Bitmap bmp = new Bitmap(picArea.Image);
                   if (bmp.Width < AreaWidth && bmp.Height < AreaHeight)
                   {
                       picArea.Width = bmp.Width;
                       picArea.Height = bmp.Height;
                   }

                   m_Rect.Width = (int)(picArea.Width * track.Value / 20.0f);
                   m_Rect.Height = m_Rect.Width * 800 / 480;
                   if (m_Rect.Height > picArea.Height)
                   {
                       m_Rect.Height = (int)(picArea.Height * track.Value / 20.0f);
                       m_Rect.Width = m_Rect.Height * 480 / 800;
                   }
                   //保证矩形选框不越界
                   if (m_Rect.X + m_Rect.Width > picArea.Width)
                       m_Rect.X = picArea.Width - m_Rect.Width;
                   if (m_Rect.Y + m_Rect.Height > picArea.Height)
                       m_Rect.Y = picArea.Height - m_Rect.Height;
               }
           }
       }
        
    }
}
