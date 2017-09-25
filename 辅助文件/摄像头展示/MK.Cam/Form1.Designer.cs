namespace MK.Cam
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            this.tscbxCameras = new System.Windows.Forms.ComboBox();
            this.btnOpenCam = new System.Windows.Forms.Button();
            this.btnCloseCam = new System.Windows.Forms.Button();
            this.btnPaizhao = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFBL = new System.Windows.Forms.ComboBox();
            this.lblfenbianlv = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // videoSourcePlayer
            // 
            this.videoSourcePlayer.Location = new System.Drawing.Point(1, 94);
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.Size = new System.Drawing.Size(1218, 742);
            this.videoSourcePlayer.TabIndex = 0;
            this.videoSourcePlayer.Text = "videoSourcePlayer1";
            this.videoSourcePlayer.VideoSource = null;
            // 
            // tscbxCameras
            // 
            this.tscbxCameras.FormattingEnabled = true;
            this.tscbxCameras.Location = new System.Drawing.Point(94, 41);
            this.tscbxCameras.Name = "tscbxCameras";
            this.tscbxCameras.Size = new System.Drawing.Size(121, 20);
            this.tscbxCameras.TabIndex = 1;
            this.tscbxCameras.SelectedIndexChanged += new System.EventHandler(this.tscbxCameras_SelectedIndexChanged);
            // 
            // btnOpenCam
            // 
            this.btnOpenCam.Location = new System.Drawing.Point(480, 40);
            this.btnOpenCam.Name = "btnOpenCam";
            this.btnOpenCam.Size = new System.Drawing.Size(75, 23);
            this.btnOpenCam.TabIndex = 2;
            this.btnOpenCam.Text = "打开摄像头";
            this.btnOpenCam.UseVisualStyleBackColor = true;
            this.btnOpenCam.Click += new System.EventHandler(this.btnOpenCam_Click);
            // 
            // btnCloseCam
            // 
            this.btnCloseCam.Location = new System.Drawing.Point(576, 40);
            this.btnCloseCam.Name = "btnCloseCam";
            this.btnCloseCam.Size = new System.Drawing.Size(75, 23);
            this.btnCloseCam.TabIndex = 3;
            this.btnCloseCam.Text = "关闭摄像头";
            this.btnCloseCam.UseVisualStyleBackColor = true;
            this.btnCloseCam.Click += new System.EventHandler(this.btnCloseCam_Click);
            // 
            // btnPaizhao
            // 
            this.btnPaizhao.Location = new System.Drawing.Point(675, 40);
            this.btnPaizhao.Name = "btnPaizhao";
            this.btnPaizhao.Size = new System.Drawing.Size(75, 23);
            this.btnPaizhao.TabIndex = 4;
            this.btnPaizhao.Text = "拍照";
            this.btnPaizhao.UseVisualStyleBackColor = true;
            this.btnPaizhao.Click += new System.EventHandler(this.btnPaizhao_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "摄像头列表";
            // 
            // cmbFBL
            // 
            this.cmbFBL.FormattingEnabled = true;
            this.cmbFBL.Location = new System.Drawing.Point(338, 41);
            this.cmbFBL.Name = "cmbFBL";
            this.cmbFBL.Size = new System.Drawing.Size(121, 20);
            this.cmbFBL.TabIndex = 6;
            this.cmbFBL.SelectedIndexChanged += new System.EventHandler(this.cmbFBL_SelectedIndexChanged);
            // 
            // lblfenbianlv
            // 
            this.lblfenbianlv.AutoSize = true;
            this.lblfenbianlv.Location = new System.Drawing.Point(269, 44);
            this.lblfenbianlv.Name = "lblfenbianlv";
            this.lblfenbianlv.Size = new System.Drawing.Size(41, 12);
            this.lblfenbianlv.TabIndex = 7;
            this.lblfenbianlv.Text = "分辨率";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 834);
            this.Controls.Add(this.lblfenbianlv);
            this.Controls.Add(this.cmbFBL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPaizhao);
            this.Controls.Add(this.btnCloseCam);
            this.Controls.Add(this.btnOpenCam);
            this.Controls.Add(this.tscbxCameras);
            this.Controls.Add(this.videoSourcePlayer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        private System.Windows.Forms.ComboBox tscbxCameras;
        private System.Windows.Forms.Button btnOpenCam;
        private System.Windows.Forms.Button btnCloseCam;
        private System.Windows.Forms.Button btnPaizhao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFBL;
        private System.Windows.Forms.Label lblfenbianlv;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}

