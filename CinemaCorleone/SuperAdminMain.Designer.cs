namespace CinemaCorleone
{
    partial class SuperAdminMain
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
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.panelSidebar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnKota = new Guna.UI2.WinForms.Guna2Button();
            this.btnDashboard = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.panelKonten = new Guna.UI2.WinForms.Guna2Panel();
            this.cbMinimize = new Guna.UI2.WinForms.Guna2ControlBox();
            this.cbMaximaze = new Guna.UI2.WinForms.Guna2ControlBox();
            this.cbClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.btnBioskop = new Guna.UI2.WinForms.Guna2Button();
            this.btnFilm = new Guna.UI2.WinForms.Guna2Button();
            this.panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.panelKonten.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // panelSidebar
            // 
            this.panelSidebar.Controls.Add(this.btnFilm);
            this.panelSidebar.Controls.Add(this.btnBioskop);
            this.panelSidebar.Controls.Add(this.btnKota);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Controls.Add(this.guna2PictureBox1);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(10);
            this.panelSidebar.Size = new System.Drawing.Size(227, 855);
            this.panelSidebar.TabIndex = 0;
            // 
            // btnKota
            // 
            this.btnKota.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKota.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKota.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKota.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKota.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKota.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnKota.FillColor = System.Drawing.Color.Transparent;
            this.btnKota.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnKota.Location = new System.Drawing.Point(10, 199);
            this.btnKota.Name = "btnKota";
            this.btnKota.Size = new System.Drawing.Size(207, 45);
            this.btnKota.TabIndex = 3;
            this.btnKota.Text = "Kota";
            this.btnKota.Click += new System.EventHandler(this.btnKota_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDashboard.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDashboard.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDashboard.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDashboard.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(10, 154);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(207, 45);
            this.btnDashboard.TabIndex = 2;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click_1);
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2PictureBox1.Image = global::CinemaCorleone.Properties.Resources.logo;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(10, 10);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(207, 144);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 1;
            this.guna2PictureBox1.TabStop = false;
            // 
            // panelKonten
            // 
            this.panelKonten.Controls.Add(this.cbMinimize);
            this.panelKonten.Controls.Add(this.cbMaximaze);
            this.panelKonten.Controls.Add(this.cbClose);
            this.panelKonten.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKonten.Location = new System.Drawing.Point(227, 0);
            this.panelKonten.Margin = new System.Windows.Forms.Padding(0);
            this.panelKonten.Name = "panelKonten";
            this.panelKonten.Size = new System.Drawing.Size(1325, 855);
            this.panelKonten.TabIndex = 1;
            // 
            // cbMinimize
            // 
            this.cbMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMinimize.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.cbMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbMinimize.FillColor = System.Drawing.Color.Silver;
            this.cbMinimize.IconColor = System.Drawing.Color.White;
            this.cbMinimize.Location = new System.Drawing.Point(1189, 0);
            this.cbMinimize.Name = "cbMinimize";
            this.cbMinimize.Size = new System.Drawing.Size(45, 29);
            this.cbMinimize.TabIndex = 5;
            // 
            // cbMaximaze
            // 
            this.cbMaximaze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMaximaze.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.cbMaximaze.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbMaximaze.FillColor = System.Drawing.Color.Silver;
            this.cbMaximaze.IconColor = System.Drawing.Color.White;
            this.cbMaximaze.Location = new System.Drawing.Point(1234, 0);
            this.cbMaximaze.Name = "cbMaximaze";
            this.cbMaximaze.Size = new System.Drawing.Size(45, 29);
            this.cbMaximaze.TabIndex = 4;
            // 
            // cbClose
            // 
            this.cbClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbClose.FillColor = System.Drawing.Color.Red;
            this.cbClose.IconColor = System.Drawing.Color.White;
            this.cbClose.Location = new System.Drawing.Point(1279, 0);
            this.cbClose.Name = "cbClose";
            this.cbClose.Size = new System.Drawing.Size(45, 29);
            this.cbClose.TabIndex = 3;
            // 
            // btnBioskop
            // 
            this.btnBioskop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBioskop.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBioskop.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBioskop.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBioskop.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBioskop.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBioskop.FillColor = System.Drawing.Color.Transparent;
            this.btnBioskop.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBioskop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBioskop.Location = new System.Drawing.Point(10, 244);
            this.btnBioskop.Name = "btnBioskop";
            this.btnBioskop.Size = new System.Drawing.Size(207, 45);
            this.btnBioskop.TabIndex = 4;
            this.btnBioskop.Text = "Bioskop";
            // 
            // btnFilm
            // 
            this.btnFilm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilm.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFilm.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnFilm.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnFilm.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnFilm.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFilm.FillColor = System.Drawing.Color.Transparent;
            this.btnFilm.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnFilm.Location = new System.Drawing.Point(10, 289);
            this.btnFilm.Name = "btnFilm";
            this.btnFilm.Size = new System.Drawing.Size(207, 45);
            this.btnFilm.TabIndex = 5;
            this.btnFilm.Text = "Film";
            this.btnFilm.Click += new System.EventHandler(this.btnFilm_Click_1);
            // 
            // SuperAdminMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.ClientSize = new System.Drawing.Size(1552, 855);
            this.Controls.Add(this.panelKonten);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SuperAdminMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SuperAdminMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SuperAdminMain_Load);
            this.panelSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.panelKonten.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel panelSidebar;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2Button btnDashboard;
        private Guna.UI2.WinForms.Guna2Panel panelKonten;
        private Guna.UI2.WinForms.Guna2ControlBox cbMinimize;
        private Guna.UI2.WinForms.Guna2ControlBox cbMaximaze;
        private Guna.UI2.WinForms.Guna2ControlBox cbClose;
        private Guna.UI2.WinForms.Guna2Button btnKota;
        private Guna.UI2.WinForms.Guna2Button btnFilm;
        private Guna.UI2.WinForms.Guna2Button btnBioskop;
    }
}