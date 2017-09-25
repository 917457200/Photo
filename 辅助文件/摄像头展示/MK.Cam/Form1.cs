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
using System.Windows.Media.Imaging;
using System.Windows;
//using Size = System.Drawing.Size;

namespace MK.Cam
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection videoDevices;
        public Form1()
        {
            InitializeComponent();
        }
        //加载摄像头
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // 枚举所有视频输入设备
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                foreach (FilterInfo device in videoDevices)
                {
                    tscbxCameras.Items.Add(device.Name);
                }

                tscbxCameras.SelectedIndex = 0;

            }
            catch (ApplicationException)
            {
                tscbxCameras.Items.Add("未发现摄像头");
                tscbxCameras.SelectedIndex = 0;
                videoDevices = null;
            }
        }

        //连接摄像头
        private void btnOpenCam_Click(object sender, EventArgs e)
        {

            btnCloseCam_Click(null, null);
            if (tscbxCameras.SelectedItem.ToString() == "未发现摄像头")
            {
                MessageBox.Show("未发现摄像头","错误提示",MessageBoxButtons.OK,MessageBoxIcon.Hand);
                return;
            }

            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[tscbxCameras.SelectedIndex].MonikerString);
            videoSource.VideoResolution = videoSource.VideoCapabilities[cmbFBL.SelectedIndex];
            
            videoSourcePlayer.VideoSource = videoSource;
            videoSourcePlayer.Start();
        }
        //关闭摄像头
        private void btnCloseCam_Click(object sender, EventArgs e)
        {
            if (videoSourcePlayer != null && videoSourcePlayer.IsRunning)
            {
                videoSourcePlayer.SignalToStop();
                videoSourcePlayer.WaitForStop();
            }
        }
        //关闭窗体
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnCloseCam_Click(null, null);
        }
        //拍照
        private void btnPaizhao_Click(object sender, EventArgs e)
        {
            try
            {
                if (videoSourcePlayer.IsRunning)
                {
                    BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                    videoSourcePlayer.GetCurrentVideoFrame().GetHbitmap(),
                                    IntPtr.Zero,
                                     Int32Rect.Empty,
                                    BitmapSizeOptions.FromEmptyOptions());
                    PngBitmapEncoder pE = new PngBitmapEncoder();
                    pE.Frames.Add(BitmapFrame.Create(bitmapSource));
                    string picName = GetImagePath() + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                    if (File.Exists(picName))
                    {
                        File.Delete(picName);
                    }
                    using (Stream stream = File.Create(picName))
                    {
                        pE.Save(stream);
                    }
                }
                else
                    MessageBox.Show("当前尚未连接摄像头", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (Exception ex)
            {
                MessageBox.Show("摄像头异常：" + ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //获取路径
        private string GetImagePath()
        {
            string personImgPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "Img";
            if (!Directory.Exists(personImgPath))
            {
                Directory.CreateDirectory(personImgPath);
            }

            return personImgPath;
        }
        //更改摄像头事件
        private void tscbxCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscbxCameras.SelectedItem.ToString() == "未发现摄像头")
            {
                cmbFBL.Items.Add("未发现摄像头");
                cmbFBL.SelectedIndex = 0;
                return;
            }
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[tscbxCameras.SelectedIndex].MonikerString);
            if (videoSource.VideoCapabilities.Count() == 0)
            {
                cmbFBL.Items.Add("摄像头异常");
                cmbFBL.SelectedIndex = 0;
                return;
            }
            cmbFBL.Items.Clear();
            foreach (AForge.Video.DirectShow.VideoCapabilities FBL in videoSource.VideoCapabilities)
            {
                cmbFBL.Items.Add(FBL.FrameSize.Width + "*" + FBL.FrameSize.Height);
            }

            cmbFBL.SelectedIndex = 0;
            btnOpenCam_Click(null,null);
        }
        //改变分辨率事件
        private void cmbFBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscbxCameras.SelectedItem.ToString() != "未发现摄像头")
                btnOpenCam_Click(null, null);
        }
    }
}
