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
            mainTableLayout = new TableLayoutPanel();
            selectorsPanel = new FlowLayoutPanel();
            cboEventos = new ComboBox();
            cboMeses = new ComboBox();
            cboTipoBarra = new ComboBox();
            clbProductos = new CheckedListBox();
            lblTotalVentas = new Label();
            lblTotalTragos = new Label();
            chartsTableLayout = new TableLayoutPanel();
            mainTableLayout.SuspendLayout();
            selectorsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainTableLayout
            // 
            mainTableLayout.ColumnCount = 1;
            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            mainTableLayout.Controls.Add(selectorsPanel, 0, 0);
            mainTableLayout.Controls.Add(chartsTableLayout, 0, 1);
            mainTableLayout.Dock = DockStyle.Fill;
            mainTableLayout.Location = new Point(0, 0);
            mainTableLayout.Name = "mainTableLayout";
            mainTableLayout.RowCount = 2;
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainTableLayout.Size = new Size(1200, 900);
            mainTableLayout.TabIndex = 0;
            // 
            // selectorsPanel
            // 
            selectorsPanel.Controls.Add(cboEventos);
            selectorsPanel.Controls.Add(cboMeses);
            selectorsPanel.Controls.Add(cboTipoBarra);
            selectorsPanel.Controls.Add(clbProductos);
            selectorsPanel.Controls.Add(lblTotalVentas);
            selectorsPanel.Controls.Add(lblTotalTragos);
            selectorsPanel.Dock = DockStyle.Fill;
            selectorsPanel.Location = new Point(3, 3);
            selectorsPanel.Name = "selectorsPanel";
            selectorsPanel.Size = new Size(1194, 124);
            selectorsPanel.TabIndex = 0;
            selectorsPanel.WrapContents = false;
            // 
            // cboEventos
            // 
            cboEventos.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEventos.Location = new Point(3, 3);
            cboEventos.Name = "cboEventos";
            cboEventos.Size = new Size(180, 23);
            cboEventos.TabIndex = 0;
            // 
            // cboMeses
            // 
            cboMeses.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMeses.Location = new Point(189, 3);
            cboMeses.Name = "cboMeses";
            cboMeses.Size = new Size(180, 23);
            cboMeses.TabIndex = 1;
            // 
            // cboTipoBarra
            // 
            cboTipoBarra.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoBarra.Location = new Point(375, 3);
            cboTipoBarra.Name = "cboTipoBarra";
            cboTipoBarra.Size = new Size(180, 23);
            cboTipoBarra.TabIndex = 2;
            // 
            // clbProductos
            // 
            clbProductos.CheckOnClick = true;
            clbProductos.DisplayMember = "Name";
            clbProductos.Location = new Point(561, 3);
            clbProductos.Name = "clbProductos";
            clbProductos.Size = new Size(250, 94);
            clbProductos.TabIndex = 3;
            // 
            // lblTotalVentas
            // 
            lblTotalVentas.Location = new Point(817, 0);
            lblTotalVentas.Name = "lblTotalVentas";
            lblTotalVentas.Size = new Size(180, 23);
            lblTotalVentas.TabIndex = 4;
            lblTotalVentas.Text = "Total de ventas: $0";
            lblTotalVentas.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalTragos
            // 
            lblTotalTragos.Location = new Point(1003, 0);
            lblTotalTragos.Name = "lblTotalTragos";
            lblTotalTragos.Size = new Size(200, 23);
            lblTotalTragos.TabIndex = 5;
            lblTotalTragos.Text = "Total de tragos vendidos: 0";
            lblTotalTragos.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chartsTableLayout
            // 
            chartsTableLayout.ColumnCount = 2;
            chartsTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            chartsTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            chartsTableLayout.Dock = DockStyle.Fill;
            chartsTableLayout.Location = new Point(3, 133);
            chartsTableLayout.Name = "chartsTableLayout";
            chartsTableLayout.RowCount = 3;
            chartsTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            chartsTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            chartsTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
            chartsTableLayout.Size = new Size(1194, 764);
            chartsTableLayout.TabIndex = 1;
            // 
            // StatisticsForm
            // 
            ClientSize = new Size(1200, 900);
            Controls.Add(mainTableLayout);
            Name = "StatisticsForm";
            Text = "Estadísticas de Ventas";
            mainTableLayout.ResumeLayout(false);
            selectorsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
