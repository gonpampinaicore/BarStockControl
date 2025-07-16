namespace BarStockControl.UI
{
    partial class RoleSelectionForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView lstRoles;
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
            lstRoles = new ListView();
            btnOK = new Button();
            btnCancel = new Button();
            lblTitle = new Label();
            SuspendLayout();
            // 
            // lstRoles
            // 
            lstRoles.FullRowSelect = true;
            lstRoles.GridLines = true;
            lstRoles.Location = new Point(20, 40);
            lstRoles.Name = "lstRoles";
            lstRoles.Size = new Size(400, 200);
            lstRoles.TabIndex = 0;
            lstRoles.UseCompatibleStateImageBehavior = false;
            lstRoles.View = View.Details;
            lstRoles.DoubleClick += lstRoles_DoubleClick;
            // 
            // Configurar columnas
            lstRoles.Columns.Add("Nombre", 150);
            lstRoles.Columns.Add("Descripción", 200);
            lstRoles.Columns.Add("Estado", 100);
            // 
            // btnOK
            // 
            btnOK.Location = new Point(240, 260);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 1;
            btnOK.Text = "Aceptar";
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(330, 260);
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
            lblTitle.Text = "Seleccionar Rol";
            // 
            // RoleSelectionForm
            // 
            ClientSize = new Size(440, 300);
            Controls.Add(lblTitle);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(lstRoles);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RoleSelectionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Seleccionar Rol";
            ResumeLayout(false);
            PerformLayout();
        }
    }
} 
