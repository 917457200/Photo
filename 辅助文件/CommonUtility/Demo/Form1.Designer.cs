namespace DemoImageCutAndZoom
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
            this.FilePath = new System.Windows.Forms.TextBox();
            this.Preview = new System.Windows.Forms.PictureBox();
            this.BrowseFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).BeginInit();
            this.SuspendLayout();
            // 
            // FilePath
            // 
            this.FilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(222)))));
            this.FilePath.Location = new System.Drawing.Point(42, 12);
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            this.FilePath.Size = new System.Drawing.Size(191, 21);
            this.FilePath.TabIndex = 3;
            // 
            // Preview
            // 
            this.Preview.BackColor = System.Drawing.Color.Transparent;
            this.Preview.Location = new System.Drawing.Point(68, 99);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(134, 102);
            this.Preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Preview.TabIndex = 4;
            this.Preview.TabStop = false;
            // 
            // BrowseFile
            // 
            this.BrowseFile.Location = new System.Drawing.Point(158, 39);
            this.BrowseFile.Name = "BrowseFile";
            this.BrowseFile.Size = new System.Drawing.Size(75, 23);
            this.BrowseFile.TabIndex = 5;
            this.BrowseFile.Text = "选择图片";
            this.BrowseFile.UseVisualStyleBackColor = true;
            this.BrowseFile.Click += new System.EventHandler(this.BrowseFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.BrowseFile);
            this.Controls.Add(this.Preview);
            this.Controls.Add(this.FilePath);
            this.Name = "Form1";
            this.Text = "Demo";
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.PictureBox Preview;
        private System.Windows.Forms.Button BrowseFile;
    }
}

