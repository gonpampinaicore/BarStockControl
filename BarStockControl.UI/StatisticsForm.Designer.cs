namespace BarStockControl
{
    partial class StatisticsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSales;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPie;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTotalVentas;
        private System.Windows.Forms.Label lblTotalTragos;
        private System.Windows.Forms.ComboBox cboEventos;

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
            this.chartSales = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotalVentas = new System.Windows.Forms.Label();
            this.lblTotalTragos = new System.Windows.Forms.Label();
            this.cboEventos = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(this.cboEventos, 0, 0);
            this.tableLayoutPanel1.SetColumnSpan(this.cboEventos, 2);
            this.tableLayoutPanel1.Controls.Add(this.chartSales, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chartPie, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTotalVentas, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblTotalTragos, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1000, 500);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // chartSales
            // 
            this.chartSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartSales.Location = new System.Drawing.Point(3, 3);
            this.chartSales.Name = "chartSales";
            this.chartSales.Size = new System.Drawing.Size(594, 494);
            this.chartSales.TabIndex = 0;
            this.chartSales.Text = "chartSales";
            this.chartSales.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea());
            // 
            // chartPie
            // 
            this.chartPie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPie.Location = new System.Drawing.Point(603, 3);
            this.chartPie.Name = "chartPie";
            this.chartPie.Size = new System.Drawing.Size(394, 494);
            this.chartPie.TabIndex = 1;
            this.chartPie.Text = "chartPie";
            this.chartPie.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea());
            // 
            // lblTotalVentas
            // 
            this.lblTotalVentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalVentas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalVentas.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            this.lblTotalVentas.Text = "Total de ventas: $0";
            // 
            // lblTotalTragos
            // 
            this.lblTotalTragos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalTragos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalTragos.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            this.lblTotalTragos.Text = "Total de tragos vendidos: 0";
            // 
            // StatisticsForm
            // 
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "StatisticsForm";
            this.Text = "Estadísticas de Ventas";
            ((System.ComponentModel.ISupportInitialize)(this.chartSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
