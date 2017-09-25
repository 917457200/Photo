using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Utility.Winform;
using System.Drawing.Imaging;
namespace DemoImageCutAndZoom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Image _SelectedImage;
        private void BrowseFile_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog fd = new OpenFileDialog();
                fd.Multiselect = false;
                fd.Title = "选择头像";
                fd.Filter = "图片文件(*.jpg,*.png,*.gif,*.bmp)|*.jpg;*.png;*.gif;*.bmp";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(fd.FileName);

                    if (ImageCutAndZoom.ShowCutForm(img, new Size(150, 150), out img))
                    {

                        FilePath.Text = fd.FileName;

                        Size size = Common.Utility.ImageUtil.GetPictureLimitSize(img, Preview.Size);

                        _SelectedImage = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
                        Graphics g = Graphics.FromImage(_SelectedImage);
                        Rectangle src = new Rectangle(0, 0, img.Width, img.Height);
                        Rectangle dest = new Rectangle(0, 0, size.Width, size.Height);
                        g.DrawImage(img, dest, src, GraphicsUnit.Pixel);
                        Preview.Image = _SelectedImage;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
