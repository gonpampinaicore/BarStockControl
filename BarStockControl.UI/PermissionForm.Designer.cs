namespace BarStockControl.UI
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
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnGoToUsers;
        private System.Windows.Forms.Button btnGoToRoles;
        private System.Windows.Forms.Button btnGoToMainMenu;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "CS0414:El campo está asignado pero su valor nunca se usa")]
        private void InitializeComponent()
        {
            dgvPermissions = new DataGridView();
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
            btnGoToMainMenu = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPermissions).BeginInit();
            SuspendLayout();
            // 
            // dgvPermissions
            // 
            dgvPermissions.Location = new Point(20, 150);
            dgvPermissions.Name = "dgvPermissions";
            dgvPermissions.Size = new Size(615, 411);
            dgvPermissions.TabIndex = 0;
            dgvPermissions.CellClick += dgvPermissions_CellClick;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(100, 109);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // chkOnlyActive
            // 
            chkOnlyActive.Location = new Point(360, 109);
            chkOnlyActive.Name = "chkOnlyActive";
            chkOnlyActive.Size = new Size(104, 24);
            chkOnlyActive.TabIndex = 2;
            chkOnlyActive.Text = "Solo activos";
            chkOnlyActive.CheckedChanged += chkOnlyActive_CheckedChanged;
            // 
            // txtName
            // 
            txtName.Location = new Point(100, 9);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 23);
            txtName.TabIndex = 3;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(100, 44);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(200, 23);
            txtDescription.TabIndex = 4;
            // 
            // chkActive
            // 
            chkActive.Location = new Point(100, 79);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(104, 24);
            chkActive.TabIndex = 5;
            chkActive.Text = "Activo";
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(344, 12);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 7;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(344, 50);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "Actualizar";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(344, 88);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;
            // 
            // lblName
            // 
            lblName.Location = new Point(20, 9);
            lblName.Name = "lblName";
            lblName.Size = new Size(100, 23);
            lblName.TabIndex = 11;
            lblName.Text = "Nombre:";
            // 
            // lblDescription
            // 
            lblDescription.Location = new Point(20, 44);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(100, 23);
            lblDescription.TabIndex = 12;
            lblDescription.Text = "Descripción:";
            // 
            // lblSearch
            // 
            lblSearch.Location = new Point(20, 109);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(100, 23);
            lblSearch.TabIndex = 10;
            lblSearch.Text = "Buscar:";
            // 
            // btnGoToUsers
            // 
            btnGoToUsers.Location = new Point(467, 12);
            btnGoToUsers.Name = "btnGoToUsers";
            btnGoToUsers.Size = new Size(134, 41);
            btnGoToUsers.TabIndex = 14;
            btnGoToUsers.Text = "Ir a Usuarios";
            btnGoToUsers.Click += btnGoToUsers_Click;
            // 
            // btnGoToRoles
            // 
            btnGoToRoles.Location = new Point(467, 59);
            btnGoToRoles.Name = "btnGoToRoles";
            btnGoToRoles.Size = new Size(131, 41);
            btnGoToRoles.TabIndex = 15;
            btnGoToRoles.Text = "Ir a Roles";
            btnGoToRoles.Click += btnGoToRoles_Click;
            // 
            // btnGoToMainMenu
            // 
            btnGoToMainMenu.Location = new Point(467, 106);
            btnGoToMainMenu.Name = "btnGoToMainMenu";
            btnGoToMainMenu.Size = new Size(137, 23);
            btnGoToMainMenu.TabIndex = 17;
            btnGoToMainMenu.Text = "Menú Principal";
            btnGoToMainMenu.Click += btnGoToMainMenu_Click;
            // 
            // PermissionForm
            // 
            ClientSize = new Size(656, 573);
            Controls.Add(btnGoToMainMenu);
            Controls.Add(btnGoToRoles);
            Controls.Add(btnGoToUsers);
            Controls.Add(lblDescription);
            Controls.Add(lblName);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnCreate);
            Controls.Add(chkActive);
            Controls.Add(txtDescription);
            Controls.Add(txtName);
            Controls.Add(chkOnlyActive);
            Controls.Add(txtSearch);
            Controls.Add(lblSearch);
            Controls.Add(dgvPermissions);
            Name = "PermissionForm";
            Text = "Gestión de Permisos";
            ((System.ComponentModel.ISupportInitialize)dgvPermissions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
