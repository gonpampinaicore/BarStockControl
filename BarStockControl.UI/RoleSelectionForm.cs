using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BarStockControl.Services;
using BarStockControl.Data;
using BarStockControl.DTOs;

namespace BarStockControl.UI
{
    public partial class RoleSelectionForm : Form
    {
        private readonly List<RoleDto> _availableRoles;
        public RoleDto SelectedRole { get; private set; }

        public RoleSelectionForm(List<RoleDto> availableRoles, string title)
        {
            InitializeComponent();
            _availableRoles = availableRoles;
            this.Text = title;
            LoadRoles();
        }

        private void LoadRoles()
        {
            lstRoles.Items.Clear();
            var sortedRoles = _availableRoles.OrderByDescending(r => r.Name).ToList();
            
            foreach (var role in sortedRoles)
            {
                var item = new ListViewItem(role.Name);
                item.SubItems.Add(role.Description);
                item.SubItems.Add(role.IsActive ? "Activo" : "Inactivo");
                item.Tag = role;
                lstRoles.Items.Add(item);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstRoles.SelectedItems.Count > 0)
            {
                SelectedRole = (RoleDto)lstRoles.SelectedItems[0].Tag;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccione un rol.", "Aviso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lstRoles_DoubleClick(object sender, EventArgs e)
        {
            btnOK_Click(sender, e);
        }
    }
} 
 