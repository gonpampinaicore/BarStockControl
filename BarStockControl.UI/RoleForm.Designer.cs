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
            dgvRoles = new DataGridView();
            txtSearch = new TextBox();
            chkOnlyActive = new CheckBox();
            txtName = new TextBox();
            txtDescription = new TextBox();
            chkActive = new CheckBox();
            clbPermissions = new CheckedListBox();
            btnCreate = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            lblName = new Label();
            lblDescription = new Label();
            lblSearch = new Label();
            lblPermissions = new Label();
            lblPermissionSearch = new Label();
            btnGoToUsers = new Button();
            btnGoToPermissions = new Button();
            btnGoToPermissionItems = new Button();
            btnGoToMainMenu = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvRoles).BeginInit();
            SuspendLayout();
            // 
            // dgvRoles
            // 
            dgvRoles.Location = new Point(20, 299);
            dgvRoles.Name = "dgvRoles";
            dgvRoles.ReadOnly = true;
            dgvRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoles.Size = new Size(620, 251);
            dgvRoles.TabIndex = 0;
            dgvRoles.CellClick += dgvRoles_CellClick;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(73, 267);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // chkOnlyActive
            // 
            chkOnlyActive.Location = new Point(303, 266);
            chkOnlyActive.Name = "chkOnlyActive";
            chkOnlyActive.Size = new Size(104, 24);
            chkOnlyActive.TabIndex = 2;
            chkOnlyActive.Text = "Solo activos";
            chkOnlyActive.CheckedChanged += chkOnlyActive_CheckedChanged;
            // 
            // txtName
            // 
            txtName.Location = new Point(100, 21);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 23);
            txtName.TabIndex = 3;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(100, 60);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(200, 23);
            txtDescription.TabIndex = 4;
            // 
            // chkActive
            // 
            chkActive.Location = new Point(100, 99);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(104, 24);
            chkActive.TabIndex = 5;
            chkActive.Text = "Activo";
            // 
            // clbPermissions
            // 
            clbPermissions.Location = new Point(100, 130);
            clbPermissions.Name = "clbPermissions";
            clbPermissions.Size = new Size(271, 112);
            clbPermissions.TabIndex = 6;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(372, 21);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 7;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(372, 60);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "Actualizar";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(372, 100);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;
            // 
            // lblName
            // 
            lblName.Location = new Point(12, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(72, 23);
            lblName.TabIndex = 11;
            lblName.Text = "Nombre:";
            // 
            // lblDescription
            // 
            lblDescription.Location = new Point(12, 60);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(72, 23);
            lblDescription.TabIndex = 12;
            lblDescription.Text = "Descripción:";
            // 
            // lblSearch
            // 
            lblSearch.Location = new Point(12, 270);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(100, 23);
            lblSearch.TabIndex = 10;
            lblSearch.Text = "Buscar:";
            // 
            // lblPermissions
            // 
            lblPermissions.Location = new Point(12, 130);
            lblPermissions.Name = "lblPermissions";
            lblPermissions.Size = new Size(82, 23);
            lblPermissions.TabIndex = 13;
            lblPermissions.Text = "Permisos:";
            // 
            // lblPermissionSearch
            // 
            lblPermissionSearch.Location = new Point(0, 0);
            lblPermissionSearch.Name = "lblPermissionSearch";
            lblPermissionSearch.Size = new Size(100, 23);
            lblPermissionSearch.TabIndex = 0;
            // 
            // btnGoToUsers
            // 
            btnGoToUsers.Location = new Point(520, 60);
            btnGoToUsers.Name = "btnGoToUsers";
            btnGoToUsers.Size = new Size(120, 30);
            btnGoToUsers.TabIndex = 14;
            btnGoToUsers.Text = "Ir a Usuarios";
            btnGoToUsers.Click += btnGoToUsers_Click;
            // 
            // btnGoToPermissions
            // 
            btnGoToPermissions.Location = new Point(520, 100);
            btnGoToPermissions.Name = "btnGoToPermissions";
            btnGoToPermissions.Size = new Size(120, 30);
            btnGoToPermissions.TabIndex = 15;
            btnGoToPermissions.Text = "Ir a Permisos";
            btnGoToPermissions.Click += btnGoToPermissions_Click;
            // 
            // btnGoToPermissionItems
            // 
            btnGoToPermissionItems.Location = new Point(520, 140);
            btnGoToPermissionItems.Name = "btnGoToPermissionItems";
            btnGoToPermissionItems.Size = new Size(120, 30);
            btnGoToPermissionItems.TabIndex = 16;
            btnGoToPermissionItems.Text = "Ir a Elementos";
            btnGoToPermissionItems.Click += btnGoToPermissionItems_Click;
            // 
            // btnGoToMainMenu
            // 
            btnGoToMainMenu.Location = new Point(520, 180);
            btnGoToMainMenu.Name = "btnGoToMainMenu";
            btnGoToMainMenu.Size = new Size(100, 30);
            btnGoToMainMenu.TabIndex = 17;
            btnGoToMainMenu.Text = "Menú Principal";
            btnGoToMainMenu.Click += btnGoToMainMenu_Click;
            // 
            // RoleForm
            // 
            ClientSize = new Size(671, 571);
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
