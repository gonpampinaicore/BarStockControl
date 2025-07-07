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
            dgvPermissionItems = new System.Windows.Forms.DataGridView();
            txtSearch = new System.Windows.Forms.TextBox();
            chkOnlyActive = new System.Windows.Forms.CheckBox();
            txtName = new System.Windows.Forms.TextBox();
            txtDescription = new System.Windows.Forms.TextBox();
            chkActive = new System.Windows.Forms.CheckBox();
            btnCreate = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            lblName = new System.Windows.Forms.Label();
            lblDescription = new System.Windows.Forms.Label();
            lblSearch = new System.Windows.Forms.Label();
            btnGoToUsers = new System.Windows.Forms.Button();
            btnGoToRoles = new System.Windows.Forms.Button();
            btnGoToPermissions = new System.Windows.Forms.Button();
            btnGoToMainMenu = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)dgvPermissionItems).BeginInit();
            SuspendLayout();

            // dgvPermissionItems
            dgvPermissionItems.Location = new Point(20, 200);
            dgvPermissionItems.Name = "dgvPermissionItems";
            dgvPermissionItems.Size = new Size(640, 220);
            dgvPermissionItems.TabIndex = 0;
            dgvPermissionItems.ReadOnly = true;
            dgvPermissionItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvPermissionItems.CellClick += dgvPermissionItems_CellClick;

            // txtSearch
            txtSearch.Location = new Point(100, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;

            // chkOnlyActive
            chkOnlyActive.Location = new Point(12, 12);
            chkOnlyActive.Name = "chkOnlyActive";
            chkOnlyActive.Size = new Size(100, 24);
            chkOnlyActive.TabIndex = 0;
            chkOnlyActive.Text = "Solo activos";
            chkOnlyActive.UseVisualStyleBackColor = true;
            chkOnlyActive.CheckedChanged += chkOnlyActive_CheckedChanged;

            // txtName
            txtName.Location = new Point(100, 60);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 23);
            txtName.TabIndex = 3;

            // txtDescription
            txtDescription.Location = new Point(100, 90);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(200, 23);
            txtDescription.TabIndex = 4;

            // chkActive
            chkActive.Location = new Point(12, 200);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(60, 19);
            chkActive.TabIndex = 4;
            chkActive.Text = "Activo";
            chkActive.UseVisualStyleBackColor = true;

            // btnCreate
            btnCreate.Location = new Point(12, 230);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 5;
            btnCreate.Text = "Crear";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;

            // btnUpdate
            btnUpdate.Location = new Point(93, 230);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Actualizar";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;

            // btnDelete
            btnDelete.Location = new Point(174, 230);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Eliminar";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            // Labels
            lblSearch.Location = new Point(12, 45);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(45, 15);
            lblSearch.TabIndex = 8;
            lblSearch.Text = "Buscar:";

            lblName.Location = new Point(12, 80);
            lblName.Name = "lblName";
            lblName.Size = new Size(54, 15);
            lblName.TabIndex = 9;
            lblName.Text = "Nombre:";

            lblDescription.Location = new Point(12, 110);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(72, 15);
            lblDescription.TabIndex = 10;
            lblDescription.Text = "Descripción:";

            // Buttons
            btnGoToUsers.Location = new Point(12, 260);
            btnGoToUsers.Name = "btnGoToUsers";
            btnGoToUsers.Size = new Size(75, 23);
            btnGoToUsers.TabIndex = 11;
            btnGoToUsers.Text = "Ir a Usuarios";
            btnGoToUsers.UseVisualStyleBackColor = true;
            btnGoToUsers.Click += btnGoToUsers_Click;

            btnGoToRoles.Location = new Point(93, 260);
            btnGoToRoles.Name = "btnGoToRoles";
            btnGoToRoles.Size = new Size(75, 23);
            btnGoToRoles.TabIndex = 12;
            btnGoToRoles.Text = "Ir a Roles";
            btnGoToRoles.UseVisualStyleBackColor = true;
            btnGoToRoles.Click += btnGoToRoles_Click;

            btnGoToPermissions.Location = new Point(174, 260);
            btnGoToPermissions.Name = "btnGoToPermissions";
            btnGoToPermissions.Size = new Size(75, 23);
            btnGoToPermissions.TabIndex = 13;
            btnGoToPermissions.Text = "Ir a Permisos";
            btnGoToPermissions.UseVisualStyleBackColor = true;
            btnGoToPermissions.Click += btnGoToPermissions_Click;

            btnGoToMainMenu.Location = new Point(255, 260);
            btnGoToMainMenu.Name = "btnGoToMainMenu";
            btnGoToMainMenu.Size = new Size(75, 23);
            btnGoToMainMenu.TabIndex = 14;
            btnGoToMainMenu.Text = "Menú Principal";
            btnGoToMainMenu.UseVisualStyleBackColor = true;
            btnGoToMainMenu.Click += btnGoToMainMenu_Click;

            // Form
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
