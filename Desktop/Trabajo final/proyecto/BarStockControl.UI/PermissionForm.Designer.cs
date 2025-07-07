namespace BarStockControl.Forms.Permissions
{
    partial class PermissionForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvPermissions;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkOnlyActive;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.CheckedListBox clbPermissionItems;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblPermissionItems;
        private System.Windows.Forms.Button btnGoToUsers;
        private System.Windows.Forms.Button btnGoToRoles;
        private System.Windows.Forms.Button btnGoToPermissionItems;
        private System.Windows.Forms.Button btnGoToMainMenu;

        private void InitializeComponent()
        {
            dgvPermissions = new DataGridView();
            txtSearch = new TextBox();
            chkOnlyActive = new CheckBox();
            txtName = new TextBox();
            txtDescription = new TextBox();
            chkActive = new CheckBox();
            clbPermissionItems = new CheckedListBox();
            btnCreate = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            lblName = new Label();
            lblDescription = new Label();
            lblSearch = new Label();
            lblPermissionItems = new Label();
            btnGoToUsers = new Button();
            btnGoToRoles = new Button();
            btnGoToPermissionItems = new Button();
            btnGoToMainMenu = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPermissions).BeginInit();
            SuspendLayout();

            // dgvPermissions
            dgvPermissions.Location = new Point(20, 220);
            dgvPermissions.Name = "dgvPermissions";
            dgvPermissions.Size = new Size(600, 200);
            dgvPermissions.TabIndex = 0;
            dgvPermissions.CellClick += dgvPermissions_CellClick;

            // txtSearch
            txtSearch.Location = new Point(100, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;

            // chkOnlyActive
            chkOnlyActive.Location = new Point(320, 20);
            chkOnlyActive.Name = "chkOnlyActive";
            chkOnlyActive.Size = new Size(104, 24);
            chkOnlyActive.TabIndex = 2;
            chkOnlyActive.Text = "Solo activos";
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
            chkActive.Location = new Point(100, 120);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(104, 24);
            chkActive.TabIndex = 5;
            chkActive.Text = "Activo";

            // clbPermissionItems
            clbPermissionItems.Location = new Point(320, 60);
            clbPermissionItems.Name = "clbPermissionItems";
            clbPermissionItems.Size = new Size(200, 84);
            clbPermissionItems.TabIndex = 6;
            clbPermissionItems.CheckOnClick = true;

            // btnCreate
            btnCreate.Location = new Point(100, 160);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 7;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;

            // btnUpdate
            btnUpdate.Location = new Point(200, 160);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "Actualizar";
            btnUpdate.Click += btnUpdate_Click;

            // btnDelete
            btnDelete.Location = new Point(300, 160);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;

            // Labels
            lblSearch.Location = new Point(20, 23);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(100, 23);
            lblSearch.TabIndex = 10;
            lblSearch.Text = "Buscar:";

            lblName.Location = new Point(20, 63);
            lblName.Name = "lblName";
            lblName.Size = new Size(100, 23);
            lblName.TabIndex = 11;
            lblName.Text = "Nombre:";

            lblDescription.Location = new Point(20, 93);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(100, 23);
            lblDescription.TabIndex = 12;
            lblDescription.Text = "Descripción:";

            lblPermissionItems.Location = new Point(20, 123);
            lblPermissionItems.Name = "lblPermissionItems";
            lblPermissionItems.Size = new Size(100, 23);
            lblPermissionItems.TabIndex = 13;
            lblPermissionItems.Text = "Permisos:";

            // Buttons
            btnGoToUsers.Location = new Point(100, 190);
            btnGoToUsers.Name = "btnGoToUsers";
            btnGoToUsers.Size = new Size(75, 23);
            btnGoToUsers.TabIndex = 14;
            btnGoToUsers.Text = "Volver a Categoría";
            btnGoToUsers.Click += btnGoToUsers_Click;

            btnGoToRoles.Location = new Point(200, 190);
            btnGoToRoles.Name = "btnGoToRoles";
            btnGoToRoles.Size = new Size(75, 23);
            btnGoToRoles.TabIndex = 15;
            btnGoToRoles.Text = "Volver a Categoría";
            btnGoToRoles.Click += btnGoToRoles_Click;

            btnGoToPermissionItems.Location = new Point(300, 190);
            btnGoToPermissionItems.Name = "btnGoToPermissionItems";
            btnGoToPermissionItems.Size = new Size(75, 23);
            btnGoToPermissionItems.TabIndex = 16;
            btnGoToPermissionItems.Text = "Ir a Elementos de Permisos";
            btnGoToPermissionItems.Click += btnGoToPermissionItems_Click;

            btnGoToMainMenu.Location = new Point(400, 190);
            btnGoToMainMenu.Name = "btnGoToMainMenu";
            btnGoToMainMenu.Size = new Size(75, 23);
            btnGoToMainMenu.TabIndex = 17;
            btnGoToMainMenu.Text = "Menú Principal";
            btnGoToMainMenu.Click += btnGoToMainMenu_Click;

            // Form
            ClientSize = new Size(640, 450);
            Controls.Add(dgvPermissions);
            Controls.Add(txtSearch);
            Controls.Add(chkOnlyActive);
            Controls.Add(txtName);
            Controls.Add(txtDescription);
            Controls.Add(chkActive);
            Controls.Add(clbPermissionItems);
            Controls.Add(btnCreate);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(lblSearch);
            Controls.Add(lblName);
            Controls.Add(lblDescription);
            Controls.Add(lblPermissionItems);
            Controls.Add(btnGoToUsers);
            Controls.Add(btnGoToRoles);
            Controls.Add(btnGoToPermissionItems);
            Controls.Add(btnGoToMainMenu);
            Name = "PermissionForm";
            Text = "Gestión de Permisos";
            ((System.ComponentModel.ISupportInitialize)dgvPermissions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
