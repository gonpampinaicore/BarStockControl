namespace BarStockControl.Forms
{
    partial class UserRoleSelectionForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.CheckedListBox clbRoles;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            clbRoles = new CheckedListBox();
            btnOK = new Button();
            btnCancel = new Button();
            lblTitle = new Label();
            SuspendLayout();
            // 
            // clbRoles
            // 
            clbRoles.FormattingEnabled = true;
            clbRoles.Location = new Point(20, 40);
            clbRoles.Name = "clbRoles";
            clbRoles.Size = new Size(300, 200);
            clbRoles.TabIndex = 0;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(140, 260);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 1;
            btnOK.Text = "Aceptar";
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(230, 260);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancelar";
            btnCancel.Click += btnCancel_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(200, 15);
            lblTitle.TabIndex = 3;
            lblTitle.Text = "Seleccionar Roles";
            // 
            // UserRoleSelectionForm
            // 
            ClientSize = new Size(340, 300);
            Controls.Add(lblTitle);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(clbRoles);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserRoleSelectionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Seleccionar Roles";
            ResumeLayout(false);
            PerformLayout();
        }
    }
} 
