namespace BarStockControl
{
    partial class EventManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label lblTitle;

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(20, 100);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(700, 400);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Height = 80;
            this.topPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.topPanel.Controls.Add(this.lblTitle);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold);
            this.lblTitle.Text = "Gestión de Eventos";
            // 
            // EventManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 520);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.topPanel);
            this.Name = "EventManagementForm";
            this.Text = "Gestión de Eventos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
        }
    }
} 
