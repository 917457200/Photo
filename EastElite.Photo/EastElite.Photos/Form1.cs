using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AForge;
using AForge.Controls;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Timers;
using DeviceScan;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Net;

namespace EastElite.Photos
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection videoDevices;
        private string tscbxCameras = "";
        private string userCode = "";
        private string schoolCode = "";
        Bll.StuUser Stu = new Bll.StuUser();

        DeviceScan.EastEliteSMSWS.EastEliteSMSWSSoapClient Service = new DeviceScan.EastEliteSMSWS.EastEliteSMSWSSoapClient();

        private string picName = "";   //当前截图
        private string PaiName = "";
        private string StuCode = "";

        private Rectangle m_Rect;                    // 矩形选框，用于截取图片
        private bool canMove = false;              //指定鼠标移动事件是否需要响应
        private int mouseLocationX = 0;          //初始化鼠标当前X坐标
        private int mouseLocationY = 0;          //初始化鼠标当前Y坐标
        private int MaxWidth = 0;
        private int MaxHeiht = 0;
        private string Capabilities = "";

        public Form1( string Code, string SchoolCode )
        {
            userCode = Code;
            schoolCode = SchoolCode;
            InitializeComponent();
        }

        //加载摄像头
        private void Form1_Load( object sender, EventArgs e )
        {
            try
            {
                this.WindowState = FormWindowState.Maximized;//全屏
                // 枚举所有视频输入设备
                videoDevices = new FilterInfoCollection( FilterCategory.VideoInputDevice );

                if( videoDevices.Count == 0 )
                    throw new ApplicationException();

                if( videoDevices.Count > 0 )
                {
                    tscbxCameras = videoDevices[videoDevices.Count - 1].Name;
                }
                Moshi( 1 );
                OpenCam();
            }
            catch( ApplicationException )
            {
                tscbxCameras = "未发现摄像头";
                videoDevices = null;
            }
        }

        //关闭摄像头
        private void btnCloseCam_Click( object sender, EventArgs e )
        {
            if( videoSourcePlayer != null && videoSourcePlayer.IsRunning )
            {
                videoSourcePlayer.SignalToStop();
                videoSourcePlayer.WaitForStop();
            }
        }

        //关闭窗体
        private void Form1_FormClosing( object sender, FormClosingEventArgs e )
        {
            btnCloseCam_Click( null, null );
        }

        //获取路径
        private string GetImagePath()
        {
            string personImgPath = Path.GetDirectoryName( AppDomain.CurrentDomain.BaseDirectory ) + "Img";
            if( !Directory.Exists( personImgPath ) )
            {
                Directory.CreateDirectory( personImgPath );
            }

            return personImgPath;
        }

        //获取路径
        private string GetStuImagePath()
        {
            string personImgPath = Path.GetDirectoryName( AppDomain.CurrentDomain.BaseDirectory ) + "StuImg";
            if( !Directory.Exists( personImgPath ) )
            {
                Directory.CreateDirectory( personImgPath );
            }

            return personImgPath;
        }

        //连接摄像头
        private void OpenCam()
        {
            if( tscbxCameras == "未发现摄像头" )
            {
                MessageBox.Show( "未发现摄像头", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Hand );
                return;
            }

            VideoCaptureDevice videoSource = new VideoCaptureDevice( videoDevices[0].MonikerString );
            if( videoSource.VideoCapabilities.Count() == 0 )
            {
                return;
            }
            int D = videoSource.VideoCapabilities.Length;
            Capabilities = videoSource.VideoCapabilities[D - 1].FrameSize.Height + "*" + videoSource.VideoCapabilities[D - 1].FrameSize.Width;
            videoSource.VideoResolution = videoSource.VideoCapabilities[D - 1];

            videoSourcePlayer.VideoSource = videoSource;
            videoSourcePlayer.Start();
            FSizeChanged();
            track.Value = (int) ( 20 * 0.8 );                      //初始化选框大小调整滑块

            //根据pictureBox控件尺寸调整矩形选框尺寸，保证选框始终在pictureBox控件内部
            int Width = (int) ( videoSourcePlayer.Width * 0.8f );
            int Height = Width * 400 / 300;
            if( Height > videoSourcePlayer.Height )
            {
                Height = (int) ( videoSourcePlayer.Height * 0.8f );
                Width = Height * 300 / 400;
            }
            int Top = ( videoSourcePlayer.Width - Width ) / 2;
            int Left = ( videoSourcePlayer.Height - Height ) / 2;


            m_Rect = new Rectangle( Top, Left, Width, Height );//初始化矩形裁剪选框

            videoSourcePlayer.Invalidate();   //重绘pictureBox控件，触发paint事件
        }

        //拍照
        private void btnPaizhao_Click_1( object sender, EventArgs e )
        {
            if( StuCode == "" )
            {
                MessageBox.Show( "请先输入或读卡获取学生信息！" );
                return;
            }
            try
            {
                if( videoSourcePlayer.IsRunning )
                {
                    BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                    videoSourcePlayer.GetCurrentVideoFrame().GetHbitmap(),
                                    IntPtr.Zero,
                                     Int32Rect.Empty,
                                    BitmapSizeOptions.FromEmptyOptions() );
                    PngBitmapEncoder pE = new PngBitmapEncoder();
                    pE.Frames.Add( BitmapFrame.Create( bitmapSource ) );

                    Directory.Delete( GetImagePath(), true );

                    PaiName = GetImagePath() + "\\" + DateTime.Now.ToString( "yyyyMMddHHmmssfff" ) + ".jpg";

                    if( File.Exists( PaiName ) )
                    {
                        File.Delete( PaiName );
                    }
                    using( Stream stream = File.Create( PaiName ) )
                    {
                        pE.Save( stream );
                    }
                    Image img = Image.FromFile( PaiName );

                    if( img != null )
                    {
                        Bitmap map = new Bitmap( img );
                        map = KiCut( map, m_Rect.X * map.Width / videoSourcePlayer.Width, m_Rect.Y * map.Height / videoSourcePlayer.Height, map.Width * m_Rect.Width / videoSourcePlayer.Width, map.Height * m_Rect.Height / videoSourcePlayer.Height );  //通过比例换算裁剪图片保证裁剪结果的正确
                        picName = GetStuImagePath() + "\\2_2_" + StuCode + ".jpg";
                        img.Dispose();
                        map.Save( picName );
                        pictureBox1.Image = GetThumbnail( map, 200, 150 );
                        if( this.checkBox1.Checked )
                        {
                            UploadFile();
                        }
                    }
                    else
                    {
                        MessageBox.Show( "Please load a picture first！" );
                    }

                }
                else
                    MessageBox.Show( "当前尚未连接摄像头", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Hand );
            }
            catch( Exception ex )
            {
                MessageBox.Show( "摄像头异常：" + ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        //读卡
        private void timer1_Tick( object sender, EventArgs e )
        {
            try
            {
                string ICCardSN = "";
                CardScanHelper cardHelper = new CardScanHelper();
                ICCardSN = cardHelper.ScanCard();
                string IC01 = ICCardSN.Substring( 0, 2 );
                string IC02 = ICCardSN.Substring( 2, 2 );
                string IC03 = ICCardSN.Substring( 4, 2 );
                string IC04 = ICCardSN.Substring( 6, 2 );
                ICCardSN = IC04 + IC03 + IC02 + IC01;
                if( ICCardSN != "" )
                {
                    if( this.Card.Text != ICCardSN )
                    {
                        this.Card.Text = ICCardSN;
                        string UserStr = Service.GetStudentDataByCustomerCardSN( schoolCode, ICCardSN );
                        Bll.StuUser.StudentUser Use = Stu.GetUserNameForSerVice( UserStr );
                        if( Use.StatusID == "1" )
                        {
                            StuCode = Use.Code;
                            this.Card.Text = Use.CustomerCardSN;
                            this.Code.Text = Use.Code;
                            this.label5.Text = Use.Name;
                            this.label7.Text = Use.SexText;
                            this.label9.Text = Use.ClassCode;
                            this.label11.Text = Use.ClassName;
                            this.label13.Text = Use.Telephone;
                            this.label15.Text = Use.DiningCardCode;
                            System.Net.WebClient web = new System.Net.WebClient();
                            byte[] buffer = web.DownloadData( Use.PhotoUrl );
                            web.Dispose();
                            System.IO.MemoryStream ms = new System.IO.MemoryStream( buffer );
                            this.UserInage.Image = Image.FromStream( ms );
                        }
                        else
                        {
                            StuCode = "";
                            this.Code.Text = "";
                            this.label5.Text = "";
                            this.label7.Text = "";
                            this.label9.Text = "";
                            this.label11.Text = "";
                            this.label13.Text = "";
                            this.label15.Text = "";
                            this.UserInage.Image = (System.Drawing.Image) Properties.Resources.ResourceManager.GetObject( "timg1" );
                        }
                        masgerShow( Use.StatusText );
                    }
                }
            }
            catch( Exception ex )
            {
                // MessageBox.Show( ex.Message );
                return;
            }
        }

        //读卡模式
        private void ReaCard_Click( object sender, EventArgs e )
        {
            Moshi( 1 );
        }

        //手动输入模式
        private void InHand_Click( object sender, EventArgs e )
        {
            Moshi( 0 );
        }

        //模式转换
        private void Moshi( int J )
        {
            if( J == 1 )
            {
                this.timer1.Enabled = true;
                this.Code.ReadOnly = true;
                this.Code.Width = 108;
                this.Codeyes.Hide();
                masgerShow( "进入读卡模式" );
            }
            else
            {
                this.timer1.Stop();
                this.timer1.Enabled = false;
                this.Code.ReadOnly = false;
                this.Code.Width = 71;
                this.Codeyes.Show();
                masgerShow( "进入手动输入模式" );
            }

        }

        /// <summary>
        /// 信息展示
        /// </summary>
        /// <param name="Value"></param>
        private void masgerShow( string Value )
        {
            this.richTextBox1.AppendText( Value + "\n\r" );
            this.richTextBox1.Select( this.richTextBox1.TextLength, 0 );//设置光标的位置到文本尾  
            this.richTextBox1.ScrollToCaret();//滚动到控件光标处  
        }

        //确定学生学号
        private void button1_Click( object sender, EventArgs e )
        {
            try
            {
                string code = this.Code.Text;
                if( code == "" )
                {
                    return;
                }
                string UserStr = Service.GetStudentDataByCode( schoolCode, code );
                Bll.StuUser.StudentUser Use = Stu.GetUserNameForSerVice( UserStr );
                if( Use.StatusID == "1" )
                {
                    StuCode = Use.Code;
                    this.Card.Text = Use.CustomerCardSN;
                    this.Code.Text = Use.Code;
                    this.label5.Text = Use.Name;
                    this.label7.Text = Use.SexText;
                    this.label9.Text = Use.ClassCode.Substring( 10, Use.ClassCode.Length - 10 );
                    this.label11.Text = Use.ClassName;
                    this.label13.Text = Use.Telephone;
                    this.label15.Text = Use.DiningCardCode;
                    System.Net.WebClient web = new System.Net.WebClient();
                    byte[] buffer = web.DownloadData( Use.PhotoUrl );
                    web.Dispose();
                    System.IO.MemoryStream ms = new System.IO.MemoryStream( buffer );

                    this.UserInage.Image = Image.FromStream( ms );
                }
                else
                {
                    StuCode = "";
                    this.Code.Text = "";
                    this.label5.Text = "";
                    this.label7.Text = "";
                    this.label9.Text = "";
                    this.label11.Text = "";
                    this.label13.Text = "";
                    this.label15.Text = "";
                    this.UserInage.Image = (System.Drawing.Image) Properties.Resources.ResourceManager.GetObject( "timg1" );
                }
                masgerShow( Use.StatusText );
            }
            catch( Exception ex )
            {
                // MessageBox.Show( ex.Message );
                return;
            }
        }

        // videoSourcePlayer控件重绘
        private void videoSourcePlayer_Paint( object sender, PaintEventArgs e )
        {
            Graphics g = e.Graphics;
            if( m_Rect != null )
                g.DrawRectangle( new Pen( new SolidBrush( Color.Red ), 2 ), this.m_Rect );
        }

        // videoSourcePlayer内鼠标单击事件响应，保证鼠标在矩形选框内才可响应鼠标移动事件
        private void videoSourcePlayer_MouseDown( object sender, MouseEventArgs e )
        {
            System.Drawing.Point pt = videoSourcePlayer.PointToClient( Control.MousePosition );
            if( pt.X < m_Rect.X || pt.X > m_Rect.X + m_Rect.Width || pt.Y < m_Rect.Y || pt.Y > m_Rect.Y + m_Rect.Height )
                canMove = false;
            else
            {
                canMove = true;
                mouseLocationX = pt.X;    //更新当前鼠标X坐标
                mouseLocationY = pt.Y;    //更新当前鼠标Y坐标
            }
        }

        //鼠标矩形选框内移动事件响应，保证矩形选框始终在 videoSourcePlayer控件内部
        private void videoSourcePlayer_MouseMove( object sender, MouseEventArgs e )
        {
            if( canMove )
            {
                System.Drawing.Point pt = videoSourcePlayer.PointToClient( Control.MousePosition );    //获取鼠标相对pictureBox控件的坐标
                m_Rect.X += pt.X - mouseLocationX;
                m_Rect.Y += pt.Y - mouseLocationY;
                mouseLocationX = pt.X;
                mouseLocationY = pt.Y;

                m_Rect.X = m_Rect.X < 0 ? 0 : m_Rect.X;
                m_Rect.Y = m_Rect.Y < 0 ? 0 : m_Rect.Y;
                m_Rect.X = m_Rect.X + m_Rect.Width > videoSourcePlayer.Width ? videoSourcePlayer.Width - m_Rect.Width : m_Rect.X;
                m_Rect.Y = m_Rect.Y + m_Rect.Height > videoSourcePlayer.Height ? videoSourcePlayer.Height - m_Rect.Height : m_Rect.Y;

                videoSourcePlayer.Invalidate();     //重绘pictureBox控件
            }
        }

        //鼠标按键弹起事件响应，保证鼠标没有按键按下时无法响应鼠标移动事件
        private void videoSourcePlayer_MouseUp( object sender, MouseEventArgs e )
        {
            canMove = false;
        }
        /*
       * 图片裁剪方法
       * param b:需裁剪的图片位图
       * param StartX：裁剪起始X坐标
       * param StartY：裁剪起始Y坐标
       * param iWidth：裁剪宽度
       * param iHeight：裁剪高度
       * */
        public static Bitmap KiCut( Bitmap b, int StartX, int StartY, int iWidth, int iHeight )
        {
            if( b == null )
            {
                return null;
            }

            int w = b.Width;
            int h = b.Height;
            if( StartX >= w || StartY >= h )
            {
                return null;
            }

            if( StartX + iWidth > w )
            {
                iWidth = w - StartX;
            }

            if( StartY + iHeight > h )
            {
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap( iWidth, iHeight, PixelFormat.Format24bppRgb );
                Graphics g = Graphics.FromImage( bmpOut );
                g.DrawImage( b, new Rectangle( 0, 0, iWidth, iHeight ), new Rectangle( StartX, StartY, iWidth, iHeight ), GraphicsUnit.Pixel );
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }

        public static Bitmap GetThumbnail( Bitmap b, int destHeight, int destWidth )
        {
            System.Drawing.Image imgSource = b;
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放           
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if( sHeight > destHeight || sWidth > destWidth )
            {
                if( ( sWidth * destHeight ) > ( sHeight * destWidth ) )
                {
                    sW = destWidth;
                    sH = ( destWidth * sHeight ) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = ( sWidth * destHeight ) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            Bitmap outBmp = new Bitmap( destWidth, destHeight );
            Graphics g = Graphics.FromImage( outBmp );
            g.Clear( Color.Transparent );
            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage( imgSource, new Rectangle( ( destWidth - sW ) / 2, ( destHeight - sH ) / 2, sW, sH ), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel );
            g.Dispose();
            // 以下代码为保存图片时，设置压缩质量     
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter( System.Drawing.Imaging.Encoder.Quality, quality );
            encoderParams.Param[0] = encoderParam;
            imgSource.Dispose();
            return outBmp;
        }

        private void Upload_Click( object sender, EventArgs e )
        {
            UploadFile();
        }

        public void UploadFile()
        {
            if( StuCode == "" )
            {
                MessageBox.Show( "请先输入或读卡获取学生信息，点击拍照再上传！" );
                return;
            }
            if( picName == "" )
            {
                MessageBox.Show( "请先点击拍照再上传！" );
                return;
            }
            string UploadName = Path.GetFileName( picName );
            string Reslect = Service.UpdateStudentPhotoUrl( StuCode, UploadName, userCode );
            if( Reslect == "1" )
            {
                WebClient web = new WebClient();
                web.Credentials = CredentialCache.DefaultCredentials;
                string targetPath = System.Configuration.ConfigurationSettings.AppSettings["targetPath"].ToString();
                web.UploadFile( targetPath, "POST", picName );

                picName = "";
                masgerShow( "上传成功" );
            }
            else
            {
                masgerShow( "上传失败" );
            }
        }

        private void Form1_SizeChanged( object sender, EventArgs e )
        {
            FSizeChanged();
        }

        private void FSizeChanged() {
            double Height = videoSourcePlayer.Height;
            double Width = videoSourcePlayer.Width;
            if( Capabilities=="" )
            {
                return;
            }
            double ViedoHeight = double.Parse( Capabilities.Split( '*' )[0] );
            double ViedoWidth = double.Parse( Capabilities.Split( '*' )[1] );

            double ViedoBi = ViedoHeight / ViedoWidth;
            if( Width > MaxWidth && MaxWidth != 0 )
            {
                Width = MaxWidth;
            }
            if( Height > MaxHeiht && MaxHeiht != 0 )
            {
                Height = MaxHeiht;
            }

            double Bi = Height / Width;

            if( Bi > ViedoBi )
            {
                videoSourcePlayer.Height = (int) ( videoSourcePlayer.Width * ViedoBi );
            }
            else if( Bi < 0.75 )
            {
                videoSourcePlayer.Width = (int) ( videoSourcePlayer.Height / ViedoBi );
            }
          
            if( MaxWidth == 0 || MaxHeiht == 0 )
            {
                MaxHeiht = videoSourcePlayer.Height;
                MaxWidth = videoSourcePlayer.Width;
            }
        }

        //使用滑块动态调整矩形选框大小

        //最大化图片和恢复图片原始尺寸显示方式的切换
        private void track_ValueChanged( object sender, EventArgs e )
        {
            float ch = track.Value / 20.0f;
            if( ( videoSourcePlayer.Width - m_Rect.Width ) * 1.0f / ( videoSourcePlayer.Height - m_Rect.Height ) < 0.6 )  //从最快趋近于pictureBox边界的一边调节
            {
                m_Rect.Width = (int) ( videoSourcePlayer.Width * ch );
                m_Rect.Height = m_Rect.Width * 400 / 300;
            }
            else
            {
                m_Rect.Height = (int) ( videoSourcePlayer.Height * ch );
                m_Rect.Width = m_Rect.Height * 300 / 400;
            }
            //保证矩形选框不越界
            if( m_Rect.X + m_Rect.Width > videoSourcePlayer.Width )
                m_Rect.X = videoSourcePlayer.Width - m_Rect.Width;
            if( m_Rect.Y + m_Rect.Height > videoSourcePlayer.Height )
                m_Rect.Y = videoSourcePlayer.Height - m_Rect.Height;
        }

        private void Form1_FormClosed( object sender, FormClosedEventArgs e )
        {
            System.Environment.Exit( 0 );
        }

    }
}
