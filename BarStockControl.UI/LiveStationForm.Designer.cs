namespace BarStockControl.UI
{
    partial class LiveStationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "LiveStationForm";
            lblOrderItems = new Label();
            lblOrderItems.Text = "Tragos de la orden";
            lblOrderItems.Location = new System.Drawing.Point(10, 30);
            lblOrderItems.Size = new System.Drawing.Size(400, 20);
            lblOrderItems.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            Controls.Add(lblOrderItems);
            dgvOrderItems = new DataGridView();
            dgvOrderItems.Location = new System.Drawing.Point(10, 55);
            dgvOrderItems.Size = new System.Drawing.Size(400, 120);
            Controls.Add(dgvOrderItems);
            lblRecipeItems = new Label();
            lblRecipeItems.Text = "Receta del trago seleccionado";
            lblRecipeItems.Location = new System.Drawing.Point(10, 180);
            lblRecipeItems.Size = new System.Drawing.Size(400, 20);
            lblRecipeItems.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            Controls.Add(lblRecipeItems);
            dgvRecipeItems = new DataGridView();
            dgvRecipeItems.Location = new System.Drawing.Point(10, 205);
            dgvRecipeItems.Size = new System.Drawing.Size(400, 100);
            Controls.Add(dgvRecipeItems);
            lblStock = new Label();
            lblStock.Text = "Stock de la estación";
            lblStock.Location = new System.Drawing.Point(420, 30);
            lblStock.Size = new System.Drawing.Size(300, 20);
            lblStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            Controls.Add(lblStock);
            dgvStock = new DataGridView();
            dgvStock.Location = new System.Drawing.Point(420, 55);
            dgvStock.Size = new System.Drawing.Size(300, 230);
            Controls.Add(dgvStock);
            btnPreparar = new Button();
            btnPreparar.Text = "Marcar En Preparación";
            btnPreparar.Location = new System.Drawing.Point(10, 320);
            btnPreparar.Size = new System.Drawing.Size(150, 30);
            Controls.Add(btnPreparar);
            btnEntregar = new Button();
            btnEntregar.Text = "Marcar Entregado";
            btnEntregar.Location = new System.Drawing.Point(170, 320);
            btnEntregar.Size = new System.Drawing.Size(150, 30);
            Controls.Add(btnEntregar);
        }

        #endregion

        private Label lblOrderItems;
        private Label lblRecipeItems;
        private Label lblStock;
        private DataGridView dgvOrderItems;
        private DataGridView dgvRecipeItems;
        private DataGridView dgvStock;
        private Button btnPreparar;
        private Button btnEntregar;
    }
}
