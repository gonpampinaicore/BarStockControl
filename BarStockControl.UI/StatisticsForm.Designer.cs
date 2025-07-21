namespace BarStockControl.UI
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
            chartPieEventos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartBarras = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartCostVsSales = new System.Windows.Forms.DataVisualization.Charting.Chart();
            mainTableLayout.SuspendLayout();
            selectorsPanel.SuspendLayout();
            chartsTableLayout.SuspendLayout();
            SuspendLayout();
            // 
            // mainTableLayout
            // 
            mainTableLayout.ColumnCount = 1;
            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTableLayout.Controls.Add(selectorsPanel, 0, 0);
            mainTableLayout.Controls.Add(chartsTableLayout, 0, 2);
            mainTableLayout.Dock = DockStyle.Fill;
            mainTableLayout.Location = new Point(0, 0);
            mainTableLayout.Name = "mainTableLayout";
            mainTableLayout.RowCount = 3;
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
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
            chartsTableLayout.Controls.Add(chartSales, 0, 0);
            chartsTableLayout.Controls.Add(chartPie, 1, 0);
            chartsTableLayout.Controls.Add(chartPieEventos, 0, 1);
            chartsTableLayout.Controls.Add(chartBarras, 1, 1);
            chartsTableLayout.Controls.Add(chartCostVsSales, 0, 2);
            chartsTableLayout.Dock = DockStyle.Fill;
            chartsTableLayout.Location = new Point(3, 103);
            chartsTableLayout.Name = "chartsTableLayout";
            chartsTableLayout.Padding = new Padding(10);
            chartsTableLayout.RowCount = 3;
            chartsTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            chartsTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            chartsTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
            chartsTableLayout.Size = new Size(1914, 957);
            chartsTableLayout.TabIndex = 1;
            // 
            // chartSales
            // 
            chartSales.Dock = DockStyle.Fill;
            chartSales.Location = new Point(13, 13);
            chartSales.Name = "chartSales";
            chartSales.Size = new Size(940, 300);
            chartSales.TabIndex = 0;
            // 
            // chartPie
            // 
            chartPie.Dock = DockStyle.Fill;
            chartPie.Location = new Point(973, 13);
            chartPie.Name = "chartPie";
            chartPie.Size = new Size(940, 300);
            chartPie.TabIndex = 1;
            // 
            // chartPieEventos
            // 
            chartPieEventos.Dock = DockStyle.Fill;
            chartPieEventos.Location = new Point(13, 333);
            chartPieEventos.Name = "chartPieEventos";
            chartPieEventos.Size = new Size(940, 300);
            chartPieEventos.TabIndex = 2;
            // 
            // chartBarras
            // 
            chartBarras.Dock = DockStyle.Fill;
            chartBarras.Location = new Point(973, 333);
            chartBarras.Name = "chartBarras";
            chartBarras.Size = new Size(940, 300);
            chartBarras.TabIndex = 3;
            // 
            // chartCostVsSales
            // 
            chartCostVsSales.Dock = DockStyle.Fill;
            chartCostVsSales.Location = new Point(13, 653);
            chartCostVsSales.Name = "chartCostVsSales";
            chartCostVsSales.Size = new Size(940, 300);
            chartCostVsSales.TabIndex = 4;
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
            chartsTableLayout.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
