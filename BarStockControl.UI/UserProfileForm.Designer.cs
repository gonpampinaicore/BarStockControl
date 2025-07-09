namespace BarStockControl
{
    partial class UserProfileForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel mainPanel;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Padding = new System.Windows.Forms.Padding(20);

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 30);
            this.lblTitle.Text = "Mi Perfil de Usuario";

            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.lblFirstName.Location = new System.Drawing.Point(20, 80);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(100, 19);
            this.lblFirstName.Text = "Nombre:";

            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.txtFirstName.Location = new System.Drawing.Point(20, 105);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(300, 25);
            this.txtFirstName.TabIndex = 1;

            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.lblLastName.Location = new System.Drawing.Point(20, 150);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(100, 19);
            this.lblLastName.Text = "Apellido:";

            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.txtLastName.Location = new System.Drawing.Point(20, 175);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(300, 25);
            this.txtLastName.TabIndex = 2;

            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.lblEmail.Location = new System.Drawing.Point(20, 220);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(100, 19);
            this.lblEmail.Text = "Email:";

            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.txtEmail.Location = new System.Drawing.Point(20, 245);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(300, 25);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.BackColor = System.Drawing.Color.White;

            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.lblPassword.Location = new System.Drawing.Point(20, 290);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(100, 19);
            this.lblPassword.Text = "Contraseña:";

            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.txtPassword.Location = new System.Drawing.Point(20, 315);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(300, 25);
            this.txtPassword.TabIndex = 4;

            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.chkActive.Location = new System.Drawing.Point(20, 360);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(150, 23);
            this.chkActive.Text = "Usuario Activo";
            this.chkActive.Enabled = false;

            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.btnSave.Location = new System.Drawing.Point(20, 420);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(160, 420);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // 
            // UserProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(400, 500);
            this.Controls.Add(this.mainPanel);
            this.mainPanel.Controls.Add(this.lblTitle);
            this.mainPanel.Controls.Add(this.lblFirstName);
            this.mainPanel.Controls.Add(this.txtFirstName);
            this.mainPanel.Controls.Add(this.lblLastName);
            this.mainPanel.Controls.Add(this.txtLastName);
            this.mainPanel.Controls.Add(this.lblEmail);
            this.mainPanel.Controls.Add(this.txtEmail);
            this.mainPanel.Controls.Add(this.lblPassword);
            this.mainPanel.Controls.Add(this.txtPassword);
            this.mainPanel.Controls.Add(this.chkActive);
            this.mainPanel.Controls.Add(this.btnSave);
            this.mainPanel.Controls.Add(this.btnCancel);
            this.Name = "UserProfileForm";
            this.Text = "Mi Perfil";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ResumeLayout(false);
        }
    }
} 
