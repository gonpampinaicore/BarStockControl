// StockMovementForm.Designer.cs
namespace BarStockControl.Forms.StockMovements
{
    partial class StockMovementForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cmbEvent;
        private System.Windows.Forms.RadioButton rdoFromDeposit;
        private System.Windows.Forms.RadioButton rdoFromStation;
        private System.Windows.Forms.ComboBox cmbFromLocation;
        private System.Windows.Forms.DataGridView dgvFromStock;
        private System.Windows.Forms.RadioButton rdoToDeposit;
        private System.Windows.Forms.RadioButton rdoToStation;
        private System.Windows.Forms.ComboBox cmbToLocation;
        private System.Windows.Forms.DataGridView dgvToStock;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.DataGridView dgvMovements;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnChangeStatus;
        private System.Windows.Forms.Label lblEvent;
        private System.Windows.Forms.Label lblFromLocation;
        private System.Windows.Forms.Label lblFromStock;
        private System.Windows.Forms.Label lblToLocation;
        private System.Windows.Forms.Label lblToStock;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Label lblMovements;
        private System.Windows.Forms.Label lblStatus;

        private void InitializeComponent()
        {
            cmbEvent = new ComboBox();
            rdoFromDeposit = new RadioButton();
            rdoFromStation = new RadioButton();
            cmbFromLocation = new ComboBox();
            dgvFromStock = new DataGridView();
            rdoToDeposit = new RadioButton();
            rdoToStation = new RadioButton();
            cmbToLocation = new ComboBox();
            dgvToStock = new DataGridView();
            txtQuantity = new TextBox();
            txtComment = new TextBox();
            btnCreate = new Button();
            dgvMovements = new DataGridView();
            cmbStatus = new ComboBox();
            btnChangeStatus = new Button();
            lblEvent = new Label();
            lblFromLocation = new Label();
            lblFromStock = new Label();
            lblToLocation = new Label();
            lblToStock = new Label();
            lblQuantity = new Label();
            lblComment = new Label();
            lblMovements = new Label();
            lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvFromStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvToStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMovements).BeginInit();
            SuspendLayout();

            cmbEvent.Location = new Point(20, 30);
            cmbEvent.Name = "cmbEvent";
            cmbEvent.Size = new Size(350, 23);
            cmbEvent.TabIndex = 1;
            cmbEvent.SelectedIndexChanged += cmbEvent_SelectedIndexChanged;

            rdoFromDeposit.Location = new Point(20, 90);
            rdoFromDeposit.Name = "rdoFromDeposit";
            rdoFromDeposit.Size = new Size(100, 20);
            rdoFromDeposit.TabIndex = 3;
            rdoFromDeposit.Text = "Depósito";

            rdoFromStation.Location = new Point(130, 90);
            rdoFromStation.Name = "rdoFromStation";
            rdoFromStation.Size = new Size(100, 20);
            rdoFromStation.TabIndex = 4;
            rdoFromStation.Text = "Estación";

            cmbFromLocation.Location = new Point(20, 115);
            cmbFromLocation.Name = "cmbFromLocation";
            cmbFromLocation.Size = new Size(350, 23);
            cmbFromLocation.TabIndex = 5;

            dgvFromStock.AllowUserToAddRows = false;
            dgvFromStock.AllowUserToDeleteRows = false;
            dgvFromStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvFromStock.Location = new Point(20, 165);
            dgvFromStock.Name = "dgvFromStock";
            dgvFromStock.ReadOnly = true;
            dgvFromStock.Size = new Size(540, 180);
            dgvFromStock.TabIndex = 7;

            rdoToDeposit.Location = new Point(580, 90);
            rdoToDeposit.Name = "rdoToDeposit";
            rdoToDeposit.Size = new Size(100, 20);
            rdoToDeposit.TabIndex = 9;
            rdoToDeposit.Text = "Depósito";

            rdoToStation.Location = new Point(690, 90);
            rdoToStation.Name = "rdoToStation";
            rdoToStation.Size = new Size(100, 20);
            rdoToStation.TabIndex = 10;
            rdoToStation.Text = "Estación";

            cmbToLocation.Location = new Point(580, 115);
            cmbToLocation.Name = "cmbToLocation";
            cmbToLocation.Size = new Size(350, 23);
            cmbToLocation.TabIndex = 11;

            dgvToStock.AllowUserToAddRows = false;
            dgvToStock.AllowUserToDeleteRows = false;
            dgvToStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvToStock.Location = new Point(580, 165);
            dgvToStock.Name = "dgvToStock";
            dgvToStock.ReadOnly = true;
            dgvToStock.Size = new Size(540, 180);
            dgvToStock.TabIndex = 13;

