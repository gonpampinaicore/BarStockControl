namespace BarStockControl.UI
{
    partial class DepositForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvDeposits;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblName;

        private void InitializeComponent()
        {
            dgvDeposits = new DataGridView();
            txtName = new TextBox();
            chkActive = new CheckBox();
            btnCreate = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            lblName = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvDeposits).BeginInit();
            SuspendLayout();
            // 
            // dgvDeposits
            // 
            dgvDeposits.Location = new Point(20, 20);
            dgvDeposits.Name = "dgvDeposits";
            dgvDeposits.Size = new Size(400, 200);
            dgvDeposits.TabIndex = 0;
            dgvDeposits.CellClick += dgvDeposits_CellClick;
            // 
            // txtName
            // 
            txtName.Location = new Point(126, 231);
            txtName.Name = "txtName";
            txtName.Size = new Size(100, 23);
            txtName.TabIndex = 1;
            // 
            // chkActive
            // 
            chkActive.Location = new Point(250, 230);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(104, 24);
            chkActive.TabIndex = 2;
            chkActive.Text = "Active";
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(20, 270);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 3;
            btnCreate.Text = "Create";
            btnCreate.Click += btnCreate_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(110, 270);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(200, 270);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // lblName
            // 
            lblName.Location = new Point(20, 230);
            lblName.Name = "lblName";
            lblName.Size = new Size(100, 23);
            lblName.TabIndex = 1;
            lblName.Text = "Name:";
            // 
            // DepositForm
            // 
            ClientSize = new Size(450, 320);
            Controls.Add(dgvDeposits);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(chkActive);
            Controls.Add(btnCreate);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Name = "DepositForm";
            Text = "Deposit Management";
            ((System.ComponentModel.ISupportInitialize)dgvDeposits).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
