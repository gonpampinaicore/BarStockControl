
using BarStockControl.Data;
using BarStockControl.DTOs;
using BarStockControl.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BarStockControl
{
    public partial class RoleForm : Form
    {
        private readonly RoleService _roleService;
        private readonly PermissionService _permissionService;
        private RoleDto _selectedRole;

        public RoleForm()
        {
            InitializeComponent();
            _roleService = new RoleService(new XmlDataManager("Xml/data.xml"));
            _permissionService = new PermissionService(new XmlDataManager("Xml/data.xml"));
            LoadPermissions();
            LoadRoles();
        }

        private void LoadRoles()
        {
            try
            {
                var roles = _roleService.GetAllRoles();

                if (chkOnlyActive.Checked)
                    roles = roles.Where(r => r.IsActive).ToList();

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var filter = txtSearch.Text.ToLower();
                    roles = roles.Where(r =>
                        r.Name.ToLower().Contains(filter) ||
                        r.Description.ToLower().Contains(filter)).ToList();
                }

                dgvRoles.DataSource = roles;

                clbRoles.Items.Clear();
                foreach (var role in roles)
                {
                    clbRoles.Items.Add(role, false);
                }
                clbRoles.DisplayMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar roles: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPermissions()
        {
            try
            {
                clbPermissions.Items.Clear();
                var permissions = _permissionService.GetAllPermissions().Select(_permissionService.ToDto).ToList();
                foreach (var p in permissions)
                {
                    clbPermissions.Items.Add(p, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar permisos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var roleDto = GetRoleFromForm();
                var errors = _roleService.CreateRole(roleDto);

                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadRoles();
            }
            catch
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedRole == null)
                {
                    MessageBox.Show("Seleccioná un rol para actualizar.");
                    return;
                }

                var roleDto = GetRoleFromForm();
                roleDto.Id = _selectedRole.Id;

                var errors = _roleService.UpdateRole(roleDto);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join("\n", errors), "Errores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClearForm();
                LoadRoles();
            }
            catch
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedRole == null)
                {
                    MessageBox.Show("Seleccioná un rol para eliminar.");
                    return;
                }

                var confirm = MessageBox.Show("¿Estás seguro de eliminar este rol?", "Confirmar", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _roleService.DeleteRole(_selectedRole.Id);
                    ClearForm();
                    LoadRoles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    _selectedRole = (RoleDto)dgvRoles.Rows[e.RowIndex].DataBoundItem;

                    txtName.Text = _selectedRole.Name;
                    txtDescription.Text = _selectedRole.Description;
                    chkActive.Checked = _selectedRole.IsActive;

                    for (int i = 0; i < clbPermissions.Items.Count; i++)
                    {
                        if (clbPermissions.Items[i] is PermissionDto p)
                            clbPermissions.SetItemChecked(i, _selectedRole.PermissionIds.Contains(p.Id));
                    }

                    for (int i = 0; i < clbRoles.Items.Count; i++)
                    {
                        if (clbRoles.Items[i] is RoleDto r)
                            clbRoles.SetItemChecked(i, _selectedRole.RoleIds.Contains(r.Id));
                    }
                }
            }
            catch
            {
                MessageBox.Show("Lo siento, algo salió mal. Por favor, intenta nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private RoleDto GetRoleFromForm()
        {
            var selectedPermissionIds = clbPermissions.CheckedItems.Cast<PermissionDto>().Select(p => p.Id).ToList();
            var selectedRoleIds = clbRoles.CheckedItems.Cast<RoleDto>().Select(r => r.Id).ToList();

            return new RoleDto
            {
                Id = _selectedRole?.Id ?? 0,
                Name = txtName.Text,
                Description = txtDescription.Text,
                IsActive = chkActive.Checked,
                PermissionIds = selectedPermissionIds,
                RoleIds = selectedRoleIds
            };
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtDescription.Clear();
            chkActive.Checked = true;
            _selectedRole = null;

            for (int i = 0; i < clbPermissions.Items.Count; i++)
                clbPermissions.SetItemChecked(i, false);

            for (int i = 0; i < clbRoles.Items.Count; i++)
                clbRoles.SetItemChecked(i, false);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => LoadRoles();
        private void chkOnlyActive_CheckedChanged(object sender, EventArgs e) => LoadRoles();
    }
}
