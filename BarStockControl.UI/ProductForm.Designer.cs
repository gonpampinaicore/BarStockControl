namespace BarStockControl.UI
{
    partial class ProductForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtCapacity;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtEstimatedServings;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnCalculateServings;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCapacity;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label lblEstimatedServings;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.ComboBox cmbQualityCategory;
        private System.Windows.Forms.CheckBox chkIsImported;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblQualityCategory;
        private System.Windows.Forms.Label lblIsImported;

        private void InitializeComponent()
        {
            dgvProducts = new DataGridView();
            txtSearch = new TextBox();
            txtName = new TextBox();
            txtName.TextChanged += txtName_TextChanged;
            txtCapacity = new TextBox();
            txtPrecio = new TextBox();
            txtEstimatedServings = new TextBox();
            cmbUnit = new ComboBox();
            cmbCategory = new ComboBox();
            chkActive = new CheckBox();
            btnCreate = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClearSearch = new Button();
            btnExport = new Button();
            btnCalculateServings = new Button();
            lblSearch = new Label();
            lblName = new Label();
            lblCapacity = new Label();
            lblUnit = new Label();
            lblCategory = new Label();
            lblPrecio = new Label();
            lblEstimatedServings = new Label();
            cmbType = new ComboBox();
            cmbQualityCategory = new ComboBox();
            chkIsImported = new CheckBox();
            lblType = new Label();
            lblQualityCategory = new Label();
            lblIsImported = new Label();

            SuspendLayout();

            // dgvProducts
            dgvProducts.Location = new System.Drawing.Point(30, 220);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Size = new System.Drawing.Size(720, 320);
            dgvProducts.TabIndex = 0;
            dgvProducts.CellClick += dgvProducts_CellClick;

            // txtSearch
            txtSearch.Location = new System.Drawing.Point(30, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(300, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;

            // lblSearch
            lblSearch.Location = new System.Drawing.Point(20, 23);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new System.Drawing.Size(80, 23);
            lblSearch.Text = "Buscar:";

            // txtName
            txtName.Location = new System.Drawing.Point(100, 270);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(200, 23);
            txtName.TabIndex = 2;

            // lblName
            lblName.Location = new System.Drawing.Point(20, 273);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(80, 23);
            lblName.Text = "Nombre:";

            // txtCapacity
            txtCapacity.Location = new System.Drawing.Point(100, 300);
            txtCapacity.Name = "txtCapacity";
            txtCapacity.Size = new System.Drawing.Size(200, 23);
            txtCapacity.TabIndex = 3;

            // lblCapacity
            lblCapacity.Location = new System.Drawing.Point(20, 303);
            lblCapacity.Name = "lblCapacity";
            lblCapacity.Size = new System.Drawing.Size(80, 23);
            lblCapacity.Text = "Capacidad:";

            // txtPrecio
            txtPrecio.Location = new System.Drawing.Point(100, 330);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new System.Drawing.Size(200, 23);
            txtPrecio.TabIndex = 4;

            // lblPrecio
            lblPrecio.Location = new System.Drawing.Point(20, 333);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new System.Drawing.Size(80, 23);
            lblPrecio.Text = "Precio:";

            // txtEstimatedServings
            txtEstimatedServings.Location = new System.Drawing.Point(100, 360);
            txtEstimatedServings.Name = "txtEstimatedServings";
            txtEstimatedServings.Size = new System.Drawing.Size(200, 23);
            txtEstimatedServings.TabIndex = 5;
            txtEstimatedServings.ReadOnly = true;
            txtEstimatedServings.BackColor = System.Drawing.Color.LightGray;

            // lblEstimatedServings
            lblEstimatedServings.Location = new System.Drawing.Point(20, 363);
            lblEstimatedServings.Name = "lblEstimatedServings";
            lblEstimatedServings.Size = new System.Drawing.Size(80, 23);
            lblEstimatedServings.Text = "Porciones (calc):";

            // cmbUnit
            cmbUnit.Location = new System.Drawing.Point(100, 390);
            cmbUnit.Name = "cmbUnit";
            cmbUnit.Size = new System.Drawing.Size(200, 23);
            cmbUnit.TabIndex = 6;
            cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;

            // lblUnit
            lblUnit.Location = new System.Drawing.Point(20, 393);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new System.Drawing.Size(80, 23);
            lblUnit.Text = "Unidad:";

            // cmbCategory
            cmbCategory.Location = new System.Drawing.Point(100, 420);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new System.Drawing.Size(200, 23);
            cmbCategory.TabIndex = 7;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            // lblCategory
            lblCategory.Location = new System.Drawing.Point(20, 423);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new System.Drawing.Size(80, 23);
            lblCategory.Text = "Categoría:";

            // chkActive
            chkActive.Location = new System.Drawing.Point(100, 450);
            chkActive.Name = "chkActive";
            chkActive.Size = new System.Drawing.Size(104, 24);
            chkActive.TabIndex = 8;
            chkActive.Text = "Activo";

            // btnCreate
            btnCreate.Location = new System.Drawing.Point(350, 270);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new System.Drawing.Size(75, 23);
            btnCreate.TabIndex = 9;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;

            // btnUpdate
            btnUpdate.Location = new System.Drawing.Point(350, 310);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new System.Drawing.Size(75, 23);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "Actualizar";
            btnUpdate.Click += btnUpdate_Click;

            // btnDelete
            btnDelete.Location = new System.Drawing.Point(350, 350);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(75, 23);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;

            // btnClearSearch
            btnClearSearch.Location = new System.Drawing.Point(530, 20);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Size = new System.Drawing.Size(75, 23);
            btnClearSearch.TabIndex = 12;
            btnClearSearch.Text = "Limpiar";
            btnClearSearch.Click += btnClearSearch_Click;

            // btnExport
            btnExport.Location = new System.Drawing.Point(430, 350);
            btnExport.Name = "btnExport";
            btnExport.Size = new System.Drawing.Size(75, 23);
            btnExport.TabIndex = 13;
            btnExport.Text = "Exportar";
            btnExport.Click += btnExport_Click;

            // btnCalculateServings
            btnCalculateServings.Location = new System.Drawing.Point(530, 310);
            btnCalculateServings.Name = "btnCalculateServings";
            btnCalculateServings.Size = new System.Drawing.Size(75, 23);
            btnCalculateServings.TabIndex = 14;
            btnCalculateServings.Text = "Calcular";
            btnCalculateServings.Click += btnCalculateServings_Click;

            // Form
            ClientSize = new System.Drawing.Size(640, 480);
            Controls.Add(dgvProducts);
            Controls.Add(txtSearch);
            Controls.Add(lblSearch);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(txtCapacity);
            Controls.Add(lblCapacity);
            Controls.Add(txtPrecio);
            Controls.Add(lblPrecio);
            Controls.Add(txtEstimatedServings);
            Controls.Add(lblEstimatedServings);
            Controls.Add(cmbUnit);
            Controls.Add(lblUnit);
            Controls.Add(cmbCategory);
            Controls.Add(lblCategory);
            Controls.Add(chkActive);
            Controls.Add(btnCreate);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnClearSearch);
            Controls.Add(btnExport);
            Controls.Add(btnCalculateServings);
            Name = "ProductForm";
            Text = "Gestión de Productos";
            ResumeLayout(false);
            PerformLayout();

            // Labels y controles nuevos
            lblType.Text = "Tipo";
            lblType.Location = new System.Drawing.Point(30, 110);
            lblType.Size = new System.Drawing.Size(100, 23);
            Controls.Add(lblType);
            cmbType.Location = new System.Drawing.Point(140, 110);
            cmbType.Size = new System.Drawing.Size(180, 23);
            Controls.Add(cmbType);

            lblQualityCategory.Text = "Calidad";
            lblQualityCategory.Location = new System.Drawing.Point(30, 140);
            lblQualityCategory.Size = new System.Drawing.Size(100, 23);
            Controls.Add(lblQualityCategory);
            cmbQualityCategory.Location = new System.Drawing.Point(140, 140);
            cmbQualityCategory.Size = new System.Drawing.Size(180, 23);
            Controls.Add(cmbQualityCategory);

            lblIsImported.Text = "Importado";
            lblIsImported.Location = new System.Drawing.Point(30, 170);
            lblIsImported.Size = new System.Drawing.Size(100, 23);
            Controls.Add(lblIsImported);
            chkIsImported.Location = new System.Drawing.Point(140, 170);
            chkIsImported.Size = new System.Drawing.Size(20, 23);
            Controls.Add(chkIsImported);

            // Reubicar controles en la parte superior
            lblType.Location = new System.Drawing.Point(30, 110);
            cmbType.Location = new System.Drawing.Point(140, 110);
            lblQualityCategory.Location = new System.Drawing.Point(30, 140);
            cmbQualityCategory.Location = new System.Drawing.Point(140, 140);
            lblIsImported.Location = new System.Drawing.Point(30, 170);
            chkIsImported.Location = new System.Drawing.Point(140, 170);

            // Mover el DataGridView más abajo
            this.dgvProducts.Location = new System.Drawing.Point(30, 220);
            this.dgvProducts.Size = new System.Drawing.Size(720, 320);

            // Ajustar el tamaño del formulario
            this.Size = new System.Drawing.Size(800, 600);

            // Barra de búsqueda
            this.txtSearch.Location = new System.Drawing.Point(30, 20);
            this.txtSearch.Size = new System.Drawing.Size(300, 23);
            this.Controls.Add(this.txtSearch);
            this.btnClearSearch.Location = new System.Drawing.Point(340, 20);
            this.btnClearSearch.Size = new System.Drawing.Size(100, 23);
            this.Controls.Add(this.btnClearSearch);

            // DataGridView
            this.dgvProducts.Location = new System.Drawing.Point(30, 60);
            this.dgvProducts.Size = new System.Drawing.Size(720, 220);
            this.Controls.Add(this.dgvProducts);

            int camposY = 300;
            int labelX = 30;
            int controlX = 140;
            int spacingY = 30;

            this.lblName.Location = new System.Drawing.Point(labelX, camposY);
            this.txtName.Location = new System.Drawing.Point(controlX, camposY);

            this.lblCapacity.Location = new System.Drawing.Point(labelX, camposY + spacingY);
            this.txtCapacity.Location = new System.Drawing.Point(controlX, camposY + spacingY);

            this.lblPrecio.Location = new System.Drawing.Point(labelX, camposY + 2 * spacingY);
            this.txtPrecio.Location = new System.Drawing.Point(controlX, camposY + 2 * spacingY);

            this.lblEstimatedServings.Location = new System.Drawing.Point(labelX, camposY + 3 * spacingY);
            this.txtEstimatedServings.Location = new System.Drawing.Point(controlX, camposY + 3 * spacingY);

            this.lblUnit.Location = new System.Drawing.Point(labelX, camposY + 4 * spacingY);
            this.cmbUnit.Location = new System.Drawing.Point(controlX, camposY + 4 * spacingY);

            this.lblCategory.Location = new System.Drawing.Point(labelX, camposY + 5 * spacingY);
            this.cmbCategory.Location = new System.Drawing.Point(controlX, camposY + 5 * spacingY);

            this.lblType.Location = new System.Drawing.Point(labelX, camposY + 6 * spacingY);
            this.cmbType.Location = new System.Drawing.Point(controlX, camposY + 6 * spacingY);

            this.lblQualityCategory.Location = new System.Drawing.Point(labelX, camposY + 7 * spacingY);
            this.cmbQualityCategory.Location = new System.Drawing.Point(controlX, camposY + 7 * spacingY);

            this.lblIsImported.Location = new System.Drawing.Point(labelX, camposY + 8 * spacingY);
            this.chkIsImported.Location = new System.Drawing.Point(controlX, camposY + 8 * spacingY);

            this.chkActive.Location = new System.Drawing.Point(labelX, camposY + 9 * spacingY);

            int buttonX = 400;
            int buttonY = camposY;
            int buttonSpacingY = 40;
            this.btnCreate.Location = new System.Drawing.Point(buttonX, buttonY);
            this.btnUpdate.Location = new System.Drawing.Point(buttonX, buttonY + buttonSpacingY);
            this.btnDelete.Location = new System.Drawing.Point(buttonX, buttonY + 2 * buttonSpacingY);
            this.btnExport.Location = new System.Drawing.Point(buttonX, buttonY + 3 * buttonSpacingY);
            this.btnCalculateServings.Location = new System.Drawing.Point(buttonX, buttonY + 4 * buttonSpacingY);

            this.Size = new System.Drawing.Size(800, 700);
        }
    }
}
