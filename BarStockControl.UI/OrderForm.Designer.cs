namespace BarStockControl.UI
{
    partial class OrderForm
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbDrinks;
        private NumericUpDown nudQuantity;
        private Button btnAddItem;
        private DataGridView dgvItems;
        private Button btnConfirm;
        private Button btnCancel;
        private Label lblDrink;
        private Label lblQuantity;
        private Label lblTotal;
        private Label lblTotalValue;
        private DataGridView dgvDrinks;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            cmbDrinks = new ComboBox();
            nudQuantity = new NumericUpDown();
            btnAddItem = new Button();
            dgvItems = new DataGridView();
            btnConfirm = new Button();
            btnCancel = new Button();
            lblDrink = new Label();
            lblQuantity = new Label();
            lblTotal = new Label();
            lblTotalValue = new Label();

            ((System.ComponentModel.ISupportInitialize)(nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dgvItems)).BeginInit();
            SuspendLayout();

            // lblDrink
            lblDrink.AutoSize = true;
            lblDrink.Location = new System.Drawing.Point(20, 20);
            lblDrink.Name = "lblDrink";
            lblDrink.Size = new System.Drawing.Size(44, 15);
            lblDrink.Text = "Trago:";

            // dgvDrinks
            this.dgvDrinks = new System.Windows.Forms.DataGridView();
            this.dgvDrinks.Location = new System.Drawing.Point(80, 17);
            this.dgvDrinks.Name = "dgvDrinks";
            this.dgvDrinks.Size = new System.Drawing.Size(400, 180);
            this.dgvDrinks.ReadOnly = true;
            this.dgvDrinks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvDrinks.MultiSelect = false;
            this.dgvDrinks.AllowUserToAddRows = false;
            this.dgvDrinks.AllowUserToDeleteRows = false;
            this.dgvDrinks.RowHeadersVisible = false;
            this.dgvDrinks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDrinks.TabIndex = 0;

            // cmbDrinks
            cmbDrinks.Location = new System.Drawing.Point(80, 17);
            cmbDrinks.Name = "cmbDrinks";
            cmbDrinks.Size = new System.Drawing.Size(200, 23);
            cmbDrinks.TabIndex = 0;

            // lblQuantity
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new System.Drawing.Point(300, 20);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new System.Drawing.Size(61, 15);
            lblQuantity.Text = "Cantidad:";

            // nudQuantity
            nudQuantity.Location = new System.Drawing.Point(500, 17);
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new System.Drawing.Size(60, 23);
            nudQuantity.Minimum = 1;
            nudQuantity.Maximum = 1000;
            nudQuantity.Value = 1;
            nudQuantity.TabIndex = 1;

            // btnAddItem
            btnAddItem.Location = new System.Drawing.Point(580, 15);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new System.Drawing.Size(100, 25);
            btnAddItem.TabIndex = 2;
            btnAddItem.Text = "Agregar ítem";
            btnAddItem.UseVisualStyleBackColor = true;
            btnAddItem.Click += new System.EventHandler(btnAddItem_Click);

            // dgvItems
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Location = new System.Drawing.Point(20, 210);
            dgvItems.Name = "dgvItems";
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersVisible = false;
            dgvItems.Size = new System.Drawing.Size(660, 220);
            dgvItems.TabIndex = 3;
            dgvItems.Columns.Clear();
            dgvItems.Columns.Add("DrinkName", "Trago");
            dgvItems.Columns.Add("Quantity", "Cantidad");
            dgvItems.Columns.Add("UnitPrice", "Precio Unitario");
            dgvItems.Columns.Add("Subtotal", "Subtotal");
            var btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "Eliminar";
            btnDelete.HeaderText = "Eliminar";
            btnDelete.Text = "Eliminar";
            btnDelete.UseColumnTextForButtonValue = true;
            dgvItems.Columns.Add(btnDelete);
            dgvItems.CellContentClick += new DataGridViewCellEventHandler(dgvItems_CellContentClick);

            // lblTotal
            lblTotal.AutoSize = true;
            lblTotal.Location = new System.Drawing.Point(500, 440);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new System.Drawing.Size(38, 15);
            lblTotal.Text = "Total:";

            // lblTotalValue
            lblTotalValue.AutoSize = true;
            lblTotalValue.Location = new System.Drawing.Point(550, 440);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new System.Drawing.Size(34, 15);
            lblTotalValue.Text = "$0.00";

            // btnConfirm
            btnConfirm.Location = new System.Drawing.Point(20, 480);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new System.Drawing.Size(120, 35);
            btnConfirm.TabIndex = 4;
            btnConfirm.Text = "Cobrar";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += new System.EventHandler(btnConfirm_Click);

            // btnCancel
            btnCancel.Location = new System.Drawing.Point(160, 480);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(120, 35);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new System.EventHandler(btnCancel_Click);

            // OrderForm
            ClientSize = new System.Drawing.Size(700, 540);
            Controls.Add(lblDrink);
            Controls.Add(dgvDrinks);
            Controls.Add(lblQuantity);
            Controls.Add(nudQuantity);
            Controls.Add(btnAddItem);
            Controls.Add(dgvItems);
            Controls.Add(lblTotal);
            Controls.Add(lblTotalValue);
            Controls.Add(btnConfirm);
            Controls.Add(btnCancel);
            Name = "OrderForm";
            Text = "Nueva Orden";

            ((System.ComponentModel.ISupportInitialize)(nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dgvItems)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
