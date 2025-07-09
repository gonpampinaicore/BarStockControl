namespace BarStockControl.Forms.Events
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

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblStatus;

        private void InitializeComponent()
        {
            dgvEvents = new System.Windows.Forms.DataGridView();
            txtSearch = new System.Windows.Forms.TextBox();
            chkOnlyActive = new System.Windows.Forms.CheckBox();
            txtName = new System.Windows.Forms.TextBox();
            txtDescription = new System.Windows.Forms.TextBox();
            dtpStart = new System.Windows.Forms.DateTimePicker();
            dtpEnd = new System.Windows.Forms.DateTimePicker();
            cmbStatus = new System.Windows.Forms.ComboBox();
            chkActive = new System.Windows.Forms.CheckBox();
            btnCreate = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            lblSearch = new System.Windows.Forms.Label();
            lblName = new System.Windows.Forms.Label();
            lblDescription = new System.Windows.Forms.Label();
            lblStart = new System.Windows.Forms.Label();
            lblEnd = new System.Windows.Forms.Label();
            lblStatus = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(dgvEvents)).BeginInit();
            SuspendLayout();

            // dgvEvents
            dgvEvents.Location = new System.Drawing.Point(20, 50);
            dgvEvents.Name = "dgvEvents";
            dgvEvents.Size = new System.Drawing.Size(600, 200);
            dgvEvents.TabIndex = 0;
            dgvEvents.CellClick += dgvEvents_CellClick;

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
            chkOnlyActive.Text = "Only active";
            chkOnlyActive.CheckedChanged += chkOnlyActive_CheckedChanged;

            // lblSearch
            lblSearch.Location = new System.Drawing.Point(20, 23);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new System.Drawing.Size(80, 23);
            lblSearch.Text = "Search:";

            // txtName
            txtName.Location = new System.Drawing.Point(100, 270);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(200, 23);
            txtName.TabIndex = 3;

            // lblName
            lblName.Location = new System.Drawing.Point(20, 273);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(80, 23);
            lblName.Text = "Name:";

            // txtDescription
            txtDescription.Location = new System.Drawing.Point(100, 300);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new System.Drawing.Size(200, 23);
            txtDescription.TabIndex = 4;

            // lblDescription
            lblDescription.Location = new System.Drawing.Point(20, 303);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new System.Drawing.Size(80, 23);
            lblDescription.Text = "Description:";

            // dtpStart
            dtpStart.Location = new System.Drawing.Point(100, 330);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new System.Drawing.Size(200, 23);
            dtpStart.TabIndex = 5;

            // lblStart
            lblStart.Location = new System.Drawing.Point(20, 333);
            lblStart.Name = "lblStart";
            lblStart.Size = new System.Drawing.Size(80, 23);
            lblStart.Text = "Start date:";

            // dtpEnd
            dtpEnd.Location = new System.Drawing.Point(100, 360);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new System.Drawing.Size(200, 23);
            dtpEnd.TabIndex = 6;

            // lblEnd
            lblEnd.Location = new System.Drawing.Point(20, 363);
            lblEnd.Name = "lblEnd";
            lblEnd.Size = new System.Drawing.Size(80, 23);
            lblEnd.Text = "End date:";

            // cmbStatus
            cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbStatus.Location = new System.Drawing.Point(100, 390);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new System.Drawing.Size(200, 23);
            cmbStatus.TabIndex = 7;

            // lblStatus
            lblStatus.Location = new System.Drawing.Point(20, 393);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(80, 23);
            lblStatus.Text = "Status:";

            // chkActive
            chkActive.Location = new System.Drawing.Point(100, 420);
            chkActive.Name = "chkActive";
            chkActive.Size = new System.Drawing.Size(104, 24);
            chkActive.TabIndex = 8;
            chkActive.Text = "Active";

            // btnCreate
            btnCreate.Location = new System.Drawing.Point(320, 270);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new System.Drawing.Size(75, 23);
            btnCreate.TabIndex = 9;
            btnCreate.Text = "Create";
            btnCreate.Click += btnCreate_Click;

            // btnUpdate
            btnUpdate.Location = new System.Drawing.Point(320, 300);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new System.Drawing.Size(75, 23);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdate_Click;

            // btnDelete
            btnDelete.Location = new System.Drawing.Point(320, 330);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(75, 23);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;

            // EventForm
            ClientSize = new System.Drawing.Size(640, 480);
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
            Controls.Add(lblSearch);
            Controls.Add(lblName);
            Controls.Add(lblDescription);
            Controls.Add(lblStart);
            Controls.Add(lblEnd);
            Controls.Add(lblStatus);
            Name = "EventForm";
            Text = "Event Management";

            ((System.ComponentModel.ISupportInitialize)(dgvEvents)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
