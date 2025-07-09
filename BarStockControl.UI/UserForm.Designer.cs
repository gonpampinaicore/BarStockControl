namespace BarStockControl.Forms.Users
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
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridView dgvRoles;
        private System.Windows.Forms.TextBox txtRoleSearch;
        private System.Windows.Forms.Label lblRoleSearch;
        private System.Windows.Forms.Button btnGoToRoles;
        private System.Windows.Forms.Button btnGoToPermissions;
        private System.Windows.Forms.Button btnGoToPermissionItems;
        private System.Windows.Forms.Button btnGoToMainMenu;
        private System.Windows.Forms.Button btnClear;

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
            lblFirstName = new Label();
            lblLastName = new Label();
            lblEmail = new Label();
            lblPassword = new Label();
            lblSearch = new Label();
            txtRoleSearch = new TextBox();
            lblRoleSearch = new Label();
            dgvRoles = new DataGridView();
            colSelected = new DataGridViewCheckBoxColumn();
            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            btnGoToRoles = new Button();
            btnGoToPermissions = new Button();
            btnGoToPermissionItems = new Button();
            btnGoToMainMenu = new Button();
            btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRoles).BeginInit();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.Location = new Point(20, 50);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.Size = new Size(673, 205);
            dgvUsers.TabIndex = 0;
            dgvUsers.CellClick += dgvUsers_CellClick;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(100, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // chkOnlyActive
            // 
            chkOnlyActive.Location = new Point(320, 20);
            chkOnlyActive.Name = "chkOnlyActive";
            chkOnlyActive.Size = new Size(104, 24);
            chkOnlyActive.TabIndex = 2;
            chkOnlyActive.Text = "Solo activos";
            chkOnlyActive.CheckedChanged += chkOnlyActive_CheckedChanged;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(100, 270);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(100, 23);
            txtFirstName.TabIndex = 3;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(100, 300);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(100, 23);
            txtLastName.TabIndex = 4;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(100, 330);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(100, 23);
            txtEmail.TabIndex = 5;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(100, 360);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(100, 23);
            txtPassword.TabIndex = 6;
            // 
            // chkActive
            // 
            chkActive.Location = new Point(100, 390);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(104, 24);
            chkActive.TabIndex = 7;
            chkActive.Text = "Activo";
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(250, 270);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 8;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(250, 310);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Actualizar";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(250, 350);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;
            // 
            // lblFirstName
            // 
            lblFirstName.Location = new Point(20, 273);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(100, 23);
            lblFirstName.TabIndex = 12;
            lblFirstName.Text = "Nombre:";
            // 
            // lblLastName
            // 
            lblLastName.Location = new Point(20, 303);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(100, 23);
            lblLastName.TabIndex = 13;
            lblLastName.Text = "Apellido:";
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(20, 333);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 14;
            lblEmail.Text = "Email:";
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(20, 363);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 15;
            lblPassword.Text = "Contraseña:";
            // 
            // lblSearch
            // 
            lblSearch.Location = new Point(20, 23);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(100, 23);
            lblSearch.TabIndex = 11;
            lblSearch.Text = "Buscar:";
            // 
            // txtRoleSearch
            // 
            txtRoleSearch.Location = new Point(100, 420);
            txtRoleSearch.Name = "txtRoleSearch";
            txtRoleSearch.Size = new Size(200, 23);
            txtRoleSearch.TabIndex = 16;
            txtRoleSearch.TextChanged += txtRoleSearch_TextChanged;
            // 
            // lblRoleSearch
            // 
            lblRoleSearch.Location = new Point(20, 423);
            lblRoleSearch.Name = "lblRoleSearch";
            lblRoleSearch.Size = new Size(80, 23);
            lblRoleSearch.TabIndex = 17;
            lblRoleSearch.Text = "Buscar rol:";
            // 
            // dgvRoles
            // 
            dgvRoles.AllowUserToAddRows = false;
            dgvRoles.Columns.AddRange(new DataGridViewColumn[] { colSelected, colId, colName });
            dgvRoles.Location = new Point(20, 450);
            dgvRoles.Name = "dgvRoles";
            dgvRoles.RowHeadersVisible = false;
            dgvRoles.Size = new Size(340, 153);
            dgvRoles.TabIndex = 17;
            // 
            // colSelected
            // 
            colSelected.HeaderText = "Seleccionado";
            colSelected.Name = "Selected";
            colSelected.Width = 50;
            // 
            // colId
            // 
            colId.HeaderText = "ID";
            colId.Name = "Id";
            colId.Visible = false;
            // 
            // colName
            // 
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colName.HeaderText = "Rol";
            colName.Name = "Name";
            // 
            // btnGoToRoles
            // 
            btnGoToRoles.Location = new Point(500, 270);
            btnGoToRoles.Name = "btnGoToRoles";
            btnGoToRoles.Size = new Size(120, 30);
            btnGoToRoles.TabIndex = 18;
            btnGoToRoles.Text = "Ir a Roles";
            btnGoToRoles.Click += btnGoToRoles_Click;
            // 
            // btnGoToPermissions
            // 
            btnGoToPermissions.Location = new Point(500, 310);
            btnGoToPermissions.Name = "btnGoToPermissions";
            btnGoToPermissions.Size = new Size(120, 30);
            btnGoToPermissions.TabIndex = 19;
            btnGoToPermissions.Text = "Ir a Permisos";
            btnGoToPermissions.Click += btnGoToPermissions_Click;
            // 
            // btnGoToPermissionItems
            // 
            btnGoToPermissionItems.Location = new Point(500, 350);
            btnGoToPermissionItems.Name = "btnGoToPermissionItems";
            btnGoToPermissionItems.Size = new Size(120, 30);
            btnGoToPermissionItems.TabIndex = 20;
            btnGoToPermissionItems.Text = "Ir a Elementos de Permisos";
            btnGoToPermissionItems.Click += btnGoToPermissionItems_Click;
            // 
            // btnGoToMainMenu
            // 
            btnGoToMainMenu.Location = new Point(500, 390);
            btnGoToMainMenu.Name = "btnGoToMainMenu";
            btnGoToMainMenu.Size = new Size(120, 30);
            btnGoToMainMenu.TabIndex = 21;
            btnGoToMainMenu.Text = "Menú Principal";
            btnGoToMainMenu.Click += btnGoToMainMenu_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(250, 390);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 16;
            btnClear.Text = "Limpiar";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // UserForm
            // 
            ClientSize = new Size(724, 659);
            Controls.Add(dgvUsers);
            Controls.Add(txtSearch);
            Controls.Add(chkOnlyActive);
            Controls.Add(txtFirstName);
            Controls.Add(txtLastName);
            Controls.Add(txtEmail);
            Controls.Add(txtPassword);
            Controls.Add(chkActive);
            Controls.Add(btnCreate);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(lblSearch);
            Controls.Add(lblFirstName);
            Controls.Add(lblLastName);
            Controls.Add(lblEmail);
            Controls.Add(lblPassword);
            Controls.Add(txtRoleSearch);
            Controls.Add(lblRoleSearch);
            Controls.Add(dgvRoles);
            Controls.Add(btnGoToRoles);
            Controls.Add(btnGoToPermissions);
            Controls.Add(btnGoToPermissionItems);
            Controls.Add(btnGoToMainMenu);
            Controls.Add(btnClear);
            Name = "UserForm";
            Text = "Gestión de Usuarios";
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRoles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private DataGridViewCheckBoxColumn colSelected;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colName;
    }
}
