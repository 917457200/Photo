namespace Common.Utility.Winform
{
    partial class ImageCutAndZoom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonBigger = new System.Windows.Forms.Button();
            this.buttonSmaller = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPreview.Location = new System.Drawing.Point(470, 10);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(150, 150);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxPreview.TabIndex = 1;
            this.pictureBoxPreview.TabStop = false;
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.Color.Silver;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Location = new System.Drawing.Point(470, 322);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(132, 38);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "确认";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Silver;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location = new System.Drawing.Point(470, 362);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(132, 38);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonBigger
            // 
            this.buttonBigger.BackColor = System.Drawing.Color.Silver;
            this.buttonBigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBigger.Location = new System.Drawing.Point(470, 195);
            this.buttonBigger.Name = "buttonBigger";
            this.buttonBigger.Size = new System.Drawing.Size(50, 50);
            this.buttonBigger.TabIndex = 3;
            this.buttonBigger.Text = "放大";
            this.buttonBigger.UseVisualStyleBackColor = false;
            this.buttonBigger.Click += new System.EventHandler(this.buttonBigger_Click);
            // 
            // buttonSmaller
            // 
            this.buttonSmaller.BackColor = System.Drawing.Color.Silver;
            this.buttonSmaller.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSmaller.Location = new System.Drawing.Point(470, 251);
            this.buttonSmaller.Name = "buttonSmaller";
            this.buttonSmaller.Size = new System.Drawing.Size(50, 50);
            this.buttonSmaller.TabIndex = 4;
            this.buttonSmaller.Text = "缩小";
            this.buttonSmaller.UseVisualStyleBackColor = false;
            this.buttonSmaller.Click += new System.EventHandler(this.buttonSmaller_Click);
            // 
            // ImageCutAndZoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(640, 420);
            this.ControlBox = false;
            this.Controls.Add(this.buttonSmaller);
            this.Controls.Add(this.buttonBigger);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.pictureBoxPreview);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ImageCutAndZoom";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图片选取";
            this.Load += new System.EventHandler(this.ImageCutAndZoom_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageCutAndZoom_MouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageCutAndZoom_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageCutAndZoom_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// 预览图片框大小
        /// </summary>
        internal System.Windows.Forms.PictureBox pictureBoxPreview;
        /// <summary>
        /// 确认按钮
        /// </summary>
        internal System.Windows.Forms.Button buttonOK;
        /// <summary>
        /// 取消按钮
        /// </summary>
        internal System.Windows.Forms.Button buttonCancel;
        /// <summary>
        /// 放大按钮
        /// </summary>
        internal System.Windows.Forms.Button buttonBigger;
        /// <summary>
        /// 缩小按钮
        /// </summary>
        internal System.Windows.Forms.Button buttonSmaller;

    }
}