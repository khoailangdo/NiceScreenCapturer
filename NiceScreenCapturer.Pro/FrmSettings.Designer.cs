namespace NiceScreenCapturer
{
    partial class FrmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        //public System.ComponentModel.ComponentResourceManager resources1 = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CaptureRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CaptureScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RbVietnamese = new System.Windows.Forms.RadioButton();
            this.RbEnglish = new System.Windows.Forms.RadioButton();
            this.ChkEnableDrawMarkRectangle = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RbSaveAsImage = new System.Windows.Forms.RadioButton();
            this.RbClipboard = new System.Windows.Forms.RadioButton();
            this.ChkStartWithWindows = new System.Windows.Forms.CheckBox();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnApply = new System.Windows.Forms.Button();
            this.toolStripMenuItemReload = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CaptureRegionToolStripMenuItem,
            this.CaptureScreenToolStripMenuItem,
            this.toolStripSeparator1,
            this.SettingsToolStripMenuItem,
            this.toolStripMenuItemReload,
            this.AboutToolStripMenuItem,
            this.toolStripSeparator2,
            this.ExitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(268, 188);
            // 
            // CaptureRegionToolStripMenuItem
            // 
            this.CaptureRegionToolStripMenuItem.Name = "CaptureRegionToolStripMenuItem";
            this.CaptureRegionToolStripMenuItem.Size = new System.Drawing.Size(267, 24);
            this.CaptureRegionToolStripMenuItem.Text = "Capture Region [Ctrl+Alt+A]";
            this.CaptureRegionToolStripMenuItem.Click += new System.EventHandler(this.CaptureRegionToolStripMenuItem_Click);
            // 
            // CaptureScreenToolStripMenuItem
            // 
            this.CaptureScreenToolStripMenuItem.Name = "CaptureScreenToolStripMenuItem";
            this.CaptureScreenToolStripMenuItem.Size = new System.Drawing.Size(267, 24);
            this.CaptureScreenToolStripMenuItem.Text = "Capture Screen [Ctrl+Alt+S]";
            this.CaptureScreenToolStripMenuItem.Click += new System.EventHandler(this.CaptureScreenToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(264, 6);
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(267, 24);
            this.SettingsToolStripMenuItem.Text = "Settings";
            this.SettingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(267, 24);
            this.AboutToolStripMenuItem.Text = "About";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(264, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(267, 24);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(475, 350);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.ChkEnableDrawMarkRectangle);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.ChkStartWithWindows);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(467, 321);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RbVietnamese);
            this.groupBox2.Controls.Add(this.RbEnglish);
            this.groupBox2.Location = new System.Drawing.Point(20, 205);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(415, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Interface languages";
            // 
            // RbVietnamese
            // 
            this.RbVietnamese.AutoSize = true;
            this.RbVietnamese.Location = new System.Drawing.Point(238, 45);
            this.RbVietnamese.Name = "RbVietnamese";
            this.RbVietnamese.Size = new System.Drawing.Size(93, 21);
            this.RbVietnamese.TabIndex = 5;
            this.RbVietnamese.TabStop = true;
            this.RbVietnamese.Text = "Tiếng Việt";
            this.RbVietnamese.UseVisualStyleBackColor = true;
            // 
            // RbEnglish
            // 
            this.RbEnglish.AutoSize = true;
            this.RbEnglish.Checked = true;
            this.RbEnglish.Location = new System.Drawing.Point(52, 45);
            this.RbEnglish.Name = "RbEnglish";
            this.RbEnglish.Size = new System.Drawing.Size(75, 21);
            this.RbEnglish.TabIndex = 4;
            this.RbEnglish.TabStop = true;
            this.RbEnglish.Text = "English";
            this.RbEnglish.UseVisualStyleBackColor = true;
            // 
            // ChkEnableDrawMarkRectangle
            // 
            this.ChkEnableDrawMarkRectangle.AutoSize = true;
            this.ChkEnableDrawMarkRectangle.Location = new System.Drawing.Point(72, 67);
            this.ChkEnableDrawMarkRectangle.Name = "ChkEnableDrawMarkRectangle";
            this.ChkEnableDrawMarkRectangle.Size = new System.Drawing.Size(236, 21);
            this.ChkEnableDrawMarkRectangle.TabIndex = 1;
            this.ChkEnableDrawMarkRectangle.Text = "Enable draw Red mark rectangle";
            this.ChkEnableDrawMarkRectangle.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RbSaveAsImage);
            this.groupBox1.Controls.Add(this.RbClipboard);
            this.groupBox1.Location = new System.Drawing.Point(20, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 97);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stored image as";
            // 
            // RbSaveAsImage
            // 
            this.RbSaveAsImage.AutoSize = true;
            this.RbSaveAsImage.Location = new System.Drawing.Point(238, 41);
            this.RbSaveAsImage.Name = "RbSaveAsImage";
            this.RbSaveAsImage.Size = new System.Drawing.Size(122, 21);
            this.RbSaveAsImage.TabIndex = 3;
            this.RbSaveAsImage.Text = "Save as image";
            this.RbSaveAsImage.UseVisualStyleBackColor = true;
            // 
            // RbClipboard
            // 
            this.RbClipboard.AutoSize = true;
            this.RbClipboard.Checked = true;
            this.RbClipboard.Location = new System.Drawing.Point(52, 41);
            this.RbClipboard.Name = "RbClipboard";
            this.RbClipboard.Size = new System.Drawing.Size(89, 21);
            this.RbClipboard.TabIndex = 2;
            this.RbClipboard.TabStop = true;
            this.RbClipboard.Text = "Clipboard";
            this.RbClipboard.UseVisualStyleBackColor = true;
            // 
            // ChkStartWithWindows
            // 
            this.ChkStartWithWindows.AutoSize = true;
            this.ChkStartWithWindows.Location = new System.Drawing.Point(72, 33);
            this.ChkStartWithWindows.Name = "ChkStartWithWindows";
            this.ChkStartWithWindows.Size = new System.Drawing.Size(195, 21);
            this.ChkStartWithWindows.TabIndex = 0;
            this.ChkStartWithWindows.Text = "Run when computer starts";
            this.ChkStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(224, 361);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 28);
            this.BtnOk.TabIndex = 6;
            this.BtnOk.Text = "OK";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(314, 361);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 28);
            this.BtnCancel.TabIndex = 7;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnApply
            // 
            this.BtnApply.Location = new System.Drawing.Point(402, 361);
            this.BtnApply.Name = "BtnApply";
            this.BtnApply.Size = new System.Drawing.Size(75, 28);
            this.BtnApply.TabIndex = 8;
            this.BtnApply.Text = "Apply";
            this.BtnApply.UseVisualStyleBackColor = true;
            this.BtnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // toolStripMenuItemReload
            // 
            this.toolStripMenuItemReload.Name = "toolStripMenuItemReload";
            this.toolStripMenuItemReload.Size = new System.Drawing.Size(267, 24);
            this.toolStripMenuItemReload.Text = "Reload";
            this.toolStripMenuItemReload.Click += new System.EventHandler(this.toolStripMenuItemReload_Click);
            // 
            // FrmSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(482, 402);
            this.Controls.Add(this.BtnApply);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.ShowInTaskbar = false;
            this.Text = "Preferences";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSettings_FormClosing);
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.Resize += new System.EventHandler(this.FrmSettings_Resize);
            this.contextMenuStrip.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem CaptureRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CaptureScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RbSaveAsImage;
        private System.Windows.Forms.RadioButton RbClipboard;
        private System.Windows.Forms.CheckBox ChkStartWithWindows;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnApply;
        private System.Windows.Forms.CheckBox ChkEnableDrawMarkRectangle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton RbVietnamese;
        private System.Windows.Forms.RadioButton RbEnglish;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReload;
    }
}

