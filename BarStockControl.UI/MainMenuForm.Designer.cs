namespace BarStockControl
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnUserProfile;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblRole;

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnUserProfile = new System.Windows.Forms.Button();
            this.topPanel = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();

            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Height = 90;
            this.topPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.topPanel.Controls.Add(this.lblWelcome);
            this.topPanel.Controls.Add(this.lblRole);
            this.topPanel.Controls.Add(this.btnUserProfile);
            this.topPanel.Controls.Add(this.btnLogout);

            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(10, 10);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            this.lblWelcome.Text = "";

            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.lblRole.TextAlign = ContentAlignment.TopCenter;
            this.lblRole.Text = "";
            this.lblRole.Location = new Point(220, 20);
            this.lblRole.Name = "lblRole";

            // 
            // btnUserProfile
            // 
            this.btnUserProfile.Text = "⚙️";
            this.btnUserProfile.Size = new System.Drawing.Size(40, 40);
            this.btnUserProfile.Location = new System.Drawing.Point(450, 10);
            this.btnUserProfile.TabIndex = 98;
            this.btnUserProfile.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            this.btnUserProfile.UseVisualStyleBackColor = true;
            this.btnUserProfile.Click += new System.EventHandler(this.btnUserProfile_Click);

            // 
            // btnLogout
            // 
            this.btnLogout.Text = "Cerrar sesión";
            this.btnLogout.Size = new System.Drawing.Size(120, 30);
            this.btnLogout.Location = new System.Drawing.Point(500, 10);
            this.btnLogout.TabIndex = 99;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(20, 100);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 500);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.AutoScroll = true;

            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(840, 620);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.topPanel);
            this.Name = "MainMenuForm";
            this.Text = "Menú Principal";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
        }
    }
}
