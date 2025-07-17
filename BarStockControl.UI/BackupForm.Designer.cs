namespace BarStockControl.UI
{
    partial class BackupForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvBackups;
        private System.Windows.Forms.Button btnCreateBackup;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnDeleteBackup;
        private System.Windows.Forms.CheckBox chkOnlyBackups;
        private System.Windows.Forms.CheckBox chkOnlyRestores;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "CS0414:El campo está asignado pero su valor nunca se usa")]
        private void InitializeComponent()
        {
            this.dgvBackups = new System.Windows.Forms.DataGridView();
            this.btnCreateBackup = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnDeleteBackup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackups)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBackups
            // 
            this.dgvBackups.Location = new System.Drawing.Point(20, 20);
            this.dgvBackups.Name = "dgvBackups";
            this.dgvBackups.Size = new System.Drawing.Size(700, 250);
            this.dgvBackups.TabIndex = 0;
            this.dgvBackups.ReadOnly = true;
            this.dgvBackups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // btnCreateBackup
            // 
            this.btnCreateBackup.Location = new System.Drawing.Point(20, 290);
            this.btnCreateBackup.Name = "btnCreateBackup";
            this.btnCreateBackup.Size = new System.Drawing.Size(120, 30);
            this.btnCreateBackup.TabIndex = 1;
            this.btnCreateBackup.Text = "Crear Backup";
            this.btnCreateBackup.UseVisualStyleBackColor = true;
            this.btnCreateBackup.Click += new System.EventHandler(this.btnCreateBackup_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(160, 290);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(120, 30);
            this.btnRestore.TabIndex = 2;
            this.btnRestore.Text = "Restaurar";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnDeleteBackup
            // 
            this.btnDeleteBackup.Location = new System.Drawing.Point(300, 290);
            this.btnDeleteBackup.Name = "btnDeleteBackup";
            this.btnDeleteBackup.Size = new System.Drawing.Size(120, 30);
            this.btnDeleteBackup.TabIndex = 3;
            this.btnDeleteBackup.Text = "Eliminar Archivo";
            this.btnDeleteBackup.UseVisualStyleBackColor = true;
            this.btnDeleteBackup.Click += new System.EventHandler(this.btnDeleteBackup_Click);
            // chkOnlyBackups
            this.chkOnlyBackups = new System.Windows.Forms.CheckBox();
            this.chkOnlyBackups.Location = new System.Drawing.Point(440, 290);
            this.chkOnlyBackups.Name = "chkOnlyBackups";
            this.chkOnlyBackups.Size = new System.Drawing.Size(90, 30);
            this.chkOnlyBackups.TabIndex = 4;
            this.chkOnlyBackups.Text = "Backups";
            this.chkOnlyBackups.CheckedChanged += new System.EventHandler(this.FilterChanged);

            // chkOnlyRestores
            this.chkOnlyRestores = new System.Windows.Forms.CheckBox();
            this.chkOnlyRestores.Location = new System.Drawing.Point(540, 290);
            this.chkOnlyRestores.Name = "chkOnlyRestores";
            this.chkOnlyRestores.Size = new System.Drawing.Size(90, 30);
            this.chkOnlyRestores.TabIndex = 5;
            this.chkOnlyRestores.Text = "Restores";
            this.chkOnlyRestores.CheckedChanged += new System.EventHandler(this.FilterChanged);

            // Agregás los checkboxes al form
            this.Controls.Add(this.chkOnlyBackups);
            this.Controls.Add(this.chkOnlyRestores);
            // 
            // BackupForm
            // 
            this.ClientSize = new System.Drawing.Size(770, 340);
            this.Controls.Add(this.dgvBackups);
            this.Controls.Add(this.btnCreateBackup);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnDeleteBackup);
            this.Name = "BackupForm";
            this.Text = "Gestión de Backups";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackups)).EndInit();
            this.ResumeLayout(false);
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

        }
    }
}
