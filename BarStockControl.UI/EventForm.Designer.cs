namespace BarStockControl.UI
{
    partial class EventForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvEvents;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkOnlyActive;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblStatus;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "CS0414:El campo está asignado pero su valor nunca se usa")]
        private void InitializeComponent()
        {
            dgvEvents = new DataGridView();
            txtSearch = new TextBox();
            chkOnlyActive = new CheckBox();
            txtName = new TextBox();
            txtDescription = new TextBox();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            cmbStatus = new ComboBox();
            chkActive = new CheckBox();
            btnCreate = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            lblSearch = new Label();
            lblName = new Label();
            lblDescription = new Label();
            lblStart = new Label();
            lblEnd = new Label();
            lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            SuspendLayout();
            // 
            // dgvEvents
            // 
            dgvEvents.Location = new Point(20, 50);
            dgvEvents.Name = "dgvEvents";
            dgvEvents.Size = new Size(685, 200);
            dgvEvents.TabIndex = 0;
            dgvEvents.CellClick += dgvEvents_CellClick;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(100, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // chkOnlyActive
            // 
            chkOnlyActive.Location = new Point(320, 20);
            chkOnlyActive.Name = "chkOnlyActive";
            chkOnlyActive.Size = new Size(104, 24);
            chkOnlyActive.TabIndex = 2;
            chkOnlyActive.Text = "Solo activos";
            chkOnlyActive.CheckedChanged += chkOnlyActive_CheckedChanged;
            // 
            // txtName
            // 
            txtName.Location = new Point(100, 270);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 23);
            txtName.TabIndex = 3;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(100, 300);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(200, 23);
            txtDescription.TabIndex = 4;
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(100, 330);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(229, 23);
            dtpStart.TabIndex = 5;
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(100, 360);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(229, 23);
            dtpEnd.TabIndex = 6;
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Location = new Point(100, 390);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(229, 23);
            cmbStatus.TabIndex = 7;
            // 
            // chkActive
            // 
            chkActive.Location = new Point(100, 420);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(104, 24);
            chkActive.TabIndex = 8;
            chkActive.Text = "Activo";
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(377, 270);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 9;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(377, 299);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "Actualizar";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(377, 328);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(377, 357);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 99;
            btnClear.Text = "Limpiar";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // lblSearch
            // 
            lblSearch.Location = new Point(20, 23);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(80, 23);
            lblSearch.TabIndex = 12;
            lblSearch.Text = "Buscar:";
            // 
            // lblName
            // 
            lblName.Location = new Point(20, 273);
            lblName.Name = "lblName";
            lblName.Size = new Size(80, 23);
            lblName.TabIndex = 13;
            lblName.Text = "Nombre:";
            // 
            // lblDescription
            // 
            lblDescription.Location = new Point(20, 303);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(80, 23);
            lblDescription.TabIndex = 14;
            lblDescription.Text = "Descripción:";
            // 
            // lblStart
            // 
            lblStart.Location = new Point(20, 333);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(80, 23);
            lblStart.TabIndex = 15;
            lblStart.Text = "Fecha de inicio:";
            // 
            // lblEnd
            // 
            lblEnd.Location = new Point(20, 363);
            lblEnd.Name = "lblEnd";
            lblEnd.Size = new Size(80, 23);
            lblEnd.TabIndex = 16;
            lblEnd.Text = "Fecha de fin:";
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(20, 393);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(80, 23);
            lblStatus.TabIndex = 17;
            lblStatus.Text = "Estado:";
            // 
            // EventForm
            // 
            ClientSize = new Size(746, 484);
            Controls.Add(dgvEvents);
            Controls.Add(txtSearch);
            Controls.Add(chkOnlyActive);
            Controls.Add(txtName);
            Controls.Add(txtDescription);
            Controls.Add(dtpStart);
            Controls.Add(dtpEnd);
            Controls.Add(cmbStatus);
            Controls.Add(chkActive);
            Controls.Add(btnCreate);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(lblSearch);
            Controls.Add(lblName);
            Controls.Add(lblDescription);
            Controls.Add(lblStart);
            Controls.Add(lblEnd);
            Controls.Add(lblStatus);
            Name = "EventForm";
            Text = "Gestión de eventos";
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
