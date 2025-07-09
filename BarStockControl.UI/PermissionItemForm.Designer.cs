namespace BarStockControl.Forms.Permissions
{
    partial class PermissionItemForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvPermissionItems;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkOnlyActive;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnGoToUsers;
        private System.Windows.Forms.Button btnGoToRoles;
        private System.Windows.Forms.Button btnGoToPermissions;
        private System.Windows.Forms.Button btnGoToMainMenu;

        private void InitializeComponent()
        {
            dgvPermissionItems = new DataGridView();
            txtSearch = new TextBox();
            chkOnlyActive = new CheckBox();
            txtName = new TextBox();
            txtDescription = new TextBox();
            chkActive = new CheckBox();
            btnCreate = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            lblName = new Label();
            lblDescription = new Label();
            lblSearch = new Label();
            btnGoToUsers = new Button();
            btnGoToRoles = new Button();
            btnGoToPermissions = new Button();
            btnGoToMainMenu = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPermissionItems).BeginInit();
            SuspendLayout();
            // 
            // dgvPermissionItems
            // 
            dgvPermissionItems.Location = new Point(12, 189);
            dgvPermissionItems.Name = "dgvPermissionItems";
            dgvPermissionItems.ReadOnly = true;
            dgvPermissionItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPermissionItems.Size = new Size(676, 249);
            dgvPermissionItems.TabIndex = 0;
            dgvPermissionItems.CellClick += dgvPermissionItems_CellClick;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(100, 159);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // chkOnlyActive
            // 
            chkOnlyActive.Location = new Point(362, 159);
            chkOnlyActive.Name = "chkOnlyActive";
            chkOnlyActive.Size = new Size(100, 24);
            chkOnlyActive.TabIndex = 0;
            chkOnlyActive.Text = "Solo activos";
            chkOnlyActive.UseVisualStyleBackColor = true;
            chkOnlyActive.CheckedChanged += chkOnlyActive_CheckedChanged;
            // 
            // txtName
            // 
            txtName.Location = new Point(100, 28);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 23);
            txtName.TabIndex = 3;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(100, 75);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(200, 23);
            txtDescription.TabIndex = 4;
            // 
            // chkActive
            // 
            chkActive.Location = new Point(100, 119);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(60, 19);
            chkActive.TabIndex = 4;
            chkActive.Text = "Activo";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(362, 28);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(83, 23);
            btnCreate.TabIndex = 5;
            btnCreate.Text = "Crear";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(362, 59);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(83, 23);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Actualizar";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(362, 90);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(83, 23);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Eliminar";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // lblName
            // 
            lblName.Location = new Point(18, 28);
            lblName.Name = "lblName";
            lblName.Size = new Size(54, 15);
            lblName.TabIndex = 9;
            lblName.Text = "Nombre:";
            // 
            // lblDescription
            // 
            lblDescription.Location = new Point(12, 75);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(72, 15);
            lblDescription.TabIndex = 10;
            lblDescription.Text = "Descripción:";
            // 
            // lblSearch
            // 
            lblSearch.Location = new Point(18, 159);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(45, 15);
            lblSearch.TabIndex = 8;
            lblSearch.Text = "Buscar:";
            // 
            // btnGoToUsers
            // 
            btnGoToUsers.Location = new Point(541, 67);
            btnGoToUsers.Name = "btnGoToUsers";
            btnGoToUsers.Size = new Size(130, 23);
            btnGoToUsers.TabIndex = 11;
            btnGoToUsers.Text = "Ir a Usuarios";
            btnGoToUsers.UseVisualStyleBackColor = true;
            btnGoToUsers.Click += btnGoToUsers_Click;
            // 
            // btnGoToRoles
            // 
            btnGoToRoles.Location = new Point(541, 115);
            btnGoToRoles.Name = "btnGoToRoles";
            btnGoToRoles.Size = new Size(130, 23);
            btnGoToRoles.TabIndex = 12;
            btnGoToRoles.Text = "Ir a Roles";
            btnGoToRoles.UseVisualStyleBackColor = true;
            btnGoToRoles.Click += btnGoToRoles_Click;
            // 
            // btnGoToPermissions
            // 
            btnGoToPermissions.Location = new Point(541, 151);
            btnGoToPermissions.Name = "btnGoToPermissions";
            btnGoToPermissions.Size = new Size(130, 23);
            btnGoToPermissions.TabIndex = 13;
            btnGoToPermissions.Text = "Ir a Permisos";
            btnGoToPermissions.UseVisualStyleBackColor = true;
            btnGoToPermissions.Click += btnGoToPermissions_Click;
            // 
            // btnGoToMainMenu
            // 
            btnGoToMainMenu.Location = new Point(541, 28);
            btnGoToMainMenu.Name = "btnGoToMainMenu";
            btnGoToMainMenu.Size = new Size(130, 23);
            btnGoToMainMenu.TabIndex = 14;
            btnGoToMainMenu.Text = "Menú Principal";
            btnGoToMainMenu.UseVisualStyleBackColor = true;
            btnGoToMainMenu.Click += btnGoToMainMenu_Click;
            // 
            // PermissionItemForm
            // 
            ClientSize = new Size(700, 450);
            Controls.Add(dgvPermissionItems);
            Controls.Add(txtSearch);
            Controls.Add(chkOnlyActive);
            Controls.Add(txtName);
            Controls.Add(txtDescription);
            Controls.Add(chkActive);
            Controls.Add(btnCreate);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(lblSearch);
            Controls.Add(lblName);
            Controls.Add(lblDescription);
            Controls.Add(btnGoToUsers);
            Controls.Add(btnGoToRoles);
            Controls.Add(btnGoToPermissions);
            Controls.Add(btnGoToMainMenu);
            Name = "PermissionItemForm";
            Text = "Gestión de Elementos de Permisos";
            ((System.ComponentModel.ISupportInitialize)dgvPermissionItems).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
