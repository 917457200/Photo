namespace EastElite.Photos
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
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.DataPanel1 = new System.Windows.Forms.Panel();
            this.Codeyes = new System.Windows.Forms.Button();
            this.InHand = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ReaCard = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.UserInage = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.Code = new System.Windows.Forms.TextBox();
            this.Card = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            this.btnPaizhao = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Upload = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.track = new System.Windows.Forms.TrackBar();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DataPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UserInage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.track)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataPanel1
            // 
            this.DataPanel1.Controls.Add(this.Codeyes);
            this.DataPanel1.Controls.Add(this.InHand);
            this.DataPanel1.Controls.Add(this.label2);
            this.DataPanel1.Controls.Add(this.ReaCard);
            this.DataPanel1.Controls.Add(this.richTextBox1);
            this.DataPanel1.Controls.Add(this.UserInage);
            this.DataPanel1.Controls.Add(this.label15);
            this.DataPanel1.Controls.Add(this.label14);
            this.DataPanel1.Controls.Add(this.label13);
            this.DataPanel1.Controls.Add(this.label12);
            this.DataPanel1.Controls.Add(this.label11);
            this.DataPanel1.Controls.Add(this.label10);
            this.DataPanel1.Controls.Add(this.label9);
            this.DataPanel1.Controls.Add(this.label8);
            this.DataPanel1.Controls.Add(this.label7);
            this.DataPanel1.Controls.Add(this.label6);
            this.DataPanel1.Controls.Add(this.label5);
            this.DataPanel1.Controls.Add(this.label4);
            this.DataPanel1.Controls.Add(this.label3);
            this.DataPanel1.Controls.Add(this.label58);
            this.DataPanel1.Controls.Add(this.Code);
            this.DataPanel1.Controls.Add(this.Card);
            this.DataPanel1.Controls.Add(this.label1);
            this.DataPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.DataPanel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DataPanel1.Location = new System.Drawing.Point(0, 0);
            this.DataPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.DataPanel1.Name = "DataPanel1";
            this.DataPanel1.Size = new System.Drawing.Size(201, 521);
            this.DataPanel1.TabIndex = 80;
            // 
            // Codeyes
            // 
            this.Codeyes.BackColor = System.Drawing.Color.DodgerBlue;
            this.Codeyes.ForeColor = System.Drawing.Color.White;
            this.Codeyes.Location = new System.Drawing.Point(148, 197);
            this.Codeyes.Name = "Codeyes";
            this.Codeyes.Size = new System.Drawing.Size(37, 21);
            this.Codeyes.TabIndex = 105;
            this.Codeyes.Text = "确定";
            this.Codeyes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Codeyes.UseVisualStyleBackColor = false;
            this.Codeyes.Click += new System.EventHandler(this.button1_Click);
            // 
            // InHand
            // 
            this.InHand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InHand.BackColor = System.Drawing.Color.DodgerBlue;
            this.InHand.ForeColor = System.Drawing.Color.White;
            this.InHand.Location = new System.Drawing.Point(92, 484);
            this.InHand.Name = "InHand";
            this.InHand.Size = new System.Drawing.Size(93, 26);
            this.InHand.TabIndex = 85;
            this.InHand.Text = "手动输入模式";
            this.InHand.UseVisualStyleBackColor = false;
            this.InHand.Click += new System.EventHandler(this.InHand_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(64, 199);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 104;
            this.label2.Text = "*";
            // 
            // ReaCard
            // 
            this.ReaCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ReaCard.BackColor = System.Drawing.Color.DodgerBlue;
            this.ReaCard.ForeColor = System.Drawing.Color.White;
            this.ReaCard.Location = new System.Drawing.Point(12, 484);
            this.ReaCard.Name = "ReaCard";
            this.ReaCard.Size = new System.Drawing.Size(70, 26);
            this.ReaCard.TabIndex = 85;
            this.ReaCard.Text = "读卡模式";
            this.ReaCard.UseVisualStyleBackColor = false;
            this.ReaCard.Click += new System.EventHandler(this.ReaCard_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(12, 376);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(173, 102);
            this.richTextBox1.TabIndex = 103;
            this.richTextBox1.Text = "";
            // 
            // UserInage
            // 
            this.UserInage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UserInage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserInage.Image = ((System.Drawing.Image)(resources.GetObject("UserInage.Image")));
            this.UserInage.Location = new System.Drawing.Point(42, 9);
            this.UserInage.Name = "UserInage";
            this.UserInage.Size = new System.Drawing.Size(119, 159);
            this.UserInage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.UserInage.TabIndex = 102;
            this.UserInage.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(75, 357);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(0, 12);
            this.label15.TabIndex = 101;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(13, 357);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 101;
            this.label14.Text = "餐厅卡号";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(75, 331);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 12);
            this.label13.TabIndex = 101;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(13, 331);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 101;
            this.label12.Text = "联系电话";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(75, 305);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 12);
            this.label11.TabIndex = 101;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(13, 304);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 101;
            this.label10.Text = "所在班级";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(75, 277);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 12);
            this.label9.TabIndex = 101;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(13, 277);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 101;
            this.label8.Text = "班级代码";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(75, 250);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 12);
            this.label7.TabIndex = 101;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(13, 250);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 101;
            this.label6.Text = "学生性别";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(75, 225);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 101;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(13, 225);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 101;
            this.label4.Text = "学生姓名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(13, 200);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 101;
            this.label3.Text = "学生学号";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label58.ForeColor = System.Drawing.Color.Red;
            this.label58.Location = new System.Drawing.Point(63, 177);
            this.label58.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(11, 12);
            this.label58.TabIndex = 100;
            this.label58.Text = "*";
            // 
            // Code
            // 
            this.Code.BackColor = System.Drawing.Color.White;
            this.Code.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Code.Location = new System.Drawing.Point(77, 197);
            this.Code.Margin = new System.Windows.Forms.Padding(4);
            this.Code.MaxLength = 12;
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Size = new System.Drawing.Size(71, 21);
            this.Code.TabIndex = 99;
            // 
            // Card
            // 
            this.Card.BackColor = System.Drawing.Color.White;
            this.Card.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Card.Location = new System.Drawing.Point(77, 174);
            this.Card.Margin = new System.Windows.Forms.Padding(4);
            this.Card.MaxLength = 12;
            this.Card.Name = "Card";
            this.Card.ReadOnly = true;
            this.Card.Size = new System.Drawing.Size(108, 21);
            this.Card.TabIndex = 99;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 178);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 81;
            this.label1.Text = "IC  卡号";
            // 
            // videoSourcePlayer
            // 
            this.videoSourcePlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.videoSourcePlayer.Location = new System.Drawing.Point(208, 8);
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.Size = new System.Drawing.Size(630, 450);
            this.videoSourcePlayer.TabIndex = 82;
            this.videoSourcePlayer.TabStop = false;
            this.videoSourcePlayer.Text = "videoSourcePlayer1";
            this.videoSourcePlayer.VideoSource = null;
            this.videoSourcePlayer.Paint += new System.Windows.Forms.PaintEventHandler(this.videoSourcePlayer_Paint);
            this.videoSourcePlayer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.videoSourcePlayer_MouseDown);
            this.videoSourcePlayer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.videoSourcePlayer_MouseMove);
            this.videoSourcePlayer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.videoSourcePlayer_MouseUp);
            // 
            // btnPaizhao
            // 
            this.btnPaizhao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPaizhao.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPaizhao.ForeColor = System.Drawing.Color.White;
            this.btnPaizhao.Location = new System.Drawing.Point(22, 351);
            this.btnPaizhao.Name = "btnPaizhao";
            this.btnPaizhao.Size = new System.Drawing.Size(150, 40);
            this.btnPaizhao.TabIndex = 85;
            this.btnPaizhao.Text = "拍照";
            this.btnPaizhao.UseVisualStyleBackColor = false;
            this.btnPaizhao.Click += new System.EventHandler(this.btnPaizhao_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(15, 8);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 87;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Upload
            // 
            this.Upload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Upload.BackColor = System.Drawing.Color.DodgerBlue;
            this.Upload.ForeColor = System.Drawing.Color.White;
            this.Upload.Location = new System.Drawing.Point(22, 401);
            this.Upload.Name = "Upload";
            this.Upload.Size = new System.Drawing.Size(150, 40);
            this.Upload.TabIndex = 85;
            this.Upload.Text = "上传";
            this.Upload.UseVisualStyleBackColor = false;
            this.Upload.Click += new System.EventHandler(this.Upload_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(29, 324);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 88;
            this.checkBox1.Text = "拍照并上传";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // track
            // 
            this.track.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.track.Location = new System.Drawing.Point(15, 248);
            this.track.Margin = new System.Windows.Forms.Padding(0);
            this.track.Maximum = 20;
            this.track.Name = "track";
            this.track.Size = new System.Drawing.Size(150, 42);
            this.track.TabIndex = 89;
            this.track.ValueChanged += new System.EventHandler(this.track_ValueChanged);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(27, 297);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 12);
            this.label16.TabIndex = 90;
            this.label16.Text = "调整选择框大小";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(2, 253);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 12);
            this.label17.TabIndex = 91;
            this.label17.Text = "小";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(157, 253);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 12);
            this.label18.TabIndex = 92;
            this.label18.Text = "大";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.track);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.btnPaizhao);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.Upload);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Location = new System.Drawing.Point(844, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(179, 521);
            this.panel1.TabIndex = 93;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1020, 521);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.videoSourcePlayer);
            this.Controls.Add(this.DataPanel1);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "壹键通学生照片采集系统";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.DataPanel1.ResumeLayout(false);
            this.DataPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UserInage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.track)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel DataPanel1;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.TextBox Card;
        private System.Windows.Forms.Label label1;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        private System.Windows.Forms.Button btnPaizhao;
        private System.Windows.Forms.Button ReaCard;
        private System.Windows.Forms.Button InHand;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox UserInage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Code;
        private System.Windows.Forms.Button Codeyes;
        private System.Windows.Forms.Button Upload;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TrackBar track;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

    }
}

