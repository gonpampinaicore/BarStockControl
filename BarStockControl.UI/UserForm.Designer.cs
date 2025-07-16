namespace BarStockControl.UI
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
        private System.Windows.Forms.TreeView tvUserRoles;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUserRoles;
        private System.Windows.Forms.Button btnAddPermission;
        private System.Windows.Forms.Button btnRemovePermission;
        private System.Windows.Forms.Button btnAddRole;

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
            tvUserRoles = new TreeView();
            lblFirstName = new Label();
            lblLastName = new Label();
            lblEmail = new Label();
            lblPassword = new Label();
            lblUserRoles = new Label();
            btnAddPermission = new Button();
            btnRemovePermission = new Button();
            btnAddRole = new Button();
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
            // tvUserRoles
            // 
            tvUserRoles.Location = new Point(384, 237);
            tvUserRoles.Name = "tvUserRoles";
            tvUserRoles.Size = new Size(236, 243);
            tvUserRoles.TabIndex = 6;
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
            // lblUserRoles
            // 
            lblUserRoles.Location = new Point(405, 184);
            lblUserRoles.Name = "lblUserRoles";
            lblUserRoles.Size = new Size(200, 15);
            lblUserRoles.TabIndex = 4;
            lblUserRoles.Text = "Jerarquía del Usuario";
            // 
            // btnAddPermission
            // 
            btnAddPermission.Location = new Point(371, 509);
            btnAddPermission.Name = "btnAddPermission";
            btnAddPermission.Size = new Size(132, 23);
            btnAddPermission.TabIndex = 12;
            btnAddPermission.Text = "Agregar Permiso";
            btnAddPermission.Click += btnAddPermission_Click;
            // 
            // btnRemovePermission
            // 
            btnRemovePermission.Location = new Point(509, 509);
            btnRemovePermission.Name = "btnRemovePermission";
            btnRemovePermission.Size = new Size(125, 23);
            btnRemovePermission.TabIndex = 13;
            btnRemovePermission.Text = "Quitar Elemento";
            btnRemovePermission.Click += btnRemovePermission_Click;
            // 
            // btnAddRole
            // 
            btnAddRole.Location = new Point(405, 538);
            btnAddRole.Name = "btnAddRole";
            btnAddRole.Size = new Size(200, 23);
            btnAddRole.TabIndex = 14;
            btnAddRole.Text = "Agregar Rol";
            btnAddRole.Click += btnAddRole_Click;
            // 
            // UserForm
            // 
            ClientSize = new Size(700, 586);
            Controls.Add(lblFirstName);
            Controls.Add(lblLastName);
            Controls.Add(lblEmail);
            Controls.Add(lblPassword);
            Controls.Add(lblUserRoles);
            Controls.Add(btnAddPermission);
            Controls.Add(btnRemovePermission);
            Controls.Add(btnAddRole);
            Controls.Add(dgvUsers);
            Controls.Add(txtSearch);
            Controls.Add(chkOnlyActive);
            Controls.Add(txtFirstName);
            Controls.Add(txtLastName);
            Controls.Add(txtEmail);
            Controls.Add(txtPassword);
            Controls.Add(chkActive);
            Controls.Add(tvUserRoles);
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
