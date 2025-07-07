namespace BarStockControl.Forms.Roles
{
    partial class RoleForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvRoles;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkOnlyActive;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.CheckedListBox clbPermissions;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblPermissions;
        private System.Windows.Forms.Label lblPermissionSearch;
        private System.Windows.Forms.Button btnGoToUsers;
        private System.Windows.Forms.Button btnGoToPermissions;
        private System.Windows.Forms.Button btnGoToPermissionItems;
        private System.Windows.Forms.Button btnGoToMainMenu;

        private void InitializeComponent()
        {
            dgvRoles = new System.Windows.Forms.DataGridView();
            txtSearch = new System.Windows.Forms.TextBox();
            chkOnlyActive = new System.Windows.Forms.CheckBox();
            txtName = new System.Windows.Forms.TextBox();
            txtDescription = new System.Windows.Forms.TextBox();
            chkActive = new System.Windows.Forms.CheckBox();
            clbPermissions = new System.Windows.Forms.CheckedListBox();
            btnCreate = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            lblName = new System.Windows.Forms.Label();
            lblDescription = new System.Windows.Forms.Label();
            lblSearch = new System.Windows.Forms.Label();
            lblPermissions = new System.Windows.Forms.Label();
            lblPermissionSearch = new System.Windows.Forms.Label();
            btnGoToUsers = new System.Windows.Forms.Button();
            btnGoToPermissions = new System.Windows.Forms.Button();
            btnGoToPermissionItems = new System.Windows.Forms.Button();
            btnGoToMainMenu = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)dgvRoles).BeginInit();
            SuspendLayout();

            // dgvRoles
            dgvRoles.Location = new System.Drawing.Point(20, 260);
            dgvRoles.Name = "dgvRoles";
            dgvRoles.Size = new System.Drawing.Size(600, 200);
            dgvRoles.TabIndex = 0;
            dgvRoles.ReadOnly = true;
            dgvRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvRoles.CellClick += dgvRoles_CellClick;

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

            // txtName
            txtName.Location = new System.Drawing.Point(100, 60);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(200, 23);
            txtName.TabIndex = 3;

            // txtDescription
            txtDescription.Location = new System.Drawing.Point(100, 90);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new System.Drawing.Size(200, 23);
            txtDescription.TabIndex = 4;

            // chkActive
            chkActive.Location = new System.Drawing.Point(100, 120);
            chkActive.Name = "chkActive";
            chkActive.Size = new System.Drawing.Size(104, 24);
            chkActive.TabIndex = 5;
            chkActive.Text = "Activo";

            // clbPermissions
            clbPermissions.Location = new System.Drawing.Point(100, 160);
            clbPermissions.Name = "clbPermissions";
            clbPermissions.Size = new System.Drawing.Size(300, 70);
            clbPermissions.TabIndex = 6;

            // btnCreate
            btnCreate.Location = new System.Drawing.Point(420, 60);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new System.Drawing.Size(75, 23);
            btnCreate.TabIndex = 7;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;

            // btnUpdate
            btnUpdate.Location = new System.Drawing.Point(420, 90);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new System.Drawing.Size(75, 23);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "Actualizar";
            btnUpdate.Click += btnUpdate_Click;

            // btnDelete
            btnDelete.Location = new System.Drawing.Point(420, 120);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(75, 23);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;

            // Labels
            lblSearch.Location = new System.Drawing.Point(20, 23);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new System.Drawing.Size(100, 23);
            lblSearch.TabIndex = 10;
            lblSearch.Text = "Buscar:";

            lblName.Location = new System.Drawing.Point(20, 63);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(100, 23);
            lblName.TabIndex = 11;
            lblName.Text = "Nombre:";

            lblDescription.Location = new System.Drawing.Point(20, 93);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new System.Drawing.Size(100, 23);
            lblDescription.TabIndex = 12;
            lblDescription.Text = "Descripción:";

            lblPermissions.Location = new System.Drawing.Point(20, 160);
            lblPermissions.Name = "lblPermissions";
            lblPermissions.Size = new System.Drawing.Size(100, 23);
            lblPermissions.TabIndex = 13;
            lblPermissions.Text = "Permisos:";

            // btnGoToUsers
            btnGoToUsers.Location = new System.Drawing.Point(520, 60);
            btnGoToUsers.Name = "btnGoToUsers";
            btnGoToUsers.Size = new System.Drawing.Size(120, 30);
            btnGoToUsers.TabIndex = 14;
            btnGoToUsers.Text = "Ir a Usuarios";
            btnGoToUsers.Click += btnGoToUsers_Click;

            // btnGoToPermissions
            btnGoToPermissions.Location = new System.Drawing.Point(520, 100);
            btnGoToPermissions.Name = "btnGoToPermissions";
            btnGoToPermissions.Size = new System.Drawing.Size(120, 30);
            btnGoToPermissions.TabIndex = 15;
            btnGoToPermissions.Text = "Ir a Permisos";
            btnGoToPermissions.Click += btnGoToPermissions_Click;

            // btnGoToPermissionItems
            btnGoToPermissionItems.Location = new System.Drawing.Point(520, 140);
            btnGoToPermissionItems.Name = "btnGoToPermissionItems";
            btnGoToPermissionItems.Size = new System.Drawing.Size(120, 30);
            btnGoToPermissionItems.TabIndex = 16;
            btnGoToPermissionItems.Text = "Ir a Elementos";
            btnGoToPermissionItems.Click += btnGoToPermissionItems_Click;

            // btnGoToMainMenu
            btnGoToMainMenu.Location = new System.Drawing.Point(520, 180);
            btnGoToMainMenu.Name = "btnGoToMainMenu";
            btnGoToMainMenu.Size = new System.Drawing.Size(100, 30);
            btnGoToMainMenu.TabIndex = 17;
            btnGoToMainMenu.Text = "Menú Principal";
            btnGoToMainMenu.Click += btnGoToMainMenu_Click;

            // Form
            ClientSize = new System.Drawing.Size(640, 480);
            Controls.Add(dgvRoles);
            Controls.Add(txtSearch);
            Controls.Add(chkOnlyActive);
            Controls.Add(txtName);
            Controls.Add(txtDescription);
            Controls.Add(chkActive);
            Controls.Add(clbPermissions);
            Controls.Add(btnCreate);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(lblSearch);
            Controls.Add(lblName);
            Controls.Add(lblDescription);
            Controls.Add(lblPermissions);
            Controls.Add(btnGoToUsers);
            Controls.Add(btnGoToPermissions);
            Controls.Add(btnGoToPermissionItems);
            Controls.Add(btnGoToMainMenu);
            Name = "RoleForm";
            Text = "Gestión de Roles";
            ((System.ComponentModel.ISupportInitialize)dgvRoles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
