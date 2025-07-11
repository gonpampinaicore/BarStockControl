namespace BarStockControl
{
    partial class StatisticsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.FlowLayoutPanel selectorsPanel;
        private System.Windows.Forms.TableLayoutPanel chartsTableLayout;
        private System.Windows.Forms.ComboBox cboEventos;
        private System.Windows.Forms.ComboBox cboMeses;
        private System.Windows.Forms.ComboBox cboTipoBarra;
        private System.Windows.Forms.CheckedListBox clbProductos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSales;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPie;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPieEventos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBarras;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCostVsSales;
        private System.Windows.Forms.Label lblTotalVentas;
        private System.Windows.Forms.Label lblTotalTragos;

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
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.selectorsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.chartsTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.cboEventos = new System.Windows.Forms.ComboBox();
            this.cboMeses = new System.Windows.Forms.ComboBox();
            this.cboTipoBarra = new System.Windows.Forms.ComboBox();
            this.clbProductos = new System.Windows.Forms.CheckedListBox();
            this.chartSales = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartPieEventos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartBarras = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartCostVsSales = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblTotalVentas = new System.Windows.Forms.Label();
            this.lblTotalTragos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPieEventos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBarras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCostVsSales)).BeginInit();
            this.SuspendLayout();
            // mainTableLayout
            this.mainTableLayout.ColumnCount = 1;
            this.mainTableLayout.RowCount = 2;
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.Controls.Add(this.selectorsPanel, 0, 0);
            this.mainTableLayout.Controls.Add(this.chartsTableLayout, 0, 1);
            // selectorsPanel
            this.selectorsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectorsPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.selectorsPanel.WrapContents = false;
            this.selectorsPanel.Controls.Add(this.cboEventos);
            this.selectorsPanel.Controls.Add(this.cboMeses);
            this.selectorsPanel.Controls.Add(this.cboTipoBarra);
            this.clbProductos.Width = 250;
            this.clbProductos.Height = 100;
            this.selectorsPanel.Controls.Add(this.clbProductos);
            this.selectorsPanel.Controls.Add(this.lblTotalVentas);
            this.selectorsPanel.Controls.Add(this.lblTotalTragos);
            // cboEventos
            this.cboEventos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEventos.Width = 180;
            // cboMeses
            this.cboMeses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMeses.Width = 180;
            // cboTipoBarra
            this.cboTipoBarra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoBarra.Width = 180;
            // clbProductos
            this.clbProductos.CheckOnClick = true;
            this.clbProductos.DisplayMember = "Name";
            // lblTotalVentas
            this.lblTotalVentas.Text = "Total de ventas: $0";
            this.lblTotalVentas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotalVentas.Width = 180;
            // lblTotalTragos
            this.lblTotalTragos.Text = "Total de tragos vendidos: 0";
            this.lblTotalTragos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotalTragos.Width = 200;
            // chartsTableLayout
            this.chartsTableLayout.ColumnCount = 2;
            this.chartsTableLayout.RowCount = 3;
            this.chartsTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.chartsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.chartsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.chartsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.chartsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.chartsTableLayout.Controls.Add(this.chartSales, 0, 0);
            this.chartsTableLayout.Controls.Add(this.chartPie, 1, 0);
            this.chartsTableLayout.Controls.Add(this.chartPieEventos, 0, 1);
            this.chartsTableLayout.Controls.Add(this.chartBarras, 1, 1);
            this.chartsTableLayout.Controls.Add(this.chartCostVsSales, 0, 2);
            this.chartsTableLayout.SetColumnSpan(this.chartCostVsSales, 2);
            // chartSales
            this.chartSales.Dock = System.Windows.Forms.DockStyle.Fill;
            // chartPie
            this.chartPie.Dock = System.Windows.Forms.DockStyle.Fill;
            // chartPieEventos
            this.chartPieEventos.Dock = System.Windows.Forms.DockStyle.Fill;
            // chartBarras
            this.chartBarras.Dock = System.Windows.Forms.DockStyle.Fill;
            // chartCostVsSales
            this.chartCostVsSales.Dock = System.Windows.Forms.DockStyle.Fill;
            // StatisticsForm
            this.ClientSize = new System.Drawing.Size(1200, 900);
            this.Controls.Add(this.mainTableLayout);
            this.Name = "StatisticsForm";
            this.Text = "Estadísticas de Ventas";
            ((System.ComponentModel.ISupportInitialize)(this.chartSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPieEventos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBarras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCostVsSales)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
