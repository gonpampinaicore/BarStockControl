namespace BarStockControl.Forms
{
    partial class UserForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkOnlyActive;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckedListBox clbRoles;
        private System.Windows.Forms.CheckedListBox clbPermissions;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblRoles;
        private System.Windows.Forms.Label lblPermissions;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvUsers = new DataGridView();
            txtSearch = new TextBox();
            chkOnlyActive = new CheckBox();
            txtFirstName = new TextBox();
            txtLastName = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            chkActive = new CheckBox();
            btnCreate = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            clbRoles = new CheckedListBox();
            clbPermissions = new CheckedListBox();
            lblFirstName = new Label();
            lblLastName = new Label();
            lblEmail = new Label();
            lblPassword = new Label();
            lblRoles = new Label();
            lblPermissions = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.Location = new Point(20, 20);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.Size = new Size(640, 150);
            dgvUsers.TabIndex = 0;
            dgvUsers.CellClick += dgvUsers_CellClick;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(20, 176);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // chkOnlyActive
            // 
            chkOnlyActive.Location = new Point(236, 176);
            chkOnlyActive.Name = "chkOnlyActive";
            chkOnlyActive.Size = new Size(100, 24);
            chkOnlyActive.TabIndex = 6;
            chkOnlyActive.Text = "Solo activos";
            chkOnlyActive.CheckedChanged += chkOnlyActive_CheckedChanged;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(20, 220);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(150, 23);
            txtFirstName.TabIndex = 2;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(20, 264);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(150, 23);
            txtLastName.TabIndex = 3;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(20, 308);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(150, 23);
            txtEmail.TabIndex = 4;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(20, 352);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(150, 23);
            txtPassword.TabIndex = 5;
            // 
            // chkActive
            // 
            chkActive.Location = new Point(20, 381);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(100, 24);
            chkActive.TabIndex = 7;
            chkActive.Text = "Activo";
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(20, 411);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 8;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(117, 411);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Actualizar";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(210, 411);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(303, 411);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 11;
            btnClear.Text = "Limpiar";
            btnClear.Click += btnClear_Click;
            // 
            // clbRoles
            // 
            clbRoles.Location = new Point(405, 215);
            clbRoles.Name = "clbRoles";
            clbRoles.Size = new Size(200, 94);
            clbRoles.TabIndex = 6;
            // 
            // clbPermissions
            // 
            clbPermissions.Location = new Point(405, 340);
            clbPermissions.Name = "clbPermissions";
            clbPermissions.Size = new Size(200, 94);
            clbPermissions.TabIndex = 7;
            // 
            // lblFirstName
            // 
            lblFirstName.Location = new Point(20, 202);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(150, 15);
            lblFirstName.TabIndex = 0;
            lblFirstName.Text = "Nombre";
            // 
            // lblLastName
            // 
            lblLastName.Location = new Point(20, 246);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(150, 15);
            lblLastName.TabIndex = 1;
            lblLastName.Text = "Apellido";
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(20, 294);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(150, 15);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(20, 334);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(150, 15);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Contraseña";
            // 
            // lblRoles
            // 
            lblRoles.Location = new Point(405, 184);
            lblRoles.Name = "lblRoles";
            lblRoles.Size = new Size(200, 15);
            lblRoles.TabIndex = 4;
            lblRoles.Text = "Roles";
            // 
            // lblPermissions
            // 
            lblPermissions.Location = new Point(405, 322);
            lblPermissions.Name = "lblPermissions";
            lblPermissions.Size = new Size(200, 15);
            lblPermissions.TabIndex = 5;
            lblPermissions.Text = "Permisos";
            // 
            // UserForm
            // 
            ClientSize = new Size(700, 460);
            Controls.Add(lblFirstName);
            Controls.Add(lblLastName);
            Controls.Add(lblEmail);
            Controls.Add(lblPassword);
            Controls.Add(lblRoles);
            Controls.Add(lblPermissions);
            Controls.Add(dgvUsers);
            Controls.Add(txtSearch);
            Controls.Add(chkOnlyActive);
            Controls.Add(txtFirstName);
            Controls.Add(txtLastName);
            Controls.Add(txtEmail);
            Controls.Add(txtPassword);
            Controls.Add(chkActive);
            Controls.Add(clbRoles);
            Controls.Add(clbPermissions);
            Controls.Add(btnCreate);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Name = "UserForm";
            Text = "Gestión de Usuarios";
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
