using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common.Utility;

namespace Common.Utility.Winform
{
    public partial class ImageCutAndZoom : Form
    {
        #region constructor


        public ImageCutAndZoom()
        {
            InitializeComponent();

        }
        #endregion


        #region deltegate

        #endregion


        #region variable

        #region UI参数

        /// <summary>
        /// 能流畅处理的最大位图像素大小
        /// </summary>
        public const double MAX_PIXEL_MULTY = 4000000;

        /// <summary>
        /// 界面左侧图片主区域
        /// </summary>
        public static Rectangle MAIN_REGION = new Rectangle(10,10,450, 400);
        /// <summary>
        /// 界面右侧图预览区域
        /// </summary>
        public static Rectangle PREVIEW_REGION = new Rectangle(450, 626-450, 450, 400);

        /// <summary>
        /// 当前待选择图片的区域
        /// </summary>
        internal Rectangle MainImageRegion = MAIN_REGION;

        /// <summary>
        /// 当前剪切图片的区域
        /// </summary>
        internal Rectangle CutRegion = new Rectangle(150,150,150,150);

        private Image _mainImage;

        /// <summary>
        /// 当前缩放后的待选择图片
        /// </summary>
        internal Image NowMainImage { get; set; }
        /// <summary>
        /// 当前缩放后的待选择图片的可见部分
        /// </summary>
        internal Image NowMainImageCansee { get; set; }
        /// <summary>
        /// 当前缩放后的待选择图片的可见部分的区域[决定了背景图的可见部分画在哪里]
        /// </summary>
        internal Rectangle NowMainImageCanseeRegion = new Rectangle();

        /// <summary>
        /// 当前缩放后的待选择图片的可见区域被截取的部分
        /// </summary>
        internal Image NowMainImageCanseeCuted { get; set; }
        /// <summary>
        /// 当前缩放后的待选择图片的可见区域被截取的部分的区域[决定了背景图的可见部分的被选择部分画在哪里]
        /// </summary>
        internal Rectangle NowMainImageCanseeCutedRegion = new Rectangle();
        /// <summary>
        /// 保存待选择图片原件
        /// </summary>
        internal Image MainImage
        {
            get
            {
                return _mainImage;
            }
            set
            {
                _mainImage = value;
                NowMainImage = value;
                ResetMainImageRect();
                ResetCutRect();
            }
        }

        /// <summary>
        /// 用户选择区域大小
        /// </summary>
        internal Size ChooseImageSize = new Size(150, 150);

        /// <summary>
        /// 左侧背景框画笔
        /// </summary>
        internal static Pen PenOfLeftMainRegion = new Pen(Color.DarkGray, 1);

        /// <summary>
        /// 左侧选择框画笔
        /// </summary>
        internal static Pen PenOfLeftCutRegion = new Pen(Color.Green, 1);


        #endregion

        #region 鼠标参数
        MouseState _mouseStatus = MouseState.None;

