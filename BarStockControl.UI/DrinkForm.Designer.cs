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
            this.components = new System.ComponentModel.Container();
            
            // Controles principales
            this.dgvDrinks = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.chkIsComposed = new System.Windows.Forms.CheckBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblEstimatedCost = new System.Windows.Forms.Label();

            // Panel de receta
            this.pnlRecipe = new System.Windows.Forms.Panel();
            this.dgvRecipeItems = new System.Windows.Forms.DataGridView();
            this.cboProduct = new System.Windows.Forms.ComboBox();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnRemoveProduct = new System.Windows.Forms.Button();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.btnCalculateEstimatedCost = new System.Windows.Forms.Button();

            // Configuración de controles
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrinks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipeItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.pnlRecipe.SuspendLayout();
            this.SuspendLayout();

            // dgvDrinks
            this.dgvDrinks.AllowUserToAddRows = false;
            this.dgvDrinks.AllowUserToDeleteRows = false;
            this.dgvDrinks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDrinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDrinks.Location = new System.Drawing.Point(12, 40);
            this.dgvDrinks.MultiSelect = false;
            this.dgvDrinks.Name = "dgvDrinks";
            this.dgvDrinks.ReadOnly = true;
            this.dgvDrinks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDrinks.Size = new System.Drawing.Size(500, 200);
            this.dgvDrinks.TabIndex = 0;

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(70, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 20);
            this.txtSearch.TabIndex = 1;

            // lblSearch
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 15);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(43, 13);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Buscar:";

            // txtName
            this.txtName.Location = new System.Drawing.Point(70, 250);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.TabIndex = 3;

            // lblName
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 253);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(47, 13);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Nombre:";

            // numPrice
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Location = new System.Drawing.Point(70, 280);
            this.numPrice.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(120, 20);
            this.numPrice.TabIndex = 5;

            // lblPrice
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(12, 282);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(40, 13);
            this.lblPrice.TabIndex = 6;
            this.lblPrice.Text = "Precio:";

            // chkIsComposed
            this.chkIsComposed.AutoSize = true;
            this.chkIsComposed.Location = new System.Drawing.Point(70, 310);
            this.chkIsComposed.Name = "chkIsComposed";
            this.chkIsComposed.Size = new System.Drawing.Size(93, 17);
            this.chkIsComposed.TabIndex = 7;
            this.chkIsComposed.Text = "Es Compuesto";

            // Panel de Receta
            this.pnlRecipe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRecipe.Controls.Add(this.dgvRecipeItems);
            this.pnlRecipe.Controls.Add(this.cboProduct);
            this.pnlRecipe.Controls.Add(this.numQuantity);
            this.pnlRecipe.Controls.Add(this.btnAddProduct);
            this.pnlRecipe.Controls.Add(this.btnRemoveProduct);
            this.pnlRecipe.Controls.Add(this.lblProduct);
            this.pnlRecipe.Controls.Add(this.lblQuantity);
            this.pnlRecipe.Controls.Add(this.lblEstimatedCost);
            this.pnlRecipe.Controls.Add(this.btnCalculateEstimatedCost);
            this.pnlRecipe.Location = new System.Drawing.Point(12, 340);
            this.pnlRecipe.Name = "pnlRecipe";
            this.pnlRecipe.Size = new System.Drawing.Size(500, 300);
            this.pnlRecipe.TabIndex = 8;
            this.pnlRecipe.Visible = false;

            // dgvRecipeItems
            this.dgvRecipeItems.AllowUserToAddRows = false;
            this.dgvRecipeItems.AllowUserToDeleteRows = false;
            this.dgvRecipeItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecipeItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecipeItems.Location = new System.Drawing.Point(10, 100);
            this.dgvRecipeItems.MultiSelect = false;
            this.dgvRecipeItems.Name = "dgvRecipeItems";
            this.dgvRecipeItems.ReadOnly = true;
            this.dgvRecipeItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecipeItems.Size = new System.Drawing.Size(480, 150);
            this.dgvRecipeItems.TabIndex = 9;

            // cboProduct
            this.cboProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProduct.FormattingEnabled = true;
            this.cboProduct.Location = new System.Drawing.Point(70, 10);
            this.cboProduct.Name = "cboProduct";
            this.cboProduct.Size = new System.Drawing.Size(200, 21);
            this.cboProduct.TabIndex = 10;

            // lblProduct
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(10, 13);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(53, 13);
            this.lblProduct.TabIndex = 11;
            this.lblProduct.Text = "Producto:";

            // numQuantity
            this.numQuantity.DecimalPlaces = 2;
            this.numQuantity.Location = new System.Drawing.Point(70, 40);
            this.numQuantity.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.numQuantity.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(120, 20);
            this.numQuantity.TabIndex = 12;
            this.numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // lblQuantity
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(10, 42);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(52, 13);
            this.lblQuantity.TabIndex = 13;
            this.lblQuantity.Text = "Cantidad:";

            // btnAddProduct
            this.btnAddProduct.Location = new System.Drawing.Point(70, 70);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(75, 23);
            this.btnAddProduct.TabIndex = 14;
            this.btnAddProduct.Text = "Agregar";
            this.btnAddProduct.UseVisualStyleBackColor = true;

            // btnRemoveProduct
            this.btnRemoveProduct.Location = new System.Drawing.Point(150, 70);
            this.btnRemoveProduct.Name = "btnRemoveProduct";
            this.btnRemoveProduct.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveProduct.TabIndex = 15;
            this.btnRemoveProduct.Text = "Quitar";
            this.btnRemoveProduct.UseVisualStyleBackColor = true;

            // lblEstimatedCost
            this.lblEstimatedCost.AutoSize = true;
            this.lblEstimatedCost.Location = new System.Drawing.Point(10, 270);
            this.lblEstimatedCost.Name = "lblEstimatedCost";
            this.lblEstimatedCost.Size = new System.Drawing.Size(89, 13);
            this.lblEstimatedCost.TabIndex = 16;
            this.lblEstimatedCost.Text = "Costo Estimado: $0.00";

            // btnCalculateEstimatedCost
            this.btnCalculateEstimatedCost.Location = new System.Drawing.Point(150, 265);
            this.btnCalculateEstimatedCost.Name = "btnCalculateEstimatedCost";
            this.btnCalculateEstimatedCost.Size = new System.Drawing.Size(120, 23);
            this.btnCalculateEstimatedCost.TabIndex = 17;
            this.btnCalculateEstimatedCost.Text = "Calcular Costo";
            this.btnCalculateEstimatedCost.UseVisualStyleBackColor = true;

            // Botones principales
            this.btnCreate.Location = new System.Drawing.Point(12, 650);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 18;
            this.btnCreate.Text = "Crear";
            this.btnCreate.UseVisualStyleBackColor = true;

            this.btnUpdate.Location = new System.Drawing.Point(93, 650);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 19;
            this.btnUpdate.Text = "Actualizar";
            this.btnUpdate.UseVisualStyleBackColor = true;

            this.btnDelete.Location = new System.Drawing.Point(174, 650);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.UseVisualStyleBackColor = true;

            this.btnClear = new System.Windows.Forms.Button();

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 685);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.pnlRecipe);
            this.Controls.Add(this.chkIsComposed);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.numPrice);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvDrinks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DrinkForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Tragos";

            ((System.ComponentModel.ISupportInitialize)(this.dgvDrinks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipeItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.pnlRecipe.ResumeLayout(false);
            this.pnlRecipe.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
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
