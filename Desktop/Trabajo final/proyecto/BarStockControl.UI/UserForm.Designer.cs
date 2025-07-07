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

        private void InitializeComponent()
        {
            dgvUsers = new System.Windows.Forms.DataGridView();
            txtSearch = new System.Windows.Forms.TextBox();
            chkOnlyActive = new System.Windows.Forms.CheckBox();
            txtFirstName = new System.Windows.Forms.TextBox();
            txtLastName = new System.Windows.Forms.TextBox();
            txtEmail = new System.Windows.Forms.TextBox();
            txtPassword = new System.Windows.Forms.TextBox();
            chkActive = new System.Windows.Forms.CheckBox();
            btnCreate = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            lblFirstName = new System.Windows.Forms.Label();
            lblLastName = new System.Windows.Forms.Label();
            lblEmail = new System.Windows.Forms.Label();
            lblPassword = new System.Windows.Forms.Label();
            lblSearch = new System.Windows.Forms.Label();
            txtRoleSearch = new System.Windows.Forms.TextBox();
            lblRoleSearch = new System.Windows.Forms.Label();
            dgvRoles = new System.Windows.Forms.DataGridView();
            btnGoToRoles = new System.Windows.Forms.Button();
            btnGoToPermissions = new System.Windows.Forms.Button();
            btnGoToPermissionItems = new System.Windows.Forms.Button();
            btnGoToMainMenu = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(dgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dgvRoles)).BeginInit();
            SuspendLayout();

            // dgvUsers
            dgvUsers.Location = new System.Drawing.Point(20, 50);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.Size = new System.Drawing.Size(600, 200);
            dgvUsers.TabIndex = 0;
            dgvUsers.CellClick += dgvUsers_CellClick;

            // txtSearch
            txtSearch.Location = new System.Drawing.Point(100, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(200, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;

            // chkOnlyActive
            chkOnlyActive.Location = new System.Drawing.Point(320, 20);
            chkOnlyActive.Name = "chkOnlyActive";
            chkOnlyActive.Size = new System.Drawing.Size(104, 24);
            chkOnlyActive.TabIndex = 2;
            chkOnlyActive.Text = "Solo activos";
            chkOnlyActive.CheckedChanged += chkOnlyActive_CheckedChanged;

            // txtFirstName
            txtFirstName.Location = new System.Drawing.Point(100, 270);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new System.Drawing.Size(100, 23);
            txtFirstName.TabIndex = 3;

            // txtLastName
            txtLastName.Location = new System.Drawing.Point(100, 300);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new System.Drawing.Size(100, 23);
            txtLastName.TabIndex = 4;

            // txtEmail
            txtEmail.Location = new System.Drawing.Point(100, 330);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new System.Drawing.Size(100, 23);
            txtEmail.TabIndex = 5;

            // txtPassword
            txtPassword.Location = new System.Drawing.Point(100, 360);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new System.Drawing.Size(100, 23);
            txtPassword.TabIndex = 6;

            // chkActive
            chkActive.Location = new System.Drawing.Point(100, 390);
            chkActive.Name = "chkActive";
            chkActive.Size = new System.Drawing.Size(104, 24);
            chkActive.TabIndex = 7;
            chkActive.Text = "Activo";

            // btnCreate
            btnCreate.Location = new System.Drawing.Point(250, 270);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new System.Drawing.Size(75, 23);
            btnCreate.TabIndex = 8;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;

            // btnUpdate
            btnUpdate.Location = new System.Drawing.Point(250, 310);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new System.Drawing.Size(75, 23);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Actualizar";
            btnUpdate.Click += btnUpdate_Click;

            // btnDelete
            btnDelete.Location = new System.Drawing.Point(250, 350);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(75, 23);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;

            // lblSearch
            lblSearch.Location = new System.Drawing.Point(20, 23);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new System.Drawing.Size(100, 23);
            lblSearch.TabIndex = 11;
            lblSearch.Text = "Buscar:";

            // lblFirstName
            lblFirstName.Location = new System.Drawing.Point(20, 273);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new System.Drawing.Size(100, 23);
            lblFirstName.TabIndex = 12;
            lblFirstName.Text = "Nombre:";

            // lblLastName
            lblLastName.Location = new System.Drawing.Point(20, 303);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new System.Drawing.Size(100, 23);
            lblLastName.TabIndex = 13;
            lblLastName.Text = "Apellido:";

            // lblEmail
            lblEmail.Location = new System.Drawing.Point(20, 333);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new System.Drawing.Size(100, 23);
            lblEmail.TabIndex = 14;
            lblEmail.Text = "Email:";

            // lblPassword
            lblPassword.Location = new System.Drawing.Point(20, 363);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(100, 23);
            lblPassword.TabIndex = 15;
            lblPassword.Text = "Contraseña:";

            // txtRoleSearch
            txtRoleSearch.Location = new System.Drawing.Point(100, 420);
            txtRoleSearch.Name = "txtRoleSearch";
            txtRoleSearch.Size = new System.Drawing.Size(200, 23);
            txtRoleSearch.TabIndex = 16;
            txtRoleSearch.TextChanged += txtRoleSearch_TextChanged;

            // lblRoleSearch
            lblRoleSearch.Location = new System.Drawing.Point(20, 423);
            lblRoleSearch.Name = "lblRoleSearch";
            lblRoleSearch.Size = new System.Drawing.Size(80, 23);
            lblRoleSearch.Text = "Buscar rol:";

            // dgvRoles
            dgvRoles.Location = new System.Drawing.Point(20, 450);
            dgvRoles.Name = "dgvRoles";
            dgvRoles.Size = new System.Drawing.Size(300, 150);
            dgvRoles.TabIndex = 17;
            dgvRoles.ReadOnly = false;
            dgvRoles.AllowUserToAddRows = false;
            dgvRoles.RowHeadersVisible = false;
            dgvRoles.AutoGenerateColumns = false;

            var colSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            colSelected.Name = "Selected";
            colSelected.HeaderText = "";
            colSelected.Width = 30;
            dgvRoles.Columns.Add(colSelected);

            var colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colId.Name = "Id";
            colId.HeaderText = "ID";
            colId.ReadOnly = true;
            dgvRoles.Columns.Add(colId);

            var colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colName.Name = "Name";
            colName.HeaderText = "Nombre";
            colName.ReadOnly = true;
            colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dgvRoles.Columns.Add(colName);

            // btnGoToRoles
            btnGoToRoles.Location = new System.Drawing.Point(350, 270);
            btnGoToRoles.Name = "btnGoToRoles";
            btnGoToRoles.Size = new System.Drawing.Size(120, 30);
            btnGoToRoles.TabIndex = 18;
            btnGoToRoles.Text = "Ir a Roles";
            btnGoToRoles.Click += btnGoToRoles_Click;

            // btnGoToPermissions
            btnGoToPermissions.Location = new System.Drawing.Point(350, 310);
            btnGoToPermissions.Name = "btnGoToPermissions";
            btnGoToPermissions.Size = new System.Drawing.Size(120, 30);
            btnGoToPermissions.TabIndex = 19;
            btnGoToPermissions.Text = "Ir a Permisos";
            btnGoToPermissions.Click += btnGoToPermissions_Click;

            // btnGoToPermissionItems
            btnGoToPermissionItems.Location = new System.Drawing.Point(350, 350);
            btnGoToPermissionItems.Name = "btnGoToPermissionItems";
            btnGoToPermissionItems.Size = new System.Drawing.Size(120, 30);
            btnGoToPermissionItems.TabIndex = 20;
            btnGoToPermissionItems.Text = "Ir a Elementos de Permisos";
            btnGoToPermissionItems.Click += btnGoToPermissionItems_Click;

            // btnGoToMainMenu
            btnGoToMainMenu.Location = new System.Drawing.Point(350, 390);
            btnGoToMainMenu.Name = "btnGoToMainMenu";
            btnGoToMainMenu.Size = new System.Drawing.Size(120, 30);
            btnGoToMainMenu.TabIndex = 21;
            btnGoToMainMenu.Text = "Menú Principal";
            btnGoToMainMenu.Click += btnGoToMainMenu_Click;

            // UserForm
            ClientSize = new System.Drawing.Size(640, 650);
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
            Name = "UserForm";
            Text = "Gestión de Usuarios";

            ((System.ComponentModel.ISupportInitialize)(dgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dgvRoles)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