        /// <summary>
        /// 当前鼠标状态
        /// </summary>
        MouseState MouseStatus
        {
            get
            {
                return _mouseStatus;
            }
            set
            {
                _mouseStatus = value;
                switch (value)
                {
                    case MouseState.None:
                        Cursor = Cursors.Default;
                        break;
                    case MouseState.Draging:
                        Cursor = Cursors.NoMove2D;
                        break;
                    case MouseState.CanDrag:
                        Cursor = Cursors.SizeAll;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 保存上次鼠标所在位置
        /// </summary>
        Point MousePositonOld = new Point();

        #endregion

        #region 缩放参数

        /// <summary>
        /// 最大缩放倍数
        /// </summary>
        public const float ResizeMaxCount = 8f;

        /// <summary>
        /// 最小缩放倍数
        /// </summary>
        public const float ResizeMinCount = 0.125f;

        /// <summary>
        /// 每点击一下放大或者缩小的倍数
        /// </summary>
        public const float StepPerClick = 2;

        private float _resizeCount = 1f;

        /// <summary>
        /// 缩放倍数
        /// </summary>
        internal float ResizeCount
        {
            get
            {
                return _resizeCount;
            }
            set
            {
                if (value > ResizeMaxCount || value < ResizeMinCount)
                {
                    return;
                }
                else
                {
                    _resizeCount = value;
                }
            }
        }

        #endregion

        #endregion

        #region method

        /// <summary>
        /// 根据当前缩放倍率，更新图片显示区域大小,并且重绘界面
        /// </summary>
        public void ResizeMainImageRegion()
        {
            if (MainImageRegion.Width * ResizeCount * MainImageRegion.Height * ResizeCount > MAX_PIXEL_MULTY)
            {
                ResizeCount /= StepPerClick;
                return;
            }
            MainImageRegion.Size = new Size(Convert.ToInt32(MainImage.Width * ResizeCount), Convert.ToInt32(MainImage.Height * ResizeCount));
            this.Invalidate();
        }

        /// <summary>
        /// 根据当前待选择图片大小，初始化待选择图片区域（根据缩放倍数）
        /// </summary>
        public void ResetMainImageRect()
        {
            //确定起点
            int y = (MAIN_REGION.Bottom - MainImage.Height) / 2;
            int x = (MAIN_REGION.Right - MainImage.Width) / 2;
            Point location = new Point(x, y);
            //初始化
            MainImageRegion = new Rectangle(location, MainImage.Size);
        }
        /// <summary>
        /// 根据当前目标图片大小，初始化剪切图片区域
        /// </summary>
        public void ResetCutRect()
        {
            //确定起点
            int y = (MAIN_REGION.Bottom - ChooseImageSize.Height)/2;
            int x = (MAIN_REGION.Right - ChooseImageSize.Width) / 2;
            Point location = new Point(x,y);
            //初始化
            CutRegion = new Rectangle(location,ChooseImageSize);
        }

        /// <summary>
        /// 重写窗体绘制函数
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            bool IsDrawMainImg = true;//是否绘制待选择图片的可见区域
            bool IsDrawMainImgChoose = true;//是否绘制待选择图片的可见区域被选择的部分

            //--------------------------绘制左侧拖拽区域外侧框线--------------------------
            e.Graphics.DrawRectangle(PenOfLeftMainRegion,MAIN_REGION);
            //--------------------------计算当前背景图--------------------------
            NowMainImage = Utility.ImageUtil.KiResizeImage(MainImage, MainImageRegion.Width, MainImageRegion.Height, 0);

            //--------------------------绘制当前背景图--------------------------
            //1:计算当前可见的是图片的哪部分,并截取对应部分的图片

            Rectangle InsectRectToImage = MainImageRegion;//相交部分在图片坐标系的表示
            //a)如果左侧区域包含了背景图，则可见部分就是整个背景图
            if (MAIN_REGION.Contains(MainImageRegion))
            {
                NowMainImageCansee = NowMainImage;
                NowMainImageCanseeRegion = MainImageRegion;
            }
            //b)如果背景图包含了左侧区域，则可见部分就是左侧区域
            else if (MainImageRegion.Contains(MAIN_REGION))
            {
                NowMainImageCansee = Utility.ImageUtil.KiCut(NowMainImage, MAIN_REGION.Location.X - MainImageRegion.X,
                    MAIN_REGION.Location.Y - MainImageRegion.Y, MAIN_REGION.Width, MAIN_REGION.Height);
                NowMainImageCanseeRegion = MAIN_REGION;
            }
            //c)如果2个矩形相交，则计算出相交部分的矩形
            else if (MainImageRegion.IntersectsWith(MAIN_REGION))
            {
                //计算出相交部分的矩形，其坐标系是基于form的
                InsectRectToImage.Intersect(MAIN_REGION);
                NowMainImageCanseeRegion = InsectRectToImage;
                //对基于图片的坐标进行转换
                //首先把相对位置矩形放到（0,0）位置
                InsectRectToImage.Offset(-InsectRectToImage.X, -InsectRectToImage.Y);
                //然后针对相交矩形在容器中的位置以及目标图片矩形在容器中的位置，计算相交矩形在图片矩形中的表示
                InsectRectToImage.Offset((NowMainImageCanseeRegion.X - MainImageRegion.Location.X), (NowMainImageCanseeRegion.Y - MainImageRegion.Location.Y));

                NowMainImageCansee = Utility.ImageUtil.KiCut(NowMainImage, InsectRectToImage.Location.X,
                    InsectRectToImage.Location.Y, InsectRectToImage.Width, InsectRectToImage.Height);
            }
            else
            {
                IsDrawMainImg=false;
            }
            if (IsDrawMainImg)
            {
                e.Graphics.DrawImage(NowMainImageCansee, NowMainImageCanseeRegion);
            }



            //--------------------------绘制背景图被选择框选择部分的效果--------------------------
            //1:计算选择区截取的是哪部分图片，截取之
            Rectangle InsectRectToImageCuted = CutRegion;
            //a)如果剪切区域包含了图片的可见区域，则可见部分就是整个背景图的可见区域
            if (CutRegion.Contains(NowMainImageCanseeRegion))
            {
                NowMainImageCanseeCuted = NowMainImageCansee;
                NowMainImageCanseeCutedRegion = NowMainImageCanseeRegion;
            }
            //b)如果图片的可见区域包含了剪切区域，则可见部分就是剪切区域
            else if (NowMainImageCanseeRegion.Contains(CutRegion))
            {
                NowMainImageCanseeCuted = Utility.ImageUtil.KiCut(NowMainImageCansee, CutRegion.Location.X - NowMainImageCanseeRegion.X,
                    CutRegion.Location.Y - NowMainImageCanseeRegion.Y, CutRegion.Width, CutRegion.Height);
                NowMainImageCanseeCutedRegion = CutRegion;
            }
            //c)如果2个矩形相交，则计算出相交部分的矩形
            else if (NowMainImageCanseeRegion.IntersectsWith(CutRegion))
            {
                //计算出相交部分的矩形，其坐标系是基于form的
                InsectRectToImageCuted.Intersect(NowMainImageCanseeRegion);
                NowMainImageCanseeCutedRegion = InsectRectToImageCuted;
                //对基于图片的坐标进行转换
                //首先把相对位置矩形放到（0,0）位置
                InsectRectToImageCuted.Offset(-InsectRectToImageCuted.X, -InsectRectToImageCuted.Y);
                //然后针对相交矩形在容器中的位置以及目标图片矩形在容器中的位置，计算相交矩形在图片矩形中的表示
                InsectRectToImageCuted.Offset((NowMainImageCanseeCutedRegion.X - NowMainImageCanseeRegion.Location.X),
                    (NowMainImageCanseeCutedRegion.Y - NowMainImageCanseeRegion.Location.Y));

                NowMainImageCanseeCuted = Utility.ImageUtil.KiCut(NowMainImageCansee, InsectRectToImageCuted.Location.X,
                    InsectRectToImageCuted.Location.Y, InsectRectToImageCuted.Width, InsectRectToImageCuted.Height);
            }
            else
            {
                IsDrawMainImgChoose = false;
            }
            if (IsDrawMainImgChoose)
            {
                e.Graphics.DrawImage(NowMainImageCanseeCuted, NowMainImageCanseeCutedRegion);
            }

            //--------------------------绘制左侧图片选取区域框线--------------------------
            e.Graphics.DrawRectangle(PenOfLeftCutRegion, CutRegion);
            //--------------------------绘制预览框里的内容--------------------------
            pictureBoxPreview.Image = NowMainImageCanseeCuted;
        }

       

        /// <summary>
        /// 打开图片缩放窗体
        /// </summary>
        /// <param name="img">要缩放的图片原件</param>
        /// <param name="limitSize">选取图片目标大小,不能大于ImageCutAndZoom.MAIN_REGION_SIZE参数</param>
        /// <param name="imgOutPut">最终选择的图片</param>
        /// <returns>窗体成功打开则返回true</returns>
        public static bool ShowCutForm(Image img, Size limitSize,out Image imgOutPut)
        {
            //返回参数初始化
            imgOutPut = null;
            try
            {
                ////检测是否可以成功打开窗体
                ////判断图片不为空并且目标大小不超过预设值
                //if (limitSize.Width > MAIN_REGION.Width || limitSize.Height > MAIN_REGION.Height
                //    ||img==null)
                //{
                //    return false;
                //}
                //建立窗体
                ImageCutAndZoom CutForm = new ImageCutAndZoom();
                //根据传入参数，设置本次预览区域的实际大小
                CutForm.pictureBoxPreview.Size = limitSize;
                CutForm.ChooseImageSize = limitSize;
                CutForm.MainImage = img;

                var Result =CutForm.ShowDialog();
                if (Result == DialogResult.OK)
                {
                    imgOutPut = CutForm.pictureBoxPreview.Image;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region eventHandle

        #region 鼠标


        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageCutAndZoom_MouseMove(object sender, MouseEventArgs e)
        {
            
            //首先更新鼠标状态
            if (NowMainImageCanseeRegion.Contains(e.X, e.Y))
            {
                if (MouseStatus!= MouseState.Draging)
                {
                    MouseStatus = MouseState.CanDrag;
                }
            }
            else
            {
                MouseStatus = MouseState.None;
            }
            //鼠标在移动的时候，根据当前的鼠标状态进行逻辑处理
            switch (MouseStatus)
            {
                case MouseState.Draging:
                    //计算鼠标移动的距离
                    Point offset = new Point(e.Location.X - MousePositonOld.X,e.Location.Y - MousePositonOld.Y);
                    MainImageRegion.Offset(offset);
                    this.Invalidate();
                    break;
                default:
                    break;
            }
            MousePositonOld = e.Location;
         
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageCutAndZoom_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (MouseStatus == MouseState.CanDrag || MouseStatus == MouseState.Draging))
            {
                MouseStatus = MouseState.Draging;
            }
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageCutAndZoom_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MouseStatus == MouseState.Draging)
            {
                MouseStatus = MouseState.CanDrag;
            }
        }

        #endregion

        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageCutAndZoom_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 图片放大按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBigger_Click(object sender, EventArgs e)
        {
            ResizeCount *= StepPerClick;
            ResizeMainImageRegion();
        }

        /// <summary>
        /// 图片缩小按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSmaller_Click(object sender, EventArgs e)
        {

            ResizeCount /= StepPerClick;
            ResizeMainImageRegion();
        }
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WinformParam.WM_NCLBUTTONDBLCLK)
            {
                return;
            }
            base.WndProc(ref m);
        }


        #endregion

     
       
    }

    /// <summary>
    /// 鼠标状态
    /// </summary>
    internal enum MouseState
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 正在拖动状态
        /// </summary>
        Draging,
        /// <summary>
        /// 可以拖动状态
        /// </summary>
        CanDrag
    }
}
