namespace BarStockControl.UI
{
    partial class BarForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvBars;
        private System.Windows.Forms.Label lblListado;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox chkOnlyActive;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvBars = new System.Windows.Forms.DataGridView();
            this.lblListado = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblActive = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkOnlyActive = new System.Windows.Forms.CheckBox();

            ((System.ComponentModel.ISupportInitialize)(this.dgvBars)).BeginInit();
            this.SuspendLayout();

            // txtName
            this.txtName.Location = new System.Drawing.Point(100, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 23);
            this.txtName.TabIndex = 0;

            // chkActive
            this.chkActive.Location = new System.Drawing.Point(100, 60);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(100, 24);
            this.chkActive.TabIndex = 1;
            this.chkActive.Text = "Activo";
            this.chkActive.UseVisualStyleBackColor = true;

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(100, 100);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnCreate_Click);

            // dgvBars
            this.dgvBars.Location = new System.Drawing.Point(20, 200);
            this.dgvBars.Name = "dgvBars";
            this.dgvBars.Size = new System.Drawing.Size(400, 150);
            this.dgvBars.TabIndex = 3;
            this.dgvBars.ReadOnly = true;
            this.dgvBars.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBars.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBars_CellClick);

            // lblListado
            this.lblListado.Location = new System.Drawing.Point(20, 180);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(150, 15);
            this.lblListado.Text = "Barras existentes:";

            // lblName
            this.lblName.Location = new System.Drawing.Point(20, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(80, 23);
            this.lblName.Text = "Nombre:";

            // lblActive
            this.lblActive.Location = new System.Drawing.Point(20, 60);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(80, 23);
            this.lblActive.Text = "Activo:";

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(20, 370);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 23);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.PlaceholderText = "Buscar por nombre...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // chkOnlyActive
            this.chkOnlyActive.Location = new System.Drawing.Point(240, 370);
            this.chkOnlyActive.Name = "chkOnlyActive";
            this.chkOnlyActive.Size = new System.Drawing.Size(180, 24);
            this.chkOnlyActive.TabIndex = 5;
            this.chkOnlyActive.Text = "Mostrar solo activos";
            this.chkOnlyActive.CheckedChanged += new System.EventHandler(this.chkOnlyActive_CheckedChanged);

            // BarForm
            this.ClientSize = new System.Drawing.Size(450, 420);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvBars);
            this.Controls.Add(this.lblListado);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblActive);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.chkOnlyActive);
            this.Name = "BarForm";
            this.Text = "Gestión de Barras";

            ((System.ComponentModel.ISupportInitialize)(this.dgvBars)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
