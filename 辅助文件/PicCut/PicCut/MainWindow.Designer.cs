namespace PicCut
{
    partial class MainWindow
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
            this.picArea = new System.Windows.Forms.PictureBox();
            this.load = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.track = new System.Windows.Forms.TrackBar();
            this.maxPic = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picArea)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track)).BeginInit();
            this.SuspendLayout();
            // 
            // picArea
            // 
            this.picArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.picArea.Location = new System.Drawing.Point(5, 5);
            this.picArea.Name = "picArea";
            this.picArea.Size = new System.Drawing.Size(512, 374);
            this.picArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picArea.TabIndex = 0;
            this.picArea.TabStop = false;
            this.picArea.SizeChanged += new System.EventHandler(this.picArea_SizeChanged);
            this.picArea.Paint += new System.Windows.Forms.PaintEventHandler(this.picArea_Paint);
            this.picArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picArea_MouseDown);
            this.picArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picArea_MouseMove);
            this.picArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picArea_MouseUp);
            // 
            // load
            // 
            this.load.Location = new System.Drawing.Point(45, 395);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(60, 20);
            this.load.TabIndex = 1;
            this.load.Text = "Load...";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(415, 395);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(60, 20);
            this.ok.TabIndex = 3;
            this.ok.Text = "Save...";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.picArea);
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(530, 390);
            this.panel.TabIndex = 4;
            // 
            // track
            // 
            this.track.Location = new System.Drawing.Point(145, 395);
            this.track.Margin = new System.Windows.Forms.Padding(0);
            this.track.Maximum = 20;
            this.track.Name = "track";
            this.track.Size = new System.Drawing.Size(150, 42);
            this.track.TabIndex = 5;
            this.track.ValueChanged += new System.EventHandler(this.load_Click);
            // 
            // maxPic
            // 
            this.maxPic.AutoSize = true;
            this.maxPic.Location = new System.Drawing.Point(310, 395);
            this.maxPic.Name = "maxPic";
            this.maxPic.Size = new System.Drawing.Size(102, 16);
            this.maxPic.TabIndex = 6;
            this.maxPic.Text = "Image Maximum";
            this.maxPic.UseVisualStyleBackColor = true;
            this.maxPic.Click += new System.EventHandler(this.maxPic_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 422);
            this.Controls.Add(this.maxPic);
            this.Controls.Add(this.track);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.load);
            this.Name = "MainWindow";
            this.Text = "PictureCut";
            ((System.ComponentModel.ISupportInitialize)(this.picArea)).EndInit();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.track)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox picArea;
        private System.Windows.Forms.Button load;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TrackBar track;
        private System.Windows.Forms.CheckBox maxPic;
    }
}

