namespace BarStockControl.UI
{
    partial class DrinkForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvDrinks = new DataGridView();
            txtSearch = new TextBox();
            txtName = new TextBox();
            numPrice = new NumericUpDown();
            chkIsComposed = new CheckBox();
            btnCreate = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            lblSearch = new Label();
            lblName = new Label();
            lblPrice = new Label();
            lblEstimatedCost = new Label();
            pnlRecipe = new Panel();
            dgvRecipeItems = new DataGridView();
            cboProduct = new ComboBox();
            numQuantity = new NumericUpDown();
            btnAddProduct = new Button();
            btnRemoveProduct = new Button();
            lblProduct = new Label();
            lblQuantity = new Label();
            btnCalculateEstimatedCost = new Button();
            btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDrinks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            pnlRecipe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecipeItems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            SuspendLayout();
            // 
            // dgvDrinks
            // 
            dgvDrinks.AllowUserToAddRows = false;
            dgvDrinks.AllowUserToDeleteRows = false;
            dgvDrinks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDrinks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDrinks.Location = new Point(14, 46);
            dgvDrinks.Margin = new Padding(4, 3, 4, 3);
            dgvDrinks.MultiSelect = false;
            dgvDrinks.Name = "dgvDrinks";
            dgvDrinks.ReadOnly = true;
            dgvDrinks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDrinks.Size = new Size(583, 231);
            dgvDrinks.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(82, 14);
            txtSearch.Margin = new Padding(4, 3, 4, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(233, 23);
            txtSearch.TabIndex = 1;
            // 
            // txtName
            // 
            txtName.Location = new Point(82, 288);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(233, 23);
            txtName.TabIndex = 3;
            // 
            // numPrice
            // 
            numPrice.DecimalPlaces = 2;
            numPrice.Location = new Point(82, 323);
            numPrice.Margin = new Padding(4, 3, 4, 3);
            numPrice.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(140, 23);
            numPrice.TabIndex = 5;
            // 
            // chkIsComposed
            // 
            chkIsComposed.AutoSize = true;
            chkIsComposed.Location = new Point(82, 358);
            chkIsComposed.Margin = new Padding(4, 3, 4, 3);
            chkIsComposed.Name = "chkIsComposed";
            chkIsComposed.Size = new Size(102, 19);
            chkIsComposed.TabIndex = 7;
            chkIsComposed.Text = "Es Compuesto";
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(14, 750);
            btnCreate.Margin = new Padding(4, 3, 4, 3);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(88, 27);
            btnCreate.TabIndex = 18;
            btnCreate.Text = "Crear";
            btnCreate.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(108, 750);
            btnUpdate.Margin = new Padding(4, 3, 4, 3);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(88, 27);
            btnUpdate.TabIndex = 19;
            btnUpdate.Text = "Actualizar";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(203, 750);
            btnDelete.Margin = new Padding(4, 3, 4, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(88, 27);
            btnDelete.TabIndex = 20;
            btnDelete.Text = "Eliminar";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(14, 17);
            lblSearch.Margin = new Padding(4, 0, 4, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(45, 15);
            lblSearch.TabIndex = 2;
            lblSearch.Text = "Buscar:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(14, 292);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(54, 15);
            lblName.TabIndex = 4;
            lblName.Text = "Nombre:";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(14, 325);
            lblPrice.Margin = new Padding(4, 0, 4, 0);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(43, 15);
            lblPrice.TabIndex = 6;
            lblPrice.Text = "Precio:";
            // 
            // lblEstimatedCost
            // 
            lblEstimatedCost.AutoSize = true;
            lblEstimatedCost.Location = new Point(12, 312);
            lblEstimatedCost.Margin = new Padding(4, 0, 4, 0);
            lblEstimatedCost.Name = "lblEstimatedCost";
            lblEstimatedCost.Size = new Size(123, 15);
            lblEstimatedCost.TabIndex = 16;
            lblEstimatedCost.Text = "Costo Estimado: $0.00";
            // 
            // pnlRecipe
            // 
            pnlRecipe.BorderStyle = BorderStyle.FixedSingle;
            pnlRecipe.Controls.Add(dgvRecipeItems);
            pnlRecipe.Controls.Add(cboProduct);
            pnlRecipe.Controls.Add(numQuantity);
            pnlRecipe.Controls.Add(btnAddProduct);
            pnlRecipe.Controls.Add(btnRemoveProduct);
            pnlRecipe.Controls.Add(lblProduct);
            pnlRecipe.Controls.Add(lblQuantity);
            pnlRecipe.Controls.Add(lblEstimatedCost);
            pnlRecipe.Controls.Add(btnCalculateEstimatedCost);
            pnlRecipe.Location = new Point(14, 392);
            pnlRecipe.Margin = new Padding(4, 3, 4, 3);
            pnlRecipe.Name = "pnlRecipe";
            pnlRecipe.Size = new Size(583, 346);
            pnlRecipe.TabIndex = 8;
            pnlRecipe.Visible = false;
            // 
            // dgvRecipeItems
            // 
            dgvRecipeItems.AllowUserToAddRows = false;
            dgvRecipeItems.AllowUserToDeleteRows = false;
            dgvRecipeItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecipeItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecipeItems.Location = new Point(12, 115);
            dgvRecipeItems.Margin = new Padding(4, 3, 4, 3);
            dgvRecipeItems.MultiSelect = false;
            dgvRecipeItems.Name = "dgvRecipeItems";
            dgvRecipeItems.ReadOnly = true;
            dgvRecipeItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecipeItems.Size = new Size(560, 173);
            dgvRecipeItems.TabIndex = 9;
            // 
            // cboProduct
            // 
            cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProduct.FormattingEnabled = true;
            cboProduct.Location = new Point(82, 12);
            cboProduct.Margin = new Padding(4, 3, 4, 3);
            cboProduct.Name = "cboProduct";
            cboProduct.Size = new Size(233, 23);
            cboProduct.TabIndex = 10;
            // 
            // numQuantity
            // 
            numQuantity.DecimalPlaces = 2;
            numQuantity.Location = new Point(82, 46);
            numQuantity.Margin = new Padding(4, 3, 4, 3);
            numQuantity.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(140, 23);
            numQuantity.TabIndex = 12;
            numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAddProduct
            // 
            btnAddProduct.Location = new Point(82, 81);
            btnAddProduct.Margin = new Padding(4, 3, 4, 3);
            btnAddProduct.Name = "btnAddProduct";
            btnAddProduct.Size = new Size(88, 27);
            btnAddProduct.TabIndex = 14;
            btnAddProduct.Text = "Agregar";
            btnAddProduct.UseVisualStyleBackColor = true;
            // 
            // btnRemoveProduct
            // 
            btnRemoveProduct.Location = new Point(175, 81);
            btnRemoveProduct.Margin = new Padding(4, 3, 4, 3);
            btnRemoveProduct.Name = "btnRemoveProduct";
            btnRemoveProduct.Size = new Size(88, 27);
            btnRemoveProduct.TabIndex = 15;
            btnRemoveProduct.Text = "Quitar";
            btnRemoveProduct.UseVisualStyleBackColor = true;
            // 
            // lblProduct
            // 
            lblProduct.AutoSize = true;
            lblProduct.Location = new Point(12, 15);
            lblProduct.Margin = new Padding(4, 0, 4, 0);
            lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(59, 15);
            lblProduct.TabIndex = 11;
            lblProduct.Text = "Producto:";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(12, 48);
            lblQuantity.Margin = new Padding(4, 0, 4, 0);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(58, 15);
            lblQuantity.TabIndex = 13;
            lblQuantity.Text = "Cantidad:";
            // 
            // btnCalculateEstimatedCost
            // 
            btnCalculateEstimatedCost.Location = new Point(175, 306);
            btnCalculateEstimatedCost.Margin = new Padding(4, 3, 4, 3);
            btnCalculateEstimatedCost.Name = "btnCalculateEstimatedCost";
            btnCalculateEstimatedCost.Size = new Size(140, 27);
            btnCalculateEstimatedCost.TabIndex = 17;
            btnCalculateEstimatedCost.Text = "Calcular Costo";
            btnCalculateEstimatedCost.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(411, 313);
            btnClear.Margin = new Padding(4, 3, 4, 3);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(114, 27);
            btnClear.TabIndex = 0;
            btnClear.Text = "Limpiar";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // DrinkForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(611, 790);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnCreate);
            Controls.Add(pnlRecipe);
            Controls.Add(chkIsComposed);
            Controls.Add(lblPrice);
            Controls.Add(numPrice);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
            Controls.Add(dgvDrinks);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "DrinkForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Tragos";
            ((System.ComponentModel.ISupportInitialize)dgvDrinks).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            pnlRecipe.ResumeLayout(false);
            pnlRecipe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecipeItems).EndInit();
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDrinks;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.CheckBox chkIsComposed;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblEstimatedCost;
        private System.Windows.Forms.Panel pnlRecipe;
        private System.Windows.Forms.DataGridView dgvRecipeItems;
        private System.Windows.Forms.ComboBox cboProduct;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnRemoveProduct;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Button btnCalculateEstimatedCost;
        private System.Windows.Forms.Button btnClear;
    }
} 
