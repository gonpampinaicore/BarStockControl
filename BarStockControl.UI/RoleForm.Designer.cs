
namespace BarStockControl
{
    partial class RoleForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.CheckedListBox clbPermissions;
        private System.Windows.Forms.CheckedListBox clbRoles;
        private System.Windows.Forms.DataGridView dgvRoles;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkOnlyActive;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.clbPermissions = new System.Windows.Forms.CheckedListBox();
            this.clbRoles = new System.Windows.Forms.CheckedListBox();
            this.dgvRoles = new System.Windows.Forms.DataGridView();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkOnlyActive = new System.Windows.Forms.CheckBox();

            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).BeginInit();
            this.SuspendLayout();

            // txtName
            this.txtName.Location = new System.Drawing.Point(20, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 23);
            this.txtName.TabIndex = 0;

            // txtDescription
            this.txtDescription.Location = new System.Drawing.Point(20, 60);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(200, 23);
            this.txtDescription.TabIndex = 1;

            // chkActive
            this.chkActive.Location = new System.Drawing.Point(20, 100);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(80, 24);
            this.chkActive.Text = "Activo";
            this.chkActive.TabIndex = 2;

            // clbPermissions
            this.clbPermissions.FormattingEnabled = true;
            this.clbPermissions.Location = new System.Drawing.Point(240, 20);
            this.clbPermissions.Name = "clbPermissions";
            this.clbPermissions.Size = new System.Drawing.Size(200, 130);
            this.clbPermissions.TabIndex = 3;

            // clbRoles
            this.clbRoles.FormattingEnabled = true;
            this.clbRoles.Location = new System.Drawing.Point(460, 20);
            this.clbRoles.Name = "clbRoles";
            this.clbRoles.Size = new System.Drawing.Size(200, 130);
            this.clbRoles.TabIndex = 4;

            // dgvRoles
            this.dgvRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoles.Location = new System.Drawing.Point(20, 220);
            this.dgvRoles.Name = "dgvRoles";
            this.dgvRoles.RowTemplate.Height = 25;
            this.dgvRoles.Size = new System.Drawing.Size(640, 150);
            this.dgvRoles.TabIndex = 5;
            this.dgvRoles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRoles_CellClick);

            // btnCreate
            this.btnCreate.Location = new System.Drawing.Point(20, 180);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.Text = "Crear";
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);

            // btnUpdate
            this.btnUpdate.Location = new System.Drawing.Point(110, 180);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.Text = "Actualizar";
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(200, 180);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(20, 390);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 23);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // chkOnlyActive
            this.chkOnlyActive.Location = new System.Drawing.Point(240, 390);
            this.chkOnlyActive.Name = "chkOnlyActive";
            this.chkOnlyActive.Size = new System.Drawing.Size(120, 24);
            this.chkOnlyActive.Text = "Solo Activos";
            this.chkOnlyActive.TabIndex = 10;
            this.chkOnlyActive.CheckedChanged += new System.EventHandler(this.chkOnlyActive_CheckedChanged);

            // RoleForm
            this.ClientSize = new System.Drawing.Size(684, 441);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.clbPermissions);
            this.Controls.Add(this.clbRoles);
            this.Controls.Add(this.dgvRoles);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.chkOnlyActive);
            this.Name = "RoleForm";
            this.Text = "Gestión de Roles";

            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
