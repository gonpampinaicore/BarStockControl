namespace BarStockControl.Forms
{
    partial class StockForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.DataGridView dgvLocations;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.RadioButton rdoDeposit;
        private System.Windows.Forms.RadioButton rdoStation;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblSelectedProduct;
        private System.Windows.Forms.Label lblSelectedLocation;
        private System.Windows.Forms.ComboBox cmbProductFilter;
        private System.Windows.Forms.Label lblProductFilter;

        private void InitializeComponent()
        {
            dgvStock = new System.Windows.Forms.DataGridView();
            dgvProducts = new System.Windows.Forms.DataGridView();
            dgvLocations = new System.Windows.Forms.DataGridView();
            txtQuantity = new System.Windows.Forms.TextBox();
            lblQuantity = new System.Windows.Forms.Label();
            rdoDeposit = new System.Windows.Forms.RadioButton();
            rdoStation = new System.Windows.Forms.RadioButton();
            btnCreate = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            lblSelectedProduct = new System.Windows.Forms.Label();
            lblSelectedLocation = new System.Windows.Forms.Label();
            cmbProductFilter = new System.Windows.Forms.ComboBox();
            lblProductFilter = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(dgvStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dgvProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dgvLocations)).BeginInit();
            SuspendLayout();

            dgvStock.Location = new System.Drawing.Point(20, 50);
            dgvStock.Name = "dgvStock";
            dgvStock.Size = new System.Drawing.Size(500, 200);
            dgvStock.TabIndex = 0;
            dgvStock.CellClick += dgvStock_CellClick;

            lblProductFilter.Text = "Filtrar por producto:";
            lblProductFilter.Location = new System.Drawing.Point(20, 20);
            lblProductFilter.Size = new System.Drawing.Size(120, 20);

            cmbProductFilter.Location = new System.Drawing.Point(140, 20);
            cmbProductFilter.Size = new System.Drawing.Size(200, 23);
            cmbProductFilter.SelectedIndexChanged += cmbProductFilter_SelectedIndexChanged;

            dgvProducts.Location = new System.Drawing.Point(540, 40);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Size = new System.Drawing.Size(250, 120);
            dgvProducts.TabIndex = 1;
            dgvProducts.CellClick += dgvProducts_CellClick;

            lblSelectedProduct.Location = new System.Drawing.Point(540, 20);
            lblSelectedProduct.Size = new System.Drawing.Size(240, 20);
            lblSelectedProduct.Text = "Producto seleccionado:";

            rdoDeposit.Location = new System.Drawing.Point(540, 170);
            rdoDeposit.Name = "rdoDeposit";
            rdoDeposit.Size = new System.Drawing.Size(80, 24);
            rdoDeposit.TabIndex = 2;
            rdoDeposit.Text = "Depósito";
            rdoDeposit.CheckedChanged += rdoDeposit_CheckedChanged;

            rdoStation.Location = new System.Drawing.Point(630, 170);
            rdoStation.Name = "rdoStation";
            rdoStation.Size = new System.Drawing.Size(80, 24);
            rdoStation.TabIndex = 3;
            rdoStation.Text = "Estación";
            rdoStation.CheckedChanged += rdoStation_CheckedChanged;

            dgvLocations.Location = new System.Drawing.Point(540, 230);
            dgvLocations.Name = "dgvLocations";
            dgvLocations.Size = new System.Drawing.Size(240, 100);
            dgvLocations.TabIndex = 4;
            dgvLocations.CellClick += dgvLocations_CellClick;

            lblSelectedLocation.Location = new System.Drawing.Point(540, 210);
            lblSelectedLocation.Size = new System.Drawing.Size(240, 20);
            lblSelectedLocation.Text = "Ubicación seleccionada:";

            lblQuantity.Location = new System.Drawing.Point(20, 270);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new System.Drawing.Size(60, 23);
            lblQuantity.Text = "Cantidad:";

            txtQuantity.Location = new System.Drawing.Point(90, 270);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new System.Drawing.Size(100, 23);
            txtQuantity.TabIndex = 5;

            btnCreate.Location = new System.Drawing.Point(20, 310);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new System.Drawing.Size(75, 23);
            btnCreate.TabIndex = 6;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;

            btnUpdate.Location = new System.Drawing.Point(110, 310);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new System.Drawing.Size(75, 23);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Actualizar";
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Location = new System.Drawing.Point(200, 310);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(75, 23);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;

            ClientSize = new System.Drawing.Size(800, 360);
            Controls.Add(dgvStock);
            Controls.Add(lblProductFilter);
            Controls.Add(cmbProductFilter);
            Controls.Add(dgvProducts);
            Controls.Add(lblSelectedProduct);
            Controls.Add(rdoDeposit);
            Controls.Add(rdoStation);
            Controls.Add(lblSelectedLocation);
            Controls.Add(dgvLocations);
            Controls.Add(lblQuantity);
            Controls.Add(txtQuantity);
            Controls.Add(btnCreate);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Name = "StockForm";
            Text = "Gestión de Stock";

            ((System.ComponentModel.ISupportInitialize)(dgvStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dgvProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dgvLocations)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
