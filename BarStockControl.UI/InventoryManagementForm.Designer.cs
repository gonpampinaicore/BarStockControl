namespace BarStockControl.UI
{
    partial class InventoryManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label lblTitle;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "CS0414:El campo está asignado pero su valor nunca se usa")]
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBack = new System.Windows.Forms.Button();
            this.topPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();

            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Height = 80;
            this.topPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.topPanel.Controls.Add(this.lblTitle);
            this.topPanel.Controls.Add(this.btnBack);

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold);
            this.lblTitle.Text = "Gestión de Productos e Inventario";

            // 
            // btnBack
            // 
            this.btnBack.Text = "Volver al Menú Principal";
            this.btnBack.Size = new System.Drawing.Size(150, 35);
            this.btnBack.Location = new System.Drawing.Point(500, 20);
            this.btnBack.TabIndex = 99;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(20, 100);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(700, 400);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.AutoScroll = true;

            // 
            // InventoryManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(740, 520);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.topPanel);
            this.Name = "InventoryManagementForm";
            this.Text = "Gestión de Productos e Inventario";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
        }
    }
} 
