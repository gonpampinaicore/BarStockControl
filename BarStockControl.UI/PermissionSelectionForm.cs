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
        public PermissionDto SelectedPermission { get; private set; }

        public PermissionSelectionForm(List<PermissionDto> availablePermissions, string title)
        {
            InitializeComponent();
            _availablePermissions = availablePermissions;
            this.Text = title;
            LoadPermissions();
        }

        private void LoadPermissions()
        {
            lstPermissions.Items.Clear();
            var sortedPermissions = _availablePermissions.OrderByDescending(p => p.Name).ToList();
            
            foreach (var permission in sortedPermissions)
            {
                var item = new ListViewItem(permission.Name);
                item.SubItems.Add(permission.Description);
                item.SubItems.Add(permission.IsActive ? "Activo" : "Inactivo");
                item.Tag = permission;
                lstPermissions.Items.Add(item);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstPermissions.SelectedItems.Count > 0)
            {
                SelectedPermission = (PermissionDto)lstPermissions.SelectedItems[0].Tag;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccione un permiso.", "Aviso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lstPermissions_DoubleClick(object sender, EventArgs e)
        {
            btnOK_Click(sender, e);
        }
    }
} 
 