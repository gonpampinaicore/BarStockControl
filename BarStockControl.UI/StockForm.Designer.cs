namespace BarStockControl.UI
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
            dgvStock = new DataGridView();
            dgvProducts = new DataGridView();
            dgvLocations = new DataGridView();
            txtQuantity = new TextBox();
            lblQuantity = new Label();
            rdoDeposit = new RadioButton();
            rdoStation = new RadioButton();
            btnCreate = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            lblSelectedProduct = new Label();
            lblSelectedLocation = new Label();
            cmbProductFilter = new ComboBox();
            lblProductFilter = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLocations).BeginInit();
            SuspendLayout();
            // 
            // dgvStock
            // 
            dgvStock.Location = new Point(540, 39);
            dgvStock.Name = "dgvStock";
            dgvStock.Size = new Size(459, 433);
            dgvStock.TabIndex = 0;
            dgvStock.CellClick += dgvStock_CellClick;
            // 
            // dgvProducts
            // 
            dgvProducts.Location = new Point(20, 42);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Size = new Size(390, 197);
            dgvProducts.TabIndex = 1;
            dgvProducts.CellClick += dgvProducts_CellClick;
            // 
            // dgvLocations
            // 
            dgvLocations.Location = new Point(20, 369);
            dgvLocations.Name = "dgvLocations";
            dgvLocations.Size = new Size(314, 103);
            dgvLocations.TabIndex = 4;
            dgvLocations.CellClick += dgvLocations_CellClick;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(129, 256);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(100, 23);
            txtQuantity.TabIndex = 5;
            // 
            // lblQuantity
            // 
            lblQuantity.Location = new Point(20, 256);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(60, 23);
            lblQuantity.TabIndex = 5;
            lblQuantity.Text = "Cantidad:";
            // 
            // rdoDeposit
            // 
            rdoDeposit.Location = new Point(20, 297);
            rdoDeposit.Name = "rdoDeposit";
            rdoDeposit.Size = new Size(80, 24);
            rdoDeposit.TabIndex = 2;
            rdoDeposit.Text = "Depósito";
            rdoDeposit.CheckedChanged += rdoDeposit_CheckedChanged;
            // 
            // rdoStation
            // 
            rdoStation.Location = new Point(115, 297);
            rdoStation.Name = "rdoStation";
            rdoStation.Size = new Size(80, 24);
            rdoStation.TabIndex = 3;
            rdoStation.Text = "Estación";
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(20, 511);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 6;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(139, 511);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Actualizar";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(259, 511);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Eliminar";
            btnDelete.Click += btnDelete_Click;
            // 
            // lblSelectedProduct
            // 
            lblSelectedProduct.Location = new Point(20, 9);
            lblSelectedProduct.Name = "lblSelectedProduct";
            lblSelectedProduct.Size = new Size(240, 20);
            lblSelectedProduct.TabIndex = 3;
            lblSelectedProduct.Text = "Producto seleccionado:";
            // 
            // lblSelectedLocation
            // 
            lblSelectedLocation.Location = new Point(20, 336);
            lblSelectedLocation.Name = "lblSelectedLocation";
            lblSelectedLocation.Size = new Size(240, 20);
            lblSelectedLocation.TabIndex = 4;
            lblSelectedLocation.Text = "Ubicación seleccionada:";
            // 
            // cmbProductFilter
            // 
            cmbProductFilter.Location = new Point(666, 6);
            cmbProductFilter.Name = "cmbProductFilter";
            cmbProductFilter.Size = new Size(200, 23);
            cmbProductFilter.TabIndex = 2;
            cmbProductFilter.SelectedIndexChanged += cmbProductFilter_SelectedIndexChanged;
            // 
            // lblProductFilter
            // 
            lblProductFilter.Location = new Point(540, 9);
            lblProductFilter.Name = "lblProductFilter";
            lblProductFilter.Size = new Size(120, 20);
            lblProductFilter.TabIndex = 1;
            lblProductFilter.Text = "Filtrar por producto:";
            // 
            // StockForm
            // 
            ClientSize = new Size(1022, 548);
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
            ((System.ComponentModel.ISupportInitialize)dgvStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLocations).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
