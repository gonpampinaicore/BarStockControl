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
            mainTableLayout = new TableLayoutPanel();
            selectorsPanel = new FlowLayoutPanel();
            cboEventos = new ComboBox();
            cboMeses = new ComboBox();
            cboTipoBarra = new ComboBox();
            clbProductos = new CheckedListBox();
            chartsTableLayout = new TableLayoutPanel();
            chartSales = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartBarras = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartPieEventos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartCostVsSales = new System.Windows.Forms.DataVisualization.Charting.Chart();
            mainTableLayout.SuspendLayout();
            selectorsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainTableLayout
            // 
            mainTableLayout.ColumnCount = 1;
            mainTableLayout.Controls.Clear();
            mainTableLayout.RowCount = 3;
            mainTableLayout.RowStyles.Clear();
            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // selectors
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // totales
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // charts
            mainTableLayout.Controls.Add(selectorsPanel, 0, 0);
            mainTableLayout.Controls.Add(chartsTableLayout, 0, 2);
            mainTableLayout.Dock = DockStyle.Fill;
            mainTableLayout.Location = new Point(0, 0);
            mainTableLayout.Name = "mainTableLayout";
            mainTableLayout.Size = new Size(1920, 1063);
            mainTableLayout.TabIndex = 0;
            // 
            // selectorsPanel
            // 
            selectorsPanel.Controls.Add(cboEventos);
            selectorsPanel.Controls.Add(cboMeses);
            selectorsPanel.Controls.Add(cboTipoBarra);
            selectorsPanel.Controls.Add(clbProductos);
            selectorsPanel.Dock = DockStyle.Fill;
            selectorsPanel.Location = new Point(3, 3);
            selectorsPanel.Name = "selectorsPanel";
            selectorsPanel.Padding = new Padding(10);
            selectorsPanel.Size = new Size(1914, 54);
            selectorsPanel.TabIndex = 0;
            selectorsPanel.WrapContents = false;
            // 
            // cboEventos
            // 
            cboEventos.Location = new Point(13, 13);
            cboEventos.Name = "cboEventos";
            cboEventos.Size = new Size(121, 23);
            cboEventos.TabIndex = 0;
            // 
            // cboMeses
            // 
            cboMeses.Location = new Point(140, 13);
            cboMeses.Name = "cboMeses";
            cboMeses.Size = new Size(121, 23);
            cboMeses.TabIndex = 1;
            // 
            // cboTipoBarra
            // 
            cboTipoBarra.Location = new Point(267, 13);
            cboTipoBarra.Name = "cboTipoBarra";
            cboTipoBarra.Size = new Size(121, 23);
            cboTipoBarra.TabIndex = 2;
            // 
            // clbProductos
            // 
            clbProductos.Location = new Point(394, 13);
            clbProductos.Name = "clbProductos";
            clbProductos.Size = new Size(120, 94);
            clbProductos.TabIndex = 3;
            // 
            // chartsTableLayout
            // 
            chartsTableLayout.ColumnCount = 2;
            chartsTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            chartsTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            chartsTableLayout.Dock = DockStyle.Fill;
            chartsTableLayout.Location = new Point(3, 63);
            chartsTableLayout.Name = "chartsTableLayout";
            chartsTableLayout.Padding = new Padding(10);
            chartsTableLayout.RowCount = 3;
            chartsTableLayout.RowStyles.Clear();
            chartsTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            chartsTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            chartsTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
            chartsTableLayout.Size = new Size(1914, 997);
            chartsTableLayout.TabIndex = 1;
            chartsTableLayout.Controls.Add(chartSales, 0, 0);
            chartsTableLayout.Controls.Add(chartPie, 1, 0);
            chartsTableLayout.Controls.Add(chartBarras, 0, 1);
            chartsTableLayout.Controls.Add(chartPieEventos, 1, 1);
            chartsTableLayout.Controls.Add(chartCostVsSales, 0, 2);
            chartSales.Dock = DockStyle.Fill;
            chartPie.Dock = DockStyle.Fill;
            chartBarras.Dock = DockStyle.Fill;
            chartPieEventos.Dock = DockStyle.Fill;
            chartCostVsSales.Dock = DockStyle.Fill;
            chartSales.Name = "chartSales";
            chartPie.Name = "chartPie";
            chartBarras.Name = "chartBarras";
            chartPieEventos.Name = "chartPieEventos";
            chartCostVsSales.Name = "chartCostVsSales";
            // 
            // StatisticsForm
            // 
            ClientSize = new Size(1920, 1063);
            Controls.Add(mainTableLayout);
            Name = "StatisticsForm";
            Text = "Estadísticas de Ventas";
            WindowState = FormWindowState.Maximized;
            mainTableLayout.ResumeLayout(false);
            selectorsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
