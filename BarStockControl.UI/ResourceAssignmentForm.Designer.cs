namespace BarStockControl.Forms.Assignments
{
    partial class ResourceAssignmentForm
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbEvent;
        private ComboBox cmbResourceType;
        private ComboBox cmbResource;
        private ComboBox cmbUser;
        private Label lblEvent;
        private Label lblResourceType;
        private Label lblResource;
        private Label lblUser;
        private Button btnAdd;
        private Button btnSave;
        private Button btnRemove;
        private DataGridView dgvAssignments;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            cmbEvent = new ComboBox();
            cmbResourceType = new ComboBox();
            cmbResource = new ComboBox();
            cmbUser = new ComboBox();
            lblEvent = new Label();
            lblResourceType = new Label();
            lblResource = new Label();
            lblUser = new Label();
            btnAdd = new Button();
            btnSave = new Button();
            btnRemove = new Button();
            dgvAssignments = new DataGridView();

            SuspendLayout();

            lblEvent.Text = "Evento:";
            lblEvent.Location = new System.Drawing.Point(20, 20);
            lblEvent.Size = new System.Drawing.Size(100, 23);

            cmbEvent.Location = new System.Drawing.Point(130, 20);
            cmbEvent.Size = new System.Drawing.Size(300, 23);

            lblResourceType.Text = "Tipo de recurso:";
            lblResourceType.Location = new System.Drawing.Point(20, 60);
            lblResourceType.Size = new System.Drawing.Size(100, 23);

            cmbResourceType.Location = new System.Drawing.Point(130, 60);
            cmbResourceType.Size = new System.Drawing.Size(200, 23);
            cmbResourceType.SelectedIndexChanged += cmbResourceType_SelectedIndexChanged;

            lblResource.Text = "Recurso:";
            lblResource.Location = new System.Drawing.Point(20, 100);
            lblResource.Size = new System.Drawing.Size(100, 23);

            cmbResource.Location = new System.Drawing.Point(130, 100);
            cmbResource.Size = new System.Drawing.Size(200, 23);

            lblUser.Text = "Usuario:";
            lblUser.Location = new System.Drawing.Point(20, 140);
            lblUser.Size = new System.Drawing.Size(100, 23);

            cmbUser.Location = new System.Drawing.Point(130, 140);
            cmbUser.Size = new System.Drawing.Size(200, 23);

            btnAdd.Text = "Agregar asignación";
            btnAdd.Location = new System.Drawing.Point(350, 140);
            btnAdd.Size = new System.Drawing.Size(150, 30);
            btnAdd.Click += btnAdd_Click;

            dgvAssignments.Location = new System.Drawing.Point(20, 190);
            dgvAssignments.Size = new System.Drawing.Size(600, 200);
            dgvAssignments.Columns.Add("ResourceType", "Tipo");
            dgvAssignments.Columns.Add("ResourceName", "Recurso");
            dgvAssignments.Columns.Add("UserName", "Usuario");

            btnRemove.Text = "Eliminar asignación seleccionada";
            btnRemove.Location = new System.Drawing.Point(350, 410);
            btnRemove.Size = new System.Drawing.Size(160, 30);
            btnRemove.Click += btnRemove_Click;

            btnSave.Text = "Guardar";
            btnSave.Location = new System.Drawing.Point(520, 410);
            btnSave.Size = new System.Drawing.Size(100, 30);
            btnSave.Click += btnSave_Click;

            ClientSize = new System.Drawing.Size(700, 480);
            Controls.Add(lblEvent);
            Controls.Add(cmbEvent);
            Controls.Add(lblResourceType);
            Controls.Add(cmbResourceType);
            Controls.Add(lblResource);
            Controls.Add(cmbResource);
            Controls.Add(lblUser);
            Controls.Add(cmbUser);
            Controls.Add(btnAdd);
            Controls.Add(dgvAssignments);
            Controls.Add(btnRemove);
            Controls.Add(btnSave);

            Name = "ResourceAssignmentForm";
            Text = "Asignación de Recursos por Evento";

            ResumeLayout(false);
        }
    }
}
