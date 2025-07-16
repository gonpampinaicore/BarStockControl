namespace BarStockControl.UI
{
    partial class StationForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvStations;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkOnlyActive;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbBar;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblBar;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label lblComment;

        private void InitializeComponent()
        {
            dgvStations = new System.Windows.Forms.DataGridView();
            txtSearch = new System.Windows.Forms.TextBox();
            chkOnlyActive = new System.Windows.Forms.CheckBox();
            txtName = new System.Windows.Forms.TextBox();
            cmbStatus = new System.Windows.Forms.ComboBox();
            cmbBar = new System.Windows.Forms.ComboBox();
            chkActive = new System.Windows.Forms.CheckBox();
            btnCreate = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            lblSearch = new System.Windows.Forms.Label();
            lblName = new System.Windows.Forms.Label();
            lblStatus = new System.Windows.Forms.Label();
            lblBar = new System.Windows.Forms.Label();
            txtComment = new System.Windows.Forms.TextBox();
            lblComment = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(dgvStations)).BeginInit();
            SuspendLayout();

            // dgvStations
            dgvStations.Location = new System.Drawing.Point(20, 50);
            dgvStations.Name = "dgvStations";
            dgvStations.Size = new System.Drawing.Size(600, 200);
            dgvStations.TabIndex = 0;
            dgvStations.CellClick += dgvStations_CellClick;

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

            // cmbStatus
            cmbStatus.Location = new System.Drawing.Point(100, 300);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new System.Drawing.Size(200, 23);
            cmbStatus.TabIndex = 4;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            // cmbBar
            cmbBar.Location = new System.Drawing.Point(100, 330);
            cmbBar.Name = "cmbBar";
            cmbBar.Size = new System.Drawing.Size(200, 23);
            cmbBar.TabIndex = 5;
            cmbBar.DropDownStyle = ComboBoxStyle.DropDownList;

            // txtComment
            txtComment.Location = new System.Drawing.Point(100, 360);
            txtComment.Name = "txtComment";
            txtComment.Size = new System.Drawing.Size(200, 23);
            txtComment.TabIndex = 6;

            // chkActive
            chkActive.Location = new System.Drawing.Point(100, 390);
            chkActive.Name = "chkActive";
            chkActive.Size = new System.Drawing.Size(104, 24);
            chkActive.TabIndex = 7;
            chkActive.Text = "Activa";

            // btnCreate
            btnCreate.Location = new System.Drawing.Point(320, 270);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new System.Drawing.Size(75, 23);
            btnCreate.TabIndex = 8;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;

            // btnUpdate
            btnUpdate.Location = new System.Drawing.Point(320, 300);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new System.Drawing.Size(75, 23);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Actualizar";
            btnUpdate.Click += btnUpdate_Click;

            // btnDelete
            btnDelete.Location = new System.Drawing.Point(320, 330);
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

            // lblName
            lblName.Location = new System.Drawing.Point(20, 273);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(100, 23);
            lblName.TabIndex = 12;
            lblName.Text = "Nombre:";

            // lblStatus
            lblStatus.Location = new System.Drawing.Point(20, 303);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(100, 23);
            lblStatus.TabIndex = 13;
            lblStatus.Text = "Estado:";

            // lblBar
            lblBar.Location = new System.Drawing.Point(20, 333);
            lblBar.Name = "lblBar";
            lblBar.Size = new System.Drawing.Size(100, 23);
            lblBar.TabIndex = 14;
            lblBar.Text = "Bar:";

            // lblComment
            lblComment.Location = new System.Drawing.Point(20, 363);
            lblComment.Name = "lblComment";
            lblComment.Size = new System.Drawing.Size(100, 23);
            lblComment.TabIndex = 15;
            lblComment.Text = "Comentario:";

            // StationForm
            ClientSize = new System.Drawing.Size(640, 570);
            Controls.Add(dgvStations);
            Controls.Add(txtSearch);
            Controls.Add(chkOnlyActive);
            Controls.Add(txtName);
            Controls.Add(cmbStatus);
            Controls.Add(cmbBar);
            Controls.Add(txtComment);
            Controls.Add(chkActive);
            Controls.Add(btnCreate);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(lblSearch);
            Controls.Add(lblName);
            Controls.Add(lblStatus);
            Controls.Add(lblBar);
            Controls.Add(lblComment);
            Name = "StationForm";
            Text = "Gestión de Estaciones";
            ((System.ComponentModel.ISupportInitialize)(dgvStations)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
