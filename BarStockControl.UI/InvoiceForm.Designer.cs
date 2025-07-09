using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace BarStockControl.Forms.Invoices
{
    partial class InvoiceForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblEvent;
        private Label lblCashier;
        private Label lblCashRegister;
        private Label lblDate;
        private Label lblPayment;
        private Label lblStatus;
        private Label lblTotal;
        private DataGridView dgvItems;
        private Button btnClose;
        private Button btnPrintPdf;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblEvent = new Label();
            lblCashier = new Label();
            lblCashRegister = new Label();
            lblDate = new Label();
            lblPayment = new Label();
            lblStatus = new Label();
            lblTotal = new Label();
            dgvItems = new DataGridView();
            btnClose = new Button();
            btnPrintPdf = new Button();

            ((System.ComponentModel.ISupportInitialize)(dgvItems)).BeginInit();
            SuspendLayout();

            lblEvent.Location = new System.Drawing.Point(20, 20);
            lblEvent.Name = "lblEvent";
            lblEvent.Size = new System.Drawing.Size(400, 20);
            lblEvent.TabIndex = 0;

            lblCashier.Location = new System.Drawing.Point(20, 50);
            lblCashier.Name = "lblCashier";
            lblCashier.Size = new System.Drawing.Size(400, 20);
            lblCashier.TabIndex = 1;

            lblCashRegister.Location = new System.Drawing.Point(20, 80);
            lblCashRegister.Name = "lblCashRegister";
            lblCashRegister.Size = new System.Drawing.Size(400, 20);
            lblCashRegister.TabIndex = 2;

            lblDate.Location = new System.Drawing.Point(20, 110);
            lblDate.Name = "lblDate";
            lblDate.Size = new System.Drawing.Size(400, 20);
            lblDate.TabIndex = 3;

            lblPayment.Location = new System.Drawing.Point(20, 140);
            lblPayment.Name = "lblPayment";
            lblPayment.Size = new System.Drawing.Size(400, 20);
            lblPayment.TabIndex = 4;

            lblStatus.Location = new System.Drawing.Point(20, 170);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(400, 20);
            lblStatus.TabIndex = 5;

            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Location = new System.Drawing.Point(20, 210);
            dgvItems.Name = "dgvItems";
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersVisible = false;
            dgvItems.Size = new System.Drawing.Size(600, 200);
            dgvItems.TabIndex = 6;
            dgvItems.Columns.Add("DrinkName", "Trago");
            dgvItems.Columns.Add("Quantity", "Cantidad");
            dgvItems.Columns.Add("UnitPrice", "Precio Unitario");
            dgvItems.Columns.Add("Discount", "Descuento");
            dgvItems.Columns.Add("Subtotal", "Subtotal");

            lblTotal.Location = new System.Drawing.Point(420, 420);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new System.Drawing.Size(200, 20);
            lblTotal.TabIndex = 7;
            lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            btnClose.Location = new System.Drawing.Point(20, 460);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(100, 30);
            btnClose.TabIndex = 8;
            btnClose.Text = "Cerrar";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += new System.EventHandler(btnClose_Click);

            btnPrintPdf.Location = new System.Drawing.Point(140, 460);
            btnPrintPdf.Name = "btnPrintPdf";
            btnPrintPdf.Size = new System.Drawing.Size(120, 30);
            btnPrintPdf.TabIndex = 9;
            btnPrintPdf.Text = "Imprimir PDF";
            btnPrintPdf.UseVisualStyleBackColor = true;
            btnPrintPdf.Click += new System.EventHandler(btnPrintPdf_Click);

            ClientSize = new System.Drawing.Size(650, 510);
            Controls.Add(lblEvent);
            Controls.Add(lblCashier);
            Controls.Add(lblCashRegister);
            Controls.Add(lblDate);
            Controls.Add(lblPayment);
            Controls.Add(lblStatus);
            Controls.Add(dgvItems);
            Controls.Add(lblTotal);
            Controls.Add(btnClose);
            Controls.Add(btnPrintPdf);
            Name = "InvoiceForm";
            Text = "Factura";

            ((System.ComponentModel.ISupportInitialize)(dgvItems)).EndInit();
            ResumeLayout(false);
        }
    }
}
