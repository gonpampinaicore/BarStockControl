
namespace BarStockControl.UI
{
    partial class RoleForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.TreeView tvRoleHierarchy;
        private System.Windows.Forms.DataGridView dgvRoles;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkOnlyActive;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblHierarchy;
        private System.Windows.Forms.Button btnAddRole;
        private System.Windows.Forms.Button btnAddPermission;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Panel pnlHierarchy;
        private System.Windows.Forms.ToolTip toolTip1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtName = new TextBox();
            txtDescription = new TextBox();
            chkActive = new CheckBox();
            tvRoleHierarchy = new TreeView();
            dgvRoles = new DataGridView();
            btnCreate = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            txtSearch = new TextBox();
            chkOnlyActive = new CheckBox();
            lblName = new Label();
            lblDescription = new Label();
            lblHierarchy = new Label();
            btnAddRole = new Button();
            btnAddPermission = new Button();
            btnRemoveItem = new Button();
            pnlForm = new Panel();
            pnlHierarchy = new Panel();
            toolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)dgvRoles).BeginInit();
            pnlForm.SuspendLayout();
            pnlHierarchy.SuspendLayout();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(10, 30);
            txtName.Name = "txtName";
            txtName.Size = new Size(280, 23);
            txtName.TabIndex = 1;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(10, 80);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(280, 23);
            txtDescription.TabIndex = 3;
            // 
            // chkActive
            // 
            chkActive.AutoSize = true;
            chkActive.Location = new Point(10, 115);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(60, 19);
            chkActive.TabIndex = 4;
            chkActive.Text = "Activo";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // tvRoleHierarchy
            // 
            tvRoleHierarchy.Location = new Point(10, 30);
            tvRoleHierarchy.Name = "tvRoleHierarchy";
            tvRoleHierarchy.Size = new Size(430, 200);
            tvRoleHierarchy.TabIndex = 1;
            // 
            // dgvRoles
            // 
            dgvRoles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRoles.Location = new Point(12, 330);
            dgvRoles.Name = "dgvRoles";
            dgvRoles.Size = new Size(768, 200);
            dgvRoles.TabIndex = 2;
            dgvRoles.CellClick += dgvRoles_CellClick;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(12, 540);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 3;
            btnCreate.Text = "Crear";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(100, 540);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Actualizar";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(188, 540);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Eliminar";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(276, 540);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 6;
            btnClear.Text = "Limpiar";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(12, 301);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 7;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // chkOnlyActive
            // 
            chkOnlyActive.AutoSize = true;
            chkOnlyActive.Location = new Point(221, 303);
            chkOnlyActive.Name = "chkOnlyActive";
            chkOnlyActive.Size = new Size(91, 19);
            chkOnlyActive.TabIndex = 8;
            chkOnlyActive.Text = "Solo Activos";
            chkOnlyActive.UseVisualStyleBackColor = true;
            chkOnlyActive.CheckedChanged += chkOnlyActive_CheckedChanged;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 10);
            lblName.Name = "lblName";
            lblName.Size = new Size(54, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Nombre:";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(10, 60);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(72, 15);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Descripción:";
            // 
            // lblHierarchy
            // 
            lblHierarchy.AutoSize = true;
            lblHierarchy.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            lblHierarchy.Location = new Point(10, 10);
            lblHierarchy.Name = "lblHierarchy";
            lblHierarchy.Size = new Size(109, 13);
            lblHierarchy.TabIndex = 0;
            lblHierarchy.Text = "Jerarquía del Rol:";
            // 
            // btnAddRole
            // 
            btnAddRole.Location = new Point(10, 240);
            btnAddRole.Name = "btnAddRole";
            btnAddRole.Size = new Size(100, 23);
            btnAddRole.TabIndex = 2;
            btnAddRole.Text = "Agregar Rol";
            btnAddRole.UseVisualStyleBackColor = true;
            // 
            // btnAddPermission
            // 
            btnAddPermission.Location = new Point(120, 240);
            btnAddPermission.Name = "btnAddPermission";
            btnAddPermission.Size = new Size(120, 23);
            btnAddPermission.TabIndex = 3;
            btnAddPermission.Text = "Agregar Permiso";
            btnAddPermission.UseVisualStyleBackColor = true;
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.Location = new Point(250, 240);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new Size(100, 23);
            btnRemoveItem.TabIndex = 4;
            btnRemoveItem.Text = "Quitar Item";
            btnRemoveItem.UseVisualStyleBackColor = true;
            // 
            // pnlForm
            // 
            pnlForm.BorderStyle = BorderStyle.FixedSingle;
            pnlForm.Controls.Add(lblDescription);
            pnlForm.Controls.Add(lblName);
            pnlForm.Controls.Add(chkActive);
            pnlForm.Controls.Add(txtDescription);
            pnlForm.Controls.Add(txtName);
            pnlForm.Location = new Point(12, 12);
            pnlForm.Name = "pnlForm";
            pnlForm.Size = new Size(300, 150);
            pnlForm.TabIndex = 0;
            // 
            // pnlHierarchy
            // 
            pnlHierarchy.BorderStyle = BorderStyle.FixedSingle;
            pnlHierarchy.Controls.Add(btnRemoveItem);
            pnlHierarchy.Controls.Add(btnAddPermission);
            pnlHierarchy.Controls.Add(btnAddRole);
            pnlHierarchy.Controls.Add(lblHierarchy);
            pnlHierarchy.Controls.Add(tvRoleHierarchy);
            pnlHierarchy.Location = new Point(330, 12);
            pnlHierarchy.Name = "pnlHierarchy";
            pnlHierarchy.Size = new Size(450, 300);
            pnlHierarchy.TabIndex = 1;
            // 
            // RoleForm
            // 
            ClientSize = new Size(792, 580);
            Controls.Add(pnlForm);
            Controls.Add(pnlHierarchy);
            Controls.Add(dgvRoles);
            Controls.Add(btnCreate);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(txtSearch);
            Controls.Add(chkOnlyActive);
            Name = "RoleForm";
            Text = "Gestión de Roles";
            ((System.ComponentModel.ISupportInitialize)dgvRoles).EndInit();
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            pnlHierarchy.ResumeLayout(false);
            pnlHierarchy.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
