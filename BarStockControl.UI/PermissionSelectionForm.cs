using BarStockControl.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BarStockControl.UI
{
    public partial class PermissionSelectionForm : Form
    {
        private readonly List<PermissionDto> _availablePermissions;
        public List<PermissionDto> SelectedPermissions { get; private set; } = new List<PermissionDto>();

        public PermissionSelectionForm(List<PermissionDto> availablePermissions, string title)
        {
            InitializeComponent();
            _availablePermissions = availablePermissions;
            this.Text = title;
            LoadPermissions();
        }

        private void LoadPermissions()
        {
            dgvPermissions.Rows.Clear();
            var sortedPermissions = _availablePermissions.OrderByDescending(p => p.Name).ToList();
            
            foreach (var permission in sortedPermissions)
            {
                var rowIndex = dgvPermissions.Rows.Add();
                dgvPermissions.Rows[rowIndex].Cells["chkSelected"].Value = false;
                dgvPermissions.Rows[rowIndex].Cells["colName"].Value = permission.Name;
                dgvPermissions.Rows[rowIndex].Cells["colDescription"].Value = permission.Description;
                dgvPermissions.Rows[rowIndex].Cells["colStatus"].Value = permission.IsActive ? "Activo" : "Inactivo";
                dgvPermissions.Rows[rowIndex].Tag = permission;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvPermissions.Rows)
            {
                row.Cells["chkSelected"].Value = true;
            }
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvPermissions.Rows)
            {
                row.Cells["chkSelected"].Value = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedPermissions.Clear();
            
            foreach (DataGridViewRow row in dgvPermissions.Rows)
            {
                var isSelected = (bool)row.Cells["chkSelected"].Value;
                if (isSelected)
                {
                    var permission = (PermissionDto)row.Tag;
                    SelectedPermissions.Add(permission);
                }
            }

            if (SelectedPermissions.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un permiso.", "Aviso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvPermissions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvPermissions.Columns["chkSelected"].Index && e.RowIndex >= 0)
            {
                var currentValue = (bool)dgvPermissions.Rows[e.RowIndex].Cells["chkSelected"].Value;
                dgvPermissions.Rows[e.RowIndex].Cells["chkSelected"].Value = !currentValue;
            }
        }
    }
} 
 