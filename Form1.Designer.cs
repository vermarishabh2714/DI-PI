namespace PrivateLocker
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.fbd1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.chkStandalone = new System.Windows.Forms.CheckBox();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.bgw = new System.ComponentModel.BackgroundWorker();
            this.bgw2 = new System.ComponentModel.BackgroundWorker();
            this.pnlBtns = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkCompress = new System.Windows.Forms.CheckBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            this.pnlBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnEncrypt.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEncrypt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEncrypt.ImageIndex = 1;
            this.btnEncrypt.ImageList = this.imageList1;
            this.btnEncrypt.Location = new System.Drawing.Point(491, 212);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(256, 39);
            this.btnEncrypt.TabIndex = 1;
            this.btnEncrypt.Text = "Encryption";
            this.btnEncrypt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEncrypt.UseVisualStyleBackColor = false;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "lock_open.ico");
            this.imageList1.Images.SetKeyName(1, "lock_warning.ico");
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnDecrypt.Font = new System.Drawing.Font("Arial Black", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecrypt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDecrypt.ImageIndex = 0;
            this.btnDecrypt.ImageList = this.imageList1;
            this.btnDecrypt.Location = new System.Drawing.Point(491, 257);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(256, 39);
            this.btnDecrypt.TabIndex = 2;
            this.btnDecrypt.Text = "Decryption";
            this.btnDecrypt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDecrypt.UseVisualStyleBackColor = false;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // chkStandalone
            // 
            this.chkStandalone.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkStandalone.Location = new System.Drawing.Point(0, 0);
            this.chkStandalone.Name = "chkStandalone";
            this.chkStandalone.Size = new System.Drawing.Size(10, 20);
            this.chkStandalone.TabIndex = 3;
            this.chkStandalone.Text = "Standalone";
            this.chkStandalone.UseVisualStyleBackColor = true;
            // 
            // pbar
            // 
            this.pbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbar.Location = new System.Drawing.Point(0, 353);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(865, 25);
            this.pbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbar.TabIndex = 4;
            this.pbar.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.chkStandalone);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 378);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(865, 20);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(10, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(65, 20);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Ready..";
            // 
            // bgw
            // 
            this.bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_DoWork);
            this.bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_RunWorkerCompleted);
            // 
            // bgw2
            // 
            this.bgw2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw2_DoWork);
            this.bgw2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw2_RunWorkerCompleted);
            // 
            // pnlBtns
            // 
            this.pnlBtns.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlBtns.BackgroundImage = global::PrivateLocker.Properties.Resources.logo;
            this.pnlBtns.Controls.Add(this.pictureBox1);
            this.pnlBtns.Controls.Add(this.label1);
            this.pnlBtns.Controls.Add(this.chkCompress);
            this.pnlBtns.Controls.Add(this.btnEncrypt);
            this.pnlBtns.Controls.Add(this.btnDecrypt);
            this.pnlBtns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBtns.Location = new System.Drawing.Point(0, 0);
            this.pnlBtns.Name = "pnlBtns";
            this.pnlBtns.Size = new System.Drawing.Size(865, 353);
            this.pnlBtns.TabIndex = 6;
            this.pnlBtns.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBtns_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::PrivateLocker.Properties.Resources.padlock1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(546, 118);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(158, 75);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Goudy Stout", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(510, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 82);
            this.label1.TabIndex = 4;
            this.label1.Text = "DI-PI";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // chkCompress
            // 
            this.chkCompress.AutoSize = true;
            this.chkCompress.Checked = true;
            this.chkCompress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCompress.Location = new System.Drawing.Point(491, 302);
            this.chkCompress.Name = "chkCompress";
            this.chkCompress.Size = new System.Drawing.Size(106, 24);
            this.chkCompress.TabIndex = 3;
            this.chkCompress.Text = "Compress";
            this.chkCompress.UseVisualStyleBackColor = true;
            this.chkCompress.Visible = false;
            this.chkCompress.CheckedChanged += new System.EventHandler(this.chkCompress_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 398);
            this.Controls.Add(this.pnlBtns);
            this.Controls.Add(this.pbar);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Maiandra GD", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "DI-PI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlBtns.ResumeLayout(false);
            this.pnlBtns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog fbd1;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox chkStandalone;
        private System.Windows.Forms.ProgressBar pbar;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker bgw;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.BackgroundWorker bgw2;
        private System.Windows.Forms.Panel pnlBtns;
        private System.Windows.Forms.CheckBox chkCompress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}

