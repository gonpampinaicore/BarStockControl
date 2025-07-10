namespace BarStockControl.UI
{
    partial class LiveBarForm
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Text = "Gestión de Bar - Evento en Vivo";
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitle = new Label();
            lblTitle.Text = "Gestión de Bar - Evento en Vivo";
            lblTitle.Location = new System.Drawing.Point(10, 10);
            lblTitle.Size = new System.Drawing.Size(400, 25);
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            Controls.Add(lblTitle);

            lblOrders = new Label();
            lblOrders.Text = "Órdenes del Evento";
            lblOrders.Location = new System.Drawing.Point(10, 45);
            lblOrders.Size = new System.Drawing.Size(200, 20);
            lblOrders.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            Controls.Add(lblOrders);

            dgvOrders = new DataGridView();
            dgvOrders.Location = new System.Drawing.Point(10, 70);
            dgvOrders.Size = new System.Drawing.Size(580, 150);
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.ReadOnly = true;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Controls.Add(dgvOrders);

            lblStations = new Label();
            lblStations.Text = "Estaciones del Evento";
            lblStations.Location = new System.Drawing.Point(10, 240);
            lblStations.Size = new System.Drawing.Size(200, 20);
            lblStations.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            Controls.Add(lblStations);

            cboStations = new ComboBox();
            cboStations.Location = new System.Drawing.Point(10, 265);
            cboStations.Size = new System.Drawing.Size(250, 25);
            cboStations.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(cboStations);

            lblStationStock = new Label();
            lblStationStock.Text = "Stock de la Estación Seleccionada";
            lblStationStock.Location = new System.Drawing.Point(10, 300);
            lblStationStock.Size = new System.Drawing.Size(300, 20);
            lblStationStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            Controls.Add(lblStationStock);

            dgvStationStock = new DataGridView();
            dgvStationStock.Location = new System.Drawing.Point(10, 325);
            dgvStationStock.Size = new System.Drawing.Size(580, 150);
            dgvStationStock.AllowUserToAddRows = false;
            dgvStationStock.AllowUserToDeleteRows = false;
            dgvStationStock.ReadOnly = true;
            dgvStationStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Controls.Add(dgvStationStock);

            lblBarmanOrders = new Label();
            lblBarmanOrders.Text = "Órdenes preparadas por barman de la estación";
            lblBarmanOrders.Location = new System.Drawing.Point(10, 490);
            lblBarmanOrders.Size = new System.Drawing.Size(400, 20);
            lblBarmanOrders.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            Controls.Add(lblBarmanOrders);

            dgvBarmanOrders = new DataGridView();
            dgvBarmanOrders.Location = new System.Drawing.Point(10, 515);
            dgvBarmanOrders.Size = new System.Drawing.Size(580, 150);
            dgvBarmanOrders.AllowUserToAddRows = false;
            dgvBarmanOrders.AllowUserToDeleteRows = false;
            dgvBarmanOrders.ReadOnly = true;
            dgvBarmanOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Controls.Add(dgvBarmanOrders);

            lblTotalStock = new Label();
            lblTotalStock.Text = "Stock Total de Todas las Estaciones";
            lblTotalStock.Location = new System.Drawing.Point(610, 45);
            lblTotalStock.Size = new System.Drawing.Size(350, 20);
            lblTotalStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            Controls.Add(lblTotalStock);

            dgvTotalStock = new DataGridView();
            dgvTotalStock.Location = new System.Drawing.Point(610, 70);
            dgvTotalStock.Size = new System.Drawing.Size(570, 405);
            dgvTotalStock.AllowUserToAddRows = false;
            dgvTotalStock.AllowUserToDeleteRows = false;
            dgvTotalStock.ReadOnly = true;
            dgvTotalStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Controls.Add(dgvTotalStock);
        }

        private Label lblTitle;
        private Label lblOrders;
        private Label lblStations;
        private Label lblStationStock;
        private Label lblTotalStock;
        private DataGridView dgvOrders;
        private DataGridView dgvStationStock;
        private DataGridView dgvTotalStock;
        private ComboBox cboStations;
        private Label lblBarmanOrders;
        private DataGridView dgvBarmanOrders;
    }
}
