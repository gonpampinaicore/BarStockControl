namespace BarStockControl.UI
{
    partial class PermissionSelectionForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView lstPermissions;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lstPermissions = new System.Windows.Forms.ListView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lstPermissions
            this.lstPermissions.FullRowSelect = true;
            this.lstPermissions.GridLines = true;
            this.lstPermissions.Location = new System.Drawing.Point(12, 12);
            this.lstPermissions.Name = "lstPermissions";
            this.lstPermissions.Size = new System.Drawing.Size(460, 200);
            this.lstPermissions.TabIndex = 0;
            this.lstPermissions.UseCompatibleStateImageBehavior = false;
            this.lstPermissions.View = System.Windows.Forms.View.Details;
            this.lstPermissions.DoubleClick += new System.EventHandler(this.lstPermissions_DoubleClick);

            // Configurar columnas
            this.lstPermissions.Columns.Add("Nombre", 150);
            this.lstPermissions.Columns.Add("Descripción", 200);
            this.lstPermissions.Columns.Add("Estado", 100);

            // btnOK
            this.btnOK.Location = new System.Drawing.Point(316, 230);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Aceptar";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(397, 230);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // PermissionSelectionForm
            this.ClientSize = new System.Drawing.Size(484, 265);
            this.Controls.Add(this.lstPermissions);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PermissionSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Seleccionar Permiso";

            this.ResumeLayout(false);
        }
    }
} 
