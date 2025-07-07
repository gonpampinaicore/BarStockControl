namespace BarStockControl.Forms.CashRegisters
{
    partial class CashRegisterForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvCashRegisters;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkOnlyActive;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbBar;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblBar;

        private void InitializeComponent()
        {
            dgvCashRegisters = new System.Windows.Forms.DataGridView();
            txtSearch = new System.Windows.Forms.TextBox();
            chkOnlyActive = new System.Windows.Forms.CheckBox();
            txtName = new System.Windows.Forms.TextBox();
            cmbBar = new System.Windows.Forms.ComboBox();
            chkActive = new System.Windows.Forms.CheckBox();
            btnCreate = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            lblSearch = new System.Windows.Forms.Label();
            lblName = new System.Windows.Forms.Label();
            lblBar = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(dgvCashRegisters)).BeginInit();
            SuspendLayout();

            // dgvCashRegisters
            dgvCashRegisters.Location = new System.Drawing.Point(20, 50);
            dgvCashRegisters.Name = "dgvCashRegisters";
            dgvCashRegisters.Size = new System.Drawing.Size(600, 200);
            dgvCashRegisters.TabIndex = 0;
            dgvCashRegisters.ReadOnly = true;
            dgvCashRegisters.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCashRegisters.CellClick += dgvCashRegisters_CellClick;

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
            chkOnlyActive.Text = "Solo activas";
            chkOnlyActive.CheckedChanged += chkOnlyActive_CheckedChanged;

            // txtName
            txtName.Location = new System.Drawing.Point(100, 270);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(200, 23);
            txtName.TabIndex = 3;

            // cmbBar
            cmbBar.Location = new System.Drawing.Point(100, 300);
            cmbBar.Name = "cmbBar";
            cmbBar.Size = new System.Drawing.Size(200, 23);
            cmbBar.TabIndex = 4;
            cmbBar.DropDownStyle = ComboBoxStyle.DropDownList;

            // chkActive
            chkActive.Location = new System.Drawing.Point(100, 330);
            chkActive.Name = "chkActive";
            chkActive.Size = new System.Drawing.Size(104, 24);
            chkActive.TabIndex = 5;
            chkActive.Text = "Activa";

            // btnCreate
            btnCreate.Location = new System.Drawing.Point(320, 270);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new System.Drawing.Size(75, 23);
            btnCreate.TabIndex = 6;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;

            // btnUpdate
            btnUpdate.Location = new System.Drawing.Point(320, 300);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new System.Drawing.Size(75, 23);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Actualizar";
            btnUpdate.Click += btnUpdate_Click;

            // btnDelete
            btnDelete.Location = new System.Drawing.Point(320, 330);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(75, 23);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;

            // lblSearch
            lblSearch.Location = new System.Drawing.Point(20, 23);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new System.Drawing.Size(100, 23);
            lblSearch.Text = "Buscar:";

            // lblName
            lblName.Location = new System.Drawing.Point(20, 273);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(100, 23);
            lblName.Text = "Nombre:";

            // lblBar
            lblBar.Location = new System.Drawing.Point(20, 303);
            lblBar.Name = "lblBar";
            lblBar.Size = new System.Drawing.Size(100, 23);
            lblBar.Text = "Bar:";

            // CashRegisterForm
            ClientSize = new System.Drawing.Size(640, 570);
            Controls.AddRange(new Control[] {
                dgvCashRegisters, txtSearch, chkOnlyActive,
                txtName, cmbBar, chkActive,
                btnCreate, btnUpdate, btnDelete,
                lblSearch, lblName, lblBar
            });
            Name = "CashRegisterForm";
            Text = "Gestión de Cajas";
            ((System.ComponentModel.ISupportInitialize)(dgvCashRegisters)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