            txtQuantity.Location = new Point(20, 380);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(100, 23);
            txtQuantity.TabIndex = 15;

            txtComment.Location = new Point(140, 380);
            txtComment.Name = "txtComment";
            txtComment.Size = new Size(300, 23);
            txtComment.TabIndex = 17;

            btnCreate.Location = new Point(460, 380);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(100, 23);
            btnCreate.TabIndex = 18;
            btnCreate.Text = "Crear";
            btnCreate.Click += btnCreate_Click;

            dgvMovements.AllowUserToAddRows = false;
            dgvMovements.AllowUserToDeleteRows = false;
            dgvMovements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvMovements.Location = new Point(20, 440);
            dgvMovements.Name = "dgvMovements";
            dgvMovements.ReadOnly = true;
            dgvMovements.Size = new Size(1100, 180);
            dgvMovements.TabIndex = 20;

            cmbStatus.Location = new Point(20, 660);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(150, 23);
            cmbStatus.TabIndex = 22;

            btnChangeStatus.Location = new Point(190, 660);
            btnChangeStatus.Name = "btnChangeStatus";
            btnChangeStatus.Size = new Size(100, 23);
            btnChangeStatus.TabIndex = 23;
            btnChangeStatus.Text = "Cambiar";

            lblEvent.Location = new Point(20, 10);
            lblEvent.Name = "lblEvent";
            lblEvent.Size = new Size(300, 15);
            lblEvent.TabIndex = 0;
            lblEvent.Text = "Evento activo";

            lblFromLocation.Location = new Point(20, 70);
            lblFromLocation.Name = "lblFromLocation";
            lblFromLocation.Size = new Size(300, 15);
            lblFromLocation.TabIndex = 2;
            lblFromLocation.Text = "Origen del producto";

            lblFromStock.Location = new Point(20, 145);
            lblFromStock.Name = "lblFromStock";
            lblFromStock.Size = new Size(300, 15);
            lblFromStock.TabIndex = 6;
            lblFromStock.Text = "Stock disponible en origen";

            lblToLocation.Location = new Point(580, 70);
            lblToLocation.Name = "lblToLocation";
            lblToLocation.Size = new Size(300, 15);
            lblToLocation.TabIndex = 8;
            lblToLocation.Text = "Destino del producto";

            lblToStock.Location = new Point(580, 145);
            lblToStock.Name = "lblToStock";
            lblToStock.Size = new Size(300, 15);
            lblToStock.TabIndex = 12;
            lblToStock.Text = "Stock actual en destino";

            lblQuantity.Location = new Point(20, 360);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(150, 17);
            lblQuantity.TabIndex = 14;
            lblQuantity.Text = "Cantidad a trasladar";

            lblComment.Location = new Point(140, 360);
            lblComment.Name = "lblComment";
            lblComment.Size = new Size(150, 17);
            lblComment.TabIndex = 16;
            lblComment.Text = "Comentario (opcional)";

            lblMovements.Location = new Point(20, 420);
            lblMovements.Name = "lblMovements";
            lblMovements.Size = new Size(400, 15);
            lblMovements.TabIndex = 19;
            lblMovements.Text = "Movimientos realizados en este evento";

            lblStatus.Location = new Point(20, 640);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(150, 15);
            lblStatus.TabIndex = 21;
            lblStatus.Text = "Estado del movimiento";

            ClientSize = new Size(1150, 750);
            Controls.Add(lblEvent);
            Controls.Add(cmbEvent);
            Controls.Add(lblFromLocation);
            Controls.Add(rdoFromDeposit);
            Controls.Add(rdoFromStation);
            Controls.Add(cmbFromLocation);
            Controls.Add(lblFromStock);
            Controls.Add(dgvFromStock);
            Controls.Add(lblToLocation);
            Controls.Add(rdoToDeposit);
            Controls.Add(rdoToStation);
            Controls.Add(cmbToLocation);
            Controls.Add(lblToStock);
            Controls.Add(dgvToStock);
            Controls.Add(lblQuantity);
            Controls.Add(txtQuantity);
            Controls.Add(lblComment);
            Controls.Add(txtComment);
            Controls.Add(btnCreate);
            Controls.Add(lblMovements);
            Controls.Add(dgvMovements);
            Controls.Add(lblStatus);
            Controls.Add(cmbStatus);
            Controls.Add(btnChangeStatus);
            Name = "StockMovementForm";
            Text = "Gestión de Movimientos de Stock";
            ((System.ComponentModel.ISupportInitialize)dgvFromStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvToStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMovements).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
