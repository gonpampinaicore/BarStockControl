namespace BarStockControl
{
    partial class LiveEvent
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblEventName = new System.Windows.Forms.Label();
            this.lblEventDate = new System.Windows.Forms.Label();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResourceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnIngresar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblEventName
            // 
            this.lblEventName.AutoSize = true;
            this.lblEventName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEventName.Location = new System.Drawing.Point(12, 9);
            this.lblEventName.Name = "lblEventName";
            this.lblEventName.Size = new System.Drawing.Size(89, 21);
            this.lblEventName.TabIndex = 0;
            this.lblEventName.Text = "Evento: ...";
            // 
            // lblEventDate
            // 
            this.lblEventDate.AutoSize = true;
            this.lblEventDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblEventDate.Location = new System.Drawing.Point(12, 40);
            this.lblEventDate.Name = "lblEventDate";
            this.lblEventDate.Size = new System.Drawing.Size(89, 19);
            this.lblEventDate.TabIndex = 1;
            this.lblEventDate.Text = "Fecha: ...";
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.FirstName,
            this.LastName,
            this.Email,
            this.ResourceType,
            this.btnIngresar});
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.Location = new System.Drawing.Point(0, 0);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowTemplate.Height = 25;
            this.dgvUsers.Size = new System.Drawing.Size(800, 400);
            this.dgvUsers.TabIndex = 2;
            this.dgvUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellContentClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // FirstName
            // 
            this.FirstName.DataPropertyName = "FirstName";
            this.FirstName.HeaderText = "Nombre";
            this.FirstName.Name = "FirstName";
            this.FirstName.ReadOnly = true;
            // 
            // LastName
            // 
            this.LastName.DataPropertyName = "LastName";
            this.LastName.HeaderText = "Apellido";
            this.LastName.Name = "LastName";
            this.LastName.ReadOnly = true;
            // 
            // Email
            // 
            this.Email.DataPropertyName = "Email";
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            // 
            // ResourceType
            // 
            this.ResourceType.DataPropertyName = "ResourceType";
            this.ResourceType.HeaderText = "Sector";
            this.ResourceType.Name = "ResourceType";
            this.ResourceType.ReadOnly = true;
            // 
            // btnIngresar
            // 
            this.btnIngresar.HeaderText = "Acción";
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.ReadOnly = true;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseColumnTextForButtonValue = true;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(120, 30);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Evento Vivo";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 50);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblEventName);
            this.panel2.Controls.Add(this.lblEventDate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 70);
            this.panel2.TabIndex = 5;
            // 
            // LiveEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "LiveEvent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Evento Vivo";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblEventName;
        private Label lblEventDate;
        private DataGridView dgvUsers;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn FirstName;
        private DataGridViewTextBoxColumn LastName;
        private DataGridViewTextBoxColumn Email;
        private DataGridViewTextBoxColumn ResourceType;
        private DataGridViewButtonColumn btnIngresar;
        private Label lblTitle;
        private Panel panel1;
        private Panel panel2;
    }
}
